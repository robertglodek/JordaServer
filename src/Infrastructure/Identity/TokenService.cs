using FluentValidation;
using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Configuration;
using Jorda.Server.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Models.Identity.Requests;
using Jorda.Server.Application.Common.Models.Identity.Responses;
using Jorda.Server.Infrastructure.Common.Exceptions;
using Jorda.Server.Infrastructure.Common.Resources;
using Jorda.Server.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jorda.Server.Infrastructure.Identity;

public class TokenService : BaseService, ITokenService
{

    private readonly UserManager<JordaUser> _userManager;
    private readonly RoleManager<JordaRole> _roleManager;
    private readonly SignInManager<JordaUser> _signInManager;
    private readonly IEmailService _emailService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<JwtSettings> _jwtSettings;

    public TokenService(UserManager<JordaUser> userManager, RoleManager<JordaRole> roleManager,
        SignInManager<JordaUser> signInManager, IEmailService emailService, IServiceProvider serviceProvider, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _serviceProvider = serviceProvider;
        _jwtSettings = jwtSettings;
    }


    public async Task<TokenResponse> LoginAsync(TokenRequest request)
    {
        await ValidateAsync(_serviceProvider.GetService<IEnumerable<IValidator<TokenRequest>>>()!, request);

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(JordaUser), request.Email);
        }
        if (!user.EmailConfirmed)
        {
            throw new IdentityErrorException(IdentityGeneral.EmailNotConfirmed);
        }
        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
        {
            throw new IdentityErrorException(IdentityGeneral.PasswordNotValid);
        }
        var token = await GenerateJwtAsync(user);
        return new TokenResponse { Token = token };
    }

    private async Task<string> GenerateJwtAsync(JordaUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_jwtSettings.Value.ExpireDays);
        var claims = new List<Claim>
        {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).FirstOrDefault()!)
        };

        var token = new JwtSecurityToken(_jwtSettings.Value.Issuer,
            _jwtSettings.Value.Audience,
            expires: expires,
            claims: claims,
            signingCredentials: cred);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
