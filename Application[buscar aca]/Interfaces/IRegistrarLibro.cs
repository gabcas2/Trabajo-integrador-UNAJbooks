using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.Interfaces
{
    public interface IRegistrarLibro
    {
        Libro Ejecutar(
            string titulo,
            string autor,
            string editorial,
            int stock);
    }
}