using MediatR;

namespace Jorda.Server.Application.CQRS.Note.Commands.CreateNote;

public class CreateNoteCommand:IRequest<Guid>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? Source { get; set; }
    public Guid SectionId { get; set; }
}
