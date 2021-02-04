using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.REPORTE
{
    public partial class frmReporteSaldosEmisionCheque : Form
    {
        public frmReporteSaldosEmisionCheque()
        {
            InitializeComponent();
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {

            
            string mensaje = "Se va a generar el reporte de la fecha de emisión de cheque al " + string.Format("{0:d}",fecha1.Value);
            globales.MessageBoxInformation(mensaje,"Aviso",this);
            string query = $"select * from datos.p_quirog where f_emischeq between '{string.Format("{0:yyyy-MM-dd}",fecha1.Value)}' and '{string.Format("{0:yyyy-MM-dd}", fecha2.Value)}' order by folio desc";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count == 0) {
                globales.MessageBoxExclamation("No existen prestamos en la fecha de emision de cheque establecida","Aviso",globales.menuPrincipal);
                return;
            }
            Cursor = Cursors.WaitCursor;
            object[] objeto = new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string,object> item in resultado) {
                string folio = Convert.ToString(item["folio"]);
                string nombre = Convert.ToString(item["nombre_em"]);
                string fecha_solicitud = string.Format("{0:d}", item["f_solicitud"]);
                string f_emischeq = string.Format("{0:d}", item["f_emischeq"]);

                string neto = string.IsNullOrWhiteSpace(Convert.ToString(item["importe"]))?"0": Convert.ToString(item["importe"]);
                string liquido = string.IsNullOrWhiteSpace(Convert.ToString(item["liquido"]))?"0": Convert.ToString(item["liquido"]);
                object[] tt1 = { folio,nombre,fecha_solicitud,neto,liquido,f_emischeq };
                objeto[contador] = tt1;

                contador++;
            }
            this.Cursor = Cursors.Default;

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);
            object[][] parametros = new object[2][];
            object[] header = { "logo" };
            object[] body = { logo };
            parametros[0] = header;
            parametros[1] = body;

            globales.reportes("reporteSaldosEmisioncheque", "p_quirog",objeto,"",false,parametros);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmReporteSaldosEmisionCheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                this.Owner.Close();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
