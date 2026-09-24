using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Application.UseCases.Socios;
using TP_integrador_2026.Application.UseCases.Libros;

namespace TP_integrador_2026.Presentacion.Biblioteca
{
    static void RegistrarSocioMenu(RegistrarSocio registrarSocio)
        {
            try
            {
                Console.WriteLine("---------- REGISTRAR SOCIO ----------");
                Console.WriteLine();

                Console.Write("Nombre: ");
                string nombre = Console.ReadLine() ?? "";

                Console.Write("Apellido: ");
                string apellido = Console.ReadLine() ?? "";

                Console.Write("DNI: ");
                string dniTexto = Console.ReadLine() ?? "";

                if (!int.TryParse(dniTexto, out int dni))
                {
                    throw new FormatException("El DNI debe ser un número entero.");
                }

                Console.Write("Teléfono: ");
                string telefonoTexto = Console.ReadLine() ?? "";

                if (!int.TryParse(telefonoTexto, out int numTelefono))
                {
                    throw new FormatException("El teléfono debe ser un número entero.");
                }

                Console.Write("Dirección: ");
                string direccion = Console.ReadLine() ?? "";

                Console.Write("¿Es socio premium? (s/n): ");
                string premiumTexto = Console.ReadLine() ?? "";

                bool esPremium = premiumTexto.Equals(
                    "s",
                    StringComparison.OrdinalIgnoreCase);

                var socio = registrarSocio.Ejecutar(
                    nombre,
                    apellido,
                    dni,
                    numTelefono,
                    direccion,
                    esPremium);

                Console.WriteLine();
                Console.WriteLine("Socio registrado correctamente.");
                Console.WriteLine($"ID: {socio.Id}");
                Console.WriteLine($"Nombre: {socio.Nombre}");
                Console.WriteLine($"Apellido: {socio.Apellido}");
                Console.WriteLine($"DNI: {socio.DNI}");
                Console.WriteLine($"Teléfono: {socio.NumTelefono}");
                Console.WriteLine($"Dirección: {socio.Direccion}");
                Console.WriteLine($"Tipo: {(esPremium ? "Premium" : "Normal")}");
            }

            catch(FormatException ex)
            {
                Console.WriteLine($"[Error de formato]: {ex.Message}");
            }
            
            catch(Exception ex)
            {
                Console.WriteLine($"[Error al registrar socio]: {ex.Message}");
            }
        }

        static void BuscarSocioMenu(BuscarSocio buscarSocio)
        {
            try
            {
                Console.WriteLine("---------- BUSCAR SOCIO ----------");
                Console.WriteLine();

                Console.Write("DNI del socio: ");
                string dniTexto = Console.ReadLine() ?? "";

                if (!int.TryParse(dniTexto, out int dni))
                {
                    throw new FormatException("El DNI debe ser un número entero.");
                }

                var socio = buscarSocio.Ejecutar(dni);

                Console.WriteLine();
                Console.WriteLine("Socio encontrado.");
                Console.WriteLine($"ID: {socio.Id}");
                Console.WriteLine($"Nombre: {socio.Nombre}");
                Console.WriteLine($"Apellido: {socio.Apellido}");
                Console.WriteLine($"DNI: {socio.DNI}");
                Console.WriteLine($"Teléfono: {socio.NumTelefono}");
                Console.WriteLine($"Dirección: {socio.Direccion}");
                Console.WriteLine($"Tipo: {(socio is SocioPremium ? "Premium" : "Normal")}");
            }
            catch(FormatException ex)
            {
                Console.WriteLine($"[Error de formato]: {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[Error al buscar socio]: {ex.Message}");
            }
        }

    static void BuscarLibroMenu(BuscarLibro buscarLibro)
        {
            try
                {
                    Console.WriteLine("---------- BUSCAR LIBRO ----------");
                    Console.WriteLine();

                    Console.Write("Código del libro: ");
                    string codigo = Console.ReadLine() ?? "";

                    var libro = buscarLibro.Ejecutar(codigo);

                    Console.WriteLine();
                    Console.WriteLine("Libro encontrado.");
                    Console.WriteLine($"ID: {libro.Id}");
                    Console.WriteLine($"Código: {libro.Codigo}");
                    Console.WriteLine($"Título: {libro.Titulo}");
                    Console.WriteLine($"Autor: {libro.Autor}");
                    Console.WriteLine($"Editorial: {libro.Editorial}");
                    Console.WriteLine($"Stock: {libro.Stock}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"[Error al buscar libro]: {ex.Message}");
                }
        }

    static void ConsultarLibrosMenu(ConsultarLibros consultarLibros)
        {
            try
            {
                Console.WriteLine("---------- LIBROS ----------");
                Console.WriteLine();

                var libros = consultarLibros.Ejecutar();

                if (libros.Count == 0)
                {
                    Console.WriteLine("No hay libros registrados.");
                    return;
                }

                foreach (var libro in libros)
                {
                    Console.WriteLine(
                        $"ID: {libro.Id} | " +
                        $"Código: {libro.Codigo} | " +
                        $"Título: {libro.Titulo} | " +
                        $"Autor: {libro.Autor} | " +
                        $"Stock: {libro.Stock}");
                }
            }catch(Exception ex)
            {
                Console.WriteLine($"[Error al consultar libros]: {ex.Message}");
            }    
        }

    static void EliminarLibroMenu(EliminarLibro eliminarLibro)
        {
            try
            {
                Console.WriteLine("---------- ELIMINAR LIBRO ----------");
                Console.WriteLine();

                Console.Write("Código del libro: ");
                string codigo = Console.ReadLine() ?? "";

                eliminarLibro.Ejecutar(codigo);

                Console.WriteLine();
                Console.WriteLine("Libro eliminado correctamente.");
            }catch(Exception ex)
            {
                Console.WriteLine($"[Error al eliminar libro]: {ex.Message}");
            }    
        }

    static void RegistrarLibroMenu(RegistrarLibro registrarLibro)
        {
            try
            {
                Console.WriteLine("---------- REGISTRAR LIBRO ----------");
                Console.WriteLine();

                Console.Write("Título: ");
                string titulo = Console.ReadLine() ?? "";

                Console.Write("Autor: ");
                string autor = Console.ReadLine() ?? "";

                Console.Write("Editorial: ");
                string editorial = Console.ReadLine() ?? "";

                Console.Write("Stock: ");

                string stockTexto = Console.ReadLine() ?? "";

                if (!int.TryParse(stockTexto, out int stock))
                {
                    throw new DatoLibroInvalidoException("El stock debe ser un número entero.");
                }

                var libro = registrarLibro.Ejecutar(
                    titulo,
                    autor,
                    editorial,
                    stock);

                Console.WriteLine();
                Console.WriteLine("Libro registrado correctamente.");
                Console.WriteLine($"ID: {libro.Id}");
                Console.WriteLine($"Código: {libro.Codigo}");
            }
            catch (DatoLibroInvalidoException ex)
            {
                Console.WriteLine($"[Error de validación]: {ex.Message}");
            }
            catch (StockInsuficienteException ex)
            {
                Console.WriteLine($"[Aviso de Stock]: {ex.Message}");
            }
        } 

        public class StockInsuficienteException : Exception
        {
            public StockInsuficienteException(string mensaje) : base(mensaje) { }
        }

        public class DatoLibroInvalidoException : Exception
        {
            public DatoLibroInvalidoException(string mensaje) : base(mensaje) { }
        }                   
}