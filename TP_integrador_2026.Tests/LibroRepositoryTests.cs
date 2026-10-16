
using Microsoft.EntityFrameworkCore;
using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Infrastructure.Persistence;
using TP_integrador_2026.Infrastructure.Repositories;

namespace TP_integrador_2026.Tests;

public class LibroRepositoryTests
{
    // Caso normal: agrega un libro y lo guarda en el contexto.
    [Fact]
    public void Agregar_ConLibroValido_GuardaYDevuelveElLibro()
    {
        using var contexto = CrearContexto();
        var repositorio = new LibroRepository(contexto);

        var libro = CrearLibro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            3);

        var resultado = repositorio.Agregar(libro);

        Assert.Same(libro, resultado);
        Assert.Equal(libro.Titulo, contexto.Libros.Single().Titulo);
        Assert.Equal(3, contexto.Libros.Single().Stock);
    }

    // Caso normal: busca un libro por su código.
    [Fact]
    public void BuscarPorCodigo_CuandoExiste_DevuelveElLibro()
    {
        using var contexto = CrearContexto();
        var repositorio = new LibroRepository(contexto);

        var libro = CrearLibro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            2);

        contexto.Libros.Add(libro);
        contexto.SaveChanges();

        var resultado = repositorio.BuscarPorCodigo(libro.Codigo);

        Assert.NotNull(resultado);
        Assert.Equal(libro.Codigo, resultado.Codigo);
        Assert.Equal(libro.Titulo, resultado.Titulo);
    }

    // Caso de borde: consultar un código inexistente devuelve null.
    [Fact]
    public void BuscarPorCodigo_CuandoNoExiste_DevuelveNull()
    {
        using var contexto = CrearContexto();
        var repositorio = new LibroRepository(contexto);

        var resultado = repositorio.BuscarPorCodigo("CODIGO-INEXISTENTE");

        Assert.Null(resultado);
    }

    // Caso normal: devuelve los libros ordenados por título.
    [Fact]
    public void ObtenerTodos_ConVariosLibros_OrdenaPorTitulo()
    {
        using var contexto = CrearContexto();
        var repositorio = new LibroRepository(contexto);

        contexto.Libros.AddRange(
            CrearLibro("La Torre Oscura I: El pistolero", "Stephen King", 1),
            CrearLibro("Dune", "Frank Herbert", 2),
            CrearLibro("Harry Potter y la piedra filosofal", "J. K. Rowling", 3));

        contexto.SaveChanges();

        var resultado = repositorio.ObtenerTodos();

        Assert.Equal(
            new[]
            {
                "Dune",
                "Harry Potter y la piedra filosofal",
                "La Torre Oscura I: El pistolero"
            },
            resultado.Select(libro => libro.Titulo).ToArray());
    }

    // Caso normal y de borde: comprueba códigos existentes e inexistentes.
    [Fact]
    public void ExisteCodigo_DevuelveResultadoSegunElCodigo()
    {
        using var contexto = CrearContexto();
        var repositorio = new LibroRepository(contexto);

        var libro = CrearLibro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            1);

        contexto.Libros.Add(libro);
        contexto.SaveChanges();

        Assert.True(repositorio.ExisteCodigo(libro.Codigo));
        Assert.False(repositorio.ExisteCodigo("CODIGO-INEXISTENTE"));
    }

    // Comprueba si el libro tiene un préstamo activo o ya devuelto.
    [Fact]
    public void TienePrestamosActivos_DistinguePrestamoActivoDeDevuelto()
    {
        using var contexto = CrearContexto();
        var repositorio = new LibroRepository(contexto);

        var libro = CrearLibro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            1);

        var socio = new Socio(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Bernal Oeste");

        contexto.Libros.Add(libro);
        contexto.Socios.Add(socio);
        contexto.SaveChanges();

        var prestamo = new Prestamo(libro, socio);

        contexto.Prestamos.Add(prestamo);
        contexto.SaveChanges();

        Assert.True(repositorio.TienePrestamosActivos(libro.Id));

        prestamo.RegistrarDevolucion();
        contexto.SaveChanges();

        Assert.False(repositorio.TienePrestamosActivos(libro.Id));
    }

    private static AppDbContext CrearContexto()
    {
        var opciones = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(opciones);
    }

    private static Libro CrearLibro(
        string titulo,
        string autor,
        int stock)
    {
        return new Libro(
            titulo,
            autor,
            "Editorial de prueba",
            stock);
    }
}
