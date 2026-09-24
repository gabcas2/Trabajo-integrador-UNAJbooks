using TP_integrador_2026.Application.Interfaces;

namespace TP_integrador_2026.Application.UseCases.Libros
{
    public class EliminarLibro
    {
        private readonly ILibroRepository _libroRepository;

        public EliminarLibro(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public void Ejecutar(string codigo)
        {
            var libro = _libroRepository.BuscarPorCodigo(codigo);

            if (libro == null)
            {
                throw new InvalidOperationException(
                    "No se encontró un libro con el código indicado.");
            }

            if (_libroRepository.TienePrestamosActivos(libro.Id))
            {
                throw new InvalidOperationException(
                    "No se puede eliminar el libro porque tiene préstamos asociados.");
            }

            _libroRepository.Eliminar(libro);
        }
    }
}