using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Models.Identity.Requests;
using Jorda.Server.Infrastructure.Common.Exceptions;
using Jorda.Server.Infrastructure.Identity;
using Jorda.Server.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text;
using Jorda.Server.Infrastructure.Common.Resources;
using Jorda.Server.Application.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Hangfire;

namespace Jorda.Infrastructure.Identity;

public class AccountService : BaseService, IAccountService
{
    private readonly UserManager<JordaUser> _userManager;
    private readonly RoleManager<JordaRole> _roleManager;
    private readonly SignInManager<JordaUser> _signInManager;
    private readonly IEmailService _emailService;
    private readonly IServiceProvider _serviceProvider;
    private readonly ICurrentUserService _currentUserService;

    public AccountService(UserManager<JordaUser> userManager, RoleManager<JordaRole> roleManager,
        SignInManager<JordaUser> signInManager, IEmailService emailService, IServiceProvider serviceProvider, ICurrentUserService currentUserService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _serviceProvider = serviceProvider;
        _currentUserService = currentUserService;
    }

    public async Task ChangePasswordAsync(ChangePasswordRequest request)
    {
        await ValidateAsync(_serviceProvider.GetService<IEnumerable<IValidator<ChangePasswordRequest>>>()!, request);
        var user = await _userManager.FindByIdAsync(_currentUserService.UserId!);
        if (user == null)
        {
            throw new NotFoundException(nameof(JordaUser), _currentUserService.UserId!);
        }    
        var identityResult = await _userManager.ChangePasswordAsync(
            user,
            request.Password,
            request.NewPassword);
        if(!identityResult.Succeeded)
        {
            throw new IdentityErrorException(identityResult.Errors.Select(n => n.Description).ToArray());
        }       
    }

    public async Task ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
    {
        await ValidateAsync(_serviceProvider.GetService<IEnumerable<IValidator<ForgotPasswordRequest>>>()!, request);
        var user = await _userManager.FindByEmailAsync(request.Email);

        if(user==null || !await _userManager.IsEmailConfirmedAsync(user))
        {
            throw new IdentityErrorException(IdentityGeneral.ErrorOccurred);
        }
        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var route = "account/reset-password";
        var endpointUri = new Uri(string.Concat($"{origin}/", route));
        var passwordResetURL = QueryHelpers.AddQueryString(endpointUri.ToString(), "Token", code);
        var mailRequest = new EmailRequest
        {
            Body = string.Format(IdentityGeneral.ResetPasswordEmail, HtmlEncoder.Default.Encode(passwordResetURL)),
            Subject = IdentityGeneral.ResetPassword,
            To = request.Email
        };
        BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(mailRequest));
    }

    public async Task<string> CreateUserAsync(CreateUserRequest request, string origin)
    {
        await ValidateAsync(_serviceProvider.GetService<IEnumerable<IValidator<CreateUserRequest>>>()!, request);
        var user = new JordaUser
        {
            Email = request.Email,
            UserName = request.Email,
            EmailConfirmed = request.AutoConfirmEmail
            
        };

        var role = await _roleManager.Roles.FirstOrDefaultAsync(n=>n.Name==request.Role);

        if(role == null)
        {
            throw new NotFoundException(nameof(JordaRole), request.Role);
        }

        var createUserResult = await _userManager.CreateAsync(user, request.Password);

        if(!createUserResult.Succeeded)
        {
            throw new IdentityErrorException(createUserResult.Errors.Select(n => n.Description).ToArray());
        }
        await _userManager.AddToRoleAsync(user,role.Name!);

        if (!request.AutoConfirmEmail)
        {
            var verificationUri = await SendVerificationEmail(user, origin);
            var mailRequest = new EmailRequest
            {
                To = user.Email,
                Body = string.Format(IdentityGeneral.ConfirmRegistrationEmail, HtmlEncoder.Default.Encode(verificationUri)),
                Subject = IdentityGeneral.ConfirmRegistration
            };
            BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(mailRequest));
        }
        return user.Id;
    }
    private async Task<string> SendVerificationEmail(JordaUser user, string origin)
    {
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var route = "api/identity/user/confirm-email/";
        var endpointUri = new Uri(string.Concat($"{origin}/", route));
        var verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "userId", user.Id.ToString());
        verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
        return verificationUri;
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest request)
    {
        await ValidateAsync(_serviceProvider.GetService<IEnumerable<IValidator<ResetPasswordRequest>>>()!, request);
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(JordaUser), request.Email);
        }
        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
        if (!result.Succeeded)
        {
            throw new IdentityErrorException(result.Errors.Select(n => n.Description).ToArray());
        }
    }

    public async Task ConfirmEmailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException(nameof(JordaUser), userId);
        }

        var result =await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            throw new IdentityErrorException(result.Errors.Select(n => n.Description).ToArray());
        }
   
    }
}
