using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class SocioTests
{
    // Caso normal: un socio se crea con datos válidos.
    [Fact]
    public void Constructor_ConDatosValidos_CreaSocioCorrectamente()
    {
        var socio = new Socio(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Avenida Mitre 123, Bernal");

        Assert.Equal("María", socio.Nombre);
        Assert.Equal("Pérez", socio.Apellido);
        Assert.Equal(30123456, socio.DNI);
        Assert.Equal(1123456789, socio.NumTelefono);
        Assert.Equal("Avenida Mitre 123, Bernal", socio.Direccion);
        Assert.Equal(4, socio.MaxPrestamosActivos);
        Assert.Equal(15, socio.DiasPrestamo);
    }

    // Caso de borde: DNI y teléfono con el mínimo valor positivo.
    [Fact]
    public void Constructor_ConValoresPositivosMinimos_AceptaElSocio()
    {
        var socio = new Socio(
            "Ana",
            "Gómez",
            1,
            1,
            "");

        Assert.Equal(1, socio.DNI);
        Assert.Equal(1, socio.NumTelefono);
        Assert.Equal("", socio.Direccion);
    }

    // Caso de error: un DNI igual a cero no es válido.
    [Fact]
    public void Constructor_ConDniCero_LanzaExcepcion()
    {
        var excepcion = Assert.Throws<ArgumentException>(() =>
            new Socio(
                "María",
                "Pérez",
                0,
                1123456789,
                "Bernal"));

        Assert.Contains("DNI", excepcion.Message);
    }

    // Caso normal: se actualiza el teléfono.
    [Fact]
    public void ActualizarTelefono_ConNumeroValido_ActualizaElTelefono()
    {
        var socio = new Socio(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Bernal");

        socio.ActualizarTelefono(1145678901);

        Assert.Equal(1145678901, socio.NumTelefono);
    }

    // Caso de error: no se permite un teléfono igual a cero.
    [Fact]
    public void ActualizarTelefono_ConCero_LanzaExcepcion()
    {
        var socio = new Socio(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Bernal");

        Assert.Throws<ArgumentException>(
            () => socio.ActualizarTelefono(0));
    }

    // Caso de borde: una dirección nula se convierte en texto vacío.
    [Fact]
    public void ActualizarDireccion_ConValorNulo_DejaDireccionVacia()
    {
        var socio = new Socio(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Bernal");

        socio.ActualizarDireccion(null!);

        Assert.Equal("", socio.Direccion);
    }
}