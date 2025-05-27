namespace PPAICU37
{
    partial class PantallaCerrarOrden
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvOrdenesInspeccion = new DataGridView();
            txtObservacionCierre = new TextBox();
            cmbTiposMotivo = new ComboBox();
            txtComentarioMotivo = new TextBox();
            dgvMotivosFueraServicio = new DataGridView();
            btnAgregarMotivo = new Button();
            btnCerrarOrden = new Button();
            btnCancelar = new Button();
            btnIniciarSesionSimulado = new Button();
            btnSeleccionarOrden = new Button();
            btnConfirmarObservacion = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvOrdenesInspeccion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMotivosFueraServicio).BeginInit();
            SuspendLayout();
            // 
            // dgvOrdenesInspeccion
            // 
            dgvOrdenesInspeccion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrdenesInspeccion.Location = new Point(88, 42);
            dgvOrdenesInspeccion.Name = "dgvOrdenesInspeccion";
            dgvOrdenesInspeccion.Size = new Size(511, 150);
            dgvOrdenesInspeccion.TabIndex = 0;
            dgvOrdenesInspeccion.SelectionChanged += dgvOrdenesInspeccion_SelectionChanged;
            // 
            // txtObservacionCierre
            // 
            txtObservacionCierre.Location = new Point(109, 280);
            txtObservacionCierre.Name = "txtObservacionCierre";
            txtObservacionCierre.Size = new Size(228, 23);
            txtObservacionCierre.TabIndex = 1;
            // 
            // cmbTiposMotivo
            // 
            cmbTiposMotivo.FormattingEnabled = true;
            cmbTiposMotivo.Location = new Point(504, 407);
            cmbTiposMotivo.Name = "cmbTiposMotivo";
            cmbTiposMotivo.Size = new Size(121, 23);
            cmbTiposMotivo.TabIndex = 2;
            cmbTiposMotivo.SelectedIndexChanged += cmbTiposMotivo_SelectedIndexChanged;
            // 
            // txtComentarioMotivo
            // 
            txtComentarioMotivo.Location = new Point(515, 452);
            txtComentarioMotivo.Name = "txtComentarioMotivo";
            txtComentarioMotivo.Size = new Size(100, 23);
            txtComentarioMotivo.TabIndex = 3;
            // 
            // dgvMotivosFueraServicio
            // 
            dgvMotivosFueraServicio.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMotivosFueraServicio.Location = new Point(33, 382);
            dgvMotivosFueraServicio.Name = "dgvMotivosFueraServicio";
            dgvMotivosFueraServicio.Size = new Size(448, 150);
            dgvMotivosFueraServicio.TabIndex = 4;
            // 
            // btnAgregarMotivo
            // 
            btnAgregarMotivo.Location = new Point(659, 438);
            btnAgregarMotivo.Name = "btnAgregarMotivo";
            btnAgregarMotivo.Size = new Size(75, 23);
            btnAgregarMotivo.TabIndex = 5;
            btnAgregarMotivo.Text = "Agregar";
            btnAgregarMotivo.UseVisualStyleBackColor = true;
            btnAgregarMotivo.Click += btnAgregarMotivo_Click;
            // 
            // btnCerrarOrden
            // 
            btnCerrarOrden.Location = new Point(574, 624);
            btnCerrarOrden.Name = "btnCerrarOrden";
            btnCerrarOrden.Size = new Size(102, 23);
            btnCerrarOrden.TabIndex = 6;
            btnCerrarOrden.Text = "Cerrar Orden";
            btnCerrarOrden.UseVisualStyleBackColor = true;
            btnCerrarOrden.Click += btnCerrarOrden_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(702, 624);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnIniciarSesionSimulado
            // 
            btnIniciarSesionSimulado.Location = new Point(632, 77);
            btnIniciarSesionSimulado.Name = "btnIniciarSesionSimulado";
            btnIniciarSesionSimulado.Size = new Size(102, 23);
            btnIniciarSesionSimulado.TabIndex = 8;
            btnIniciarSesionSimulado.Text = "Iniciar";
            btnIniciarSesionSimulado.UseVisualStyleBackColor = true;
            btnIniciarSesionSimulado.Click += btnIniciarSesionSimulado_Click;
            // 
            // btnSeleccionarOrden
            // 
            btnSeleccionarOrden.Location = new Point(413, 212);
            btnSeleccionarOrden.Name = "btnSeleccionarOrden";
            btnSeleccionarOrden.Size = new Size(75, 23);
            btnSeleccionarOrden.TabIndex = 9;
            btnSeleccionarOrden.Text = "Seleccionar";
            btnSeleccionarOrden.UseVisualStyleBackColor = true;
            btnSeleccionarOrden.Click += btnSeleccionarOrden_Click;
            // 
            // btnConfirmarObservacion
            // 
            btnConfirmarObservacion.Location = new Point(362, 279);
            btnConfirmarObservacion.Name = "btnConfirmarObservacion";
            btnConfirmarObservacion.Size = new Size(75, 23);
            btnConfirmarObservacion.TabIndex = 10;
            btnConfirmarObservacion.Text = "Registrar Observacion";
            btnConfirmarObservacion.UseVisualStyleBackColor = true;
            btnConfirmarObservacion.Click += btnConfirmarObservacion_Click;
            // 
            // PantallaCerrarOrden
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(818, 678);
            Controls.Add(btnConfirmarObservacion);
            Controls.Add(btnSeleccionarOrden);
            Controls.Add(btnIniciarSesionSimulado);
            Controls.Add(btnCancelar);
            Controls.Add(btnCerrarOrden);
            Controls.Add(btnAgregarMotivo);
            Controls.Add(dgvMotivosFueraServicio);
            Controls.Add(txtComentarioMotivo);
            Controls.Add(cmbTiposMotivo);
            Controls.Add(txtObservacionCierre);
            Controls.Add(dgvOrdenesInspeccion);
            Name = "PantallaCerrarOrden";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvOrdenesInspeccion).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMotivosFueraServicio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvOrdenesInspeccion;
        private TextBox txtObservacionCierre;
        private ComboBox cmbTiposMotivo;
        private TextBox txtComentarioMotivo;
        private DataGridView dgvMotivosFueraServicio;
        private Button btnAgregarMotivo;
        private Button btnCerrarOrden;
        private Button btnCancelar;
        private Button btnIniciarSesionSimulado;
        private Button btnSeleccionarOrden;
        private Button btnConfirmarObservacion;
    }
}
