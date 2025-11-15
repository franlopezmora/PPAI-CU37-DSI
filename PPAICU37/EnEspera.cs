using System;
using System.Collections.Generic;

namespace PPAICU37;

public class EnEspera : Estado
{
    public EnEspera() { }

    public override string nombreEstado => "En Espera";

    public override string registrarCertificacionTerreno()
    {
        // TODO: Implementar lógica de transición
        return "";
    }
}

