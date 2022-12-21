using Jorda.Application.Common.Models;
using Jorda.Server.Application.CQRS.HistoryItem.Queries.DTOs;
using Jorda.Server.Application.CQRS.HistoryItem.Queries.GetHistoryItems;
using Jorda.Server.WebUi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Jorda.Server.WebUi.Controllers;

[Route("api/historyItem")]
public class HistoryItemController : ApiControllerBase
{
    [HttpGet]
    [AuthorizeAction]
    public async Task<ActionResult<PagedResult<HistoryItemDTO>>> Get(GetHistoryItemsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
