using TP_integrador_2026.Application.Interfaces;

namespace TP_integrador_2026.Application.UseCases.Libros
{
    public class ConsultarStockLibro
    {
        private readonly ILibroRepository _libroRepository;

        public ConsultarStockLibro(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public int Ejecutar(string codigo)
        {
            var libro = _libroRepository.BuscarPorCodigo(codigo);
            if (libro == null)
            {
                throw new Exception($"No se encontró ningún libro con el código '{codigo}'.");
            }

            return libro.Stock;
        }
    }
}