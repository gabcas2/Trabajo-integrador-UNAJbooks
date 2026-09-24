using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.Interfaces
{
    public interface IConsultarPrestamosSocio
    {
        Socio BuscarSocio(int dni);

        List<Prestamo> Ejecutar(Socio socio);
    }
}
