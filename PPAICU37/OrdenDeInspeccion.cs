using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class OrdenDeInspeccion
    {
        public int numeroOrden { get; set; }
        public DateTime fechaHoraInicio { get; set; }
        public DateTime? fechaHoraFinalizacion { get; set; }
        public DateTime? fechaHoraCierre { get; set; }
        public string observacionCierre { get; set; }
        public EstadoOrden Estado { get; set; }
        public Empleado Empleado { get; set; }
        public EstacionSismologica EstacionSismologica { get; set; } 

        public OrdenDeInspeccion()
        {
        }

        public bool esDeEmpleado(Empleado EmpleadoLogueado)
        {
            return Empleado == EmpleadoLogueado;
        }

        public bool esCompletamenteRealizada()
        {
            return Estado != null && Estado.esCompletamenteRealizada();
        }

        public string[] getInfoOrdenInspeccion(List<Sismografo> sismografos)
        {
            string nombreEstacion = EstacionSismologica.getNombre();
            string idSismografo = EstacionSismologica.buscarIdSismografo(sismografos);
            DateTime? fechaHoraFinalizacion = this.fechaHoraFinalizacion;
            string[] info = new string[]
            {
                this.numeroOrden.ToString(),
                nombreEstacion,
                idSismografo,
                fechaHoraFinalizacion.HasValue ? fechaHoraFinalizacion.Value.ToString("yyyy-MM-dd HH:mm:ss") : "No finalizada"
            };
            return info;
        }

        public void registrarCierreOrden(DateTime fechaHoraCierre, string observacion, EstadoOrden estadoCerrado)
        {
            // Persistir en base de datos
            DataAccess.ActualizarOrdenInspeccion(numeroOrden, fechaHoraCierre, observacion, estadoCerrado);
            
            // Actualizar en memoria
            this.fechaHoraCierre = fechaHoraCierre;
            this.observacionCierre = observacion;
            this.Estado = estadoCerrado;
        }

        public string ponerSismografoFueraDeServicio(DateTime fechaHora, List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario, List<Sismografo> sismografos, Empleado responsableLogueado)
        {
            // El estado actual del sismógrafo decide a qué estado transicionar
            string nombreEstadoFueraServicio = EstacionSismologica.ponerSismografoFueraDeServicio(fechaHora, listaMotivosTipoComentario, sismografos, responsableLogueado);
            return nombreEstadoFueraServicio;
        }
    }
}
