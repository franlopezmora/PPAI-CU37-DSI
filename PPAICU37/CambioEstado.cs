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
        public List<MotivoFueraServicio> MotivoFueraServicio { get; private set; }
        public Estado Estado { get; set; }

        private CambioEstado()
        {
            MotivoFueraServicio = new List<MotivoFueraServicio>();
        }

        public static CambioEstado crear(DateTime fechaHoraInicio, List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario, Estado estado)
        {
            var cambio = new CambioEstado
            {
                fechaHoraInicio = fechaHoraInicio,
                Estado = estado
            };
            listaMotivosTipoComentario = listaMotivosTipoComentario ?? new List<Tuple<string, MotivoTipo>>();

            MotivoFueraServicio motivo;
            for (int i = 0; i < listaMotivosTipoComentario.Count; i++)
            {
                motivo = new MotivoFueraServicio(listaMotivosTipoComentario[i].Item2, listaMotivosTipoComentario[i].Item1);
                cambio.MotivoFueraServicio.Add(motivo);
            }
            return cambio;
        }

        public bool esActual()
        {
            return fechaHoraFin == null;
        }

        public void setMotivo(MotivoFueraServicio motivo)
        {
            if (motivo != null)
            {
                this.MotivoFueraServicio.Add(motivo);
            }
        }

        public void setFechaHora(DateTime fechaHoraInicio)
        {
            this.fechaHoraInicio = fechaHoraInicio;
        }

        public void finalizar(DateTime fechaHoraFin)
        {
            this.fechaHoraFin = fechaHoraFin;
            // Console.WriteLine($"DEBUG: CambioEstado finalizado en {fechaHoraFin}."); // Para depuración
        }
    }
}
