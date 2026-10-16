
using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Socios;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class EliminarSocioTests
{
    // Caso normal: se elimina un socio sin préstamos activos.
    [Fact]
    public void Ejecutar_SinPrestamosActivos_EliminaElSocio()
    {
        var socio = new Socio(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Bernal Oeste");

        var socioRepository = new FakeSocioRepository(socio);
        var prestamoRepository = new FakePrestamoRepository
        {
            TienePrestamosActivosResultado = false
        };

        var eliminarSocio = new EliminarSocio(
            socioRepository,
            prestamoRepository);

        eliminarSocio.Ejecutar(30123456);

        Assert.Empty(socioRepository.Socios);
        Assert.Same(socio, socioRepository.SocioEliminado);
        Assert.True(prestamoRepository.ConsultoPrestamosActivos);
    }

    // Caso de borde: no se permite eliminar un socio con préstamos activos.
    [Fact]
    public void Ejecutar_ConPrestamosActivos_LanzaExcepcionYConservaElSocio()
    {
        var socio = new Socio(
            "Carlos",
            "Gómez",
            45678130,
            1123456789,
            "Bernal Oeste");

        var socioRepository = new FakeSocioRepository(socio);
        var prestamoRepository = new FakePrestamoRepository
        {
            TienePrestamosActivosResultado = true
        };

        var eliminarSocio = new EliminarSocio(
            socioRepository,
            prestamoRepository);

        var excepcion = Assert.Throws<InvalidOperationException>(
            () => eliminarSocio.Ejecutar(45678130));

        Assert.Contains("tiene libros prestados", excepcion.Message);
        Assert.Single(socioRepository.Socios);
        Assert.Null(socioRepository.SocioEliminado);
    }

    // Caso de error: el socio no existe.
    [Fact]
    public void Ejecutar_CuandoSocioNoExiste_LanzaExcepcion()
    {
        var socioRepository = new FakeSocioRepository();
        var prestamoRepository = new FakePrestamoRepository
        {
            TienePrestamosActivosResultado = true
        };

        var eliminarSocio = new EliminarSocio(
            socioRepository,
            prestamoRepository);

        var excepcion = Assert.Throws<InvalidOperationException>(
            () => eliminarSocio.Ejecutar(30123456));

        Assert.Contains("No existe un socio", excepcion.Message);
        Assert.False(prestamoRepository.ConsultoPrestamosActivos);
    }

    private sealed class FakeSocioRepository : ISocioRepository
    {
        public List<Socio> Socios { get; }

        public Socio? SocioEliminado { get; private set; }

        public FakeSocioRepository(params Socio[] socios)
        {
            Socios = socios.ToList();
        }

        public Socio Agregar(Socio socio)
        {
            Socios.Add(socio);
            return socio;
        }

        public Socio? BuscarPorDNI(int dni)
        {
            return Socios.FirstOrDefault(socio => socio.DNI == dni);
        }

        public List<Socio> ObtenerTodos()
        {
            return Socios.ToList();
        }

        public void Eliminar(Socio socio)
        {
            SocioEliminado = socio;
            Socios.Remove(socio);
        }

        public bool ExisteDNI(int dni)
        {
            return Socios.Any(socio => socio.DNI == dni);
        }

        public void Actualizar(Socio socio, bool esPremium)
        {
            // No se necesita actualizar socios en estas pruebas.
        }
    }

    private sealed class FakePrestamoRepository : IPrestamoRepository
    {
        public bool TienePrestamosActivosResultado { get; set; }

        public bool ConsultoPrestamosActivos { get; private set; }

        public Prestamo Agregar(Prestamo prestamo)
        {
            return prestamo;
        }

        public Prestamo? BuscarPorId(int id)
        {
            return null;
        }

        public List<Prestamo> ObtenerTodos()
        {
            return new List<Prestamo>();
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
            ConsultoPrestamosActivos = true;
            return TienePrestamosActivosResultado;
        }

        public bool TienePrestamoActivo(int socioId, int libroId)
        {
            return false;
        }

        public int ContarPrestamosActivos(int socioId)
        {
            return 0;
        }

        public void Actualizar(Prestamo prestamo)
        {
            // No se necesita actualizar préstamos en estas pruebas.
        }
    }
}
