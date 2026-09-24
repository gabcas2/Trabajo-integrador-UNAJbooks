using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.UseCases.Libros
{
    public class RegistrarLibro : IRegistrarLibro
    {
        private readonly ILibroRepository _libroRepository;

        public RegistrarLibro(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public Libro Ejecutar(
            string titulo,
            string autor,
            string editorial,
            int stock)
        {
            var libro = new Libro(
                titulo,
                autor,
                editorial,
                stock);

            if (_libroRepository.ExisteCodigo(libro.Codigo))
            {
                throw new InvalidOperationException(
                    "Ya existe un libro con el código generado.");
            }

            return _libroRepository.Agregar(libro);
        }
    }
}