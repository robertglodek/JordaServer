using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.CQRS.Goal.Commands.CreateGoal;
using Jorda.Server.Domain.Events.Goal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Application.CQRS.Goal.Commands.DeleteGoal;
public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand,Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteGoalCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Goals
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Server.Domain.Entities.Goal), request.Id);
        }
        entity.AddDomainEvent(new GoalDeletedEvent(entity));
        _context.Goals.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
