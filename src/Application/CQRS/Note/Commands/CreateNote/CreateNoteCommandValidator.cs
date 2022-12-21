using FluentValidation;
using Jorda.Server.Application.Common.Resources.Validation.Note;

namespace Jorda.Server.Application.CQRS.Note.Commands.CreateNote
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage(CreateUpdateNoteCommandValidator.NameRequired)
                .MaximumLength(30).WithMessage(string.Format(CreateUpdateNoteCommandValidator.NameMaximumLength, 30));
            RuleFor(v => v.Description)
            .MaximumLength(200).WithMessage(string.Format(CreateUpdateNoteCommandValidator.DescriptionMaximumLength,200));
            RuleFor(v => v.Source)
            .MaximumLength(100).WithMessage(string.Format(CreateUpdateNoteCommandValidator.SourceMaximumLength,100));
        }
    }
}
