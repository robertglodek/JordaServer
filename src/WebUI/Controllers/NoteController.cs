using Jorda.Application.Common.Models;
using Jorda.Application.CQRS.Note.Queries.DTOs;
using Jorda.Server.Application.CQRS.Note.Commands.CreateNote;
using Jorda.Server.Application.CQRS.Note.Commands.DeleteNote;
using Jorda.Server.Application.CQRS.Note.Commands.UpdateNote;
using Jorda.Server.Application.CQRS.Note.Queries.GetNote;
using Jorda.Server.Application.CQRS.Note.Queries.GetNotes;
using Jorda.Server.WebUi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Jorda.Server.WebUi.Controllers;


[Route("api/section/{sectionId}/note")]
public class NoteController : ApiControllerBase
{
    [HttpGet]
    [AuthorizeAction]
    public async Task<ActionResult<PagedResult<NoteDTO>>> Get([FromQuery] GetNotesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    [AuthorizeAction]
    public async Task<ActionResult<PagedResult<NoteDTO>>> Get(Guid id, Guid sectionId)
    {
        return Ok(await Mediator.Send(new GetNoteQuery() { Id = id, SectionId= sectionId }));
    }

    [HttpPost]
    [AuthorizeAction]
    public async Task<ActionResult<Guid>> Create(CreateNoteCommand command, Guid sectionId)
    {
        if (sectionId != command.SectionId) return BadRequest();
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { sectionId = sectionId, id = result }, result);
    }

    [HttpPut("{id}")]
    [AuthorizeAction]
    public async Task<ActionResult> Update(UpdateNoteCommand command, Guid sectionId, Guid id)
    {
        if (id != command.Id || sectionId != command.SectionId) return BadRequest();
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [AuthorizeAction]
    public async Task<ActionResult> Delete(Guid sectionId, Guid id)
    {
        await Mediator.Send(new DeleteNoteCommand() { Id = id, SectionId = sectionId });
        return NoContent();
    }
}
