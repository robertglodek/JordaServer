namespace Jorda.Server.Domain.Events.Goal;

public class GoalDeletedEvent : BaseEvent
{
    public GoalDeletedEvent(Entities.Goal goal)
    {
        Goal = goal;
    }
    public Entities.Goal Goal { get; }
    public override string GetEventEntityDescription() => $"Goal deleted with id:{Goal.Id}"; 
}
