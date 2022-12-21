using Jorda.Server.WebUi.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Jorda.WebUi.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if(!context.ModelState.IsValid)
        {
            throw new InvalidModelStateException(context.ModelState);
        }

        throw context.Exception;
    }
}
