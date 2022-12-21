using Jorda.Server.Domain.Events.ToDoItem;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jorda.Server.Application.CQRS.ToDoItem.EventHandlers;

public class ToDoItemDeletedEventHandler : INotificationHandler<ToDoItemDeletedEvent>
{
    private readonly ILogger<ToDoItemDeletedEventHandler> _logger;

    public ToDoItemDeletedEventHandler(ILogger<ToDoItemDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ToDoItemDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventData}", notification.GetType().Name, notification.GetEventEntityDescription());
        return Task.CompletedTask;
    }
}
