
using Microsoft.EntityFrameworkCore;
using TP_integrador_2026.Infrastructure.Persistence;
using Xunit;

namespace TP_integrador_2026.Tests;

[CollectionDefinition("AppDbContextFactory", DisableParallelization = true)]
public class AppDbContextFactoryCollection
{
}

[Collection("AppDbContextFactory")]
public class AppDbContextFactoryTests
{
    // Caso normal: crea el contexto con una configuración válida.
    [Fact]
    public void CreateDbContext_ConConfiguracionValida_UsaProveedorNpgsql()
    {
        var directorioAnterior = Directory.GetCurrentDirectory();
        var directorioTemporal = CrearDirectorioTemporal();

        try
        {
            File.WriteAllText(
                Path.Combine(directorioTemporal, "appsettings.json"),
                """
                {
                  "ConnectionStrings": {
                    "DefaultConnection": "Host=localhost;Database=UNAJBOOKS_Test;Username=test;Password=test"
                  }
                }
                """);

            Directory.SetCurrentDirectory(directorioTemporal);

            using var contexto =
                new AppDbContextFactory().CreateDbContext(Array.Empty<string>());

            Assert.Equal(
                "Npgsql.EntityFrameworkCore.PostgreSQL",
                contexto.Database.ProviderName);

            Assert.Contains(
                "UNAJBOOKS_Test",
                contexto.Database.GetDbConnection().ConnectionString);
        }
        finally
        {
            Directory.SetCurrentDirectory(directorioAnterior);
            Directory.Delete(directorioTemporal, recursive: true);
        }
    }

    // Caso de error: falta el archivo de configuración obligatorio.
    [Fact]
    public void CreateDbContext_SinAppSettings_LanzaErrorDeArchivo()
    {
        var directorioAnterior = Directory.GetCurrentDirectory();
        var directorioTemporal = CrearDirectorioTemporal();

        try
        {
            Directory.SetCurrentDirectory(directorioTemporal);

            var factory = new AppDbContextFactory();

            Assert.ThrowsAny<IOException>(
                () => factory.CreateDbContext(Array.Empty<string>()));
        }
        finally
        {
            Directory.SetCurrentDirectory(directorioAnterior);
            Directory.Delete(directorioTemporal, recursive: true);
        }
    }

    // Caso de error: el JSON no tiene una estructura válida.
    [Fact]
    public void CreateDbContext_ConJsonInvalido_LanzaExcepcion()
    {
        var directorioAnterior = Directory.GetCurrentDirectory();
        var directorioTemporal = CrearDirectorioTemporal();

        try
        {
            File.WriteAllText(
                Path.Combine(directorioTemporal, "appsettings.json"),
                """
                {
                  "ConnectionStrings": {
                    "DefaultConnection":
                  }
                }
                """);

            Directory.SetCurrentDirectory(directorioTemporal);

            var factory = new AppDbContextFactory();

            Assert.Throws<InvalidDataException>(() => factory.CreateDbContext(Array.Empty<string>()));
        }
        finally
        {
            Directory.SetCurrentDirectory(directorioAnterior);
            Directory.Delete(directorioTemporal, recursive: true);
        }
    }

    private static string CrearDirectorioTemporal()
    {
        var directorio = Path.Combine(
            Path.GetTempPath(),
            $"UNAJBOOKS-Factory-{Guid.NewGuid():N}");

        Directory.CreateDirectory(directorio);

        return directorio;
    }
}
