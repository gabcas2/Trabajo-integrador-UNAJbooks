using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Application.UseCases.Socios;

namespace TP_integrador_2026.Presentacion
{
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
}