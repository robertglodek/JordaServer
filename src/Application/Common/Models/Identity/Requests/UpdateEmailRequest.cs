namespace Jorda.Server.Application.Common.Models.Identity.Requests;

public class UpdateEmailRequest
{
    public string Email { get; set; } = null!;
    public string NewEmail { get; set; } = null!;
    public string Password { get; set; } = null!;
}

