using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class OrdenDeInspeccion
    {
        public int NumeroOrden { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime? FechaHoraFinalizacion { get; set; }
        public DateTime? FechaHoraCierre { get; set; }
        public string ObservacionCierre { get; set; }
        public Estado EstadoActual { get; set; }
        public Empleado Responsable { get; set; }
         //public Sismografo SismografoAfectado { get; set; } // ELIMINAR ESTO URGENTE
        public List<CambioEstado> HistorialCambiosEstado { get; set; }
        public EstacionSismologica EstacionSismologica { get; set; } // Relación con la estación sismológica

        public OrdenDeInspeccion()
        {
            HistorialCambiosEstado = new List<CambioEstado>();
        }

        public bool esDeEmpleado(Empleado empleado)
        {
            return Responsable?.NombreUsuario == empleado?.NombreUsuario;
        }

        public bool esCompletamenteRealizada()
        {
            return EstadoActual != null && EstadoActual.esCompletamenteRealizada();
        }

        public string getInfoOrdenInspeccion(List<Sismografo> sismografos)
        {
            string nombreEstacion = EstacionSismologica.getNombre();
            string idSismografo = EstacionSismologica.buscarIdSismografo(sismografos); // Asegura que se busque el sismógrafo asociado a la estación
            DateTime? fechaHoraFinalizacion = this.FechaHoraFinalizacion;
            return $"Orden N°: {NumeroOrden}, Estado: {EstadoActual?.NombreEstado}, Sismógrafo: {idSismografo}, FechaHoraFinalizacion {fechaHoraFinalizacion}";
        }

        public void registrarCierreOrden(DateTime fechaHoraCierre, string observacion, Estado estadoCerrado)
        {
            this.FechaHoraCierre = fechaHoraCierre;
            this.ObservacionCierre = observacion;
            this.EstadoActual = estadoCerrado;
            // Console.WriteLine($"DEBUG: Orden {NumeroOrden} registrada como cerrada."); // Para depuración
        }

        public void ponerSismografoFueraDeServicio(DateTime fechaHora, List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario, Estado estadoFueraServicio, List<Sismografo> sismografos)
        {
            EstacionSismologica.ponerSismografoFueraDeServicio(fechaHora, listaMotivosTipoComentario, estadoFueraServicio, sismografos);
        }
    }
}
