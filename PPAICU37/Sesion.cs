﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class Sesion
    {
        public Usuario UsuarioLogueado { get; private set; }
        public DateTime FechaHoraInicio { get; private set; }
        public DateTime? FechaHoraFin { get; private set; }

        public void Iniciar(Usuario usuario)
        {
            UsuarioLogueado = usuario;
            FechaHoraInicio = DateTime.Now;
            FechaHoraFin = null;
        }

        public void Cerrar()
        {
            FechaHoraFin = DateTime.Now;
        }

        public Empleado getUsuario()
        {
            Empleado empleadoLogueado = UsuarioLogueado.getEmpleado();
            return empleadoLogueado;
        }
}
}
