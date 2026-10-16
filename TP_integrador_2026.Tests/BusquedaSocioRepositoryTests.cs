
using Microsoft.EntityFrameworkCore;
using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Infrastructure.Persistence;
using TP_integrador_2026.Infrastructure.Repositories;

namespace TP_integrador_2026.Tests;

public class BusquedaSocioRepositoryTests
{
    // Caso normal: encuentra un socio por DNI.
    [Fact]
    public void BuscarPorDni_CuandoExiste_DevuelveElSocio()
    {
        using var contexto = CrearContexto();

        var socio = CrearSocio(
            "María",
            "Pérez",
            30123456);

        contexto.Socios.Add(socio);
        contexto.SaveChanges();

        var repositorio = new BusquedaSocioRepository(contexto);

        var resultado = repositorio.BuscarPorDni(30123456);

        Assert.NotNull(resultado);
        Assert.Equal("María", resultado.Nombre);
        Assert.Equal("Pérez", resultado.Apellido);
        Assert.Equal(30123456, resultado.DNI);
    }

    // Caso de borde: no encuentra el DNI solicitado.
    [Fact]
    public void BuscarPorDni_CuandoNoExiste_DevuelveNull()
    {
        using var contexto = CrearContexto();
        var repositorio = new BusquedaSocioRepository(contexto);

        var resultado = repositorio.BuscarPorDni(99999999);

        Assert.Null(resultado);
    }

    // Caso normal: filtra los préstamos del socio y coloca
    // los activos antes que los devueltos.
    [Fact]
    public void ObtenerPrestamosPorSocio_FiltraYOrdenaPorEstado()
    {
        using var contexto = CrearContexto();

        var maria = CrearSocio(
            "María",
            "Pérez",
            30123456);

        var carlos = CrearSocio(
            "Carlos",
            "Gómez",
            45678130);

        var libroDevuelto = CrearLibro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling");

        var libroActivo = CrearLibro(
            "La Torre Oscura I: El pistolero",
            "Stephen King");

        var libroOtroSocio = CrearLibro(
            "Dune",
            "Frank Herbert");

        contexto.Socios.AddRange(maria, carlos);
        contexto.Libros.AddRange(
            libroDevuelto,
            libroActivo,
            libroOtroSocio);

        contexto.SaveChanges();

        var prestamoDevuelto = new Prestamo(
            libroDevuelto,
            maria);

        prestamoDevuelto.RegistrarDevolucion();

        var prestamoActivo = new Prestamo(
            libroActivo,
            maria);

        var prestamoCarlos = new Prestamo(
            libroOtroSocio,
            carlos);

        contexto.Prestamos.AddRange(
            prestamoDevuelto,
            prestamoActivo,
            prestamoCarlos);

        contexto.SaveChanges();

        var repositorio = new BusquedaSocioRepository(contexto);

        var resultado = repositorio.ObtenerPrestamosPorSocio(maria.Id);

        Assert.Equal(2, resultado.Count);

        // El préstamo activo debe aparecer antes que el devuelto.
        Assert.Equal(libroActivo.Id, resultado[0].LibroId);
        Assert.Null(resultado[0].FechaDevolucion);

        Assert.Equal(libroDevuelto.Id, resultado[1].LibroId);
        Assert.NotNull(resultado[1].FechaDevolucion);

        // Las relaciones incluidas deben estar disponibles.
        Assert.Equal("María", resultado[0].Socio.Nombre);
        Assert.Equal("La Torre Oscura I: El pistolero", resultado[0].Libro.Titulo);
    }

    // Caso de borde: un socio sin préstamos devuelve una lista vacía.
    [Fact]
    public void ObtenerPrestamosPorSocio_SinPrestamos_DevuelveListaVacia()
    {
        using var contexto = CrearContexto();

        var socio = CrearSocio(
            "Ana",
            "Gómez",
            30123457);

        contexto.Socios.Add(socio);
        contexto.SaveChanges();

        var repositorio = new BusquedaSocioRepository(contexto);

        var resultado = repositorio.ObtenerPrestamosPorSocio(socio.Id);

        Assert.NotNull(resultado);
        Assert.Empty(resultado);
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

    private static Libro CrearLibro(
        string titulo,
        string autor)
    {
        return new Libro(
            titulo,
            autor,
            "Editorial de prueba",
            2);
    }
}
