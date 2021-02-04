using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.UTILERIAS
{
    public partial class frmGenerarMesCheque : Form
    {
        private string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        public frmGenerarMesCheque()
        {
            InitializeComponent();
        }

        private void frmGenerarMesCheque_Load(object sender, EventArgs e)
        {
            DateTime hoy = DateTime.Now;
            cmbAño.Items.Add(hoy.Year);
            cmbAño.Items.Add(hoy.Year+1);

            cmbAño.SelectedIndex = 0;

            for (int x = 1; x < meses.Length; x++) {
                cmbMes.Items.Add(meses[x]);
            }
            cmbMes.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (globales.MessageBoxQuestion("¿Deseas generar el mes siguiente?","Meses",this)==DialogResult.No) {
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCantidad.Text)) {
                 globales.MessageBoxExclamation("Se debe ingresar la cantidad de emisión de cheques", "Cantidad de cheques",this);
                txtCantidad.Focus();
                return;
            }

            //Consulta para verificar fechas ingresadas previamente

            Dictionary<string, string> semanaEspañol = new Dictionary<string, string>();
            semanaEspañol.Add("Monday", "Lunes");
            semanaEspañol.Add("Tuesday", "Martes");
            semanaEspañol.Add("Wednesday", "Miercoles");
            semanaEspañol.Add("Thursday", "Jueves");
            semanaEspañol.Add("Friday", "Viernes");
            semanaEspañol.Add("Saturday", "Sábado");
            semanaEspañol.Add("Sunday", "Domingo");

            DateTime tiempo = new DateTime(Convert.ToInt32(cmbAño.Text), cmbMes.SelectedIndex + 1,1);
            string fecha1 = string.Format("{0:yyyy-MM-dd}",tiempo);
            tiempo = tiempo.AddMonths(1);
            tiempo = tiempo.AddDays(-1);
            string fecha2 = string.Format("{0:yyyy-MM-dd}",tiempo);

            string tmpQuery = string.Format("select count(fecha) as cantidad from catalogos.progpq  where fecha BETWEEN '{0}' and '{1}' ", fecha1,fecha2);
            List<Dictionary<string, object>> tmpResultado = globales.consulta(tmpQuery);
            int cantidad = Convert.ToInt32(tmpResultado[0]["cantidad"]);

            if (cantidad > 0) {
                globales.MessageBoxExclamation("Ya se encuentra el mes generado","Aviso",this);
                return;
            }
            int dias = DateTime.DaysInMonth(Convert.ToInt32(cmbAño.Text),Convert.ToInt32(cmbMes.SelectedIndex+1));
            
            this.Cursor = Cursors.WaitCursor;
            DateTime fecha = new DateTime(Convert.ToInt32(cmbAño.Text),cmbMes.SelectedIndex+1,1);
            int contadorDia = 1;
            List<Dictionary<string, object>> listaDias = new List<Dictionary<string, object>>();
            while (true) {
                cantidad = (string.IsNullOrWhiteSpace(txtCantidad.Text))?0:Convert.ToInt32(txtCantidad.Text);
                string fechaInsertar = string.Format("{0}-{1}-{2}",cmbAño.Text,cmbMes.SelectedIndex+1,contadorDia++);
                DayOfWeek nombreDia = fecha.DayOfWeek;
                
                string nombre = nombreDia.ToString();
                string enable = "";
                if (nombre == "Saturday" || nombre == "Sunday")
                    enable = "*";
                
                Dictionary<string, object> diccionario = new Dictionary<string, object>();
                diccionario.Add("fecha",fecha);
                diccionario.Add("enable",enable);
                diccionario.Add("cantidad",0);
                diccionario.Add("dia",semanaEspañol[nombre]);
                if (cmbMes.SelectedIndex != 11)
                {
                    if (cmbMes.SelectedIndex + 1 != fecha.Month)
                    {
                        break; //Termina la inserción del mes...
                    }
                }
                else {
                    if (fecha.Month != 12) {
                        break;//Rompe el ciclo...
                    }
                }
                fecha = fecha.AddDays(1);
                //globales.consulta(query,true);
                listaDias.Add(diccionario);
            }

            globales.MessageBoxSuccess("Proceso terminado...", "Proceso terminado", this);
            this.Cursor = Cursors.Default;
            globales.showModal(new frmGuardarProgramado(listaDias, Convert.ToInt32(txtCantidad.Text)));
            this.Owner.Close();

        }

        private void cmbMes_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
