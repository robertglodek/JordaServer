using MediatR;

namespace Jorda.Server.Application.CQRS.Notification.Commands.DeleteNotification;

public class DeleteNotificationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
