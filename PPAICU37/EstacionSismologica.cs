using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class EstacionSismologica
    {
        public string codigoEstacion { get; set; }
        public string nombreEstacion { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }

        public EstacionSismologica()
        {
        }

        public string ponerSismografoFueraDeServicio(DateTime fechaHora, List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario, Estado estadoFueraServicio, List<Sismografo> sismografos)
        {
            Sismografo sismografoAsociado = buscarSismografo(sismografos);

            string nombreEstadoFueraServicio = sismografoAsociado.ponerSismografoFueraDeServicio(fechaHora, listaMotivosTipoComentario, estadoFueraServicio);

            return nombreEstadoFueraServicio;
        }

        public string getNombre()
        {
            return nombreEstacion;
        }

        public string buscarIdSismografo(IEnumerable<Sismografo> sismografos)
        {
            Sismografo sismografo = sismografos.FirstOrDefault(s => s.EstacionSismologica == this);
            string idSismografo = sismografo.getIdSismografo();
            return idSismografo;
        }

        public Sismografo buscarSismografo(IEnumerable<Sismografo> sismografos)
        {
            return sismografos.FirstOrDefault(s => s.EstacionSismologica == this);
        }
    }
}
