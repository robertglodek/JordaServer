
namespace Jorda.Server.Domain.Events.Section;

public class SectionUpdtedEvent : BaseEvent
{
    public SectionUpdtedEvent(Entities.Section section)
    {
        Section = section;
    }

    public Entities.Section Section { get; }
    public override string GetEventEntityDescription() => $"Section updated with id:{Section.Id}";
}

