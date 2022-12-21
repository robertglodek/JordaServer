
namespace Jorda.Server.Domain.Events.UserFile;

public class UserFileDeletedEvent : BaseEvent
{
    public UserFileDeletedEvent(Entities.UserFile userFile)
    {
        UserFile = userFile;
    }
    public Entities.UserFile UserFile { get; }
    public override string GetEventEntityDescription() => $"UserFile deleted with id:{UserFile.Id}";
}
