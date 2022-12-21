namespace Jorda.Server.Application.Common.Models.Identity.Requests;

public class CreateUserRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public bool AutoConfirmEmail { get; set; } = false;
    public string Role { get; set; } = null!;
}
