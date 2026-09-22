using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Application.UseCases.Libros;
using TP_integrador_2026.Infrastructure.Persistence;
using TP_integrador_2026.Infrastructure.Repositories;
using TP_integrador_2026.Application.UseCases.Socios;
using TP_integrador_2026.Domain.Entities;

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
            services.AddScoped<ConsultarSocios>();
            services.AddScoped<ModificarSocio>();
            services.AddScoped<EliminarSocio>();

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
            var consultarSocios = scope.ServiceProvider.GetRequiredService<ConsultarSocios>();
            var modificarSocio =scope.ServiceProvider.GetRequiredService<ModificarSocio>();
            var eliminarSocio =scope.ServiceProvider.GetRequiredService<EliminarSocio>();
            

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
                Console.WriteLine("8. Modificar Socio");
                Console.WriteLine("9. Eliminar Socio");
                Console.WriteLine("10. Salir");

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
                            ConsultarSociosMenu(consultarSocios);
                            break;

                        case "8":
                            ModificarSocioMenu(modificarSocio);
                            break;

                        case "9":
                            EliminarSocioMenu(eliminarSocio);
                            break;

                        case "10":
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

        //estos metodos despues deberian ir a otra clase para que no quede tan grande el main

        static void RegistrarLibroMenu(RegistrarLibro registrarLibro)
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
                Console.WriteLine("El stock debe ser un número entero.");
                return;
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

        static void BuscarLibroMenu(BuscarLibro buscarLibro)
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

        static void ConsultarLibrosMenu(ConsultarLibros consultarLibros)
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
        }

        static void EliminarLibroMenu(EliminarLibro eliminarLibro)
        {
            Console.WriteLine("---------- ELIMINAR LIBRO ----------");
            Console.WriteLine();

            Console.Write("Código del libro: ");
            string codigo = Console.ReadLine() ?? "";

            eliminarLibro.Ejecutar(codigo);

            Console.WriteLine();
            Console.WriteLine("Libro eliminado correctamente.");
        }

        static void RegistrarSocioMenu(RegistrarSocio registrarSocio)
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
        Console.WriteLine("El DNI debe ser un número entero.");
        return;
    }

    Console.Write("Teléfono: ");
    string telefonoTexto = Console.ReadLine() ?? "";

    if (!int.TryParse(telefonoTexto, out int numTelefono))
    {
        Console.WriteLine("El teléfono debe ser un número entero.");
        return;
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
static void BuscarSocioMenu(BuscarSocio buscarSocio)
{
    Console.WriteLine("---------- BUSCAR SOCIO ----------");
    Console.WriteLine();

    Console.Write("DNI del socio: ");
    string dniTexto = Console.ReadLine() ?? "";

    if (!int.TryParse(dniTexto, out int dni))
    {
        Console.WriteLine("El DNI debe ser un número entero.");
        return;
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
    

    static void ConsultarSociosMenu(ConsultarSocios consultarSocios)
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
    static void ModificarSocioMenu(ModificarSocio modificarSocio)
{
    Console.WriteLine("---------- MODIFICAR SOCIO ----------");
    Console.WriteLine();

    Console.Write("DNI del socio: ");
    string dniTexto = Console.ReadLine() ?? "";

    if (!int.TryParse(dniTexto, out int dni))
    {
        Console.WriteLine("El DNI debe ser un número entero.");
        return;
    }

    int? nuevoTelefono = null;
    string? nuevaDireccion = null;
    bool? nuevoEsPremium = null;

    bool tieneCambios = false;
    bool finalizar = false;

    while (!finalizar)
    {
        Console.WriteLine();
        Console.WriteLine("¿Qué dato desea modificar?");
        Console.WriteLine("1. Teléfono");
        Console.WriteLine("2. Dirección");
        Console.WriteLine("3. Tipo de socio");
        Console.WriteLine("4. Finalizar");
        Console.WriteLine();

        Console.Write("Seleccione una opción: ");
        string opcion = Console.ReadLine() ?? "";

        Console.WriteLine();

        switch (opcion)
        {
            case "1":
                Console.Write("Nuevo teléfono: ");
                string telefonoTexto = Console.ReadLine() ?? "";

                if (!int.TryParse(telefonoTexto, out int telefono))
                {
                    Console.WriteLine("El teléfono debe ser un número entero.");
                    break;
                }

                nuevoTelefono = telefono;
                tieneCambios = true;

                Console.WriteLine("Teléfono seleccionado para modificar.");
                break;

            case "2":
                Console.Write("Nueva dirección: ");
                string direccion = Console.ReadLine() ?? "";

                nuevaDireccion = direccion;
                tieneCambios = true;

                Console.WriteLine("Dirección seleccionada para modificar.");
                break;

            case "3":
                Console.Write("¿El socio será Premium? (s/n): ");
                string premiumTexto = Console.ReadLine() ?? "";

                if (premiumTexto.Equals(
                    "s",
                    StringComparison.OrdinalIgnoreCase))
                {
                    nuevoEsPremium = true;
                    tieneCambios = true;

                    Console.WriteLine("El socio será Premium.");
                }
                else if (premiumTexto.Equals(
                    "n",
                    StringComparison.OrdinalIgnoreCase))
                {
                    nuevoEsPremium = false;
                    tieneCambios = true;

                    Console.WriteLine("El socio será Normal.");
                }
                else
                {
                    Console.WriteLine("Debe ingresar 's' o 'n'.");
                }

                break;

            case "4":
                finalizar = true;
                break;

            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }

    if (!tieneCambios)
    {
        Console.WriteLine();
        Console.WriteLine("No se seleccionó ningún dato para modificar.");
        return;
    }

    modificarSocio.Ejecutar(
        dni,
        nuevoTelefono,
        nuevaDireccion,
        nuevoEsPremium);

    Console.WriteLine();
    Console.WriteLine("Socio modificado correctamente.");
}
static void EliminarSocioMenu(EliminarSocio eliminarSocio)
{
    Console.WriteLine("---------- ELIMINAR SOCIO ----------");
    Console.WriteLine();

    Console.Write("DNI del socio: ");
    string dniTexto = Console.ReadLine() ?? "";

    if (!int.TryParse(dniTexto, out int dni))
    {
        Console.WriteLine("El DNI debe ser un número entero.");
        return;
    }

    eliminarSocio.Ejecutar(dni);

    Console.WriteLine();
    Console.WriteLine("Socio eliminado correctamente.");
}

}
}
    

    
