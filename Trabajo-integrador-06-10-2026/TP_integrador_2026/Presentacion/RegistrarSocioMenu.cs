using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Application.UseCases.Socios;

namespace TP_integrador_2026.Presentacion
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
}