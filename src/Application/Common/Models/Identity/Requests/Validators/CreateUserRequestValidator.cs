using FluentValidation;
using Jorda.Server.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Resources.Validation.Identity.CreateUserRequestValidation;

namespace Jorda.Server.Application.Common.Models.Identity.Requests.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    private readonly IUserService _userService;

    public CreateUserRequestValidator(IUserService userService)
    {
        _userService = userService;
        RuleFor(request => request.Email)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(CreateUserRequestValidation.EmailRequired)
            .EmailAddress().WithMessage(CreateUserRequestValidation.EmailFormatInvalid)
            .MustAsync(BeUniqueEmail).WithMessage(CreateUserRequestValidation.EmailUnique); ;
        RuleFor(request => request.Password)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(CreateUserRequestValidation.PasswordRequired)
            .MinimumLength(8).WithMessage(string.Format(CreateUserRequestValidation.PasswordTooShort, 8))
            .Matches(@"[0-9]").WithMessage(CreateUserRequestValidation.PasswordRequiresDigit);
        RuleFor(request => request.ConfirmPassword)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(CreateUserRequestValidation.PasswordConfirmationRequired)
            .Equal(request => request.Password).WithMessage(CreateUserRequestValidation.PasswordsDontMatch);
    }
    public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return !await _userService.UserExists(email);
    }
}
