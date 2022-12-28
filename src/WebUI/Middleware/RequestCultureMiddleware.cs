using System.Globalization;

namespace Jorda.Server.WebUi.Middleware;

public class RequestCultureMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var cultureQuery = context.Request.Query["culture"];

        CultureInfo? culture = null;
        if (!string.IsNullOrWhiteSpace(cultureQuery))
        {
            culture = new CultureInfo(cultureQuery!);
        }
        else if (context.Request.Headers.ContainsKey("Accept-Language"))
        {
            var cultureHeader = context.Request.Headers["Accept-Language"];
            if (cultureHeader.Any())
            {
                culture = new CultureInfo(cultureHeader.First()!.Split(',').First().Trim());
            }
        }
        if(culture == null)
        {
            culture = new CultureInfo("en-US");
        }
        
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        await next(context);
    }
}
