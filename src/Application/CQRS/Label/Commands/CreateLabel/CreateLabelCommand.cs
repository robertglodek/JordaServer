using MediatR;

namespace Jorda.Server.Application.CQRS.Label.Commands.CreateLabel;

public class CreateLabelCommand : IRequest<Guid>
{
    public string Name { get; set; } = null!;
}
