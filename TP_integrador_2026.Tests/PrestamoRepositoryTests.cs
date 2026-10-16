
using Microsoft.EntityFrameworkCore;
using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Infrastructure.Persistence;
using TP_integrador_2026.Infrastructure.Repositories;

namespace TP_integrador_2026.Tests;

public class PrestamoRepositoryTests
{
    // Caso normal: agrega un préstamo al contexto.
    [Fact]
    public void Agregar_ConPrestamoValido_GuardaYDevuelveElPrestamo()
    {
        using var contexto = CrearContexto();
        var (libro, socio) = CrearLibroYSocio(contexto);
        var repositorio = new PrestamoRepository(contexto);

        var prestamo = new Prestamo(libro, socio);

        var resultado = repositorio.Agregar(prestamo);

        Assert.Same(prestamo, resultado);
        Assert.True(resultado.Id > 0);
        Assert.Single(contexto.Prestamos);
    }

    // Caso normal: busca un préstamo y sus entidades relacionadas.
    [Fact]
    public void BuscarPorId_CuandoExiste_DevuelvePrestamoConLibroYSocio()
    {
        using var contexto = CrearContexto();
        var (libro, socio) = CrearLibroYSocio(contexto);
        var repositorio = new PrestamoRepository(contexto);

        var prestamo = repositorio.Agregar(
            new Prestamo(libro, socio));

        var resultado = repositorio.BuscarPorId(prestamo.Id);

        Assert.NotNull(resultado);
        Assert.Equal(prestamo.Id, resultado.Id);
        Assert.Equal(libro.Titulo, resultado.Libro.Titulo);
        Assert.Equal(socio.DNI, resultado.Socio.DNI);
    }

    // Caso de borde: no existe ningún préstamo con ese ID.
    [Fact]
    public void BuscarPorId_CuandoNoExiste_DevuelveNull()
    {
        using var contexto = CrearContexto();
        var repositorio = new PrestamoRepository(contexto);

        var resultado = repositorio.BuscarPorId(999);

        Assert.Null(resultado);
    }

    // Caso normal: devuelve solo los préstamos del socio solicitado.
    [Fact]
    public void ObtenerPorSocio_FiltraPorSocio()
    {
        using var contexto = CrearContexto();

        var (libroMaria, maria) = CrearLibroYSocio(
            contexto,
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "María",
            30123456);

        var (libroCarlos, carlos) = CrearLibroYSocio(
            contexto,
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Carlos",
            45678130);

        var repositorio = new PrestamoRepository(contexto);

        var prestamoMaria = repositorio.Agregar(
            new Prestamo(libroMaria, maria));

        repositorio.Agregar(new Prestamo(libroCarlos, carlos));

        var resultado = repositorio.ObtenerPorSocio(maria.Id);

        Assert.Single(resultado);
        Assert.Equal(prestamoMaria.Id, resultado[0].Id);
        Assert.Equal(maria.Id, resultado[0].SocioId);
    }

    // Caso normal: devuelve préstamos activos y excluye los devueltos.
    [Fact]
    public void ObtenerActivos_ExcluyePrestamosDevueltos()
    {
        using var contexto = CrearContexto();

        var (libroActivo, socio) = CrearLibroYSocio(contexto);

        var libroDevuelto = CrearLibro(
            contexto,
            "La Torre Oscura I: El pistolero",
            "Stephen King");

        var repositorio = new PrestamoRepository(contexto);

        var prestamoActivo = repositorio.Agregar(
            new Prestamo(libroActivo, socio));

        var prestamoDevuelto = new Prestamo(libroDevuelto, socio);
        prestamoDevuelto.RegistrarDevolucion();
        repositorio.Agregar(prestamoDevuelto);

        var resultado = repositorio.ObtenerActivos();

        Assert.Single(resultado);
        Assert.Equal(prestamoActivo.Id, resultado[0].Id);
        Assert.All(resultado, prestamo => Assert.True(prestamo.EstaActivo()));
    }

