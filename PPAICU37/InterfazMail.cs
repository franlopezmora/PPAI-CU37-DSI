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
    public partial class InterfazMail : Form
    {
        public InterfazMail()
        {
            InitializeComponent();
        }

        public void enviarMail(string idSismografo, string nombreEstado, DateTime fechaHora, List<Tuple<string, MotivoTipo>> listaMotivosTipoComentarios, string observacionCierre, string destinatarios)
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
            if (listaMotivosTipoComentarios != null && listaMotivosTipoComentarios.Any())
            {
                foreach (var tupla in listaMotivosTipoComentarios)
                {
                    string comentario = tupla.Item1;
                    string descripcion = tupla.Item2.Descripcion;
                    lstMotivosMail.Items.Add($"{descripcion}: {comentario}");
                }
            }
            else
            {
                lstMotivosMail.Items.Add("Sin motivos detallados.");
            }
            txtComentariosMail.Text = $"Observación Cierre Orden: {observacionCierre}";
            lblDestinatariosMail.Text = $"Simulación de envío a: {destinatarios}";
            Console.WriteLine("PantallaMail: Visualizando datos para el mail simulado.");
        }

        private void btnOkMail_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
