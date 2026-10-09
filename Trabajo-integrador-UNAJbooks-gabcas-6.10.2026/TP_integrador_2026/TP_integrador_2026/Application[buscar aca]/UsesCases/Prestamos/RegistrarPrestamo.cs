using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.UseCases.Prestamos
{
    public class RegistrarPrestamo
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly ISocioRepository _socioRepository;
        private readonly ILibroRepository _libroRepository;

        public RegistrarPrestamo(
            IPrestamoRepository prestamoRepository,
            ISocioRepository socioRepository,
            ILibroRepository libroRepository)
        {
            _prestamoRepository = prestamoRepository;
            _socioRepository = socioRepository;
            _libroRepository = libroRepository;
        }

        public Prestamo Ejecutar(int dniSocio, string codigoLibro)
        {
            var socio = _socioRepository.BuscarPorDNI(dniSocio);
            if (socio == null)
            {
                throw new InvalidOperationException("El socio no existe.");
            }

            var libro = _libroRepository.BuscarPorCodigo(codigoLibro);
            if (libro == null)
            {
                throw new InvalidOperationException("El libro no existe.");
            }

            libro.DisminuirStock();

            var prestamo = new Prestamo(libro, socio);

            _prestamoRepository.Agregar(prestamo);

            return prestamo;
        }
    }
}