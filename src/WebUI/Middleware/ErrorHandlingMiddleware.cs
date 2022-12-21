using Jorda.Application.Common.Exceptions;
using Jorda.Server.Application.Common.Exceptions;
using Jorda.Server.Domain.Exceptions;
using Jorda.Server.Infrastructure.Common.Exceptions;
using Jorda.Server.WebUi.Constants;
using Jorda.Server.WebUi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text.Json;

namespace Jorda.Server.WebUi.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly IDictionary<Type, Func<Exception, ProblemDetails>> _exceptionHandlers;

    public ErrorHandlingMiddleware()
    {
        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Func<Exception, ProblemDetails>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
            { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            { typeof(UnsupportedColourException), HandleUnsuportedColourException },
            { typeof(LimitationException), HandleLimitationException },
            { typeof(IdentityErrorException), HandleIdentyErrorException },
            { typeof(SmtpException), HandleSmtpException },
            { typeof(InvalidModelStateException), HandleInvalidModelStateException }
        };
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            var result = JsonSerializer.Serialize((object)HandleException(exception));
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        } 
    }
    private ProblemDetails HandleException(Exception exception)
    {
        Type type = exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            return _exceptionHandlers[type].Invoke(exception);
        }
        return HandleUnknownException(exception);
    }

    private ProblemDetails HandleValidationException(Exception exception)
    {
        var details = new ValidationProblemDetails(((ValidationException)exception).Errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = ErrorResponseType.ValidationIssues,
        };

        return details;
    }

    private ProblemDetails HandleSmtpException(Exception exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status503ServiceUnavailable,
            Title = ErrorResponseType.SmtpError,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.4"
        };

        return details;
    }

    private ProblemDetails HandleUnknownException(Exception exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = ErrorResponseType.InternalServerError,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        return details;
    }
    private ProblemDetails HandleUnsuportedColourException(Exception exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = ErrorResponseType.UnsupportedColor
        };

        return details;
    }

    private ProblemDetails HandleLimitationException(Exception exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = ErrorResponseType.Limitation,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Detail = ((LimitationException)exception).Details
        };

        return details;
    }

    private ProblemDetails HandleInvalidModelStateException(Exception exception)
    {
        var details = new ValidationProblemDetails(((InvalidModelStateException)exception).ModelState)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = ErrorResponseType.InvalidModelState
        };

        return details;
    }

    private ProblemDetails HandleNotFoundException(Exception exception)
    {

        var details = new ProblemDetails()
        {
            Status = StatusCodes.Status404NotFound,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = ErrorResponseType.NotFound,
            Detail = ((NotFoundException)exception).Details
        };

        return details;
    }

    private ProblemDetails HandleUnauthorizedAccessException(Exception exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = ErrorResponseType.Unauthorized,
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };

        return details;
    }

    private ProblemDetails HandleForbiddenAccessException(Exception exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = ErrorResponseType.Forbidden,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };

        return details;
    }

    private ProblemDetails HandleIdentyErrorException(Exception exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = ErrorResponseType.IdentityError,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };
        details.Extensions.Add("ErrorsMessages", ((IdentityErrorException)exception).Errors);

        return details;
    }
}
