using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PruebaBackend.Api.Data;
using PruebaBackend.Api.Models;

namespace PruebaBackend.Tests;

public sealed class MarcasAutosEndpointTests
{
    [Fact]
    public async Task Get_DevuelveLasTresMarcasComoJson()
    {
        using var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development");
            builder.UseSetting("Database:ApplyMigrations", "false");
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IDbContextOptionsConfiguration<MarcasAutosContext>>();
                services.AddDbContext<MarcasAutosContext>(options =>
                    options.UseInMemoryDatabase("MarcasAutosEndpoint"));
            });
        });

        using var client = factory.CreateClient();
        await using (var scope = factory.Services.CreateAsyncScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<MarcasAutosContext>();
            await context.Database.EnsureCreatedAsync();
        }

        var marcas = await client.GetFromJsonAsync<MarcaAuto[]>("/api/marcasautos");

        Assert.NotNull(marcas);
        Assert.Equal(new[] { "Toyota", "Honda", "Ford" }, marcas.Select(marca => marca.Nombre));
    }
}
