
using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Libros;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class BuscarLibroTests
{
    // Caso normal: encuentra un libro por su código.
    [Fact]
    public void Ejecutar_CuandoCodigoExiste_DevuelveElLibro()
    {
        var libro = new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            3);

        var repositorio = new FakeLibroRepository(libro);
        var buscarLibro = new BuscarLibro(repositorio);

        var resultado = buscarLibro.Ejecutar(libro.Codigo);

        Assert.Same(libro, resultado);
        Assert.Equal("Harry Potter y la piedra filosofal", resultado.Titulo);
        Assert.Equal(libro.Codigo, repositorio.UltimoCodigoBuscado);
    }

    // Caso de borde: se recibe un código vacío.
    // La clase no lo valida directamente: delega la búsqueda al repositorio.
    [Fact]
    public void Ejecutar_ConCodigoVacio_LanzaExcepcionDeNoEncontrado()
    {
        var libro = new Libro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            2);

        var repositorio = new FakeLibroRepository(libro);
        var buscarLibro = new BuscarLibro(repositorio);

        Assert.Throws<InvalidOperationException>(
            () => buscarLibro.Ejecutar(""));

        Assert.Equal("", repositorio.UltimoCodigoBuscado);
    }

    // Caso de error: el código no corresponde a ningún libro.
    [Fact]
    public void Ejecutar_ConCodigoInexistente_LanzaExcepcion()
    {
        var repositorio = new FakeLibroRepository();
        var buscarLibro = new BuscarLibro(repositorio);

        var excepcion = Assert.Throws<InvalidOperationException>(
            () => buscarLibro.Ejecutar("CODIGO-INEXISTENTE"));

        Assert.Contains("libro", excepcion.Message.ToLowerInvariant());
    }

    private sealed class FakeLibroRepository : ILibroRepository
    {
        private readonly List<Libro> _libros;

        public string? UltimoCodigoBuscado { get; private set; }

        public FakeLibroRepository(params Libro[] libros)
        {
            _libros = libros.ToList();
        }

        public Libro Agregar(Libro libro)
        {
            _libros.Add(libro);
            return libro;
        }

        public Libro? BuscarPorCodigo(string codigo)
        {
            UltimoCodigoBuscado = codigo;

            return _libros.FirstOrDefault(
                libro => libro.Codigo == codigo);
        }

        public List<Libro> ObtenerTodos()
        {
            return _libros.ToList();
        }

        public void Eliminar(Libro libro)
        {
            _libros.Remove(libro);
        }

        public bool ExisteCodigo(string codigo)
        {
            return _libros.Any(libro => libro.Codigo == codigo);
        }

        public bool TienePrestamosActivos(int libroId)
        {
            return false;
        }
    }
}
