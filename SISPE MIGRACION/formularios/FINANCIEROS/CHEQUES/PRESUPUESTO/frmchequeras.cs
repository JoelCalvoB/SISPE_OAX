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

namespace SISPE_MIGRACION.formularios.FINANCIEROS.CHEQUES.PRESUPUESTO
{
    public partial class frmchequeras : Form
    {
        private string id, banco,chequera;

        internal enviarDatos enviar;
        private List<Dictionary<string, object>> resultado;

        public frmchequeras()
        {
            InitializeComponent();
            this.KeyPreview = true;

            this.KeyPress += new KeyPressEventHandler(frmchequeras_KeyPress);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Dictionary<string, object> valor = new Dictionary<string, object>();
            valor.Add("id", "");
            valor.Add("banco", "");
            valor.Add("chequera", "");

            if (e.KeyCode == Keys.Enter)
            {

                Close();
                valor["id"] = this.id;
                valor["banco"] = this.banco;
                valor["chequera"] = this.chequera;

                enviar(valor, true);

            }

            if (e.KeyCode==Keys.F9)
            {
                string query;
                query = "delete from financieros.chequeras_presupuesto";
                globales.consulta(query);

                foreach (DataGridViewRow row in datos.Rows)

                {
                    string banco = Convert.ToString(row.Cells[1].Value);
                    string chequera = Convert.ToString(row.Cells[2].Value);

                    query = "insert into financieros.chequeras_presupuesto (banco,chequera) values ('{0}','{1}')";
                    string paso = string.Format(query, banco, chequera);
                    globales.consulta(paso);
                }
                DialogResult dialog = globales.MessageBoxSuccess("CAMBIOS GUARDADOS CON ÉXITO", "CORRECTO", globales.menuPrincipal);
                datos.Rows.Clear();
                llenado();
            }
        }

        private void frmchequeras_Load(object sender, EventArgs e)
        {
            llenado();
        }

        private void frmchequeras_KeyPress(object sender, KeyPressEventArgs e)
        {
            char S;

            S = Char.ToUpper(e.KeyChar);

            e.KeyChar = S;

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = datos.Rows[c];
            id = Convert.ToString(row.Cells[0].Value);
            banco = Convert.ToString(row.Cells[1].Value);
            chequera = Convert.ToString(row.Cells[2].Value);

        }

        private void llenado ()
        {
            datos.Rows.Clear();
            string query = "select * from financieros.chequeras_presupuesto order by id";
            List<Dictionary<string, object>> resul = globales.consulta(query);
            foreach (var item in resul)
            {
                string id = Convert.ToString(item["id"]);
                string banco = Convert.ToString(item["banco"]);
                string chequera = Convert.ToString(item["chequera"]);
                datos.Rows.Add(id, banco, chequera);

            }
        }
    }
}
