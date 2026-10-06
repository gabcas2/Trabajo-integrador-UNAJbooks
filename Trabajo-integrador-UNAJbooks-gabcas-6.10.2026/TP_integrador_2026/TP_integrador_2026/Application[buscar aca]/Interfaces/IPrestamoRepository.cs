using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.Interfaces
{
    public interface IPrestamoRepository
    {
        Prestamo Agregar(Prestamo prestamo);

        Prestamo? BuscarPorId(int id);

        List<Prestamo> ObtenerTodos();
        
        List<Prestamo> ObtenerPorSocioDni(int dni);

        List<Prestamo> ObtenerPorSocio(int socioId);

        List<Prestamo> ObtenerActivos();

        void Actualizar(Prestamo prestamo);
    }
}