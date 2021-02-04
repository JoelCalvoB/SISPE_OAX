using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.SUPERVIVENCIA
{
    public partial class frmListadoRetroactivocs : Form
    {
        public frmListadoRetroactivocs()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListadoRetroactivocs_Load(object sender, EventArgs e)
        {
            txtAño.Text = DateTime.Now.Year.ToString();
            cmbMes.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas buscar los pagos retroactivos correspondientes?","Aviso",globales.menuPrincipal);
            if (dialogo == DialogResult.No) return;

            string archivo = string.Empty;
            string mes = cmbMes.SelectedIndex < 9 ? $"0{cmbMes.SelectedIndex + 1}" : (cmbMes.SelectedIndex + 1).ToString();
            int mesanterior_int = cmbMes.SelectedIndex == 0 ? 12 : cmbMes.SelectedIndex;
            string mesAnterior = mesanterior_int < 10 ? $"0{mesanterior_int}" :mesanterior_int.ToString();


            if (mesAnterior == "12")
            {
                archivo = (globales.convertInt(txtAño.Text)-1).ToString().Substring(2) + mesAnterior;
            }
            else {
                archivo = txtAño.Text.Substring(2) + mesAnterior;
            }
            


            string añoPagar = $"{txtAño.Text}-{mes}-01";

            string query = "create temp table t1 as " +
                $" select jpp,num from nominas_catalogos.maestro where superviven = 'S' and fching < '{añoPagar}' " +
                $" except select jpp,numjpp from nominas_catalogos.respaldos_nominas where archivo = '{archivo}' group by jpp,numjpp; " +
                "select mma.* from t1 inner join nominas_catalogos.maestro mma on t1.jpp = mma.jpp and t1.num = mma.num order by mma.jpp,mma.num";

            List<Dictionary<string, object>> resultado = globales.consulta(query);

            resultado.ForEach(o => dtggrid.Rows.Add(o["jpp"], o["num"], o["rfc"], o["nombre"],o["fching"]));
        }

        private void dtggrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;
            if (c == -1) return;

            string jpp = string.Empty;
            string num = string.Empty;

            jpp = dtggrid.Rows[c].Cells[0].Value.ToString();
            num = dtggrid.Rows[c].Cells[1].Value.ToString();

            DateTime fechaIngreso = globales.convertDatetime(Convert.ToString(dtggrid.Rows[c].Cells[4].Value));

            string pago = string.Empty;


            int mes = cmbMes.SelectedIndex == 0 ? 12 : cmbMes.SelectedIndex;

            DateTime recorrer = new DateTime(Convert.ToInt32(txtAño.Text),mes,1);
            string query = $"select archivo from nominas_catalogos.respaldos_nominas where  jpp = '{jpp}' and numjpp = {num} group by archivo order by archivo desc";
            List<Dictionary<string, object>> obj = globales.consulta(query);

            pagos.Rows.Clear();
            while (fechaIngreso <= recorrer) {
                string mesStr = recorrer.Month < 10 ? $"0{recorrer.Month}" : recorrer.Month.ToString();
                string aniostr = recorrer.Year.ToString().Substring(2);

                string archivo = aniostr + mesStr;

                bool hay = obj.Any(o =>  Convert.ToString(o["archivo"]) == archivo );

                if (hay) break;

                DateTime ultimoMes = recorrer.AddMonths(1);
                ultimoMes = ultimoMes.AddDays(-1);

                string mensaje = string.Empty;

                
                    mensaje = $"{string.Format("{0:d}", recorrer)} a {string.Format("{0:d}", ultimoMes)}";
                

                pagos.Rows.Add(mensaje);

                recorrer = recorrer.AddMonths(-1);
            }

            DateTime mesultimofinal = new DateTime(fechaIngreso.Year,fechaIngreso.Month,1);
            mesultimofinal = mesultimofinal.AddMonths(1);
            mesultimofinal = mesultimofinal.AddDays(-1);

            string enviarmensaje = $"{string.Format("{0:d}", fechaIngreso)} a {string.Format("{0:d}", mesultimofinal)}";

            pagos.Rows.Add(enviarmensaje);

        }
    }
}