    // Caso normal y de borde: comprueba si hay préstamos activos
    // para un socio y para una combinación concreta de socio y libro.
    [Fact]
    public void ConsultasDePrestamosActivos_DevuelvenResultadosCorrectos()
    {
        using var contexto = CrearContexto();

        var (libro, socio) = CrearLibroYSocio(contexto);
        var repositorio = new PrestamoRepository(contexto);

        repositorio.Agregar(new Prestamo(libro, socio));

        var otroLibro = CrearLibro(
            contexto,
            "La Torre Oscura I: El pistolero",
            "Stephen King");

        Assert.True(repositorio.TienePrestamosActivos(socio.Id));
        Assert.True(
            repositorio.TienePrestamoActivo(socio.Id, libro.Id));
        Assert.False(
            repositorio.TienePrestamoActivo(socio.Id, otroLibro.Id));
        Assert.False(repositorio.TienePrestamosActivos(999));
    }

    // Caso normal y de borde: cuenta únicamente los préstamos activos
    // del socio indicado.
    [Fact]
    public void ContarPrestamosActivos_CuentaSoloLosDelSocio()
    {
        using var contexto = CrearContexto();

        var (libroUno, maria) = CrearLibroYSocio(
            contexto,
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "María",
            30123456);

        var libroDos = CrearLibro(
            contexto,
            "La Torre Oscura I: El pistolero",
            "Stephen King");

        var (libroCarlos, carlos) = CrearLibroYSocio(
            contexto,
            "Dune",
            "Frank Herbert",
            "Carlos",
            45678130);

        var repositorio = new PrestamoRepository(contexto);

        repositorio.Agregar(new Prestamo(libroUno, maria));
        repositorio.Agregar(new Prestamo(libroDos, maria));

        var devuelto = new Prestamo(libroCarlos, carlos);
        devuelto.RegistrarDevolucion();
        repositorio.Agregar(devuelto);

        Assert.Equal(2, repositorio.ContarPrestamosActivos(maria.Id));
        Assert.Equal(0, repositorio.ContarPrestamosActivos(carlos.Id));
    }

    // Caso normal: actualiza la devolución del préstamo.
    [Fact]
    public void Actualizar_ConPrestamoDevuelto_PersisteLaDevolucion()
    {
        using var contexto = CrearContexto();
        var (libro, socio) = CrearLibroYSocio(contexto);
        var repositorio = new PrestamoRepository(contexto);

        var prestamo = repositorio.Agregar(
            new Prestamo(libro, socio));

        prestamo.RegistrarDevolucion();
        repositorio.Actualizar(prestamo);

        var resultado = repositorio.BuscarPorId(prestamo.Id);

        Assert.NotNull(resultado);
        Assert.NotNull(resultado.FechaDevolucion);
        Assert.False(resultado.EstaActivo());
    }

    private static AppDbContext CrearContexto()
    {
        var opciones = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(opciones);
    }

    private static (Libro Libro, Socio Socio) CrearLibroYSocio(
        AppDbContext contexto,
        string titulo = "Harry Potter y la piedra filosofal",
        string autor = "J. K. Rowling",
        string nombre = "María",
        int dni = 30123456)
    {
        var libro = new Libro(
            titulo,
            autor,
            "Editorial de prueba",
            2);

        var socio = new Socio(
            nombre,
            "Pérez",
            dni,
            1123456789,
            "Bernal Oeste");

        contexto.Libros.Add(libro);
        contexto.Socios.Add(socio);
        contexto.SaveChanges();

        return (libro, socio);
    }

    private static Libro CrearLibro(
        AppDbContext contexto,
        string titulo,
        string autor)
    {
        var libro = new Libro(
            titulo,
            autor,
            "Editorial de prueba",
            2);

        contexto.Libros.Add(libro);
        contexto.SaveChanges();

        return libro;
    }
}
