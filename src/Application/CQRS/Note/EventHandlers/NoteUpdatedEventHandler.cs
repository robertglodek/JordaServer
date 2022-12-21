using Jorda.Server.Domain.Events.Note;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jorda.Server.Application.CQRS.Note.EventHandlers;


public class NoteUpdatedEventHandler : INotificationHandler<NoteUpdatedEvent>
{
    private readonly ILogger<NoteUpdatedEventHandler> _logger;

    public NoteUpdatedEventHandler(ILogger<NoteUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(NoteUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventData}", notification.GetType().Name, notification.GetEventEntityDescription());

        return Task.CompletedTask;
    }
}
