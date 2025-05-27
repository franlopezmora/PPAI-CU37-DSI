using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class Usuario
    {
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public Empleado EmpleadoAsociado { get; set; }

        public Empleado getEmpleado()
        {
            return EmpleadoAsociado;
        }
    }
}
