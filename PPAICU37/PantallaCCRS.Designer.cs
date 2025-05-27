namespace PPAICU37
{
    partial class PantallaCCRS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblIdSismografo = new Label();
            lblNombreEstado = new Label();
            lblFechaHoraActual = new Label();
            lstMotivos = new ListBox();
            lblComentarios = new TextBox();
            SuspendLayout();
            // 
            // lblIdSismografo
            // 
            lblIdSismografo.AutoSize = true;
            lblIdSismografo.Location = new Point(27, 34);
            lblIdSismografo.Name = "lblIdSismografo";
            lblIdSismografo.Size = new Size(102, 20);
            lblIdSismografo.TabIndex = 0;
            lblIdSismografo.Text = "Id Sismografo";
            lblIdSismografo.Click += lblIdSismografo_Click;
            // 
            // lblNombreEstado
            // 
            lblNombreEstado.AutoSize = true;
            lblNombreEstado.Location = new Point(27, 74);
            lblNombreEstado.Name = "lblNombreEstado";
            lblNombreEstado.Size = new Size(113, 20);
            lblNombreEstado.TabIndex = 1;
            lblNombreEstado.Text = "Nombre estado";
            // 
            // lblFechaHoraActual
            // 
            lblFechaHoraActual.AutoSize = true;
            lblFechaHoraActual.Location = new Point(27, 119);
            lblFechaHoraActual.Name = "lblFechaHoraActual";
            lblFechaHoraActual.Size = new Size(125, 20);
            lblFechaHoraActual.TabIndex = 2;
            lblFechaHoraActual.Text = "Fecha hora actual";
            // 
            // lstMotivos
            // 
            lstMotivos.FormattingEnabled = true;
            lstMotivos.Location = new Point(27, 171);
            lstMotivos.Margin = new Padding(3, 4, 3, 4);
            lstMotivos.Name = "lstMotivos";
            lstMotivos.Size = new Size(480, 124);
            lstMotivos.TabIndex = 3;
            lstMotivos.SelectedIndexChanged += lstMotivos_SelectedIndexChanged;
            // 
            // lblComentarios
            // 
            lblComentarios.Location = new Point(27, 321);
            lblComentarios.Margin = new Padding(3, 4, 3, 4);
            lblComentarios.Name = "lblComentarios";
            lblComentarios.Size = new Size(366, 27);
            lblComentarios.TabIndex = 4;
            // 
            // PantallaCCRS
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(538, 406);
            Controls.Add(lblComentarios);
            Controls.Add(lstMotivos);
            Controls.Add(lblFechaHoraActual);
            Controls.Add(lblNombreEstado);
            Controls.Add(lblIdSismografo);
            Margin = new Padding(3, 4, 3, 4);
            Name = "PantallaCCRS";
            Text = "PantallaCCRS";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblIdSismografo;
        private Label lblNombreEstado;
        private Label lblFechaHoraActual;
        private ListBox lstMotivos;
        private TextBox lblComentarios;
    }
}