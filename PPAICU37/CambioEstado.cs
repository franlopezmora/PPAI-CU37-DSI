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
        public DateTime FechaHoraInicio { get; set; }
        public DateTime? FechaHoraFin { get; set; }
        public List<MotivoFueraServicio> Motivos { get; private set; }
        public Estado EstadoAsociado { get; set; }

        private CambioEstado()
        {
            Motivos = new List<MotivoFueraServicio>();
        }

        public static CambioEstado crear(DateTime fechaHoraInicio, List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario, Estado estado)
        {
            var cambio = new CambioEstado
            {
                FechaHoraInicio = fechaHoraInicio,
                EstadoAsociado = estado
            };
            listaMotivosTipoComentario = listaMotivosTipoComentario ?? new List<Tuple<string, MotivoTipo>>();

            MotivoFueraServicio motivo;
            for (int i = 0; i < listaMotivosTipoComentario.Count; i++)
            {
                motivo = new MotivoFueraServicio(listaMotivosTipoComentario[i].Item2, listaMotivosTipoComentario[i].Item1);
                cambio.Motivos.Add(motivo);
            }
            return cambio;
        }

        public bool esActual()
        {
            return FechaHoraFin == null;
        }

        public void setMotivo(MotivoFueraServicio motivo)
        {
            if (motivo != null)
            {
                this.Motivos.Add(motivo);
            }
        }

        public void setFechaHora(DateTime fechaHoraInicio)
        {
            this.FechaHoraInicio = fechaHoraInicio;
        }

        public void finalizar(DateTime fechaHoraFin)
        {
            this.FechaHoraFin = fechaHoraFin;
            // Console.WriteLine($"DEBUG: CambioEstado finalizado en {fechaHoraFin}."); // Para depuración
        }
    }
}
