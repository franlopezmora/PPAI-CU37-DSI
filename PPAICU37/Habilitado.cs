using System;
using System.Collections.Generic;

namespace PPAICU37;

public class Habilitado : Estado
{
    public Habilitado() { }

    public override string nombreEstado => "Habilitado";

    public override string inspeccionar()
    {
        // TODO: Implementar lógica de transición
        return "";
    }
}

