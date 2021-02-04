using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Seguros
{
    public partial class frmactquiro : Form
    {
        private bool opcion;
        private string dato;
        public frmactquiro(bool opcion)
        {
            InitializeComponent();
            this.opcion = opcion;
        }

        private void frmactquiro_Load(object sender, EventArgs e)
        {
            label1.Focus();
           
            

            if (this.opcion)
            {
                this.dato = "s_quirog";
                label1.Text = "PRESTACIONES ECONÓMICAS / SEGUROS / ACTUALIZAR QUIROGRAFARIOS";
                string query = "select folio,rfc,nombre_em,num_desc,imp_unit,secretaria,gpo_edad from datos.s_quirog ";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                foreach (var item in resultado)
                {
                    string folio = Convert.ToString(item["folio"]);
                    string rfc = Convert.ToString(item["rfc"]);
                    string nombre_em = Convert.ToString(item["nombre_em"]);
                    string num_desc = Convert.ToString(item["num_desc"]);
                    string import_uni = Convert.ToString(item["imp_unit"]);
                    string secretaria = Convert.ToString(item["secretaria"]);
                    string gpo_edad = Convert.ToString(item["gpo_edad"]);
                    datagr.Rows.Add(folio, rfc, nombre_em, num_desc, import_uni, secretaria, gpo_edad);
                }
            }
            else
            {
                dato = "s_hipote";
                label1.Text = "PRESTACIONES ECONÓMICAS / SEGUROS / ACTUALIZAR QUIROGRAFARIOS";
                string query = "select folio,rfc,nombre_em,num_desc,import_uni,secretaria,gpo_edad from datos.s_hipote ";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                foreach (var item in resultado)
                {
                    string folio = Convert.ToString(item["folio"]);
                    string rfc = Convert.ToString(item["rfc"]);
                    string nombre_em = Convert.ToString(item["nombre_em"]);
                    string num_desc = Convert.ToString(item["num_desc"]);
                    string import_uni = Convert.ToString(item["import_uni"]);
                    string secretaria = Convert.ToString(item["secretaria"]);
                    string gpo_edad = Convert.ToString(item["gpo_edad"]);
                    datagr.Rows.Add(folio, rfc, nombre_em, num_desc, import_uni, secretaria, gpo_edad);
                }
            }

        }

        private void frmactquiro_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void frmactquiro_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogo = MessageBox.Show("¿Desea cerrar el módulo?",
       "Cerrar el módulo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogo == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void datagr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.Close();
            }
           
        }

        private void datagr_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int c = e.RowIndex;

            }

            catch
            {


            }
        }

        private void datagr_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            int c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = datagr.Rows[c];
            //folio,rfc,nombre_em,num_desc,import_uni,secretaria,gpo_edad
            string folio = Convert.ToString(row.Cells[0].Value);
            string rfc = Convert.ToString(row.Cells[1].Value);
            string nombre_em = Convert.ToString(row.Cells[2].Value);
            string num_desc = Convert.ToString(row.Cells[3].Value);
            string imp_unit = Convert.ToString(row.Cells[4].Value);
            string secretaria = Convert.ToString(row.Cells[5].Value);
            string gpo_edad = Convert.ToString(row.Cells[6].Value);

            string query = "UPDATE datos.{0} SET rfc = '{1}', nombre_em = '{2}', num_desc = {3}, imp_unit = '{4}', secretaria = '{5}', gpo_edad = '{6}' WHERE  folio = {7}";
            string convierte = string.Format(query, this.dato, rfc, nombre_em, num_desc, imp_unit, secretaria, gpo_edad, folio);

            if (globales.consulta(convierte, true))
            {
                MessageBox.Show("REGISTRO  MODIFICADO");

            }

            else
                MessageBox.Show("Existe un error en tipo de datos contacte a sistemas");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
    }


