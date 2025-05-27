using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class Empleado
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public List<Rol> Roles { get; set; }
        public string NombreUsuario { get; set; } // Para facilitar la vinculación con Usuario

        public Empleado()
        {
            Roles = new List<Rol>();
        }

        public bool sosResponsableDeReparacion()
        {
            return Roles.Any(r => r.esResponsableDeReparacion());
        }

        public string getEmail()
        {
            return Mail;
        }

        public override string ToString()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}
