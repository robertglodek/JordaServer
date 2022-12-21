using Jorda.Server.Application.CQRS.Label.Queries.DTOs;
using MediatR;

namespace Jorda.Server.Application.CQRS.Label.Queries.GetLabels;

public class GetLabelsQuery : IRequest<IEnumerable<LabelDTO>>
{

}
