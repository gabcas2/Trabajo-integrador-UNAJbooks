namespace TP_integrador_2026.Domain.Entities
{
    public class Prestamo
    {
        public int Id { get; private set; }

        public int LibroId { get; private set; }

        public int? SocioId { get; private set; }

        public DateTime FechaPrestamo { get; private set; }

        public DateTime FechaVencimiento { get; private set; }

        public DateTime? FechaDevolucion { get; private set; }

        public Libro Libro { get; private set; }

        public Socio? Socio { get; private set; }

        public Prestamo(
            Libro libro,
            Socio socio)
        {
            Libro = libro ?? throw new ArgumentNullException(nameof(libro));
            Socio = socio ?? throw new ArgumentNullException(nameof(socio));

            LibroId = libro.Id;
            SocioId = socio.Id;

            FechaPrestamo = DateTime.UtcNow;

            // El plazo se determina mediante polimorfismo
            FechaVencimiento =
                FechaPrestamo.AddDays(socio.DiasPrestamo);

            FechaDevolucion = null;
        }

        private Prestamo()
        {
            Libro = null!;
            Socio = null;
        }

        public bool EstaActivo =>
            !FechaDevolucion.HasValue;

        public void RegistrarDevolucion()
        {
            if (FechaDevolucion.HasValue)
                throw new InvalidOperationException(
                    "El préstamo ya fue devuelto.");

            FechaDevolucion = DateTime.UtcNow;
        }
    }
}