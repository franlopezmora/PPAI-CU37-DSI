using System;
using System.Collections.Generic;
using System.Linq;

namespace PPAICU37;

public class InhabilitadoPorInspeccion : Estado
{
    public InhabilitadoPorInspeccion() { }

    public override string nombreEstado => "Inhabilitado por Inspección";

    public override bool esInhabilitadoPorInspeccion()
    {
        return true;
    }
    
    public override string ponerSismografoFueraDeServicio(
        Sismografo sismografo, 
        DateTime fechaHora, 
        List<Tuple<string, MotivoTipo>> motivosTipoComentario, 
        List<CambioEstado> cambiosEstado,
        Empleado responsableLogueado)
    {
        // 1. Finalizar el cambio de estado actual (en memoria)
        CambioEstado actual = cambiosEstado.FirstOrDefault(h => h.esActual());
        actual?.finalizar(fechaHora);
        
        // El estado decide a qué estado transicionar
        Estado estadoSiguiente = crearEstado();
        
        // 2. Crear nuevo cambio de estado (en memoria)
        CambioEstado nuevo = crearCambioEstado(fechaHora, motivosTipoComentario, estadoSiguiente, responsableLogueado);
        cambiosEstado.Add(nuevo);

        // 3. Actualizar estado actual del sismógrafo (en memoria)
        sismografo.EstadoActual = estadoSiguiente;

        // 4. Persistir en base de datos (después de tener todo en memoria)
        try
        {
            DataAccess.PersistirCambioEstado(
                sismografo.identificadorSismografo,
                actual,  // Cambio de estado anterior (ya finalizado en memoria)
                nuevo     // Nuevo cambio de estado (ya creado en memoria)
            );
        }
        catch (Exception ex)
        {
            // Si falla la persistencia, revertir cambios en memoria
            // Restaurar el cambio de estado anterior
            if (actual != null)
            {
                actual.fechaHoraFin = null; // Revertir finalización
            }
            
            // Remover el nuevo cambio de estado
            cambiosEstado.Remove(nuevo);
            
            // Restaurar estado anterior
            sismografo.EstadoActual = this;
            
            throw new InvalidOperationException($"Error al persistir el cambio de estado: {ex.Message}", ex);
        }

        return estadoSiguiente.nombreEstado;
    }

    protected override Estado crearEstado()
    {
        FueraDeServicio estado = new FueraDeServicio();
        return estado;
    }
    
    protected override CambioEstado crearCambioEstado(DateTime fechaHora, List<Tuple<string, MotivoTipo>> motivosTipoComentario, Estado estadoSiguiente, Empleado responsableLogueado)
    {
        CambioEstado nuevo = CambioEstado.crear(fechaHora, motivosTipoComentario, estadoSiguiente, responsableLogueado);
        
        return nuevo;
    }

    public override string ponerEnLinea()
    {
        // TODO: Implementar lógica de transición
        return "";
    }
}