using Jorda.Server.Domain.Events.ToDoItem;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Jorda.Server.Application.CQRS.ToDoItem.EventHandlers;


public class ToDoItemAbandonedEventHandler : INotificationHandler<ToDoItemAbandonedEvent>
{
    private readonly ILogger<ToDoItemAbandonedEventHandler> _logger;

    public ToDoItemAbandonedEventHandler(ILogger<ToDoItemAbandonedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ToDoItemAbandonedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventData}", notification.GetType().Name, notification.GetEventEntityDescription());
        return Task.CompletedTask;
    }
}
