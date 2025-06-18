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
            lblIdSismografoMail.Location = new Point(33, 65);
            lblIdSismografoMail.Name = "lblIdSismografoMail";
            lblIdSismografoMail.Size = new Size(102, 20);
            lblIdSismografoMail.TabIndex = 0;
            lblIdSismografoMail.Text = "Id Sismografo";
            lblIdSismografoMail.Click += lblIdSismografoMail_Click;
            // 
            // lblNombreEstadoMail
            // 
            lblNombreEstadoMail.AutoSize = true;
            lblNombreEstadoMail.Location = new Point(33, 102);
            lblNombreEstadoMail.Name = "lblNombreEstadoMail";
            lblNombreEstadoMail.Size = new Size(113, 20);
            lblNombreEstadoMail.TabIndex = 1;
            lblNombreEstadoMail.Text = "Nombre estado";
            // 
            // lblFechaHoraActualMail
            // 
            lblFechaHoraActualMail.AutoSize = true;
            lblFechaHoraActualMail.Location = new Point(33, 142);
            lblFechaHoraActualMail.Name = "lblFechaHoraActualMail";
            lblFechaHoraActualMail.Size = new Size(125, 20);
            lblFechaHoraActualMail.TabIndex = 2;
            lblFechaHoraActualMail.Text = "Fecha hora actual";
            lblFechaHoraActualMail.TextAlign = ContentAlignment.TopCenter;
            // 
            // lstMotivosMail
            // 
            lstMotivosMail.FormattingEnabled = true;
            lstMotivosMail.Location = new Point(33, 188);
            lstMotivosMail.Margin = new Padding(3, 4, 3, 4);
            lstMotivosMail.Name = "lstMotivosMail";
            lstMotivosMail.Size = new Size(472, 124);
            lstMotivosMail.TabIndex = 3;
            // 
            // txtComentariosMail
            // 
            txtComentariosMail.Location = new Point(38, 338);
            txtComentariosMail.Margin = new Padding(3, 4, 3, 4);
            txtComentariosMail.Name = "txtComentariosMail";
            txtComentariosMail.Size = new Size(321, 27);
            txtComentariosMail.TabIndex = 4;
            txtComentariosMail.TextChanged += txtComentariosMail_TextChanged;
            // 
            // lblDestinatariosMail
            // 
            lblDestinatariosMail.AutoSize = true;
            lblDestinatariosMail.Location = new Point(33, 402);
            lblDestinatariosMail.Name = "lblDestinatariosMail";
            lblDestinatariosMail.Size = new Size(96, 20);
            lblDestinatariosMail.TabIndex = 5;
            lblDestinatariosMail.Text = "Destinatarios";
            // 
            // PantallaMail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(556, 481);
            Controls.Add(lblDestinatariosMail);
            Controls.Add(txtComentariosMail);
            Controls.Add(lstMotivosMail);
            Controls.Add(lblFechaHoraActualMail);
            Controls.Add(lblNombreEstadoMail);
            Controls.Add(lblIdSismografoMail);
            Margin = new Padding(3, 4, 3, 4);
            Name = "PantallaMail";
            Text = "PantallaMail";
            Load += PantallaMail_Load;
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