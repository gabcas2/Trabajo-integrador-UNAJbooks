using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.UseCases.Libros
{
    public class BuscarLibro
    {
        private readonly ILibroRepository _libroRepository;

        public BuscarLibro(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public Libro Ejecutar(string codigo)
        {
            var libro = _libroRepository.BuscarPorCodigo(codigo);

            if (libro == null)
            {
                throw new InvalidOperationException(
                    "No se encontró un libro con el código indicado.");
            }

            return libro;
        }
    }
}