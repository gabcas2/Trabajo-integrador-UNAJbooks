
using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Prestamos;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class RegistrarDevolucionTests
{
    // Caso normal: registra la devolución y recupera un ejemplar.
    [Fact]
    public void Ejecutar_ConPrestamoActivo_RegistraDevolucionYAumentaStock()
    {
        var libro = CrearLibro(stock: 1);
        var socio = CrearSocio();
        var prestamo = new Prestamo(libro, socio);

        var repositorio = new FakePrestamoRepository(prestamo);
        var registrarDevolucion = new RegistrarDevolucion(repositorio);

        var resultado = registrarDevolucion.Ejecutar(1);

        Assert.Same(prestamo, resultado);
        Assert.False(prestamo.EstaActivo());
        Assert.NotNull(prestamo.FechaDevolucion);
        Assert.Equal(2, libro.Stock);
        Assert.Same(prestamo, repositorio.PrestamoActualizado);
        Assert.Equal(1, repositorio.CantidadActualizaciones);
    }

    // Caso de borde: al devolver un ejemplar, el stock pasa de cero a uno.
    [Fact]
    public void Ejecutar_ConStockCero_AumentaStockAUno()
    {
        var libro = CrearLibro(stock: 0);
        var socio = CrearSocio();
        var prestamo = new Prestamo(libro, socio);

        var repositorio = new FakePrestamoRepository(prestamo);
        var registrarDevolucion = new RegistrarDevolucion(repositorio);

        registrarDevolucion.Ejecutar(1);

        Assert.Equal(1, libro.Stock);
        Assert.False(prestamo.EstaActivo());
        Assert.Equal(1, repositorio.CantidadActualizaciones);
    }

    // Caso de error: el préstamo solicitado no existe.
    [Fact]
    public void Ejecutar_ConPrestamoInexistente_LanzaExcepcion()
    {
        var repositorio = new FakePrestamoRepository();
        var registrarDevolucion = new RegistrarDevolucion(repositorio);

        Assert.Throws<InvalidOperationException>(
            () => registrarDevolucion.Ejecutar(999));

        Assert.Equal(0, repositorio.CantidadActualizaciones);
        Assert.Null(repositorio.PrestamoActualizado);
    }

    // Caso de error: el préstamo ya fue devuelto.
    [Fact]
    public void Ejecutar_ConPrestamoYaDevuelto_LanzaExcepcion()
    {
        var libro = CrearLibro(stock: 1);
        var socio = CrearSocio();
        var prestamo = new Prestamo(libro, socio);

        prestamo.RegistrarDevolucion();

        var repositorio = new FakePrestamoRepository(prestamo);
        var registrarDevolucion = new RegistrarDevolucion(repositorio);

        Assert.Throws<InvalidOperationException>(
            () => registrarDevolucion.Ejecutar(1));

        Assert.Equal(1, libro.Stock);
        Assert.Equal(0, repositorio.CantidadActualizaciones);
        Assert.Null(repositorio.PrestamoActualizado);
    }

    private static Libro CrearLibro(int stock)
    {
        return new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            stock);
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

    private sealed class FakePrestamoRepository : IPrestamoRepository
    {
        private readonly Prestamo? _prestamo;

        public Prestamo? PrestamoActualizado { get; private set; }

        public int CantidadActualizaciones { get; private set; }

        public FakePrestamoRepository(Prestamo? prestamo = null)
        {
            _prestamo = prestamo;
        }

        public Prestamo Agregar(Prestamo prestamo)
        {
            return prestamo;
        }

        public Prestamo? BuscarPorId(int id)
        {
            return id == 1 ? _prestamo : null;
        }

        public List<Prestamo> ObtenerTodos()
        {
            return _prestamo is null
                ? new List<Prestamo>()
                : new List<Prestamo> { _prestamo };
        }

        public List<Prestamo> ObtenerPorSocio(int socioId)
        {
            return _prestamo is not null &&
                   _prestamo.SocioId == socioId
                ? new List<Prestamo> { _prestamo }
                : new List<Prestamo>();
        }

        public List<Prestamo> ObtenerActivos()
        {
            return _prestamo is not null && _prestamo.EstaActivo()
                ? new List<Prestamo> { _prestamo }
                : new List<Prestamo>();
        }

        public bool TienePrestamosActivos(int socioId)
        {
            return _prestamo is not null &&
                   _prestamo.SocioId == socioId &&
                   _prestamo.EstaActivo();
        }

        public bool TienePrestamoActivo(int socioId, int libroId)
        {
            return _prestamo is not null &&
                   _prestamo.SocioId == socioId &&
                   _prestamo.LibroId == libroId &&
                   _prestamo.EstaActivo();
        }

        public int ContarPrestamosActivos(int socioId)
        {
            return TienePrestamosActivos(socioId) ? 1 : 0;
        }

        public void Actualizar(Prestamo prestamo)
        {
            PrestamoActualizado = prestamo;
            CantidadActualizaciones++;
        }
    }
}
