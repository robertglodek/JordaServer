using Jorda.Application.Common.Constants;
using Jorda.Server.Application.CQRS.UserFile.Commands.CreateUserFileCommand;
using Jorda.Server.Application.CQRS.UserFile.Commands.DeleteUserFileCommand;
using Jorda.Server.Application.CQRS.UserFile.Queries.DTOs;
using Jorda.Server.Application.CQRS.UserFile.Queries.GetUserFileQuery;
using Jorda.Server.Application.CQRS.UserFile.Queries.GetUserFilesQuery;
using Jorda.Server.WebUi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Jorda.Server.WebUi.Controllers;


[Route("api/goal/{goalId}/userFile")]
public class UserFileController : ApiControllerBase
{
    [HttpGet]
    [AuthorizeAction(Roles = UserRolesConstants.RoleUltimate + "," + UserRolesConstants.RoleAdmin)]
    public async Task<ActionResult<IEnumerable<UserFileDTO>>> Get(Guid goalId)
    {   
        return Ok(await Mediator.Send(new GetUserFilesQuery() { GoalId = goalId }));
    }

    [HttpGet("{id}")]
    [AuthorizeAction(Roles = UserRolesConstants.RoleUltimate + "," + UserRolesConstants.RoleAdmin)]
    public async Task<ActionResult<UserFileDetailsDTO>> Get(Guid goalId, Guid id)
    {
        var file = await Mediator.Send(new GetUserFileQuery { Id = id, GoalId = goalId });
        return File(Convert.FromBase64String(file.Data), file.ContentType, file.Name);
    }

    [HttpPost]
    [AuthorizeAction(Roles = UserRolesConstants.RoleUltimate + "," + UserRolesConstants.RoleAdmin)]
    public async Task<ActionResult<Guid>> Create([FromForm]CreateUserFileCommand command, Guid goalId)
    {
        if (goalId != command.GoalId) return BadRequest();
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { goalId, id = result }, result);
    }

    [HttpDelete("{id}")]
    [AuthorizeAction(Roles = UserRolesConstants.RoleUltimate + "," + UserRolesConstants.RoleAdmin)]
    public async Task<ActionResult> Delete(Guid goalId, Guid id)
    {
        await Mediator.Send(new DeleteUserFileCommand() { Id = id, GoalId = goalId });
        return NoContent();
    }

    [HttpGet("sa")]
    public ActionResult<Resp> GetAll()
    {
        return Ok(new Resp() { Id=Elo.One });
    }
}

public enum Elo
{
    One,
    Two
}

public class Resp
{
    public Elo Id { get; set; }
}

