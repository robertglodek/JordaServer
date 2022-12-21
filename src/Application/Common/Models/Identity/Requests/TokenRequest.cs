namespace Jorda.Server.Application.Common.Models.Identity.Requests;

public class TokenRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
