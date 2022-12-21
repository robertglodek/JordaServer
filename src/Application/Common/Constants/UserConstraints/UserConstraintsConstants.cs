using Jorda.Application.Common.Constants;
using Jorda.Server.Application.Common.Constants.UserLimitation;

namespace Jorda.Server.Application.Common.Constants.UserLimitations;

public static class UserConstraintsConstants
{
    public static readonly UserConstraints[] UserLimitations = {

        new UserConstraints
        {
            Role = UserRolesConstants.RoleBasic,
            GoalsCount = 5,
            SectionsPerGoalCount = 10,
            ActiveToDoItemsPerSectionCount = 30,
            NotesPerSectionCount = 60,
            UserFilesPerGoalCount = 0
        },
        new UserConstraints
        {
            Role = UserRolesConstants.RoleUltimate,
            GoalsCount = 10,
            SectionsPerGoalCount = 20,
            ActiveToDoItemsPerSectionCount = 50,
            NotesPerSectionCount = 100,
            UserFilesPerGoalCount = 10
        },
        new UserConstraints
        {
            Role = UserRolesConstants.RoleUltimate,
            GoalsCount = 14,
            SectionsPerGoalCount = 30,
            ActiveToDoItemsPerSectionCount = 50,
            NotesPerSectionCount = 100,
            UserFilesPerGoalCount = 10
        }
    };

    public static int GetMaxGoalCount(string role) => UserLimitations.FirstOrDefault(x => x.Role == role)!.GoalsCount;
    public static int GetMaxSectionsPerGoalCount(string role) => UserLimitations.FirstOrDefault(x => x.Role == role)!.SectionsPerGoalCount;
    public static int GetMaxActiveToDoItemsPerSectionCount(string role) => UserLimitations.FirstOrDefault(x => x.Role == role)!.ActiveToDoItemsPerSectionCount;
    public static int GetMaxNotesPerSectionCount(string role) => UserLimitations.FirstOrDefault(x => x.Role == role)!.NotesPerSectionCount;
    public static int GetUserFilesPerGoalCount(string role) => UserLimitations.FirstOrDefault(x => x.Role == role)!.UserFilesPerGoalCount;

}
