namespace Jorda.Server.Domain.Events.Section;

public class SectionDeletedEvent : BaseEvent
{
    public SectionDeletedEvent(Entities.Section section)
    {
        Section = section;
    }
    public Entities.Section Section { get; }
    public override string GetEventEntityDescription() => $"Section deleted with id:{Section.Id}";
}
