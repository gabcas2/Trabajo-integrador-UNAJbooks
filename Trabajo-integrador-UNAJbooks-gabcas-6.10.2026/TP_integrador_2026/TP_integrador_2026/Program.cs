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
                .Build();

            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")));

            // Repositorios
            services.AddScoped<ILibroRepository, LibroRepository>();
            services.AddScoped<ISocioRepository, SocioRepository>();
            services.AddScoped<IPrestamoRepository, PrestamoRepository>();

            // Casos de uso
            services.AddScoped<RegistrarLibro>();
            services.AddScoped<BuscarLibro>();
            services.AddScoped<EliminarLibro>();
            services.AddScoped<ConsultarLibros>();
            services.AddScoped<RegistrarSocio>();
            services.AddScoped<BuscarSocio>();
            services.AddScoped<ConsultarTodosLosPrestamos>();
            services.AddScoped<ConsultarStockLibro>();
			services.AddScoped<ConsultarLibrosPrestadosASocio>();

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
            
            var consultarTodosLosPrestamos = scope.ServiceProvider.GetRequiredService<ConsultarTodosLosPrestamos>();
            
            var consultarStockLibro = scope.ServiceProvider.GetRequiredService<ConsultarStockLibro>();
            
			var consultarLibrosPrestados = scope.ServiceProvider.GetRequiredService<ConsultarLibrosPrestadosASocio>();

            bool salir = false;
            //Socios
            var registrarSocio =scope.ServiceProvider.GetRequiredService<RegistrarSocio>();
            var buscarSocio =scope.ServiceProvider.GetRequiredService<BuscarSocio>();    
            

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
                Console.WriteLine("7. Salir");
                Console.WriteLine();
                Console.Write("Seleccione una opción: ");

                string? opcion = Console.ReadLine();

                Console.WriteLine();

                try
                {
                    switch (opcion)
                    {
                        case "1":
                    		
                            RegistrarLibroMenu(registrarLibro);
                            break;

                        case "2":
                            BuscarLibroMenu(buscarLibro);
                            break;

                        case "3":
                            ConsultarLibrosMenu(consultarLibros);
                            break;

                        case "4":
                            EliminarLibroMenu(eliminarLibro);
                            break;

                        case "5":
                            RegistrarSocioMenu(registrarSocio);
                            break;

                        case "6":
                            BuscarSocioMenu(buscarSocio);
                            break;

                        case "7":
                            salir = true;
                            Console.WriteLine("Saliendo de UNAJBOOKS...");
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