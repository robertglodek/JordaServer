using Jorda.Application.Common.Models;
using Jorda.Server.Application.Common.Models.Identity.Requests;
using Jorda.Server.Application.Common.Models.Identity.Responses;

namespace Jorda.Server.Application.Common.Interfaces;

public interface IUserService
{
    Task<PagedResult<UserResponse>> GetUsersAsync(GetUsersRequest request);
    Task<UserResponse> GetUserAsync(string userId);
    Task<string> GetUserNameAsync(string userId);
    Task LockUserAsync(UserLockoutRequest request);
    Task<bool> UserExists(string email);
}
