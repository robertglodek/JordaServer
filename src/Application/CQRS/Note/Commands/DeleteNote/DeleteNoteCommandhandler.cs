using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Domain.Events.Note;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.Note.Commands.DeleteNote;

public class DeleteNoteCommandhandler : IRequestHandler<DeleteNoteCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteNoteCommandhandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Notes
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Note), request.Id);
        }
        entity.AddDomainEvent(new NoteDeletedEvent(entity));
        _context.Notes.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
