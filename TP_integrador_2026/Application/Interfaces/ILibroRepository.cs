using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.Interfaces
{
    public interface ILibroRepository
    {
        Libro Agregar(Libro libro);

        Libro? BuscarPorCodigo(string codigo);

        List<Libro> ObtenerTodos();

        void Eliminar(Libro libro);

        bool ExisteCodigo(string codigo);

        bool TienePrestamosActivos(int libroId);
    }
}