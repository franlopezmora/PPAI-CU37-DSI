using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPAICU37
{
    // Asume que tienes los controles: lblIdSismografoMail, lblNombreEstadoMail, 
    // lblFechaHoraActualMail, lstMotivosMail (ListBox), txtComentariosMail (TextBox o Label),
    // lblDestinatariosMail, btnOkMail.
    public partial class PantallaMail : Form
    {
        public PantallaMail()
        {
            InitializeComponent();
        }

        public void CargarDatos(string idSismografo, string nombreEstado, DateTime fechaHora, List<MotivoFueraServicio> motivos, string observacionCierre, string destinatarios)
        {
            // Asignar a los controles de la UI, ej:
            // identificacionSismografo (Label en diagrama) -> lblIdSismografoMail.Text = idSismografo;
            // ... y así sucesivamente para los demás, similar a PantallaCCRS
            // lblDestinatariosMail.Text = $"Simulación de envío a: {destinatarios}";

            // Ejemplo con nombres de control supuestos:
            lblIdSismografoMail.Text = $"Sismógrafo: {idSismografo}";
            lblNombreEstadoMail.Text = $"Estado: {nombreEstado}";
            lblFechaHoraActualMail.Text = $"Fecha y Hora: {fechaHora:g}";

            lstMotivosMail.Items.Clear();
            if (motivos != null && motivos.Any())
            {
                foreach (var motivo in motivos)
                {
                    lstMotivosMail.Items.Add($"Motivo: {motivo.Tipo.Descripcion} - Comentario: {motivo.Comentario}");
                }
            }
            else
            {
                lstMotivosMail.Items.Add("Sin motivos detallados.");
            }
            txtComentariosMail.Text = $"Observación Cierre Orden: {observacionCierre}";
            lblDestinatariosMail.Text = $"Simulación de envío a: {destinatarios}";

            enviarMail(); // Simula la acción de enviar [cite: 1]
        }

        // enviarMail() [cite: 1]
        private void enviarMail()
        {
            // La "simulación" principal ya ocurrió en el controlador.
            // Esta pantalla solo muestra la información que se "enviaría".
            // Puedes agregar un log o confirmación visual aquí si es necesario.
            Console.WriteLine("PantallaMail: Visualizando datos para el mail simulado.");
        }

        private void btnOkMail_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
