using SISPE_MIGRACION.formularios.CATÁLOGOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA
{
    public partial class frmdevol : Form
    {
        public frmdevol()
        {
            InitializeComponent();
        }

        private void frmdevol_Load(object sender, EventArgs e)
        {

            frmEmpleados p_quirog = new frmEmpleados();
            p_quirog.enviar = rellenarConsulta;

            p_quirog.ShowDialog();

        }


        public void rellenarConsulta(Dictionary<string, object> resultado, bool externo = false)
        {
            this.txtrfc.Text = Convert.ToString(resultado["rfc"]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            string rfc1 = txtrfc.Text;
            object[] objetotablaReporte;

            string query = string.Format("select *,false as esaval from datos.p_edocta where rfc like '%{0}%'", rfc1.Substring(0,10));
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            query = $"select tt1.folio,tt1.rfc,tt1.nombre_em,tt2.f_emischeq,tt2.importe,true as esaval from datos.d_quirog tt1 inner join datos.p_edocta tt2 on tt2.folio = tt1.folio  where tt1.rfc like '%{rfc1.Substring(0,10)}%'";
            List<Dictionary<string, object>> tt2 = globales.consulta(query);

            foreach (Dictionary<string,object> tt in tt2) {
                resultado.Add(tt);
            }

            query = "";

            objetotablaReporte = new object[resultado.Count];
            int contador = 0;

            foreach (Dictionary<string, object> item in resultado)
            {
                string folio = Convert.ToString(item["folio"]);
                string rfc = Convert.ToString(item["rfc"]);
                string nombre_em = Convert.ToString(item["nombre_em"]);
                string emision_cheque = Convert.ToString(item["f_emischeq"]).Replace(" 12:00:00 a. m.", "");
                string importe = Convert.ToString(item["importe"]);
                bool esaval = Convert.ToBoolean(item["esaval"]);

                query = string.Format("select  max(f_descuento) as fultimopago,sum(importe) as pagado,({0} - sum(importe)) as saldo from datos.descuentos where folio = {1}", importe, folio);
                List<Dictionary<string, object>> tmp = globales.consulta(query);
                string fultimopago = Convert.ToString(tmp[0]["fultimopago"]).Replace(" 12:00:00 a. m.", "");
                string pagado = Convert.ToString(tmp[0]["pagado"]);
                string saldo = Convert.ToString(tmp[0]["saldo"]);
                if (string.IsNullOrWhiteSpace(fultimopago)) continue;

                object[] arregloAux1;

                if (!esaval) {
                    object[] arregloAux = { rfc, nombre_em, folio, "SUSCRIPTOR", fultimopago, importe, pagado, saldo, emision_cheque };
                    arregloAux1 = arregloAux;
                }
                else {
                    object[] arregloAux = { rfc, nombre_em, folio, "AVAL", fultimopago, importe, pagado, saldo, emision_cheque };
                    arregloAux1 = arregloAux;
                }

                
                
                objetotablaReporte[contador] = arregloAux1;
                contador++;
            }

            object[] objectoenviar = new object[contador];
            for (int x = 0; x < objetotablaReporte.Length; x++)
            {
                if (objetotablaReporte[x] == null) break;
                objectoenviar[x] = objetotablaReporte[x];
            }

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);


            object[][] parametros = new object[2][];
            object[] header = { "logo" };
            object[] body = { logo };

            globales.reportes("reporteConsultaPDevolucion", "consultaDevolucion", objectoenviar,"",false,parametros);
            this.Cursor = Cursors.Default;
        }

        private void frmdevol_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogo = MessageBox.Show("¿Desea cerrar el módulo?",
          "Cerrar el módulo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogo == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void frmdevol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void txtrfc_KeyPress(object sender, KeyPressEventArgs e)
        {
            frmEmpleados p_quirog = new frmEmpleados();
            p_quirog.enviar = rellenarConsulta;

            p_quirog.ShowDialog();
        }
    }
}

