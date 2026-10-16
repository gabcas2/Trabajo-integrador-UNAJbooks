using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Libros;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class ConsultarLibrosTests
{  // Pruebas unitarias para la clase ConsultarLibros
    [Fact]
     public void FiltrarLibrosConStock_DevuelveSoloLibrosDisponibles()
    {
        var harryPotter = new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            3);

        var torreOscura = new Libro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            0);

        var dune = new Libro(
            "Dune",
            "Frank Herbert",
            "Debolsillo",
            1);

        var repositorio = new FakeLibroRepository(
            harryPotter,
            torreOscura,
            dune);

        var consultarLibros = new ConsultarLibros(repositorio);

        var resultado = consultarLibros.FiltrarLibrosConStock();

        Assert.Equal(2, resultado.Count);
        Assert.Contains(harryPotter, resultado);
        Assert.Contains(dune, resultado);
        Assert.DoesNotContain(torreOscura, resultado);
    }

    [Fact]
    public void FiltrarLibrosSinStock_DevuelveSoloLibrosAgotados()
    {// Prueba unitaria para verificar que la función FiltrarLibrosSinStock devuelve solo los libros que están agotados
        var harryPotter = new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            2);

        var torreOscura = new Libro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            0);

        var repositorio = new FakeLibroRepository(
            harryPotter,
            torreOscura);

        var consultarLibros = new ConsultarLibros(repositorio);

        var resultado = consultarLibros.FiltrarLibrosSinStock();

        Assert.Single(resultado);
        Assert.Equal("La Torre Oscura I: El pistolero", resultado[0].Titulo);
        Assert.Equal(0, resultado[0].Stock);
    }

    [Fact]
    public void ObtenerTitulosLibros_TransformaLibrosEnTitulos()
    {
        var harryPotter = new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            2);

        var torreOscura = new Libro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            1);

        var repositorio = new FakeLibroRepository(
            harryPotter,
            torreOscura);

        var consultarLibros = new ConsultarLibros(repositorio);

        var resultado = consultarLibros.ObtenerTitulosLibros();

        Assert.Equal(
            new[]
            {
                "Harry Potter y la piedra filosofal",
                "La Torre Oscura I: El pistolero"
            },
            resultado);
    }

    [Fact]
    public void BuscarPrimerLibroConStock_DevuelveElPrimeroDisponible()
    {
        var agotado = new Libro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            0);

        var disponible = new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            2);

        var repositorio = new FakeLibroRepository(
            agotado,
            disponible);

        var consultarLibros = new ConsultarLibros(repositorio);

        var resultado = consultarLibros.BuscarPrimerLibroConStock();

        Assert.Same(disponible, resultado);
    }

    [Fact]
    public void BuscarPrimerLibroConStock_SiTodosEstanAgotados_DevuelveNull()
    {
        var torreOscura = new Libro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            0);

        var repositorio = new FakeLibroRepository(torreOscura);

        var consultarLibros = new ConsultarLibros(repositorio);

        var resultado = consultarLibros.BuscarPrimerLibroConStock();

        Assert.Null(resultado);
    }

    [Fact]
    public void FiltrarLibrosConStock_CuandoFallaElRepositorio_LanzaExcepcion()
    {
        var repositorio = new FakeLibroRepository
        {
            FallarAlObtenerTodos = true
        };

        var consultarLibros = new ConsultarLibros(repositorio);

        Assert.Throws<InvalidOperationException>(
            () => consultarLibros.FiltrarLibrosConStock());
    }

    private sealed class FakeLibroRepository : ILibroRepository
    {
        private readonly List<Libro> _libros;

        public bool FallarAlObtenerTodos { get; set; }

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
            return _libros.FirstOrDefault(
                libro => libro.Codigo == codigo);
        }

        public List<Libro> ObtenerTodos()
        {
            if (FallarAlObtenerTodos)
            {
                throw new InvalidOperationException(
                    "Fallo simulado al consultar libros.");
            }

            return _libros;
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