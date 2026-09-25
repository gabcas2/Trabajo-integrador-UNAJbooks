using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.Interfaces
{
    public interface IBusquedaSocioRepository
    {
        Socio? BuscarPorDni(int dni);

        List<Prestamo> ObtenerPrestamosPorSocio(int socioId);
    }
}
