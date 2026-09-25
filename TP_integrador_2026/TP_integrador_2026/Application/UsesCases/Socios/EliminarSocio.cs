using TP_integrador_2026.Application.Interfaces;

namespace TP_integrador_2026.Application.UseCases.Socios
{
    public class EliminarSocio
    {
        private readonly ISocioRepository _socioRepository;
        private readonly IPrestamoRepository _prestamoRepository;

        public EliminarSocio(
            ISocioRepository socioRepository,
            IPrestamoRepository prestamoRepository)
        {
            _socioRepository = socioRepository;
            _prestamoRepository = prestamoRepository;
        }

        public void Ejecutar(int dni)
        {
            var socio = _socioRepository.BuscarPorDNI(dni);

            if (socio == null)
                throw new InvalidOperationException(
                    "No existe un socio con el DNI indicado.");

            if (_prestamoRepository.TienePrestamosActivos(socio.Id))
                throw new InvalidOperationException(
                    "No se puede eliminar el socio porque tiene libros prestados actualmente.");

            _socioRepository.Eliminar(socio);
        }
    }
}