using System;
using System.Collections.Generic;

namespace PPAICU37;

public class Reclamado : Estado
{
    public Reclamado() { }

    public override string nombreEstado => "Reclamado";

    public override string enInstalacion()
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

