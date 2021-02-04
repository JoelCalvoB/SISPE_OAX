using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones
{
    public partial class frmreporlisaporta : Form
    {
        public frmreporlisaporta()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmreporlisaporta_KeyDown(object sender, KeyEventArgs e)
        {
          if (e.KeyCode == Keys.F2)
            {
                button2_Click(null, null);
            }
        }

        private void frmreporlisaporta_FormClosing(object sender, FormClosingEventArgs e)
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            metodo();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmreporlisaporta_Shown(object sender, EventArgs e)
        {
            button1.Select();
        }

        private void metodo()
        {
            Cursor.Current = Cursors.WaitCursor;
            string query = "begin;" +
    " delete from datos.reporte_aportacion;" +
            " insert into datos.reporte_aportacion select rfc,max(final) as ffinal from datos.aportaciones where new_tipo like 'A%' and rfc <> '' and extract(year from inicio)>= 2008 and coalesce(trim(status), '')<> 'p' and coalesce(trim(status), '')<> 'e' group by rfc order by rfc;" +
            " end;";
            List<Dictionary<string, object>> resultado1 = globales.consulta(query);

            string query2 = "select * from datos.reporte_aportaciones";
            List<Dictionary<string, object>> resultado2 = globales.consulta(query2);
            object[] aux2 = new object[resultado2.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado2)
            {
                string nombre_em = string.Empty;
                string rfc = string.Empty;
                string proyecto = string.Empty;
                string cve_categ = string.Empty;
                string sueldo_base = string.Empty;
                string tipo_rel = string.Empty;
                string depe = string.Empty;
                string rpe = string.Empty;
                string ffinal = string.Empty;

                try
                {
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    rfc = Convert.ToString(item["rfc"]);
                    proyecto = Convert.ToString(item["proyecto"]);
                    cve_categ = Convert.ToString(item["cve_categ"]);
                    sueldo_base = Convert.ToString(item["sueldo_base"]);
                    tipo_rel = Convert.ToString(item["tipo_rel"]);
                    depe = Convert.ToString(item["depe"]);
                    rpe = Convert.ToString(item["rpe"]);
                    ffinal = Convert.ToString(item["ffinal"]).Replace("12:00:00 a. m.", "");


                }
                catch
                {

                }
                object[] tt1 = { nombre_em, rfc, proyecto, cve_categ, sueldo_base, tipo_rel, depe, rpe, ffinal };
                aux2[contador] = tt1;
                contador++;

            }


            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[][] parametros = new object[2][];
            object[] header = { "logo" };
            object[] body = { logo};

            parametros[0] = header;
            parametros[1] = body;

            globales.reportes("frmrepoaportadores", "r_aporta", aux2,"",false,parametros);
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { metodo(); }
        }
    }
}
