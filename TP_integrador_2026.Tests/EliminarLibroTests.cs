
using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Libros;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class EliminarLibroTests
{
    // Caso normal: eliminar un libro que no tiene préstamos activos.
    [Fact]
    public void Ejecutar_SinPrestamosActivos_EliminaElLibro()
    {
        var libro = new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            2);

        var repositorio = new FakeLibroRepository(libro);
        var eliminarLibro = new EliminarLibro(repositorio);

        eliminarLibro.Ejecutar(libro.Codigo);

        Assert.Empty(repositorio.Libros);
        Assert.Same(libro, repositorio.LibroEliminado);
        Assert.True(repositorio.ConsultoPrestamosActivos);
    }

    // Caso de borde: no se puede eliminar un libro prestado.
    [Fact]
    public void Ejecutar_ConPrestamosActivos_LanzaExcepcionYConservaElLibro()
    {
        var libro = new Libro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            1);

        var repositorio = new FakeLibroRepository(libro)
        {
            TienePrestamosActivosResultado = true
        };

        var eliminarLibro = new EliminarLibro(repositorio);

        var excepcion = Assert.Throws<InvalidOperationException>(
            () => eliminarLibro.Ejecutar(libro.Codigo));

        Assert.Contains("no se puede eliminar", excepcion.Message.ToLowerInvariant());
        Assert.Single(repositorio.Libros);
        Assert.Null(repositorio.LibroEliminado);
    }

    // Caso de error: el código no corresponde a ningún libro.
    [Fact]
    public void Ejecutar_CuandoElLibroNoExiste_LanzaExcepcion()
    {
        var repositorio = new FakeLibroRepository();
        var eliminarLibro = new EliminarLibro(repositorio);

        Assert.Throws<InvalidOperationException>(
            () => eliminarLibro.Ejecutar("CODIGO-INEXISTENTE"));

        Assert.Null(repositorio.LibroEliminado);
        Assert.False(repositorio.ConsultoPrestamosActivos);
    }

    private sealed class FakeLibroRepository : ILibroRepository
    {
        public List<Libro> Libros { get; }

        public Libro? LibroEliminado { get; private set; }

        public bool TienePrestamosActivosResultado { get; set; }

        public bool ConsultoPrestamosActivos { get; private set; }

        public FakeLibroRepository(params Libro[] libros)
        {
            Libros = libros.ToList();
        }

        public Libro Agregar(Libro libro)
        {
            Libros.Add(libro);
            return libro;
        }

        public Libro? BuscarPorCodigo(string codigo)
        {
            return Libros.FirstOrDefault(
                libro => libro.Codigo == codigo);
        }

        public List<Libro> ObtenerTodos()
        {
            return Libros.ToList();
        }

        public void Eliminar(Libro libro)
        {
            LibroEliminado = libro;
            Libros.Remove(libro);
        }

        public bool ExisteCodigo(string codigo)
        {
            return Libros.Any(libro => libro.Codigo == codigo);
        }

        public bool TienePrestamosActivos(int libroId)
        {
            ConsultoPrestamosActivos = true;
            return TienePrestamosActivosResultado;
        }
    }
}
