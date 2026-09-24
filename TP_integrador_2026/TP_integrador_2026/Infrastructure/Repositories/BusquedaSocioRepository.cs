using Microsoft.EntityFrameworkCore;
using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Infrastructure.Persistence;

namespace TP_integrador_2026.Infrastructure.Repositories
{
    public class BusquedaSocioRepository : IBusquedaSocioRepository
    {
        private readonly AppDbContext _context;

        public BusquedaSocioRepository(AppDbContext context)
        {
            _context = context;
        }

        public Socio? BuscarPorDni(int dni)
        {
            return _context.Socios
                .AsNoTracking()
                .FirstOrDefault(s => s.DNI == dni);
        }

        public List<Prestamo> ObtenerPrestamosPorSocio(int socioId)
        {
            return _context.Prestamos
                .AsNoTracking()
                .Include(p => p.Libro)
                .Include(p => p.Socio)
                .Where(p => p.SocioId == socioId)
                .OrderByDescending(p => p.FechaDevolucion == null)
                .ThenByDescending(p => p.FechaPrestamo)
                .ToList();
        }
    }
}
