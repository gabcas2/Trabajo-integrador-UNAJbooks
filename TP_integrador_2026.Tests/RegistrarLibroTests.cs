using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Libros;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class RegistrarLibroTests
{
    // Caso normal: registra un libro válido.
    [Fact]
    public void Ejecutar_ConDatosValidos_RegistraElLibro()
    {
        var repositorio = new FakeLibroRepository();
        var registrarLibro = new RegistrarLibro(repositorio);

        var libro = registrarLibro.Ejecutar(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            3);

        Assert.NotNull(repositorio.LibroAgregado);
        Assert.Same(repositorio.LibroAgregado, libro);
        Assert.Equal("Harry Potter y la piedra filosofal", libro.Titulo);
        Assert.Equal(3, libro.Stock);
        Assert.True(repositorio.ConsultoExistenciaCodigo);
    }

    // Caso de borde: permite registrar un libro con stock cero.
    [Fact]
    public void Ejecutar_ConStockCero_RegistraElLibroSinEjemplares()
    {
        var repositorio = new FakeLibroRepository();
        var registrarLibro = new RegistrarLibro(repositorio);

        var libro = registrarLibro.Ejecutar(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            0);

        Assert.Equal("La Torre Oscura I: El pistolero", libro.Titulo);
        Assert.Equal(0, libro.Stock);
        Assert.Same(repositorio.LibroAgregado, libro);
    }

    // Caso de error: rechaza el libro si el código ya existe.
    [Fact]
    public void Ejecutar_CuandoElCodigoExiste_LanzaExcepcion()
    {
        var repositorio = new FakeLibroRepository
        {
            CodigoExistente = true
        };

        var registrarLibro = new RegistrarLibro(repositorio);

        Assert.Throws<InvalidOperationException>(() =>
            registrarLibro.Ejecutar(
                "Harry Potter y la piedra filosofal",
                "J. K. Rowling",
                "Salamandra",
                3));

        Assert.Null(repositorio.LibroAgregado);
    }

    // Repositorio simulado para no necesitar PostgreSQL.
    private sealed class FakeLibroRepository : ILibroRepository
    {
        public bool CodigoExistente { get; set; }

        public bool ConsultoExistenciaCodigo { get; private set; }

        public Libro? LibroAgregado { get; private set; }

        public Libro Agregar(Libro libro)
        {
            LibroAgregado = libro;
            return libro;
        }

        public Libro? BuscarPorCodigo(string codigo)
        {
            return null;
        }

        public List<Libro> ObtenerTodos()
        {
            return new List<Libro>();
        }

        public void Eliminar(Libro libro)
        {
        }

        public bool ExisteCodigo(string codigo)
        {
            ConsultoExistenciaCodigo = true;
            return CodigoExistente;
        }

        public bool TienePrestamosActivos(int libroId)
        {
            return false;
        }
    }
}