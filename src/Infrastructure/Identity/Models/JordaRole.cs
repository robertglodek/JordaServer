using Microsoft.AspNetCore.Identity;

namespace Jorda.Server.Infrastructure.Identity.Models;

public class JordaRole: IdentityRole
{
	public JordaRole()
	{

	}
	public JordaRole(string role)
		:base(role)
	{
	
	}
}
