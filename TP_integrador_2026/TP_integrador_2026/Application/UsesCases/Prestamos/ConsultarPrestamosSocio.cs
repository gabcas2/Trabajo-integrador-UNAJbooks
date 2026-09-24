using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;
using TP_integrador_2026.Domain.Exceptions;

namespace TP_integrador_2026.Application.UseCases.Prestamos
{
    public class ConsultarPrestamosSocio : IConsultarPrestamosSocio
    {
        private readonly IBusquedaSocioRepository _busquedaSocioRepository;

        public ConsultarPrestamosSocio(
            IBusquedaSocioRepository busquedaSocioRepository)
        {
            _busquedaSocioRepository = busquedaSocioRepository;
        }

        public Socio BuscarSocio(int dni)
        {
            var socio = _busquedaSocioRepository.BuscarPorDni(dni);

            if (socio == null)
                throw new NotFoundException(
                    "No existe un socio con el DNI indicado.");

            return socio;
        }

        public List<Prestamo> Ejecutar(Socio socio)
        {
            return _busquedaSocioRepository.ObtenerPrestamosPorSocio(
                socio.Id);
        }
    }
}
