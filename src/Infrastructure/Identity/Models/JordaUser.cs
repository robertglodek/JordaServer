using Microsoft.AspNetCore.Identity;

namespace Jorda.Server.Infrastructure.Identity.Models;

public class JordaUser:IdentityUser
{
    public int Score { get; set; }

    public string? LockoutMessage { get; set; }
}
