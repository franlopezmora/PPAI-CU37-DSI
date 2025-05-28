using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class ControladorCerrarOrden
    {
        //private OrdenDeInspeccion _ordenTemporalmenteSeleccionadaEnGrilla;
        public OrdenDeInspeccion OrdenSeleccionada { get; private set; }
        public string ObservacionIngresada { get; private set; }
        public Empleado ResponsableLogueado { get; private set; }
        public List<OrdenDeInspeccion> Ordenes { get; private set; }
        public MotivoTipo MotivoSeleccionado { get; private set; }
        public string ComentarioMotivoIngresado { get; private set; }

        public bool confirmacionRegistrada { get; private set; } // Indica si el RI ha confirmado el cierre de la orden

        private List<Usuario> _usuariosRegistrados;
        private List<Empleado> _empleadosRegistrados;
        private List<Estado> _estadosPosibles;
        private List<MotivoTipo> _tiposDeMotivoDisponibles;
        private List<EstacionSismologica> _estaciones; // Lista de estaciones
        private Sesion _sesionActual;
        private EstacionSismologica _estacionSeleccionada;
        public List<Sismografo> _sismografos; // Lista de sismógrafos
        public List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario;


        public ControladorCerrarOrden()
        {
            listaMotivosTipoComentario = new List<Tuple<string, MotivoTipo>>(); // Inicializa la lista de motivos y comentarios
            _usuariosRegistrados = new List<Usuario>();
            _empleadosRegistrados = new List<Empleado>();
            _estadosPosibles = new List<Estado>();
            _tiposDeMotivoDisponibles = new List<MotivoTipo>();
            Ordenes = new List<OrdenDeInspeccion>(); // Esta se llenará dinámicamente
            _estaciones = new List<EstacionSismologica>();
            CargarDatosDePrueba();
            _sesionActual = null;
            ResponsableLogueado = null; // Inicialmente no hay usuario logueado
            _estacionSeleccionada = null; // No hay estación seleccionada al inicio

        }

        private void CargarDatosDePrueba()
        {
            // Estados
            var estadoPendiente = new Estado { NombreEstado = "Pendiente de Inspección", Ambito = "OrdenInspeccion" };
            var estadoRealizada = new Estado { NombreEstado = "Completamente Realizada", Ambito = "OrdenInspeccion" };
            var estadoCerrada = new Estado { NombreEstado = "Cerrada", Ambito = "OrdenInspeccion" };
            var estadoFueraServicio = new Estado { NombreEstado = "Fuera de Servicio", Ambito = "Sismografo" };
            var estadoOperativo = new Estado { NombreEstado = "Operativo", Ambito = "Sismografo" };
            _estadosPosibles.AddRange(new[] { estadoPendiente, estadoRealizada, estadoCerrada, estadoFueraServicio, estadoOperativo });

            // Roles
            var rolInspector = new Rol { NombreRol = "Inspector", DescripcionRol = "Realiza inspecciones" };
            var rolReparador = new Rol { NombreRol = "Responsable de Reparaciones", DescripcionRol = "Repara sismógrafos" };

            // Empleados y Usuarios
            var empleado1 = new Empleado { Nombre = "Juan", Apellido = "Perez", Mail = "juan.perez@example.com", Telefono = "123456", NombreUsuario = "jperez" };
            empleado1.Roles.Add(rolInspector);
            empleado1.Roles.Add(rolReparador); // Juan también puede ser reparador para simplificar
            var usuario1 = new Usuario { NombreUsuario = "jperez", Contrasena = "123", EmpleadoAsociado = empleado1 };
            _empleadosRegistrados.Add(empleado1);
            _usuariosRegistrados.Add(usuario1);

            var empleado2 = new Empleado { Nombre = "Ana", Apellido = "Lopez", Mail = "ana.lopez@example.com", Telefono = "789012", NombreUsuario = "alopez" };
            empleado2.Roles.Add(rolReparador);
            var usuario2 = new Usuario { NombreUsuario = "alopez", Contrasena = "456", EmpleadoAsociado = empleado2 };
            _empleadosRegistrados.Add(empleado2);
            _usuariosRegistrados.Add(usuario2);

            // MotivoTipos
            _tiposDeMotivoDisponibles.Add(new MotivoTipo (1, "Falla de sensor" ));
            _tiposDeMotivoDisponibles.Add(new MotivoTipo (2, "Problema de alimentación" ));
            _tiposDeMotivoDisponibles.Add(new MotivoTipo (3, "Mantenimiento programado"));
            _tiposDeMotivoDisponibles.Add(new MotivoTipo (4, "Otro"));

            // Estaciones y Sismógrafos
            var estacion1 = new EstacionSismologica { CodigoEstacion = "EST001", NombreEstacion = "Central Cordobesa" };

        //    CambioEstado cambioEstado1 = CambioEstado.crear(sismo1.FechaAdquisicion, null, estadoOperativo); // Estado inicial
            var sismo1 = new Sismografo { IdentificadorSismografo = "SISM001", NroSerie = "SN111", FechaAdquisicion = DateTime.Now.AddYears(-2), Estacion = estacion1 };
            
            sismo1.HistorialCambios.Add(CambioEstado.crear(sismo1.FechaAdquisicion, null, estadoOperativo)); // Estado inicial


          //  CambioEstado cambioEstado2 = CambioEstado.crear(sismo2.FechaAdquisicion, null, estadoOperativo); // Estado inicial
            var sismo2 = new Sismografo { IdentificadorSismografo = "SISM002", NroSerie = "SN222", FechaAdquisicion = DateTime.Now.AddYears(-1), Estacion = estacion1 };
            sismo2.HistorialCambios.Add(CambioEstado.crear(sismo2.FechaAdquisicion, null, estadoOperativo)); // Estado inicial

            // estacion1.Sismografos.AddRange(new[] { sismo1, sismo2 });   ELIMINAR ESTO URGENTE
            _estaciones.Add(estacion1);

            // Órdenes de Inspección de ejemplo
            var ordenesGlobales = new List<OrdenDeInspeccion>();
            ordenesGlobales.Add(new OrdenDeInspeccion { NumeroOrden = 101, FechaHoraInicio = DateTime.Now.AddDays(-10), FechaHoraFinalizacion = DateTime.Now.AddDays(-8), EstadoActual = estadoRealizada, Responsable = empleado1, EstacionSismologica = sismo1.Estacion, ObservacionCierre = "Muy mal"});
            ordenesGlobales.Add(new OrdenDeInspeccion { NumeroOrden = 102, FechaHoraInicio = DateTime.Now.AddDays(-5), FechaHoraFinalizacion = DateTime.Now.AddDays(-3), EstadoActual = estadoRealizada, Responsable = empleado1, EstacionSismologica = sismo2.Estacion });
            ordenesGlobales.Add(new OrdenDeInspeccion { NumeroOrden = 103, FechaHoraInicio = DateTime.Now.AddDays(-2), EstadoActual = estadoPendiente, Responsable = empleado1, EstacionSismologica = sismo2.Estacion });
            ordenesGlobales.Add(new OrdenDeInspeccion { NumeroOrden = 104, FechaHoraInicio = DateTime.Now.AddDays(-15), FechaHoraFinalizacion = DateTime.Now.AddDays(-12), EstadoActual = estadoRealizada, Responsable = empleado2 , EstacionSismologica = sismo1.Estacion});

            // Usuarios
            var usuarioLogueado = new Usuario
            {
                NombreUsuario = "jperez",
                Contrasena = "123",
                EmpleadoAsociado = empleado1
            };
            // Sismografos
            _sismografos = new List<Sismografo> { sismo1, sismo2 };

            // Asignar las órdenes a la lista que usa el controlador para las operaciones.
            // En un escenario real, estas se obtendrían de una fuente de datos.
            this.Ordenes = ordenesGlobales;
        }

        public IReadOnlyList<Sismografo> Sismografos
            => _sismografos.AsReadOnly();

        public List<OrdenDeInspeccion> tomarOpcionSeleccionada(string opcion) // Invocado al inicio del CU
        {
            if (opcion == "CERRAR_ORDEN_INSPECCION")
            {
                // Simular login (Paso 2 del CU)
                // En una app real, la UI de login llamaría a buscarUsuario o iniciarSesion.

                var usuarioLogueado = login("jperez", "123"); // Simula login de jperez

                if (usuarioLogueado != null)
                {
                    _sesionActual = new Sesion();
                    _sesionActual.Iniciar(usuarioLogueado);

                    ResponsableLogueado = buscarUsuario(_sesionActual);
                    // Console.WriteLine($"DEBUG: Usuario {ResponsableLogueado.NombreUsuario} logueado."); // Para depuración

                    List<OrdenDeInspeccion> OrdenesFiltradas = buscarOrdenInspeccion(); // Carga las órdenes elegibles

                    return OrdenesFiltradas;
                }
                return null;
            }
            return null;
        }


        public Usuario login(string nombreUsuario, string contrasena)
        {
            return _usuariosRegistrados.FirstOrDefault(u => u.NombreUsuario == nombreUsuario && u.Contrasena == contrasena);
        }

        public Empleado buscarUsuario(Sesion _sesionActual)
        {
            Empleado empleadoBuscado = _sesionActual.getUsuario().getEmpleado();

            return empleadoBuscado;

        }

        public List<OrdenDeInspeccion> buscarOrdenInspeccion() // Paso 2 CU
        {
            if (ResponsableLogueado == null)
            {
                Ordenes = new List<OrdenDeInspeccion>(); // No hay usuario, no hay órdenes
                return Ordenes;
            }

            // Filtra órdenes "completamente realizadas" [cite: 2]
            // El CU dice "todas las órdenes de inspección del RI que están en estado completamente realizadas" [cite: 2]
            // Asumimos que el RI es el ResponsableLogueado.

            List<OrdenDeInspeccion> OrdenesFiltradas = new List<OrdenDeInspeccion>();

            for (int i = 0; i < Ordenes.Count; i++)
            {
                OrdenDeInspeccion orden = Ordenes[i]; // Obtiene la orden actual

                if (Ordenes[i].EstadoActual.esCompletamenteRealizada() && Ordenes[i].esDeEmpleado(ResponsableLogueado)) 
                    {OrdenesFiltradas.Add(orden);}// Verifica si la orden es del empleado logueado

                orden.getInfoOrdenInspeccion(_sismografos);
            }

            ordenarPorFecha(OrdenesFiltradas);

            return OrdenesFiltradas; // Retorna la lista filtrada
           
        }

        public void ordenarPorFecha(List<OrdenDeInspeccion> OrdenesFiltradas)
        {
            OrdenesFiltradas = OrdenesFiltradas.OrderByDescending(o => o.FechaHoraFinalizacion ?? DateTime.MinValue).ToList();
        }

        public void tomarOrdenSeleccionada(OrdenDeInspeccion orden) // Paso 3 CU [cite: 2]
        {
            OrdenSeleccionada = orden;
            listaMotivosTipoComentario.Clear();
            ObservacionIngresada = string.Empty;
            ComentarioMotivoIngresado = string.Empty;
            MotivoSeleccionado = null;
            // Console.WriteLine($"DEBUG: Orden {orden?.NumeroOrden} seleccionada."); // Para depuración
        }

        public void tomarObservacionIngresada(string observacion) // Paso 5 CU [cite: 2]
        {
            ObservacionIngresada = observacion;
        }

        public List<MotivoTipo> buscarTiposMotivos() // Paso 6 CU (cargar tipos para UI) [cite: 2]
        {

            List<MotivoTipo> listaMotivosEncontrados = new List<MotivoTipo>();
            List<MotivoTipo> motivos = _tiposDeMotivoDisponibles;

            for (int i = 0; i < motivos.Count; i++)
            {
                MotivoTipo motivo = motivos[i];
                MotivoTipo motivoEncontrado = motivo.buscarMotivoTipo();
                listaMotivosEncontrados.Add(motivoEncontrado);
                // Console.WriteLine($"DEBUG: MotivoTipo {i + 1}: {motivos[i].Descripcion}"); // Para depuración
            }
           
            return listaMotivosEncontrados;
        }


        public void tomarMotivoSeleccionado(MotivoTipo motivoTipo) // Paso 7 CU (selección de tipo) [cite: 2]
        {
            MotivoSeleccionado = motivoTipo;
        }

        public void tomarComentarioIngresado(string comentario) // Paso 7 CU (ingreso de comentario para motivo) [cite: 2]
        {
            ComentarioMotivoIngresado = comentario;
        }

        public List<Tuple<string, MotivoTipo>> agregarMotivoALista() // Acción de UI para agregar un motivo a la lista temporal
        {
            listaMotivosTipoComentario.Add(new Tuple<string, MotivoTipo>(ComentarioMotivoIngresado, MotivoSeleccionado));
            return listaMotivosTipoComentario;

            //if (MotivoSeleccionado != null && !string.IsNullOrWhiteSpace(ComentarioMotivoIngresado))
            //{
            //    var nuevoMotivo = new MotivoFueraServicio(MotivoSeleccionado, ComentarioMotivoIngresado);
            //    MotivosAgregados.Add(nuevoMotivo);
            //    ComentarioMotivoIngresado = string.Empty; // Limpiar para el próximo
            //    return true;
            //}
            //return false;
        }

        public bool tomarConfirmacionRegistrada() // Paso 9 CU: RI confirma cierre [cite: 2]
        {
            confirmacionRegistrada = true; // Marca que el RI ha confirmado el cierre de la orden
            bool validacionObs = validarObservacion();
            bool validacionMotivoCom = validarMotivoSeleccionado();
            Estado estadoCerrado = buscarEstadoCerrado();
            Estado estadoFueraServicio = buscarEstadoFueraServicio();
            DateTime fechaActual = getFechaHoraActual();
            bool exitoCerrar = cerrarOrden(validacionMotivoCom, validacionMotivoCom, estadoCerrado, estadoFueraServicio, fechaActual);
            return exitoCerrar;
        }

        public bool validarObservacion() // Paso 10 CU (parte 1) [cite: 2]
        {
            return !string.IsNullOrWhiteSpace(ObservacionIngresada);
        }

        public bool validarMotivoSeleccionado() // Paso 10 CU (parte 2) [cite: 2]
        {
            return listaMotivosTipoComentario.Any();
        }

        public Estado buscarEstadoCerrado()
        {
            return _estadosPosibles.FirstOrDefault(e => e.NombreEstado == "Cerrada" && e.esAmbitoOrden());
        }
        public Estado buscarEstadoFueraServicio()
        {
            return _estadosPosibles.FirstOrDefault(e => e.NombreEstado == "Fuera de Servicio" && e.esAmbitoSismografo());
        }

        public DateTime getFechaHoraActual()
        {
            return DateTime.Now;
        }

        public bool cerrarOrden(bool validacionObs, bool validacionMotivoCom, Estado estadoCerrado, Estado estadoFueraServicio, DateTime fechaActual) // Lógica principal de cierre (Pasos 10, 11, 12 CU) [cite: 2]
        {
            if (OrdenSeleccionada == null || !validacionObs || !validacionMotivoCom)
            {
                // Console.WriteLine("Error: Validación fallida antes de cerrar orden."); // Para depuración
                return false; // Validaciones fallaron
            }

            if (estadoCerrado == null || estadoFueraServicio == null)
            {
                // Console.WriteLine("Error: Estados críticos no configurados (Cerrada/Fuera de Servicio)."); // Para depuración
                return false;
            }

            // Paso 11: Actualiza la orden de inspección a cerrada y registra la fecha y hora [cite: 2]
            OrdenSeleccionada.registrarCierreOrden(fechaActual, ObservacionIngresada, estadoCerrado);

            // Paso 12: Actualiza al sismógrafo como fuera de servicio, asociando motivos, fecha, y responsable [cite: 2]

            ponerFueraServicio(estadoFueraServicio, fechaActual); // Delegar lógica a la entidad OrdenDeInspeccion

            //OrdenSeleccionada.ponerSismografoFueraDeServicio(fechaActual, MotivosAgregados, estadoFueraServicio); VIEJO CÓDIGO, NO USAR. ELIMINAR

            // El ResponsableLogueado es el RI que realiza el cierre.

            // Console.WriteLine($"DEBUG: Orden {OrdenSeleccionada.NumeroOrden} cerrada exitosamente."); // Para depuración
            return true;
        }

        // El método ponerFueraServicio() del diagrama de clases del controlador [cite: 1]
        // es invocado conceptualmente a través de OrdenDeInspeccion.ponerSismografoFueraDeServicio()
        public void ponerFueraServicio(Estado estadoFueraServicio, DateTime fechaActual)
        {
            OrdenSeleccionada.ponerSismografoFueraDeServicio(getFechaHoraActual(), listaMotivosTipoComentario, estadoFueraServicio, _sismografos);

            // Console.WriteLine($"DEBUG: Método ponerFueraServicio del controlador llamado (lógica delegada)."); // Para depuración
        }

        // Paso 13 CU: Notificaciones [cite: 2]
        public List<string> obtenerEmailsResponsablesReparacion()
        {
            return _empleadosRegistrados
                .Where(e => e.sosResponsableDeReparacion())
                .Select(e => e.getEmail())
                .Distinct()
                .ToList();
        }

        // notificarViaMail() del diagrama de clases del controlador [cite: 1]
        public string construirMensajeNotificacion()
        {
            if (OrdenSeleccionada == null || OrdenSeleccionada.EstacionSismologica.buscarIdSismografo(_sismografos) == null) return string.Empty;


            Sismografo sismografo = OrdenSeleccionada.EstacionSismologica.buscarSismografo(_sismografos);
            var cambioEstadoActualSismografo = sismografo?.HistorialCambios.FirstOrDefault(h => h.esActual());

            var listaTuplas = listaMotivosTipoComentario; // Lista de motivos y comentarios
            string motivosStr = string.Join("; ", listaTuplas.Select(t => $"{t.Item2.Descripcion}: {t.Item1}"));

            return $"Sismógrafo: {sismografo.IdentificadorSismografo}\n" +
                   $"Nuevo Estado: {cambioEstadoActualSismografo?.EstadoAsociado.NombreEstado}\n" +
                   $"Fecha y Hora: {getFechaHoraActual():g}\n" +
                   $"Motivos: {motivosStr}\n" +
                   $"Observación de Cierre Orden: {ObservacionIngresada}\n" +
                   $"Cerrada por: {ResponsableLogueado?.Nombre} {ResponsableLogueado?.Apellido}";
        }

        // actualizarPantalla() del diagrama de clases del controlador [cite: 1]
        // Este es un concepto, la actualización real la hacen las pantallas al pedir datos.
        // Para CCRS, se preparan datos específicos.
        public object[] getDatosParaPantallaCCRS() // Adaptado de actualizarPantallaCCRS
        {
            if (OrdenSeleccionada == null || OrdenSeleccionada.EstacionSismologica.buscarIdSismografo(_sismografos) == null) return null;

            //var estadoActualSismografo = sismografo.CambioEstadoActualSismografo.EstadoAsociado;
            if (OrdenSeleccionada == null || OrdenSeleccionada.EstacionSismologica == null)
                    return null;

               // Busco el Sismógrafo real desde la estación
            var sismografo = OrdenSeleccionada
                    .EstacionSismologica
                    .buscarSismografo(_sismografos);

            CambioEstado cambioEstadoActualSismografo = sismografo.HistorialCambios.FirstOrDefault(h => h.esActual());
            var estadoActual = cambioEstadoActualSismografo.EstadoAsociado;


            var motivosTuplas = listaMotivosTipoComentario; // Lista de motivos y comentarios

            return new object[] {
            sismografo?.IdentificadorSismografo,
            estadoActual?.NombreEstado ?? "N/A",
            getFechaHoraActual(),
            motivosTuplas, // Copia de la lista
            ObservacionIngresada,
            _sismografos.AsReadOnly()
        };
        }

        public void finCU() // fin C.U. del diagrama [cite: 1]
        {
            OrdenSeleccionada = null;
            ObservacionIngresada = string.Empty;
            MotivoSeleccionado = null;
   //         MotivosAgregados.Clear();
            listaMotivosTipoComentario.Clear();
            ComentarioMotivoIngresado = string.Empty;
            buscarOrdenInspeccion();
            // ResponsableLogueado y SesionActual podrían persistir si el usuario sigue en la app.
            // Ordenes se recargaría con buscarOrdenInspeccion() si es necesario para una nueva operación.
            // Console.WriteLine("DEBUG: Fin del Caso de Uso. Estado del controlador parcialmente reseteado."); // Para depuración
        }
    }
}
