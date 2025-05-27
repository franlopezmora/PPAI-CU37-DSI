using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class Rol
    {
        public string NombreRol { get; set; }
        public string DescripcionRol { get; set; }

        public bool esResponsableDeReparacion()
        {
            return NombreRol == "Responsable de Reparaciones"; // Asume un nombre específico para el rol [cite: 1]
        }
    }
}
