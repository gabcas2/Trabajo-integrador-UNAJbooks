using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.Interfaces
{
    public interface IRegistrarPrestamo
    {
        Prestamo Ejecutar(int dniSocio, string codigoLibro);
    }
}