using System;
using System.Collections.Generic;

namespace PPAICU37;

public class FueraDeServicio : Estado
{
    public FueraDeServicio() { }

    public override string nombreEstado => "Fuera de Servicio";

    public override bool esFueraDeServicio()
    {
        return true;
    }
    
    // No sobrescribir ponerSismografoFueraDeServicio porque ya está en Fuera de Servicio
    // El método por defecto lanzará excepción, o podemos permitir que se quede en el mismo estado
    // Por ahora, lanzará excepción si se intenta poner fuera de servicio cuando ya lo está

    public override string ponerEnLinea()
    {
        // TODO: Implementar lógica de transición
        return "";
    }

    public override string darBaja()
    {
        // TODO: Implementar lógica de transición
        return "";
    }
}