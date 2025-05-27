namespace PPAICU37
{
    partial class PantallaMail
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
            btnOkMail = new Button();
            SuspendLayout();
            // 
            // lblIdSismografoMail
            // 
            lblIdSismografoMail.AutoSize = true;
            lblIdSismografoMail.Location = new Point(260, 63);
            lblIdSismografoMail.Name = "lblIdSismografoMail";
            lblIdSismografoMail.Size = new Size(38, 15);
            lblIdSismografoMail.TabIndex = 0;
            lblIdSismografoMail.Text = "label1";
            // 
            // lblNombreEstadoMail
            // 
            lblNombreEstadoMail.AutoSize = true;
            lblNombreEstadoMail.Location = new Point(260, 93);
            lblNombreEstadoMail.Name = "lblNombreEstadoMail";
            lblNombreEstadoMail.Size = new Size(38, 15);
            lblNombreEstadoMail.TabIndex = 1;
            lblNombreEstadoMail.Text = "label1";
            // 
            // lblFechaHoraActualMail
            // 
            lblFechaHoraActualMail.AutoSize = true;
            lblFechaHoraActualMail.Location = new Point(260, 121);
            lblFechaHoraActualMail.Name = "lblFechaHoraActualMail";
            lblFechaHoraActualMail.Size = new Size(38, 15);
            lblFechaHoraActualMail.TabIndex = 2;
            lblFechaHoraActualMail.Text = "label1";
            // 
            // lstMotivosMail
            // 
            lstMotivosMail.FormattingEnabled = true;
            lstMotivosMail.ItemHeight = 15;
            lstMotivosMail.Location = new Point(253, 158);
            lstMotivosMail.Name = "lstMotivosMail";
            lstMotivosMail.Size = new Size(120, 94);
            lstMotivosMail.TabIndex = 3;
            // 
            // txtComentariosMail
            // 
            txtComentariosMail.Location = new Point(257, 271);
            txtComentariosMail.Name = "txtComentariosMail";
            txtComentariosMail.Size = new Size(100, 23);
            txtComentariosMail.TabIndex = 4;
            // 
            // lblDestinatariosMail
            // 
            lblDestinatariosMail.AutoSize = true;
            lblDestinatariosMail.Location = new Point(279, 317);
            lblDestinatariosMail.Name = "lblDestinatariosMail";
            lblDestinatariosMail.Size = new Size(38, 15);
            lblDestinatariosMail.TabIndex = 5;
            lblDestinatariosMail.Text = "label1";
            // 
            // btnOkMail
            // 
            btnOkMail.Location = new Point(359, 340);
            btnOkMail.Name = "btnOkMail";
            btnOkMail.Size = new Size(75, 23);
            btnOkMail.TabIndex = 6;
            btnOkMail.Text = "button1";
            btnOkMail.UseVisualStyleBackColor = true;
            btnOkMail.Click += btnOkMail_Click;
            // 
            // PantallaMail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnOkMail);
            Controls.Add(lblDestinatariosMail);
            Controls.Add(txtComentariosMail);
            Controls.Add(lstMotivosMail);
            Controls.Add(lblFechaHoraActualMail);
            Controls.Add(lblNombreEstadoMail);
            Controls.Add(lblIdSismografoMail);
            Name = "PantallaMail";
            Text = "PantallaMail";
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
        private Button btnOkMail;
    }
}