
using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Prestamos;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class RegistrarPrestamoTests
{
    // Caso normal: registra el préstamo y descuenta un ejemplar.
    [Fact]
    public void Ejecutar_ConDatosValidos_RegistraPrestamoYReduceStock()
    {
        var socio = CrearSocio();
        var libro = CrearLibro(stock: 2);

        var socioRepository = new FakeSocioRepository(socio);
        var libroRepository = new FakeLibroRepository(libro);
        var prestamoRepository = new FakePrestamoRepository();

        var registrarPrestamo = new RegistrarPrestamo(
            socioRepository,
            libroRepository,
            prestamoRepository);

        var prestamo = registrarPrestamo.Ejecutar(
            socio.DNI,
            libro.Codigo);

        Assert.NotNull(prestamo);
        Assert.Same(socio, prestamo.Socio);
        Assert.Same(libro, prestamo.Libro);
        Assert.Equal(1, libro.Stock);
        Assert.Same(prestamo, prestamoRepository.PrestamoAgregado);
        Assert.True(prestamo.EstaActivo());
    }

    // Caso de borde: se presta el último ejemplar disponible.
    [Fact]
    public void Ejecutar_ConUnSoloEjemplar_DejaElStockEnCero()
    {
        var socio = CrearSocio();
        var libro = CrearLibro(stock: 1);

        var registrarPrestamo = CrearCaso(
            socio,
            libro,
            out var prestamoRepository);

        var prestamo = registrarPrestamo.Ejecutar(
            socio.DNI,
            libro.Codigo);

        Assert.Equal(0, libro.Stock);
        Assert.Same(prestamo, prestamoRepository.PrestamoAgregado);
    }

    // Caso de error: el socio no está registrado.
    [Fact]
    public void Ejecutar_CuandoSocioNoExiste_LanzaExcepcion()
    {
        var libro = CrearLibro(stock: 2);

        var registrarPrestamo = CrearCaso(
            null,
            libro,
            out var prestamoRepository);

        Assert.Throws<InvalidOperationException>(
            () => registrarPrestamo.Ejecutar(
                30123456,
                libro.Codigo));

        Assert.Equal(2, libro.Stock);
        Assert.Null(prestamoRepository.PrestamoAgregado);
    }

    // Caso de error: el libro no está registrado.
    [Fact]
    public void Ejecutar_CuandoLibroNoExiste_LanzaExcepcion()
    {
        var socio = CrearSocio();

        var registrarPrestamo = CrearCaso(
            socio,
            null,
            out var prestamoRepository);

        Assert.Throws<InvalidOperationException>(
            () => registrarPrestamo.Ejecutar(
                socio.DNI,
                "CODIGO-INEXISTENTE"));

        Assert.Null(prestamoRepository.PrestamoAgregado);
    }

    // Caso de error: no hay ejemplares disponibles.
    [Fact]
    public void Ejecutar_SinStock_LanzaExcepcionYNoRegistraPrestamo()
    {
        var socio = CrearSocio();
        var libro = CrearLibro(stock: 0);

        var registrarPrestamo = CrearCaso(
            socio,
            libro,
            out var prestamoRepository);

        var excepcion = Assert.Throws<InvalidOperationException>(
            () => registrarPrestamo.Ejecutar(
                socio.DNI,
                libro.Codigo));

        Assert.Contains("ejemplares disponibles", excepcion.Message);
        Assert.Equal(0, libro.Stock);
        Assert.Null(prestamoRepository.PrestamoAgregado);
    }

    // Caso de error: el socio ya tiene este libro prestado.
    [Fact]
    public void Ejecutar_ConPrestamoDuplicado_LanzaExcepcion()
    {
        var socio = CrearSocio();
        var libro = CrearLibro(stock: 2);

        var registrarPrestamo = CrearCaso(
            socio,
            libro,
            out var prestamoRepository);

        prestamoRepository.TienePrestamoActivoResultado = true;

        Assert.Throws<InvalidOperationException>(
            () => registrarPrestamo.Ejecutar(
                socio.DNI,
                libro.Codigo));

        Assert.Equal(2, libro.Stock);
        Assert.Null(prestamoRepository.PrestamoAgregado);
    }

    // Caso de error: el socio alcanzó su límite de préstamos activos.
    [Fact]
    public void Ejecutar_CuandoAlcanzaElLimite_LanzaExcepcion()
    {
        var socio = CrearSocio();
        var libro = CrearLibro(stock: 2);

        var registrarPrestamo = CrearCaso(
            socio,
            libro,
            out var prestamoRepository);

        prestamoRepository.CantidadPrestamosActivos = 4;

        Assert.Throws<InvalidOperationException>(
            () => registrarPrestamo.Ejecutar(
                socio.DNI,
                libro.Codigo));

        Assert.Equal(2, libro.Stock);
        Assert.Null(prestamoRepository.PrestamoAgregado);
    }

    private static Socio CrearSocio()
    {
        return new Socio(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Bernal Oeste");
    }

    private static Libro CrearLibro(int stock)
    {
        return new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            stock);
    }

    private static RegistrarPrestamo CrearCaso(
        Socio? socio,
        Libro? libro,
        out FakePrestamoRepository prestamoRepository)
    {
        var socioRepository = new FakeSocioRepository(
            socio is null ? Array.Empty<Socio>() : new[] { socio });

        var libroRepository = new FakeLibroRepository(
            libro is null ? Array.Empty<Libro>() : new[] { libro });

        prestamoRepository = new FakePrestamoRepository();

        return new RegistrarPrestamo(
            socioRepository,
            libroRepository,
            prestamoRepository);
    }

    private sealed class FakeSocioRepository : ISocioRepository
    {
        private readonly List<Socio> _socios;

        public FakeSocioRepository(params Socio[] socios)
        {
            _socios = socios.ToList();
        }

        public Socio Agregar(Socio socio)
        {
            _socios.Add(socio);
            return socio;
        }

        public Socio? BuscarPorDNI(int dni)
        {
            return _socios.FirstOrDefault(socio => socio.DNI == dni);
        }

        public List<Socio> ObtenerTodos()
        {
            return _socios.ToList();
        }

        public void Eliminar(Socio socio)
        {
            _socios.Remove(socio);
        }

        public bool ExisteDNI(int dni)
        {
            return _socios.Any(socio => socio.DNI == dni);
        }

        public void Actualizar(Socio socio, bool esPremium)
        {
        }
    }

    private sealed class FakeLibroRepository : ILibroRepository
    {
        private readonly List<Libro> _libros;

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
            return _libros.FirstOrDefault(libro => libro.Codigo == codigo);
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

    private sealed class FakePrestamoRepository : IPrestamoRepository
    {
        public Prestamo? PrestamoAgregado { get; private set; }

        public bool TienePrestamoActivoResultado { get; set; }

        public int CantidadPrestamosActivos { get; set; }

        public Prestamo Agregar(Prestamo prestamo)
        {
            PrestamoAgregado = prestamo;
            return prestamo;
        }

        public Prestamo? BuscarPorId(int id)
        {
            return null;
        }

        public List<Prestamo> ObtenerTodos()
        {
            return PrestamoAgregado is null
                ? new List<Prestamo>()
                : new List<Prestamo> { PrestamoAgregado };
        }

        public List<Prestamo> ObtenerPorSocio(int socioId)
        {
            return new List<Prestamo>();
        }

        public List<Prestamo> ObtenerActivos()
        {
            return new List<Prestamo>();
        }

        public bool TienePrestamosActivos(int socioId)
        {
            return false;
        }

        public bool TienePrestamoActivo(int socioId, int libroId)
        {
            return TienePrestamoActivoResultado;
        }

        public int ContarPrestamosActivos(int socioId)
        {
            return CantidadPrestamosActivos;
        }

        public void Actualizar(Prestamo prestamo)
        {
        }
    }
}
