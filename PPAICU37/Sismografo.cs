using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class Sismografo
    {
        public string IdentificadorSismografo { get; set; }
        public string NroSerie { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public EstacionSismologica Estacion { get; set; }
        public CambioEstado CambioEstadoActualSismografo { get; set; }
        public List<CambioEstado> HistorialCambios { get; private set; }

        public Sismografo()
        {
            HistorialCambios = new List<CambioEstado>();
        }

        public string getidSismografo()
        {
            return IdentificadorSismografo;
        }

        // Este método es invocado por OrdenDeInspeccion.ponerSismografoFueraDeServicio a través de Sismografo.cambiarEstado
        public void ponerSismografoFueraDeServicio(DateTime fechaHora, List<MotivoFueraServicio> motivos, Estado estadoFueraServicio)
        {
            var cambioActualActivo = HistorialCambios.FirstOrDefault(h => h.esActual());
            if (cambioActualActivo != null)
            {
                cambioActualActivo.finalizar(fechaHora); // Finaliza el estado actual con la fecha y hora actual
            }
            CambioEstado nuevoCambio = CambioEstado.crear(fechaHora, motivos, estadoFueraServicio); // Crea un nuevo cambio sin motivos
            HistorialCambios.Add(nuevoCambio); // Agrega el nuevo cambio al historial
            this.CambioEstadoActualSismografo = nuevoCambio; // Actualiza el estado actual del sismógrafo

            // Console.WriteLine($"DEBUG: Sismógrafo {IdentificadorSismografo} marcado como fuera de servicio (método interno)."); // Para depuración
        }

        //public void cambiarEstado(CambioEstado nuevoCambio)
        //{
        //    if (nuevoCambio == null) return;

        //    var cambioActualActivo = HistorialCambios.FirstOrDefault(h => h.esActual());
        //    if (cambioActualActivo != null)
        //    {
        //        cambioActualActivo.finalizar(nuevoCambio.FechaHoraInicio); // Finaliza el estado anterior con la fecha de inicio del nuevo
        //    }

        //    HistorialCambios.Add(nuevoCambio);
        //    this.EstadoActualSismografo = nuevoCambio.EstadoAsociado;
        //    // Console.WriteLine($"DEBUG: Sismógrafo {IdentificadorSismografo} cambió a estado {EstadoActualSismografo?.NombreEstado}."); // Para depuración
        //} ELIMINAR ESTO DESPUES DE QUE FUNCIONE TODO BIEN
    }
}
