using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class MotivoFueraServicio
    {
        public MotivoTipo Tipo { get; set; }
        public string Comentario { get; set; }

        public MotivoFueraServicio(MotivoTipo tipo, string comentario)
        {
            this.Tipo = tipo;
            this.Comentario = comentario;
        }
    }
}
