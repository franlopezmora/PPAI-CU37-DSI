using System;
using System.Collections.Generic;

namespace PPAICU37;

public class EnInstalacion : Estado
{
    public EnInstalacion() { }

    public override string nombreEstado => "En Instalación";

    public override string ponerEnLinea()
    {
        // TODO: Implementar lógica de transición
        return "";
    }

    public override string realizarReclamo()
    {
        // TODO: Implementar lógica de transición
        return "";
    }
}

