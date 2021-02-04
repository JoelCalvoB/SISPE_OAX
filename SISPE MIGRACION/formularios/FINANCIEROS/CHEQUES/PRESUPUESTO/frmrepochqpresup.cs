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
    public partial class frmrepochqpresup : Form
    {
        public frmrepochqpresup()
        {
            InitializeComponent();
        }

        private void frmrepocheques_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtini.Text) || string.IsNullOrWhiteSpace(txtfinal.Text) || string.IsNullOrWhiteSpace(txtchequera.Text))
            {
                DialogResult dialogo = globales.MessageBoxError("FAVOR DE INGRESAR EL INTERVALO DE CHEQUES, NO DEJE VACIÓ LOS CAMPOS", "ERROR", this);
                return;
            }
            string query = "select cheques.folio,cheques.numcheque,cheques.concep1,cheques.fecha,cheques.reviso,cheques.autorizo,cheques.elaboro,cheques.numpoliz,detalle.debe_haber,detalle.importe,detalle.concepto,detalle.ctacontab,detalle.referencia,cheques.benefic,cheques.chequera,cheques.impcheque  from financieros.datos_presupuesto cheques inner join financieros.detalle_presupuesto detalle on detalle.numcheque = cheques.numcheque where cheques.chequera='{0}' AND cheques.numcheque BETWEEN '{1}' and '{2}' order by cheques.numcheque desc,debe_haber asc,referencia desc";
            string pasa = string.Format(query, txtchequera.Text, txtini.Text, txtfinal.Text);
            List<Dictionary<string, object>> resultado = globales.consulta(pasa);
            List<object> objetos = new List<object>();
            int contador = 0;

            if (resultado.Count == 0)
            {
                DialogResult dialogo = globales.MessageBoxError("NO EXISTE LA INFORMACIÓN INGRESADA", "ERROR", this);

                return;
            }
            DialogResult dialogo1 = globales.MessageBoxSuccess("GENERANDO REPORTE DE CHEQUES", "", globales.menuPrincipal);
            Dictionary<string, object> folioInicio = (Dictionary<string, object>)(resultado[0]);
            string folioUltimo = Convert.ToString(resultado[resultado.Count - 1]["folio"]);
            int tope = 25;
            foreach (Dictionary<string, object> item in resultado)
            {
                string folio = Convert.ToString(item["folio"]);
                string numCheque = Convert.ToString(item["numcheque"]);
                string fecha = string.Format("{0:d}", DateTime.Parse(Convert.ToString(item["fecha"])));
                string reviso = Convert.ToString(item["reviso"]);
                string autorizo = Convert.ToString(item["autorizo"]);
                string elaboro = Convert.ToString(item["elaboro"]);
                string numpoliz = Convert.ToString(item["numpoliz"]);
                string debeHaber = Convert.ToString(item["debe_haber"]);
                string importe = Convert.ToString(item["importe"]);
                string concepto = Convert.ToString(item["concepto"]);
                string ctacontab = Convert.ToString(item["ctacontab"]);
                string referencia = Convert.ToString(item["referencia"]);
                string benefic = Convert.ToString(item["benefic"]).Replace("C.", "");
                string concep1 = Convert.ToString(item["concep1"]);
                string impcheque = Convert.ToString(item["impcheque"]);
                string importeDebe = string.Empty;
                string importeHaber = string.Empty;

                if (debeHaber == "D")
                {
                    importeDebe = string.IsNullOrWhiteSpace(importe) ? "" : string.Format("{0:C}", double.Parse(importe, System.Globalization.NumberStyles.Currency)).Replace("$", "");
                    importeHaber = "";
                }
                else
                {
                    importeHaber = string.IsNullOrWhiteSpace(importe) ? "" : string.Format("{0:C}", double.Parse(importe, System.Globalization.NumberStyles.Currency)).Replace("$", "");
                    importeDebe = "";
                }

                DateTime dtFecha = DateTime.Parse(fecha);
                string[] meses = globales.getMeses();
                string importeLetra = globales.convertirNumerosLetras(impcheque, true);
                impcheque = string.Format("{0:C}", double.Parse(impcheque, System.Globalization.NumberStyles.Currency)).Replace("$", "");
                string srtFecha = string.Format("{0} DE {1} DEL {2}", dtFecha.Day, meses[dtFecha.Month].ToUpper(), dtFecha.Year);

                if (Convert.ToString(folioInicio["folio"]) != folio)
                {
                    for (int x = 1; x <= tope; x++)
                    {
                        object[] obj1 = {folioInicio["folio"],folioInicio["numcheque"],string.Format("{0:d}", DateTime.Parse(Convert.ToString(folioInicio["fecha"]))),folioInicio["reviso"],folioInicio["autorizo"],folioInicio["elaboro"],folioInicio["numpoliz"],folioInicio["debe_haber"],folioInicio["importe"],"",
                                "","","","","","",""};
                        objetos.Add(obj1);
                    }
                    folioInicio = item;
                    tope = 25;
                }
                tope--;
                double sumaDebe = resultado.Where(o => Convert.ToString(o["folio"]) == folio && Convert.ToString(o["debe_haber"]) == "D").Sum(o => Convert.ToDouble(o["importe"]));
                double sumaHaber = resultado.Where(o => Convert.ToString(o["folio"]) == folio && Convert.ToString(o["debe_haber"]) == "H").Sum(o => Convert.ToDouble(o["importe"]));


                object[] obj = {folio,numCheque,srtFecha,reviso,autorizo,elaboro,numpoliz,debeHaber,impcheque,concepto,
                                ctacontab,referencia,benefic,$"({importeLetra})",concep1,importeDebe,importeHaber,string.Format("{0:C}",sumaDebe).Replace("$",""),string.Format("{0:C}",sumaHaber).Replace("$","")};
                objetos.Add(obj);
            }

            if (resultado.Count > 0)
            {
                for (int x = 1; x <= tope; x++)
                {
                    object[] obj1 = {folioUltimo,"","","","","","","","","",
                                "","","","","","",""};
                    objetos.Add(obj1);
                    contador++;
                }
            }
            contador = 0;
            object[] enviarObjeto = new object[objetos.Count];
            for (int x = contador; x < objetos.Count; x++)
            {
                enviarObjeto[x] = objetos[x];
            }
            globales.reportes("reporteFinancierosEmisionCheque", "emisionCheque", enviarObjeto);
        }

        private void txtchequera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtini.Select();

            }

            if (e.KeyCode==Keys.Insert)
            {
                frmchequeras chequeras = new frmchequeras();
                chequeras.enviar = rellenaChequera;
                chequeras.ShowDialog();
            }
        }

        public void rellenaChequera(Dictionary<string,object> obj ,bool externo= false)
        {
            txtchequera.Text = Convert.ToString(obj["chequera"]);
        }

        private void txtini_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtfinal.Select();

            }
        }

        private void txtfinal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null); //
            }

        }


        private void rangos()
        {
            txtchequera.Select();
            try
            {
                string query = "SELECT min(numcheque) as minimo , max(numcheque) as maximo FROM financieros.datos_presupuesto where chequera='{0}'";
                string pasa = string.Format(query, txtchequera.Text);
                List<Dictionary<string, object>> resultado = globales.consulta(pasa);
                txtini.Text = Convert.ToString(resultado[0]["minimo"]);
                txtfinal.Text = Convert.ToString(resultado[0]["maximo"]);
            }
            catch
            {
            }
        }

        private void txtchequera_Leave(object sender, EventArgs e)
        {
            rangos();
        }
    }
}
