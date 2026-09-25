using TP_integrador_2026.Application.Interfaces;

namespace TP_integrador_2026.Application.UseCases.Socios
{
    public class ModificarSocio
    {
        private readonly ISocioRepository _socioRepository;

        public ModificarSocio(ISocioRepository socioRepository)
        {
            _socioRepository = socioRepository;
        }

        public void Ejecutar(
            int dni,
            int? nuevoTelefono,
            string? nuevaDireccion,
            bool? nuevoEsPremium)
        {
            var socio = _socioRepository.BuscarPorDNI(dni);

            if (socio == null)
                throw new InvalidOperationException(
                    "No existe un socio con el DNI indicado.");

            if (nuevoTelefono.HasValue)
                socio.ActualizarTelefono(nuevoTelefono.Value);

            if (nuevaDireccion != null)
                socio.ActualizarDireccion(nuevaDireccion);

            bool esPremiumActual = socio is Domain.Entities.SocioPremium;

            bool esPremiumFinal = nuevoEsPremium ?? esPremiumActual;

            _socioRepository.Actualizar(
                socio,
                esPremiumFinal);
        }
    }
}