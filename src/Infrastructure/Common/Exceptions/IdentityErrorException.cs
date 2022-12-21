
namespace Jorda.Server.Infrastructure.Common.Exceptions;

public class IdentityErrorException:Exception
{
	public IdentityErrorException(params string[] errors)
	{	
		Errors = errors.ToArray();
    }
	public string[] Errors { get; set; }

}
