using Jorda.Application.Common.Models;
using Jorda.Application.CQRS.Note.Queries.DTOs;
using Jorda.Server.Domain.Enums;
using MediatR;

namespace Jorda.Server.Application.CQRS.Note.Queries.GetNotes;

public class GetNotesQuery : IRequest<PagedResult<NoteDTO>>
{
    public Guid SectionId { get; set; }
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
