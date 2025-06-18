using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Pipes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PPAICU37
{
    public class ControladorCerrarOrden
    {
        public OrdenDeInspeccion ordenSeleccionada { get; private set; }
        public Sismografo sismografoSeleccionado { get; private set; }
        public EstacionSismologica estacionSeleccionada { get; private set; }
        public string observacionIngresada { get; private set; }
        public Usuario responsableLogueado { get; private set; }
        public List<OrdenDeInspeccion> ordenes { get; private set; }
        public MotivoTipo motivoSeleccionado { get; private set; }
        public string comentarioMotivoIngresado { get; private set; }
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
            ordenes = new List<OrdenDeInspeccion>(); // Esta se llenará dinámicamente
            _estaciones = new List<EstacionSismologica>();
            cargarDatosDePrueba();
            _sesionActual = null;
            responsableLogueado = null; // Inicialmente no hay usuario logueado
            _estacionSeleccionada = null; // No hay estación seleccionada al inicio

        }

        private void cargarDatosDePrueba()
        {
            // Estados
            var estadoPendiente = new Estado { nombreEstado = "Pendiente de Inspección", ambito = "OrdenInspeccion" };
            var estadoRealizada = new Estado { nombreEstado = "Completamente Realizada", ambito = "OrdenInspeccion" };
            var estadoCerrada = new Estado { nombreEstado = "Cerrada", ambito = "OrdenInspeccion" };
            var estadoFueraServicio = new Estado { nombreEstado = "Fuera de Servicio", ambito = "Sismografo" };
            var estadoOperativo = new Estado { nombreEstado = "Operativo", ambito = "Sismografo" };
            _estadosPosibles.AddRange(new[] { estadoPendiente, estadoRealizada, estadoCerrada, estadoFueraServicio, estadoOperativo });

            // Roles
            var rolInspector = new Rol { NombreRol = "Inspector", DescripcionRol = "Realiza inspecciones" };
            var rolReparador = new Rol { NombreRol = "Responsable de Reparaciones", DescripcionRol = "Repara sismógrafos" };

            // Empleados y Usuarios
            var empleado1 = new Empleado { nombre = "Juan", apellido = "Perez", mail = "juan.perez@example.com", telefono = "123456" };
        
            empleado1.Rol = rolReparador;
            var usuario1 = new Usuario { nombreUsuario = "jperez", contrasena = "123", Empleado = empleado1 };
            _empleadosRegistrados.Add(empleado1);
            _usuariosRegistrados.Add(usuario1);

            var empleado2 = new Empleado { nombre = "Ana", apellido = "Lopez", mail = "ana.lopez@example.com", telefono = "789012" };
            empleado2.Rol = rolReparador;
            var usuario2 = new Usuario { nombreUsuario = "alopez", contrasena = "456", Empleado = empleado2 };
            _empleadosRegistrados.Add(empleado2);
            _usuariosRegistrados.Add(usuario2);

            // MotivoTipos
            _tiposDeMotivoDisponibles.Add(new MotivoTipo (1, "Falla de sensor" ));
            _tiposDeMotivoDisponibles.Add(new MotivoTipo (2, "Problema de alimentación" ));
            _tiposDeMotivoDisponibles.Add(new MotivoTipo (3, "Mantenimiento programado"));
            _tiposDeMotivoDisponibles.Add(new MotivoTipo (4, "Otro"));

            // Estaciones y Sismógrafos
            var estacion1 = new EstacionSismologica { codigoEstacion = "EST001", nombreEstacion = "Central Cordoba" };

        //    CambioEstado cambioEstado1 = CambioEstado.crear(sismo1.FechaAdquisicion, null, estadoOperativo); // Estado inicial
            var sismo1 = new Sismografo { identificadorSismografo = "SISM001", nroSerie = "SN111", fechaAdquisicion = DateTime.Now.AddYears(-2), EstacionSismologica = estacion1 };
            
            sismo1.CambioEstado.Add(CambioEstado.crear(sismo1.fechaAdquisicion, null, estadoOperativo)); // Estado inicial


          //  CambioEstado cambioEstado2 = CambioEstado.crear(sismo2.FechaAdquisicion, null, estadoOperativo); // Estado inicial
            var sismo2 = new Sismografo { identificadorSismografo = "SISM002", nroSerie = "SN222", fechaAdquisicion = DateTime.Now.AddYears(-1), EstacionSismologica = estacion1 };
            sismo2.CambioEstado.Add(CambioEstado.crear(sismo2.fechaAdquisicion, null, estadoOperativo)); // Estado inicial

            // estacion1.Sismografos.AddRange(new[] { sismo1, sismo2 });   ELIMINAR ESTO URGENTE
            _estaciones.Add(estacion1);

            // Órdenes de Inspección de ejemplo
            var ordenesGlobales = new List<OrdenDeInspeccion>();
            ordenesGlobales.Add(new OrdenDeInspeccion { numeroOrden = 101, fechaHoraInicio = DateTime.Now.AddDays(-10), fechaHoraFinalizacion = DateTime.Now.AddDays(-8), Estado = estadoRealizada, Empleado = empleado1, EstacionSismologica = sismo1.EstacionSismologica, observacionCierre = "Muy mal"});
            ordenesGlobales.Add(new OrdenDeInspeccion { numeroOrden = 102, fechaHoraInicio = DateTime.Now.AddDays(-5), fechaHoraFinalizacion = DateTime.Now.AddDays(-3), Estado = estadoRealizada, Empleado = empleado1, EstacionSismologica = sismo2.EstacionSismologica });
            ordenesGlobales.Add(new OrdenDeInspeccion { numeroOrden = 103, fechaHoraInicio = DateTime.Now.AddDays(-2), Estado = estadoPendiente, Empleado = empleado1, EstacionSismologica = sismo2.EstacionSismologica });
            ordenesGlobales.Add(new OrdenDeInspeccion { numeroOrden = 104, fechaHoraInicio = DateTime.Now.AddDays(-15), fechaHoraFinalizacion = DateTime.Now.AddDays(-12), Estado = estadoRealizada, Empleado = empleado2 , EstacionSismologica = sismo1.EstacionSismologica});

            // Usuarios
            var usuarioLogueado = new Usuario
            {
                nombreUsuario = "jperez",
                contrasena = "123",
                Empleado = empleado1
            };
            // Sismografos
            _sismografos = new List<Sismografo> { sismo1, sismo2 };

            // Asignar las órdenes a la lista que usa el controlador para las operaciones.
            // En un escenario real, estas se obtendrían de una fuente de datos.
            this.ordenes = ordenesGlobales;
        }

        public IReadOnlyList<Sismografo> Sismografos
            => _sismografos.AsReadOnly();

        public DataTable tomarOpcionSeleccionada(string opcion) // Invocado al inicio del CU
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

                    responsableLogueado = buscarUsuario(_sesionActual);

                    DataTable tablaGenerada = buscarOrdenInspeccion(ordenes); // Carga las órdenes elegibles

                    return tablaGenerada;
                }
                return null;
            }
            return null;
        }

        public Usuario login(string nombreUsuario, string contrasena)
        {
            return _usuariosRegistrados.FirstOrDefault(u => u.nombreUsuario == nombreUsuario && u.contrasena == contrasena);
        }

        public Usuario buscarUsuario(Sesion _sesionActual)
        {
            Usuario empleadoBuscado = _sesionActual.getUsuario();

            return empleadoBuscado;
        }

        public DataTable buscarOrdenInspeccion(List<OrdenDeInspeccion> OrdenesDeInspeccion)
        {
            if (responsableLogueado == null)
            {
                return null;
            }

            string[] infoOrden = new string[3]; // Inicializa el array para evitar errores de referencia nula
            List<string[]> listaResultado = new List<string[]>(); // Lista para almacenar los resultados
            string[] resultado = new string[4]; // Array para almacenar los resultados de cada orden

            for (int i = 0; i < OrdenesDeInspeccion.Count; i++)
            {
                OrdenDeInspeccion orden = ordenes[i]; // Obtiene la orden actual

                if (orden.Estado.esCompletamenteRealizada() && orden.esDeEmpleado(responsableLogueado))
                {
                    infoOrden = orden.getInfoOrdenInspeccion(_sismografos);

                    int NumeroOrden;
                    string FechaHoraFinalizacion;
                    string EstacionSismologica;
                    string IdSismografo; ;

                    resultado = new string[] { infoOrden[0], infoOrden[3], infoOrden[1], infoOrden[2] };
                    listaResultado.Add(resultado); // Agrega la información de la orden a la lista de resultados
                }
            }
            listaResultado = listaResultado.OrderBy(r => r[3]).ToList();
            DataTable tablaGenerada = generarTablaOrdenes(listaResultado);

            return tablaGenerada; // Retorna la lista filtrada
        }

        public DataTable generarTablaOrdenes(List<string[]> listaResultado)
        {
            // 1) Construyo un DataTable con todas las columnas que quiero ver
            var dt = new DataTable();
            dt.Columns.Add("Número Orden", typeof(int));
            dt.Columns.Add("Fecha Finalización", typeof(string));
            dt.Columns.Add("Estación Sismológica", typeof(string));
            dt.Columns.Add("Id Sismógrafo", typeof(string));

            // 2) Lleno fila a fila
            foreach (var o in listaResultado)
            {
                dt.Rows.Add(
                    o[0],
                    o[1],
                    o[2],
                    o[3]
                );
            }
            return dt;
        }

        public void ordenarPorFecha(List<OrdenDeInspeccion> OrdenesFiltradas)
        {
            OrdenesFiltradas = OrdenesFiltradas.OrderByDescending(o => o.fechaHoraFinalizacion ?? DateTime.MinValue).ToList();
        }

        public void tomarOrdenSeleccionada(OrdenDeInspeccion orden) // Paso 3 CU
        {
            ordenSeleccionada = orden;

            estacionSeleccionada = orden?.EstacionSismologica;
            sismografoSeleccionado = estacionSeleccionada?.buscarSismografo(_sismografos);

            listaMotivosTipoComentario.Clear();
            observacionIngresada = string.Empty;
            comentarioMotivoIngresado = string.Empty;
            motivoSeleccionado = null;
        }

        public void tomarObservacionIngresada(string observacion) // Paso 5 CU
        {
            observacionIngresada = observacion;
        }

        public List<MotivoTipo> buscarTiposMotivos() // Paso 6 CU (cargar tipos para UI)
        {

            List<MotivoTipo> listaMotivosEncontrados = new List<MotivoTipo>();
            List<MotivoTipo> motivos = _tiposDeMotivoDisponibles;

            for (int i = 0; i < motivos.Count; i++)
            {
                MotivoTipo motivo = motivos[i];
                MotivoTipo motivoEncontrado = motivo.buscarMotivoTipo();
                listaMotivosEncontrados.Add(motivoEncontrado);
            }
           
            return listaMotivosEncontrados;
        }

        public void tomarTipoMotivoSeleccionado(MotivoTipo motivoTipo) // Paso 7 CU (selección de tipo)
        {
            motivoSeleccionado = motivoTipo;
        }

        public void tomarComentarioIngresado(string comentario) // Paso 7 CU (ingreso de comentario para motivo)
        {
            comentarioMotivoIngresado = comentario;
        }

        public List<Tuple<string, MotivoTipo>> agregarMotivoALista() // Acción de UI para agregar un motivo a la lista temporal
        {
            listaMotivosTipoComentario.Add(new Tuple<string, MotivoTipo>(comentarioMotivoIngresado, motivoSeleccionado));
            return listaMotivosTipoComentario;
        }

        public bool tomarConfirmacionRegistrada() // Paso 9 CU: RI confirma cierre
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

        public bool validarObservacion() // Paso 10 CU (parte 1)
        {
            return !string.IsNullOrWhiteSpace(observacionIngresada);
        }

        public bool validarMotivoSeleccionado() // Paso 10 CU (parte 2)
        {
            return listaMotivosTipoComentario.Any();
        }

        public Estado buscarEstadoCerrado()
        {
            return _estadosPosibles.FirstOrDefault(e => e.esAmbitoOrden() && e.esCerrado());
        }

        public Estado buscarEstadoFueraServicio()
        {
            return _estadosPosibles.FirstOrDefault(e => e.esAmbitoSismografo() && e.esFueraDeServicio());
        }

        public DateTime getFechaHoraActual()
        {
            return DateTime.Now;
        }

        public bool cerrarOrden(bool validacionObs, bool validacionMotivoCom, Estado estadoCerrado, Estado estadoFueraServicio, DateTime fechaActual) // Lógica principal de cierre (Pasos 10, 11, 12 CU)
        {
            if (ordenSeleccionada == null || !validacionObs || !validacionMotivoCom)
            {
                return false; // Validaciones fallaron
            }

            if (estadoCerrado == null || estadoFueraServicio == null)
            {
                return false;
            }

            // Paso 11: Actualiza la orden de inspección a cerrada y registra la fecha y hora [cite: 2]
            ordenSeleccionada.registrarCierreOrden(fechaActual, observacionIngresada, estadoCerrado);

            // Paso 12: Actualiza al sismógrafo como fuera de servicio, asociando motivos, fecha, y responsable [cite: 2]

            ponerFueraServicio(estadoFueraServicio, fechaActual); // Delegar lógica a la entidad OrdenDeInspeccion

            return true;
        }

        public void ponerFueraServicio(Estado estadoFueraServicio, DateTime fechaActual)
        {
            ordenSeleccionada.ponerSismografoFueraDeServicio(getFechaHoraActual(), listaMotivosTipoComentario, estadoFueraServicio, _sismografos);
            notificarViaMail();
            actualizarPantallaCCRS();
        }

        // Paso 13 CU: Notificaciones
        public void notificarViaMail()
        {
            List<string> EmailsResponsablesReparacion = new List<string>();


            List<string> listadoMails = new List<string>();
            string mail = "";

            foreach (Empleado empleado in _empleadosRegistrados)
                {
                if (empleado.sosResponsableDeReparacion()) {
                    mail = empleado.getEmail();
                }

                listadoMails.Add(mail);
                }

            if (ordenSeleccionada == null || sismografoSeleccionado == null) return;

            var cambioEstadoActualSismografo = sismografoSeleccionado?.CambioEstado.FirstOrDefault(h => h.esActual());

            var listaTuplas = listaMotivosTipoComentario; // Lista de motivos y comentarios

            string motivosStr = string.Join("; ", listaTuplas.Select(t => $"{t.Item2.Descripcion}: {t.Item1}"));

            string cuerpoMail = $"Sismógrafo: {sismografoSeleccionado?.identificadorSismografo}\n" +
                $"Nuevo Estado: {cambioEstadoActualSismografo?.Estado.nombreEstado}\n" +
                $"Fecha y Hora: {getFechaHoraActual():g}\n" +
                $"Motivos: {motivosStr}\n" +
                $"Observación de Cierre Orden: {observacionIngresada}\n" +
                $"Cerrada por: {responsableLogueado?.Empleado.nombre} {responsableLogueado?.Empleado.apellido}";

            InterfazMail pantallaMail = new InterfazMail();
            pantallaMail.CargarDatos(
                    (string)sismografoSeleccionado.identificadorSismografo,
                    (string)cambioEstadoActualSismografo.Estado.nombreEstado,
                    (DateTime)getFechaHoraActual(),
                    (List<Tuple<string, MotivoTipo>>)listaTuplas,
                    (string)observacionIngresada,
                    string.Join(", ", listadoMails)
                );
            pantallaMail.ShowDialog();

            MessageBox.Show($"Notificaciones enviadas (simulado a: {string.Join(", ", listadoMails)}). \n\nContenido:\n{cuerpoMail}", "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public void actualizarPantallaCCRS()
        {
            if (ordenSeleccionada == null || sismografoSeleccionado.getIdSismografo() == null) 
                    return;

            //var estadoActualSismografo = sismografo.CambioEstadoActualSismografo.EstadoAsociado;
            if (ordenSeleccionada == null || estacionSeleccionada == null)
                    return;

               // Busco el Sismógrafo real desde la estación
            var IdSismografo = ordenSeleccionada
                    .EstacionSismologica
                    .buscarIdSismografo(_sismografos);

            string idSismografo = sismografoSeleccionado.getIdSismografo();

            CambioEstado cambioEstadoActualSismografo = sismografoSeleccionado.CambioEstado.FirstOrDefault(h => h.esActual());
            var estadoActual = cambioEstadoActualSismografo.Estado.nombreEstado ?? "N/A";


            var motivosTuplas = listaMotivosTipoComentario; // Lista de motivos y comentarios

            PantallaCCRS pantallaCCRS = new PantallaCCRS();
            pantallaCCRS.CargarDatos(
                (string)IdSismografo,
                (string)estadoActual,
                (DateTime)getFechaHoraActual(),
                (List<Tuple<string, MotivoTipo>>)motivosTuplas,
                (string)observacionIngresada,
                (IEnumerable<Sismografo>)_sismografos.AsReadOnly() // Pasar la lista de sismógrafos para mostrar en CCRS si es necesario

            );
            pantallaCCRS.ShowDialog();
        }

        public void finCU() // fin C.U. del diagrama
        {
            ordenSeleccionada = null;
            sismografoSeleccionado = null;
            estacionSeleccionada = null;
            observacionIngresada = string.Empty;
            motivoSeleccionado = null;
   //         MotivosAgregados.Clear();
            listaMotivosTipoComentario.Clear();
            comentarioMotivoIngresado = string.Empty;
            buscarOrdenInspeccion(ordenes);
            // ResponsableLogueado y SesionActual podrían persistir si el usuario sigue en la app.
            // Ordenes se recargaría con buscarOrdenInspeccion() si es necesario para una nueva operación.
            // Console.WriteLine("DEBUG: Fin del Caso de Uso. Estado del controlador parcialmente reseteado."); // Para depuración
        }
    }
}
