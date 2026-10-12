namespace TP_integrador_2026.Domain.Entities
{
    public class Libro
    {
        public int Id { get; private set; }

        public string Codigo { get; private set; }

        public string Titulo { get; private set; }

        public string Autor { get; private set; }

        public string Editorial { get; private set; }

        public int Stock { get; private set; }

        public Libro(
            string titulo,
            string autor,
            string editorial,
            int stock)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El título no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(autor))
                throw new ArgumentException("El autor no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(editorial))
                throw new ArgumentException("La editorial no puede estar vacía.");

            if (stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");

            Titulo = titulo;
            Autor = autor;
            Editorial = editorial;
            Stock = stock;

            Codigo = GenerarCodigo();
        }

        // Constructor utilizado por Entity Framework Core
        private Libro()
        {
            Codigo = string.Empty;
            Titulo = string.Empty;
            Autor = string.Empty;
            Editorial = string.Empty;
        }

        private string GenerarCodigo()
        {
            Random random = new Random();

            return char.ToUpper(Titulo[0]).ToString()
                   + char.ToUpper(Autor[0]).ToString()
                   + random.Next(1, 1000)
                   + char.ToUpper(Editorial[0]);
        }

        public void AumentarStock(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad debe ser mayor que cero.");

            Stock += cantidad;
        }

        public void DisminuirStock()
        {
            if (Stock <= 0)
                throw new InvalidOperationException(
                    "No hay ejemplares disponibles para prestar.");

            Stock--;
        }

        public void ActualizarStock(int nuevoStock)
        {
            if (nuevoStock < 0)
                throw new ArgumentException(
                    "El stock no puede ser negativo.");

            Stock = nuevoStock;
        }
    }
}