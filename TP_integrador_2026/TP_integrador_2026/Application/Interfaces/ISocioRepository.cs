using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.Interfaces
{
    public interface ISocioRepository
    {
        Socio Agregar(Socio socio);
        Socio? BuscarPorDNI(int dni);
        List<Socio> ObtenerTodos();
        void Eliminar(Socio socio);
        bool ExisteDNI(int dni);

        void Actualizar(Socio socio, bool esPremium);
    }
}