using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class Sismografo
    {
        public string identificadorSismografo { get; set; }
        public string nroSerie { get; set; }
        public DateTime fechaAdquisicion { get; set; }
        public EstacionSismologica EstacionSismologica { get; set; }
        public List<CambioEstado> CambioEstado { get; private set; }

        public Sismografo()
        {
            CambioEstado = new List<CambioEstado>();
        }

        public string getIdSismografo()
        {
            return identificadorSismografo;
        }

        public void ponerSismografoFueraDeServicio(DateTime fechaHora, List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario, Estado estadoFueraServicio)
        {
            var cambioEstadoActualActivo = CambioEstado.FirstOrDefault(h => h.esActual());
            if (cambioEstadoActualActivo != null)
            {
                cambioEstadoActualActivo.finalizar(fechaHora);
            }
            CambioEstado nuevoCambio = PPAICU37.CambioEstado.crear(fechaHora, listaMotivosTipoComentario, estadoFueraServicio);
            CambioEstado.Add(nuevoCambio);

        }
    }
}
