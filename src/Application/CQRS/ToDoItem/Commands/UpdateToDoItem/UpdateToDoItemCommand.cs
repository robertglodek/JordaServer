using Jorda.Server.Domain.Enums;
using MediatR;

namespace Jorda.Application.CQRS.ToDoItem.Commands.UpdateToDoItem;

public class UpdateToDoItemCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Interval Interval { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public DateTime? DueDateUtc { get; set; }
    public Guid SectionId { get; set; }

    
}
