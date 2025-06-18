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
            grillaOrdenes = new DataGridView();
            txtObservacion = new TextBox();
            cmbTiposMotivo = new ComboBox();
            txtComentario = new TextBox();
            grillaMotivos = new DataGridView();
            btnAgregarMotivo = new Button();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            btnIniciarSesion = new Button();
            btnSeleccionarOrden = new Button();
            btnConfirmarObservacion = new Button();
            ((System.ComponentModel.ISupportInitialize)grillaOrdenes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grillaMotivos).BeginInit();
            SuspendLayout();
            // 
            // grillaOrdenes
            // 
            grillaOrdenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaOrdenes.Location = new Point(33, 85);
            grillaOrdenes.Name = "grillaOrdenes";
            grillaOrdenes.RowHeadersWidth = 51;
            grillaOrdenes.Size = new Size(511, 150);
            grillaOrdenes.TabIndex = 0;
            grillaOrdenes.SelectionChanged += grillaOrdenesCambioSeleccion;
            // 
            // txtObservacion
            // 
            txtObservacion.Location = new Point(109, 280);
            txtObservacion.Name = "txtObservacion";
            txtObservacion.Size = new Size(228, 23);
            txtObservacion.TabIndex = 1;
            // 
            // cmbTiposMotivo
            // 
            cmbTiposMotivo.FormattingEnabled = true;
            cmbTiposMotivo.Location = new Point(504, 359);
            cmbTiposMotivo.Name = "cmbTiposMotivo";
            cmbTiposMotivo.Size = new Size(121, 23);
            cmbTiposMotivo.TabIndex = 2;
            cmbTiposMotivo.SelectedIndexChanged += seleccionarTipoMotivo;
            // 
            // txtComentario
            // 
            txtComentario.Location = new Point(515, 404);
            txtComentario.Name = "txtComentario";
            txtComentario.Size = new Size(100, 23);
            txtComentario.TabIndex = 3;
            // 
            // grillaMotivos
            // 
            grillaMotivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaMotivos.Location = new Point(33, 334);
            grillaMotivos.Name = "grillaMotivos";
            grillaMotivos.RowHeadersWidth = 51;
            grillaMotivos.Size = new Size(448, 150);
            grillaMotivos.TabIndex = 4;
            // 
            // btnAgregarMotivo
            // 
            btnAgregarMotivo.Location = new Point(642, 386);
            btnAgregarMotivo.Name = "btnAgregarMotivo";
            btnAgregarMotivo.Size = new Size(75, 23);
            btnAgregarMotivo.TabIndex = 5;
            btnAgregarMotivo.Text = "Agregar";
            btnAgregarMotivo.UseVisualStyleBackColor = true;
            btnAgregarMotivo.Click += ingresarComentario;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(533, 508);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.RightToLeft = RightToLeft.No;
            btnConfirmar.Size = new Size(102, 23);
            btnConfirmar.TabIndex = 6;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += confirmarCierre;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(661, 508);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += cancelarCierre;
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.Location = new Point(174, 27);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(228, 23);
            btnIniciarSesion.TabIndex = 8;
            btnIniciarSesion.Text = "Iniciar sesión";
            btnIniciarSesion.UseVisualStyleBackColor = true;
            btnIniciarSesion.Click += opcionCerrarOrden;
            // 
            // btnSeleccionarOrden
            // 
            btnSeleccionarOrden.Location = new Point(569, 211);
            btnSeleccionarOrden.Name = "btnSeleccionarOrden";
            btnSeleccionarOrden.Size = new Size(124, 24);
            btnSeleccionarOrden.TabIndex = 9;
            btnSeleccionarOrden.Text = "Seleccionar orden";
            btnSeleccionarOrden.UseVisualStyleBackColor = true;
            btnSeleccionarOrden.Click += seleccionarOrden;
            // 
            // btnConfirmarObservacion
            // 
            btnConfirmarObservacion.Location = new Point(362, 279);
            btnConfirmarObservacion.Name = "btnConfirmarObservacion";
            btnConfirmarObservacion.Size = new Size(150, 23);
            btnConfirmarObservacion.TabIndex = 10;
            btnConfirmarObservacion.Text = "Registrar Observacion";
            btnConfirmarObservacion.UseVisualStyleBackColor = true;
            btnConfirmarObservacion.Click += ingresarObservacion;
            // 
            // PantallaCerrarOrden
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(758, 549);
            Controls.Add(btnConfirmarObservacion);
            Controls.Add(btnSeleccionarOrden);
            Controls.Add(btnIniciarSesion);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(btnAgregarMotivo);
            Controls.Add(grillaMotivos);
            Controls.Add(txtComentario);
            Controls.Add(cmbTiposMotivo);
            Controls.Add(txtObservacion);
            Controls.Add(grillaOrdenes);
            Name = "PantallaCerrarOrden";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)grillaOrdenes).EndInit();
            ((System.ComponentModel.ISupportInitialize)grillaMotivos).EndInit();
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
        private Button btnConfirmar;
        private TextBox txtObservacion;
        private Button btnIniciarSesion;
        private DataGridView grillaOrdenes;
        private TextBox txtComentario;
        private DataGridView grillaMotivos;
    }
}
