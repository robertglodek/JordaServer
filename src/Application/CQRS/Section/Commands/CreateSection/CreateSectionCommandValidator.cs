using FluentValidation;
using Jorda.Server.Application.Common.Resources.Validation.Section;

namespace Jorda.Server.Application.CQRS.Section.Commands.CreateSection;

public class CreateSectionCommandValidator : AbstractValidator<CreateSectionCommand>
{
    public CreateSectionCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage(CreateUpdateSectionCommandValidator.NameRequired)
            .MaximumLength(30).WithMessage(string.Format(CreateUpdateSectionCommandValidator.NameMaximumLength,30));
        RuleFor(v => v.Description)
        .MaximumLength(200).WithMessage(string.Format(CreateUpdateSectionCommandValidator.DescriptionMaximumLength, 200));
    }
}
