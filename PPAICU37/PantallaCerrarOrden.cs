using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PPAICU37
{
    public partial class PantallaCerrarOrden : Form
    {
        private ControladorCerrarOrden _controlador;
        private OrdenDeInspeccion _ordenTemporalmenteSeleccionadaEnGrilla; // Para guardar la selección de la grilla antes de confirmar con el botón


        public PantallaCerrarOrden()
        {
            InitializeComponent();
            _controlador = new ControladorCerrarOrden();
            ConfigurarEstadoInicialUI();
        }

        private void ConfigurarEstadoInicialUI()
        {
            HabilitarSeccionSeleccionOrden(false); // dgvOrdenesInspeccion y btnSeleccionarOrden
            HabilitarSeccionObservacion(false);    // txtObservacionCierre y btnConfirmarObservacion
            HabilitarSeccionMotivos(false);       // cmbTiposMotivo, txtComentarioMotivo, btnAgregarMotivo, dgvMotivosFueraServicio

            btnConfirmar.Enabled = false;
            btnCancelar.Enabled = false; // Se habilita después del login
            btnIniciarSesion.Enabled = true;
        }

        private void HabilitarSeccionSeleccionOrden(bool habilitar)
        {
            grillaOrdenes.Enabled = habilitar;
            // btnSeleccionarOrden se habilita solo cuando hay una fila seleccionada en la grilla
            if (!habilitar) btnSeleccionarOrden.Enabled = false;
        }

        private void HabilitarSeccionObservacion(bool habilitar)
        {
            txtObservacion.Enabled = habilitar;
            btnConfirmarObservacion.Enabled = habilitar;
            if (habilitar)
            {
                // solicitarIngresoObservacion() - Se le da foco al txt
                txtObservacion.Focus();
            }
        }

        private void HabilitarSeccionMotivos(bool habilitar)
        {
            cmbTiposMotivo.Enabled = habilitar;
            txtComentario.Enabled = habilitar;
            btnAgregarMotivo.Enabled = habilitar;
            grillaMotivos.Enabled = habilitar;
        }

        private void btnIniciarSesionSimulado_Click(object sender, EventArgs e)
        {
      //      MessageBox.Show($"Login simulado exitoso para: {_controlador.ResponsableLogueado.NombreUsuario}", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            HabilitarSeccionSeleccionOrden(true);
            List<OrdenDeInspeccion> OrdenesFiltradas = _controlador.tomarOpcionSeleccionada("CERRAR_ORDEN_INSPECCION");
            MessageBox.Show($"Login simulado exitoso para: {_controlador.ResponsableLogueado.NombreUsuario}", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnCancelar.Enabled = true;
            mostrarOrdenes(OrdenesFiltradas);
            //     cargarTiposMotivoComboBox(); // Podemos cargarlos aquí una vez
            btnIniciarSesion.Enabled = false;

        }

        private void mostrarOrdenes(List<OrdenDeInspeccion> OrdenesFiltradas)
        {
            grillaOrdenes.DataSource = null;
            _ordenTemporalmenteSeleccionadaEnGrilla = null; // Resetear la selección temporal
            btnSeleccionarOrden.Enabled = false; // Deshabilitar hasta nueva selección en grilla

            if (OrdenesFiltradas != null)
            {
                grillaOrdenes.DataSource = OrdenesFiltradas;
            }
            else
            {
                MessageBox.Show("No hay órdenes de inspección completamente realizadas para mostrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Deshabilitar las siguientes secciones hasta que se seleccione una orden explícitamente
            HabilitarSeccionObservacion(false);
            HabilitarSeccionMotivos(false);
            btnConfirmar.Enabled = false;
        }


        private void cargarTiposMotivoComboBox()
        {
            cmbTiposMotivo.DataSource = null;
            cmbTiposMotivo.DataSource = _controlador.buscarTiposMotivos();
            cmbTiposMotivo.DisplayMember = "Descripcion";
        }

        // Evento cuando cambia la selección en la grilla de órdenes
        private void dgvOrdenesInspeccion_SelectionChanged(object sender, EventArgs e)
        {
            if (grillaOrdenes.CurrentRow != null && grillaOrdenes.CurrentRow.DataBoundItem != null)
            {
                var selectedRowItem = grillaOrdenes.CurrentRow.DataBoundItem;
                int numeroOrdenSeleccionada = (int)selectedRowItem.GetType().GetProperty("NumeroOrden").GetValue(selectedRowItem, null);
                // Guardamos la orden seleccionada en la grilla temporalmente
                _ordenTemporalmenteSeleccionadaEnGrilla = _controlador.Ordenes.FirstOrDefault(o => o.NumeroOrden == numeroOrdenSeleccionada);

                if (_ordenTemporalmenteSeleccionadaEnGrilla != null)
                {
                    btnSeleccionarOrden.Enabled = true; // Habilitar el botón "Seleccionar Orden"
                }
                else
                {
                    btnSeleccionarOrden.Enabled = false;
                }
            }
            else
            {
                _ordenTemporalmenteSeleccionadaEnGrilla = null;
                btnSeleccionarOrden.Enabled = false;
            }
        }

        // NUEVO: Evento para el botón "Seleccionar Orden"
        // Este método ahora es el que realmente confirma la selección de la orden en el controlador
        // y habilita la siguiente sección (observaciones).
        // Corresponde al método `seleccionarOrden()` del diagrama de boundary [cite: 1]
        private void btnSeleccionarOrden_Click(object sender, EventArgs e)
        {
            if (_ordenTemporalmenteSeleccionadaEnGrilla != null)
            {
                _controlador.tomarOrdenSeleccionada(_ordenTemporalmenteSeleccionadaEnGrilla); // Informa al controlador

                // Limpiar campos para la nueva selección
                txtObservacion.Text = string.Empty;
                txtComentario.Text = string.Empty;
                _controlador.listaMotivosTipoComentario.Clear();
                mostrarMotivosAgregados(_controlador.listaMotivosTipoComentario);

                HabilitarSeccionObservacion(true); // Habilita txtObservacionCierre y btnConfirmarObservacion
                HabilitarSeccionMotivos(false);    // La sección de motivos se habilita DESPUÉS de confirmar observación
                btnConfirmar.Enabled = false;    // El botón final de cierre se habilita después de confirmar motivos

                grillaOrdenes.Enabled = false; // Opcional: deshabilitar la grilla para evitar cambios
                btnSeleccionarOrden.Enabled = false;  // Deshabilitar este botón una vez usado
            }
            else
            {
                MessageBox.Show("No hay una orden válida seleccionada en la grilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // NUEVO: Evento para el botón "Confirmar Observación"
        // Corresponde a los métodos `ingresarObservacion()` y `solicitarConfirmacion()` (parcial) del diagrama [cite: 1]
        private void btnConfirmarObservacion_Click(object sender, EventArgs e)
        {
            string observacion = txtObservacion.Text;
            if (string.IsNullOrWhiteSpace(observacion)) // Validación básica
            {
                MessageBox.Show("La observación de cierre no puede estar vacía.", "Dato Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // `solicitarIngresoObservacion()` - Mantener foco
                txtObservacionCierre.Focus();
                return;
            }

            _controlador.tomarObservacionIngresada(observacion); // `ingresarObservacion()`
            MessageBox.Show("Observación de cierre registrada.", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtObservacion.Enabled = false;         // Deshabilitar después de confirmar
            btnConfirmarObservacion.Enabled = false;    // Deshabilitar después de confirmar

            cargarTiposMotivoComboBox();

            HabilitarSeccionMotivos(true); // Ahora habilitar la sección de motivos
                                           // btnCerrarOrden todavía no, hasta que se agreguen motivos.
                                           // El método `solicitarConfirmacion()` del diagrama para el cierre final está en `btnCerrarOrden_Click`
        }

        // Evento cuando cambia el tipo de motivo seleccionado
        private void btnAgregarMotivo_Click(object sender, EventArgs e)
        {
            if (cmbTiposMotivo.SelectedItem is MotivoTipo motivoTipoSeleccionado && txtComentario != null)
            {
                _controlador.tomarMotivoSeleccionado(motivoTipoSeleccionado);
                _controlador.tomarComentarioIngresado(txtComentario.Text);
                List<Tuple<string, MotivoTipo>> lista = _controlador.agregarMotivoALista();

                mostrarMotivosAgregados(lista);

                txtComentario.Clear();
                cmbTiposMotivo.Focus();

                if (_controlador.listaMotivosTipoComentario.Any())
                {
                    btnConfirmar.Enabled = true;
                }

            }
            else
            {
                MessageBox.Show("Debe seleccionar un tipo de motivo y escribir un comentario.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void seleccionarMotivo(object sender, EventArgs e)
        {
            btnAgregarMotivo.Enabled = cmbTiposMotivo.SelectedItem is MotivoTipo;
        }

        private void mostrarMotivosAgregados(List<Tuple<string, MotivoTipo>> lista)
        {
            grillaMotivos.DataSource = null;
            grillaMotivos.DataSource =
              lista
                .Select(t => new
                {
                    Comentario = t.Item1,
                    Tipo = t.Item2.Descripcion
                })
                .ToList();
        }

        // Evento para el botón "Cerrar Orden"
        private void btnCerrarOrden_Click(object sender, EventArgs e)
        {
            if (_controlador.OrdenSeleccionada == null)
            {
                MessageBox.Show("Primero debe seleccionar y confirmar una orden.", "Acción requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // La observación ya fue tomada y validada (básicamente) por btnConfirmarObservacion_Click
            // pero el controlador tiene su propia validación más robusta.
            if (!_controlador.validarObservacion()) // Esta validación usa la observación guardada en el controlador
            {
                MessageBox.Show("La observación de cierre no fue registrada o es inválida.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Reactivar sección observación si fuera necesario o guiar al usuario.
                // Por ahora, es un estado anómalo si se llega aquí con observación inválida.
                return;
            }
            if (!_controlador.validarMotivoSeleccionado())
            {
                MessageBox.Show("Debe agregar al menos un motivo de fuera de servicio.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HabilitarSeccionMotivos(true); // Re-habilitar para que agregue motivos
                cmbTiposMotivo.Focus();
                return;
            }

            // `solicitarConfirmacion()` del diagrama para el cierre final
            var confirmResult = MessageBox.Show("¿Confirma el cierre final de esta orden de inspección?",
                                             "Confirmar Cierre Final", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes) return;

            bool exito = _controlador.tomarConfirmacionRegistrada();
            if (!exito)
            {
                MessageBox.Show(
                    "Error al cerrar la orden. Verifique los datos y el estado del sistema.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            MessageBox.Show(
                 "Orden cerrada. Sismógrafo puesto fuera de servicio.",
                 "Operación Exitosa",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information
            );


            var datosCCRS = _controlador.getDatosParaPantallaCCRS();
            if (datosCCRS != null)
            {
                {
                    PantallaCCRS pantallaCCRS = new PantallaCCRS();
                    pantallaCCRS.CargarDatos(
                        (string)datosCCRS[0],
                        (string)datosCCRS[1],
                        (DateTime)datosCCRS[2],
                        (List<Tuple<string, MotivoTipo>>)datosCCRS[3],
                        (string)datosCCRS[4],
                        (IEnumerable<Sismografo>)datosCCRS[5] // Pasar la lista de sismógrafos para mostrar en CCRS si es necesario

                    );
                    pantallaCCRS.ShowDialog(this);
                }
            }
            string mensajeNotificacion = _controlador.construirMensajeNotificacion();
            List<string> emailsReparadores = _controlador.obtenerEmailsResponsablesReparacion();

            PantallaMail pantallaMail = new PantallaMail();
            if (datosCCRS != null)
            {
                pantallaMail.CargarDatos(
                    (string)datosCCRS[0],
                    (string)datosCCRS[1],
                    (DateTime)datosCCRS[2],
                    (List<Tuple<string, MotivoTipo>>)datosCCRS[3],
                    (string)datosCCRS[4],
                    string.Join(", ", emailsReparadores)
                );
                pantallaMail.ShowDialog(this);
            }

            MessageBox.Show($"Notificaciones enviadas (simulado a: {string.Join(", ", emailsReparadores)}). \n\nContenido:\n{mensajeNotificacion}", "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _controlador.finCU();
            ConfigurarEstadoInicialUI(); // Volver al estado inicial para una nueva operación
            List<OrdenDeInspeccion >OrdenesFiltradas = _controlador.buscarOrdenInspeccion();
            mostrarOrdenes(OrdenesFiltradas); // Recargar la grilla de órdenes (estará vacía o con nuevas órdenes si la lógica lo permite)
            HabilitarSeccionSeleccionOrden(true); // Permitir seleccionar otra orden
            grillaOrdenes.Enabled = true;
            txtObservacion.Clear();
            mostrarMotivosAgregados(_controlador.listaMotivosTipoComentario); // Limpiar la grilla de motivos

        }

        // Evento para el botón "Cancelar"
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Reiniciar el estado del CU en el controlador y la UI
            _controlador.finCU(); // Llama a finCU para limpiar el estado del controlador

            // Resetear la UI a un estado similar al inicial después del login
            _ordenTemporalmenteSeleccionadaEnGrilla = null;
            txtObservacionCierre.Clear();
            if (cmbTiposMotivo.Items.Count > 0) cmbTiposMotivo.SelectedIndex = -1;
            txtComentarioMotivo.Clear();
            // MotivosAgregados se limpian en finCU del controlador, aquí actualizamos la grilla
            mostrarMotivosAgregados(_controlador.listaMotivosTipoComentario);

            HabilitarSeccionSeleccionOrden(true); // Permitir volver a seleccionar orden
            dgvOrdenesInspeccion.Enabled = true;  // Asegurarse que la grilla esté activa
            if (dgvOrdenesInspeccion.Rows.Count > 0) dgvOrdenesInspeccion.ClearSelection();

            HabilitarSeccionObservacion(false);
            HabilitarSeccionMotivos(false);
            btnCerrarOrden.Enabled = false;

            List<OrdenDeInspeccion> OrdenesFiltradas = _controlador.buscarOrdenInspeccion();
            mostrarOrdenes(OrdenesFiltradas); // Recargar las órdenes disponibles

            MessageBox.Show("Operación cancelada. Puede seleccionar una nueva orden.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
