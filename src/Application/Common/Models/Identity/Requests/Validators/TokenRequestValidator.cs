using FluentValidation;
using Jorda.Server.Application.Common.Resources.Validation.Identity.TokenRequestValidation;

namespace Jorda.Server.Application.Common.Models.Identity.Requests.Validators;

public class TokenRequestValidator : AbstractValidator<TokenRequest>
{
    public TokenRequestValidator()
    {
        RuleFor(request => request.Email).NotEmpty()
            .WithMessage(TokenRequestValidation.EmailRequired);

        RuleFor(request => request.Password).NotEmpty()
            .WithMessage(TokenRequestValidation.PasswordRequired);
    }
}
