using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas.forms
{
    public partial class frmImprimirReporteTasas : Form
    {

        private bool hipotecaerio { get; set; }
        public frmImprimirReporteTasas(bool hipotecario = false)
        {
            InitializeComponent();
            this.hipotecaerio = hipotecario;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string aux1 = string.Format("{0:yyyy-MM-dd}", fecha1.Value);
                string aux2 = string.Format("{0:yyyy-MM-dd}", fecha2.Value);
                string t_prestamo = this.hipotecaerio ? "H" : "Q";
                string query = string.Format("select t1.trel,t2.descripcion,t1.tasa,t1.fmodif from catalogos.tasa t1 inner join datos.c_tasai t2 on t1.trel = t2.trel where fmodif >= '{0}' and fmodif <= '{1}' and t_prestamo = '{2}' ",aux1,aux2,t_prestamo);
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                object[] objeto = new object[resultado.Count];
                int contador = 0;
                foreach (Dictionary<string,object> item in resultado) {
                    string tipoRelacion = Convert.ToString(item["trel"]);
                    string descripcion = Convert.ToString(item["descripcion"]);
                    string fechaModificacion = Convert.ToString(item["fmodif"]).Replace(" 12:00:00 a. m.","");
                    string tasa = Convert.ToString(item["tasa"]);
                    object[] objeto2 = {tipoRelacion,descripcion,(Convert.ToDouble(tasa) /24/100),fechaModificacion,tasa };
                    objeto[contador] = objeto2;
                    contador++;
                }
                aux1 = string.Format("{0:d}",fecha1.Value);
                aux2 = string.Format("{0:d}", fecha2.Value);
                string fechaActual = string.Format("{0:d}", DateTime.Now);
                string tipo_prestamo = this.hipotecaerio ? "Hipotecario" : "Quirografario";
                string titulo = string.Format("Reporte de Tasa de Interes \n Prestamos {2}\nMovimientos del  {0} al {1}",aux1,aux2,tipo_prestamo);
                object[] parametros = { "fechaActual", "titulo" };
                object[] valor = { fechaActual, titulo };
                object[][] enviarParametros = new object[2][];
                enviarParametros[0] = parametros;
                enviarParametros[1] = valor;
                globales.reportes("reporteTasasDeInteresQ", "tasaInteres",objeto,"Imprimiendo reporte de tasa de interes",false,enviarParametros);

            }
            catch {
                globales.MessageBoxError("Contacte al área de sistemas, error en capturar fechas...","Aviso",globales.menuPrincipal);
            }
            this.Cursor = Cursors.Default;
        }

        private void frmImprimirReporteTasas_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmImprimirReporteTasas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) this.Owner.Close();
        }
    }
}
