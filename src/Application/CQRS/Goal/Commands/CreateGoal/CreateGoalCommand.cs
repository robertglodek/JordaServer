using MediatR;

namespace Jorda.Application.CQRS.Goal.Commands.CreateGoal;

public class CreateGoalCommand:IRequest<Guid>
{
    public string Name { get; set; } = null!;
    public string? ColourHex { get; set; }
    public string? Description { get; set; }
}
