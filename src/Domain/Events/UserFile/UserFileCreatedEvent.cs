
namespace Jorda.Server.Domain.Events.UserFile;

public class UserFileCreatedEvent : BaseEvent
{
    public UserFileCreatedEvent(Entities.UserFile userFile)
    {
        UserFile = userFile;
    }
    public Entities.UserFile UserFile { get; }
    public override string GetEventEntityDescription() => $"UserFile created with id:{UserFile.Id}";
}
