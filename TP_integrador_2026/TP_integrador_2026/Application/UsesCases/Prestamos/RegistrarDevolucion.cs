using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.UseCases.Prestamos
{
    public class RegistrarDevolucion
    {
        private readonly IPrestamoRepository _prestamoRepository;

        public RegistrarDevolucion(IPrestamoRepository prestamoRepository)
        {
            _prestamoRepository = prestamoRepository;
        }

        public Prestamo Ejecutar(int prestamoId)
        {
            var prestamo = _prestamoRepository.BuscarPorId(prestamoId);

            if (prestamo == null)
                throw new InvalidOperationException(
                    "No existe un préstamo con el ID indicado.");

            if (!prestamo.EstaActivo)
                throw new InvalidOperationException(
                    "El préstamo ya fue devuelto.");

            prestamo.RegistrarDevolucion();

            prestamo.Libro.AumentarStock(1);

            _prestamoRepository.Actualizar(prestamo);

            return prestamo;
        }
    }
}
