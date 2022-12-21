using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Domain.Entities;
using Jorda.Server.Domain.Events.Section;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.Section.Commands.DeleteSection;

public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteSectionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sections
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Section), request.Id);
        }
        entity.AddDomainEvent(new SectionDeletedEvent(entity));
        _context.Sections.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
