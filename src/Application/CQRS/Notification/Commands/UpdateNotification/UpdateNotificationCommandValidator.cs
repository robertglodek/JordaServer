using FluentValidation;
using Jorda.Server.Application.Common.Resources.Validation.Notification;

namespace Jorda.Server.Application.CQRS.Notification.Commands.UpdateNotification;

public class UpdateNotificationCommandValidator : AbstractValidator<UpdateNotificationCommand>
{
    public UpdateNotificationCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage(CreateUpdateNotificationCommandValidator.NameRequired)
            .MaximumLength(30).WithMessage(string.Format(CreateUpdateNotificationCommandValidator.NameMaximumLength, 30));
        RuleFor(v => v.Description)
        .MaximumLength(200).WithMessage(string.Format(CreateUpdateNotificationCommandValidator.DescriptionMaximumLength, 200));
    }
}

