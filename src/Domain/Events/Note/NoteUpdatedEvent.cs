
namespace Jorda.Server.Domain.Events.Note
{
    public class NoteUpdatedEvent : BaseEvent
    {
        public NoteUpdatedEvent(Entities.Note note)
        {
            Note = note;
        }
        public Entities.Note Note { get; }
        public override string GetEventEntityDescription() => $"Note updated with id:{Note.Id}";
    }
}
