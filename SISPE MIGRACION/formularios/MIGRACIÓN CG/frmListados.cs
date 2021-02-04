using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.MIGRACIÓN_CG
{
    public partial class frmListados : Form
    {

        int collum;
        int row;

        int polnumero;
        int numeromes;
        string mes;

        public frmListados()
        {
            InitializeComponent();
            DateTime año = DateTime.Now;
            string anio = Convert.ToString(año.Year);
            comboAño.Items.Add(anio);

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.mes = string.Empty;
            if (OpcionMes.Text == "Apertura")
                mes = "00";
            if (OpcionMes.Text == "Enero")
                mes = "01";
            if (OpcionMes.Text == "Febrero")
                mes = "02";
            if (OpcionMes.Text == "Marzo")
                mes = "03";
            if (OpcionMes.Text == "Abril")
                mes = "04";
            if (OpcionMes.Text == "Mayo")
                mes = "05";
            if (OpcionMes.Text == "Junio")
                mes = "06";
            if (OpcionMes.Text == "Julio")
                mes = "07";
            if (OpcionMes.Text == "Agosto")
                mes = "08";
            if (OpcionMes.Text == "Septiembre")
                mes = "09";
            if (OpcionMes.Text == "Octubre")
                mes = "10";
            if (OpcionMes.Text == "Noviembre")
                mes = "11";
            if (OpcionMes.Text == "Diciembre")
                mes = "12";
            if (OpcionMes.Text == "Cierre")
                mes = "12";

            if (rbDiario.Checked)
            {
                dataContable.Rows.Clear();
                string query = $"SELECT substr(polnumero ,5) as polizanumero , fecha , concepto , debe , haber , grupo FROM cg_fondos.des where polmes='{mes}'  and  poltipo='D'  order by polizanumero;";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count <= 0) return;
                foreach (var item in resultado)
                {
                    int polizanumero = Convert.ToInt32(item["polizanumero"]);
                    string poliza = "D" + Convert.ToString(polizanumero);

                    string fecha =  Convert.ToString(item["fecha"]).Substring(0,2);
                    string concepto = Convert.ToString(item["concepto"]);
                    double debe = Convert.ToDouble(item["debe"]);
                    double haber = Convert.ToDouble(item["haber"]);
                    string grupo = Convert.ToString(item["grupo"]);
                    dataContable.Rows.Add(poliza, fecha, concepto, string.Format("{0:C}", debe), grupo);
                }
            }
            if (rbEgresos.Checked)
            {
                dataContable.Rows.Clear();
                string query = $"SELECT substr(polnumero ,5) as polizanumero , fecha , concepto , debe , haber , grupo FROM cg_fondos.des where polmes='{mes}'  and poltipo='E'   order by polizanumero;";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count <= 0) return;
                foreach (var item in resultado)
                {
                    int polizanumero = Convert.ToInt32(item["polizanumero"]);
                    string poliza = "E" + Convert.ToString(polizanumero);

                    string fecha = Convert.ToString(item["fecha"]).Substring(0, 2);
                    string concepto = Convert.ToString(item["concepto"]);
                    double debe = Convert.ToDouble(item["debe"]);
                    double haber = Convert.ToDouble(item["haber"]);
                    string grupo = Convert.ToString(item["grupo"]);
                    dataContable.Rows.Add(poliza, fecha, concepto, string.Format("{0:C}", debe), grupo);
                }
            }
            if (rbIngresos.Checked)
            {
                dataContable.Rows.Clear();
                string query = $"SELECT substr(polnumero ,5) as polizanumero , fecha , concepto , debe , haber , grupo FROM cg_fondos.des where polmes='{mes}'  and poltipo='I'   order by polizanumero;";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count <= 0) return;
                foreach (var item in resultado)
                {
                    int polizanumero = Convert.ToInt32(item["polizanumero"]);
                    string poliza = "I" + Convert.ToString(polizanumero);

                    string fecha = Convert.ToString(item["fecha"]).Substring(0, 2);
                    string concepto = Convert.ToString(item["concepto"]);
                    double debe = Convert.ToDouble(item["debe"]);
                    double haber = Convert.ToDouble(item["haber"]);
                    string grupo = Convert.ToString(item["grupo"]);
                    dataContable.Rows.Add(poliza, fecha, concepto, string.Format("{0:C}", debe), grupo);
                }
            }


          

        }

        private void dataContable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.row = e.RowIndex;
            if (this.row == -1) return;
            polnumero = Convert.ToInt32(dataContable.Rows[row].Cells[0].Value);
            numeromes = OpcionMes.SelectedIndex;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = $"SELECT poltipo,polnumero,fecha,elaboro,concepto FROM cg_fondos.des where  CAST(polnumero as numeric)={this.polnumero}  and  cast(polmes as numeric)={this.numeromes}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            this.Cursor = Cursors.WaitCursor;
            //   object[] aux2 = new object[resultado.Count];
            int contador = 0;

            string poltipo = Convert.ToString(resultado[0]["poltipo"]);
            string polnumero = Convert.ToString(resultado[0]["polnumero"]);
            string fecha = Convert.ToString(resultado[0]["fecha"]);
            string elaboro = Convert.ToString(resultado[0]["elaboro"]);
            string concepto = Convert.ToString(resultado[0]["concepto"]);

            string desglose = $"SELECT a1.cuenta , a2.descripcion , a1.doctipo , a1.docnumero , a1.naturaleza , a1.importe  FROM cg_fondos.pol  a1 left JOIN financieros.cuentas a2 On a1.cuenta=a2.cuenta  where  CAST(polnumero as numeric)={this.polnumero}  and  cast(polmes as numeric)={this.OpcionMes.SelectedIndex} ;";


            List<Dictionary<string, object>> resultado1 = globales.consulta(desglose);

            object[] aux2 = new object[resultado1.Count];

            foreach (var item in resultado1)
            {
                string cuenta = Convert.ToString(item["cuenta"]);
                string descripcion = Convert.ToString(item["descripcion"]);
                string doctipo = Convert.ToString(item["doctipo"]);
                string docnumero = Convert.ToString(item["docnumero"]);
                string naturelaza = Convert.ToString(item["naturaleza"]);
                string debe = string.Empty;
                string haber = string.Empty;
                if (naturelaza == "D")
                {
                    debe = Convert.ToString(item["importe"]);
                }
                if (naturelaza == "H")
                {
                    haber = Convert.ToString(item["importe"]);

                }



                object[] tt1 = { cuenta, descripcion, doctipo, docnumero, debe, haber };
                aux2[contador] = tt1;
                contador++;
            }


            object[] parametros = { "tipo_poliza", "numero", "fecha", "elaboro", "descripcion" };
            object[] valor = { poltipo, polnumero, fecha, elaboro, concepto };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("cg_poliza", "poliza_data", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;

        }

        private void rbDiario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(OpcionMes.Text))
            {
                DialogResult dialogo = globales.MessageBoxExclamation("SELECCIONA UN MES Y EL AÑO  , PARA PODES MOSTRAR INFORMACIÓN ", "AVISO", globales.menuPrincipal);
                return;
            }



            dataContable.Rows.Clear();
            string query = $"SELECT substr(polnumero ,5) as polizanumero , fecha , concepto , debe , haber , grupo FROM cg_fondos.des where polmes='{mes}'  and poltipo='D'  order by polizanumero;";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0) return;
            foreach (var item in resultado)
            {
                int polizanumero = Convert.ToInt32(item["polizanumero"]);
                string poliza = "D" + Convert.ToString(polizanumero);

                string fecha = Convert.ToString(item["fecha"]).Substring(0, 2);
                string concepto = Convert.ToString(item["concepto"]);
                double debe = Convert.ToDouble(item["debe"]);
                double haber = Convert.ToDouble(item["haber"]);
                string grupo = Convert.ToString(item["grupo"]);
                dataContable.Rows.Add(poliza, fecha, concepto, string.Format("{0:C}", debe), grupo);
            }
        }

        private void rbEgresos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(OpcionMes.Text))
            {
                DialogResult dialogo = globales.MessageBoxExclamation("SELECCIONA UN MES Y EL AÑO  , PARA PODES MOSTRAR INFORMACIÓN ", "AVISO", globales.menuPrincipal);

                return;
            }


            dataContable.Rows.Clear();
            string query = $"SELECT substr(polnumero ,5) as polizanumero , fecha , concepto , debe , haber , grupo FROM cg_fondos.des where polmes='{mes}' AND  poltipo='E'   order by polizanumero;";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0) return;
            foreach (var item in resultado)
            {
                int polizanumero = Convert.ToInt32(item["polizanumero"]);
                string poliza = "E" + Convert.ToString(polizanumero);

                string fecha = Convert.ToString(item["fecha"]).Substring(0, 2);
                string concepto = Convert.ToString(item["concepto"]);
                double debe = Convert.ToDouble(item["debe"]);
                double haber = Convert.ToDouble(item["haber"]);
                string grupo = Convert.ToString(item["grupo"]);
                dataContable.Rows.Add(poliza, fecha, concepto, string.Format("{0:C}", debe),  grupo);
            }
        }

        private void rbIngresos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(OpcionMes.Text))
            {
                DialogResult dialogo = globales.MessageBoxExclamation("SELECCIONA UN MES Y EL AÑO  , PARA PODES MOSTRAR INFORMACIÓN ", "AVISO", globales.menuPrincipal);

                return;
            }


            dataContable.Rows.Clear();
            string query = $"SELECT substr(polnumero ,5) as polizanumero , fecha , concepto , debe , haber , grupo FROM cg_fondos.des where polmes='{mes}' and  poltipo='I'   order by polizanumero;";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0) return;
            foreach (var item in resultado)
            {
                int polizanumero = Convert.ToInt32(item["polizanumero"]);
                string poliza = "I" + Convert.ToString(polizanumero);

                string fecha = Convert.ToString(item["fecha"]).Substring(0, 2);
                string concepto = Convert.ToString(item["concepto"]);
                double debe = Convert.ToDouble(item["debe"]);
                double haber = Convert.ToDouble(item["haber"]);
                string grupo = Convert.ToString(item["grupo"]);
                dataContable.Rows.Add(poliza, fecha, concepto, string.Format("{0:C}", debe),  grupo);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

