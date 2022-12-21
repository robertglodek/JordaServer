namespace Jorda.Server.Domain.Events.ToDoItem;

public class ToDoItemCompletedEvent : BaseEvent
{
    public ToDoItemCompletedEvent(Entities.ToDoItem toDoItem)
    {
        ToDoItem = toDoItem;
    }
    public Entities.ToDoItem ToDoItem { get; }
    public override string GetEventEntityDescription() => $"ToDoItem completed with id:{ToDoItem.Id}";
}
