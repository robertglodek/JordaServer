using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Constants.UserLimitations;
using Jorda.Server.Application.Common.Exceptions;
using Jorda.Server.Application.Common.Resources.Limitation;
using Jorda.Server.Domain.Events.Goal;
using Jorda.Server.Domain.ValueObjects;

using MediatR;

namespace Jorda.Application.CQRS.Goal.Commands.CreateGoal
{
    public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateGoalCommandHandler(IApplicationDbContext context,ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
        {
            
            var goalsCount = _context.Goals.Count(n => n.UserId == _currentUserService.UserId!);
            var maxGoalsCount = UserConstraintsConstants.GetMaxGoalCount(_currentUserService.UserRole!);
            if (goalsCount > maxGoalsCount)
            {
                throw new LimitationException($"Goals count exceeded: {maxGoalsCount}",
                    string.Format(Limitation.GoalsCountExceeded,maxGoalsCount) );
            } 
            var entity = new Server.Domain.Entities.Goal
            {  
                Description = request.Description,
                Name = request.Name,
                UserId= _currentUserService.UserId!  
            };
            entity.Colour = request.ColourHex != null ? (Colour)request.ColourHex : Colour.Default;
            entity.AddDomainEvent(new GoalCreatedEvent(entity));
            _context.Goals.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
