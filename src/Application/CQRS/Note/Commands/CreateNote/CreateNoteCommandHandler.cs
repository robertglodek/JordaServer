using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Constants.UserLimitations;
using Jorda.Server.Application.Common.Exceptions;
using Jorda.Server.Application.Common.Resources.Limitation;
using Jorda.Server.Domain.Events.Note;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jorda.Server.Application.CQRS.Note.Commands.CreateNote;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateNoteCommandHandler(IApplicationDbContext context,ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var section = _context.Sections.FirstOrDefaultAsync(n => n.Id == request.SectionId);
        if(section == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Section), request.SectionId);
        }
        var notesCount = _context.Notes.Count(n => n.SectionId == request.SectionId);

        var maxNotesCount = UserConstraintsConstants.GetMaxNotesPerSectionCount(_currentUserService.UserRole!);
        if (notesCount > maxNotesCount)
        {
            throw new LimitationException($"Notes count exceeded: {maxNotesCount}",
                string.Format(Limitation.NotesCountExceeded, maxNotesCount));
        }
        var entity = new Server.Domain.Entities.Note
        {
            Description = request.Description,
            Name = request.Name,
            Source = request.Source,
            SectionId = request.SectionId
        };
        entity.AddDomainEvent(new NoteCreatedEvent(entity));
        _context.Notes.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
