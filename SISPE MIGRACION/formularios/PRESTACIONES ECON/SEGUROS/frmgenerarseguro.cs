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
    public partial class frmgenerarseguro : Form
    {
        private bool opcion;
        public frmgenerarseguro()
        {
            InitializeComponent();
            this.opcion = opcion;
            
        }

        private void frmgenerarseguro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                this.Close();
        }

        private void frmgenerarseguro_Load(object sender, EventArgs e)
        {
            button1.Focus();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radio1.Checked)
            {
                
                DateTime fec1 = fecha1.Value;
                string c1 = string.Format("{0}-{1}-{2}", fec1.Year, fec1.Month, fec1.Day);


                //quirografarios y hipotecarios
                string query = "select folio,rfc,nombre_em,num_desc,imp_unit,secretaria,gpo_edad,SUBSTRING (rfc,5,2) AS extrae from datos.s_quirog";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                object[][] aux2 = new object[resultado.Count][];
                int contador = 0;


                string qqq = "";

                foreach (Dictionary<string, object> item in resultado)
                {
                    double folio = 0.00;
                    string rfc = string.Empty;
                    string nombre_em = string.Empty;
                    double num_desc = 0;
                    double imp_unit = 0;
                    string secretaria = string.Empty;
                    string extrae, final, inicio;
                    int calculo;

                    try
                    {


                        folio = Convert.ToDouble(item["folio"]);
                        rfc = Convert.ToString(item["rfc"]);
                        nombre_em = Convert.ToString(item["nombre_em"]);
                        num_desc = Convert.ToDouble(item["num_desc"]);
                        imp_unit = Convert.ToDouble(item["imp_unit"]);
                        secretaria = Convert.ToString(item["secretaria"]);
                        extrae = Convert.ToString(item["extrae"]);
                        final = "19" + extrae;
                        inicio = Convert.ToString(fec1.Year);
                        calculo = Convert.ToInt32(inicio) - Convert.ToInt32(final);
                        item["gpo_edad"] = calculo;
                        qqq += $" update datos.s_quirog set gpo_edad = {calculo} where folio = {folio}; ";
                        object[] tt1 = { rfc, nombre_em,  calculo };
                        aux2[contador] = tt1;
                        contador++;




                    }
                    catch
                    {

                    }
                    globales.consulta(qqq, true);
                   

                }
                globales.reportes("reposeguro", "seguros_r", aux2);
            }
            else
            {
              
                DateTime fec1 = fecha1.Value;
                string c1 = string.Format("{0}-{1}-{2}", fec1.Year, fec1.Month, fec1.Day);


                //hipotecarios
                string query = "select folio,rfc,nombre_em,num_desc,import_uni,secretaria,gpo_edad,SUBSTRING (rfc,5,2) AS extrae from datos.s_hipote";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                object[] aux2 = new object[resultado.Count];
                int contador = 0;


                string qqq = "";

                foreach (Dictionary<string, object> item in resultado)
                {
                    double folio = 0.00;
                    string rfc = string.Empty;
                    string nombre_em = string.Empty;
                    double num_desc = 0;
                    double import_uni = 0;
                    string secretaria = string.Empty;
                    string extrae, final, inicio;
                    int calculo;

                    try
                    {


                        folio = Convert.ToDouble(item["folio"]);
                        rfc = Convert.ToString(item["rfc"]);
                        nombre_em = Convert.ToString(item["nombre_em"]);
                        num_desc = Convert.ToDouble(item["num_desc"]);
                        import_uni = Convert.ToDouble(item["import_uni"]);
                        secretaria = Convert.ToString(item["secretaria"]);
                        extrae = Convert.ToString(item["extrae"]);
                        final = "19" + extrae;
                        inicio = Convert.ToString(fec1.Year);
                        calculo = Convert.ToInt32(inicio) - Convert.ToInt32(final);
                        item["gpo_edad"] = calculo;
                        qqq += $" update datos.s_hipote set gpo_edad = {calculo} where folio = {folio}; ";
                        object[] tt1 = { folio, nombre_em, num_desc, import_uni, secretaria, calculo };
                        aux2[contador] = tt1;
                        contador++;
                        this.Cursor = Cursors.Default;
                        object[] parametros = { "fech1", "fech2" };
                    }
                    catch
                    {

                    }
                    globales.consulta(qqq, true);
                    globales.reportes("reposeguro", "seguros_r", aux2);

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
