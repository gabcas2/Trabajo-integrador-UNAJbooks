using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.UseCases.Prestamos
{
    public class ConsultarTodosLosPrestamos
    {
        private readonly IPrestamoRepository _prestamoRepository;

        public ConsultarTodosLosPrestamos(IPrestamoRepository prestamoRepository)
        {
            _prestamoRepository = prestamoRepository;
        }

        public List<Prestamo> Ejecutar()
        {
            return _prestamoRepository.ObtenerTodos();
        }
    }
}