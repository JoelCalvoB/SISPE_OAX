using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.PAGO_DE_MARCHA
{
    public partial class frmrepormarcha : Form
    {
        public frmrepormarcha()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(fecha1.Text) || (string.IsNullOrWhiteSpace(fecha2.Text)))
            {
                DialogResult dialog = globales.MessageBoxExclamation("NO SE PUEDEN GENERAR REPORTES SIN INGRESAR LAS FECHAS", "REGISTRO VACIO", globales.menuPrincipal);
                return;
            }
            DateTime fec1 = fecha1.Value;
            DateTime fec2 = fecha2.Value;

            string c1 = string.Format("{0}-{1}-{2}", fec1.Year, fec1.Month, fec1.Day);
            string c2 = string.Format("{0}-{1}-{2}", fec2.Year, fec2.Month, fec2.Day);

            filtrar(c1, c2);


        }

        private void filtrar(string c1, string c2)
        {
            this.Cursor = Cursors.WaitCursor;
            string query = string.Format("select * from datos.p_marcha where f_recibo BETWEEN '{0}' and '{1}'", c1, c2);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                double folio = 0;
                string nombre_em = string.Empty;
                string rfc = string.Empty;
                string depe = string.Empty;
                string sueldo_base = string.Empty;
                string meses = string.Empty;
                double monto = 0;
                string f_recibo = string.Empty;
                string f_cobro = string.Empty;
                string numeroCheque = string.Empty;

                try
                {
                    folio = Convert.ToDouble(item["folio"]);
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    rfc = Convert.ToString(item["rfc"]);
                    depe = Convert.ToString(item["depe"]);
                    sueldo_base = Convert.ToString(item["sueldo_base"]);
                    meses = Convert.ToString(item["meses"]);
                    monto = Convert.ToDouble(item["monto"]);
                    f_recibo = string.Format("{0:d}", (item["f_recibo"]));
                    f_cobro = globales.parseDateTime(globales.convertDatetime(Convert.ToString(item["f_cobro"])));
                    numeroCheque = Convert.ToString(item["n_cheque"]);

                }
                catch
                {

                }
                object[] tt1 = { folio, nombre_em, rfc,depe,sueldo_base,meses,monto,f_recibo,f_cobro,numeroCheque };
                aux2[contador] = tt1;
                contador++;
            }
            object[] parametros = { "fech1", "fech2" };
            object[] valor = { fecha1.Text, fecha2.Text };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("repmarcha", "re_marcha", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;
        }

        private void frmrepormarcha_Load(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void fecha1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) fecha2.Select();
        }

        private void fecha2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1.Select();

        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(null, null);
        }

        private void frmrepormarcha_KeyDown(object sender, KeyEventArgs e)
        {
            button2_Click(null, null);
        }
    }
}
