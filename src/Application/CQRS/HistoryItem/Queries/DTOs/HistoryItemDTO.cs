
namespace Jorda.Server.Application.CQRS.HistoryItem.Queries.DTOs;

public class HistoryItemDTO
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public string Name { get; set; } = null!;

    public Domain.Enums.TaskStatus TaskStatus { get; set; }

    public DateTime Created { get; set; }
}
