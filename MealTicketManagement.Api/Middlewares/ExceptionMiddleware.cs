using System.Text.Json;
using MealTicketManagement.Domain.Exceptions;

namespace MealTicketManagement.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (BusinessException ex)
        {
            await WriteResponse(httpContext, 400, ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            await WriteResponse(httpContext, 404, ex.Message);
        }
        catch (JsonException)
        {
            await WriteResponse(httpContext, 400, "JSON inválido. Verifique os campos e valores enviados.");
        }
        catch (ArgumentNullException ex)
        {
            await WriteResponse(httpContext, 400, $"Campo obrigatório ausente: {ex.ParamName}");
        }
        catch (ArgumentException ex)
        {
            await WriteResponse(httpContext, 400, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            await WriteResponse(httpContext, 422, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro interno não tratado.");
            await WriteResponse(httpContext, 500, "Erro interno no servidor.");
        }
    }

    private static async Task WriteResponse(HttpContext httpContext, int statusCode, string message)
    {
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(new { message });
    }
}