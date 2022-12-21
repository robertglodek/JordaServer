using Jorda.Server.Domain.Events.Note;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jorda.Server.Application.CQRS.Note.EventHandlers;

public class NoteCreatedEventHandler : INotificationHandler<NoteCreatedEvent>
{
    private readonly ILogger<NoteCreatedEventHandler> _logger;

    public NoteCreatedEventHandler(ILogger<NoteCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(NoteCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventData}", notification.GetType().Name, notification.GetEventEntityDescription());

        return Task.CompletedTask;
    }
}
