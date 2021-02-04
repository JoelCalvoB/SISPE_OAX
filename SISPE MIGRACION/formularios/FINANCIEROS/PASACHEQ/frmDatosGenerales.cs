using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.FINANCIEROS.CHEQUES
{
    public partial class frmDatosGenerales : Form
    {
        public frmDatosGenerales()
        {
            InitializeComponent();
        }

        private void dtggrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void group_Enter(object sender, EventArgs e)
        {

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = globales.MessageBoxQuestion("¿Desea realizar la operación?","Aviso", globales.menuPrincipal);
            if (resultado == DialogResult.No) return;


            if (string.IsNullOrWhiteSpace(txtFecha.Text)) {
                globales.MessageBoxExclamation("No puede ir fecha vacía","Aviso",globales.menuPrincipal);
                return;
            }
            try
            {

                string query = "delete from financieros.folios";
                globales.consulta(query, true);
                query = string.Empty;
                foreach (DataGridViewRow item in datos.Rows)
                {
                    string folio1 = string.IsNullOrWhiteSpace(Convert.ToString(item.Cells[0].Value)) ? "null" : Convert.ToString(item.Cells[0].Value);
                    string folio2 = string.IsNullOrWhiteSpace(Convert.ToString(item.Cells[1].Value)) ? "null" : Convert.ToString(item.Cells[1].Value);
                    if (folio1 == "null" && folio2 == "null")
                        continue;

                    query += $"insert into financieros.folios values ({folio1},{folio2});";
                }

                globales.consulta(query, true);

                query = $"update financieros.datos set numcheque = {txtNumcheque.Text},numpoliz = {txtNumpoliz.Text},fecha = '{string.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtFecha.Text))}',banco = '{txtBanco.Text}',chequera = '{txtChequera.Text}',concep1 = '{txtConcept1.Text}',elaboro = '{txtElaboro.Text}',reviso = '{txtReviso.Text}',autorizo = '{txtAutorizo.Text}',ctapq = '{txtctapq.Text}',ctafg = '{txtFg.Text}',ctain = '{txtctain.Text}',ctaba = '{txtCtaban.Text}',ctadebe='{txtDebe.Text}',ctahaber='{txthaber.Text}'";
                globales.consulta(query, true);

                globales.MessageBoxSuccess("Información de cheques actualizada correctamente", "Información actualizada", globales.menuPrincipal);


            }
            catch {
                globales.MessageBoxExclamation("Error en campos, verifique fechas","Aviso",globales.menuPrincipal);
            }


        }

        private void frmDatosGenerales_Load(object sender, EventArgs e)
        {
            string query = "select * from financieros.datos";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count != 0) {
                txtNumcheque.Text = Convert.ToString(resultado[0]["numcheque"]);
                txtNumpoliz.Text = Convert.ToString(resultado[0]["numpoliz"]);
                txtFecha.Text = Convert.ToString(resultado[0]["fecha"]);

                txtBanco.Text = Convert.ToString(resultado[0]["banco"]);
                txtChequera.Text = Convert.ToString(resultado[0]["chequera"]);
                txtConcept1.Text = Convert.ToString(resultado[0]["concep1"]);

                txtElaboro.Text = Convert.ToString(resultado[0]["elaboro"]);
                txtReviso.Text = Convert.ToString(resultado[0]["reviso"]);
                txtAutorizo.Text = Convert.ToString(resultado[0]["autorizo"]);

                txtctapq.Text = Convert.ToString(resultado[0]["ctapq"]);
                txtFg.Text = Convert.ToString(resultado[0]["ctafg"]);
                txtctain.Text = Convert.ToString(resultado[0]["ctain"]);
                txtCtaban.Text = Convert.ToString(resultado[0]["ctaba"]);
                txtDebe.Text = Convert.ToString(resultado[0]["ctadebe"]);
                txthaber.Text = Convert.ToString(resultado[0]["ctahaber"]);

                query = "select * from financieros.folios";
                resultado = globales.consulta(query);
                resultado.ForEach(o => datos.Rows.Add(o["desde"],o["hasta"]));

            }



        }

        private void frmDatosGenerales_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

        private void frmDatosGenerales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void frmDatosGenerales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) {
                this.Close();
            }

            if (e.KeyCode==Keys.F9)
            {
                btnGuardar_Click(null,null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas cerrar el modulo?", globales.menuPrincipal);
            if (p == DialogResult.Yes)
                this.Close();
        }

        private void frmDatosGenerales_Shown(object sender, EventArgs e)
        {
            txtNumcheque.Select();
        }
    }
}
