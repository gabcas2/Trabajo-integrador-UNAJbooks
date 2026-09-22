using Microsoft.EntityFrameworkCore;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Socio> Socios { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // LIBRO
            // =========================

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.HasIndex(l => l.Codigo)
                      .IsUnique();

                entity.Property(l => l.Codigo)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(l => l.Titulo)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(l => l.Autor)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(l => l.Editorial)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(l => l.Stock)
                      .IsRequired();
            });

            // =========================
            // SOCIO
            // =========================

            modelBuilder.Entity<Socio>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.HasIndex(s => s.DNI)
                      .IsUnique();

                entity.Property(s => s.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(s => s.Apellido)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(s => s.DNI)
                      .IsRequired();

                entity.Property(s => s.NumTelefono)
                      .IsRequired();

                entity.Property(s => s.Direccion)
                      .HasMaxLength(200);
            });

            // =========================
            // SOCIO PREMIUM
            // =========================

            modelBuilder.Entity<SocioPremium>()
                .HasBaseType<Socio>();

            // =========================
            // PRESTAMO
            // =========================

            modelBuilder.Entity<Prestamo>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.FechaPrestamo)
                      .IsRequired();

                entity.Property(p => p.FechaDevolucion)
                      .IsRequired(false);

                entity.HasOne(p => p.Libro)
                      .WithMany()
                      .HasForeignKey(p => p.LibroId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Socio)
                      .WithMany()
                      .HasForeignKey(p => p.SocioId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}