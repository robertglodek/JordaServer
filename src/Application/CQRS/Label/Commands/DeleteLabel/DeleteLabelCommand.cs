using MediatR;

namespace Jorda.Server.Application.CQRS.Label.Commands.DeleteLabel;

public class DeleteLabelCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
