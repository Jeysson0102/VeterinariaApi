using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using VeterinariaApi.Models;

namespace VeterinariaApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = new ApiResponse<object>(false, $"Error interno del servidor: {ex.Message}");
                var json = JsonSerializer.Serialize(response);
                
                await context.Response.WriteAsync(json);
            }
        }
    }
}