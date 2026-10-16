
using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Prestamos;
using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Domain.Exceptions;

namespace TP_integrador_2026.Tests;

public class ConsultarPrestamosSocioTests
{
    // Caso normal: encuentra al socio por su DNI.
    [Fact]
    public void BuscarSocio_ConDniExistente_DevuelveElSocio()
    {
        var socio = CrearSocio();
        var repositorio = new FakeBusquedaSocioRepository(
            new[] { socio });

        var consultar = new ConsultarPrestamosSocio(repositorio);

        var resultado = consultar.BuscarSocio(socio.DNI);

        Assert.Same(socio, resultado);
        Assert.Equal(socio.DNI, resultado.DNI);
    }

    // Caso de error: no existe un socio con el DNI indicado.
    [Fact]
    public void BuscarSocio_ConDniInexistente_LanzaNotFoundException()
    {
        var repositorio = new FakeBusquedaSocioRepository();
        var consultar = new ConsultarPrestamosSocio(repositorio);

        var excepcion = Assert.Throws<NotFoundException>(
            () => consultar.BuscarSocio(30123456));

        Assert.Contains("No existe un socio", excepcion.Message);
    }

    // Caso normal: devuelve los préstamos recibidos del repositorio.
    [Fact]
    public void Ejecutar_ConPrestamosRegistrados_DevuelveLaLista()
    {
        var socio = CrearSocio();

        var primerPrestamo = new Prestamo(
            CrearLibro("Harry Potter y la piedra filosofal", "J. K. Rowling"),
            socio);

        var segundoPrestamo = new Prestamo(
            CrearLibro("La Torre Oscura I: El pistolero", "Stephen King"),
            socio);

        var prestamos = new[] { primerPrestamo, segundoPrestamo };

        var repositorio = new FakeBusquedaSocioRepository(
            new[] { socio },
            prestamos);

        var consultar = new ConsultarPrestamosSocio(repositorio);

        var resultado = consultar.Ejecutar(socio);

        Assert.Equal(2, resultado.Count);
        Assert.Contains(primerPrestamo, resultado);
        Assert.Contains(segundoPrestamo, resultado);
        Assert.Equal(socio.Id, repositorio.UltimoSocioIdConsultado);
    }

    // Caso de borde: el socio todavía no tiene préstamos.
    [Fact]
    public void Ejecutar_SinPrestamosRegistrados_DevuelveListaVacia()
    {
        var socio = CrearSocio();

        var repositorio = new FakeBusquedaSocioRepository(
            new[] { socio });

        var consultar = new ConsultarPrestamosSocio(repositorio);

        var resultado = consultar.Ejecutar(socio);

        Assert.NotNull(resultado);
        Assert.Empty(resultado);
        Assert.Equal(socio.Id, repositorio.UltimoSocioIdConsultado);
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

    private static Libro CrearLibro(string titulo, string autor)
    {
        return new Libro(
            titulo,
            autor,
            "Editorial de prueba",
            1);
    }

    private sealed class FakeBusquedaSocioRepository
        : IBusquedaSocioRepository
    {
        private readonly Dictionary<int, Socio> _socios;
        private readonly List<Prestamo> _prestamos;

        public int? UltimoSocioIdConsultado { get; private set; }

        public FakeBusquedaSocioRepository(
            IEnumerable<Socio>? socios = null,
            IEnumerable<Prestamo>? prestamos = null)
        {
            _socios = (socios ?? Enumerable.Empty<Socio>())
                .ToDictionary(socio => socio.DNI);

            _prestamos = (prestamos ?? Enumerable.Empty<Prestamo>())
                .ToList();
        }

        public Socio? BuscarPorDni(int dni)
        {
            return _socios.TryGetValue(dni, out var socio)
                ? socio
                : null;
        }

        public List<Prestamo> ObtenerPrestamosPorSocio(int socioId)
        {
            UltimoSocioIdConsultado = socioId;
            return _prestamos.ToList();
        }
    }
}
