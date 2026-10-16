
using Microsoft.EntityFrameworkCore;
using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Infrastructure.Persistence;
using TP_integrador_2026.Infrastructure.Repositories;

namespace TP_integrador_2026.Tests;

public class SocioRepositoryTests
{
    // Caso normal: registra y persiste un socio.
    [Fact]
    public void Agregar_ConSocioValido_GuardaElSocio()
    {
        using var contexto = CrearContexto();
        var repositorio = new SocioRepository(contexto);

        var socio = CrearSocio(
            "María",
            "Pérez",
            30123456);

        var resultado = repositorio.Agregar(socio);

        Assert.Same(socio, resultado);
        Assert.Equal(1, contexto.Socios.Count());
        Assert.Equal(30123456, contexto.Socios.Single().DNI);
    }

    // Caso normal: encuentra un socio por DNI.
    [Fact]
    public void BuscarPorDNI_CuandoExiste_DevuelveElSocio()
    {
        using var contexto = CrearContexto();
        var repositorio = new SocioRepository(contexto);

        var socio = CrearSocio("Carlos", "Gomez", 45678130);

        contexto.Socios.Add(socio);
        contexto.SaveChanges();

        var resultado = repositorio.BuscarPorDNI(45678130);

        Assert.NotNull(resultado);
        Assert.Equal("Carlos", resultado.Nombre);
        Assert.Equal("Gomez", resultado.Apellido);
    }

    // Caso de borde: no encuentra un DNI inexistente.
    [Fact]
    public void BuscarPorDNI_CuandoNoExiste_DevuelveNull()
    {
        using var contexto = CrearContexto();
        var repositorio = new SocioRepository(contexto);

        var resultado = repositorio.BuscarPorDNI(30123456);

        Assert.Null(resultado);
    }

    // Caso normal: ordena por apellido y después por nombre.
    [Fact]
    public void ObtenerTodos_ConVariosSocios_OrdenaPorApellidoYNombre()
    {
        using var contexto = CrearContexto();
        var repositorio = new SocioRepository(contexto);

        contexto.Socios.AddRange(
            CrearSocio("Maria", "Perez", 30123456),
            CrearSocio("Carlos", "Gomez", 45678130),
            CrearSocio("Zoe", "Pereira", 40123456),
            CrearSocio("Ana", "Pereira", 50123456));

        contexto.SaveChanges();

        var resultado = repositorio.ObtenerTodos();

        Assert.Equal(
            new[]
            {
                "Carlos Gomez",
                "Ana Pereira",
                "Zoe Pereira",
                "Maria Perez"
            },
            resultado.Select(socio => $"{socio.Nombre} {socio.Apellido}")
                .ToArray());
    }

    // Caso normal y de borde: detecta DNI existentes e inexistentes.
    [Fact]
    public void ExisteDNI_DevuelveResultadoSegunElDni()
    {
        using var contexto = CrearContexto();
        var repositorio = new SocioRepository(contexto);

        contexto.Socios.Add(
            CrearSocio("María", "Pérez", 30123456));

        contexto.SaveChanges();

        Assert.True(repositorio.ExisteDNI(30123456));
        Assert.False(repositorio.ExisteDNI(99999999));
    }

    // Caso normal: elimina un socio y los préstamos que tiene asociados.
    [Fact]
    public void Eliminar_ConPrestamosAsociados_EliminaSocioYPrestamos()
    {
        using var contexto = CrearContexto();
        var repositorio = new SocioRepository(contexto);

        var socio = CrearSocio("María", "Pérez", 30123456);
        var libro = new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            2);

        contexto.Socios.Add(socio);
        contexto.Libros.Add(libro);
        contexto.SaveChanges();

        var prestamo = new Prestamo(libro, socio);

        contexto.Prestamos.Add(prestamo);
        contexto.SaveChanges();

        repositorio.Eliminar(socio);

        Assert.Empty(contexto.Socios.ToList());
        Assert.Empty(contexto.Prestamos.ToList());
        Assert.Single(contexto.Libros.ToList());
    }

    private static AppDbContext CrearContexto()
    {
        var opciones = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(opciones);
    }

    private static Socio CrearSocio(
        string nombre,
        string apellido,
        int dni)
    {
        return new Socio(
            nombre,
            apellido,
            dni,
            1123456789,
            "Bernal Oeste");
    }
}
