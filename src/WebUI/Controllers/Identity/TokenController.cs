using Jorda.Server.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Models.Identity.Requests;
using Jorda.Server.Application.Common.Models.Identity.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jorda.Server.WebUi.Controllers.Identity
{
    [Route("api/identity/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult<TokenResponse>> Login(TokenRequest model)
        {
            var response = await _tokenService.LoginAsync(model);
            return Ok(response);
        }

    }
}
