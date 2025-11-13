using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public abstract class Estado
    {
        public abstract string nombreEstado { get; }
        public abstract string ambito { get; }

        // --- Métodos de consulta (helpers) ---
        public virtual bool esCompletamenteRealizada()
        {
            return false;
        }

        public virtual bool esCerrado()
        {
            return false;
        }

        public virtual bool esFueraDeServicio()
        {
            return false;
        }

        public virtual bool esInhabilitadoPorInspeccion()
        {
            return false;
        }

        public virtual bool esAmbitoOrden()
        {
            return ambito == "OrdenInspeccion";
        }

        public virtual bool esAmbitoSismografo()
        {
            return ambito == "Sismografo";
        }

        // --- Acciones "comunes" con default que lanza excepción ---
        public virtual string ponerSismografoFueraDeServicio(
            Sismografo sismografo, 
            DateTime fechaHora, 
            List<Tuple<string, MotivoTipo>> motivosTipoComentario, 
            List<CambioEstado> cambiosEstado,
            Empleado responsableLogueado
            )
            => throw new InvalidOperationException($"'{nombreEstado}' no permite poner fuera de servicio.");

        public override string ToString()
        {
            return nombreEstado;
        }

        public virtual Estado crearEstado()
        {
            return null;
        }
        
        public virtual CambioEstado crearCambioEstado(DateTime fechaHora, List<Tuple<string, MotivoTipo>> motivosTipoComentario, Estado estadoSiguiente, Empleado responsableLogueado)
        {
            return new CambioEstado();
        }
    }
}
