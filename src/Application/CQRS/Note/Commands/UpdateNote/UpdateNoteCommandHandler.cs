using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.Note.Commands.UpdateNote;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateNoteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Notes.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Note), request.Id);
        }
        if(request.SectionId!=entity.SectionId)
        {
            var section = _context.Sections.FirstOrDefaultAsync(n => n.Id == request.SectionId);
            if (section == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Section), request.SectionId);
            }
        }
        entity.Description = request.Description;
        entity.Name = request.Name;
        entity.Source = request.Source;
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
