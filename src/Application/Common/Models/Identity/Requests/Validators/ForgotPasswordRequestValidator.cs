using FluentValidation;
using Jorda.Server.Application.Common.Resources.Validation.Identity.ForgotPasswordRequestValidation;

namespace Jorda.Server.Application.Common.Models.Identity.Requests.Validators;

public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordRequestValidator()
    {
        RuleFor(request => request.Email)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(ForgotPasswordRequestValidation.EmailRequired)
            .EmailAddress().WithMessage(ForgotPasswordRequestValidation.EmailFormatInvalid);
    }
}
