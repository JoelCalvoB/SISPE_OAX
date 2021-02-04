using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.REPORTES
{
    public partial class frmOtorgadosHip : Form

       
    {
        DateTime f1, f2;
        public frmOtorgadosHip()
        {
            InitializeComponent();
        }

        private void frmOtorgadosHip_Shown(object sender, EventArgs e)
        {
            DateTime auxFecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            auxFecha = auxFecha.AddMonths(1);

            fecha1.Value = auxFecha;

            auxFecha = auxFecha.AddMonths(1);
            auxFecha = auxFecha.AddDays(-1);

            fecha2.Value = auxFecha;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            filtro();
        }

        private void fecha1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) fecha2.Focus();
        }

        private void fecha2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmOtorgadosHip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) {
                this.Owner.Close();
            }
        }

        private void filtro()
        {
            string query = "SELECT * FROM datos.p_edocth where f_emischeq BETWEEN '{0}' and '{1}'";
            string pasa = string.Format(query,string.Format("{0:yyyy-MM-dd}",(fecha1.Value)), string.Format("{0:yyyy-MM-dd}", fecha2.Value));
            List<Dictionary<string, object>> resultado = globales.consulta(pasa);
            object[] aux2 = new object[resultado.Count];
            int contador = 0;

            if (resultado.Count == 0) {
                globales.MessageBoxExclamation("No se encontraron datos para el reporte,","Aviso",globales.menuPrincipal);
                return;
            }

            foreach (var item in resultado)
            {
                string folio = Convert.ToString(item["folio"]);
                string rfc = Convert.ToString(item["rfc"]);
                string nombre_em = Convert.ToString(item["nombre_em"]);
                string proyecto = Convert.ToString(item["proyecto"]);
                string f_emischeq = Convert.ToString(item["f_emischeq"]);
                string plazo = Convert.ToString(item["plazo"]);
                string imp_unit = Convert.ToString(item["imp_unit"]);
                string importe = Convert.ToString(item["importe"]);
              object[] tt1 = { folio,rfc,nombre_em,proyecto, string.Format("{0:yyyy-MM-dd}",Convert.ToDateTime(f_emischeq)), plazo,imp_unit,importe};
                aux2[contador] = tt1;
                contador++;
            }

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[] parametros = { "fecha1", "fecha2", "logo" };
            object[] valor = { fecha1.Text, fecha2.Text, logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reportPrestOtorgadosHip", "prestamosHipo", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;
        }
    }
}
