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
        public List<CambioEstado> CambioEstado { get; set; }
        public Estado EstadoActual { get; set; }

        public Sismografo()
        {
            CambioEstado = new List<CambioEstado>();
        }


        public string ponerSismografoFueraDeServicio(DateTime fechaHora, List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario, Empleado responsableLogueado)
        {
            // Delegar al estado actual (patrón State)
            // El estado actual decide a qué estado transicionar
            if (EstadoActual == null)
            {
                throw new InvalidOperationException("El sismógrafo no tiene un estado actual asignado.");
            }
            
            return EstadoActual.ponerSismografoFueraDeServicio(this, fechaHora, listaMotivosTipoComentario, CambioEstado, responsableLogueado);
        }
    }
}
