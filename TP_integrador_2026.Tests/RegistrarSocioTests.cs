using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Socios;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class RegistrarSocioTests
{
    // Caso normal: registrar un socio común.
    [Fact]
    public void Ejecutar_SocioNormal_RegistraSocio()
    {
        var repositorio = new FakeSocioRepository();
        var registrarSocio = new RegistrarSocio(repositorio);

        var socio = registrarSocio.Ejecutar(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Bernal Oeste",
            false);

        Assert.IsType<Socio>(socio);
        Assert.Equal(30123456, socio.DNI);
        Assert.Same(repositorio.SocioAgregado, socio);
    }

    // Caso de borde: registrar un socio premium.
    [Fact]
    public void Ejecutar_SocioPremium_RegistraSocioConBeneficiosPremium()
    {
        var repositorio = new FakeSocioRepository();
        var registrarSocio = new RegistrarSocio(repositorio);

        var socio = registrarSocio.Ejecutar(
            "Carlos",
            "Gómez",
            45678130,
            1123456789,
            "Bernal Oeste",
            true);

        Assert.IsType<SocioPremium>(socio);
        Assert.Equal(20, socio.MaxPrestamosActivos);
        Assert.Equal(30, socio.DiasPrestamo);
        Assert.Same(repositorio.SocioAgregado, socio);
    }

    // Caso de error: impedir el registro de un DNI duplicado.
    [Fact]
    public void Ejecutar_DniDuplicado_LanzaExcepcion()
    {
        var repositorio = new FakeSocioRepository
        {
            DniExistente = true
        };

        var registrarSocio = new RegistrarSocio(repositorio);

        Assert.Throws<InvalidOperationException>(() =>
            registrarSocio.Ejecutar(
                "María",
                "Pérez",
                30123456,
                1123456789,
                "Bernal Oeste",
                false));

        Assert.Null(repositorio.SocioAgregado);
    }

    private sealed class FakeSocioRepository : ISocioRepository
    {
        public bool DniExistente { get; set; }

        public Socio? SocioAgregado { get; private set; }

        public Socio Agregar(Socio socio)
        {
            SocioAgregado = socio;
            return socio;
        }

        public Socio? BuscarPorDNI(int dni)
        {
            return null;
        }

        public List<Socio> ObtenerTodos()
        {
            return new List<Socio>();
        }

        public void Eliminar(Socio socio)
        {
        }

        public bool ExisteDNI(int dni)
        {
            return DniExistente;
        }

        public void Actualizar(Socio socio, bool esPremium)
        {
        }
    }
}