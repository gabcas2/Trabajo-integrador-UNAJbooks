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
    }
}