using Microsoft.EntityFrameworkCore;
using PruebaBackend.Api.Models;

namespace PruebaBackend.Api.Data;

public sealed class MarcasAutosContext(DbContextOptions<MarcasAutosContext> options) : DbContext(options)
{
    public DbSet<MarcaAuto> MarcasAutos => Set<MarcaAuto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var marcas = modelBuilder.Entity<MarcaAuto>();
        marcas.ToTable("MarcasAutos");
        marcas.Property(marca => marca.Nombre).HasMaxLength(100).IsRequired();
                
        marcas.HasData(
            new MarcaAuto { Id = 1, Nombre = "Toyota" },
            new MarcaAuto { Id = 2, Nombre = "Honda" },           
            new MarcaAuto { Id = 3, Nombre = "Ford" },
            new MarcaAuto { Id = 4, Nombre = "Audi" },
            new MarcaAuto { Id = 5, Nombre = "Subaru" },
            new MarcaAuto { Id = 6, Nombre = "Omoda" });
    }
}
