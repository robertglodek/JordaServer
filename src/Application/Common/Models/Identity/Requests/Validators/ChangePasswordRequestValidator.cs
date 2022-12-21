using FluentValidation;
using Jorda.Server.Application.Common.Resources.Validation.Identity.ChangePasswordRequestValidation;

namespace Jorda.Server.Application.Common.Models.Identity.Requests.Validators;

public class ChangePasswordRequestValidator:AbstractValidator<ChangePasswordRequest>
{
	public ChangePasswordRequestValidator()
	{
        RuleFor(request => request.Password)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(ChangePasswordRequestValidation.CurrentPasswordRequired);
        RuleFor(request => request.NewPassword)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(ChangePasswordRequestValidation.PasswordRequired)
            .MinimumLength(8).WithMessage(String.Format(ChangePasswordRequestValidation.PasswordTooShort,8))
            .Matches(@"[0-9]").WithMessage(ChangePasswordRequestValidation.PasswordRequiresDigit);
        RuleFor(request => request.ConfirmNewPassword)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(ChangePasswordRequestValidation.PasswordConfirmationRequired)
            .Equal(request => request.NewPassword).WithMessage(ChangePasswordRequestValidation.PasswordsDontMatch);
    }
}

