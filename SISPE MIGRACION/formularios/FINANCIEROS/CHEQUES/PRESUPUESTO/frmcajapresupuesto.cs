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
    public partial class frmcajapresupuesto : Form
    {
        public frmcajapresupuesto()
        {
            InitializeComponent();
        }

        private void metodoreporte()
        {
            if (String.IsNullOrWhiteSpace(txtchequera.Text) || (String.IsNullOrWhiteSpace(fec1.Text)))
            {
                DialogResult dialogo = globales.MessageBoxError("FAVOR DE RELLENAR TODOS LOS CAMPOS", "ERROR DE CAPTURA", globales.menuPrincipal);
                return;
            }
            string query = "select numpoliz,numcheque,benefic,impcheque,concep1 from financieros.datos_presupuesto where fecha='{0}' and chequera='{1}'";
            string reporte = string.Format(query, string.Format("{0:yyyy-MM-dd}",Convert.ToDateTime(fec1.Text)), txtchequera.Text);
            List<Dictionary<string, object>> resultado = globales.consulta(reporte);
            if (resultado.Count == 0)
            {
                DialogResult dialogo = globales.MessageBoxExclamation("FAVOR DE VERIFICAR LA FECHA Y LA CUENTA, NO SE ENCUENTRA INFORMACIÓN", "SIN RESULTADOS", globales.menuPrincipal);
                return;
            }
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            foreach (var item in resultado)
            {
                string numpoliz = Convert.ToString(item["numpoliz"]);
                string numcheque = Convert.ToString(item["numcheque"]);
                string benefic = Convert.ToString(item["benefic"]);
                string impcheque = Convert.ToString(item["impcheque"]);
                string concep1 = Convert.ToString(item["concep1"]);

                object[] tt1 = { numpoliz, numcheque, benefic, impcheque, concep1 };
                aux2[contador] = tt1;
                contador++;
            }
            string cuenta = txtchequera.Text;
            string trae = "select  * from financieros.firmas";
            List<Dictionary<string, object>> resul = globales.consulta(trae);
            string entrego = Convert.ToString(resul[0]["entrego"]);
            string vobo = Convert.ToString(resul[0]["vobo"]);
            //
            string fechaletra = "select datos.fechaletra ('{0}')";
            string paso = string.Format(fechaletra, fec1.Text);
            List<Dictionary<string, object>> list = globales.consulta(paso);
            string obtenerfecha = Convert.ToString(list[0]["fechaletra"]);
            object[] parametros = { "fecha", "entrego", "vobo", "cuenta" };
            object[] valor = { obtenerfecha, entrego, vobo, cuenta };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("chequescaja", "cheques_caja", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                metodoreporte();

            }
        }

        private void frmreportecaja_Load(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            string solofecha = fecha.ToLongDateString();
            solofecha = string.Format("{0:dd/MM/yyyy}", fecha);
            fec1.Text = solofecha;
            txtchequera.Select();
        }

        private void txtchequera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fec1.Select();
            }

            if (e.KeyCode==Keys.Insert)
            {
                frmchequeras chequera = new frmchequeras();
                chequera.enviar = rellenaChequera;
                chequera.ShowDialog();
            }
        }

        public void rellenaChequera(Dictionary<string, object> datos, bool externo = false)
        {
            txtchequera.Text = Convert.ToString(datos["chequera"]);
             }
        private void button1_Click(object sender, EventArgs e)
        {
            metodoreporte();
        }
    }
}
