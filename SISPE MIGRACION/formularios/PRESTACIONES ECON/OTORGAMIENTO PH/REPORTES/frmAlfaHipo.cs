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
    public partial class frmAlfaHipo : Form
    {
        public frmAlfaHipo()
        {
            InitializeComponent();
        }

        private void frmAlfaHipo_Shown(object sender, EventArgs e)
        {
            DateTime anio = DateTime.Now;
            txtAnio.Text = Convert.ToString(anio.Year);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = string.Empty;
            string qry = string.Empty;
            string opcionSecuencia = string.Empty;
            string opcionTiempo = string.Empty;
            string opcionFinalidad = string.Empty;

            if (rbTodos.Checked) opcionTiempo = "";
            if (rbAnio.Checked) opcionTiempo = $" AND a2.f_solicitud BETWEEN '{txtAnio.Text}-01-01' AND '{txtAnio.Text}-12-31'";

            if (rbInicial.Checked) opcionSecuencia = " WHERE a2.sec ='0' ";

            if (rbAmpliacion.Checked) opcionSecuencia = " WHERE a2.sec <>'0' ";

            if (rbTodosSec.Checked) opcionSecuencia = " WHERE a2.sec in ('0','1','2','3')";

            if (rbAutorizado.Checked) opcionFinalidad = " AND a2.status = 'A'";

            if (rbTramite.Checked) opcionFinalidad = " AND a2.status = 'T'";

            if (rbRechazado.Checked) opcionFinalidad = " AND a2.status = 'R'";

            if (rbEspera.Checked) opcionFinalidad = " AND a2.status = 'L'";

            if (rbTodosStatus.Checked) opcionFinalidad = " AND a2.status IN ('A','T','R','L')";





            query = "SELECT a1.folio,a1.rfc,a1.nombre_em,a1.proyecto,a1.nomina,a2.f_solicitud,a2.f_autorizacion,a2.plazoa,descri_finalid,a2.cap_prest,a2.finalidad,a2.sec,a2.status FROM  datos.p_hipote a1 INNER JOIN datos.h_solici a2 ON a1.folio = a2.expediente" + opcionSecuencia + opcionFinalidad + opcionTiempo;
            qry = string.Format(query);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            string folio = string.Empty;
            string rfc = string.Empty;
            string nombre_em = string.Empty;
            string proyecto = string.Empty;
            string nomina = string.Empty;
            string f_solicitud = string.Empty;
            string f_autorizacion = string.Empty;
            string plazoa = string.Empty;
            string cap_prest = string.Empty;
            string finalidad = string.Empty;
            string sec = string.Empty;
            string status = string.Empty;

            if (resultado.Count<=0)
            {
                DialogResult dialogo1 = globales.MessageBoxExclamation("NO SE ENCUENTRA LA INFORMACIÓN BUSCADA", "VERIFICAR", globales.menuPrincipal);
            }
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            foreach (var item in resultado)
            {
                folio = Convert.ToString(item["folio"]);
                rfc = Convert.ToString(item["rfc"]);
                nombre_em = Convert.ToString(item["nombre_em"]);
                proyecto = Convert.ToString(item["proyecto"]);
                nomina = Convert.ToString(item["nomina"]);
                f_solicitud = Convert.ToString(item["f_solicitud"]).Replace(" 12:00:00 a. m", ""); ;
                f_autorizacion = Convert.ToString(item["f_autorizacion"]).Replace(" 12:00:00 a. m", ""); ;
                plazoa = Convert.ToString(item["plazoa"]);
                finalidad = Convert.ToString(item["descri_finalid"]);
                cap_prest = Convert.ToString(item["cap_prest"]);
                object[] tt1 = { folio, rfc,nombre_em,proyecto,nomina,f_solicitud,f_autorizacion,plazoa,cap_prest,finalidad};
                aux2[contador] = tt1;
                contador++;
            }
            object[] parametros = { "anio", "seleccion" };
            object[] valor = { "", ""};
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("ReporteListaHipo", "ListadoHipotecario", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;
        
        }

        private void rbAnio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAnio.Checked)
            {
                txtAnio.Enabled = true;
            }
            else
            {
                txtAnio.Enabled = false;

            }

            }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
    }

