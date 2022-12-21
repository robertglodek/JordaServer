using Jorda.Server.Domain.Events.Note;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jorda.Server.Application.CQRS.Note.EventHandlers;

public class NoteDeletedEventHandler : INotificationHandler<NoteDeletedEvent>
{
    private readonly ILogger<NoteDeletedEventHandler> _logger;

    public NoteDeletedEventHandler(ILogger<NoteDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(NoteDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventData}", notification.GetType().Name, notification.GetEventEntityDescription());

        return Task.CompletedTask;
    }
}
