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
        private OrdenDeInspeccion _ordenTemporalmenteSeleccionadaEnGrilla; // Para guardar la selecci�n de la grilla antes de confirmar con el bot�n

        public PantallaCerrarOrden()
        {
            InitializeComponent();
            _controlador = new ControladorCerrarOrden();
            habilitar();
        }

        private void opcionCerrarOrden(object sender, EventArgs e)
        {
            habilitarSeccionSeleccionOrden(true);
            DataTable tablaFiltrada = _controlador.tomarOpcionSeleccionada("CERRAR_ORDEN_INSPECCION");
            MessageBox.Show($"Login simulado exitoso para: {_controlador.responsableLogueado.Empleado}", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnCancelar.Enabled = true;
            mostrarOrdenesConAsociados(tablaFiltrada);
            btnIniciarSesion.Enabled = false;
        }

        private void habilitar()
        {
            habilitarSeccionSeleccionOrden(false); // dgvOrdenesInspeccion y btnSeleccionarOrden
            solicitarIngresoObservacion(false);    // txtObservacionCierre y btnConfirmarObservacion
            solicitarSeleccionTiposMotivos(false);
            solicitarComentario(false);

            solicitarConfirmacion(false);
            btnCancelar.Enabled = false; // Se habilita despu�s del login
            btnIniciarSesion.Enabled = true;
        }

        private void mostrarOrdenesConAsociados(DataTable dt)
        {
            // Asigno al DataGridView
            grillaOrdenes.DataSource = dt;

            // (Opcional) afinaciones de UI
            grillaOrdenes.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            grillaOrdenes.ReadOnly = true;
        }

        private void seleccionarOrden(object sender, EventArgs e)
        {
            if (_ordenTemporalmenteSeleccionadaEnGrilla != null)
            {
                _controlador.tomarOrdenSeleccionada(_ordenTemporalmenteSeleccionadaEnGrilla); // Informa al controlador

                // Limpiar campos para la nueva selecci�n
                txtObservacion.Text = string.Empty;
                txtComentario.Text = string.Empty;
                _controlador.listaMotivosTipoComentario.Clear();
                mostrarMotivosAgregados(_controlador.listaMotivosTipoComentario);

                solicitarIngresoObservacion(true); // Habilita txtObservacionCierre y btnConfirmarObservacion
                solicitarSeleccionTiposMotivos(false);
                solicitarComentario(false);
                solicitarConfirmacion(false);

                grillaOrdenes.Enabled = false; // Opcional: deshabilitar la grilla para evitar cambios
                btnSeleccionarOrden.Enabled = false;  // Deshabilitar este bot�n una vez usado
            }
            else
            {
                MessageBox.Show("No hay una orden v�lida seleccionada en la grilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void solicitarIngresoObservacion(bool habilitar)
        {
            txtObservacion.Enabled = habilitar;
            solicitarConfirmacion(habilitar);
            if (habilitar)
            {
                // solicitarIngresoObservacion() - Se le da foco al txt
                txtObservacion.Focus();
            }
        }

        private void ingresarObservacion(object sender, EventArgs e)
        {
            string observacion = txtObservacion.Text;
            if (string.IsNullOrWhiteSpace(observacion)) // Validaci�n b�sica
            {
                MessageBox.Show("La observaci�n de cierre no puede estar vac�a.", "Dato Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // `solicitarIngresoObservacion()` - Mantener foco
                txtObservacionCierre.Focus();
                return;
            }

            _controlador.tomarObservacionIngresada(observacion);
            MessageBox.Show("Observaci�n de cierre registrada.", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtObservacion.Enabled = false;         // Deshabilitar despu�s de confirmar
            solicitarConfirmacion(false);    // Deshabilitar despu�s de confirmar

            mostrarTiposMotivos();

            solicitarSeleccionTiposMotivos(true);
            solicitarComentario(true);
        }

        private void mostrarTiposMotivos()
        {
            cmbTiposMotivo.DataSource = null;
            cmbTiposMotivo.DataSource = _controlador.buscarTiposMotivos();
            cmbTiposMotivo.DisplayMember = "Descripcion";
        }

        private void solicitarSeleccionTiposMotivos(bool habilitar)
        {
            cmbTiposMotivo.Enabled = habilitar;
            btnAgregarMotivo.Enabled = habilitar;
            grillaMotivos.Enabled = habilitar;
        }

        private void seleccionarTipoMotivo(object sender, EventArgs e)
        {
            btnAgregarMotivo.Enabled = cmbTiposMotivo.SelectedItem is MotivoTipo;
        }

        private void solicitarComentario(bool habilitar)
        {
            txtComentario.Enabled = habilitar;
        }

        private void ingresarComentario(object sender, EventArgs e)
        {
            string comentario = txtComentario.Text;
            if (!(cmbTiposMotivo.SelectedItem is MotivoTipo motivoTipoSeleccionado))
            {
                MessageBox.Show(
                    "Debe seleccionar un tipo de motivo.",
                    "Dato incompleto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (string.IsNullOrWhiteSpace(comentario))
            {
                MessageBox.Show(
                    "Debe escribir un comentario para el motivo.",
                    "Dato incompleto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtComentario.Focus();
                return;
            }
            _controlador.tomarTipoMotivoSeleccionado(motivoTipoSeleccionado);
            _controlador.tomarComentarioIngresado(txtComentario.Text);
            List<Tuple<string, MotivoTipo>> lista = _controlador.agregarMotivoALista();

            mostrarMotivosAgregados(lista);

            txtComentario.Clear();
            cmbTiposMotivo.Focus();

            if (_controlador.listaMotivosTipoComentario.Any())
            {
                solicitarConfirmacion(true);
            }
        }

        private void solicitarConfirmacion(bool habilitar)
        {
            btnConfirmar.Enabled = habilitar;
        }

        private void confirmarCierre(object sender, EventArgs e)
        {
            if (_controlador.ordenSeleccionada == null)
            {
                MessageBox.Show("Primero debe seleccionar y confirmar una orden.", "Acci�n requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // La observaci�n ya fue tomada y validada (b�sicamente) por btnConfirmarObservacion_Click
            // pero el controlador tiene su propia validaci�n m�s robusta.
            if (!_controlador.validarObservacion()) // Esta validaci�n usa la observaci�n guardada en el controlador
            {
                MessageBox.Show("La observaci�n de cierre no fue registrada o es inv�lida.", "Error de validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Reactivar secci�n observaci�n si fuera necesario o guiar al usuario.
                // Por ahora, es un estado an�malo si se llega aqu� con observaci�n inv�lida.
                return;
            }
            if (!_controlador.validarMotivoSeleccionado())
            {
                MessageBox.Show("Debe agregar al menos un motivo de fuera de servicio.", "Error de validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error);
                solicitarSeleccionTiposMotivos(true); // Re-habilitar para que agregue motivos
                solicitarComentario(true);
                cmbTiposMotivo.Focus();
                return;
            }

            // `solicitarConfirmacion()` del diagrama para el cierre final
            var confirmResult = MessageBox.Show("�Confirma el cierre final de esta orden de inspecci�n?",
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
                 "Orden cerrada. Sism�grafo puesto fuera de servicio.",
                 "Operaci�n Exitosa",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information
            );

            _controlador.finCU();
            habilitar(); // Volver al estado inicial para una nueva operaci�n

            List<OrdenDeInspeccion> Ordenes = _controlador.ordenes; // Obtener las �rdenes filtradas del controlador
            DataTable tablaFiltrada = _controlador.buscarOrdenInspeccion(Ordenes);
            mostrarOrdenesConAsociados(tablaFiltrada);

            habilitarSeccionSeleccionOrden(true); // Permitir seleccionar otra orden
            grillaOrdenes.Enabled = true;
            txtObservacion.Clear();
            mostrarMotivosAgregados(_controlador.listaMotivosTipoComentario); // Limpiar la grilla de motivos

        }

        private void habilitarSeccionSeleccionOrden(bool habilitar)
        {
            grillaOrdenes.Enabled = habilitar;
            // btnSeleccionarOrden se habilita solo cuando hay una fila seleccionada en la grilla
            if (!habilitar) btnSeleccionarOrden.Enabled = false;
        }

        private void grillaOrdenesCambioSeleccion(object sender, EventArgs e)
        {
            // 1) Si no hay fila seleccionada, deshabilito y salgo
            if (grillaOrdenes.CurrentRow == null)
            {
                _ordenTemporalmenteSeleccionadaEnGrilla = null;
                btnSeleccionarOrden.Enabled = false;
                return;
            }

            // 2) Casteo a DataRowView para extraer el campo "N�mero Orden"
            var drv = grillaOrdenes.CurrentRow.DataBoundItem as DataRowView;
            if (drv == null
             || drv["N�mero Orden"] == DBNull.Value
             || !int.TryParse(drv["N�mero Orden"].ToString(), out int nro))
            {
                _ordenTemporalmenteSeleccionadaEnGrilla = null;
                btnSeleccionarOrden.Enabled = false;
                return;
            }

            // 3) Busco la entidad completa por ese n�mero (opcional)
            _ordenTemporalmenteSeleccionadaEnGrilla =
                _controlador.ordenes
                    .FirstOrDefault(o => o.numeroOrden == nro);

            // 4) Habilito el bot�n si encontr� algo
            btnSeleccionarOrden.Enabled = _ordenTemporalmenteSeleccionadaEnGrilla != null;
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

        private void cancelarCierre(object sender, EventArgs e)
        {
            // Reiniciar el estado del CU en el controlador y la UI
            _controlador.finCU(); // Llama a finCU para limpiar el estado del controlador

            // Resetear la UI a un estado similar al inicial despu�s del login
            _ordenTemporalmenteSeleccionadaEnGrilla = null;
            txtObservacion.Clear();
            if (cmbTiposMotivo.Items.Count > 0) cmbTiposMotivo.SelectedIndex = -1;
            txtComentario.Clear();
            // MotivosAgregados se limpian en finCU del controlador, aqu� actualizamos la grilla
            mostrarMotivosAgregados(_controlador.listaMotivosTipoComentario);

            habilitarSeccionSeleccionOrden(true); // Permitir volver a seleccionar orden
            grillaOrdenes.Enabled = true;  // Asegurarse que la grilla est� activa
            if (grillaOrdenes.Rows.Count > 0) grillaOrdenes.ClearSelection();

            solicitarIngresoObservacion(false);
            solicitarSeleccionTiposMotivos(false);
            solicitarComentario(false);

            solicitarConfirmacion(false);

            //List<OrdenDeInspeccion> OrdenesFiltradas = _controlador.buscarOrdenInspeccion();
            //mostrarOrdenes(OrdenesFiltradas); // Recargar las �rdenes disponibles

            List<OrdenDeInspeccion> Ordenes = _controlador.ordenes; // Obtener las �rdenes filtradas del controlador
            DataTable tablaFiltrada = _controlador.buscarOrdenInspeccion(Ordenes);
            mostrarOrdenesConAsociados(tablaFiltrada);

            MessageBox.Show("Operaci�n cancelada. Puede seleccionar una nueva orden.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
