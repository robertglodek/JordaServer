using Jorda.Application.CQRS.Goal.Commands.CreateGoal;
using Jorda.Application.CQRS.Goal.Commands.UpdateGoal;
using Jorda.Application.CQRS.Goal.Queries.DTOs;
using Jorda.Application.CQRS.Goal.Queries.GetGoal;
using Jorda.Application.CQRS.Goal.Queries.GetGoals;
using Jorda.Server.WebUi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Jorda.Server.WebUi.Controllers;

[Route("api/goal")]
public class GoalController : ApiControllerBase
{
    [HttpGet]
    [AuthorizeAction]
    public async Task<ActionResult<IEnumerable<GoalDTO>>> Get()
    {
        return Ok(await Mediator.Send(new GetGoalsQuery()));
    }

    [HttpGet("{id}")]
    [AuthorizeAction]
    public async Task<ActionResult<GoalDTO>> Get(Guid id)
    {
        return Ok(await Mediator.Send(new GetGoalQuery { Id = id }));
    }

    [HttpPost]
    [AuthorizeAction]
    public async Task<ActionResult<Guid>> Create(CreateGoalCommand command)
    {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = result }, result);
    }

    [HttpPut("{id}")]
    [AuthorizeAction]
    public async Task<ActionResult> Update(Guid id, UpdateGoalCommand command)
    {
        if (id != command.Id) return BadRequest();
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [AuthorizeAction]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteGoalCommand() { Id=id });
        return NoContent();
    }
}
