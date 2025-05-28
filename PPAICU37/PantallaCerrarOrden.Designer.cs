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
            grillaOrdenes.Location = new Point(38, 113);
            grillaOrdenes.Margin = new Padding(3, 4, 3, 4);
            grillaOrdenes.Name = "grillaOrdenes";
            grillaOrdenes.RowHeadersWidth = 51;
            grillaOrdenes.Size = new Size(584, 200);
            grillaOrdenes.TabIndex = 0;
            grillaOrdenes.SelectionChanged += dgvOrdenesInspeccion_SelectionChanged;
            // 
            // txtObservacion
            // 
            txtObservacion.Location = new Point(125, 373);
            txtObservacion.Margin = new Padding(3, 4, 3, 4);
            txtObservacion.Name = "txtObservacion";
            txtObservacion.Size = new Size(260, 27);
            txtObservacion.TabIndex = 1;
            // 
            // cmbTiposMotivo
            // 
            cmbTiposMotivo.FormattingEnabled = true;
            cmbTiposMotivo.Location = new Point(576, 479);
            cmbTiposMotivo.Margin = new Padding(3, 4, 3, 4);
            cmbTiposMotivo.Name = "cmbTiposMotivo";
            cmbTiposMotivo.Size = new Size(138, 28);
            cmbTiposMotivo.TabIndex = 2;
            cmbTiposMotivo.SelectedIndexChanged += seleccionarMotivo;
            // 
            // btnAgregarMotivo
            // 
            btnAgregarMotivo.Location = new Point(734, 515);
            btnAgregarMotivo.Margin = new Padding(3, 4, 3, 4);
            btnAgregarMotivo.Name = "btnAgregarMotivo";
            btnAgregarMotivo.Size = new Size(86, 31);
            btnAgregarMotivo.TabIndex = 5;
            btnAgregarMotivo.Text = "Agregar";
            btnAgregarMotivo.UseVisualStyleBackColor = true;
            btnAgregarMotivo.Click += btnAgregarMotivo_Click;
            // 
            // txtComentario
            // 
            txtComentario.Location = new Point(589, 539);
            txtComentario.Margin = new Padding(3, 4, 3, 4);
            txtComentario.Name = "txtComentario";
            txtComentario.Size = new Size(114, 27);
            txtComentario.TabIndex = 3;
            // 
            // grillaMotivos
            // 
            grillaMotivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaMotivos.Location = new Point(38, 445);
            grillaMotivos.Margin = new Padding(3, 4, 3, 4);
            grillaMotivos.Name = "grillaMotivos";
            grillaMotivos.RowHeadersWidth = 51;
            grillaMotivos.Size = new Size(512, 200);
            grillaMotivos.TabIndex = 4;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(609, 677);
            btnConfirmar.Margin = new Padding(3, 4, 3, 4);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.RightToLeft = RightToLeft.No;
            btnConfirmar.Size = new Size(117, 31);
            btnConfirmar.TabIndex = 6;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnCerrarOrden_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(755, 677);
            btnCancelar.Margin = new Padding(3, 4, 3, 4);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(86, 31);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.Location = new Point(199, 36);
            btnIniciarSesion.Margin = new Padding(3, 4, 3, 4);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(260, 31);
            btnIniciarSesion.TabIndex = 8;
            btnIniciarSesion.Text = "Iniciar sesión";
            btnIniciarSesion.UseVisualStyleBackColor = true;
            btnIniciarSesion.Click += btnIniciarSesionSimulado_Click;
            // 
            // btnSeleccionarOrden
            // 
            btnSeleccionarOrden.Location = new Point(650, 281);
            btnSeleccionarOrden.Margin = new Padding(3, 4, 3, 4);
            btnSeleccionarOrden.Name = "btnSeleccionarOrden";
            btnSeleccionarOrden.Size = new Size(142, 32);
            btnSeleccionarOrden.TabIndex = 9;
            btnSeleccionarOrden.Text = "Seleccionar orden";
            btnSeleccionarOrden.UseVisualStyleBackColor = true;
            btnSeleccionarOrden.Click += btnSeleccionarOrden_Click;
            // 
            // btnConfirmarObservacion
            // 
            btnConfirmarObservacion.Location = new Point(414, 372);
            btnConfirmarObservacion.Margin = new Padding(3, 4, 3, 4);
            btnConfirmarObservacion.Name = "btnConfirmarObservacion";
            btnConfirmarObservacion.Size = new Size(172, 31);
            btnConfirmarObservacion.TabIndex = 10;
            btnConfirmarObservacion.Text = "Registrar Observacion";
            btnConfirmarObservacion.UseVisualStyleBackColor = true;
            btnConfirmarObservacion.Click += btnConfirmarObservacion_Click;
            // 
            // PantallaCerrarOrden
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(866, 732);
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
            Margin = new Padding(3, 4, 3, 4);
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
