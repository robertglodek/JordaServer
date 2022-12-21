using Jorda.Server.Domain.Enums;
using MediatR;

namespace Jorda.Application.CQRS.ToDoItem.Commands.CreateToDoItem;


public class CreateToDoItemCommand:IRequest<Guid>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Interval Interval { get; set; } = Interval.None;
    public PriorityLevel PriorityLevel { get; set; } = PriorityLevel.Low;
    public DateTime? DueDate { get; set; }
    public Guid SectionId { get; set; }
}
