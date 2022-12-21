using MediatR;

namespace Jorda.Server.Application.CQRS.Note.Commands.UpdateNote;

public class UpdateNoteCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? Source { get; set; }
    public Guid SectionId { get; set; }
}
