
namespace Jorda.Server.Application.Common.Constants.UserLimitation;

public class UserConstraints
{
    public string Role { get; set; } = null!;
    public int GoalsCount { get; set; }
    public int SectionsPerGoalCount { get; set; }
    public int ActiveToDoItemsPerSectionCount { get; set; }
    public int NotesPerSectionCount { get; set; }
    public int UserFilesPerGoalCount { get; set; }
}
