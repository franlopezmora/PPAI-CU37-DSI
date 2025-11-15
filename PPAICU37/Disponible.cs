using System;
using System.Collections.Generic;

namespace PPAICU37;

public class Disponible : Estado
{
    public Disponible() { }

    public override string nombreEstado => "Disponible";

    public override string incluirEnPlan()
    {
        // TODO: Implementar lógica de transición
        return "";
    }

    public override string reservar()
    {
        // TODO: Implementar lógica de transición
        return "";
    }
}

