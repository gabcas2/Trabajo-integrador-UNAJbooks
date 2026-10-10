using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Application.UseCases.Libros;

namespace TP_integrador_2026.Application.Presentacion
{
    static void EliminarLibroMenu(EliminarLibro eliminarLibro)
        {
            try
            {
                Console.WriteLine("---------- ELIMINAR LIBRO ----------");
                Console.WriteLine();

                Console.Write("Código del libro: ");
                string codigo = Console.ReadLine() ?? "";

                eliminarLibro.Ejecutar(codigo);

                Console.WriteLine();
                Console.WriteLine("Libro eliminado correctamente.");
            }catch(Exception ex)
            {
                Console.WriteLine($"[Error al eliminar libro]: {ex.Message}");
            }    
        }
}