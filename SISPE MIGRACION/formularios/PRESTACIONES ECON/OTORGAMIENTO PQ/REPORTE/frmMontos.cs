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
    public partial class frmMontos : Form
    {
        public frmMontos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fec1 = fe1.Value;
            DateTime fec2 = fe2.Value;

            string variable = (radioImporte.Checked) ? "importe" : "liquido";

            string c1 = string.Format("{0:yyyy-MM-dd}", fec1);
            string c2 = string.Format("{0:yyyy-MM-dd}", fec2);


            filtrar(c1, c2, variable);

        }
        private void filtrar(string c1, string c2, string variable)
        {
            this.Cursor = Cursors.WaitCursor;
            string query = string.Format("SELECT q.folio,q.rfc,q.nombre_em,q.tipo_rel,q.proyecto, q.{0},p.f_emischeq from datos.p_quirog q inner join datos.p_edocta p on p.folio = q.folio where p.f_emischeq >= '{1}' AND p.f_emischeq <= '{2}' order by 	f_emischeq , folio", variable, c1, c2);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            double suma = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                double folio = 0;
                string nombre_em = string.Empty;
                string tipo_rel = string.Empty;
                string proyecto = string.Empty;
                double temporal1 = 0;
                string rfc = string.Empty;
                DateTime f_emischeq =  new DateTime();
                try
                {
                    folio = Convert.ToDouble(item["folio"]);
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    tipo_rel = Convert.ToString(item["tipo_rel"]);
                    proyecto = Convert.ToString(item["proyecto"]);
                    temporal1 = (variable == "importe") ? Convert.ToDouble(item["importe"]) : Convert.ToDouble(item["liquido"]);
                    rfc = Convert.ToString(item["rfc"]);
                    f_emischeq = DateTime.Parse(Convert.ToString(item["f_emischeq"]));

                }
                catch
                {

                }
              //  folio,rfc,nombre_em,tipo_rel,proyecto,
                object[] tt1 = { folio, rfc, nombre_em, tipo_rel, proyecto, temporal1,f_emischeq };
                aux2[contador] = tt1;
                contador++;
                suma +=temporal1;
            }

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[] parametros = { "fec1", "fec2","suma", "logo" };
            object[] valor = { fe1.Text, fe2.Text , suma.ToString() , logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;


          

            globales.reportes("reportMonto", "p_monto", aux2, "", false, enviarParametros);

            this.Cursor = Cursors.Default;

        }

        private void frmMontos_Load(object sender, EventArgs e)
        {
           
            fe1.Format = DateTimePickerFormat.Custom;
            fe1.CustomFormat = "dd/MM/yyyy";
        
            fe2.Format = DateTimePickerFormat.Custom;
            fe2.CustomFormat = "dd/MM/yyyy";

            DateTime auxFecha = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
            auxFecha = auxFecha.AddMonths(1);
            fe1.Value = auxFecha;

            auxFecha = auxFecha.AddMonths(1);
            auxFecha = auxFecha.AddDays(-1);

            fe2.Value = auxFecha;
        }

        private void frmMontos_FormClosing(object sender, FormClosingEventArgs e)
        {
      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
