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
            txtComentariosAdicionales = new TextBox();
            btnOKCCRS = new Button();
            SuspendLayout();
            // 
            // lblIdSismografo
            // 
            lblIdSismografo.AutoSize = true;
            lblIdSismografo.Location = new Point(257, 53);
            lblIdSismografo.Name = "lblIdSismografo";
            lblIdSismografo.Size = new Size(38, 15);
            lblIdSismografo.TabIndex = 0;
            lblIdSismografo.Text = "label1";
            // 
            // lblNombreEstado
            // 
            lblNombreEstado.AutoSize = true;
            lblNombreEstado.Location = new Point(257, 82);
            lblNombreEstado.Name = "lblNombreEstado";
            lblNombreEstado.Size = new Size(38, 15);
            lblNombreEstado.TabIndex = 1;
            lblNombreEstado.Text = "label1";
            // 
            // lblFechaHoraActual
            // 
            lblFechaHoraActual.AutoSize = true;
            lblFechaHoraActual.Location = new Point(257, 115);
            lblFechaHoraActual.Name = "lblFechaHoraActual";
            lblFechaHoraActual.Size = new Size(38, 15);
            lblFechaHoraActual.TabIndex = 2;
            lblFechaHoraActual.Text = "label1";
            // 
            // lstMotivos
            // 
            lstMotivos.FormattingEnabled = true;
            lstMotivos.ItemHeight = 15;
            lstMotivos.Location = new Point(240, 155);
            lstMotivos.Name = "lstMotivos";
            lstMotivos.Size = new Size(120, 94);
            lstMotivos.TabIndex = 3;
            // 
            // txtComentariosAdicionales
            // 
            txtComentariosAdicionales.Location = new Point(257, 268);
            txtComentariosAdicionales.Name = "txtComentariosAdicionales";
            txtComentariosAdicionales.Size = new Size(100, 23);
            txtComentariosAdicionales.TabIndex = 4;
            // 
            // btnOKCCRS
            // 
            btnOKCCRS.Location = new Point(578, 310);
            btnOKCCRS.Name = "btnOKCCRS";
            btnOKCCRS.Size = new Size(75, 23);
            btnOKCCRS.TabIndex = 5;
            btnOKCCRS.Text = "button1";
            btnOKCCRS.UseVisualStyleBackColor = true;
            btnOKCCRS.Click += btnOkCCRS_Click;
            // 
            // PantallaCCRS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnOKCCRS);
            Controls.Add(txtComentariosAdicionales);
            Controls.Add(lstMotivos);
            Controls.Add(lblFechaHoraActual);
            Controls.Add(lblNombreEstado);
            Controls.Add(lblIdSismografo);
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
        private TextBox txtComentariosAdicionales;
        private Button btnOKCCRS;
    }
}