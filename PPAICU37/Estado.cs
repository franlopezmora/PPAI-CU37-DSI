using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class Estado
    {
        public string NombreEstado { get; set; }
        public string Ambito { get; set; } // Ej: "OrdenInspeccion", "Sismografo" [cite: 1]

        public bool esCompletamenteRealizada()
        {
            return NombreEstado == "Completamente Realizada";
        }

        public bool esEstadoCerrado()
        {
            return NombreEstado == "Cerrada";
        }

        public override string ToString()
        {
            return NombreEstado;
        }
    }
}
