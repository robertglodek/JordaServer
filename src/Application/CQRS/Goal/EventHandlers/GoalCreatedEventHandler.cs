using Jorda.Server.Domain.Events.Goal;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jorda.Application.CQRS.Goal.EventHandlers;
public class GoalCreatedEventHandler : INotificationHandler<GoalCreatedEvent>
{
    private readonly ILogger<GoalCreatedEventHandler> _logger;

    public GoalCreatedEventHandler(ILogger<GoalCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(GoalCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventEntityDescription}",
            notification.GetType().Name, notification.GetEventEntityDescription());
        return Task.CompletedTask;
    }
}
