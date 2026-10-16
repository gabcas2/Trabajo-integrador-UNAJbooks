
using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Prestamos;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class ConsultarPrestamosActivosTests
{
    // Caso normal: devuelve los préstamos activos del repositorio.
    [Fact]
    public void Ejecutar_ConPrestamosActivos_DevuelveLaLista()
    {
        var socio = CrearSocio(30123456, "María", "Pérez");

        var primerLibro = CrearLibro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling");

        var segundoLibro = CrearLibro(
            "La Torre Oscura I: El pistolero",
            "Stephen King");

        var primerPrestamo = new Prestamo(primerLibro, socio);
        var segundoPrestamo = new Prestamo(segundoLibro, socio);

        var repositorio = new FakePrestamoRepository(
            primerPrestamo,
            segundoPrestamo);

        var consultar = new ConsultarPrestamosActivos(repositorio);

        var resultado = consultar.Ejecutar();

        Assert.Equal(2, resultado.Count);
        Assert.Contains(primerPrestamo, resultado);
        Assert.Contains(segundoPrestamo, resultado);
    }

    // Caso de borde: no existen préstamos activos.
    [Fact]
    public void Ejecutar_SinPrestamosActivos_DevuelveListaVacia()
    {
        var repositorio = new FakePrestamoRepository();
        var consultar = new ConsultarPrestamosActivos(repositorio);

        var resultado = consultar.Ejecutar();

        Assert.NotNull(resultado);
        Assert.Empty(resultado);
    }

    // Caso normal: cuenta varios préstamos del mismo socio.
    [Fact]
    public void ContarPrestamosActivosPorSocio_AgrupaPorDni()
    {
        var maria = CrearSocio(30123456, "María", "Pérez");
        var carlos = CrearSocio(45678130, "Carlos", "Gómez");

        var prestamos = new List<Prestamo>
        {
            new(
                CrearLibro("Harry Potter y la piedra filosofal", "J. K. Rowling"),
                maria),

            new(
                CrearLibro("La Torre Oscura I: El pistolero", "Stephen King"),
                maria),

            new(
                CrearLibro("Dune", "Frank Herbert"),
                carlos)
        };

        var consultar = new ConsultarPrestamosActivos(
            new FakePrestamoRepository());

        var resultado = consultar.ContarPrestamosActivosPorSocio(prestamos);

        Assert.Equal(2, resultado.Count);
        Assert.Equal(2, resultado[30123456]);
        Assert.Equal(1, resultado[45678130]);
    }

    // Caso de borde: la lista vacía produce un diccionario vacío.
    [Fact]
    public void ContarPrestamosActivosPorSocio_SinPrestamos_DevuelveDiccionarioVacio()
    {
        var consultar = new ConsultarPrestamosActivos(
            new FakePrestamoRepository());

        var resultado = consultar.ContarPrestamosActivosPorSocio(
            new List<Prestamo>());

        Assert.NotNull(resultado);
        Assert.Empty(resultado);
    }

    // Caso de error: la excepción del repositorio se propaga.
    [Fact]
    public void Ejecutar_CuandoFallaElRepositorio_LanzaExcepcion()
    {
        var repositorio = new FakePrestamoRepository
        {
            FallarAlObtenerActivos = true
        };

        var consultar = new ConsultarPrestamosActivos(repositorio);

        Assert.Throws<InvalidOperationException>(
            () => consultar.Ejecutar());
    }

    private static Socio CrearSocio(
        int dni,
        string nombre,
        string apellido)
    {
        return new Socio(
            nombre,
            apellido,
            dni,
            1123456789,
            "Bernal Oeste");
    }

    private static Libro CrearLibro(string titulo, string autor)
    {
        return new Libro(
            titulo,
            autor,
            "Editorial de prueba",
            1);
    }

    private sealed class FakePrestamoRepository : IPrestamoRepository
    {
        private readonly List<Prestamo> _prestamos;

        public bool FallarAlObtenerActivos { get; set; }

        public FakePrestamoRepository(params Prestamo[] prestamos)
        {
            _prestamos = prestamos.ToList();
        }

        public Prestamo Agregar(Prestamo prestamo)
        {
            _prestamos.Add(prestamo);
            return prestamo;
        }

        public Prestamo? BuscarPorId(int id)
        {
            return null;
        }

        public List<Prestamo> ObtenerTodos()
        {
            return _prestamos.ToList();
        }

        public List<Prestamo> ObtenerPorSocio(int socioId)
        {
            return _prestamos
                .Where(prestamo => prestamo.SocioId == socioId)
                .ToList();
        }

        public List<Prestamo> ObtenerActivos()
        {
            if (FallarAlObtenerActivos)
            {
                throw new InvalidOperationException(
                    "Fallo simulado al consultar préstamos activos.");
            }

            return _prestamos
                .Where(prestamo => prestamo.EstaActivo())
                .ToList();
        }

        public bool TienePrestamosActivos(int socioId)
        {
            return _prestamos.Any(prestamo =>
                prestamo.SocioId == socioId && prestamo.EstaActivo());
        }

        public bool TienePrestamoActivo(int socioId, int libroId)
        {
            return _prestamos.Any(prestamo =>
                prestamo.SocioId == socioId &&
                prestamo.LibroId == libroId &&
                prestamo.EstaActivo());
        }

        public int ContarPrestamosActivos(int socioId)
        {
            return _prestamos.Count(prestamo =>
                prestamo.SocioId == socioId && prestamo.EstaActivo());
        }

        public void Actualizar(Prestamo prestamo)
        {
            // No se necesita persistir cambios en estas pruebas.
        }
    }
}
