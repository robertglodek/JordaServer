namespace Jorda.Server.Domain.Events.Goal;

public class GoalCreatedEvent : BaseEvent
{
    public GoalCreatedEvent(Entities.Goal goal)
    {
        Goal = goal;
    }
    public Entities.Goal Goal { get; }
    public override string GetEventEntityDescription()=> $"Goal created with id:{Goal.Id}";
}
