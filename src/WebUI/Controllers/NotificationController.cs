using Jorda.Application.Common.Constants;
using Jorda.Server.Application.CQRS.Notification.Commands.CreateNotification;
using Jorda.Server.Application.CQRS.Notification.Commands.DeleteNotification;
using Jorda.Server.Application.CQRS.Notification.Commands.UpdateNotification;
using Jorda.Server.Application.CQRS.Notification.Queries.DTOs;
using Jorda.Server.Application.CQRS.Notification.Queries.GetNotification;
using Jorda.Server.Application.CQRS.Notification.Queries.GetNotifications;
using Jorda.Server.WebUi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Jorda.Server.WebUi.Controllers;


[Route("api/notification")]
public class NotificationController : ApiControllerBase
{
    [HttpGet]
    [AuthorizeAction]
    public async Task<ActionResult<IEnumerable<NotificationDTO>>> Get()
    {
        return Ok(await Mediator.Send(new GetNotificationsQuery()));
    }

    [HttpGet("{id}")]
    [AuthorizeAction]
    public async Task<ActionResult<IEnumerable<NotificationDTO>>> Get(Guid id)
    {
        return Ok(await Mediator.Send(new GetNotificationQuery { Id = id }));
    }

    [HttpPost]
    [AuthorizeAction(Roles = UserRolesConstants.RoleAdmin)]
    public async Task<ActionResult<Guid>> Create(CreateNotificationCommand command)
    {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = result }, result);
    }

    [HttpPut("{id}")]
    [AuthorizeAction(Roles = UserRolesConstants.RoleAdmin)]
    public async Task<ActionResult> Update(UpdateNotificationCommand command, Guid id)
    {
        if (id != command.Id) return BadRequest();
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [AuthorizeAction(Roles = UserRolesConstants.RoleAdmin)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteNotificationCommand() { Id = id });
        return NoContent();
    }
}
