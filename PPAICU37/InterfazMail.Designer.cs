namespace PPAICU37
{
    partial class InterfazMail
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
            lblIdSismografoMail = new Label();
            lblNombreEstadoMail = new Label();
            lblFechaHoraActualMail = new Label();
            lstMotivosMail = new ListBox();
            txtComentariosMail = new TextBox();
            lblDestinatariosMail = new Label();
            SuspendLayout();
            // 
            // lblIdSismografoMail
            // 
            lblIdSismografoMail.AutoSize = true;
            lblIdSismografoMail.Location = new Point(29, 49);
            lblIdSismografoMail.Name = "lblIdSismografoMail";
            lblIdSismografoMail.Size = new Size(80, 15);
            lblIdSismografoMail.TabIndex = 0;
            lblIdSismografoMail.Text = "Id Sismografo";
            // 
            // lblNombreEstadoMail
            // 
            lblNombreEstadoMail.AutoSize = true;
            lblNombreEstadoMail.Location = new Point(29, 76);
            lblNombreEstadoMail.Name = "lblNombreEstadoMail";
            lblNombreEstadoMail.Size = new Size(89, 15);
            lblNombreEstadoMail.TabIndex = 1;
            lblNombreEstadoMail.Text = "Nombre estado";
            // 
            // lblFechaHoraActualMail
            // 
            lblFechaHoraActualMail.AutoSize = true;
            lblFechaHoraActualMail.Location = new Point(29, 106);
            lblFechaHoraActualMail.Name = "lblFechaHoraActualMail";
            lblFechaHoraActualMail.Size = new Size(100, 15);
            lblFechaHoraActualMail.TabIndex = 2;
            lblFechaHoraActualMail.Text = "Fecha hora actual";
            lblFechaHoraActualMail.TextAlign = ContentAlignment.TopCenter;
            // 
            // lstMotivosMail
            // 
            lstMotivosMail.FormattingEnabled = true;
            lstMotivosMail.ItemHeight = 15;
            lstMotivosMail.Location = new Point(29, 141);
            lstMotivosMail.Name = "lstMotivosMail";
            lstMotivosMail.Size = new Size(414, 94);
            lstMotivosMail.TabIndex = 3;
            // 
            // txtComentariosMail
            // 
            txtComentariosMail.Enabled = false;
            txtComentariosMail.Location = new Point(33, 254);
            txtComentariosMail.Name = "txtComentariosMail";
            txtComentariosMail.Size = new Size(281, 23);
            txtComentariosMail.TabIndex = 4;
            // 
            // lblDestinatariosMail
            // 
            lblDestinatariosMail.AutoSize = true;
            lblDestinatariosMail.Location = new Point(29, 302);
            lblDestinatariosMail.Name = "lblDestinatariosMail";
            lblDestinatariosMail.Size = new Size(75, 15);
            lblDestinatariosMail.TabIndex = 5;
            lblDestinatariosMail.Text = "Destinatarios";
            // 
            // InterfazMail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(486, 361);
            Controls.Add(lblDestinatariosMail);
            Controls.Add(txtComentariosMail);
            Controls.Add(lstMotivosMail);
            Controls.Add(lblFechaHoraActualMail);
            Controls.Add(lblNombreEstadoMail);
            Controls.Add(lblIdSismografoMail);
            Name = "InterfazMail";
            Text = "InterfazMail";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblIdSismografoMail;
        private Label lblNombreEstadoMail;
        private Label lblFechaHoraActualMail;
        private ListBox lstMotivosMail;
        private TextBox txtComentariosMail;
        private Label lblDestinatariosMail;
    }
}