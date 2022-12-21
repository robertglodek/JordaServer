using FluentValidation;
using Jorda.Server.Application.Common.Resources.Validation.Notification;

namespace Jorda.Server.Application.CQRS.Notification.Commands.CreateNotification;

public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage(CreateUpdateNotificationCommandValidator.NameRequired)
            .MaximumLength(30).WithMessage(string.Format(CreateUpdateNotificationCommandValidator.NameMaximumLength, 30));
        RuleFor(v => v.Description)
        .MaximumLength(200).WithMessage(string.Format(CreateUpdateNotificationCommandValidator.DescriptionMaximumLength, 200)); 
    }
}
