using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Application.UseCases.Libros;

namespace TP_integrador_2026.Presentacion
{
    static void ConsultarLibrosMenu(ConsultarLibros consultarLibros)
        {
            try
            {
                Console.WriteLine("---------- LIBROS ----------");
                Console.WriteLine();

                var libros = consultarLibros.Ejecutar();

                if (libros.Count == 0)
                {
                    Console.WriteLine("No hay libros registrados.");
                    return;
                }

                foreach (var libro in libros)
                {
                    Console.WriteLine(
                        $"ID: {libro.Id} | " +
                        $"Código: {libro.Codigo} | " +
                        $"Título: {libro.Titulo} | " +
                        $"Autor: {libro.Autor} | " +
                        $"Stock: {libro.Stock}");
                }
            }catch(Exception ex)
            {
                Console.WriteLine($"[Error al consultar libros]: {ex.Message}");
            }    
        }
}