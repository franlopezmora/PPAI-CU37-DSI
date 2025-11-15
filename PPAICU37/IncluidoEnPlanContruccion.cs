using System;
using System.Collections.Generic;

namespace PPAICU37;

public class IncluidoEnPlanContruccion : Estado
{
    public IncluidoEnPlanContruccion() { }

    public override string nombreEstado => "Incluido en Plan de Construcción";

    public override string enInstalacion()
    {
        // TODO: Implementar lógica de transición
        return "";
    }

    public override string ponerDisponible()
    {
        // TODO: Implementar lógica de transición
        return "";
    }
}

