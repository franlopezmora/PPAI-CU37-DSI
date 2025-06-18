using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class Empleado
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string mail { get; set; }
        public string telefono { get; set; }
        public Rol Rol { get; set; }

        public Empleado()
        {}

        public bool sosResponsableDeReparacion()
        {
            return Rol != null && Rol.esResponsableDeReparacion();
        }

        public string getEmail()
        {
            return mail;
        }

        public override string ToString()
        {
            return $"{nombre} {apellido}";
        }
    }
}
