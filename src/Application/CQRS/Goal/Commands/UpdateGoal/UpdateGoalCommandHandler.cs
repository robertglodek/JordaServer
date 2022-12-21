using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Domain.Events.Goal;
using Jorda.Server.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Application.CQRS.Goal.Commands.UpdateGoal;

public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand,Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateGoalCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Goals.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Server.Domain.Entities.Goal), request.Id);
        }
        entity.Description = request.Description;
        entity.Name = request.Name;
        entity.Colour = (Colour)request.ColourHex!;
        entity.AddDomainEvent(new GoalUpdatedEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
