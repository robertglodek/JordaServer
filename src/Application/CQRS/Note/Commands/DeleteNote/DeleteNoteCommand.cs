using MediatR;

namespace Jorda.Server.Application.CQRS.Note.Commands.DeleteNote;

public class DeleteNoteCommand:IRequest<Unit>
{
    public Guid Id { get; set; }

    public Guid SectionId { get; set; }
}
