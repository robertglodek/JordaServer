using MediatR;

namespace Jorda.Server.Application.CQRS.UserFile.Commands.DeleteUserFileCommand;

public class DeleteUserFileCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public Guid GoalId { get; set; }
}
