using FluentValidation;
using Jorda.Server.Application.Common.Resources.Validation.ToDoItem;

namespace Jorda.Application.CQRS.ToDoItem.Commands.UpdateToDoItem;
public class UpdateToDoItemCommandValidator : AbstractValidator<UpdateToDoItemCommand>
{
    public UpdateToDoItemCommandValidator()
    {
        RuleFor(v => v.Name)
             .NotEmpty().WithMessage(CreateUpdateToDoItemCommandValidator.NameRequired)
             .MaximumLength(30).WithMessage(string.Format(CreateUpdateToDoItemCommandValidator.NameMaximumLength, 30));
        RuleFor(v => v.Description)
        .MaximumLength(200).WithMessage(string.Format(CreateUpdateToDoItemCommandValidator.DescriptionMaximumLength, 200));
    }
}
