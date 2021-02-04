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
    public partial class frmultimoasegurado : Form
    {

        public frmultimoasegurado()
        {
            InitializeComponent();
            llenagrid();
            dataGridView1.ReadOnly = false;
        }

        private void frmultimoasegurado_Load(object sender, EventArgs e)
        {

        }


        private  void llenagrid ()
        {
            string query = "SELECT * FROM DATOS.i_quirog order by folio";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            try
            {
                foreach (var item in resultado)
                {
                    string folio = Convert.ToString(item["folio"]);
                    string rfc = Convert.ToString(item["rfc"]);
                    string nombre_em = Convert.ToString(item["nombre_em"]);
                    string proyecto = Convert.ToString(item["proyecto"]);
                    string secretaria = Convert.ToString(item["secretaria"]);
                    string f_emischeq = Convert.ToString(item["f_emischeq"]).Replace("12:00:00 a. m.", ""); ;
                    string f_primdesc = Convert.ToString(item["f_primdesc"]).Replace("12:00:00 a. m.", ""); ;
                    string antig_q = Convert.ToString(item["antig_q"]);
                    string plazo = Convert.ToString(item["plazo"]);
                    string imp_unit = Convert.ToString(item["imp_unit"]);

                    dataGridView1.Rows.Add(folio,rfc,nombre_em,proyecto,secretaria,f_emischeq,f_primdesc,antig_q,plazo,imp_unit);
                }
            }
            catch
            {

            }

        }

        private void frmultimoasegurado_FormClosing(object sender, FormClosingEventArgs e)
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

        private void frmultimoasegurado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
    }
