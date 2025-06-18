using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class EstacionSismologica
    {
        public string CodigoEstacion { get; set; }
        public string NombreEstacion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public EstacionSismologica()
        {
            // Constructor por defecto
        }

        //    public void ponerSismografoFueraDeServicio(Sismografo sismografo, List<MotivoFueraServicio> motivos, Estado estadoFueraServicio, DateTime fechaHora)
        //    {
        //
        //        var sg = Sismografos.FirstOrDefault(s => s.IdentificadorSismografo == sismografo.IdentificadorSismografo);
        //       if (sg != null)
        //      {
        //          CambioEstado cambio = CambioEstado.crear(fechaHora, motivos, estadoFueraServicio);
        //          sg.cambiarEstado(cambio);
        //      }
        //  }

        public void ponerSismografoFueraDeServicio(DateTime fechaHora, List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario, Estado estadoFueraServicio, List<Sismografo> sismografos)
        {
            Sismografo sismografoAsociado = buscarSismografo(sismografos);

            sismografoAsociado.ponerSismografoFueraDeServicio(fechaHora, listaMotivosTipoComentario, estadoFueraServicio);         

        }

        public string getNombre()
        {
            return NombreEstacion;
        }

        public string buscarIdSismografo(IEnumerable<Sismografo> sismografos)
        {
            Sismografo sismografo = sismografos.FirstOrDefault(s => s.Estacion == this);
            string idSismografo = sismografo.getidSismografo(); // Asegura que se obtenga el ID del sismógrafo asociado a la estación
            return idSismografo;
        }
        public Sismografo buscarSismografo(IEnumerable<Sismografo> sismografos)
        {
            return sismografos.FirstOrDefault(s => s.Estacion == this);
        }
    }
}
