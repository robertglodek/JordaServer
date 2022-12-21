using Jorda.Server.Application.CQRS.UserFile.Queries.DTOs;
using MediatR;

namespace Jorda.Server.Application.CQRS.UserFile.Queries.GetUserFilesQuery;

public class GetUserFilesQuery : IRequest<IEnumerable<UserFileDTO>>
{
    public Guid GoalId { get; set; }
}
