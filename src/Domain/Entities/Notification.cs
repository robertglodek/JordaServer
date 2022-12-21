
namespace Jorda.Server.Domain.Entities;

public class Notification:BaseAuditableEntity
{
    /// <summary> Name of the notification </summary>
    public string Name { get; set; } = null!;

    /// <summary> Notification description </summary>
    public string? Description { get; set; }

    /// <summary> It indicates whether the notification is active or not, if is it still valid </summary>
    public bool Active { get; set; }

    /// <summary> It describes the importance of this notification </summary>
    public ImportanceLevel ImportanceLevel { get; set; }
}
