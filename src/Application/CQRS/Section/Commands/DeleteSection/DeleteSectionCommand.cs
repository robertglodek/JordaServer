using MediatR;

namespace Jorda.Server.Application.CQRS.Section.Commands.DeleteSection;

public class DeleteSectionCommand : IRequest<Unit>
{
    public Guid GoalId { get; set; }
    public Guid Id { get; set; }
}
