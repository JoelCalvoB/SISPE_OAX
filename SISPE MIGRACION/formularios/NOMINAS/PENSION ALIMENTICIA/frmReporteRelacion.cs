using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PENSION_ALIMENTICIA
{
    public partial class frmReporteRelacion : Form
    {
        public frmReporteRelacion()
        {
            InitializeComponent();
        }

        private void frmReporteRelacion_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (rd1.Checked)
            {
                relacionPensionAlimenticia();
            }
            else if (rd2.Checked)
            {

            }
            else if (rd3.Checked)
            {
            }
            else if (rd4.Checked) {

            }

        }

        private void relacionPensionAlimenticia()
        {
            string query = "select pp1.*,mma.rfc as rfcjpp,mma.nombre as nombrejpp,mma2.rfc as rfcpea,mma2.nombre as nombrepea from nominas_catalogos.pension_alimenticia pp1 "+
                " inner join nominas_catalogos.maestro mma  "+
                " on mma.jpp = pp1.jpp and mma.num = pp1.numjpp "+
                " inner join nominas_catalogos.maestro mma2 on mma2.jpp = 'PEA' and mma2.num = pp1.numpea order by pp1.jpp ,pp1.numjpp ";


            List<Dictionary<string, object>> resultado = globales.consulta(query);

            object[] obj = new object[resultado.Count];


            int contador = 0;

           foreach (Dictionary<string,object> item in resultado) {
                string jpp = Convert.ToString(item["jpp"]);
                string numjpp = Convert.ToString(item["numjpp"]);
                string numpea = Convert.ToString(item["numpea"]);
                string porcentaje = Convert.ToString(item["descuento"]);
                string total = Convert.ToString(item["total"]);
                string rfcjpp = Convert.ToString(item["rfcjpp"]);
                string nombrejpp = Convert.ToString(item["nombrejpp"]);
                string rfcpea = Convert.ToString(item["rfcpea"]);
                string nombrepea = Convert.ToString(item["nombrepea"]);



                object[] tt1 = { jpp,numjpp,numpea,porcentaje,total,rfcjpp,nombrejpp,rfcpea,nombrepea };


                obj[contador] = tt1;

                contador++;
            }


            globales.reportes("reporte_pensionalimenticia", "pension_alimenticia", obj);

        }
    }
}
