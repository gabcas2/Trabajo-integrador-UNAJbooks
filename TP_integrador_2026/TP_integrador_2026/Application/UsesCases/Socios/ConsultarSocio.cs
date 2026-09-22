using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.UseCases.Socios
{
    public class ConsultarSocios
    {
        private readonly ISocioRepository _socioRepository;

        public ConsultarSocios(ISocioRepository socioRepository)
        {
            _socioRepository = socioRepository;
        }

        public List<Socio> Ejecutar()
        {
            return _socioRepository.ObtenerTodos();
        }
    }
}