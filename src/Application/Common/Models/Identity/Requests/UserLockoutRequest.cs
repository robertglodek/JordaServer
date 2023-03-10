
namespace Jorda.Server.Application.Common.Models.Identity.Requests;

public class UserLockoutRequest
{
    public string Id { get; set; } = null!;
    public DateTime LockoutEnd { get; set; }
    public string? Message { get; set; }
}
