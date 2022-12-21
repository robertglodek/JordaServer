using Jorda.Server.Domain.Events.Section;
using MediatR;
using Microsoft.Extensions.Logging;
namespace Jorda.Server.Application.CQRS.Section.EventHandlers;

public class SectionDeletedEventHandler : INotificationHandler<SectionDeletedEvent>
{
    private readonly ILogger<SectionDeletedEventHandler> _logger;

    public SectionDeletedEventHandler(ILogger<SectionDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SectionDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventData}", notification.GetType().Name, notification.GetEventEntityDescription());

        return Task.CompletedTask;
    }
}
