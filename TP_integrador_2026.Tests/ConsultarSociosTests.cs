using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Socios;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class ConsultarSociosTests
{
    // Caso normal: devuelve los socios registrados en el repositorio.
    [Fact]
    public void Ejecutar_ConSociosRegistrados_DevuelveLaLista()
    {
        var maria = new Socio(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Bernal Oeste");

        var carlos = new SocioPremium(
            "Carlos",
            "Gómez",
            45678130,
            1145678901,
            "Quilmes");

        var repositorio = new FakeSocioRepository(maria, carlos);
        var consultarSocios = new ConsultarSocios(repositorio);

        var resultado = consultarSocios.Ejecutar();

        Assert.Equal(2, resultado.Count);
        Assert.Contains(maria, resultado);
        Assert.Contains(carlos, resultado);
    }

    // Caso de borde: el repositorio no contiene socios.
    [Fact]
    public void Ejecutar_SinSociosRegistrados_DevuelveListaVacia()
    {
        var repositorio = new FakeSocioRepository();
        var consultarSocios = new ConsultarSocios(repositorio);

        var resultado = consultarSocios.Ejecutar();

        Assert.NotNull(resultado);
        Assert.Empty(resultado);
    }

    // Caso de error: la excepción del repositorio se propaga.
    [Fact]
    public void Ejecutar_CuandoFallaElRepositorio_LanzaExcepcion()
    {
        var repositorio = new FakeSocioRepository
        {
            FallarAlObtenerTodos = true
        };

        var consultarSocios = new ConsultarSocios(repositorio);

        Assert.Throws<InvalidOperationException>(
            () => consultarSocios.Ejecutar());
    }

    private sealed class FakeSocioRepository : ISocioRepository
    {
        private readonly List<Socio> _socios;

        public bool FallarAlObtenerTodos { get; set; }

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
            if (FallarAlObtenerTodos)
            {
                throw new InvalidOperationException(
                    "Fallo simulado al consultar los socios.");
            }

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
            // No se necesita actualizar socios en estas pruebas.
        }
    }
}