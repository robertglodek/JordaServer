﻿using System.Globalization;

namespace Jorda.Server.WebUi.Middleware;

public class RequestCultureMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var cultureQuery = context.Request.Query["culture"];
        if (!string.IsNullOrWhiteSpace(cultureQuery))
        {
            var culture = new CultureInfo(cultureQuery!);

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
        else if (context.Request.Headers.ContainsKey("Accept-Language"))
        {
            var cultureHeader = context.Request.Headers["Accept-Language"];
            if (cultureHeader.Any())
            {
                var culture = new CultureInfo(cultureHeader.First()!.Split(',').First().Trim());

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }
        }

        await next(context);
    }

   
}
