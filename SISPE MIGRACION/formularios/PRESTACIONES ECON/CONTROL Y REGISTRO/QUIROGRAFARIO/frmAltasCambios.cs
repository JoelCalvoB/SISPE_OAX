using SISPE_MIGRACION.codigo.repositorios.datos;
using SISPE_MIGRACION.formularios.CATÁLOGOS;
using SISPE_MIGRACION.formularios.CATÁLOGOS.modales_catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.QUIROGRAFARIO
{
    delegate void rellenar(Dictionary<string,object> resultado);
 
    public partial class frmAltasCambios : Form
    {
        private frmCatalogoP_quirog frmFolios;
        private int secuencia = 0;
        private int numero = 1;
        private string nombreEmpleado = "3";
        

        private bool boolFolio { get; set; }
      

        private void ejemplo2(string jaja) { }
        public frmAltasCambios()
        {
            InitializeComponent();
            txtTipo_mov1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtSec1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtF_descuento1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtProyecto1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtNombre_em1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtTipo_rel1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtNumdesc1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtTotdesc1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtImp_unit1.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtFolio_.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtNumdesc_.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtTotdesc_.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);
            txtImp_unit_.KeyPress += new KeyPressEventHandler(rellenarDatosGenerales);

            txtRfc.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtFolio.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtUbic_pagare.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);

           // txtTipo_mov1.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtSec1.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtF_descuento1.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtProyecto1.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtNombre_em1.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtTipo_rel1.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtNumdesc1.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtTotdesc1.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtImp_unit1.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtFolio_.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtNumdesc_.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtTotdesc_.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtImp_unit_.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtRfc1.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtSecretaria.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtTipo_pago.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtPlazo.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtImp_unit.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtImporte.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtF_primdesc.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
        }

        private void tabs(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                SendKeys.Send("{TAB}");
            }
        }

        private void rellenarDatosGenerales(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 3)
            {
                
                    txtProyecto1.Text = txtProyecto.Text;
                    txtRfc1.Text = txtRfc.Text;
                    txtNombre_em1.Text = txtNombre_em.Text;
                    txtNumdesc1.Text = "1";
                    txtTotdesc1.Text = txtPlazo.Text;
                    txtImp_unit1.Text = txtImp_unit.Text;
               
              
            }
            
        }

        private void frmAltasCambios_Load(object sender, EventArgs e)
        {
            frmFolios = new frmCatalogoP_quirog();
            frmFolios.tablaConsultar = "p_edocta";
            frmFolios.enviar2 = rellenarCamposFolio;

            string query = "select * from datos.p_edocta order by folio desc limit 1";
            List<Dictionary<string, object>> r1 = globales.consulta(query);

            if (r1.Count != 0)
                rellenarCamposFolio(r1[0]);

            this.boolFolio = false;
           // txtTipo_rel1.Text = "NA";
            btnFolio.Enabled = true;
            this.ActiveControl = txtFolio;
        }

        public void rellenarCamposFolio(Dictionary<string,object>resultado) {
            try
            {
                string query = $"SELECT descripcion FROM catalogos.cuentas where proy='{Convert.ToString(resultado["secretaria"])}'";
                List< Dictionary<string, object>> res = globales.consulta(query);
                string descripcion = (res.Count != 0) ? Convert.ToString(res[0]["descripcion"]):"";

                txtFolio.Text = Convert.ToString(resultado["folio"]);
                txtSecretaria.Text = Convert.ToString(resultado["secretaria"]);
                txtSecretariaDescripcion.Text = descripcion;
                txtRfc.Text = Convert.ToString (resultado["rfc"]);
                txtNombre_em.Text = Convert.ToString(resultado["nombre_em"]);
                txtTipo_pago.Text = Convert.ToString(resultado["tipo_pago"]);
                txtProyecto.Text = Convert.ToString(resultado["proyecto"]);
                txtF_primdesc.Text = Convert.ToString(resultado["f_primdesc"]).Replace(" 12:00:00 a. m.", ""); ;
                txtPlazo.Text = Convert.ToString(resultado["plazo"]);
                txtImp_unit.Text = string.Format("{0:C}",resultado["imp_unit"]);
                txtImporte.Text = string.Format("{0:C}",resultado["importe"]);
                txtDireccion.Text = Convert.ToString(resultado["direccion"]);
                txtF_solicitud.Text = Convert.ToString(resultado["f_solicitud"]).Replace(" 12:00:00 a. m.", "");
                txtF_emischeq.Text = Convert.ToString(resultado["f_emischeq"]).Replace(" 12:00:00 a. m.", "");
                txtUbic_pagare.Text = Convert.ToString(resultado["ubic_pagare"]);

                txtTipo_mov1.Text = "AN";
                txtTipo_mov1_SelectedIndexChanged(txtTipo_mov1,null);

            }
            catch(Exception e) {
               globales.MessageBoxError(e.Message,"Error",globales.menuPrincipal);
            }
        }

        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
            //limpiarSolicitudesDependencias();
            //limpiaredoCuenta();
            //if (e.KeyChar == 8) {
            //    return;
            //}
            //frmFolios.ShowDialog();
            e.Handled = !globales.numerico(e.KeyChar);

        }

        private void limpiaredoCuenta()
        {
            txtFolio.Text = "";
            txtSecretaria.Text = "";
            txtSecretariaDescripcion.Text = "";
            txtRfc.Text = "";
            txtNombre_em.Text = "";
            txtProyecto.Text = "";
            txtTipo_pago.Text = "";
            txtF_primdesc.Text = "";
            txtPlazo.Text = "";
            txtImp_unit.Text = string.Format("{0:C}",0);
            txtImporte.Text = string.Format("{0:C}",0);
            txtDireccion.Text = "";
            txtF_solicitud.Text = "";
            txtF_emischeq.Text = "";
            txtUbic_pagare.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Parte de validaciones... favor de ingresarlas más tarde...
                if (string.IsNullOrWhiteSpace(txtFolio.Text)) {
                    globales.MessageBoxExclamation("Favor de seleccionar un folio del estado de cuenta", "Aviso", globales.menuPrincipal);
                    txtFolio.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTipo_mov1.Text)) {
                    globales.MessageBoxExclamation("Seleccionar un tipo de movimiento", "Aviso", globales.menuPrincipal);
                    txtTipo_mov1.Focus();
                    return;
                }

                string checarFecha = txtF_descuento1.Text.Replace("/","");
                if (string.IsNullOrWhiteSpace(checarFecha)) {
                    globales.MessageBoxExclamation("Se debe ingresar fecha de aplicación", "Aviso", globales.menuPrincipal);
                    txtF_descuento1.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNumdesc1.Text)) {
                    globales.MessageBoxExclamation("Se debe ingresar numero de descuento", "Aviso", globales.menuPrincipal);
                    txtNumdesc1.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtNumdesc1.Text))
                {
                    globales.MessageBoxExclamation("Se debe ingresar el total de descuentos", "Aviso", globales.menuPrincipal);
                    txtTotdesc1.Focus();
                    return;
                }


                string ubicacionPagare = txtUbic_pagare.Text;
                string sFecha = string.Empty;
                try
                {
                    sFecha = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtF_primdesc.Text));
                }
                catch {
                    globales.MessageBoxError("Error en fecha, Favor de verificar", "Aviso", globales.menuPrincipal);
                    txtF_descuento1.Focus();
                    return;
                }



                string query = string.Format("update datos.p_edocta set secretaria = '{8}',descripcion = '{9}', ubic_pagare = '{0}',f_primdesc='{2}',plazo = {3}, imp_unit={4},importe={5},f_emischeq = '{6}',tipo_pago='{7}' where folio = {1}", ubicacionPagare, txtFolio.Text, sFecha, txtPlazo.Text, double.Parse(txtImp_unit.Text, NumberStyles.Currency), double.Parse(txtImporte.Text, NumberStyles.Currency), string.Format("{0:yyyy-MM-dd}",DateTime.Parse(txtF_emischeq.Text)),txtTipo_pago.Text,txtSecretaria.Text,txtSecretariaDescripcion.Text);
                if (globales.consulta(query, true))
                {

                    query = string.Format("select * from datos.solicitud_dependencias where folio = {0} and tipo_mov = '{1}' and sec = '{2}' and t_prestamo = 'Q'", txtFolio.Text,txtTipo_mov1.Text,txtSec1.Text);
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    DateTime fecha = DateTime.Parse(txtF_descuento1.Text);
                    string fechaDescuento = string.Format("{0}-{1}-{2}", fecha.Year, fecha.Month, fecha.Day);
                    if (resultado.Count > 0)
                    {
                        
                        query = string.Format("update datos.solicitud_dependencias set tipo_mov='{0}',sec='{1}',tipo_rel='{2}',f_descuento='{3}',numdesc={4},totdesc={5},imp_unit={6},rfc='{7}',nombre_em='{8}',proyecto='{9}', folio_ = {13}, numdesc_={14},totdesc_={15},imp_unit_={16} where folio = {10} and t_prestamo = 'Q' AND sec = '{11}' and tipo_mov = '{12}'",
                                         txtTipo_mov1.Text, txtSec1.Text, txtTipo_rel1.Text, fechaDescuento, txtNumdesc1.Text, txtTotdesc1.Text, double.Parse(txtImp_unit1.Text, NumberStyles.Currency), txtRfc1.Text, txtNombre_em1.Text, txtProyecto1.Text, txtFolio.Text,txtSec1.Text,txtTipo_mov1.Text,string.IsNullOrWhiteSpace(txtFolio_.Text)?"null":txtFolio_.Text,globales.convertNull(txtNumdesc_.Text),globales.convertNull(txtTotdesc_.Text),globales.convertDouble(txtImp_unit_.Text));
                        globales.consulta(query,true);
                        globales.MessageBoxSuccess("Registro actualizado correctamente","Aviso",globales.menuPrincipal);
                        return;
                    }

                    double p1 = string.IsNullOrWhiteSpace(txtImp_unit1.Text) ? 0 : double.Parse(txtImp_unit1.Text,NumberStyles.Currency);
                    
                    query = string.Format("insert into datos.solicitud_dependencias(folio,tipo_mov,sec,tipo_rel,f_descuento,numdesc,totdesc,imp_unit,rfc,nombre_em,proyecto,t_prestamo) values({0},'{1}','{2}','{3}','{4}',{5},{6},{7},'{8}','{9}','{10}','Q')",
                                        txtFolio.Text, txtTipo_mov1.Text, txtSec1.Text, txtTipo_rel1.Text, fechaDescuento, txtNumdesc1.Text, txtTotdesc1.Text, p1, txtRfc1.Text, txtNombre_em1.Text, txtProyecto1.Text);
                    globales.consulta(query, true);
                    globales.MessageBoxSuccess("Registro guardado exitosamente!!","Registro guardado",globales.menuPrincipal);
                    //limpiaredoCuenta();
                    //limpiarSolicitudesDependencias();
                }
                else {
                    globales.MessageBoxError("Error al modificar altas y cambios, favor de contactar a los de sistemas..","Error de registro",globales.menuPrincipal);
                }
            }
            catch {
                globales.MessageBoxError("Error, contactar a los de sistemas","Error en sistemas",globales.menuPrincipal);
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtTipo_mov1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8) {
                if (string.IsNullOrWhiteSpace(txtFolio.Text)) return;
                string query = string.Format("select count(folio) as cantidad from datos.solicitud_dependencias where folio = {0} and t_prestamo = 'Q'", txtFolio.Text);
                var aux = globales.consulta(query);
                secuencia =(int) aux[0]["cantidad"];
                secuencia++;
                limpiarSolicitudesDependencias();
                txtSec1.Text = Convert.ToString(secuencia);
            }
        }

        private void limpiarSolicitudesDependencias() {
            txtTipo_mov1.Text = "";
            txtSec1.Text = "";
            txtF_descuento1.Text = "";
            txtProyecto1.Text = "";
            txtRfc1.Text = "";
            txtNombre_em1.Text = "";
            txtTipo_rel1.Text = "";
            txtNumdesc1.Text = "";
            txtTotdesc1.Text = "";
            txtImp_unit1.Text = string.Format("{0:C}",0);

            txtFolio_.Text = "";
            txtNumdesc_.Text = "";
            txtTotdesc_.Text = "";
            txtImp_unit_.Text = "";
        }

        private void panel6_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void frmAltasCambios_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                btnFolio_Click(null,null);

        }

        private void btnFolio_Click(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);

            //frmFolios = new frmCatalogoP_quirog();
            //frmFolios.tablaConsultar = "p_edocta";
            //frmFolios.enviar2 = rellenarCamposFolio;
            //frmFolios.ShowDialog();
            //ActiveControl = txtSecretaria;
        }

        private void txtFolio_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFolio.Text) || !boolFolio) return;


            string query = string.Format("select * from datos.p_edocta where folio = {0}", txtFolio.Text);
            List<Dictionary<string, object>> listita = globales.consulta(query);
            if (listita.Count > 0)
            {
                rellenarCamposFolio(listita[0]);
            }
            else
            {

                globales.MessageBoxExclamation("No se encuentra el FOLIO en el cátalogo", "Aviso", globales.menuPrincipal);
                limpiarSolicitudesDependencias();
                limpiaredoCuenta();
                this.ActiveControl = txtFolio;
                
            }
        }

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {
            this.boolFolio = true;
        }

        private void txtTipo_mov1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtF_descuento1.Focus();  
        }

        private void txtTipo_mov1_DragLeave(object sender, EventArgs e)
        {
           
        }

        private void txtTipo_mov1_Leave(object sender, EventArgs e)
        {
            string query = $"select * from datos.solicitud_dependencias where folio = {txtFolio.Text} and tipo_mov = '{txtTipo_mov1.Text}' and t_prestamo = 'Q' order by sec desc";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count != 0)
            {
                if (Convert.ToInt32(resultado[0]["sec"])+1 == Convert.ToInt32(txtSec1.Text)) {
                    return;
                }
                
                
                    int secuencia = Convert.ToInt32(resultado[0]["sec"]);
                    txtSec1.Text = Convert.ToString(secuencia);
            }
            else {
                txtSec1.Text = "1";
            }
        }

        private void txtSecretaria_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
        }

        private void txtPlazo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);
        }

        private void txtImp_unit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);
        }

        private void txtImp_unit_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtImp_unit.Text) || ((txtImp_unit.Text.Contains("$") || txtImp_unit.Text.Contains(".")) && (txtImp_unit.Text.Length == 1 || txtImp_unit.Text.Length == 2)))
                txtImp_unit.Text = string.Format("{0:C}", 0);
            else
            {
                txtImp_unit.Text = string.Format("{0:C}", double.Parse(txtImp_unit.Text, NumberStyles.Currency));

            }
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtImporte.Text) || ((txtImporte.Text.Contains("$") || txtImporte.Text.Contains(".")) && (txtImporte.Text.Length == 1 || txtImporte.Text.Length == 2)))
                txtImporte.Text = string.Format("{0:C}", 0);
            else
            {
                txtImporte.Text = string.Format("{0:C}", double.Parse(txtImporte.Text, NumberStyles.Currency));

            }
        }

        private void txtImp_unit1_Leave(object sender, EventArgs e)
        {
            txtImp_unit1.Text = string.Format("{0:C}", globales.convertDouble(txtImp_unit1.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
                this.Close();
        }

        private void txtTipo_mov1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            string query = $"select * from datos.solicitud_dependencias where folio = {txtFolio.Text} and t_prestamo = 'Q' and tipo_mov = '{combo.Text}' order by sec desc";
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            

            if (resultado.Count != 0)
            {
                Dictionary<string, object> obj = resultado[0];
                txtSec1.Text = Convert.ToString(obj["sec"]);
                txtF_descuento1.Text = string.Format("{0:d}", obj["f_descuento"]);
                txtProyecto1.Text = Convert.ToString(obj["proyecto"]);
                txtRfc1.Text = Convert.ToString(obj["rfc"]); ;
                txtNombre_em1.Text = Convert.ToString(obj["nombre_em"]);
                txtTipo_rel1.Text = Convert.ToString(obj["tipo_rel"]);


                txtNumdesc1.Text = Convert.ToString(obj["numdesc"]);
                txtTotdesc1.Text = Convert.ToString(obj["totdesc"]);

                txtImp_unit1.Text = Convert.ToString(obj["imp_unit"]);
                txtFolio_.Text = Convert.ToString(obj["folio_"]); ;
                txtNumdesc_.Text = Convert.ToString(obj["numdesc_"]);
                txtTotdesc_.Text = Convert.ToString(obj["totdesc_"]);
                txtImp_unit_.Text = Convert.ToString(obj["imp_unit_"]);
                txtSec1.Items.Clear();
                foreach (Dictionary<string,object> item in resultado) {
                    string secuencia = Convert.ToString(item["sec"]);
                    txtSec1.Items.Add(secuencia);
                }

            }
            else {
                txtSec1.Text = "";
                txtF_descuento1.Text = "";
                txtProyecto1.Text = "";
                txtRfc1.Text = "";
                txtNombre_em1.Text = "";
                txtTipo_rel1.Text = "";


                txtNumdesc1.Text = "";

                txtImp_unit1.Text = "";
                txtFolio_.Text = "";
                txtNumdesc_.Text = "";
                txtTotdesc_.Text = "";
                txtImp_unit_.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtSec1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

           


            if (string.IsNullOrWhiteSpace(txtSec1.Text)) {
                globales.MessageBoxExclamation("No existe número de secuencia", "Aviso", globales.menuPrincipal);
                return;
            }

            string query = $"select * from datos.solicitud_dependencias where folio = {txtFolio.Text} and t_prestamo = 'Q' and tipo_mov = '{txtTipo_mov1.Text}' and sec = '{txtSec1.Text}' order by sec desc";
            List<Dictionary<string, object>> resultado = globales.consulta(query);



            if (resultado.Count != 0)
            {
                Dictionary<string, object> obj = resultado[0];
                txtSec1.Text = Convert.ToString(obj["sec"]);
                txtF_descuento1.Text = string.Format("{0:d}", obj["f_descuento"]);
                txtProyecto1.Text = Convert.ToString(obj["proyecto"]);
                txtRfc1.Text = Convert.ToString(obj["rfc"]); ;
                txtNombre_em1.Text = Convert.ToString(obj["nombre_em"]);
                txtTipo_rel1.Text = Convert.ToString(obj["tipo_rel"]);


                txtNumdesc1.Text = Convert.ToString(obj["numdesc"]);
                txtTotdesc1.Text = Convert.ToString(obj["totdesc"]);

                txtImp_unit1.Text = Convert.ToString(obj["imp_unit"]);
                txtFolio_.Text = Convert.ToString(obj["folio_"]); ;
                txtNumdesc_.Text = Convert.ToString(obj["numdesc_"]);
                txtTotdesc_.Text = Convert.ToString(obj["totdesc_"]);
                txtImp_unit_.Text = Convert.ToString(obj["imp_unit_"]);
                
            }
            else
            {
                txtSec1.Text = "";
                txtF_descuento1.Text = "";
                txtProyecto1.Text = "";
                txtRfc1.Text = "";
                txtNombre_em1.Text = "";
                txtTipo_rel1.Text = "";


                txtNumdesc1.Text = "";

                txtImp_unit1.Text = "";
                txtFolio_.Text = "";
                txtNumdesc_.Text = "";
                txtTotdesc_.Text = "";
                txtImp_unit_.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas generar el siguiente número de secuencia?", "Aviso", globales.menuPrincipal);
            if (p == DialogResult.Yes)
            {

                string query = $"select * from datos.solicitud_dependencias where folio = {txtFolio.Text} and tipo_mov = '{txtTipo_mov1.Text}' and t_prestamo = 'Q' order by sec desc";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count != 0)
                {
                    int secuencia = Convert.ToInt32(resultado[0]["sec"]);
                    txtSec1.Text = Convert.ToString(secuencia + 1);
                }
                else
                {
                    txtSec1.Text = "1";
                }

                txtF_descuento1.Focus();
                txtF_descuento1.Text = "";
                txtProyecto1.Text = "";
                txtRfc1.Text = "";
                txtNombre_em1.Text = "";
                txtTipo_rel1.Text = "";


                txtNumdesc1.Text = "";

                txtImp_unit1.Text = "";
                txtFolio_.Text = "";
                txtNumdesc_.Text = "";
                txtTotdesc_.Text = "";
                txtImp_unit_.Text = "";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Desea cancelar el estado de cuenta?","Cancelación de Edo. Cuenta",globales.menuPrincipal);
            if (p == DialogResult.No) return;

            string query = "select count(folio) as cantidad from datos.descuentos where t_prestamo = 'Q' and folio = "+this.txtFolio.Text;
            List<Dictionary<string,object>> result = globales.consulta(query);
            int cantidad = Convert.ToInt32(result[0]["cantidad"]);
            if ( cantidad > 0) {
                globales.MessageBoxExclamation("Imposible de eliminar teniendo en la cuenta descuentos","Aviso",globales.menuPrincipal);
                return;
            }

             query = $"update datos.p_edocta set f_emischeq = null,f_primdesc = null, importe = 0 where folio = {txtFolio.Text}; ";
            query += $"update datos.solicitud_dependencias set f_descuento = null,tipo_rel = '' where folio = {txtFolio.Text};";
            if (globales.consulta(query,true)) {
                globales.MessageBoxSuccess("Estado de Cuenta Cancelado con Exito","Aviso",globales.menuPrincipal);
                limpiaredoCuenta();
                limpiarSolicitudesDependencias();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion($"¿Deseas eliminar la solicitud {txtTipo_mov1.Text} con secuencia {txtSec1.Text}?","Aviso",globales.menuPrincipal);
            if (p == DialogResult.No) return;

            string query = $"delete from datos.solicitud_dependencias where folio = {txtFolio.Text} and tipo_mov = '{txtTipo_mov1.Text}' and sec = '{txtSec1.Text}' and t_prestamo = 'Q'";
            if (globales.consulta(query,true)) {
                globales.MessageBoxSuccess("Solicitud a dependencia eliminado correctamente","Aviso",globales.menuPrincipal);
                txtTipo_mov1.SelectedIndex = 1;
                
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            modalQuirografario<p_edocta> quirog = new modalQuirografario<p_edocta>();
            quirog.enviar = rellenarCamposFolio;
            globales.showModal(quirog);
            ActiveControl = txtSecretaria;
            Cursor = Cursors.Default;



            //frmFolios = new frmCatalogoP_quirog();
            //frmFolios.tablaConsultar = "p_edocta";
            //frmFolios.enviar2 = rellenarCamposFolio;
            //frmFolios.ShowDialog();
            //ActiveControl = txtSecretaria;txtProyecto
        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void txtSecretaria_TextChanged(object sender, EventArgs e)
        {
            string secretaria = txtSecretaria.Text;


            string query = $"SELECT descripcion FROM catalogos.cuentas where proy='{secretaria}'";
            List<Dictionary<string, object>> res = globales.consulta(query);
            string descripcion = (res.Count != 0) ? Convert.ToString(res[0]["descripcion"]) : "";

            txtSecretariaDescripcion.Text = descripcion;

        }
    }
}
