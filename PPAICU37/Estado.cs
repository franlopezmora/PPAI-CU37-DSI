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

        // --- Métodos de consulta (helpers) ---
        public virtual bool esFueraDeServicio()
        {
            return false;
        }

        public virtual bool esInhabilitadoPorInspeccion()
        {
            return false;
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

        public virtual string inspeccionar()
            => throw new InvalidOperationException($"'{nombreEstado}' no permite inspeccionar.");

        public virtual string enInstalacion()
            => throw new InvalidOperationException($"'{nombreEstado}' no permite poner en instalación.");

        public virtual string darBaja()
            => throw new InvalidOperationException($"'{nombreEstado}' no permite dar de baja.");

        public virtual string ponerDisponible()
            => throw new InvalidOperationException($"'{nombreEstado}' no permite poner disponible.");

        public virtual string registrarCertificacionTerreno()
            => throw new InvalidOperationException($"'{nombreEstado}' no permite registrar certificación de terreno.");

        public virtual string ponerEnLinea()
            => throw new InvalidOperationException($"'{nombreEstado}' no permite poner en línea.");

        public virtual string incluirEnPlan()
            => throw new InvalidOperationException($"'{nombreEstado}' no permite incluir en plan.");

        public virtual string reservar()
            => throw new InvalidOperationException($"'{nombreEstado}' no permite reservar.");

        public virtual string realizarReclamo()
            => throw new InvalidOperationException($"'{nombreEstado}' no permite realizar reclamo.");

        public override string ToString()
        {
            return nombreEstado;
        }

        protected virtual Estado crearEstado()
        {
            throw new InvalidOperationException($"'{nombreEstado}' no puede determinar el estado siguiente para esta transición.");
        }
        
        protected virtual CambioEstado crearCambioEstado(DateTime fechaHora, List<Tuple<string, MotivoTipo>> motivosTipoComentario, Estado estadoSiguiente, Empleado responsableLogueado)
        {
            return CambioEstado.crear(fechaHora, motivosTipoComentario, estadoSiguiente, responsableLogueado);
        }
    }
}
