using System;
using System.Collections.Generic;

namespace PPAICU37;

public class HabilitadoASerIncluido : Estado
{
    public HabilitadoASerIncluido() { }

    public override string nombreEstado => "Habilitado a Ser Incluido";

    public override string incluirEnPlan()
    {
        // TODO: Implementar lógica de transición
        return "";
    }
}

