using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public void CargarDatos(string idSismo, string estado, DateTime fecha, List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario, string observacion, IEnumerable<Sismografo> todosLosSismografos)
        {
            // Asignar a los controles de la UI, ej:
            // identificacionSismografo (Label en diagrama) -> lblIdSismografo.Text = idSismografo;
            // nombreEstado (Label en diagrama) -> lblNombreEstado.Text = nombreEstado;
            // fechaHoraActual (Label en diagrama) -> lblFechaHoraActual.Text = fechaHora.ToString("g");
            // listaMotivo (ListBox/DataGridView en diagrama) -> lstMotivos.Items.Clear();
            // foreach(var motivo in motivos) { lstMotivos.Items.Add($"{motivo.Tipo.Descripcion}: {motivo.Comentario}"); }
            // comentarios (Label/TextBox en diagrama) -> txtComentariosAdicionales.Text = observacionCierre;

            // Ejemplo con nombres de control supuestos:

            lblIdSismografo.Text = $"Sismógrafo: {idSismo}";
            lblNombreEstado.Text = $"Estado: {estado}";
            lblFechaHoraActual.Text = $"Fecha y Hora: {fecha:g}";

            lstMotivos.Items.Clear();
            if (listaMotivosTipoComentario != null && listaMotivosTipoComentario.Any())
            {
                foreach (var tupla in listaMotivosTipoComentario)
                {
                    string comentario = tupla.Item1;
                    string descripcion = tupla.Item2.Descripcion;
                    lstMotivos.Items.Add($"{descripcion}: {comentario}");
                }
            }
            else
            {
                lstMotivos.Items.Add("Sin motivos detallados.");
            }
            lblComentarios.Text = $"Observación Cierre Orden: {observacion}";
        }

        private void btnOkCCRS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblIdSismografo_Click(object sender, EventArgs e)
        {

        }

        private void lstMotivos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
