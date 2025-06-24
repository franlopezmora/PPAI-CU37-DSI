﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class MotivoTipo
    {
        public int IdTipo { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }
        public MotivoTipo(int idTipo, string descripcion)
        {
            IdTipo = idTipo;
            Descripcion = descripcion;
        }
        public MotivoTipo buscarMotivoTipo() 
        { 
            return this;
        }
    }
}
