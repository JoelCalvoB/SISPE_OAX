using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.MIGRACIÓN_CG.CONSULTAS
{
    public partial class frmBalanzaComprobacion : Form
    {

        private string polMes = string.Empty;
        public frmBalanzaComprobacion()
        {
            InitializeComponent();
        }

        private void txtanio_Leave(object sender, EventArgs e)
        {

        }

        private void frmBalanzaComprobacion_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }


        public void recargarCatalogos() {

            string query = string.Empty;

                query = $"create temp table t1 as select hc.cuenta as cuentahisto,hc.naturaleza as naturaleza,hc.descripcion,a.cuenta,case when a.naturaleza = 'D' then a.importe else 0 end debe, case when a.naturaleza = 'H' then a.importe else 0 end haber, 0 as inicialdebe,0 as inicialhaber from  cg_fondos.\"historicoCuentas\" hc left JOIN  (select substring(cuenta,1,5) as cuenta,naturaleza,sum(importe) as importe from cg_fondos.pol where polmes = '{polMes}' and anio = {txtanio.Text} "+
"group by substring(cuenta, 1, 5),naturaleza)a on a.cuenta = trim(hc.cuenta) where length(trim(hc.cuenta)) <= 5 order by hc.cuenta; select cuentahisto,naturaleza,descripcion,cuenta,sum(debe) as debe,sum(haber) as haber,sum(inicialdebe) as inicialdebe,sum(inicialhaber) as inicialhaber from t1 group by cuentahisto,naturaleza,descripcion,cuenta order by cuentahisto;";
            


            List<Dictionary<string, object>> resultado = globales.consulta(query);


            List<Dictionary<string, object>> r1 = new List<Dictionary<string, object>>();
            if (tabControl1.SelectedIndex > 0) {
                string agrupado = string.Empty;

                for (int x = 0; x < tabControl1.SelectedIndex; x++) {
                    string aux = (x < 10)?$"0{x}": x.ToString();
                    agrupado += $"'{aux}',";
                }

                agrupado = agrupado.Substring(0,agrupado.Length-1);

                query = $"create temp table t1 as select hc.cuenta as cuentahisto,hc.naturaleza as naturaleza,hc.descripcion,a.cuenta,case when a.naturaleza = 'D' then a.importe else 0 end debe, case when a.naturaleza = 'H' then a.importe else 0 end haber, 0 as inicialdebe,0 as inicialhaber from  cg_fondos.\"historicoCuentas\" hc left JOIN  (select substring(cuenta,1,5) as cuenta,naturaleza,sum(importe) as importe from cg_fondos.pol where polmes in ({agrupado}) and anio = {txtanio.Text} " +
    "group by substring(cuenta, 1, 5),naturaleza)a on a.cuenta = trim(hc.cuenta) where length(trim(hc.cuenta)) <= 5 order by hc.cuenta; select cuentahisto,naturaleza,descripcion,cuenta,sum(debe) as debe,sum(haber) as haber,sum(inicialdebe) as inicialdebe,sum(inicialhaber) as inicialhaber from t1 group by cuentahisto,naturaleza,descripcion,cuenta order by cuentahisto;";
                r1 = globales.consulta(query);
            }

            if (r1.Count != 0) {
                foreach (Dictionary<string,object> item in r1) {
                    bool encontrar = resultado.Any(o => Convert.ToString(o["cuentahisto"]).Trim() ==Convert.ToString(item["cuentahisto"]).Trim());
                    if (encontrar) {
                         Dictionary<string,object> obj1 =  resultado.Where(o => Convert.ToString(o["cuentahisto"]).Trim() == Convert.ToString(item["cuentahisto"]).Trim()).First();
                        obj1["inicialdebe"] = item["debe"];
                        obj1["inicialhaber"] = item["haber"];

                    }
                }
            }

            dtggrid.Rows.Clear();
            foreach (Dictionary<string,object> item in resultado) {
                string cuenta = Convert.ToString(item["cuentahisto"]);
                string descripcion = Convert.ToString(item["descripcion"]);
                string naturaleza = Convert.ToString(item["naturaleza"]);
                double debe = globales.convertDouble(Convert.ToString(item["debe"]));
                double haber = globales.convertDouble(Convert.ToString(item["haber"]));

                if (debe < 0) {

                }

                double inicialDebe = globales.convertDouble(Convert.ToString(item["inicialdebe"]));
                double inicialhaber = globales.convertDouble(Convert.ToString(item["inicialhaber"]));

                double finalDebe = 0;
                double finalhaber = 0;

                string strFinalDebe = string.Empty;
                string strFinalHaber = string.Empty;

                string strInicialDebe = string.Empty;
                string stroInicialHaber = string.Empty;

                
                bool esdebe = false;
                if (debe != 0 || inicialDebe != 0) {
                    esdebe = true;
                } else if (haber != 0 || inicialhaber != 0) {
                    esdebe = false;
                }

                double resultadofinal = 0;
                if (naturaleza == "D")
                {
                    resultadofinal = debe - haber;
                }
                else {
                    resultadofinal = haber - debe;
                }
                

                string strDebe = globales.convertMoneda(debe);
                string strHaber = globales.convertMoneda(haber);

                if (naturaleza == "D")
                {

                    inicialDebe = inicialDebe - inicialhaber;

                    inicialhaber = 0;
                    stroInicialHaber = "--";

                    if (haber == 0) {
                        strHaber = "--";
                    }
                    finalDebe = inicialDebe + resultadofinal;
                    strFinalDebe = globales.convertMoneda(finalDebe);
                    strInicialDebe = globales.convertMoneda(inicialDebe);

                }
                else
                {
                    strInicialDebe = "--";
                    strFinalDebe = "--";
                }

                if (naturaleza == "H")
                {
                    inicialhaber = inicialhaber - inicialDebe;
                    inicialDebe = 0;

                    strInicialDebe = "--";

                    if (debe == 0)
                    {
                        strDebe = "--";
                    }

                    finalhaber = inicialhaber + resultadofinal;
                //    finalhaber = esdebe ? finalhaber * (-1) : finalhaber;
                    strFinalHaber = globales.convertMoneda(finalhaber);
                    stroInicialHaber = globales.convertMoneda(inicialhaber);
                }
                else
                {
                    stroInicialHaber = "--";
                    strFinalHaber = "--";
                }


                
                


                int modificado = this.dtggrid.Rows.Add(cuenta,descripcion,strInicialDebe,stroInicialHaber,strDebe,strHaber,strFinalDebe,strFinalHaber);
                if (inicialDebe < 0) {
                    this.dtggrid.Rows[modificado].Cells[2].Style.ForeColor = Color.Red;
                }

                if (inicialhaber < 0)
                {
                    this.dtggrid.Rows[modificado].Cells[3].Style.ForeColor = Color.Red;
                }
                if (debe < 0)
                {
                    this.dtggrid.Rows[modificado].Cells[4].Style.ForeColor = Color.Red;
                }

                if (haber < 0) {
                    this.dtggrid.Rows[modificado].Cells[5].Style.ForeColor = Color.Red;
                }


            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            polMes = tabControl1.SelectedIndex < 10 ? $"0{tabControl1.SelectedIndex}" : tabControl1.SelectedIndex.ToString();
            recargarCatalogos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
