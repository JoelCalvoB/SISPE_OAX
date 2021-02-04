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
    public partial class frmConcentrado : Form
    {
        public frmConcentrado()
        {
            InitializeComponent();
        }

        private void frmConcentrado_Shown(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            txtFecha.Text = Convert.ToString(fecha);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;

            string query = string.Empty;

            if (rbInicio.Checked) query = "select a1.expediente,a1.sec,a1.descri_finalid,a1.f_autorizacion,a2.nombre_em,a2.nomina,a2.descripcion,a2.direc_inmu,a2.ant_a,a3.imp_estimt,a3.imp_faltante,a4.proyecto,a4.cred_ant,a4.avance_o,a4.necesida,a4.solvencia,a4.observacion,a5.valor_bien from datos.h_solici a1 JOIN datos.p_hipote a2 ON a1.expediente=a2.folio JOIN datos.h_sretec a3 ON a1.expediente=a3.expediente JOIN datos.h_sconsj a4 ON a3.expediente=a4.expediente JOIN datos.h_eavalu a5 ON a4.expediente=a5.expediente where a1.f_autorizacion='{0}'  AND a1.sec='0' AND a3.sec='0' and a4.sec='0'";
            if (rbampliación.Checked) query = "select a1.expediente,a1.sec,a1.descri_finalid,a1.f_autorizacion,a2.nombre_em,a2.nomina,a2.descripcion,a2.direc_inmu,a2.ant_a,a3.imp_estimt,a3.imp_faltante,a4.proyecto,a4.cred_ant,a4.avance_o,a4.necesida,a4.solvencia,a4.observacion,a5.valor_bien from datos.h_solici a1 JOIN datos.p_hipote a2 ON a1.expediente=a2.folio JOIN datos.h_sretec a3 ON a1.expediente=a3.expediente JOIN datos.h_sconsj a4 ON a3.expediente=a4.expediente JOIN datos.h_eavalu a5 ON a4.expediente=a5.expediente where a1.f_autorizacion='{0}'  AND a1.sec<>'0' AND a3.sec<>'0' and a4.sec<>'0'";


            string pasa = string.Format(query, string.Format("{0:yyyy-MM-dd}",Convert.ToDateTime(txtFecha.Text)));
                List<Dictionary<string, object>> resultado = globales.consulta(pasa);
                if (resultado.Count >= 1) {
                    object[] aux2 = new object[resultado.Count];
               
                    int contador = 0;
                    foreach (var item in resultado)
                    {
                        string sec = Convert.ToString(item["sec"]);
                        string expediente = Convert.ToString(item["expediente"]);
                        string descri_finalid = Convert.ToString(item["descri_finalid"]);
                        string f_autorizacion = Convert.ToString(item["f_autorizacion"]).Replace(" 12:00:00 a. m", "");
                        string nombre_em = Convert.ToString(item["nombre_em"]);
                        string nomina = Convert.ToString(item["nomina"]);
                        string descripcion = Convert.ToString(item["descripcion"]);
                        string direc_inmu = Convert.ToString(item["direc_inmu"]);
                        string ant_a = Convert.ToString(item["ant_a"]);
                        string imp_estimt = Convert.ToString(item["imp_estimt"]);
                        string imp_faltante = Convert.ToString(item["imp_faltante"]);
                        string proyecto = Convert.ToString(item["proyecto"]);
                        string cred_ant = Convert.ToString(item["cred_ant"]);
                        string avance_o = Convert.ToString(item["avance_o"]);
                        string necesida = Convert.ToString(item["necesida"]);
                        string solvencia = Convert.ToString(item["solvencia"]);
                        string observacion = Convert.ToString(item["observacion"]);
                        string valor_bien = Convert.ToString(item["valor_bien"]);

                        object[] tt1 = { expediente, sec, descri_finalid, f_autorizacion, nombre_em, nomina,descripcion, direc_inmu,ant_a,imp_estimt,imp_faltante,proyecto,cred_ant,avance_o,necesida,solvencia,observacion,valor_bien };
                        aux2[contador] = tt1;
                        contador++;
                    }
                    object[] parametros = { "fech1", "fech1" };
                    object[] valor = { string.Format("{0:d}",Convert.ToDateTime(txtFecha.Text)), string.Format("{0:d}", Convert.ToDateTime(txtFecha.Text)) };
                    object[][] enviarParametros = new object[2][];

                    enviarParametros[0] = parametros;
                    enviarParametros[1] = valor;

                    globales.reportes("reporteConsejoHipo", "ConsejoHipo", aux2, "", false, enviarParametros);
                    this.Cursor = Cursors.Default;
                
                        }
            if (resultado.Count <= 0)
            {

                globales.MessageBoxExclamation("NO SE ENCUENTRA INFORMACIÓN EN EL DÍA INGRESADO", "VERIFICAR", globales.menuPrincipal);
                return;

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
