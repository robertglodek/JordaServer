
namespace Jorda.Server.Application.Common.Models.Identity.Requests;

public class ResetPasswordRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public string Token { get; set; } = null!;
}
