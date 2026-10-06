using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.UseCases.Prestamos
{
    public class ConsultarLibrosPrestadosASocio
    {
        private readonly IPrestamoRepository _prestamoRepository;

        public ConsultarLibrosPrestadosASocio(IPrestamoRepository prestamoRepository)
        {
            _prestamoRepository = prestamoRepository;
        }

        public List<Libro> Ejecutar(int dniSocio)
        {
            var prestamos = _prestamoRepository.ObtenerPorSocioDni(dniSocio);

            var librosPrestados = prestamos
                .Where(p => p.FechaDevolucion == null && p.Libro != null)
                .Select(p => p.Libro!)
                .ToList();

            return librosPrestados;
        }
    }
}