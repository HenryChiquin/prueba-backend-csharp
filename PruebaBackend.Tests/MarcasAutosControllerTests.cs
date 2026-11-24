using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaBackend.Api.Controllers;
using PruebaBackend.Api.Data;
using PruebaBackend.Api.Models;

namespace PruebaBackend.Tests;

public sealed class MarcasAutosControllerTests
{
    [Fact]
    public async Task ObtenerTodas_DevuelveLasMarcasInicialesEnOrden()
    {
        await using var context = CrearContexto();
        await context.Database.EnsureCreatedAsync();
        var controller = new MarcasAutosController(context);

        var respuesta = await controller.ObtenerTodas(CancellationToken.None);

        var resultado = Assert.IsType<OkObjectResult>(respuesta.Result);
        var marcas = Assert.IsAssignableFrom<IReadOnlyList<MarcaAuto>>(resultado.Value);
        Assert.Equal(new[] { 1, 2, 3 }, marcas.Select(marca => marca.Id));
        Assert.Equal(new[] { "Toyota", "Honda", "Ford" }, marcas.Select(marca => marca.Nombre));
    }

    [Fact]
    public async Task ObtenerTodas_DevuelveUnaListaVaciaSiNoHayMarcas()
    {
        await using var context = CrearContexto();
        var controller = new MarcasAutosController(context);

        var respuesta = await controller.ObtenerTodas(CancellationToken.None);

        var resultado = Assert.IsType<OkObjectResult>(respuesta.Result);
        var marcas = Assert.IsAssignableFrom<IReadOnlyList<MarcaAuto>>(resultado.Value);
        Assert.Empty(marcas);
    }

    [Fact]
    public async Task ObtenerTodas_ConsultaLasMarcasGuardadas()
    {
        await using var context = CrearContexto();
        context.MarcasAutos.Add(new MarcaAuto { Id = 7, Nombre = "Mazda" });
        await context.SaveChangesAsync();
        var controller = new MarcasAutosController(context);

        var respuesta = await controller.ObtenerTodas(CancellationToken.None);

        var resultado = Assert.IsType<OkObjectResult>(respuesta.Result);
        var marcas = Assert.IsAssignableFrom<IReadOnlyList<MarcaAuto>>(resultado.Value);
        Assert.Equal("Mazda", Assert.Single(marcas).Nombre);
    }

    private static MarcasAutosContext CrearContexto()
    {
        var options = new DbContextOptionsBuilder<MarcasAutosContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new MarcasAutosContext(options);
    }
}
