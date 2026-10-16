using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class PrestamoTests
{
    // Caso normal: se crea un préstamo para un socio normal.
    [Fact]
    public void Constructor_ConDatosValidos_CreaPrestamoActivo()
    {
        var libro = new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            2);

        var socio = new Socio(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Bernal");

        var prestamo = new Prestamo(libro, socio);

        Assert.Same(libro, prestamo.Libro);
        Assert.Same(socio, prestamo.Socio);
        Assert.True(prestamo.EstaActivo());
        Assert.Null(prestamo.FechaDevolucion);
        Assert.Equal(
            TimeSpan.FromDays(15),
            prestamo.FechaVencimiento - prestamo.FechaPrestamo);
    }

    // Caso de borde: un socio premium tiene 30 días de préstamo.
    [Fact]
    public void Constructor_ConSocioPremium_UsaPlazoDeTreintaDias()
    {
        var libro = new Libro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            1);

        var socio = new SocioPremium(
            "Carlos",
            "Gómez",
            45678130,
            1123456789,
            "Bernal Oeste");

        var prestamo = new Prestamo(libro, socio);

        Assert.Equal(
            TimeSpan.FromDays(30),
            prestamo.FechaVencimiento - prestamo.FechaPrestamo);

        Assert.True(prestamo.EstaActivo());
    }

    // Caso de error: no se puede crear un préstamo sin un libro.
    [Fact]
    public void Constructor_SinLibro_LanzaExcepcion()
    {
        var socio = new Socio(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Bernal");

        Assert.Throws<ArgumentNullException>(
            () => new Prestamo(null!, socio));
    }

    // Caso normal: registrar la devolución desactiva el préstamo.
    [Fact]
    public void RegistrarDevolucion_EnPrestamoActivo_LoDesactiva()
    {
        var libro = new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            1);

        var socio = new Socio(
            "María",
            "Pérez",
            30123456,
            1123456789,
            "Bernal");

        var prestamo = new Prestamo(libro, socio);

        prestamo.RegistrarDevolucion();

        Assert.False(prestamo.EstaActivo());
        Assert.NotNull(prestamo.FechaDevolucion);
    }

    // Caso de error: no se puede devolver dos veces el mismo préstamo.
    [Fact]
    public void RegistrarDevolucion_DosVeces_LanzaExcepcion()
    {
        var libro = new Libro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            1);

        var socio = new Socio(
            "Carlos",
            "Gómez",
            45678130,
            1123456789,
            "Bernal Oeste");

        var prestamo = new Prestamo(libro, socio);

        prestamo.RegistrarDevolucion();

        Assert.Throws<InvalidOperationException>(
            () => prestamo.RegistrarDevolucion());
    }
}