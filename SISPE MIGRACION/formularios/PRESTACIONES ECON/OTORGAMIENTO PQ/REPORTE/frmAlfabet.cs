using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes
{
    public partial class frmAlfabet : Form
    {
        public frmAlfabet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fec1 = fecha1.Value;
            DateTime fec2 = fecha2.Value;

            string c1 = string.Format("{0}-{1}-{2}", fec1.Year, fec1.Month, fec1.Day);
            string c2 = string.Format("{0}-{1}-{2}", fec2.Year, fec2.Month, fec2.Day);

            filtrar(c1, c2);


        }

        private void filtrar(string c1, string c2)
        {
            this.Cursor = Cursors.WaitCursor;
            string query = string.Format("select folio, nombre_em , f_solicitud , f_emischeq ,proyecto , importe from datos.p_quirog where f_emischeq >= '{0}' and f_emischeq <= '{1}' order by nombre_em asc", c1, c2);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                double folio = 0;
                string nombre_em = string.Empty;
                string fecha_solicitud = string.Empty;
                string fecha_emision = string.Empty;
                string proyecto = string.Empty;
                double importe = 0;

                try
                {
                    folio = Convert.ToDouble(item["folio"]);
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    fecha_solicitud = string.Format("{0:d}",item["f_solicitud"]);
                    fecha_emision = string.Format("{0:d}",item["f_emischeq"]);
                    proyecto = Convert.ToString(item["proyecto"]);
                    importe = Convert.ToDouble(item["importe"]);

                }
                catch
                {

                }           
                object[] tt1 = { folio, nombre_em, fecha_solicitud, " ", fecha_emision, importe, " ", proyecto};
                aux2[contador] = tt1;
                contador++;
            }

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            

            object[] parametros = { "fech1", "fech2", "logo" };
            object[] valor = { fecha1.Text, fecha2.Text, logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reporteAlfa", "p_quirog", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;
        }

        private void frmAlfabet_Load(object sender, EventArgs e)
        {
            DateTime fec1 = fecha1.Value;
            fecha1.Format = DateTimePickerFormat.Custom;
            fecha1.CustomFormat = "dd/MM/yyyy";
            DateTime fec2 = fecha2.Value;
            fecha2.Format = DateTimePickerFormat.Custom;
            fecha2.CustomFormat = "dd/MM/yyyy";

            DateTime auxFecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            auxFecha = auxFecha.AddMonths(1);
            fecha1.Value = auxFecha;

            auxFecha = auxFecha.AddMonths(1);
            auxFecha = auxFecha.AddDays(-1);

            fecha2.Value = auxFecha;
        }

        private void frmAlfabet_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}

       

