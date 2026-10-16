using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Socios;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class BuscarSocioTests
{
    // Caso normal: encuentra un socio por su DNI.
    [Fact]
    public void Ejecutar_CuandoDniExiste_DevuelveElSocio()
    {
        var socio = new Socio(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Bernal Oeste");

        var repositorio = new FakeSocioRepository(socio);
        var buscarSocio = new BuscarSocio(repositorio);

        var resultado = buscarSocio.Ejecutar(30123456);

        Assert.Same(socio, resultado);
        Assert.Equal("María", resultado.Nombre);
        Assert.Equal(30123456, resultado.DNI);
    }

    // Caso de borde: encuentra un socio con el DNI positivo mínimo.
    [Fact]
    public void Ejecutar_ConDniUno_DevuelveElSocio()
    {
        var socio = new Socio(
            "Ana",
            "Gómez",
            1,
            1,
            "");

        var repositorio = new FakeSocioRepository(socio);
        var buscarSocio = new BuscarSocio(repositorio);

        var resultado = buscarSocio.Ejecutar(1);

        Assert.Same(socio, resultado);
        Assert.Equal(1, resultado.DNI);
    }

    // Caso de error: el DNI no está registrado.
    [Fact]
    public void Ejecutar_CuandoDniNoExiste_LanzaExcepcion()
    {
        var repositorio = new FakeSocioRepository();
        var buscarSocio = new BuscarSocio(repositorio);

        var excepcion = Assert.Throws<InvalidOperationException>(
            () => buscarSocio.Ejecutar(30123456));

        Assert.Contains("No existe un socio", excepcion.Message);
    }

    // Repositorio simulado: mantiene los datos solo durante cada prueba.
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
            // No se necesita modificar socios en estas pruebas.
        }
    }
}