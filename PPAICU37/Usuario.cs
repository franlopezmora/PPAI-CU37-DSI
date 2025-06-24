using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class Usuario
    {
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public Empleado Empleado { get; set; }

        public Empleado getEmpleado()
        {
            return Empleado;
        }
    }
}
