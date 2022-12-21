using MediatR;


namespace Jorda.Server.Application.CQRS.Label.Commands.UpdateLabel;

public class UpdateLabelCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

