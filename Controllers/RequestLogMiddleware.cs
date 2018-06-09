using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading.Tasks; 
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
public class RequestLogMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<RequestLogMiddleware> logger)
    {
        var sw = new Stopwatch();
        sw.Start();

        await this._next(context);

        var data = new 
        {
            Method = context.Request.Method,
            Url = context.Request.GetDisplayUrl(),
            Duration = sw.ElapsedMilliseconds,
            StatusCode = context.Response?.StatusCode
        };

        logger.LogInformation("Processing {@data}", data);
    }

}