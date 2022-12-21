using Jorda.Application.CQRS.Note.Queries.DTOs;
using MediatR;

namespace Jorda.Server.Application.CQRS.Note.Queries.GetNote
{
    public class GetNoteQuery : IRequest<NoteDTO>
    {
        public Guid Id { get; set; }

        public Guid SectionId { get; set; }
    }
}
