using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Tests;

public class LibroTests
{
    // Caso normal: se registra un libro con datos válidos.
    [Fact]
    public void Constructor_ConHarryPotter_CreaLibroCorrectamente()
    {
        var libro = new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            3);

        Assert.Equal(
            "Harry Potter y la piedra filosofal",
            libro.Titulo);

        Assert.Equal("J. K. Rowling", libro.Autor);
        Assert.Equal("Salamandra", libro.Editorial);
        Assert.Equal(3, libro.Stock);
        Assert.False(string.IsNullOrWhiteSpace(libro.Codigo));
    }

    // Caso borde: se permite registrar un libro sin ejemplares disponibles.
    [Fact]
    public void Constructor_ConLaTorreOscuraYStockCero_AceptaElLibro()
    {
        var libro = new Libro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            0);

        Assert.Equal(
            "La Torre Oscura I: El pistolero",
            libro.Titulo);

        Assert.Equal("Stephen King", libro.Autor);
        Assert.Equal(0, libro.Stock);
    }

    // Caso de error: no se permite registrar stock negativo.
    [Fact]
    public void Constructor_ConHarryPotterYStockNegativo_LanzaExcepcion()
    {
        var excepcion = Assert.Throws<ArgumentException>(() =>
            new Libro(
                "Harry Potter y la piedra filosofal",
                "J. K. Rowling",
                "Salamandra",
                -1));

        Assert.Contains(
            "stock",
            excepcion.Message.ToLowerInvariant());
    }

        [Fact]
    public void AumentarStock_ConCantidadPositiva_IncrementaStock()
    {
        var libro = new Libro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            1);

        libro.AumentarStock(2);

        Assert.Equal(3, libro.Stock);
    }

    [Fact]
    public void AumentarStock_ConCantidadCero_LanzaExcepcion()
    {
        var libro = new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            2);

        Assert.Throws<ArgumentException>(
            () => libro.AumentarStock(0));
    }

    [Fact]
    public void DisminuirStock_ConStockDisponible_ReduceUnaUnidad()
    {
        var libro = new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            2);

        libro.DisminuirStock();

        Assert.Equal(1, libro.Stock);
    }

    [Fact]
    public void DisminuirStock_SinStock_LanzaExcepcion()
    {
        var libro = new Libro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            0);

        Assert.Throws<InvalidOperationException>(
            () => libro.DisminuirStock());
    }

    [Fact]
    public void ActualizarStock_ConCero_AceptaElNuevoValor()
    {
        var libro = new Libro(
            "Harry Potter y la piedra filosofal",
            "J. K. Rowling",
            "Salamandra",
            3);

        libro.ActualizarStock(0);

        Assert.Equal(0, libro.Stock);
    }

    [Fact]
    public void ActualizarStock_ConValorNegativo_LanzaExcepcion()
    {
        var libro = new Libro(
            "La Torre Oscura I: El pistolero",
            "Stephen King",
            "Plaza & Janés",
            2);

        Assert.Throws<ArgumentException>(
            () => libro.ActualizarStock(-1));
    }
}