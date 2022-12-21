using Jorda.Application.Common.Models;
using Jorda.Server.Application.CQRS.HistoryItem.Queries.DTOs;
using Jorda.Server.Domain.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Jorda.Server.Application.CQRS.HistoryItem.Queries.GetHistoryItems;

public class GetHistoryItemsQuery:IRequest<PagedResult<HistoryItemDTO>>
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = null!;

    [JsonPropertyName("searchPhrase")]
    public string? SearchPhrase { get; set; }

    [JsonPropertyName("pageNumber")]
    public int PageNumber { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("sortBy")]
    public string? SortBy { get; set; }

    [JsonPropertyName("sortDirection")]
    public SortDirection SortDirection { get; set; }
}
