using Microsoft.EntityFrameworkCore;
using PruebaBackend.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<MarcasAutosContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MarcasAutos")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

// Compose espera a que PostgreSQL esté listo antes de iniciar la API.
if (app.Configuration.GetValue<bool>("Database:ApplyMigrations"))
{
    await using var scope = app.Services.CreateAsyncScope();
    var context = scope.ServiceProvider.GetRequiredService<MarcasAutosContext>();
    await context.Database.MigrateAsync();
}

await app.RunAsync();

public partial class Program { }
