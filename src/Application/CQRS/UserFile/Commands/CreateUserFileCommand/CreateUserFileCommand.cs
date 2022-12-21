using MediatR;
using Microsoft.AspNetCore.Http;

namespace Jorda.Server.Application.CQRS.UserFile.Commands.CreateUserFileCommand;

public class CreateUserFileCommand : IRequest<Guid>
{
    public Guid GoalId { get; set; }
    public IFormFile File { get; set; } = null!;
}
