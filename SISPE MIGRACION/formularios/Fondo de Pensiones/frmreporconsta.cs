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
    public partial class frmreporconsta : Form
    {
        public frmreporconsta()
        {
            InitializeComponent();
        }

        private void frmreporconsta_Load(object sender, EventArgs e)
        {
            //DateTime fec1 = DateTime.Parse(fecha1.Text);
            //DateTime fec2 = DateTime.Parse(fecha2.Text);
          

            //DateTime auxFecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
            //fecha1.Text = string.Format("{0:d}", auxFecha);

            //auxFecha = auxFecha.AddMonths(1);
            //auxFecha = auxFecha.AddDays(-1);

            //fecha2.Text = string.Format("{0:d}", auxFecha);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fec1 = DateTime.Parse(fecha1.Text);
            DateTime fec2 = DateTime.Parse(fecha2.Text);

            string c1 = string.Format("{0:yyyy-MM-dd}", fec1);
            string c2 = string.Format("{0:yyyy-MM-dd}", fec2);

            string query = "SELECT a1.rfc, b2.nombre_em, a1.num_histo, a1.num_const, a1.num_recib, a1.fecha_exp, a1.monto_total FROM datos.constancias a1  LEFT JOIN datos.empleados b2 ON a1.rfc=b2.rfc WHERE	fecha_exp BETWEEN '{0}' AND '{1}' ORDER BY num_const";
            string convierte = string.Format(query, string.Format("{0:yyyy-MM-dd}",fec1), string.Format("{0:yyyy-MM-dd}", fec2));
            List<Dictionary<string, object>> resultado = globales.consulta(convierte);
            object[] aux2 = new object[resultado.Count];
            int contador = 0;

            foreach (Dictionary<string, object> item in resultado)
            {
                string rfc = string.Empty;
                string nombre_em = string.Empty;
                string num_histo = string.Empty;
                string num_const = string.Empty;
                string num_recib = string.Empty;
                string fecha_exp = string.Empty;
                string monto_total = string.Empty;
            
            try
            {
                    rfc = Convert.ToString(item["rfc"]);
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    num_histo = Convert.ToString(item["num_histo"]);
                    num_const = Convert.ToString(item["num_const"]);
                    num_recib = Convert.ToString(item["num_recib"]);
                    fecha_exp = Convert.ToString(item["fecha_exp"]).Replace("12:00:00 a. m.", ""); ;
                    monto_total = Convert.ToString(item["monto_total"]);

            }

           catch
            {

            }
                object[] tt1 = { rfc,nombre_em, num_histo  ,num_const, num_recib,fecha_exp,monto_total};
                aux2[contador] = tt1;
                contador++;
            }

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[] parametros = { "fech1", "fech2", "logo" };
            object[] valor = { fecha1.Text, fecha2.Text , logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reporteconstancias", "reporte_consta", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fecha1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                fecha2.Select();
                fecha2.Focus();
            }
        }

        private void fecha2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                DateTime fec1 = DateTime.Parse(fecha1.Text);
                DateTime fec2 = DateTime.Parse(fecha2.Text);

                string c1 = string.Format("{0:yyyy-MM-dd}", fec1);
                string c2 = string.Format("{0:yyyy-MM-dd}", fec2);

                string query = "SELECT a1.rfc, b2.nombre_em, a1.num_histo, a1.num_const, a1.num_recib, a1.fecha_exp, a1.monto_total FROM datos.constancias a1  LEFT JOIN datos.empleados b2 ON a1.rfc=b2.rfc WHERE	fecha_exp BETWEEN '{0}' AND '{1}' ORDER BY num_const";
                string convierte = string.Format(query, string.Format("{0:yyyy-MM-dd}", fec1), string.Format("{0:yyyy-MM-dd}", fec2));
                List<Dictionary<string, object>> resultado = globales.consulta(convierte);
                object[] aux2 = new object[resultado.Count];
                int contador = 0;

                foreach (Dictionary<string, object> item in resultado)
                {
                    string rfc = string.Empty;
                    string nombre_em = string.Empty;
                    string num_histo = string.Empty;
                    string num_const = string.Empty;
                    string num_recib = string.Empty;
                    string fecha_exp = string.Empty;
                    string monto_total = string.Empty;

                    try
                    {
                        rfc = Convert.ToString(item["rfc"]);
                        nombre_em = Convert.ToString(item["nombre_em"]);
                        num_histo = Convert.ToString(item["num_histo"]);
                        num_const = Convert.ToString(item["num_const"]);
                        num_recib = Convert.ToString(item["num_recib"]);
                        fecha_exp = Convert.ToString(item["fecha_exp"]).Replace("12:00:00 a. m.", ""); ;
                        monto_total = Convert.ToString(item["monto_total"]);

                    }

                    catch
                    {

                    }
                    object[] tt1 = { rfc, nombre_em, num_histo, num_const, num_recib, fecha_exp, monto_total };
                    aux2[contador] = tt1;
                    contador++;
                }

                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                object[] parametros = { "fech1", "fech2" , "logo" };
                object[] valor = { fecha1.Text, fecha2.Text, logo };
                object[][] enviarParametros = new object[2][];

                enviarParametros[0] = parametros;
                enviarParametros[1] = valor;

                globales.reportes("reporteconstancias", "reporte_consta", aux2, "", false, enviarParametros);
                this.Cursor = Cursors.Default;
            }
        }

        private void frmreporconsta_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            genera();

        }

        private void genera()
        {
            DateTime fec1 = DateTime.Parse(fecha1.Text);
            DateTime fec2 = DateTime.Parse(fecha2.Text);

            string c1 = string.Format("{0:yyyy-MM-dd}", fec1);
            string c2 = string.Format("{0:yyyy-MM-dd}", fec2);

            string query = "SELECT a1.rfc, b2.nombre_em, a1.num_histo, a1.num_const, a1.num_recib, a1.fecha_exp, a1.monto_total,a1.motivo FROM datos.constancias a1  LEFT JOIN datos.empleados b2 ON a1.rfc=b2.rfc WHERE	fecha_exp BETWEEN '{0}' AND '{1}' ORDER BY num_const";
            string convierte = string.Format(query, string.Format("{0:yyyy-MM-dd}", fec1), string.Format("{0:yyyy-MM-dd}", fec2));
            List<Dictionary<string, object>> resultado = globales.consulta(convierte);
            object[] aux2 = new object[resultado.Count];
            int contador = 0;

            foreach (Dictionary<string, object> item in resultado)
            {
                string rfc = string.Empty;
                string nombre_em = string.Empty;
                string num_histo = string.Empty;
                string num_const = string.Empty;
                string num_recib = string.Empty;
                string fecha_exp = string.Empty;
                double monto_total = 0;
                string motivo = string.Empty;

                try
                {
                    rfc = Convert.ToString(item["rfc"]);
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    num_histo = Convert.ToString(item["num_histo"]);
                    num_const = Convert.ToString(item["num_const"]);
                    num_recib = Convert.ToString(item["num_recib"]);
                    fecha_exp = Convert.ToString(item["fecha_exp"]).Replace("12:00:00 a. m.", ""); ;
                    monto_total = globales.convertDouble(Convert.ToString(item["monto_total"]));
                    motivo = Convert.ToString(item["motivo"]);

                }

                catch
                {

                }
                object[] tt1 = { rfc, nombre_em, num_histo, num_const, num_recib, fecha_exp, monto_total,motivo };
                aux2[contador] = tt1;
                contador++;
            }


            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[] parametros = { "fech1", "fech2","logo" };
            object[] valor = { fecha1.Text, fecha2.Text,logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reporteconstancias", "reporte_consta", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;
        }

        private void frmreporconsta_Shown(object sender, EventArgs e)
        {
            fecha1.Select();
        }

        private void fecha1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                fecha2.Select();
            }
        }

        private void fecha2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                genera();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
