using Jorda.Server.Domain.Enums;

namespace Jorda.Server.Application.Common.Models.Identity.Requests;

public class GetUsersRequest
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
