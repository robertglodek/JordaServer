namespace Jorda.Server.Domain.Events.Note;

public class NoteCreatedEvent : BaseEvent
{
    public NoteCreatedEvent(Entities.Note note)
    {
        Note = note;
    }
    public Entities.Note Note { get; }
    public override string GetEventEntityDescription() => $"Note created with id:{Note.Id}";
}




