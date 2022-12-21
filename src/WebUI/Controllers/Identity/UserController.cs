using Jorda.Application.Common.Constants;
using Jorda.Server.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Models.Identity.Requests;
using Jorda.Server.WebUi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Jorda.Server.WebUi.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [AuthorizeAction(Roles = UserRolesConstants.RoleAdmin)]
        public async Task<IActionResult> Get(GetUsersRequest request)
        {
            var users = await _userService.GetUsersAsync(request);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [AuthorizeAction]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetUserAsync(id);
            return Ok(user);
        }
    }
}

