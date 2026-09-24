using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Application.UseCases.Libros;

namespace TP_integrador_2026.Presentacion
{
    static void RegistrarLibroMenu(RegistrarLibro registrarLibro)
        {
            try
            {
                Console.WriteLine("---------- REGISTRAR LIBRO ----------");
                Console.WriteLine();

                Console.Write("Título: ");
                string titulo = Console.ReadLine() ?? "";

                Console.Write("Autor: ");
                string autor = Console.ReadLine() ?? "";

                Console.Write("Editorial: ");
                string editorial = Console.ReadLine() ?? "";

                Console.Write("Stock: ");

                string stockTexto = Console.ReadLine() ?? "";

                if (!int.TryParse(stockTexto, out int stock))
                {
                    throw new DatoLibroInvalidoException("El stock debe ser un número entero.");
                }

                var libro = registrarLibro.Ejecutar(
                    titulo,
                    autor,
                    editorial,
                    stock);

                Console.WriteLine();
                Console.WriteLine("Libro registrado correctamente.");
                Console.WriteLine($"ID: {libro.Id}");
                Console.WriteLine($"Código: {libro.Codigo}");
            }
            catch (DatoLibroInvalidoException ex)
            {
                Console.WriteLine($"[Error de validación]: {ex.Message}");
            }
            catch (StockInsuficienteException ex)
            {
                Console.WriteLine($"[Aviso de Stock]: {ex.Message}");
            }
        }
}