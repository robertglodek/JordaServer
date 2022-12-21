using Jorda.Server.Application.Common.Models.Identity.Requests;

namespace Jorda.Application.Common.Interfaces;

public interface IAccountService
{
    Task ChangePasswordAsync(ChangePasswordRequest request);
    Task<string> CreateUserAsync(CreateUserRequest request, string origin);
    Task ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
    Task ResetPasswordAsync(ResetPasswordRequest request);
    Task ConfirmEmailAsync(string userId, string code);
}
