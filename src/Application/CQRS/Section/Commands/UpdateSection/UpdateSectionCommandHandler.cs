using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Domain.Events.Goal;
using Jorda.Server.Domain.Events.Section;
using Jorda.Server.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.Section.Commands.UpdateSection;

public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateSectionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sections.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Server.Domain.Entities.Goal), request.Id);
        }
        entity.Description = request.Description;
        entity.Name = request.Name;
        entity.AddDomainEvent(new SectionUpdtedEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

