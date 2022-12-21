namespace Jorda.Server.Domain.Events.Section;

public class SectionCreatedEvent : BaseEvent
{
    public SectionCreatedEvent(Entities.Section section)
    {
        Section = section;
    }

    public Entities.Section Section { get; }
    public override string GetEventEntityDescription() => $"Section created with id:{Section.Id}";
}
