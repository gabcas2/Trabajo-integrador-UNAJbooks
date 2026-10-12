using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.Interfaces
{
    public interface IPrestamoRepository
    {
        Prestamo Agregar(Prestamo prestamo);

        Prestamo? BuscarPorId(int id);

        List<Prestamo> ObtenerTodos();

        List<Prestamo> ObtenerPorSocio(int socioId);

        List<Prestamo> ObtenerActivos();

        bool TienePrestamosActivos(int socioId);

        bool TienePrestamoActivo(int socioId, int libroId);

        int ContarPrestamosActivos(int socioId);

        void Actualizar(Prestamo prestamo);
    }
}