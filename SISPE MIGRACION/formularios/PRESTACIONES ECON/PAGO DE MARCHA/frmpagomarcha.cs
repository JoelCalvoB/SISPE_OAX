using SISPE_MIGRACION.codigo.repositorios.datos;
using SISPE_MIGRACION.formularios.CATÁLOGOS;
using SISPE_MIGRACION.formularios.CATÁLOGOS.modales_catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.PAGO_DE_MARCHA
{
    public partial class frmpagomarcha : Form
    {


        private bool esInsetar;

        public frmpagomarcha()
        {
            InitializeComponent();
            txtdescuento.Leave += new EventHandler(salirDescuento);

        }

        private void salirDescuento(object sender, EventArgs e)
        {
            txtdescuento.Text = string.Format("{0:C}",globales.convertDouble(txtdescuento.Text));
        }

        private void frmpagomarcha_Load(object sender, EventArgs e)
        {


        }




        private void limpiaTodosCampos()
        {
            txtdependencia.Text = "";
            txtdescuento.Text = "";
            fecha.Text = "";
            txtfecobro.Text = "";
            txtfolio.Text = "";
            txtimporteletra.Text = "";
            txtliquido.Text = "";
            txtmeses.Text = "";
            txtmonto.Text = "";
            txtmuerto.Text = "";
            txtnombre.Text = "";
            txtnumcheq.Text = "";
            txtparentesco.Text = "";
            txtrfc.Text = "";
            txtsueldo.Text = "";
            txtsuertudo.Text = "";
            txtvida.Text = "";
            txtconcepto.Text = "";
            txtfolio.Text = "AUTOGENERADO";


        }

        public void llenacampos(Dictionary<string, object> campo)
        {


            this.txtfolio.Text = Convert.ToString(campo["folio"]);
            this.txtrfc.Text = Convert.ToString(campo["rfc"]);
            this.txtsueldo.Text = Convert.ToString(campo["sueldo_base"]);
            this.txtvida.Text = Convert.ToString(campo["descripcion"]);
            this.fecha.Text = Convert.ToString(campo["f_recibo"]).Replace("12:00:00 a. m.", "");
            this.txtnumcheq.Text = Convert.ToString(campo["n_cheque"]);
            this.txtnombre.Text = Convert.ToString(campo["nombre_em"]);
            this.txtdependencia.Text = Convert.ToString(campo["depe"]);
            this.txtmuerto.Text = Convert.ToString(campo["f_acaec"]).Replace("12:00:00 a. m.", "");
            this.txtmeses.Text = Convert.ToString(campo["meses"]);
            this.txtsuertudo.Text = Convert.ToString(campo["pers_cobro"]);
            this.txtparentesco.Text = Convert.ToString(campo["parentesco"]);
            this.txtfecobro.Text = Convert.ToString(campo["f_cobro"]).Replace("12:00:00 a. m.", "");
            this.txtmonto.Text = Convert.ToString(campo["monto"]);
            this.txtdescuento.Text = Convert.ToString(campo["descuentos"]);
            this.txtconcepto.Text = Convert.ToString(campo["concepto_desc"]);
            this.txtliquido.Text = Convert.ToString(campo["liquido"]);
            this.txtimporteletra.Text = Convert.ToString(campo["monto"]);



            string numero;
            numero = txtimporteletra.Text;
            string qry = "select * from datos.numletra ('{0}')";
            string pasa = string.Format(qry, numero);
            List<Dictionary<string, object>> resu = globales.consulta(pasa);
            string num = Convert.ToString(resu[0]["numletra"]);

            this.txtimporteletra.Text = num;


        }




  
        private void btnsalir_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmpagomarcha_KeyDown(object sender, KeyEventArgs e)
        {
           
        }


        private void llenacamposRfc(Dictionary<string, object> datos)
        {

            txtrfc.Text = Convert.ToString(datos["rfc"]);
            txtnombre.Text = Convert.ToString(datos["nombre_em"]);
            fecha.Focus();

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
            p_quirog.enviar2 = imprimirReporte;
            p_quirog.tablaConsultar = "p_marcha";
            p_quirog.ShowDialog();
        }
        private void imprimirReporte(Dictionary<string, object> resultado)
        {
            DateTime fecha = DateTime.Parse(Convert.ToString(resultado["f_recibo"]));
            string solofecha = string.Format("{0:dd/MM/yy}", fecha);
            string fechaletra = "select datos.fechaletra ('{0}')";
            string paso = string.Format(fechaletra, solofecha);
            List<Dictionary<string, object>> list = globales.consulta(paso);
            string obtenerfecha = Convert.ToString(list[0]["fechaletra"]);


            string logogobierno = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[][] parametros = new object[2][];

            object[] cabeceras = { "cantidad", "concepto_desc", "imporletra", "nombre", "rfc", "tipo", "meses", "sueldo", "menos", "liquido", "facaec", "parentesco", "fechaletra", "benefic" ,"rutalogo"};
            object[] valores = { globales.checarDecimales(resultado["monto"]), Convert.ToString(resultado["concepto_desc"]), "(" + globales.convertirNumerosLetras(Convert.ToString(resultado["monto"]), true) + ")", Convert.ToString(resultado["nombre_em"]), Convert.ToString(resultado["rfc"]), Convert.ToString(resultado["descripcion"]), Convert.ToString(resultado["meses"]), globales.checarDecimales(resultado["sueldo_base"]), globales.checarDecimales(resultado["descuentos"]), globales.checarDecimales(resultado["liquido"]), Convert.ToString(resultado["f_acaec"]).Replace("12:00:00 a. m.", ""), Convert.ToString(resultado["parentesco"]), obtenerfecha, Convert.ToString(resultado["pers_cobro"]), logogobierno };

            parametros[0] = cabeceras;
            parametros[1] = valores;

            globales.reportes("ReporteMarcha", "p_marcha", new object[] { }, "", false, parametros);
        }

        private void txtdescuento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {

                    double desc = globales.convertDouble(txtdescuento.Text);
                    double liquido = globales.convertDouble(txtmonto.Text) - desc;
                    this.txtdescuento.Text = string.Format("{0:C}", (desc)); ;


                    string pasoliq = string.Format("{0:C}", (liquido));
                    this.txtliquido.Text = pasoliq;

                    double conliqui = globales.convertDouble(txtliquido.Text);

                    double nume = globales.convertDouble(txtliquido.Text); ;
                    string pasa = "select * from datos.numletra('{0}')";
                    string n = string.Format(pasa, nume);
                    List<Dictionary<string, object>> res = globales.consulta(n);
                    string liquiletra = Convert.ToString(res[0]["numletra"]);
                    this.txtimporteletra.Text = liquiletra;


                }
                catch
                {
                    globales.MessageBoxExclamation("Verifique el formato de entrada", "Aviso", globales.menuPrincipal);
                }
            }


        }



        private void txtmeses_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtmeses_TextChanged(object sender, EventArgs e)
        {

        }


        private void txtdependencia_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void txtsueldo_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtsueldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtmonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
     if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan 
                e.Handled = true;
            }
        }

        private void txtdescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else
     if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan 
                e.Handled = true;
            }
        }

        private void txtliquido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else
     if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan 
                e.Handled = true;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            globales.showModal(new frmrepormarcha());

        }

        private void frmpagomarcha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtvida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void btnFolio_Click(object sender, EventArgs e)
        {
            modalQuirografario<p_marcha> modal = new modalQuirografario<p_marcha>();
            modal.enviar = rellenar;
            globales.showModal(modal);
        }

        public void rellenar(Dictionary<string,object> datos) {
            limpiaTodosCampos();
            dbaseORM orm = new dbaseORM();
            p_marcha obj = orm.getObject<p_marcha>(datos);

            txtfolio.Text = Convert.ToString(obj.folio);
            fecha.Text = globales.parseDateTime(obj.f_recibo);
            txtnumcheq.Text = obj.n_cheque;
            txtrfc.Text = obj.rfc;
            txtnombre.Text = obj.nombre_em;
            txtsueldo.Text = string.Format("{0:c}", obj.sueldo_base);
            txtdependencia.Text = obj.depe;
            txtvida.Text = obj.descripcion;
            txtmuerto.Text = globales.parseDateTime(obj.f_acaec);
            txtmeses.Text = Convert.ToString(obj.meses);
            txtsuertudo.Text = obj.pers_cobro;
            txtparentesco.Text = obj.parentesco;
            txtfecobro.Text = globales.parseDateTime(obj.f_cobro);
            txtmonto.Text = string.Format("{0:C}",obj.monto);
            txtdescuento.Text = string.Format("{0:C}",obj.descuentos);
            txtconcepto.Text = obj.concepto_desc;
            txtliquido.Text = string.Format("{0:c}",obj.liquido);
            txtimporteletra.Text = obj.liq_letra;

            ActiveControl = fecha;
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnModifica.Enabled = false;
            btnModifica.Visible = false;
            btnGuardar.Enabled = true;
            btnGuardar.Visible = true;
            btnsalir.Text = "CANCELAR";

            txtfolio.Text = "AUTOGENERADO";
            txtfolio.Enabled = false;
            btnFolio.Enabled = false;

            this.esInsetar = true;

            habilitarBotonesBasicos(true);
        }

        private void habilitarBotonesBasicos(bool v)
        {
            fecha.Enabled = v;
            txtnumcheq.Enabled = v;
            txtrfc.Enabled = v;
            btnRfc.Enabled = v;
            txtsueldo.Enabled = v;
            txtdependencia.Enabled = v;
            txtmuerto.Enabled = v;
            txtmeses.Enabled = v;
            txtsuertudo.Enabled = v;
            txtparentesco.Enabled = v;
            txtfecobro.Enabled = v;
            txtmonto.Enabled = v;
            txtdescuento.Enabled = v;
            txtconcepto.Enabled = v;
        }

        private void btnRfc_Click(object sender, EventArgs e)
        {
            modalEmpleados empleados = new modalEmpleados();
            empleados.enviar = llenacamposRfc;
            globales.showModal(empleados);
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {


            if (!this.esInsetar)
            {
                DialogResult resultado = globales.MessageBoxQuestion("¿Deseas guardar el registro?", "Aviso", globales.menuPrincipal);
                if (resultado == DialogResult.No) return;
                if (string.IsNullOrWhiteSpace(txtfolio.Text))
                {
                    globales.MessageBoxExclamation("Debes elegir el folio", "Aviso", globales.menuPrincipal);
                    return;
                }
            }
            else
            {
                DialogResult resultado = globales.MessageBoxQuestion("¿Deseas actualizar el registro?", "Aviso", globales.menuPrincipal);
                if (resultado == DialogResult.No) return;
            }

            if (string.IsNullOrWhiteSpace(txtrfc.Text))
            {
                globales.MessageBoxExclamation("Debes ingresar el RFC", "Aviso", globales.menuPrincipal);
                return;
            }




            if (this.esInsetar)
                insertar();
            else
                modificar();
            limpiaTodosCampos();
            btnModifica.Visible = true;
            btnModifica.Enabled = true;
            btnNuevo.Enabled = true;
            btnGuardar.Visible = false;
            btnGuardar.Enabled = false;
            btnFolio.Enabled = false;
            btnsalir.Text = "SALIR";
            txtfolio.Enabled = false;
            btnFolio.Enabled = false;
            habilitarBotonesBasicos(false);
            limpiaTodosCampos();
        }

        private void modificar()
        {
            p_marcha obj = rellenarPagoMarcha();
            dbaseORM orm = new dbaseORM();

            obj.folio = globales.convertInt(txtfolio.Text);

            bool actualizado = orm.update<p_marcha>(obj);
            if (actualizado)
                globales.MessageBoxSuccess("Registro actualizado correctamente", "Aviso", globales.menuPrincipal);
        }

        private void insertar()
        {
            p_marcha obj = rellenarPagoMarcha();
            dbaseORM orm = new dbaseORM();

            List<Dictionary<string, object>> resultado = orm.query("select COALESCE(max(folio)+1,1) as maximo from datos.p_marcha");
            string maximo = string.Empty;
            if (resultado.Count != 0)
                maximo = Convert.ToString(resultado[0]["maximo"]);

            obj.folio = globales.convertInt(maximo);

            bool insertado = orm.insert<p_marcha>(obj);
            if (insertado) 
                globales.MessageBoxSuccess("Registro guardado correctamente","Aviso",globales.menuPrincipal);            


        }

        private p_marcha rellenarPagoMarcha()
        {
            p_marcha p = new p_marcha();
            p.f_recibo = globales.convertDatetime(fecha.Text);
            p.n_cheque = txtnumcheq.Text;
            p.rfc = txtrfc.Text;
            p.nombre_em = txtnombre.Text;
            p.sueldo_base = globales.convertDouble(txtsueldo.Text);
            p.depe = txtdependencia.Text;
            p.descripcion = txtvida.Text;
            p.f_acaec = globales.convertDatetime(txtmuerto.Text);
            p.meses = globales.convertInt(txtmeses.Text);
            p.pers_cobro = txtsuertudo.Text;
            p.parentesco = txtparentesco.Text;
            p.f_cobro = globales.convertDatetime(txtfecobro.Text);
            p.monto = globales.convertDouble(txtmonto.Text);
            p.descuentos = globales.convertDouble(txtdescuento.Text);
            p.concepto_desc = txtconcepto.Text;
            p.liquido = globales.convertDouble(txtliquido.Text);
            p.liq_letra = txtimporteletra.Text;
            return p;

        }

        private void txtrfc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                modalEmpleados c_aporta = new modalEmpleados();
                c_aporta.enviar = llenacamposRfc;
                c_aporta.ShowDialog();

            }
        }

        private void txtsueldo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtsueldo.Text))
                {
                    double cantidad = globales.convertDouble(txtsueldo.Text);
                    string pesos = string.Format("{0:C}", (cantidad));
                    this.txtsueldo.Text = pesos;
                    //  txtdependencia.Focus();

                }
            }
            catch
            {
                globales.MessageBoxExclamation("Verifique el formato de entrada", "Aviso", globales.menuPrincipal);
            }
        }

        private void txtdependencia_Leave(object sender, EventArgs e)
        {
            string depe = txtdependencia.Text;
            if (depe == "J")
            {
                txtvida.Text = "JUBILADO";
            }

            if (depe == "P")
            {
                txtvida.Text = "PENSIONADO";
            }
            ActiveControl = txtmuerto;
        }

        private void txtmeses_Leave(object sender, EventArgs e)
        {


            double importe = (globales.convertDouble(txtsueldo.Text) * globales.convertDouble(txtmeses.Text));
            string pasomonto = string.Format("{0:C}", (importe));
            txtmonto.Text = pasomonto;
        }

        private void txtliquido_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtdescuento_ImeModeChanged(object sender, EventArgs e)
        {

        }

        private void txtsueldo_KeyDown(object sender, KeyEventArgs e)
        {



        }

        private void txtmonto_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtconcepto_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtliquido_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            this.esInsetar = false;


            btnNuevo.Enabled = false;
            btnModifica.Enabled = false;
            btnModifica.Visible = false;
            btnGuardar.Enabled = true;
            btnGuardar.Visible = true;
            btnsalir.Text = "CANCELAR";

            txtfolio.Text = "";
            txtfolio.Enabled = true;
            btnFolio.Enabled = true;

            habilitarBotonesBasicos(true);

            ActiveControl = txtfolio;
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            if (btnsalir.Text == "CANCELAR")
            {
                DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas cancelar la operación?", "Aviso", globales.menuPrincipal);
                if (dialogo == DialogResult.No) return;

                habilitarBotonesBasicos(false);
                limpiaTodosCampos();
                txtfolio.Text = "AUTOGENERADo";
                btnsalir.Text = "SALIR";

                btnFolio.Enabled = false;
                txtfolio.Enabled = false;

                btnNuevo.Enabled = true;
                btnNuevo.Visible = true;
                btnModifica.Enabled = true;
                btnModifica.Visible = true;
                btnGuardar.Enabled = false;
                btnGuardar.Visible = false;
            }
            else {
                this.Close();
            }
        }

        private void txtfolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End) {
                SendKeys.Send("{TAB}");
            }
            if (e.KeyCode == Keys.F1) {
                modalEmpleados empleados = new modalEmpleados();
                empleados.enviar = llenacamposRfc;
                globales.showModal(empleados);
            }
        }

        private void txtfolio_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtfolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void txtfolio_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtfolio.Text))
            {
                string query = "select * from datos.p_marcha where folio = " + txtfolio.Text;
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count != 0)
                {
                    Dictionary<string, object> obj = resultado[0];
                    rellenar(obj);
                }
                else
                {
                    globales.MessageBoxExclamation("No existe información de funeral", "Aviso", globales.menuPrincipal);
                    txtfolio.Text = string.Empty;
                    ActiveControl = txtfolio;
                }
            }
        }

        private void txtliquido_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtdescuento_Leave(object sender, EventArgs e)
        {

            txtmonto.Text = string.Format("{0:C}", globales.convertDouble(txtsueldo.Text) * Convert.ToDouble(txtmeses.Text));
            double liquido = globales.convertDouble(txtmonto.Text) - globales.convertDouble(txtdescuento.Text);
            txtliquido.Text = string.Format("{0:C}", liquido);
            txtimporteletra.Text = globales.convertirNumerosLetras(txtmonto.Text, true);
        }

        private void txtconcepto_Leave(object sender, EventArgs e)
        {
            ActiveControl = btnGuardar;
        }
    }
}