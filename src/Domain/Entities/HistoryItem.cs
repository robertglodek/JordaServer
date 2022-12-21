namespace Jorda.Server.Domain.Entities;

public class HistoryItem : BaseAuditableEntity
{
    /// <summary> User that the task belongs to </summary>
    public string UserId { get; set; } = null!;

    /// <summary> User task name </summary>
    public string Name { get; set; } = null!;

    /// <summary> It describes the task status, etc. task completed, task added  </summary>
    public Enums.TaskStatus TaskStatus { get; set; }
}
