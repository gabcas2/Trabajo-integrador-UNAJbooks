namespace TP_integrador_2026.Domain.Entities
{
    public class SocioPremium : Socio
    {
        public override int MaxPrestamosActivos => 20;

        public override int DiasPrestamo => 30;

        public SocioPremium(
            string nombre,
            string apellido,
            int dni,
            int numTelefono,
            string direccion)
            : base(nombre, apellido, dni, numTelefono, direccion)
        {
        }

        private SocioPremium() : base()
        {
        }
    }
}