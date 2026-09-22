namespace TP_integrador_2026.Domain.Entities
{
    public class Socio
    {
        public int Id { get; private set; }

        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public int DNI { get; private set; }
        public int NumTelefono { get; private set; }
        public string Direccion { get; private set; }

        public Socio(
            string nombre,
            string apellido,
            int dni,
            int numTelefono,
            string direccion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido es obligatorio.");

            if (dni <= 0)
                throw new ArgumentException("El DNI debe ser válido.");

            if (numTelefono <= 0)
                throw new ArgumentException("El teléfono debe ser válido.");

            Nombre = nombre;
            Apellido = apellido;
            DNI = dni;
            NumTelefono = numTelefono;
            Direccion = direccion;
        }

        protected Socio()
        {
            Nombre = string.Empty;
            Apellido = string.Empty;
            Direccion = string.Empty;
        }


        public void ActualizarTelefono(int nuevoTelefono)
        {
            if (nuevoTelefono <= 0)
             throw new ArgumentException("El teléfono debe ser válido.");

            NumTelefono = nuevoTelefono;
        }

        public void ActualizarDireccion(string nuevaDireccion)
        {
            if (string.IsNullOrWhiteSpace(nuevaDireccion))
            throw new ArgumentException("La dirección es obligatoria.");

            Direccion = nuevaDireccion;
        }   
    }
}