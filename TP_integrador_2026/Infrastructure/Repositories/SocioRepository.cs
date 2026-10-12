using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TP_integrador_2026.Infrastructure.Repositories
{
    public class SocioRepository : ISocioRepository
    {
        private readonly AppDbContext _context;

        public SocioRepository(AppDbContext context)
        {
            _context = context;
        }

        public Socio Agregar(Socio socio)
        {
            _context.Socios.Add(socio);
            _context.SaveChanges();

            return socio;
        }

        public Socio? BuscarPorDNI(int dni)
        {
            return _context.Socios
                .FirstOrDefault(s => s.DNI == dni);
        }

        public List<Socio> ObtenerTodos()
        {
            return _context.Socios
                .OrderBy(s => s.Apellido)
                .ThenBy(s => s.Nombre)
                .ToList();
        }

       public void Eliminar(Socio socio)
{
    var prestamos = _context.Prestamos
        .Where(p => p.SocioId == socio.Id)
        .ToList();

    _context.Prestamos.RemoveRange(prestamos);
    _context.Socios.Remove(socio);

    _context.SaveChanges();
}

        public bool ExisteDNI(int dni)
        {
            return _context.Socios
                .Any(s => s.DNI == dni);
        }
        public void Actualizar(Socio socio, bool esPremium)
        {
            using var transaction = _context.Database.BeginTransaction();

            try{
                // Guardar los cambios de teléfono y dirección.
                 _context.SaveChanges();

                // Determinar el discriminador correspondiente al tipo elegido.
                string discriminador = esPremium? nameof(SocioPremium): nameof(Socio);

                // Actualizar el tipo de socio en PostgreSQL.
                int filasActualizadas = _context.Database.ExecuteSqlInterpolated(
                    $"UPDATE \"Socios\" SET \"Discriminator\" = {discriminador} WHERE \"Id\" = {socio.Id}");

                if (filasActualizadas != 1)
                {
                    throw new InvalidOperationException(
                     "No se pudo actualizar el tipo de socio.");
                }

                transaction.Commit();
            }
            finally
            {
                // Evitar que EF Core reutilice entidades con tipos desactualizados.
                _context.ChangeTracker.Clear();
             }
        }
    }
}