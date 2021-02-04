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
    public partial class frmFecha_statust : Form
    {
        public frmFecha_statust()
        {
            InitializeComponent();
        }

        private void frmFecha_aut_Shown(object sender, EventArgs e)
        {
            DateTime hoy = DateTime.Now;
            txtFecha.Text = Convert.ToString(hoy);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fecha = globales.parseDateTime(globales.convertDatetime(txtFecha.Text));
            if (string.IsNullOrWhiteSpace(fecha)) {
                globales.MessageBoxExclamation("Favor de ingresar fecha correctamente","Aviso",globales.menuPrincipal);
                txtFecha.Text = fecha;
                return;
            }



            string query = $"WITH resultadoUpdate AS (update datos.h_solici set status='A' where f_autorizacion='{string.Format("{0:yyyy-MM-dd}",globales.convertDatetime(txtFecha.Text))}' and status <> 'A' RETURNING status) SELECT COUNT(status) as conteo FROM resultadoUpdate";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            int conteo = globales.convertInt(Convert.ToString(resultado[0]["conteo"]));

            if (conteo != 0) {
                globales.MessageBoxSuccess("Se afecto status de " + Convert.ToString(conteo) + " registros", "Aviso", globales.menuPrincipal);
            }
            else {
                globales.MessageBoxExclamation("No se encontro fecha de autorización a afectar ó el status ha sido afectado anteriormente","Aviso",globales.menuPrincipal);
            }


            this.Owner.Close();
                 
            
          
        }

        private void txtFecha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmFecha_statust_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.F2 == e.KeyCode) {
                this.Owner.Close();
            }
        }
    }
}
