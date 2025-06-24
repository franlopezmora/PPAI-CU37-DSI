using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class Estado
    {
        public string nombreEstado { get; set; }
        public string ambito { get; set; }

        public bool esCompletamenteRealizada()
        {
            return nombreEstado == "Completamente Realizada";
        }

        public bool esCerrado()
        {
            return nombreEstado == "Cerrada";
        }

        public bool esFueraDeServicio()
        {
            return nombreEstado == "Fuera de Servicio";
        }

        public bool esAmbitoOrden()
        {
            if (ambito == "OrdenInspeccion") 
            { return true; }
            else { return false; }
        }
        public bool esAmbitoSismografo()
        {
            if (ambito == "Sismografo")
            { return true; }
            else { return false; }
        }

        public override string ToString()
        {
            return nombreEstado;
        }
    }
}
