using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaBackend.Api.Data;
using PruebaBackend.Api.Models;

namespace PruebaBackend.Api.Controllers;

[ApiController]
[Route("api/marcasautos")]
public sealed class MarcasAutosController(MarcasAutosContext context) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<MarcaAuto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<MarcaAuto>>> ObtenerTodas(CancellationToken cancellationToken)
    {
        var marcas = await context.MarcasAutos
            .AsNoTracking()
            .OrderBy(marca => marca.Id)
            .ToListAsync(cancellationToken);

        return Ok(marcas);
    }
}
