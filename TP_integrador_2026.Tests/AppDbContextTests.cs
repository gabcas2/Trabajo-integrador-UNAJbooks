using Microsoft.EntityFrameworkCore;
using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Infrastructure.Persistence;

namespace TP_integrador_2026.Tests;

public class AppDbContextTests
{
    // Comprueba que el código del libro tenga un índice único.
    [Fact]
    public void Modelo_LibroCodigo_TieneIndiceUnico()
    {
        using var contexto = CrearContexto();

        var entidad = contexto.Model.FindEntityType(typeof(Libro));

        Assert.NotNull(entidad);

        var indice = entidad!.GetIndexes()
            .SingleOrDefault(index =>
                index.Properties.Any(
                    propiedad => propiedad.Name == nameof(Libro.Codigo)));

        Assert.NotNull(indice);
        Assert.True(indice!.IsUnique);
    }

    // Comprueba que el DNI del socio tenga un índice único.
    [Fact]
    public void Modelo_SocioDni_TieneIndiceUnico()
    {
        using var contexto = CrearContexto();

        var entidad = contexto.Model.FindEntityType(typeof(Socio));

        Assert.NotNull(entidad);

        var indice = entidad!.GetIndexes()
            .SingleOrDefault(index =>
                index.Properties.Any(
                    propiedad => propiedad.Name == nameof(Socio.DNI)));

        Assert.NotNull(indice);
        Assert.True(indice!.IsUnique);
    }

    // Comprueba campos obligatorios y longitudes de Libro.
    [Fact]
    public void Modelo_Libro_RespetaCamposObligatoriosYLongitudes()
    {
        using var contexto = CrearContexto();

        var entidad = contexto.Model.FindEntityType(typeof(Libro));

        Assert.NotNull(entidad);

        var codigo = entidad!.FindProperty(nameof(Libro.Codigo));
        var titulo = entidad.FindProperty(nameof(Libro.Titulo));
        var autor = entidad.FindProperty(nameof(Libro.Autor));
        var editorial = entidad.FindProperty(nameof(Libro.Editorial));

        Assert.NotNull(codigo);
        Assert.NotNull(titulo);
        Assert.NotNull(autor);
        Assert.NotNull(editorial);

        Assert.False(codigo!.IsNullable);
        Assert.Equal(20, codigo.GetMaxLength());

        Assert.False(titulo!.IsNullable);
        Assert.Equal(200, titulo.GetMaxLength());

        Assert.False(autor!.IsNullable);
        Assert.Equal(150, autor.GetMaxLength());

        Assert.False(editorial!.IsNullable);
        Assert.Equal(150, editorial.GetMaxLength());
    }

    // Comprueba que EF Core modele SocioPremium como subtipo de Socio.
    [Fact]
    public void Modelo_SocioPremium_HeredaDeSocio()
    {
        using var contexto = CrearContexto();

        var entidad = contexto.Model.FindEntityType(typeof(SocioPremium));

        Assert.NotNull(entidad);
        Assert.Equal(typeof(Socio), entidad!.BaseType?.ClrType);
    }

    // Comprueba que las relaciones de Prestamo impidan borrar
    // automáticamente el libro o el socio relacionado.
    [Fact]
    public void Modelo_Prestamo_UsaRestrictEnLasDosRelaciones()
    {
        using var contexto = CrearContexto();

        var entidad = contexto.Model.FindEntityType(typeof(Prestamo));

        Assert.NotNull(entidad);

        var relacionLibro = entidad!.GetForeignKeys()
            .SingleOrDefault(relacion =>
                relacion.Properties.Any(
                    propiedad => propiedad.Name == nameof(Prestamo.LibroId)));

        var relacionSocio = entidad.GetForeignKeys()
            .SingleOrDefault(relacion =>
                relacion.Properties.Any(
                    propiedad => propiedad.Name == nameof(Prestamo.SocioId)));

        Assert.NotNull(relacionLibro);
        Assert.NotNull(relacionSocio);

        Assert.Equal(DeleteBehavior.Restrict, relacionLibro!.DeleteBehavior);
        Assert.Equal(DeleteBehavior.Restrict, relacionSocio!.DeleteBehavior);
    }

    private static AppDbContext CrearContexto()
    {
        var opciones = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(
                "Host=localhost;Database=UNAJBOOKS_Test;Username=test;Password=test")
            .Options;

        return new AppDbContext(opciones);
    }
}