using SISPE_MIGRACION.codigo.repositorios.datos;
using SISPE_MIGRACION.formularios.CATÁLOGOS;
using SISPE_MIGRACION.formularios.CATÁLOGOS.modales_catalogos;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.PRINCIPAL_PH.extras;
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
    public partial class frmexpediente : Form
    {
        string sueldo, solonumerosueldo, maximo;
        private bool boolRfc;

        public frmexpediente()
        {
            InitializeComponent();


        }

        private void llenacampos(Dictionary<string, object> datos)
        {

            limpiacampos();
            this.txtExpediente.Text = Convert.ToString(datos["folio"]);
            this.txtRfc.Text = Convert.ToString(datos["rfc"]);
            this.txtNombre_em.Text = Convert.ToString(datos["nombre_em"]);
            this.txtDireccion.Text = Convert.ToString(datos["direccion"]);
            this.txtFecha_nac.Text = Convert.ToString(datos["fecha_nac"]).Replace("12:00:00 a. m.", "");
            this.txtNombre_cony.Text = Convert.ToString(datos["nombre_cony"]);
            this.txtTel_partic.Text = Convert.ToString(datos["tel_partic"]);
            this.txtEdad.Text = Convert.ToString(datos["edad"]);
            this.txtEdo_civil.Text = Convert.ToString(datos["edo_civil"]);


            this.txtSecretaria.Text = Convert.ToString(datos["secretaria"]);
            this.txtProyecto.Text = Convert.ToString(datos["proyecto"]);
            this.txtF_nombram.Text = Convert.ToString(datos["f_nombram"]).Replace("12:00:00 a. m.", "");
            this.txtSueldobase.Text = Convert.ToString(datos["sueldo_base"]);
            this.txtCve_categ.Text = Convert.ToString(datos["cve_categ"]);
            this.txtTel_ofic.Text = Convert.ToString(datos["tel_ofici"]);
            this.txtAnt_a.Text = Convert.ToString(datos["ant_a"]);
            this.txtNomina.Text = Convert.ToString(datos["nomina"]);
            this.txtTipo_rel.Text = Convert.ToString(datos["tipo_rel"]);
            this.txtsexo.Text = Convert.ToString(datos["sexo"]);
            this.txtCcatdes.Text = Convert.ToString(datos["ccatdes"]);
            this.txtDescripcion.Text = Convert.ToString(datos["descripcion"]);
            // panel 2
            this.txtDire_inmueb.Text = Convert.ToString(datos["direc_inmu"]);
            controles(true);
            button1.Enabled = true;
            button3.Enabled = true;
        }

        private void limpiacampos()
        {

            txtAnt_a.Clear();
            txtNombre_cony.Clear();
            txtCve_categ.Clear();
            txtSecretaria.Clear();
            txtDireccion.Clear();
            txtEdad.Clear();
            txtEdo_civil.Clear();
            txtF_nombram.Clear();
            txtFecha_nac.Clear();
            txtNombre_em.Clear();
            txtNomina.Clear();
            txtProyecto.Clear();
            txtRfc.Clear();
            txtSueldobase.Clear();
            txtTel_partic.Clear();
            txtTel_ofic.Clear();
            txtTipo_rel.Clear();
            txtDire_inmueb.Clear();
            txtsexo.Clear();
            txtDescripcion.Clear();
            txtCcatdes.Clear();


        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmexpediente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }

            if (e.KeyCode == Keys.F5)
            {
                btnFolio_Click(null, null);
                if (string.IsNullOrWhiteSpace(txtExpediente.Text))
                {
                    txtExpediente.Text = "AUTOGENERADO";
                }

            }

            if (e.KeyCode == Keys.F9)
            {

            }
        }


        private void frmexpediente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void frmexpediente_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void controles(bool v)
        {


            txtRfc.Enabled = v;
            txtDireccion.Enabled = v;
            txtFecha_nac.Enabled = v;
            txtNombre_cony.Enabled = v;
            txtsexo.Enabled = v;
            txtTel_partic.Enabled = v;
            txtEdo_civil.Enabled = v;
            txtEdad.Enabled = v;

            txtSecretaria.Enabled = v;
            txtDescripcion.Enabled = v;
            txtProyecto.Enabled = v;
            txtF_nombram.Enabled = v;
            txtSueldobase.Enabled = v;
            txtCve_categ.Enabled = v;
            txtCcatdes.Enabled = v;
            txtAnt_a.Enabled = v;
            txtTel_ofic.Enabled = v;
            txtTipo_rel.Enabled = v;
            txtNomina.Enabled = v;


            txtDire_inmueb.Enabled = v;

            
        }

        private void rellenarConsulta(Dictionary<string, object> resultado, List<Dictionary<string, object>> avales, bool externo = false)
        {
            this.txtRfc.Text = Convert.ToString(resultado["rfc"]);
            this.txtNombre_em.Text = Convert.ToString(resultado["nombre_em"]);
            this.txtDireccion.Text = Convert.ToString(resultado["direccion"]);
            this.txtFecha_nac.Text = Convert.ToString(resultado["fecha_nac"]).Replace("12:00:00 a. m.", "");
            this.txtProyecto.Text = Convert.ToString(resultado["proyecto"]);
            this.txtSueldobase.Text = Convert.ToString(resultado["sueldo_base"]);
            this.txtCve_categ.Text = Convert.ToString(resultado["cve_categ"]);
            this.txtTipo_rel.Text = Convert.ToString(resultado["tipo_rel"]);
            this.txtsexo.Text = Convert.ToString(resultado["sexo"]);
            this.txtSecretaria.Text = Convert.ToString(resultado["depe"]);

            string dato = txtSecretaria.Text;
            string depe = "SELECT descripcion FROM catalogos.dependencias where proy = '{0}'";
            string convierte = string.Format(depe, dato);
            List<Dictionary<string, object>> tmp = globales.consulta(convierte);
            if (tmp.Count > 0)
            {
                string descripcion = Convert.ToString(tmp[0]["descripcion"]);
                this.txtDescripcion.Text = descripcion;
            }

        }

        

        private void nuevo(p_hipote obj)
        {

            
            dbaseORM orm = new dbaseORM();
            bool insertado = orm.insert<p_hipote>(obj);

            if (insertado)
            {
                globales.MessageBoxSuccess("NUEVO FOLIO AGREGADO:" + txtExpediente.Text, "PROCESO CORRECTO", globales.menuPrincipal);
                controles(false);
                limpiacampos();
                btnGuardar.Enabled = false;
                btnGuardar.Visible = false;

                btnModifica.Enabled = true;
                btnModifica.Visible = true;

                btnNuevo.Visible = true;
                btnNuevo.Enabled = true;
            }
            else
            {
                globales.MessageBoxError("ERROR , CONTACTE A SISTEMAS", "Aviso", globales.menuPrincipal);
            }

        }

        private void actualiza(p_hipote obj)
        {
            try
            {
                dbaseORM orm = new dbaseORM();
                bool actualizado = orm.update<p_hipote>(obj);
                if (actualizado)
                {
                    globales.MessageBoxSuccess(" SE ACTUALIZO EL SIGUIENTE FOLIO: " + txtExpediente.Text, "REGISTRO ACTUALIZADO", globales.menuPrincipal);
                    controles(false);
                    limpiacampos();
                    btnGuardar.Enabled = false;
                    btnGuardar.Visible = false;

                    btnModifica.Enabled = true;
                    btnModifica.Visible = true;

                    btnNuevo.Visible = true;
                    btnNuevo.Enabled = true;
                    txtExpediente.Enabled = false;
                    btnFolio.Enabled = false;
                    txtExpediente.Text = "";

                }
                else
                {
                    globales.MessageBoxError("Error al actualizar el registro", "Aviso", globales.menuPrincipal);
                }
            }

            catch
            {


            }

        }





        private void frmexpediente_Load(object sender, EventArgs e)
        {
            
            txtExpediente.Enabled = false;
            btnFolio.Enabled = false;

        }

        private void txtexpediente_KeyDown(object sender, KeyEventArgs e)
        {


      
        }

        private void txtRfc_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F1)
            {
                button1_Click_1(null,null);
            }

        }

        private void frmexpediente_Shown(object sender, EventArgs e)
        {
            txtRfc.Select();
        }

        private void btnFolio_Click(object sender, EventArgs e)
        {
            limpiacampos();
            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
            p_quirog.enviar2 = llenacampos;
            p_quirog.tablaConsultar = "p_hipote";
            p_quirog.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            p_hipote p = formarCuerpo();
            if (txtExpediente.Text == "AUTOGENERADO")
            {
                if (string.IsNullOrWhiteSpace(txtRfc.Text)) {
                    globales.MessageBoxExclamation("Favor de seleccionar RFC del empleado","Aviso",globales.menuPrincipal);
                    return;
                }
                string query = "SELECT COALESCE(max(folio)+1,1) as maximo FROM datos.p_hipote";
                List<Dictionary<string, object>> lista = globales.consulta(query);
                string folio = Convert.ToString(lista[0]["maximo"]);
                p.folio = int.Parse(folio);
                nuevo(p);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtExpediente.Text)) {
                    globales.MessageBoxExclamation("Favor de ingresar expediente.","Aviso",globales.menuPrincipal);
                    return;
                }
                p.folio = globales.convertInt(txtExpediente.Text);
                actualiza(p);

            }
            limpiacampos();
            btnFolio.Enabled = false;
            btnRfc.Enabled = false;

            btnsalir.Text = "SALIR";
        }

        private p_hipote formarCuerpo()
        {

            double solosueldo = globales.convertDouble(txtSueldobase.Text);
            solonumerosueldo = Convert.ToString(solosueldo);
            p_hipote hipote = new p_hipote();
            hipote.rfc = txtRfc.Text;
            hipote.nombre_em = txtNombre_em.Text;
            hipote.direccion = txtDireccion.Text;
            hipote.tel_partic = txtTel_partic.Text;
            hipote.fecha_nac = globales.convertDatetime(txtFecha_nac.Text);
            hipote.sexo = txtsexo.Text;
            hipote.edad = globales.convertInt(txtEdad.Text);
            hipote.nombre_cony = txtNombre_cony.Text;
            hipote.edo_civil = txtEdo_civil.Text;
            hipote.secretaria = txtSecretaria.Text;
            hipote.descripcion = txtDescripcion.Text;
            hipote.tel_ofici = txtTel_ofic.Text;
            hipote.proyecto = txtProyecto.Text;
            hipote.cve_categ = txtCve_categ.Text;
            hipote.ccatdes = txtCcatdes.Text;
            hipote.f_nombram = globales.convertDatetime(txtF_nombram.Text);
            hipote.ant_a = globales.convertInt(txtAnt_a.Text);
            hipote.tipo_rel = txtTipo_rel.Text;
            hipote.sueldo_base = globales.convertDouble(txtSueldobase.Text);
            hipote.nomina = txtNomina.Text;
            hipote.direc_inmu = txtDire_inmueb.Text;

            return hipote;
        }

        private void txtExpediente_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRfc_TextChanged(object sender, EventArgs e)
        {
            boolRfc = true;
        }

        private void txtNombre_em_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFecha_nac_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtsexo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEdad_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_cony_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnsalir_Click_1(object sender, EventArgs e)
        {
            if (btnsalir.Text == "CANCELAR")
            {
                DialogResult p = globales.MessageBoxQuestion("¿Deseas cancelar la operación?", "Aviso", globales.menuPrincipal);
                if (p == DialogResult.Yes)
                {
                    limpiacampos();
                    controles(false);
                    txtExpediente.Enabled = false;
                    btnFolio.Enabled = false;
                    btnRfc.Enabled = false;
                    btnsalir.Text = "SALIR";

                    btnGuardar.Enabled = false;
                    btnGuardar.Visible = false;

                    btnModifica.Enabled = true;
                    btnModifica.Visible = true;

                    btnNuevo.Enabled = true;
                    txtExpediente.Text = "";
                    button1.Enabled = false;
                }
            }
            else
            {
                this.Close();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            controles(true);
            txtExpediente.Text = "autogenerado";
            btnModifica.Visible = false;
            btnModifica.Enabled = false;

            btnGuardar.Visible = true;
            btnGuardar.Enabled = true;
            ActiveControl = txtRfc;
            btnsalir.Text = "CANCELAR";

            btnRfc.Enabled = true;
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            txtExpediente.Text = "";
            btnsalir.Text = "CANCELAR";

            btnModifica.Visible = false;
            btnModifica.Enabled = false;

            btnNuevo.Enabled = false;

            btnGuardar.Visible = true;
            btnGuardar.Enabled = false;
            ActiveControl = txtExpediente;

            txtExpediente.Enabled = true;
            btnFolio.Enabled = true;
            ActiveControl = txtExpediente;

            btnGuardar.Enabled = true;
        }

        private void txtExpediente_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtExpediente.Text))
            {
                string query = "select * from datos.p_hipote where folio={0}";
                query = string.Format(query, txtExpediente.Text);
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count != 0)
                {
                    Dictionary<string, object> item = resultado[0];
                    llenacampos(item);
                    btnGuardar.Enabled = true;
                }
                else
                {
                    globales.MessageBoxExclamation($"No existe el expediente N°: {txtExpediente.Text}", "Aviso", globales.menuPrincipal);
                    controles(false);
                    limpiacampos();
                    btnGuardar.Enabled = false;
                }
            }
        }

        private void txtFecha_nac_Leave(object sender, EventArgs e)
        {
            //Calculando fecha de nacimiento...

            try
            {
                DateTime fecha = DateTime.Parse(txtFecha_nac.Text);
                var dias = fecha - DateTime.Now;
                int diasint = dias.Days;

                int añosTranscurridos = Math.Abs(diasint / 365);

                txtEdad.Text = añosTranscurridos.ToString();

            }
            catch {
                globales.MessageBoxError("Error en la fecha de nacimiento","Aviso",globales.menuPrincipal);
            }
        }

        private void txtFecha_nac_Enter(object sender, EventArgs e)
        {
            //Calculando fecha de nacimiento 

            try
            {
                DateTime fecha = DateTime.Parse(txtFecha_nac.Text);
            }
            catch {
                //Si sale error se calcula la fecha de nacimiento en el rfc

                try
                {
                    string rfcAño = txtRfc.Text.Substring(4, 6);
                    string año = rfcAño.Substring(0, 2);
                    string mes = rfcAño.Substring(2, 2);
                    string dia = rfcAño.Substring(4, 2);

                    string resto = (Convert.ToInt32(año) <= DateTime.Now.Year) ? "19" : "20";//Este código perdurara para siempre por @samvProgrammer
                    año = resto + año;

                    DateTime fecha = new DateTime(Convert.ToInt32(año), Convert.ToInt32(mes), Convert.ToInt32(dia));
                    txtFecha_nac.Text = string.Format("{0:d}",fecha);
                   

                }
                catch {
                    globales.MessageBoxError("RFC incorrecto, no se puede determinar edad ni fecha de nacimiento.","Aviso",globales.menuPrincipal);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            limpiacampos();
            modalEmpleados empleados = new modalEmpleados();
            empleados.enviar = rellenarEmpleado;
            globales.showModal(empleados);
        }

        private void rellenarEmpleado(Dictionary<string, object> datos)
        {
            limpiacamposEmpleado();
            empleados obj = new dbaseORM().getObject<empleados>(datos);

            txtRfc.Text = obj.rfc;
            txtNombre_em.Text = obj.nombre_em;
            txtsexo.Text = obj.sexo;
            txtDireccion.Text = obj.direccion;
         //   txtSecretaria.Text = obj.proyecto;
            txtCve_categ.Text = obj.cve_categ;
            txtTipo_rel.Text = obj.tipo_rel;
            txtSueldobase.Text = globales.convertMoneda(obj.sueldo_base);
            txtProyecto.Text = (obj.proyecto.Length >=12) ? obj.proyecto.Substring(0, 12): obj.proyecto;
            txtNomina.Text = obj.modalidad.Replace("MMYS", "MMS");

            if (!string.IsNullOrWhiteSpace(txtCve_categ.Text))
            {
                string query = $"SELECT * FROM catalogos.categorias where ccatcve='{txtCve_categ.Text.Trim()}'";
                List<Dictionary<string, object>> resul = globales.consulta(query);
                if (resul.Count != 0) {
                    txtCcatdes.Text = Convert.ToString(resul[0]["ccatdes"]);
                }
            }
            txtDireccion.Select();
            button1.Enabled = true;
            button3.Enabled = true;

            verificaCreditosAnteriores();
        }

        private void verificaCreditosAnteriores()
        {
            string nombre = txtNombre_em.Text;
            string query = $"select * from datos.p_edocth where nombre_em like '%{txtNombre_em.Text}%' order by folio asc";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count > 0) {
                globales.MessageBoxExclamation("La persona seleccionada ya ha tramitado un crédito con anterioridad","Aviso",globales.menuPrincipal);
                frmAutorizaCreditos creditos = new frmAutorizaCreditos(resultado);
                globales.showModal(creditos);
            }
        }

        private void limpiacamposEmpleado()
        {
            txtRfc.Text = "";
            txtNombre_em.Text = "";
            txtsexo.Text = "";
            txtDireccion.Text = "";
            txtSecretaria.Text = "";
            txtCve_categ.Text = "";
            txtTipo_rel.Text = "";
            txtSueldobase.Text = "0.00";
        }

        private void txtRfc_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRfc.Text) || !boolRfc) return;


            string query = string.Format("select * from datos.empleados where rfc = '{0}'", txtRfc.Text);
            List<Dictionary<string, object>> listita = globales.consulta(query);
            if (listita.Count > 0)
            {
                boolRfc = false;
                rellenarEmpleado(listita[0]);
            }
            else
            {

                globales.MessageBoxExclamation("No se encuentra RFC en el cátalogo", "Aviso", globales.menuPrincipal);
                limpiacamposEmpleado();
                this.ActiveControl = txtRfc;

            }
            
        }

        private void txtRfc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
        }

        private void txtSecretaria_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            bool bandera = true;
            frmdependencias frm = new frmdependencias();
            frm.enviar = rellenarCampo;
            frm.ShowDialog();
        }

        private void rellenarCampo(Dictionary<string, object> datos, bool externo = false)
        {
            txtSecretaria.Text = Convert.ToString(datos["cuenta"]);
            txtDescripcion.Text = Convert.ToString(datos["descripcion"]);
            txtProyecto.Text = Convert.ToString(datos["proy"]);


        }

        private void txtCve_categ_Leave(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM catalogos.categorias where ccatcve='{txtCve_categ.Text}'";
            List<Dictionary<string, object>> resul = globales.consulta(query);
            if (resul.Count != 0) {
                txtCcatdes.Text = Convert.ToString(resul[0]["ccatdes"]);
            }
        }

        private void txtSecretaria_Leave(object sender, EventArgs e)
        {



            if (txtSecretaria.Text.Contains("J")) {
                txtSecretaria.Text = txtSecretaria.Text.Substring(0, 1);
                txtDescripcion.Text = "JUBILADO";
                txtProyecto.Text = "JUBILADO";
                txtCve_categ.Text = string.Empty;
                txtCcatdes.Text = string.Empty;

                txtTipo_rel.Text = txtSecretaria.Text;
                txtNomina.Text = "JUBILADO";

            }
            else if(txtSecretaria.Text.Contains("P")){
                txtDescripcion.Text = "PENSIONADO";
                txtSecretaria.Text = txtSecretaria.Text.Substring(0, 1);
                txtProyecto.Text = "PENSIONADO";
                txtCve_categ.Text = string.Empty;
                txtCcatdes.Text = string.Empty;

                txtTipo_rel.Text = txtSecretaria.Text;
                txtNomina.Text = "PENSIONADO";
            }
            else {
                string query = $"SELECT * FROM catalogos.cuentas where proy='{txtSecretaria.Text}'";
                List<Dictionary<string, object>> datos = globales.consulta(query);
                if (string.IsNullOrWhiteSpace(txtSecretaria.Text)) return;
                if (datos.Count != 0)
                {
                    //txtSecretaria.Text = Convert.ToString(datos[0]["proy"]);
                    txtDescripcion.Text = Convert.ToString(datos[0]["descripcion"]);
                    //txtProyecto.Text = Convert.ToString(datos[0]["proy"]);
                }
                else {
                    txtSecretaria.Text = string.Empty;
                    txtDescripcion.Text = string.Empty;
                }
            }

        }

        private void txtEdo_civil_Leave(object sender, EventArgs e)
        {
            txtSecretaria.Select();
        }

        private void txtSecretaria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F1)
            {
                button1_Click_2(null, null);
            }
        }

        private void txtTipo_rel_Leave(object sender, EventArgs e)
        {
            string strCuenta = txtTipo_rel.Text;
            string tipoRelacion="";
            switch (strCuenta)
            {
                case "F51":
                    tipoRelacion = "BASE";//BAS
                    break;
                case "M51":
                    tipoRelacion = "BASE";//BAS
                    break;
                case "MBN":
                    tipoRelacion = "BASE";//BASE
                    break;
                case "SBN":
                    tipoRelacion = "BASE";
                    break;
                case "511":
                    tipoRelacion = "CONF";
                    break;
                case "CMM":
                    tipoRelacion = "CONF";
                    break;
                case "FCO":
                    tipoRelacion = "CONF";
                    break;
                case "MCN":
                    tipoRelacion = "CONF";
                    break;
                case "SCN":
                    tipoRelacion = "CONF";
                    break;
                case "FMM":
                    tipoRelacion = "MMS";
                    break;
                case "MM5":
                    tipoRelacion = "MMS";
                    break;
                case "MMM":
                    tipoRelacion = "MMS";
                    break;
                case "MMS":
                    tipoRelacion = "MMS";
                    break;
                case "SMM":
                    tipoRelacion = "MMS";
                    break;
                case "JUB":
                    tipoRelacion = "JUBILADO";
                    break;
                case "PDO":
                    tipoRelacion = "PENSIONADO";
                    break;
                default:
                    break;
            }
            if (string.IsNullOrWhiteSpace(txtNomina.Text)) {
                txtNomina.Text = tipoRelacion;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmCategorias frm = new frmCategorias();
            frm.enviar = rellenarDepe;
            frm.ShowDialog();
        }

        private void txtteléfono_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtCve_categ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F1)
            {
                button3_Click(null, null);
            }
        }

        private void txtDire_inmueb_Leave(object sender, EventArgs e)
        {
            
        }

        private void rellenarDepe(Dictionary<string, object> datos, bool externo = false)
        {
            foreach (var item in datos) {
                txtCve_categ.Text = Convert.ToString(datos["ccatcve"]);
                txtCcatdes.Text = Convert.ToString(datos["ccatdes"]);
                    }
        }

        private void txtsueldob_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double numero = globales.convertDouble(txtSueldobase.Text);
                sueldo = string.Format("{0:C}", (numero));
                this.txtSueldobase.Text = sueldo;

            }
        }


    }
}
