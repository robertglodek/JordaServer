using Jorda.Server.Domain.Events.ToDoItem;
using MediatR;
using Microsoft.Extensions.Logging;
namespace Jorda.Server.Application.CQRS.ToDoItem.EventHandlers;

public class ToDoItemCompletedEventHandler : INotificationHandler<ToDoItemCompletedEvent>
{
    private readonly ILogger<ToDoItemCompletedEventHandler> _logger;

    public ToDoItemCompletedEventHandler(ILogger<ToDoItemCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ToDoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventData}", notification.GetType().Name, notification.GetEventEntityDescription());
        return Task.CompletedTask;
    }
}
