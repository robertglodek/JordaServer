using Jorda.Server.Domain.Events.Section;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jorda.Server.Application.CQRS.Section.EventHandlers;

public class SectionCreatedEventHandler : INotificationHandler<SectionCreatedEvent>
{
    private readonly ILogger<SectionCreatedEventHandler> _logger;

    public SectionCreatedEventHandler(ILogger<SectionCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SectionCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventData}", notification.GetType().Name, notification.GetEventEntityDescription());

        return Task.CompletedTask;
    }
}
