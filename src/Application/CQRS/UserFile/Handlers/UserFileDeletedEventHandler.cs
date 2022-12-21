using Jorda.Server.Domain.Events.UserFile;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jorda.Server.Application.CQRS.UserFile.Handlers;

public class UserFileDeletedEventHandler : INotificationHandler<UserFileDeletedEvent>
{
    private readonly ILogger<UserFileDeletedEventHandler> _logger;

    public UserFileDeletedEventHandler(ILogger<UserFileDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserFileDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventData}", notification.GetType().Name, notification.GetEventEntityDescription());

        return Task.CompletedTask;
    }
}
