using Jorda.Server.Domain.Events.Goal;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jorda.Application.CQRS.Goal.EventHandlers;

public class GoalDeletedEventHandler : INotificationHandler<GoalDeletedEvent>
{
    private readonly ILogger<GoalDeletedEventHandler> _logger;

    public GoalDeletedEventHandler(ILogger<GoalDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(GoalDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventEntityDescription}",
            notification.GetType().Name, notification.GetEventEntityDescription());
        return Task.CompletedTask;
    }
}
