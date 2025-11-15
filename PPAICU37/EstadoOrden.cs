using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class EstadoOrden
    {
        public string nombreEstado { get; set; }
        public string ambito { get; set; }

        public EstadoOrden(string nombreEstado, string ambito)
        {
            this.nombreEstado = nombreEstado;
            this.ambito = ambito;
        }

        public bool esCompletamenteRealizada()
        {
            return nombreEstado == "Completamente Realizada";
        }

        public bool esCerrado()
        {
            return nombreEstado == "Cerrada";
        }

        public bool esAmbitoOrden()
        {
            return ambito == "OrdenInspeccion";
        }

        public override string ToString()
        {
            return nombreEstado;
        }
    }
}

