using MediatR;
namespace Jorda.Application.CQRS.Goal.Commands.CreateGoal;


public class DeleteGoalCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

