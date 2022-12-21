using Jorda.Server.Domain.Events.ToDoItem;
using MediatR;
using Microsoft.Extensions.Logging;
namespace Jorda.Server.Application.CQRS.ToDoItem.EventHandlers;

public class ToDoItemCreatedEventHandler : INotificationHandler<ToDoItemCreatedEvent>
{
    private readonly ILogger<ToDoItemCreatedEventHandler> _logger;

    public ToDoItemCreatedEventHandler(ILogger<ToDoItemCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ToDoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventData}", notification.GetType().Name, notification.GetEventEntityDescription());
        return Task.CompletedTask;
    }
}
