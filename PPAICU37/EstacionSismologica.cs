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

        public string ponerSismografoFueraDeServicio(DateTime fechaHora, List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario, List<Sismografo> sismografos, Empleado responsableLogueado)
        {
            Sismografo sismografoAsociado = buscarSismografo(sismografos);

            // El estado actual del sismógrafo decide a qué estado transicionar
            string nombreEstadoFueraServicio = sismografoAsociado.ponerSismografoFueraDeServicio(fechaHora, listaMotivosTipoComentario, responsableLogueado);

            return nombreEstadoFueraServicio;
        }

        public string getNombre()
        {
            return nombreEstacion;
        }

        public string buscarIdSismografo(IEnumerable<Sismografo> sismografos)
        {
            Sismografo sismografo = sismografos.FirstOrDefault(s => s.EstacionSismologica == this);
            return sismografo?.identificadorSismografo;
        }

        public Sismografo buscarSismografo(IEnumerable<Sismografo> sismografos)
        {
            return sismografos.FirstOrDefault(s => s.EstacionSismologica == this);
        }
    }
}
