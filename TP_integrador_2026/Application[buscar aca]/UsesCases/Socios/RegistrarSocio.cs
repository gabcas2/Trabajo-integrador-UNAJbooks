using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.UseCases.Socios
{
    public class RegistrarSocio
    {
        private readonly ISocioRepository _socioRepository;

        public RegistrarSocio(ISocioRepository socioRepository)
        {
            _socioRepository = socioRepository;
        }

        public Socio Ejecutar(
            string nombre,
            string apellido,
            int dni,
            int numTelefono,
            string direccion,
            bool esPremium)
        {
            if (_socioRepository.ExisteDNI(dni))
            {
                throw new InvalidOperationException(
                    "Ya existe un socio registrado con ese DNI.");
            }

            Socio socio;

            if (esPremium)
            {
                socio = new SocioPremium(
                    nombre,
                    apellido,
                    dni,
                    numTelefono,
                    direccion);
            }
            else
            {
                socio = new Socio(
                    nombre,
                    apellido,
                    dni,
                    numTelefono,
                    direccion);
            }

            return _socioRepository.Agregar(socio);
        }
    }
}