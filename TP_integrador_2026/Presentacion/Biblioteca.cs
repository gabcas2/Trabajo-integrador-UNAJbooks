using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Application.UseCases.Socios;
using TP_integrador_2026.Application.UseCases.Libros;
using TP_integrador_2026.Application.UseCases.Prestamos;

namespace TP_integrador_2026.Presentacion.Biblioteca
{
    public static class Biblioteca
    {
        public static void RegistrarSocioMenu(RegistrarSocio registrarSocio)
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

        public static void BuscarSocioMenu(BuscarSocio buscarSocio)
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

        public static void ModificarSocioMenu(ModificarSocio modificarSocio)
        {
            try
            {
                Console.WriteLine("---------- MODIFICAR SOCIO ----------");
                Console.WriteLine();

                Console.Write("DNI del socio: ");
                string dniTexto = Console.ReadLine() ?? "";

                if (!int.TryParse(dniTexto, out int dni))
                    {
                        throw new FormatException("El DNI debe ser un número entero.");
                    }

                Console.Write("Nuevo teléfono (ENTER para mantener el actual): ");
                string telefonoTexto = Console.ReadLine() ?? "";

                int? nuevoTelefono = null;

                if (!string.IsNullOrWhiteSpace(telefonoTexto))
                {
                    if (!int.TryParse(telefonoTexto, out int telefono))
                    {
                        throw new FormatException("El teléfono debe ser un número entero.");
                    }

                nuevoTelefono = telefono;
                }

            Console.Write("Nueva dirección (ENTER para mantener la actual): ");
            string direccionTexto = Console.ReadLine() ?? "";

            string? nuevaDireccion = null;

            if (!string.IsNullOrWhiteSpace(direccionTexto))
            {
                nuevaDireccion = direccionTexto;
            }

            Console.Write("¿Cambiar tipo de socio? (s/n/ENTER para mantener): ");
            string tipoTexto = Console.ReadLine() ?? "";

            bool? nuevoEsPremium = null;

            if (!string.IsNullOrWhiteSpace(tipoTexto))
            {
                if (tipoTexto.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    nuevoEsPremium = true;
                }
                else if (tipoTexto.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    nuevoEsPremium = false;
                }
                else
                {
                    throw new FormatException(
                    "Debe ingresar 's', 'n' o presionar ENTER.");
                }
            }   

            modificarSocio.Ejecutar(
                dni,
                nuevoTelefono,
                nuevaDireccion,
                nuevoEsPremium);

            Console.WriteLine();
            Console.WriteLine("Socio modificado correctamente.");
            }
        catch (FormatException ex)
        {
            Console.WriteLine($"[Error de formato]: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error al modificar socio]: {ex.Message}");
        }
    } 

        public static void BuscarLibroMenu(BuscarLibro buscarLibro)
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

        public static void ConsultarLibrosMenu(ConsultarLibros consultarLibros)
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

        public static void EliminarLibroMenu(EliminarLibro eliminarLibro)
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

        public static void RegistrarLibroMenu(RegistrarLibro registrarLibro)
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
        public static void ConsultarSociosMenu(ConsultarSocios consultarSocios)
        {
            try
            {
                Console.WriteLine("---------- SOCIOS ----------");
                Console.WriteLine();

                var socios = consultarSocios.Ejecutar();

                if (socios.Count == 0)
                {
                    Console.WriteLine("No hay socios registrados.");
                    return;
                }

                foreach (var socio in socios)
                {
                    string tipo = socio is SocioPremium ? "Premium" : "Normal";

                    Console.WriteLine(
                    $"ID: {socio.Id} | " +
                    $"Nombre: {socio.Nombre} | " +
                    $"Apellido: {socio.Apellido} | " +
                    $"DNI: {socio.DNI} | " +
                    $"Teléfono: {socio.NumTelefono} | " +
                    $"Dirección: {socio.Direccion} | " +
                    $"Tipo: {tipo}");
                }
            }
            catch (Exception ex)
            {
            Console.WriteLine($"[Error al consultar socios]: {ex.Message}");
            }
        }

