using Jorda.Server.Application.Common.Models.Identity.Requests;
using Jorda.Server.Application.Common.Models.Identity.Responses;

namespace Jorda.Server.Application.Common.Interfaces;

public interface ITokenService
{
    Task<TokenResponse> LoginAsync(TokenRequest request);
}
