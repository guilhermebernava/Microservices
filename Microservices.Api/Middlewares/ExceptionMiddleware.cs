using FluentValidation;
using Microservices.Api.Dtos;
using Microservices.Infra.Exceptions;
using System.Net;
using System.Text.Json;

namespace Microservices.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;

        if (exception is RepositoryException || exception is UnityOfWorkException)
        {
            statusCode = HttpStatusCode.InternalServerError;
        }
        else if (exception is UnauthorizedAccessException)
        {
            statusCode = HttpStatusCode.Unauthorized;
        }
        else if (exception is ValidationException)
        {
            statusCode = HttpStatusCode.BadRequest;
        }

        var responseJson = JsonSerializer.Serialize(new ErrorDto
        {
            StatusCode = (int)statusCode,
            Message = exception.Message
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(responseJson);
    }
}