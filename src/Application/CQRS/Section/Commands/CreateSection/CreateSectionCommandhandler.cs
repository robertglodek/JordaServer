using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Constants.UserLimitations;
using Jorda.Server.Application.Common.Exceptions;
using Jorda.Server.Application.Common.Resources.Limitation;
using Jorda.Server.Domain.Events.Section;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.Section.Commands.CreateSection;

public class CreateSectionCommandhandler : IRequestHandler<CreateSectionCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateSectionCommandhandler(IApplicationDbContext context,ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        var goal = _context.Goals.FirstOrDefaultAsync(n => n.Id == request.GoalId);
        if (goal == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Goal), request.GoalId);
        }

        var sectionsCount = _context.Sections.Count(n => n.GoalId == request.GoalId);

        var maxSectionsCount = UserConstraintsConstants.GetMaxSectionsPerGoalCount(_currentUserService.UserRole!);
        if (sectionsCount > maxSectionsCount)
        {
            throw new LimitationException($"Goals count exceeded: {maxSectionsCount}",
                string.Format(Limitation.SectionsCountExceeded, maxSectionsCount));
        }
        var entity = new Domain.Entities.Section
        {
            Description = request.Description,
            Name = request.Name,
        };
       
        entity.AddDomainEvent(new SectionCreatedEvent(entity));
        _context.Sections.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
