using Jorda.Server.Application.CQRS.Notification.Queries.DTOs;
using MediatR;

namespace Jorda.Server.Application.CQRS.Notification.Queries.GetNotifications
{
    public class GetNotificationsQuery : IRequest<IEnumerable<NotificationDTO>>
    {
        public bool? ActiveOnly { get; set; }
    }
}
