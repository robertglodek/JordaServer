using MediatR;

namespace Jorda.Server.Application.CQRS.Section.Commands.UpdateSection;

public class UpdateSectionCommand : IRequest<Unit>
{
    public Guid GoalId { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
