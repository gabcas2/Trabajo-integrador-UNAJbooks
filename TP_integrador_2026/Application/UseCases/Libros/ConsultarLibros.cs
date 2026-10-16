using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.UseCases.Libros
{
    public class ConsultarLibros
    {
        private readonly ILibroRepository _libroRepository;

        public ConsultarLibros(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public List<Libro> Ejecutar()
        {
            return _libroRepository.ObtenerTodos();
        }

        // Recorrido 1: filtrar los libros que cumplen la condición.
        // Condición: tener al menos un ejemplar disponible.
        public List<Libro> FiltrarLibrosConStock()
        {
            var libros = _libroRepository.ObtenerTodos();

            return libros
                .Where(libro => libro.Stock > 0)
                .ToList();
        }

        // Recorrido 2: filtrar los libros que NO cumplen la condición
        // de tener stock disponible.
        public List<Libro> FiltrarLibrosSinStock()
        {
            var libros = _libroRepository.ObtenerTodos();

            return libros
                .Where(libro => !(libro.Stock > 0))
                .ToList();
        }

        // Recorrido 3: transformar cada libro en su título.
        public List<string> ObtenerTitulosLibros()
        {
            var libros = _libroRepository.ObtenerTodos();

            return libros
                .Select(libro => libro.Titulo)
                .ToList();
        }

        // Recorrido 4: encontrar el primer libro con stock disponible.
        // Devuelve null si no existe ninguno.
        public Libro? BuscarPrimerLibroConStock()
        {
            var libros = _libroRepository.ObtenerTodos();

            return libros
                .FirstOrDefault(libro => libro.Stock > 0);
        }
    }
}