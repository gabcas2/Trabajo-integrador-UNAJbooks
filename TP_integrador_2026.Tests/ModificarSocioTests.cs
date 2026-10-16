
using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Socios;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class ModificarSocioTests
{
    // Caso normal: modifica el teléfono y la dirección.
    // Al no especificar el tipo, conserva el tipo actual.
    [Fact]
    public void Ejecutar_ConTelefonoYDireccionNuevos_ModificaLosDatos()
    {
        var socio = new Socio(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Bernal");

        var repositorio = new FakeSocioRepository(socio);
        var modificarSocio = new ModificarSocio(repositorio);

        modificarSocio.Ejecutar(
            30123456,
            1145678901,
            "Quilmes Oeste",
            null);

        Assert.Equal(1145678901, socio.NumTelefono);
        Assert.Equal("Quilmes Oeste", socio.Direccion);
        Assert.Same(socio, repositorio.SocioActualizado);
        Assert.Equal(false, repositorio.EsPremiumSolicitado);
        Assert.Equal(1, repositorio.CantidadActualizaciones);
    }

    // Caso de borde: no se solicita ningún cambio.
    // Los datos actuales deben conservarse.
    [Fact]
    public void Ejecutar_SinCambiosSolicitados_ConservaLosDatos()
    {
        var socio = new Socio(
            "Carlos",
            "Gómez",
            45678130,
            1123456789,
            "Bernal Oeste");

        var repositorio = new FakeSocioRepository(socio);
        var modificarSocio = new ModificarSocio(repositorio);

        modificarSocio.Ejecutar(
            45678130,
            null,
            null,
            null);

        Assert.Equal(1123456789, socio.NumTelefono);
        Assert.Equal("Bernal Oeste", socio.Direccion);
        Assert.Same(socio, repositorio.SocioActualizado);
        Assert.Equal(false, repositorio.EsPremiumSolicitado);
        Assert.Equal(1, repositorio.CantidadActualizaciones);
    }

    // Caso de error: el socio no existe.
    [Fact]
    public void Ejecutar_CuandoDniNoExiste_LanzaExcepcion()
    {
        var repositorio = new FakeSocioRepository();
        var modificarSocio = new ModificarSocio(repositorio);

        var excepcion = Assert.Throws<InvalidOperationException>(
            () => modificarSocio.Ejecutar(
                30123456,
                1145678901,
                "Quilmes Oeste",
                true));

        Assert.Contains("No existe un socio", excepcion.Message);
        Assert.Equal(0, repositorio.CantidadActualizaciones);
    }

    private sealed class FakeSocioRepository : ISocioRepository
    {
        private readonly List<Socio> _socios;

        public Socio? SocioActualizado { get; private set; }

        public bool? EsPremiumSolicitado { get; private set; }

        public int CantidadActualizaciones { get; private set; }

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
            SocioActualizado = socio;
            EsPremiumSolicitado = esPremium;
            CantidadActualizaciones++;
        }
    }
}
