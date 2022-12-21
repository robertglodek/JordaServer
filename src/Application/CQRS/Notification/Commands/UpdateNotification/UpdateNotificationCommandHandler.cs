using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.Notification.Commands.UpdateNotification;

public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateNotificationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Notification), request.Id);
        }
        entity.Description = request.Description;
        entity.Name = request.Name;
        entity.Active = request.Active;
        entity.ImportanceLevel = request.ImportanceLevel;
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
