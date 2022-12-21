namespace Jorda.Application.CQRS.Section.Queries.DTOs;

public class SectionDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid GoalId { get; set; }
}
