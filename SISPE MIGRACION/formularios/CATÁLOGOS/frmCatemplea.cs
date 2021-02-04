using SISPE_MIGRACION.codigo.baseDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    public partial class frmCatemplea : Form
    {

        private List<Dictionary<string, object>> resultado;
        private Dictionary<string, object> persona;
        private bool esInsertar = false;
        private bool esModal { get; set; }
        public frmCatemplea()
        {
            InitializeComponent();
            txtrfc.KeyDown += new KeyEventHandler(pasar);
            txtnombre.KeyDown += new KeyEventHandler(pasar);
            rdMasculino.KeyDown += new KeyEventHandler(pasar);
            rdFemenino.KeyDown += new KeyEventHandler(pasar);
            fechaNacimiento.KeyDown += new KeyEventHandler(pasar);
            txtstatus.KeyDown += new KeyEventHandler(pasar);
            txtdirec.KeyDown += new KeyEventHandler(pasar);
            txtNap.KeyDown += new KeyEventHandler(pasar);
            fechaIngresos.KeyDown += new KeyEventHandler(pasar);
            txtDependencia.KeyDown += new KeyEventHandler(pasar);
            txtProyectos.KeyDown += new KeyEventHandler(pasar);
            txtClaveCategoria.KeyDown += new KeyEventHandler(pasar);
            txtSueldoBase.KeyDown += new KeyEventHandler(pasar);
            txtRelacionLaboral.KeyDown += new KeyEventHandler(pasar);
            txtTel.KeyDown += new KeyEventHandler(pasar);


            this.esModal = esModal;
        }

        private void pasar(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void txtbuscar_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down) {
                    if (!string.IsNullOrWhiteSpace(txtbuscar.Text)) {
                        if (listBusqueda.Items.Count != 0) {
                            listBusqueda.Focus();
                            listBusqueda.SelectedIndex = 0;
                        }
                    }
                    return;
                }

                listBusqueda.Items.Clear();
                if (string.IsNullOrWhiteSpace(txtbuscar.Text))
                    return;
                string busqueda = txtbuscar.Text;
                string query = string.Format("select * from datos.empleados where (rfc like '{0}%' OR nombre_em LIKE  '{0}%') and pendiente = 'f' limit 30;", busqueda,busqueda);
                resultado = baseDatos.consulta(query);
                resultado.ForEach(o => listBusqueda.Items.Add(o["rfc"]+" | "+o["nombre_em"]));
            }
            catch {

            }
        }

        private void listBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {
                ListBox lista = sender as ListBox;
                int seleccionado = lista.SelectedIndex;
                if (seleccionado == -1) return;
                limpiarControles();
                activarControles(true);
                txtrfc.Enabled = false;               
                persona = resultado[seleccionado];
                txtrfc.Text = persona["rfc"].ToString();
                txtnombre.Text = persona["nombre_em"].ToString();
                txtstatus.Text = persona["status"].ToString();
                rdMasculino.Checked = (((!string.IsNullOrWhiteSpace(persona["sexo"] as string)) && persona["sexo"].ToString() == "H"));
                rdFemenino.Checked = (((!string.IsNullOrWhiteSpace(persona["sexo"] as string)) && persona["sexo"].ToString() == "F"));
                txtdirec.Text = persona["direccion"].ToString();
                txtNap.Text = Convert.ToString( persona["nap"]);
                txtDependencia.Text = persona["depe"].ToString();
                txtClaveCategoria.Text = persona["cve_categ"].ToString();
                fechaNacimiento.Text = persona["fecha_nac"].ToString();
                fechaIngresos.Text = persona["fecha_ing"].ToString();
                txtProyectos.Text = persona["proyecto"].ToString();
                txtSueldoBase.Text = string.Format("{0:C}", persona["sueldo_base"]);
                txtRelacionLaboral.Text = persona["tipo_rel"].ToString();
                txtTel.Text = persona["telefono"].ToString();

                txtbuscar.Text = "";
                lista.Items.Clear();
                this.esInsertar = false;
                ActiveControl = txtnombre;

                if (!rdFemenino.Checked)
                    rdMasculino.Checked = true;
            }
        }

        private void frmCatemplea_Load(object sender, EventArgs e)
        {
            activarControles(false);
           
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
           
            
        }

        private void activarControles(bool activar)
        {
            txtrfc.Enabled = activar;
            rdFemenino.Enabled = activar;
            rdMasculino.Enabled = activar;
            txtnombre.Enabled = activar;
            fechaIngresos.Enabled = activar;
            fechaNacimiento.Enabled = activar;
            txtstatus.Enabled = activar;
            txtdirec.Enabled = activar;
            txtNap.Enabled = activar;
            txtDependencia.Enabled = activar;
            txtProyectos.Enabled = activar;
            txtClaveCategoria.Enabled = activar;
            txtSueldoBase.Enabled = activar;
            txtRelacionLaboral.Enabled = activar;
            txtTel.Enabled = activar;

        }

        private void limpiarControles() {
            txtrfc.Text = "";
            rdFemenino.Checked = false;
            rdMasculino.Checked = true;
            txtnombre.Text = "";
            fechaIngresos.Text = "";
            fechaNacimiento.Text = "";
            txtstatus.Text = "";
            txtdirec.Text = "";
            txtNap.Text = "";
            txtDependencia.Text = "";
            txtProyectos.Text = "";
            txtClaveCategoria.Text = "";
            txtSueldoBase.Text = "";
            txtTel.Text = "";

            txtRelacionLaboral.Text = "";
            checkFallecido.Checked = false;
            fec_falle.Visible = false;
        }


        private void btnNuevoclick(object sender, EventArgs e)
        {
        



        }

        private bool validaciones()
        {

            if (string.IsNullOrWhiteSpace(txtrfc.Text))
            {
                globales.MessageBoxExclamation("favor de ingresar el rfc", "Advertencia",globales.menuPrincipal);
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
            else {
                return true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            if (validaciones()) {
                string p1 = txtrfc.Text;
                string p2 = txtnombre.Text;
                string p3 = (rdMasculino.Checked) ? "H" : "F";
                string p4 = string.IsNullOrWhiteSpace(fechaNacimiento.Text.Replace("/","")) ? "null" : "'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(fechaNacimiento.Text)) + "'";

                string p5 = txtstatus.Text;
                string p6 = txtdirec.Text;
                string p7 = (string.IsNullOrWhiteSpace(txtNap.Text)) ? "0" : txtNap.Text;
                string p8 = string.IsNullOrWhiteSpace(fechaIngresos.Text.Replace("/",""))?"null": "'"+ string.Format("{0:yyyy-MM-dd}", DateTime.Parse(fechaIngresos.Text))+"'";
                string p9 = txtDependencia.Text;
                string p10 = txtProyectos.Text;
                string p11 = txtClaveCategoria.Text;
                string p12 = (string.IsNullOrWhiteSpace(txtSueldoBase.Text)) ? "0" : Convert.ToString(double.Parse(txtSueldoBase.Text,System.Globalization.NumberStyles.Currency));
                string p13 = txtRelacionLaboral.Text;
                string p14 = txtTel.Text;
                string identificador = persona["id"].ToString();

                string query = string.Format("update datos.empleados set nombre_em = '{0}',  sexo = '{1}', "+
                                             "fecha_nac = {2},  status = '{3}',  direccion = '{4}', "+
                                             "nap = {5},  fecha_ing = {6},  depe = '{7}',  proyecto = '{8}', "+
                                             "cve_categ = '{9}',  sueldo_base = {10},  tipo_rel = '{11}' , telefono= '{12}' "+
                                             "where id = {12}",p2,p3,p4,p5,p6,p7,p8,p9,p10,p11,p12,p13,p14,identificador);


                if (baseDatos.consulta(query, true))
                {
                    globales.MessageBoxSuccess("Registro del empleado actualizado correctamente", "Registro actualizado", globales.menuPrincipal);
                    
                }
                else
                {
                    globales.MessageBoxExclamation("Error en la actualización de datos, favor de contactar a sistemas", "Advertencia", globales.menuPrincipal);
                }

                limpiarControles();
                activarControles(false);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = globales.MessageBoxQuestion("¿Desea eliminar este registro?","Eliminando registro",globales.menuPrincipal);
            if (resultado == DialogResult.Yes) {
                string id = persona["id"].ToString();
                string query = string.Format("delete from datos.empleados where id = {0}",id);
                if (baseDatos.consulta(query,true)) {
                    globales.MessageBoxSuccess("Registro del empleado eliminado correctamente","Registro eliminado",globales.menuPrincipal);
                    limpiarControles();
                    activarControles(false);
                  
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            activarControles(false);
        }

        private void frmCatemplea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                button2_Click_1(null,null);
                return;
            }

            if (e.KeyCode == Keys.Insert) {
                activarControles(true);
                this.esInsertar = true;
                this.ActiveControl = txtrfc;
                limpiarControles();
            }

            if (e.KeyCode == Keys.F11) {
                btbuscar_Click(null,null);
            }

            if (e.KeyCode == Keys.F5) {
                txtbuscar.Focus();
            }

        }

        private void frmCatemplea_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
          
                this.Close();
        }

        private void listBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (((e.KeyValue >= 'A' && e.KeyValue <= 'Z') || e.KeyValue >= '0' && e.KeyValue <= '9') || e.KeyCode == Keys.Back) {
                txtbuscar.Focus();
            }
        }

        private void txtSueldoBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);
        }

        private void txtSueldoBase_Leave(object sender, EventArgs e)
        {
            try
            {
                double cantidad = double.Parse(txtSueldoBase.Text,System.Globalization.NumberStyles.Currency);
                txtSueldoBase.Text = string.Format("{0:C}",cantidad);
            }
            catch {
                txtSueldoBase.Text = string.Format("{0:C}",0);
            }
        }

        private void frmCatemplea_Shown(object sender, EventArgs e)
        {
            txtbuscar.Focus();
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult p = globales.MessageBoxQuestion("¿Desea realizar la operación?","Aviso",globales.menuPrincipal);
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


                listBusqueda.Items.Clear();
                txtbuscar.Text = "";
                string fallecido = string.Empty;
                string fec_fallecio = string.Empty;
                if (checkFallecido.Checked)
                {
                    fallecido = "true";
                    fec_fallecio ="'"+ fec_falle.Text+"'";
                }
                else
                {
                    fallecido = "null";
                    fec_fallecio = "null";
                }

                if (this.esInsertar)
                {
                    query = string.Format("insert into datos.empleados(rfc,nombre_em,sexo,fecha_nac,status,direccion," +
                                                 "nap,fecha_ing,depe,proyecto,cve_categ,sueldo_base,tipo_rel , fallecio , fec_fallecio , telefono) VALUES" +
                                                 "('{0}', '{1}', '{2}', {3}, '{4}', '{5}'," +
                                                 " {6}, {7}, '{8}', '{9}', '{10}', {11}, '{12}',{13},{14},{15})", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, fallecido, fec_fallecio , p14);
                }
                else {
                    query = string.Format("update datos.empleados set nombre_em='{1}',sexo='{2}',fecha_nac={3},status='{4}',direccion='{5}'," +
                                                 "nap={6},fecha_ing={7},depe='{8}',proyecto='{9}',cve_categ='{10}',sueldo_base={11},tipo_rel='{12}' , fallecio ={13} , fec_fallecio={14} , telefono ='{15}' where rfc = '{0}'"
                                                 , p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13 , fallecido , fec_fallecio , p14);

                }

                if (globales.consulta(query, true))
                {
                    string mensaje = "";
                    if (this.esInsertar)
                    {
                        mensaje = "Registro insertado correctamente";
                    }
                    else {
                        mensaje = "Registro actualizado correctamente";
                    }
                    globales.MessageBoxSuccess(mensaje,"Aviso",globales.menuPrincipal);
                    limpiarControles();
                    activarControles(false);
                }
                else
                {
                    globales.MessageBoxError("Error en hacer la operación a la base de datos, llamar al de sistemas", "Aviso", globales.menuPrincipal);
                }
            }
            catch {
                globales.MessageBoxError("Error en el sistema","Error",globales.menuPrincipal);
            }
        }

        private void listBusqueda_DoubleClick(object sender, EventArgs e)
        {
            ListBox lista = sender as ListBox;
            int seleccionado = lista.SelectedIndex;
            if (seleccionado == -1) return;
            limpiarControles();
            activarControles(true);
            txtrfc.Enabled = false;
            persona = resultado[seleccionado];
            txtrfc.Text = persona["rfc"].ToString();
            txtnombre.Text = persona["nombre_em"].ToString();
            txtstatus.Text = persona["status"].ToString();
            rdMasculino.Checked = (((!string.IsNullOrWhiteSpace(persona["sexo"] as string)) && persona["sexo"].ToString() == "H"));
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
            txtTel.Text = persona["telefono"].ToString();

            txtbuscar.Text = "";
            lista.Items.Clear();
            this.esInsertar = false;
            ActiveControl = txtnombre;

            if (!rdFemenino.Checked)
                rdMasculino.Checked = true;
            string f = Convert.ToString(persona["fallecio"]);
            if (!string.IsNullOrWhiteSpace(f))
                {
                checkFallecido.Checked = true;
                fec_falle.Text = Convert.ToString(persona["fec_fallecio"]);
            }
            else
            {
                checkFallecido.Checked = false;

            }
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFallecido.Checked)
            {
                fec_falle.Visible = true;
               
            }
          else
            {
                fec_falle.Visible = false;
            }
        }
    }
}
