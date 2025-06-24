﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class OrdenDeInspeccion
    {
        public int numeroOrden { get; set; }
        public DateTime fechaHoraInicio { get; set; }
        public DateTime? fechaHoraFinalizacion { get; set; }
        public DateTime? fechaHoraCierre { get; set; }
        public string observacionCierre { get; set; }
        public Estado Estado { get; set; }
        public Empleado Empleado { get; set; }
        public EstacionSismologica EstacionSismologica { get; set; } 

        public OrdenDeInspeccion()
        {
        }

        public bool esDeEmpleado(Usuario UsuarioEmpleado)
        {
            return Empleado == UsuarioEmpleado.Empleado;
        }

        public bool esCompletamenteRealizada()
        {
            return Estado != null && Estado.esCompletamenteRealizada();
        }

        public string[] getInfoOrdenInspeccion(List<Sismografo> sismografos)
        {
            string nombreEstacion = EstacionSismologica.getNombre();
            string idSismografo = EstacionSismologica.buscarIdSismografo(sismografos); // Asegura que se busque el sismógrafo asociado a la estación
            DateTime? fechaHoraFinalizacion = this.fechaHoraFinalizacion;
            string[] info = new string[]
            {
                this.numeroOrden.ToString(),
                nombreEstacion,
                idSismografo,
                fechaHoraFinalizacion.HasValue ? fechaHoraFinalizacion.Value.ToString("yyyy-MM-dd HH:mm:ss") : "No finalizada"
            };
            return info;
        }

        public void registrarCierreOrden(DateTime fechaHoraCierre, string observacion, Estado estadoCerrado)
        {
            this.fechaHoraCierre = fechaHoraCierre;
            this.observacionCierre = observacion;
            this.Estado = estadoCerrado;
        }

        public void ponerSismografoFueraDeServicio(DateTime fechaHora, List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario, Estado estadoFueraServicio, List<Sismografo> sismografos)
        {
            EstacionSismologica.ponerSismografoFueraDeServicio(fechaHora, listaMotivosTipoComentario, estadoFueraServicio, sismografos);
        }
    }
}
