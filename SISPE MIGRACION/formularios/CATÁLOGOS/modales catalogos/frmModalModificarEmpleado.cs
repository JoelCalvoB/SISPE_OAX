using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.CATÁLOGOS.modales_catalogos
{
    public partial class frmModalModificarEmpleado : Form
    {

        private string rfc { get; set; }
        public frmModalModificarEmpleado(string rfc)
        {
            InitializeComponent();
            this.rfc = rfc;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmModalModificarEmpleado_Load(object sender, EventArgs e)
        {
            string query = $"select * from datos.empleados where rfc = '{this.rfc}' and pendiente = 'f'";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count != 0) {
                Dictionary<string, object> persona = resultado[0];
                txtrfc.Text = persona["rfc"].ToString();
                txtnombre.Text = persona["nombre_em"].ToString();
                txtstatus.Text = persona["status"].ToString();
                rdMasculino.Checked = (((!string.IsNullOrWhiteSpace(persona["sexo"] as string)) && persona["sexo"].ToString() == "M"));
                rdFemenino.Checked = (((!string.IsNullOrWhiteSpace(persona["sexo"] as string)) && persona["sexo"].ToString() == "F"));
                txtdirec.Text = persona["direccion"].ToString();
                txtNap.Text = Convert.ToString(persona["nap"]);
                txtDependencia.Text = persona["depe"].ToString();
                txtClaveCategoria.Text = persona["cve_categ"].ToString();
                fechaNacimiento.Text = persona["fecha_nac"].ToString();
                fechaIngresos.Text = persona["fecha_ing"].ToString();
                txtProyectos.Text = persona["proyecto"].ToString();
                txtSueldoBase.Text = string.Format("{0:C}", persona["sueldo_base"]);
                txtRelacionLaboral.Text = persona["tipo_rel"].ToString();
                txtTel.Text= persona["telefono"].ToString();
            }
        }

        private bool validaciones()
        {

            if (string.IsNullOrWhiteSpace(txtrfc.Text))
            {
                globales.MessageBoxExclamation("favor de ingresar el rfc", "Advertencia", globales.menuPrincipal);
                txtrfc.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtnombre.Text))
            {
                globales.MessageBoxExclamation("favor de ingresar el nombre", "Advertencia", globales.menuPrincipal);
                txtnombre.Focus();
                return false;
            }
            else if (!rdFemenino.Checked && !rdMasculino.Checked)
            {
                globales.MessageBoxExclamation("favor seleccionar sexo", "Advertencia", globales.menuPrincipal);
                rdMasculino.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(fechaIngresos.Text))
            {
                globales.MessageBoxExclamation("favor de ingresar fecha de nombramiento", "Advertencia", globales.menuPrincipal);
                fechaIngresos.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Desea realizar la operación?", "Aviso", globales.menuPrincipal);
            if (p == DialogResult.No) return;
            if (!validaciones()) return;
            string query = string.Empty;

            string p1 = txtrfc.Text;
            string p2 = txtnombre.Text;
            string p3 = (rdMasculino.Checked) ? "M" : "F";
            string p4 = string.IsNullOrWhiteSpace(fechaNacimiento.Text.Replace("/", "")) ? "null" : "'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(fechaNacimiento.Text)) + "'";
            string p5 = txtstatus.Text;
            string p6 = txtdirec.Text;
            string p7 = (string.IsNullOrWhiteSpace(txtNap.Text)) ? "0" : txtNap.Text;
            string p8 = string.IsNullOrWhiteSpace(fechaIngresos.Text.Replace("/", "")) ? "null" : "'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(fechaIngresos.Text)) + "'";
            string p9 = txtDependencia.Text;
            string p10 = txtProyectos.Text;
            string p11 = txtClaveCategoria.Text;
            string p12 = (string.IsNullOrWhiteSpace(txtSueldoBase.Text)) ? "0" : Convert.ToString(double.Parse(txtSueldoBase.Text, System.Globalization.NumberStyles.Currency));
            string p13 = txtRelacionLaboral.Text;
            string p14 = txtTel.Text;

            query = string.Format("update datos.empleados set nombre_em='{1}',sexo='{2}',fecha_nac={3},status='{4}',direccion='{5}'," +
                                                 "nap={6},fecha_ing={7},depe='{8}',proyecto='{9}',cve_categ='{10}',sueldo_base={11},tipo_rel='{12}', telefono='{13}' where rfc = '{0}'"
                                                 , p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13 , p14);

            if (globales.consulta(query, true)) {
                globales.MessageBoxSuccess("Registro actualizado","Aviso",globales.menuPrincipal);
                this.Owner.Close();
            }
        }

        private void frmModalModificarEmpleado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) {
                this.Owner.Close();
            }
        }

        private void frmModalModificarEmpleado_Shown(object sender, EventArgs e)
        {
            button2.Focus();
        }

        private void txtSueldoBase_Leave(object sender, EventArgs e)
        {
            txtSueldoBase.Text = globales.convertMoneda(globales.convertDouble(txtSueldoBase.Text));
        }
    }
}
