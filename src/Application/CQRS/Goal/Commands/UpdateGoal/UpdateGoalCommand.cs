using MediatR;
using Microsoft.AspNetCore.Http;

namespace Jorda.Application.CQRS.Goal.Commands.UpdateGoal;

public class UpdateGoalCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string ColourHex { get; set; } = null!;
    public string? Description { get; set; }
}
