namespace Jorda.Server.Domain.Events.ToDoItem;

public class ToDoItemDeletedEvent : BaseEvent
{
    public ToDoItemDeletedEvent(Entities.ToDoItem toDoItem)
    {
        ToDoItem = toDoItem;
    }
    public Entities.ToDoItem ToDoItem { get; }
    public override string GetEventEntityDescription() => $"ToDoItem deleted with id:{ToDoItem.Id}";
}
