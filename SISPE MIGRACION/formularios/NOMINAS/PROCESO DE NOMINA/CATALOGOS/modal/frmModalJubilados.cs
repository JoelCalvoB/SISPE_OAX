using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.CATALOGOS.modal
{
    public partial class frmModalJubilados : Form
    {
        private string rfc;
        internal metodoEnviar enviar;
        private string jpp;
        private string num;
        private string otro;

        private bool soloPëa {
            get; set; }

        public frmModalJubilados(bool soloPea = false)
        {
            InitializeComponent();
            this.soloPëa = soloPea;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void btnGuardarSecundario_Click(object sender, EventArgs e)
        {
            aceptar();
        }

        private void frmModalJubilados_Load(object sender, EventArgs e)
        {

            otro = string.Empty;
            otro = this.soloPëa ? " and jpp <> 'PEA' ":"";

            string query = $"select * from nominas_catalogos.maestro where superviven='S' {otro} order by jpp asc, num asc limit 100";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            resultado.ForEach(o => {
                dtggrid.Rows.Add(o["jpp"], o["num"], o["rfc"], o["nombre"]);
            });
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

            dtggrid.Rows.Clear();
            string query = $"select * from nominas_catalogos.maestro where (concat(jpp,num) like '%{txtBuscar.Text}%' or rfc like '%{txtBuscar.Text}%' or nombre like '%{txtBuscar.Text}%') AND superviven='S' {this.otro} order by jpp asc, num asc  limit 100";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            resultado.ForEach(o => {
                dtggrid.Rows.Add(o["jpp"], o["num"], o["rfc"], o["nombre"]);
            });
        }

        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            rfc = Convert.ToString(dtggrid.Rows[e.RowIndex].Cells[2].Value);
            this.num = Convert.ToString(dtggrid.Rows[e.RowIndex].Cells[1].Value);
            this.jpp = Convert.ToString(dtggrid.Rows[e.RowIndex].Cells[0].Value);
        }
    

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{TAB}");
                SendKeys.Send("{DOWN}");
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                aceptar();
            }
        }

        private void aceptar()
        {
            string query = $"select * from nominas_catalogos.maestro where rfc = '{this.rfc}' and num = {num} and jpp = '{this.jpp}' {otro} order by jpp";
            List<Dictionary<string, object>> resultaod = globales.consulta(query);
            enviar(resultaod[0]);
            this.Owner.Close();
        }

        private void dtggrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                aceptar();
            }
        }

        private void dtggrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                if (e.KeyChar != 13) txtBuscar.Text += e.KeyChar.ToString();
                txtBuscar.Focus();
                txtBuscar.SelectionStart = txtBuscar.Text.Length;
            }
        }

        private void frmModalJubilados_Shown(object sender, EventArgs e)
        {
            txtBuscar.Focus();
        }
    }
}
