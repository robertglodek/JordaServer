
namespace Jorda.Server.Domain.Entities;

public class Label:BaseAuditableEntity
{
    /// <summary> Label name </summary>
    public string Name { get; set; } = null!;

    /// <summary> User that created the label </summary>
    public Guid UserId { get; set; }

    /// <summary> Tasks that the label is assigned to </summary>
    public IEnumerable<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();

    /// <summary> Notes that the label is assigned to </summary>
    public IEnumerable<Note> Notes { get; set; } = new List<Note>();
}
