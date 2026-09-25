using Microsoft.EntityFrameworkCore;
using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Infrastructure.Persistence;

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
            string discriminator = esPremium
                ? "SocioPremium"
                : "Socio";

            _context.Database.ExecuteSqlInterpolated($@"
                UPDATE ""Socios""
                SET
                    ""NumTelefono"" = {socio.NumTelefono},
                    ""Direccion"" = {socio.Direccion},
                    ""Discriminator"" = {discriminator}
                WHERE ""Id"" = {socio.Id}");

            _context.Entry(socio).State = EntityState.Detached;
        }
    }
}