using MediatR;

namespace Jorda.Server.Application.CQRS.Section.Commands.CreateSection;

public class CreateSectionCommand : IRequest<Guid>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid GoalId { get; set; }
}
