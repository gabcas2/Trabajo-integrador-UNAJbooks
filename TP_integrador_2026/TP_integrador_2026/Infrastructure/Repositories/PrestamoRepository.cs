using Microsoft.EntityFrameworkCore;
using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Infrastructure.Persistence;

namespace TP_integrador_2026.Infrastructure.Repositories
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly AppDbContext _context;

        public PrestamoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Prestamo Agregar(Prestamo prestamo)
        {
            _context.Prestamos.Add(prestamo);
            _context.SaveChanges();

            return prestamo;
        }

        public Prestamo? BuscarPorId(int id)
        {
            return _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Socio)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Prestamo> ObtenerTodos()
        {
            return _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Socio)
                .OrderByDescending(p => p.FechaPrestamo)
                .ToList();
        }

        public List<Prestamo> ObtenerPorSocio(int socioId)
        {
            return _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Socio)
                .Where(p => p.SocioId == socioId)
                .OrderByDescending(p => p.FechaPrestamo)
                .ToList();
        }

        public List<Prestamo> ObtenerActivos()
        {
            return _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Socio)
                .Where(p => p.FechaDevolucion == null)
                .OrderBy(p => p.FechaPrestamo)
                .ToList();
        }

        public void Actualizar(Prestamo prestamo)
        {
            _context.Prestamos.Update(prestamo);
            _context.SaveChanges();
        }

        public bool TienePrestamosActivos(int socioId)
        {
            return _context.Prestamos.Any(p => p.SocioId == socioId && p.FechaDevolucion == null);
        }
    }
}