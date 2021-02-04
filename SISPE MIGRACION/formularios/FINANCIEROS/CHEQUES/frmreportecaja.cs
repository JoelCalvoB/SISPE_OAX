using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.FINANCIEROS.CHEQUES
{
    public partial class frmreportecaja : Form
    {
        public frmreportecaja()
        {
            InitializeComponent();
        }

        private void metodoreporte()
        {
            if (String.IsNullOrWhiteSpace(txtchequera.Text)|| (String.IsNullOrWhiteSpace(fec1.Text)))
            {
                DialogResult dialogo = globales.MessageBoxError("FAVOR DE RELLENAR TODOS LOS CAMPOS","ERROR DE CAPTURA", globales.menuPrincipal);
                return;
            }
            string query = " create temp table tablas as select numpoliz,numcheque,benefic,impcheque,concep1,folio,esfondo from financieros.datoscheques where fecha='{0}' and chequera='{1}' order by numcheque asc;";
            string reporte = string.Format(query, string.Format("{0:yyyy-MM-dd}",DateTime.Parse(fec1.Text)), txtchequera.Text);
            reporte += " create temp table tablaquiro as select tablas.*,q.f_emischeq as fechacheque from tablas left join datos.p_quirog q on q.folio = tablas.folio  where esfondo = false; ";
            reporte += " create temp table tablafondo1 as select tablas.*,q.fecha_pago as fechacheque from tablas left join datos.d_fondo q on q.folio = tablas.folio  where esfondo = true; ";
            reporte += " (select * from tablaquiro union select * from tablafondo1) order by numpoliz asc ";
            List<Dictionary<string, object>> resultado = globales.consulta(reporte);
            if (resultado.Count==0)
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
                string fechaCheque = (string.IsNullOrWhiteSpace(Convert.ToString(item["fechacheque"]))) ? "" : string.Format("{0:d}", DateTime.Parse(Convert.ToString(item["fechacheque"])));

                object[] tt1 = { numpoliz, numcheque, benefic, impcheque,concep1,fechaCheque };
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
            if (e.KeyCode==Keys.Enter)
            {
                fec1.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            metodoreporte();   
        }
    }
}
