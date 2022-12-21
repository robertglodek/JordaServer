using Jorda.Application.CQRS.Goal.Queries.DTOs;
using MediatR;
namespace Jorda.Application.CQRS.Goal.Queries.GetGoal;

public class GetGoalQuery:IRequest<GoalDTO>
{
    public Guid Id { get; set; }
}
