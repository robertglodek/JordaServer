
namespace Jorda.Server.Domain.Events.ToDoItem;

public class ToDoITemUpdatedEvent : BaseEvent
{
    public ToDoITemUpdatedEvent(Entities.ToDoItem toDoItem)
    {
        ToDoItem = toDoItem;
    }
    public Entities.ToDoItem ToDoItem { get; }
    public override string GetEventEntityDescription() => $"ToDoItem updated with id:{ToDoItem.Id}";
}
