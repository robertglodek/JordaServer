using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Models.Identity.Requests;
using Jorda.Server.WebUi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jorda.Server.WebUi.Controllers.Identity;

[Route("api/identity/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }


    [HttpPost("changePassword")]
    [AuthorizeAction]
    public async Task<ActionResult> ChangePassword(ChangePasswordRequest request)
    {
        await _accountService.ChangePasswordAsync(request);
        return NoContent();
    }


    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult> CreateUser(CreateUserRequest request)
    {
        return Ok(await _accountService.CreateUserAsync(request, Request.Headers["origin"]!));
    }


    [HttpGet("confirm-email")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
    {
        await _accountService.ConfirmEmailAsync(userId, code);
        return NoContent();
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        await _accountService.ForgotPasswordAsync(request, Request.Headers["origin"]!);
        return NoContent();
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
    {
        await _accountService.ResetPasswordAsync(request);
        return NoContent();
    }
}
