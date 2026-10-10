using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.UseCases.Socios
{
    public class BuscarSocio : IBuscarSocio
    {
        private readonly ISocioRepository _socioRepository;

        public BuscarSocio(ISocioRepository socioRepository)
        {
            _socioRepository = socioRepository;
        }

        public Socio Ejecutar(int dni)
        {
            var socio = _socioRepository.BuscarPorDNI(dni);

            if (socio == null)
                throw new InvalidOperationException(
                    "No existe un socio con el DNI indicado.");

            return socio;
        }
    }
}