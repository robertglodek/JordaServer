using Jorda.Server.Domain.Events.UserFile;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Jorda.Server.Application.CQRS.UserFile.Handlers;

public class UserFileCreatedEventHandler : INotificationHandler<UserFileCreatedEvent>
{
    private readonly ILogger<UserFileCreatedEventHandler> _logger;

    public UserFileCreatedEventHandler(ILogger<UserFileCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserFileCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New Domain Event: {DomainEvent}, {EventData}", notification.GetType().Name, notification.GetEventEntityDescription());

        return Task.CompletedTask;
    }
}

