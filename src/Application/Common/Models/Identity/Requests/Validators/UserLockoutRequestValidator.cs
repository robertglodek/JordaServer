using FluentValidation;
using Jorda.Server.Application.Common.Resources.Validation.Identity.UserLockoutRequestValidation;

namespace Jorda.Server.Application.Common.Models.Identity.Requests.Validators;

public class UserLockoutRequestValidator : AbstractValidator<UserLockoutRequest>
{
    public UserLockoutRequestValidator()
    {

        RuleFor(request => request.LockoutEnd).NotEmpty()
            .WithMessage(UserLockoutRequestValidation.LockoutEndRequired);

        RuleFor(request => request.Message).MaximumLength(100)
           .WithMessage(string.Format(UserLockoutRequestValidation.LockoutMessageMaxLength,100));
    }
}
