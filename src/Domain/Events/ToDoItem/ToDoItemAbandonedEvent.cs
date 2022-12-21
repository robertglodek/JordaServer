
namespace Jorda.Server.Domain.Events.ToDoItem;

public class ToDoItemAbandonedEvent : BaseEvent
{
    public ToDoItemAbandonedEvent(Entities.ToDoItem toDoItem)
    {
        ToDoItem = toDoItem;
    }
    public Entities.ToDoItem ToDoItem { get; }
    public override string GetEventEntityDescription() => $"ToDoItem abandoned with id:{ToDoItem.Id}";
}
