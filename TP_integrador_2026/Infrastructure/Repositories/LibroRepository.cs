using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Infrastructure.Persistence;

namespace TP_integrador_2026.Infrastructure.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        private readonly AppDbContext _context;

        public LibroRepository(AppDbContext context)
        {
            _context = context;
        }

        public Libro Agregar(Libro libro)
        {
            _context.Libros.Add(libro);
            _context.SaveChanges();

            return libro;
        }

        public Libro? BuscarPorCodigo(string codigo)
        {
            return _context.Libros
                .FirstOrDefault(l => l.Codigo == codigo);
        }

        public List<Libro> ObtenerTodos()
        {
            return _context.Libros
                .OrderBy(l => l.Titulo)
                .ToList();
        }

        public void Eliminar(Libro libro)
        {
            var prestamos = _context.Prestamos
            .Where(p => p.LibroId == libro.Id)
            .ToList();

            _context.Prestamos.RemoveRange(prestamos);
            _context.Libros.Remove(libro);

            _context.SaveChanges();
        }
        public bool ExisteCodigo(string codigo)
        {
            return _context.Libros
                .Any(l => l.Codigo == codigo);
        }

       public bool TienePrestamosActivos(int libroId)
        {
            return _context.Prestamos
            .Any(p => p.LibroId == libroId && p.FechaDevolucion == null);
        }

        
    }
}