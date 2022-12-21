using Jorda.Application.CQRS.Note.Queries.DTOs;
using Jorda.Application.CQRS.ToDoItem.Queries.DTOs;

namespace Jorda.Server.Application.CQRS.Section.Queries.DTOs;

public class SectionDetailsDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid GoalId { get; set; }
    public IEnumerable<NoteDTO> Notes { get; set; } = null!;
    public IEnumerable<ToDoItemDTO> ToDoItems { get; set; } = null!;
}
