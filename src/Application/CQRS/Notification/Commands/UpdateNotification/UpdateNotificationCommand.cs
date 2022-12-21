using Jorda.Server.Domain.Enums;
using MediatR;

namespace Jorda.Server.Application.CQRS.Notification.Commands.UpdateNotification;

public class UpdateNotificationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool Active { get; set; }
    public ImportanceLevel ImportanceLevel { get; set; }
}
