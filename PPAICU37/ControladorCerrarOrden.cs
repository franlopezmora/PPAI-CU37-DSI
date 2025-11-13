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
        public Empleado responsableLogueado { get; private set; }
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
        private DestinoNotificacion destinoNotificacion = DestinoNotificacion.Ambas;
        private string idSismografoSeleccionado;
        private string nombreEstadoActualFueraServicio;
        private DateTime fechaHoraActual;
        public enum DestinoNotificacion
        {
            Ambas,
            SoloMail,
            SoloPantalla
        }

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
            // Cargar datos desde la base de datos (orden correcto: primero las dependencias)
            _estadosPosibles = DataAccess.CargarEstados();
            
            var roles = DataAccess.CargarRoles();
            _empleadosRegistrados = DataAccess.CargarEmpleados(roles);
            _usuariosRegistrados = DataAccess.CargarUsuarios(_empleadosRegistrados);
            
            _tiposDeMotivoDisponibles = DataAccess.CargarMotivosTipo();
            
            _estaciones = DataAccess.CargarEstaciones();
            _sismografos = DataAccess.CargarSismografos(_estaciones, _estadosPosibles);
            
            // Cargar órdenes al final, después de tener empleados, estaciones y estados
            ordenes = DataAccess.CargarOrdenesInspeccion(_empleadosRegistrados, _estaciones, _estadosPosibles);
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

        public Empleado buscarUsuario(Sesion _sesionActual)
        {
            Empleado empleadoBuscado = _sesionActual.getUsuario();

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

                if (orden.esCompletamenteRealizada() && orden.esDeEmpleado(responsableLogueado))
                {
                    infoOrden = orden.getInfoOrdenInspeccion(_sismografos);
                    idSismografoSeleccionado = infoOrden[2]; // Asigna el ID del sismógrafo seleccionado

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

        public List<MotivoTipo> tomarObservacionIngresada(string observacion) // Paso 5 CU
        {
            observacionIngresada = observacion;
            return buscarTiposMotivos();
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

        public bool tomarConfirmacionRegistrada(DestinoNotificacion destino)
        {
            confirmacionRegistrada = true;

            bool validacionObs = validarObservacion();
            bool validacionMotivoCom = validarMotivoSeleccionado();
            Estado estadoCerrado = buscarEstadoCerrado();
            DateTime fechaActual = getFechaHoraActual();
            fechaHoraActual = fechaActual;

            // El estado actual del sismógrafo decide a qué estado transicionar (patrón State)
            bool exitoCerrar = cerrarOrden(validacionMotivoCom, validacionMotivoCom, estadoCerrado, fechaActual);

            if (exitoCerrar)
            {
                if (destino == DestinoNotificacion.Ambas || destino == DestinoNotificacion.SoloMail)
                    notificarViaMail();

                if (destino == DestinoNotificacion.Ambas || destino == DestinoNotificacion.SoloPantalla)
                    actualizarPantallaCCRS();
            }

            finCU(); // Limpia el estado del controlador al finalizar el CU

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

        public bool cerrarOrden(bool validacionObs, bool validacionMotivoCom, Estado estadoCerrado, DateTime fechaActual) // Lógica principal de cierre (Pasos 10, 11, 12 CU)
        {
            if (ordenSeleccionada == null || !validacionObs || !validacionMotivoCom)
            {
                return false; // Validaciones fallaron
            }

            if (estadoCerrado == null)
            {
                return false;
            }

            // Paso 11: Actualiza la orden de inspección a cerrada y registra la fecha y hora [cite: 2]
            ordenSeleccionada.registrarCierreOrden(fechaActual, observacionIngresada, estadoCerrado);

            // Paso 12: Actualiza al sismógrafo como fuera de servicio, asociando motivos, fecha, y responsable [cite: 2]
            // El estado actual del sismógrafo decide a qué estado transicionar (patrón State)
            ponerFueraServicio(fechaActual);

            return true;
        }

        public void ponerFueraServicio(DateTime fechaHora)
        {
            // El estado actual del sismógrafo decide a qué estado transicionar
            string nombreEstadoFueraServicio = ordenSeleccionada.ponerSismografoFueraDeServicio(fechaHora, listaMotivosTipoComentario, _sismografos, responsableLogueado);
            
            nombreEstadoActualFueraServicio = nombreEstadoFueraServicio;
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

            var listaTuplas = listaMotivosTipoComentario; // Lista de motivos y comentarios

            string motivosStr = string.Join("; ", listaTuplas.Select(t => $"{t.Item2.Descripcion}: {t.Item1}"));

            string cuerpoMail = $"Sismógrafo: {idSismografoSeleccionado}\n" +
                $"Nuevo Estado: {nombreEstadoActualFueraServicio}\n" +
                $"Fecha y Hora: {fechaHoraActual:g}\n" +
                $"Motivos: {motivosStr}\n" +
                $"Observación de Cierre Orden: {observacionIngresada}\n" +
                $"Cerrada por: {responsableLogueado}";

            InterfazMail pantallaMail = new InterfazMail();
            pantallaMail.enviarMail(
                    (string)sismografoSeleccionado.identificadorSismografo,
                    (string)nombreEstadoActualFueraServicio,
                    fechaHoraActual,
                    (List<Tuple<string, MotivoTipo>>)listaTuplas,
                    (string)observacionIngresada,
                    string.Join(", ", listadoMails)
                );
            pantallaMail.ShowDialog();

            MessageBox.Show($"Notificaciones enviadas (simulado a: {string.Join(", ", listadoMails)}). \n\nContenido:\n{cuerpoMail}", "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void actualizarPantallaCCRS()
        {
            if (ordenSeleccionada == null || idSismografoSeleccionado == null) 
                    return;

            //var estadoActualSismografo = sismografo.CambioEstadoActualSismografo.EstadoAsociado;
            if (ordenSeleccionada == null || estacionSeleccionada == null)
                    return;

            var motivosTuplas = listaMotivosTipoComentario; // Lista de motivos y comentarios

            PantallaCCRS pantallaCCRS = new PantallaCCRS();
            pantallaCCRS.actualizarMonitor(
                idSismografoSeleccionado,
                nombreEstadoActualFueraServicio,
                fechaHoraActual,
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
        }

        public void setearDestinoNotificacion(DestinoNotificacion destino)
        {
            destinoNotificacion = destino;
        }

    }
}
