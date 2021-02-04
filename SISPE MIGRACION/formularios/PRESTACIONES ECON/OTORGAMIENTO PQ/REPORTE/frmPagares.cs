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
    public partial class frmPagares : Form
    {
        public frmPagares()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fec1 = fe1.Value;
            DateTime fec2 = fe2.Value;

            string variable = (radioCentral.Checked) ? "CEN%" : "DES%";

            string c1 = string.Format("{0}-{1}-{2}", fec1.Year, fec1.Month, fec1.Day);
            string c2 = string.Format("{0}-{1}-{2}", fec2.Year, fec2.Month, fec2.Day);


             filtrar(c1, c2,variable);
        }

        private void filtrar(string c1, string c2,string variable)
        {
            this.Cursor = Cursors.WaitCursor;
            string query = string.Format("SELECT folio, nombre_em, rfc ,proyecto, importe FROM datos.p_quirog AA LEFT JOIN catalogos.cuentas BB ON AA.secretaria = BB.proy WHERE BB.tipo_dep LIKE '{0}' AND f_emischeq >= '{1}' AND f_emischeq <= '{2}' ORDER BY AA.PROYECTO ASC,AA.FOLIO ASC", variable, c1, c2);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                double folio = 0;
                string nombre_em = string.Empty;
                string rfc = string.Empty;
                string proyecto = string.Empty;
                double importe = 0;
                string fecha_solicitud = string.Empty;
                string fecha_emision = string.Empty;

                try
                {
                    folio = Convert.ToDouble(item["folio"]);
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    rfc = Convert.ToString(item["rfc"]);
                    proyecto = Convert.ToString(item["proyecto"]);
                    importe = Convert.ToDouble(item["importe"]);
                    fecha_solicitud = string.Format("{0:d}", (item["f_solicitud"]));
                    fecha_emision = string.Format("{0:d}", (item["f_emischeq"]));

                }
                catch
                {

                }
                fecha_solicitud = fecha_solicitud.Replace(" 12:00:00 a. m.", "");
                fecha_emision = fecha_emision.Replace(" 12:00:00 a. m.", "");

                string[] aux = fecha_solicitud.Split('/');
               
                object[] tt1 = { folio, nombre_em, " ", " ", " " , importe , " ", proyecto ,  rfc };
                aux2[contador] = tt1;
                contador++;
            }

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[] parametros = { "fec1", "fec2", "logo" };
            object[] valor = { fe1.Text, fe2.Text, logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("Reportpagare", "p_quirog", aux2, "", false, enviarParametros);

            this.Cursor = Cursors.Default;
        }

        private void frmPagares_Load_1(object sender, EventArgs e)
        {
           
            fe1.Format = DateTimePickerFormat.Custom;
            fe1.CustomFormat = "dd/MM/yyyy";
        
            fe2.Format = DateTimePickerFormat.Custom;
            fe2.CustomFormat = "dd/MM/yyyy";

            DateTime auxFecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            auxFecha = auxFecha.AddMonths(1);
            fe1.Value = auxFecha;

            auxFecha = auxFecha.AddMonths(1);
            auxFecha = auxFecha.AddDays(-1);

            fe2.Value = auxFecha;
        }

        private void frmPagares_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }

       
    }


