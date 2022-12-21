using Jorda.Application.CQRS.Section.Queries.DTOs;
using Jorda.Server.Application.CQRS.Section.Commands.CreateSection;
using Jorda.Server.Application.CQRS.Section.Commands.DeleteSection;
using Jorda.Server.Application.CQRS.Section.Commands.UpdateSection;
using Jorda.Server.Application.CQRS.Section.Queries.GetSection;
using Jorda.Server.Application.CQRS.Section.Queries.GetSections;
using Jorda.Server.WebUi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Jorda.Server.WebUi.Controllers;

[Route("api/goal/{goalId}/section")]
public class SectionController : ApiControllerBase
{
    [HttpGet]
    [AuthorizeAction]
    public async Task<ActionResult<IEnumerable<SectionDTO>>> Get(Guid goalId)
    {
        return Ok(await Mediator.Send(new GetSectionsQuery() { GoalId=goalId }));
    }

    [HttpGet("{id}")]
    [AuthorizeAction]
    public async Task<ActionResult<SectionDTO>> Get(Guid goalId, Guid id)
    {
        return Ok(await Mediator.Send(new GetSectionQuery { Id = id, GoalId=goalId }));
    }

    [HttpPost]
    [AuthorizeAction]
    public async Task<ActionResult<Guid>> Create(CreateSectionCommand command, Guid goalId)
    {
        if (goalId != command.GoalId) return BadRequest();
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { goalId=goalId, id = result }, result);
    }

    [HttpPut("{id}")]
    [AuthorizeAction]
    public async Task<ActionResult> Update(UpdateSectionCommand command, Guid goalId, Guid id)
    {
        if (id != command.Id || goalId != command.GoalId) return BadRequest();
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [AuthorizeAction]
    public async Task<ActionResult> Delete(Guid goalId, Guid id)
    {
        await Mediator.Send(new DeleteSectionCommand() { Id = id, GoalId=goalId });
        return NoContent();
    }
}
