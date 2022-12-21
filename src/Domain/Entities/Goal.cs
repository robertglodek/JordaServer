
namespace Jorda.Server.Domain.Entities;

public class Goal : BaseAuditableEntity
{
    /// <summary> Goal name etc. Programming, Workout, Jogging </summary>
    public string Name { get; set; } = null!;

    /// <summary> Color associated with the goal </summary>
    public Colour Colour { get; set; } = null!;

    /// <summary> User that owns the goal </summary>
    public string UserId { get; set; } = null!;

    /// <summary>Describes what the goal is about </summary>
    public string? Description { get; set; }

    /// <summary> Sections contained within the goal </summary>
    public List<Section> Sections { get; private set; } = new List<Section>();

    /// <summary> User files contained within the goal </summary>
    public List<UserFile> UserFiles { get; private set; } = new List<UserFile>();
}
