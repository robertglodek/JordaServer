using Jorda.Application.CQRS.Goal.Queries.DTOs;
using MediatR;

namespace Jorda.Application.CQRS.Goal.Queries.GetGoals;

public class GetGoalsQuery:IRequest<IEnumerable<GoalDTO>>
{

}
