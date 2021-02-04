using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH
{
    public partial class frmFechaAutorizacion : Form
    {
        public frmFechaAutorizacion()
        {
            InitializeComponent();
        }

        private void frmFechaAutorizacion_Shown(object sender, EventArgs e)
        {
            DateTime actual = DateTime.Now;
            txtF1.Text = Convert.ToString(actual);
            txtF2.Text = Convert.ToString(actual);
        }

        private void txtFecha_A_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtF2.Focus();
        }

        private void txtFecha_N_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtF1.Text == txtF2.Text)
            {
                globales.MessageBoxExclamation("NO PUEDE INGRESAR FECHAS IGUALES, FAVOR DE VALIDAR", "VALIDAR!", globales.menuPrincipal);
            }
            else
            {
                string query = " WITH RETURNUPDATE AS(update datos.h_solici set f_autorizacion ='{0}' where f_autorizacion='{1}' RETURNING f_autorizacion) select count(f_autorizacion) as conteo from RETURNUPDATE";
                string pasa = string.Format(query, txtF2.Text, txtF1.Text);
                List<Dictionary<string, object>> resultado = globales.consulta(pasa);
                int conteo = Convert.ToInt32(resultado[0]["conteo"]);

                if (conteo>0)
                {
                    DialogResult dialogp = globales.MessageBoxSuccess("SE ACTUALIZARÓN " + conteo + " REGISTROS", "PROCESO HECHO CORRECTAMENTE", globales.menuPrincipal);
                }
                else
                {
                    DialogResult diag = globales.MessageBoxExclamation("NO SE ENCUENTRA LA FECHA QUE SE DESEA ACTUALIZAR", "VALIDAR", globales.menuPrincipal);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas realizar la operación?","Aviso",globales.menuPrincipal);
            if (dialogo == DialogResult.No) return;

            string t1 = globales.parseDateTime(globales.convertDatetime(txtF1.Text));
            string t2 = globales.parseDateTime(globales.convertDatetime(txtF2.Text));

            txtF1.Text = t1;
            txtF2.Text = t2;

            if (string.IsNullOrWhiteSpace(t1) || string.IsNullOrWhiteSpace(t2))
            {
                globales.MessageBoxExclamation("Fechas incorrectas","Aviso",globales.menuPrincipal);
                return;
            }

            string query = " WITH RETURNUPDATE AS(update datos.h_solici set f_autorizacion ='{0}' where f_autorizacion='{1}' RETURNING f_autorizacion) select count(f_autorizacion) as conteo from RETURNUPDATE";
                query = string.Format(query, txtF2.Text, txtF1.Text);
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                int conteo = Convert.ToInt32(resultado[0]["conteo"]);

            if (conteo != 0)
            {
                globales.MessageBoxSuccess("Se actualizarón " + conteo + " registros", "Aviso", globales.menuPrincipal);
                this.Owner.Close();
            }
            else
                globales.MessageBoxExclamation("No se encuentra fechas a actualizar", "Aviso", globales.menuPrincipal);
        }

        private void frmFechaAutorizacion_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmFechaAutorizacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.F2 == e.KeyCode) this.Owner.Close();
        }
    }
}
