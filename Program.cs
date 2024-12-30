using ApiEnvioPedidos.Business;
using ApiEnvioPedidos.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();  

// Inyección de dependencias para servicios de negocio y SOAP
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddHttpClient<ISoapClient, SoapClient>();

// Configuración CORS para permitir cualquier origen (ajustable según necesidades)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Construcción de la aplicación
var app = builder.Build();

// Configuración del entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");  // Permitir CORS solo en desarrollo
}

// Manejador global de excepciones (centralizado)
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var errorResponse = new { message = "Ocurrió un error interno." };
        await context.Response.WriteAsJsonAsync(errorResponse);
    });
});

// HTTPS opcional (descomentar si se requiere redireccionar a HTTPS)
// app.UseHttpsRedirection();  

// Mapear los controladores
app.MapControllers();

// Endpoint de prueba (Weather Forecast) con OpenAPI
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// Iniciar la aplicación
app.Run();

// Modelo de ejemplo para el endpoint de prueba
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
