using Jorda.Server.Application.CQRS.Notification.Queries.DTOs;
using MediatR;

namespace Jorda.Server.Application.CQRS.Notification.Queries.GetNotification;

public class GetNotificationQuery : IRequest<NotificationDTO>
{
    public Guid Id { get; set; }
}

