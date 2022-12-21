using Jorda.Server.Application.CQRS.UserFile.Queries.DTOs;
using MediatR;

namespace Jorda.Server.Application.CQRS.UserFile.Queries.GetUserFileQuery;

public class GetUserFileQuery : IRequest<UserFileDetailsDTO>
{
    public Guid Id { get; set; }

    public Guid GoalId { get; set; }
}

