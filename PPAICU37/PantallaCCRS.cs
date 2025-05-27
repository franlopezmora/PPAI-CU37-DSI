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
    public partial class PantallaCCRS : Form
    {
        public PantallaCCRS()
        {
            InitializeComponent();
        }

        // actualizarMonitor() [cite: 1] - Adaptado para cargar datos
        public void CargarDatos(string idSismografo, string nombreEstado, DateTime fechaHora, List<MotivoFueraServicio> motivos, string observacionCierre)
        {
            // Asignar a los controles de la UI, ej:
            // identificacionSismografo (Label en diagrama) -> lblIdSismografo.Text = idSismografo;
            // nombreEstado (Label en diagrama) -> lblNombreEstado.Text = nombreEstado;
            // fechaHoraActual (Label en diagrama) -> lblFechaHoraActual.Text = fechaHora.ToString("g");
            // listaMotivo (ListBox/DataGridView en diagrama) -> lstMotivos.Items.Clear();
            // foreach(var motivo in motivos) { lstMotivos.Items.Add($"{motivo.Tipo.Descripcion}: {motivo.Comentario}"); }
            // comentarios (Label/TextBox en diagrama) -> txtComentariosAdicionales.Text = observacionCierre;

            // Ejemplo con nombres de control supuestos:
            lblIdSismografo.Text = $"Sismógrafo: {idSismografo}";
            lblNombreEstado.Text = $"Estado: {nombreEstado}";
            lblFechaHoraActual.Text = $"Fecha y Hora: {fechaHora:g}";

            lstMotivos.Items.Clear();
            if (motivos != null && motivos.Any())
            {
                foreach (var motivo in motivos)
                {
                    lstMotivos.Items.Add($"Motivo: {motivo.Tipo.Descripcion} - Comentario: {motivo.Comentario}");
                }
            }
            else
            {
                lstMotivos.Items.Add("Sin motivos detallados.");
            }
            txtComentariosAdicionales.Text = $"Observación Cierre Orden: {observacionCierre}";
        }

        private void btnOkCCRS_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
