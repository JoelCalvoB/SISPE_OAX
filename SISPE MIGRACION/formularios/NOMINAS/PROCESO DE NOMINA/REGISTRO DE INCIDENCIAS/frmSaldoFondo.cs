using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.REGISTRO_DE_INCIDENCIAS
{
    public partial class frmSaldoFondo : Form
    {
        public frmSaldoFondo()
        {
            InitializeComponent();
        }

        private void frmSaldoFondo_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBox1.Select();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conv = comboBox1.Text + textBox1.Text;
            string query = $"SELECT SUM (a2.monto) as monto, a2.archivo,max (a1.nombre) as nombre FROM 	nominas_catalogos.maestro a1 JOIN nominas_catalogos.respaldos_nominas a2 ON a1.num = a2.numjpp WHERE 	a1.superviven = 'S' AND a1.jpp = a2.jpp AND concat (a1.jpp, a1.num) = '{conv}' AND a2.tipo_nomina = 'N' AND a2.clave=202 GROUP BY a2.archivo ORDER by a2.archivo";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0)
            {
                DialogResult dialo = globales.MessageBoxExclamation("NO HAY INFORMACIÓN PARA MOSTRAR", "AVISO", globales.menuPrincipal);

                return;

            }
            string compuesto = string.Empty;
            string monto = string.Empty;
            double tot = 00;
            string total = string.Empty;
            string archivo = string.Empty;
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            string nombre = Convert.ToString(resultado[0]["nombre"]);
            foreach (Dictionary<string, object> item in resultado)
            {
                try
                {
                    monto = Convert.ToString(item["monto"]);
                    archivo = Convert.ToString(item["archivo"]);
                    string mes = archivo.Substring(2, 2);
                    string año = archivo.Substring(0, 2);
                    if (mes == "01") compuesto = "Enero del año " + año;
                    if (mes == "02") compuesto = "Febrero del año " + año;
                    if (mes == "03") compuesto = "Marzo del año " + año;
                    if (mes == "04") compuesto = "Abril del año " + año;
                    if (mes == "05") compuesto = "Mayo del año " + año;
                    if (mes == "06") compuesto = "Junio del año " + año;
                    if (mes == "07") compuesto = "Julio del año " + año;
                    if (mes == "08") compuesto = "Agosto del año " + año;
                    if (mes == "09") compuesto = "Septiembre del año " + año;
                    if (mes == "10") compuesto = "Octubre del año " + año;
                    if (mes == "11") compuesto = "Noviembre del año " + año;
                    if (mes == "12") compuesto = "Diciembre del año " + año;
                    tot += Convert.ToDouble(item["monto"]);
                     total = Convert.ToString(string.Format("{0:c}", tot));
                }
                catch
                {

                }
                object[] tt1 = { monto, compuesto };
                aux2[contador] = tt1;
                contador++;
            }
            object[] parametros = { "nombre", "jpp","total" };
            object[] valor = { nombre, conv, total };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reporteSaldoFondo", "FondoPensiones", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;
        }
    }
}
