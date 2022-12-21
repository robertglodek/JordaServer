namespace Jorda.Server.Domain.Entities;


public class Section : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid GoalId { get; set; }
    public Goal Goal { get; set; } = null!;
    public List<ToDoItem> Items { get; private set; } = new List<ToDoItem>();
    public List<Note> Notes { get; private set; } = new List<Note>();
}
