using Jorda.Server.Application.CQRS.Label.Queries.DTOs;
using MediatR;

namespace Jorda.Server.Application.CQRS.Label.Queries.GetLabel;

public class GetLabelQuery : IRequest<LabelDTO>
{
    public Guid Id { get; set; }
}
