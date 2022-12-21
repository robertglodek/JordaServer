namespace Jorda.Server.Domain.Events.Goal;

public class GoalUpdatedEvent : BaseEvent
{
    public GoalUpdatedEvent(Entities.Goal goal)
    {
        Goal = goal;
    }
    public Entities.Goal Goal { get; }
    public override string GetEventEntityDescription() => $"Goal updated with id:{Goal.Id}";
}

