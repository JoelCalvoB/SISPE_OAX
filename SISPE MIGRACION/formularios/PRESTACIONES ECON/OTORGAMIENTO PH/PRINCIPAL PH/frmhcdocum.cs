using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.PRINCIPAL_PH
{
    public partial class frmhcdocum : Form
    {
        internal enviarDatos enviar;
        private List<Dictionary<string, object>> resultado;
        private string cve_docum, descripcion, id;

        public frmhcdocum()
        {
            InitializeComponent();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmhcdocum_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void datos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = datos.Rows[c];
            cve_docum = Convert.ToString(row.Cells[0].Value);
            descripcion = Convert.ToString(row.Cells[1].Value);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string query = string.Format("select * from datos.c_hdocum where cve_docum like '{0}%'", txtBusqueda.Text);
            resultado = baseDatos.consulta(query);
            datos.Rows.Clear();
            foreach (Dictionary<string, object> item in resultado)
            {
                string cve_docum = Convert.ToString(item["cve_docum"]);
                string documento = Convert.ToString(item["documento"]);
                datos.Rows.Add(cve_docum, documento);
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
            if (e.KeyChar == 13)
            {
                Dictionary<string, object> valor = new Dictionary<string, object>();
                valor.Add("cve_docum", "");
                valor.Add("descripcion", "");

                Close();
                valor["cve_docum"] = this.cve_docum;
                valor["descripcion"] = this.descripcion;
                enviar(valor, true);
            }
        }

        private void frmhcdocum_Load(object sender, EventArgs e)
        {
            txtBusqueda.Select();
            string llena = "SELECT * FROM datos.c_hdocum";
            List<Dictionary<string, object>> lista = globales.consulta(llena);
            foreach ( var item in lista)
            {
                string cve_docum = Convert.ToString(item["cve_docum"]);
                string documento = Convert.ToString(item["documento"]);
                datos.Rows.Add(cve_docum,documento);
            }
        }

        private void datos_KeyDown(object sender, KeyEventArgs e)
        {
          
            if (e.KeyCode == Keys.Enter)
            {
                Dictionary<string, object> valor = new Dictionary<string, object>();
                valor.Add("cve_docum", "");
                valor.Add("descripcion", "");

                Close();
                valor["cve_docum"] = this.cve_docum;
                valor["descripcion"] = this.descripcion;
                enviar(valor, true);
            }
        }
    }
}
