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

        public bool esCerrado()
        {
            return NombreEstado == "Cerrada";
        }

        public bool esFueraDeServicio()
        {
            return NombreEstado == "Fuera de Servicio";
        }

        public bool esAmbitoOrden()
        {
            if (Ambito == "OrdenInspeccion") 
            { return true; }
            else { return false; }
        }
        public bool esAmbitoSismografo()
        {
            if (Ambito == "Sismografo")
            { return true; }
            else { return false; }
        }

        public override string ToString()
        {
            return NombreEstado;
        }
    }
}
