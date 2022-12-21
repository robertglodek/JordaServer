namespace Jorda.Server.Domain.Events.ToDoItem;

public class ToDoItemCreatedEvent : BaseEvent
{
    public ToDoItemCreatedEvent(Entities.ToDoItem toDoItem)
    {
        ToDoItem = toDoItem;
    }
    public Entities.ToDoItem ToDoItem { get; }
    public override string GetEventEntityDescription() => $"ToDoItem created with id:{ToDoItem.Id}";
}
