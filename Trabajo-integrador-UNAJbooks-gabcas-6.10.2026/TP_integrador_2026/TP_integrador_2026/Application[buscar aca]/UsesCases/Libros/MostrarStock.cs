using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Libros;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.UseCases.Libros
{
    public class BuscarLibrodos
    {
        private readonly ILibroRepository _libroRepository;

        public BuscarLibrodos(ILibroRepository libroRepository)//fijarse si conservar el nombre de la clase
        {
            _libroRepository = libroRepository;// preguntar si estos metodos deben estar en "biblioteca"
        }

        public void MostrarStock(ConsultarLibros consultarLibros)
        {
            string codi = Console.ReadLine() ?? "";

            var ListaLibros = consultarLibros.Ejecutar();

            Libro? libroEncontrado = null;

            foreach (var l in ListaLibros)
            {
                if (l.Codigo.Equals(codi, StringComparison.OrdinalIgnoreCase))
                {
                    libroEncontrado = l;
                    
                    Console.WriteLine($"Stock: {libroEncontrado.Stock}");
                    break;
                } 
            }
        }
    }
}