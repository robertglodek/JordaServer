using Jorda.Server.Domain.Enums;
using MediatR;

namespace Jorda.Server.Application.CQRS.Notification.Commands.CreateNotification;

public class CreateNotificationCommand : IRequest<Guid>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool Active { get; set; }
    public ImportanceLevel ImportanceLevel { get; set; } = ImportanceLevel.Information;
}

