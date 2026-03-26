
using AgroOrganizer.Models.ErrorHandling.CustomExceptions;
using System.Net;
using System.Text.Json;


namespace AgroOrganizer.Models.ErrorHandling.ExceptionMiddleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
           await HandleException(httpContext, e);
        }
    }

    private static async Task HandleException(HttpContext httpContext, Exception e)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = e.Message;

        switch (e)
        {
            case BadRequestException:
                statusCode = HttpStatusCode.BadRequest;
                break;
            case ConflictException:
                statusCode = HttpStatusCode.Conflict;
                break;
            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                break;
            case UnauthorizedException:
                statusCode = HttpStatusCode.Unauthorized;
                break;
        }

        var responseObject = new
        {
            error = message,
            status = (int)statusCode
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(responseObject);
        
        await httpContext.Response.WriteAsync(json);
    }
}