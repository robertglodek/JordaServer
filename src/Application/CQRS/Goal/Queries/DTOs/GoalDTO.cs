namespace Jorda.Application.CQRS.Goal.Queries.DTOs;

public class GoalDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Colour { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string? Description { get; set; }
}
