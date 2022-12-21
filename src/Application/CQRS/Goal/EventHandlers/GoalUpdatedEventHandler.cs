using Jorda.Server.Domain.Events.Goal;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jorda.Server.Application.CQRS.Goal.EventHandlers;

public class GoalUpdatedEventHandler : INotificationHandler<GoalUpdatedEvent>
{
    private readonly ILogger<GoalUpdatedEventHandler> _logger;

    public GoalUpdatedEventHandler(ILogger<GoalUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(GoalUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventEntityDescription}",
            notification.GetType().Name, notification.GetEventEntityDescription());
        return Task.CompletedTask;
    }
}
