using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Libros;
using TP_integrador_2026.Infrastructure.Persistence;
using TP_integrador_2026.Infrastructure.Repositories;
using TP_integrador_2026.Application.UseCases.Socios;
using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Presentacion.Biblioteca;
using TP_integrador_2026.Application.UseCases.Prestamos;

namespace TP_integrador_2026
{
    internal class Program
    {
        static void Main(string[] args)
        {
           var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")));

            // Repositorios
            services.AddScoped<ILibroRepository, LibroRepository>();
            services.AddScoped<ISocioRepository, SocioRepository>();
            services.AddScoped<IPrestamoRepository, PrestamoRepository>();
            services.AddScoped<IBusquedaSocioRepository, BusquedaSocioRepository>();



            // Casos de uso
            services.AddScoped<RegistrarLibro>();
            services.AddScoped<BuscarLibro>();
            services.AddScoped<EliminarLibro>();
            services.AddScoped<ConsultarLibros>();
            services.AddScoped<RegistrarSocio>();
            services.AddScoped<BuscarSocio>();
            services.AddScoped<ModificarSocio>();
            services.AddScoped<ConsultarSocios>();
            services.AddScoped<EliminarSocio>();
            services.AddScoped<RegistrarPrestamo>();
            services.AddScoped<RegistrarDevolucion>();
            services.AddScoped<ConsultarPrestamosActivos>();
            services.AddScoped<ConsultarPrestamosSocio>();

            using var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            //Libros
            var registrarLibro =
                scope.ServiceProvider.GetRequiredService<RegistrarLibro>();

            var buscarLibro =
                scope.ServiceProvider.GetRequiredService<BuscarLibro>();

            var consultarLibros =
                scope.ServiceProvider.GetRequiredService<ConsultarLibros>();

            var eliminarLibro =
                scope.ServiceProvider.GetRequiredService<EliminarLibro>();

            bool salir = false;
            //Socios
            var registrarSocio =scope.ServiceProvider.GetRequiredService<RegistrarSocio>();
            var buscarSocio =scope.ServiceProvider.GetRequiredService<BuscarSocio>();
            var modificarSocio =scope.ServiceProvider.GetRequiredService<ModificarSocio>();
            var consultarSocios =scope.ServiceProvider.GetRequiredService<ConsultarSocios>();
            var eliminarSocio =scope.ServiceProvider.GetRequiredService<EliminarSocio>();

            //Prestamos
            var registrarPrestamo = scope.ServiceProvider.GetRequiredService<RegistrarPrestamo>();
            var registrarDevolucion = scope.ServiceProvider.GetRequiredService<RegistrarDevolucion>();
            var verPrestamosActivos = scope.ServiceProvider.GetRequiredService<ConsultarPrestamosActivos>();
            var consultarPrestamosSocio = scope.ServiceProvider.GetRequiredService<ConsultarPrestamosSocio>();



            while (!salir)
            {
                Console.Clear();

                Console.WriteLine("=================================");
                Console.WriteLine("            UNAJBOOKS");
                Console.WriteLine("=================================");
                Console.WriteLine();
                Console.WriteLine("1. Registrar libro");
                Console.WriteLine("2. Buscar libro por código");
                Console.WriteLine("3. Consultar todos los libros");
                Console.WriteLine("4. Eliminar libro");
                Console.WriteLine("5. Registrar socio");
                Console.WriteLine("6. Buscar socio por DNI");
                Console.WriteLine("7. Consultar todos los socios");
                Console.WriteLine("8. Modificar socio");
                Console.WriteLine("9. Eliminar Socio");
                Console.WriteLine("10. Registrar préstamo");
                Console.WriteLine("11. Registrar devolución");
                Console.WriteLine("12. Ver préstamos activos");
                Console.WriteLine("13. Consultar préstamos de un socio");
                Console.WriteLine("14. Demostracion de recorridos de colección");
                Console.WriteLine("15. Salir");

                Console.WriteLine();
                Console.Write("Seleccione una opción: ");

                string? opcion = Console.ReadLine();

                Console.WriteLine();

                try
                {
                    switch (opcion)
                    {
                        case "1":
                            Biblioteca.RegistrarLibroMenu(registrarLibro);
                            break;

                        case "2":
                            Biblioteca.BuscarLibroMenu(buscarLibro);
                            break;

                        case "3":
                            Biblioteca.ConsultarLibrosMenu(consultarLibros);
                            break;

                        case "4":
                            Biblioteca.EliminarLibroMenu(eliminarLibro);
                            break;

                        case "5":
                            Biblioteca.RegistrarSocioMenu(registrarSocio);
                            break;

                        case "6":
                            Biblioteca.BuscarSocioMenu(buscarSocio);
                            break;
                        case "7":
                            Biblioteca.ConsultarSociosMenu(consultarSocios);
                            break;

                        case "8":
                            Biblioteca.ModificarSocioMenu(modificarSocio);
                            break;

                        case "9":
                            Biblioteca.EliminarSocioMenu(eliminarSocio);
                            break;
                        case "10":
                           Biblioteca.RegistrarPrestamoMenu(registrarPrestamo);
                            break;
                        case "11":
                            Biblioteca.RegistrarDevolucionMenu(registrarDevolucion);
                            break;
                        case "12":
                            Biblioteca.VerPrestamosActivosMenu(verPrestamosActivos);
                            break;
                        case "13":
                            Biblioteca.ConsultarPrestamosSocioMenu(consultarPrestamosSocio);
                            break;
                        case "14":
                            Biblioteca.RecorridosLibrosMenu(consultarLibros);
                            break;

                        case "15":
                            Console.WriteLine("Saliendo de UNAJBOOKS...");
                            salir = true;
                            break;


                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Error: {ex.Message}");
                }

                if (!salir)
                {
                    Console.WriteLine();
                    Console.WriteLine("Presione ENTER para continuar...");
                    Console.ReadLine();
                }
            }
        }
    }

}