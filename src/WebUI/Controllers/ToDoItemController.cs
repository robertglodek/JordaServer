using Jorda.Application.Common.Models;
using Jorda.Application.CQRS.ToDoItem.Commands.CreateToDoItem;
using Jorda.Application.CQRS.ToDoItem.Commands.DeleteToDoItem;
using Jorda.Application.CQRS.ToDoItem.Commands.UpdateToDoItem;
using Jorda.Application.CQRS.ToDoItem.Queries.DTOs;
using Jorda.Server.Application.CQRS.ToDoItem.Commands.RealizeToDoItem;
using Jorda.Server.Application.CQRS.ToDoItem.Queries.GetToDoItem;
using Jorda.Server.Application.CQRS.ToDoItem.Queries.GetToDoItems;
using Jorda.Server.WebUi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Jorda.Server.WebUi.Controllers;

[Route("api/section/{sectionId}/toDoItem")]
public class ToDoItemController : ApiControllerBase
{
    [HttpGet]
    [AuthorizeAction]
    public async Task<ActionResult<PagedResult<ToDoItemDTO>>> Get([FromQuery]GetToDoItemsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    [AuthorizeAction]
    public async Task<ActionResult<ToDoItemDTO>> Get(Guid id)
    {
        return Ok(await Mediator.Send(new GetToDoItemQuery() { Id=id }));
    }

    [HttpPost]
    [AuthorizeAction]
    public async Task<ActionResult<Guid>> Create(CreateToDoItemCommand command, Guid sectionId)
    {
        if (sectionId != command.SectionId) return BadRequest();
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { sectionId = sectionId, id = result }, result);
    }

    [HttpPut("{id}")]
    [AuthorizeAction]
    public async Task<ActionResult> Update(UpdateToDoItemCommand command, Guid sectionId, Guid id)
    {
        if (id != command.Id || sectionId != command.SectionId) return BadRequest();
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPut("realize/{id}")]
    [AuthorizeAction]
    public async Task<ActionResult> Realize(RealizeToDoItemCommand command, Guid sectionId, Guid id)
    {
        if (id != command.Id || sectionId != command.SectionId) return BadRequest();
        await Mediator.Send(command);
        return NoContent();
    }


    [HttpDelete("{id}")]
    [AuthorizeAction]
    public async Task<ActionResult> Delete(Guid sectionId, Guid id)
    {
        await Mediator.Send(new DeleteToDoItemCommand() { Id = id, SectionId = sectionId });
        return NoContent();
    }
}

