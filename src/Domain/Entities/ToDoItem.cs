using Jorda.Server.Domain.Events.ToDoItem;
namespace Jorda.Server.Domain.Entities;


public class ToDoItem:BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Interval Interval { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public DateTime? DueDate { get; set; }
    public bool Active { get; set; } = true;
    public void Succeeded()
    {
        if(Active == true)
        {
            AddDomainEvent(new ToDoItemCompletedEvent(this));

            if (Interval == Interval.None)
            {
                Active = false;
            }
        }  
    }

    public void Failed()
    {
        if (Active == true)
        {
            AddDomainEvent(new ToDoItemAbandonedEvent(this));
            if (Interval == Interval.None)
            {
                Active = false;
            }
        }
    }
    public Guid SectionId { get; set; }
    public Section Section { get; set; } = null!;
    public IEnumerable<Label> Labels { get; set; } = new List<Label>();
}
