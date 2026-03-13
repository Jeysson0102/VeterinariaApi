using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VeterinariaApi.Middlewares; // Importamos el middleware

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Activa el manejo global de errores ANTES de los controladores
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();