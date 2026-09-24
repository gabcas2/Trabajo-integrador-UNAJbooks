using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Application.UseCases.Libros;

namespace TP_integrador_2026.Presentacion
{
    static void BuscarLibroMenu(BuscarLibro buscarLibro)
        {
            try
                {
                    Console.WriteLine("---------- BUSCAR LIBRO ----------");
                    Console.WriteLine();

                    Console.Write("Código del libro: ");
                    string codigo = Console.ReadLine() ?? "";

                    var libro = buscarLibro.Ejecutar(codigo);

                    Console.WriteLine();
                    Console.WriteLine("Libro encontrado.");
                    Console.WriteLine($"ID: {libro.Id}");
                    Console.WriteLine($"Código: {libro.Codigo}");
                    Console.WriteLine($"Título: {libro.Titulo}");
                    Console.WriteLine($"Autor: {libro.Autor}");
                    Console.WriteLine($"Editorial: {libro.Editorial}");
                    Console.WriteLine($"Stock: {libro.Stock}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"[Error al buscar libro]: {ex.Message}");
                }
        }
}