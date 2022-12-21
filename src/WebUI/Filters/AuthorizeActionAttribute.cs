using Microsoft.AspNetCore.Mvc.Filters;
using Jorda.Application.Common.Interfaces;
using Jorda.Application.Common.Exceptions;
using Jorda.Server.WebUi.Exceptions;

namespace Jorda.Server.WebUi.Filters;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class AuthorizeActionAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var currentUserService = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();

        // Must be authenticated user
        if (currentUserService.UserId == null)
        {
            throw new UnauthorizedAccessException();
        }

        if ((!string.IsNullOrWhiteSpace(Roles) && !Roles.Split(',').Contains(currentUserService.UserRole)))
        {
            throw new ForbiddenAccessException();
        }     
    }

    public string Roles { get; set; } = string.Empty;
}
