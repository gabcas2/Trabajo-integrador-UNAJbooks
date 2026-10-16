using TP_integrador_2026.Application.Interfaces;
using TP_integrador_2026.Domain.Entities;

namespace TP_integrador_2026.Application.UseCases.Prestamos
{
    public class ConsultarPrestamosActivos
    {
        private readonly IPrestamoRepository _prestamoRepository;

        public ConsultarPrestamosActivos(IPrestamoRepository prestamoRepository)
        {
            _prestamoRepository = prestamoRepository;
        }

        public List<Prestamo> Ejecutar()
        {
            return _prestamoRepository.ObtenerActivos();
        }

          public Dictionary<int, int> ContarPrestamosActivosPorSocio(
            List<Prestamo> prestamosActivos)
        {
            var conteoPorDni = new Dictionary<int, int>();

            foreach (var prestamo in prestamosActivos)
            {
                int dni = prestamo.Socio.DNI;

                if (conteoPorDni.ContainsKey(dni))
                {
                    conteoPorDni[dni]++;
                }
                else
                {
                    conteoPorDni[dni] = 1;
                }
            }

            return conteoPorDni;
        }
    }
}