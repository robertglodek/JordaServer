
namespace Jorda.Server.Domain.Entities;

public class UserFile:BaseAuditableEntity
{
    /// <summary> File name </summary>
    public string Name { get; set; } = null!;

    /// <summary> Type of the file </summary>
    public string ContentType { get; set; } = null!;

    /// <summary> File data, saved in base64 encoding </summary>
    public string Data { get; set; } = null!;

    /// <summary> Goal that this file is assigned to </summary>
    public Guid GoalId { get; set; }
}
