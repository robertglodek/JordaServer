namespace Jorda.Server.Domain.Events.Note;

public class NoteDeletedEvent : BaseEvent
{
    public NoteDeletedEvent(Entities.Note note)
    {
        Note = note;
    }
    public Entities.Note Note { get; }
    public override string GetEventEntityDescription() => $"Note deleted with id:{Note.Id}";
}