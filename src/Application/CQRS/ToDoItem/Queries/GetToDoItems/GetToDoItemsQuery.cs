using Jorda.Application.Common.Models;
using Jorda.Application.CQRS.ToDoItem.Queries.DTOs;
using Jorda.Server.Domain.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Jorda.Server.Application.CQRS.ToDoItem.Queries.GetToDoItems;

public class GetToDoItemsQuery : IRequest<PagedResult<ToDoItemDTO>>
{
    [JsonPropertyName("active")]
    public bool? Active { get; set; }

    [JsonPropertyName("sectionId")]
    public Guid SectionId { get; set; }

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
