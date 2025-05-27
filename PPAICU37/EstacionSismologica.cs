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
        public List<Sismografo> Sismografos { get; set; }

        public EstacionSismologica()
        {
            Sismografos = new List<Sismografo>();
        }

        public void ponerSismografoFueraDeServicio(Sismografo sismografo, List<MotivoFueraServicio> motivos, Estado estadoFueraServicio, DateTime fechaHora)
        {
            var sg = Sismografos.FirstOrDefault(s => s.IdentificadorSismografo == sismografo.IdentificadorSismografo);
            if (sg != null)
            {
                CambioEstado cambio = CambioEstado.crear(fechaHora, motivos, estadoFueraServicio);
                sg.cambiarEstado(cambio);
            }
        }

        public string getNombre()
        {
            return NombreEstacion;
        }

        public Sismografo buscarIdSismografo(string idSismografo)
        {
            return Sismografos.FirstOrDefault(s => s.IdentificadorSismografo == idSismografo);
        }
    }
}
