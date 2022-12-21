namespace Jorda.Server.Application.Common.Models.Identity.Requests;

public class ChangePasswordRequest
{
    public string Password { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmNewPassword { get; set; } = null!;
}
