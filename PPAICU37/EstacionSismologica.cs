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

        public void ponerSismografoFueraDeServicio(List<Sismografo> sismografos)
        {
            Sismografo sismografoAsociado = buscarIdSismografo(sismografos);
            sismografoAsociado.ponerSismografoFueraDeServicio();         

        }

        public string getNombre()
        {
            return NombreEstacion;
        }

        public Sismografo buscarIdSismografo(IEnumerable<Sismografo> sismografos)
        {
            Sismografo sismografo = sismografos.FirstOrDefault(s => s.Estacion == this);
            //    return sismografo.getidSismografo();
            return sismografo;
        }
    }
}
