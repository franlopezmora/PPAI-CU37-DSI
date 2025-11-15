using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class CambioEstado
    {
        public DateTime fechaHoraInicio { get; set; }
        public DateTime? fechaHoraFin { get; set; }
        public List<MotivoFueraServicio> MotivoFueraServicio { get; set; }
        public Estado Estado { get; set; }
        public Empleado Empleado { get; set; }

        public CambioEstado()
        {
            MotivoFueraServicio = new List<MotivoFueraServicio>();
        }

        public static CambioEstado crear(DateTime fechaHoraInicio, List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario, Estado estado, Empleado responsableLogueado)
        {
            var cambio = new CambioEstado
            {
                Estado = estado
            };
            
            cambio.setFechaHoraInicio(fechaHoraInicio);
            cambio.setResponsableInspeccion(responsableLogueado);
            
            listaMotivosTipoComentario = listaMotivosTipoComentario ?? new List<Tuple<string, MotivoTipo>>();
            
            cambio.setMotivo(listaMotivosTipoComentario);
            return cambio;
        }

        public bool esActual()
        {
            return fechaHoraFin == null;
        }

        private void setFechaHoraInicio(DateTime fechaHoraInicio)
        {
            this.fechaHoraInicio = fechaHoraInicio;
        }

        private void setResponsableInspeccion(Empleado empleado)
        {
            this.Empleado = empleado;
        }

        private void setMotivo(List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario)
        {
            MotivoFueraServicio motivo;
            for (int i = 0; i < listaMotivosTipoComentario.Count; i++)
            {
                motivo = new MotivoFueraServicio(listaMotivosTipoComentario[i].Item2, listaMotivosTipoComentario[i].Item1);
                MotivoFueraServicio.Add(motivo);
            }
        }

        public void finalizar(DateTime fechaHoraFin)
        {
            this.fechaHoraFin = fechaHoraFin;
            // Console.WriteLine($"DEBUG: CambioEstado finalizado en {fechaHoraFin}."); // Para depuración
        }
    }
}
