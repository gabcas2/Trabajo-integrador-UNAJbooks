using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.UseCases.Prestamos
{
    public class RegistrarPrestamo
    {
        private readonly ISocioRepository _socioRepository;
        private readonly ILibroRepository _libroRepository;
        private readonly IPrestamoRepository _prestamoRepository;

        public RegistrarPrestamo(
            ISocioRepository socioRepository,
            ILibroRepository libroRepository,
            IPrestamoRepository prestamoRepository)
        {
            _socioRepository = socioRepository;
            _libroRepository = libroRepository;
            _prestamoRepository = prestamoRepository;
        }

        public Prestamo Ejecutar(int dni, string codigoLibro)
        {
            var socio = _socioRepository.BuscarPorDNI(dni);

            if (socio == null)
                throw new InvalidOperationException(
                    "No existe un socio con el DNI indicado.");

            var libro = _libroRepository.BuscarPorCodigo(codigoLibro);

            if (libro == null)
                throw new InvalidOperationException(
                    "No existe un libro con el código indicado.");

            if (libro.Stock <= 0)
                throw new InvalidOperationException(
                    "No hay ejemplares disponibles de este libro.");

            if (_prestamoRepository.TienePrestamoActivo(
                socio.Id,
                libro.Id))
            {
                throw new InvalidOperationException(
                    "El socio ya tiene este libro prestado actualmente.");
            }

            int cantidadPrestamos =
                _prestamoRepository.ContarPrestamosActivos(socio.Id);

            if (cantidadPrestamos >= socio.MaxPrestamosActivos)
            {
                throw new InvalidOperationException(
                    $"El socio alcanzó el límite de " +
                    $"{socio.MaxPrestamosActivos} préstamos activos.");
            }

            libro.DisminuirStock();

            var prestamo = new Prestamo(
                libro,
                socio);

            return _prestamoRepository.Agregar(prestamo);
        }
    }
}