using FluentValidation;
using Jorda.Server.Application.Common.Resources.Validation.Section;


namespace Jorda.Server.Application.CQRS.Section.Commands.UpdateSection;

public class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
{
    public UpdateSectionCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage(CreateUpdateSectionCommandValidator.NameRequired)
            .MaximumLength(30).WithMessage(string.Format(CreateUpdateSectionCommandValidator.NameMaximumLength, 30));
        RuleFor(v => v.Description)
        .MaximumLength(200).WithMessage(string.Format(CreateUpdateSectionCommandValidator.DescriptionMaximumLength, 200));
    }
}

