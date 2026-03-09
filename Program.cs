var builder = WebApplication.CreateBuilder(args);

// 1. AGREGAR ESTA LÍNEA: Le dice a .NET que use nuestros Controladores
builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection(); // Comentado para evitar el warning amarillo en consola

// 2. AGREGAR ESTA LÍNEA: Mapea las rutas de nuestros controladores (ej. /api/citas)
app.MapControllers();

app.Run();