        public static void EliminarSocioMenu(EliminarSocio eliminarSocio)
        {
            try
            {
                Console.WriteLine("---------- ELIMINAR SOCIO ----------");
                Console.WriteLine();

                Console.Write("DNI del socio: ");
                string dniTexto = Console.ReadLine() ?? "";

                if (!int.TryParse(dniTexto, out int dni))
                    {
                        throw new FormatException("El DNI debe ser un número entero.");
                    }

                eliminarSocio.Ejecutar(dni);

                Console.WriteLine();
                Console.WriteLine("Socio eliminado correctamente.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"[Error de formato]: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error al eliminar socio]: {ex.Message}");
            }
        }
        public static void RegistrarPrestamoMenu(RegistrarPrestamo registrarPrestamo)
{
    try
    {
        Console.WriteLine("---------- REGISTRAR PRÉSTAMO ----------");
        Console.WriteLine();

        Console.Write("DNI del socio: ");
        string dniTexto = Console.ReadLine() ?? "";

        if (!int.TryParse(dniTexto, out int dni))
        {
            throw new FormatException(
                "El DNI debe ser un número entero.");
        }

        Console.Write("Código del libro: ");
        string codigoLibro = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(codigoLibro))
        {
            throw new FormatException(
                "El código del libro es obligatorio.");
        }

        var prestamo = registrarPrestamo.Ejecutar(
            dni,
            codigoLibro);

        Console.WriteLine();
        Console.WriteLine("Préstamo registrado correctamente.");
        Console.WriteLine($"ID del préstamo: {prestamo.Id}");
        Console.WriteLine($"Libro: {prestamo.Libro.Titulo}");
        Console.WriteLine($"Socio: {prestamo.Socio.Nombre} {prestamo.Socio.Apellido}");
        Console.WriteLine($"Fecha de préstamo: {prestamo.FechaPrestamo}");
        Console.WriteLine($"Fecha de vencimiento: {prestamo.FechaVencimiento}");
    }
    catch (FormatException ex)
    {
        Console.WriteLine($"[Error de formato]: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Error al registrar préstamo]: {ex.Message}");
    }
}

public static void RegistrarDevolucionMenu(RegistrarDevolucion registrarDevolucion)
{
    try
    {
        Console.WriteLine("---------- REGISTRAR DEVOLUCIÓN ----------");
        Console.WriteLine();

        Console.Write("ID del préstamo: ");
        string prestamoTexto = Console.ReadLine() ?? "";

        if (!int.TryParse(prestamoTexto, out int prestamoId))
        {
            throw new FormatException(
                "El ID del préstamo debe ser un número entero.");
        }

        var prestamo = registrarDevolucion.Ejecutar(prestamoId);

        Console.WriteLine();
        Console.WriteLine("Devolución registrada correctamente.");
        Console.WriteLine($"ID del préstamo: {prestamo.Id}");
        Console.WriteLine($"Libro: {prestamo.Libro.Titulo}");
        Console.WriteLine($"Socio: {prestamo.Socio.Nombre} {prestamo.Socio.Apellido}");
        Console.WriteLine($"Fecha de devolución: {prestamo.FechaDevolucion}");
    }
    catch (FormatException ex)
    {
        Console.WriteLine($"[Error de formato]: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Error al registrar devolución]: {ex.Message}");
    }
}

public static void VerPrestamosActivosMenu(
    ConsultarPrestamosActivos consultarPrestamosActivos)
{
    try
    {
        Console.WriteLine("---------- PRÉSTAMOS ACTIVOS ----------");
        Console.WriteLine();

        var prestamos = consultarPrestamosActivos.Ejecutar();

        if (prestamos.Count == 0)
        {
            Console.WriteLine("No hay préstamos activos.");
            return;
        }

        foreach (var prestamo in prestamos)
        {
            Console.WriteLine(
                $"ID: {prestamo.Id} | " +
                $"Libro: {prestamo.Libro.Titulo} | " +
                $"Socio: {prestamo.Socio.Nombre} {prestamo.Socio.Apellido} | " +
                $"DNI: {prestamo.Socio.DNI} | " +
                $"Préstamo: {prestamo.FechaPrestamo} | " +
                $"Vencimiento: {prestamo.FechaVencimiento}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"[Error al consultar préstamos activos]: {ex.Message}");
    }
}

public static void ConsultarPrestamosSocioMenu(
    ConsultarPrestamosSocio consultarPrestamosSocio)
{
    try
    {
        Console.WriteLine("---------- PRÉSTAMOS DEL SOCIO ----------");
        Console.WriteLine();

        Console.Write("DNI del socio: ");
        string dniTexto = Console.ReadLine() ?? "";

        if (!int.TryParse(dniTexto, out int dni))
        {
            throw new FormatException(
                "El DNI debe ser un número entero.");
        }

        var socio = consultarPrestamosSocio.BuscarSocio(dni);
        var prestamos = consultarPrestamosSocio.Ejecutar(socio);

        Console.WriteLine();
        Console.WriteLine(
            $"Socio: {socio.Nombre} {socio.Apellido} | DNI: {socio.DNI}");

        if (prestamos.Count == 0)
        {
            Console.WriteLine("El socio no tiene préstamos registrados.");
            return;
        }

        Console.WriteLine();

        foreach (var prestamo in prestamos)
        {
            string estado = prestamo.EstaActivo()
                ? "Activo"
                : "Devuelto";

            Console.WriteLine(
                $"ID: {prestamo.Id} | " +
                $"Libro: {prestamo.Libro.Titulo} | " +
                $"Fecha préstamo: {prestamo.FechaPrestamo} | " +
                $"Vencimiento: {prestamo.FechaVencimiento} | " +
                $"Estado: {estado} | " +
                $"Devolución: {prestamo.FechaDevolucion}");
        }
    }
    catch (FormatException ex)
    {
        Console.WriteLine($"[Error de formato]: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"[Error al consultar préstamos del socio]: {ex.Message}");
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
}