using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class SocioPremiumTests
{
    // Caso normal: el socio premium tiene sus beneficios específicos.
    [Fact]
    public void Constructor_ConDatosValidos_UsaBeneficiosPremium()
    {
        Socio socio = new SocioPremium(
            "Carlos",
            "Pérez",
            45678130,
            1123456789,
            "Bernal Oeste");

        Assert.Equal("Carlos", socio.Nombre);
        Assert.Equal("Pérez", socio.Apellido);
        Assert.Equal(45678130, socio.DNI);
        Assert.Equal(20, socio.MaxPrestamosActivos);
        Assert.Equal(30, socio.DiasPrestamo);
    }

    // Caso de borde: se aceptan los valores positivos mínimos
    // exigidos por el constructor de Socio.
    [Fact]
    public void Constructor_ConDniYTelefonoMinimos_AceptaElSocioPremium()
    {
        var socio = new SocioPremium(
            "Ana",
            "Gómez",
            1,
            1,
            "");

        Assert.Equal(1, socio.DNI);
        Assert.Equal(1, socio.NumTelefono);
        Assert.Equal(20, socio.MaxPrestamosActivos);
        Assert.Equal(30, socio.DiasPrestamo);
    }

    // Caso de error: SocioPremium hereda la validación del DNI.
    [Fact]
    public void Constructor_ConDniCero_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() =>
            new SocioPremium(
                "Carlos",
                "Pérez",
                0,
                1123456789,
                "Bernal Oeste"));
    }
}