using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.codigo.baseDatos.repositorios;
using SISPE_MIGRACION.codigo.herramientas.forms;
using SISPE_MIGRACION.formularios.CATÁLOGOS;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.REPORTE;
using SISPE_MIGRACION.reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ
{
    delegate void enviarDatos(Dictionary<string, object> datos, bool externo = false);
    delegate void enviarDatos2(Dictionary<string, object> quirografario, List<Dictionary<string, object>> avales, bool externo = false);
    delegate void cambiarDatos(string texto);
    public partial class frmAltas : Form
    {
        private Dictionary<string, string> modalidades;
        private frmEmpleados frmEmpleados;
        private frmdependencias frmdependencias;
        private p_quirog quiro;
        private double Ant_A = 0;
        private double Ant_M = 0;
        private double Ant_Q = 0;
        private double meses_corres = 0;
        private int plazo = 0;
        private string tipo_pago = string.Empty;
        private Dictionary<string, object> auxiliar;
        private string fechaSolicitud = string.Empty;
        private double t_interes = 0;
        private string aceptado = string.Empty;
        private double Secuen = 0.00;
        private string carta = string.Empty;
        private string v_fecha = string.Empty;
        private string b_fecha = string.Empty;
        private bool guardar = false;
        private string cve_categ = string.Empty;
        private bool boolDeducciones = false;
        private string txtFecha = string.Empty;

        //Parte de las deducciones como variables globales

        private double PER;
        private double DED;
        private string D;
        private double DED1;
        private double PER2;

        private double PER3 = 0.00;
        private double PER4 = 0.00;
        private double PER5 = 0.00;
        private double PER6 = 0.00;

        private double DED3 = 0.00;
        private double DED4 = 0.00;
        private double DED5 = 0.00;
        private double DED6 = 0.00;
        private double DED7 = 0.00;
        private double DED8 = 0.00;
        private double DED9 = 0.00;
        private double DED10 = 0.00;


        private double RES = 0.00;
        private double RES1 = 0.00;

        double TOPE = 0.00;
        double RESL = 0.00;
        double RES3 = 0.00;
        double Por = 0.00;
        double RESD = 0.00;
        double RES2 = 0.00;
        double IMP = 0.00;
        double IM = 0.00;

        private string f_primdesc = string.Empty;

        private bool boolRfc { get; set; }
        private bool boolSecretaria { get; set; }
        private bool boolRfc1 { get; set; }
        private bool boolRfc2 { get; set; }
        private bool boolFolio { get; set; }
        private List<Dictionary<string, object>> resultadoaux;
        private string tipo_rel;
        private string sexo;

        private bool tasaLaboral12 { get; set; }
        private bool boolAnt1 = false;
        private bool boolsueldo_base = false;

        public frmAltas()
        {
            InitializeComponent();
            modalidades = new Dictionary<string, string>();
            modalidades.Add("B", "BASE");
            modalidades.Add("C", "CONFIANZA");
            modalidades.Add("J", "JUBILADO");
            modalidades.Add("M", "MANDO MEDIO");
            modalidades.Add("P", "PENSIONADO");
            modalidades.Add("T", "PENSIONISTA");
            //=============================  Inicialziación de eventos para la tecla ENTER -> TAB ===================
            this.txtProyecto.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtSecretaria.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtAntQ.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtSueldoBase.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtTelefono.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtExtencion.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtDomicilio.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtNue.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtnap2.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtmeses_corres.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtOtros_desc.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtPorc.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtplazo.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtdesc.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtFolio.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtAdscripcion.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);

            this.txtRfc1.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtAnti1.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);


            this.txtrfc2.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtantg2.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtRfc.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtProyect1.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);
            this.txtproy2.PreviewKeyDown += new PreviewKeyDownEventHandler(cambiarTab);

            //==================================================================================================
        }

        private void cambiarTab(object sender, PreviewKeyDownEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                SendKeys.Send("{TAB}");//Cuando se presiona la tecla enter, este le manda señal a la tecla TAB para que active el evento de traspaso...
            }
           
        }

        private void ALTAS_Load(object sender, EventArgs e)
        {
            label48.Visible = false;
            txtTrl.Text = modalidades.First().Key;
            txtTrl.ForeColor = Color.White;
            lblmod.Text = modalidades.First().Value;

            frmdependencias = new frmdependencias();
            frmdependencias.enviar = rellenarCamposSecretarias;

            txtAntiguedad.Text = "A M Q";

            txtEmisionCheque.Text = "";
            fechaSolicitud = string.Format("{0:d}", DateTime.Now);

        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (btnsalir.Text.Contains("Cancel"))
            {
                DialogResult dialogo = globales.MessageBoxQuestion("¿Seguro que desea cancelar la operación?","Aviso", globales.menuPrincipal);
                if (dialogo == DialogResult.No) return;
                limpiarTodosCampos();

                btnsalir.Text = "Salir";
                txtFolio.Text = "AUTOGENERADO";
                btnNuevo.Enabled = true;
                btnNuevo.Visible = true;

                btnModifica.Enabled = true;
                btnModifica.Visible = true;

                btnGuardar.Enabled = false;
                btnGuardar.Visible = false;

                // btnCalculo.Enabled = false;
                btnImprimir.Enabled = true;
                txtEmisionCheque.Text = txtFecha;
            }
            else
            {
                Close();
            }
        }

        private void limpiarTodosCampos()
        {
            limpiarCamposRFC();
            desactivarControlesBasicos();
            limpiarSecretariaCampos();
            limpiarLiquidoCampos();
            txtAntQ.Text = "0";
            limpiarAvales();
            txtTelefono.Text = "";
            txtExtencion.Text = "";
            txtdesc.Text = "0.00";


            Ant_Q = 0;
            Ant_M = 0;
            Ant_A = 0;
            carta = " ";
            aceptado = " ";
            Secuen = 0;
        }

        private void limpiarAvales()
        {
            txtrfc2.Text = "";
            txtproy2.Text = "";
            txtnap2.Text = "";
            txtnombre2.Text = "";
            txtdomicilio2.Text = "";
            txtnue2.Text = "";
            txtantg2.Text = "";

            txtRfc1.Text = "";
            txtProyect1.Text = "";
            txtNap1.Text = "";
            txtNombre1.Text = "";
            txtdomicilio1.Text = "";
            txtNue1.Text = "";
            txtAnti1.Text = "";

            desactivarControl(txtRfc1);
            desactivarControl(txtrfc2);
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        public void rellenarCamposdeRFC(Dictionary<string, object> datos, bool externo = false)
        {
            txtSueldoBase.Text = "$0.00";
            string rfc = Convert.ToString(datos["rfc"]);

            //Verifica que el susuario que se ingreso con su RFC no se encuentre en la tabla de P_QUIROG.....
            //Si este se encuentra verifica que no se haya realizado algún movimiento en los último 120 días...

            if (guardar)
            {
                //   MessageBox.Show("Se verificara el RFC en FOLIOs anteriores....", "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cursor = Cursors.WaitCursor;
                string query = string.Format("select *, (select CAST(now() AS DATE) - CAST('120 days' AS INTERVAL)) as limite from datos.P_QUIROG " +
                                             "where F_solicitud >= (select CAST(now() AS DATE) - CAST('120 days' AS INTERVAL)) " +
                                             "and RFC like '%{0}%'", rfc);
                List<Dictionary<string, object>> resultado = baseDatos.consulta(query);
                Cursor = Cursors.Default;
                if (resultado.Count > 0)
                {
                    globales.MessageBoxExclamation("Este RFC ya fue utilizado en un préstamo después del " + string.Format("{0:d}", DateTime.Parse(Convert.ToString(resultado[0]["limite"]))), "Notificación", globales.menuPrincipal);
                }
            }

            this.txtRfc.Text = rfc;

            this.txtnombre_em.Text = Convert.ToString(datos["nombre_em"]);
            this.txtProyecto.Text = Convert.ToString(datos["proyecto"]).Length > 13 ? Convert.ToString(datos["proyecto"]).Substring(0,12): Convert.ToString(datos["proyecto"]);
            this.txtTelefono.Text = Convert.ToString(datos["telefono"]);
           if (string.IsNullOrWhiteSpace(txtSueldoBase.Text)  || txtSueldoBase.Text== "$0.00")
            {
                this.txtSueldoBase.Text = string.IsNullOrWhiteSpace(Convert.ToString(datos["sueldo_base"])) ? string.Format("{0:C}", 0) : string.Format("{0:C}", datos["sueldo_base"]);

            }
          

            this.txtNap.Text = Convert.ToString(datos["nap"]);
            if (string.IsNullOrWhiteSpace(txtSueldoBase.Text))
            {
                this.txtDomicilio.Text = Convert.ToString(datos["direccion"]);

            }
            this.txtNue.Text = Convert.ToString(datos["nue"]);
                this.sexo = Convert.ToString(datos["sexo"]);

            
            this.cve_categ = Convert.ToString(datos["cve_categ"]);
            this.tipo_rel = Convert.ToString(datos["tipo_rel"]);
            this.txtDomicilio.Text = Convert.ToString(datos["direccion"]);

        }

        public void rellenarCamposSecretarias(Dictionary<string, object> datos, bool externo = false)
        {

            if (!externo)
            {
                /*
                    Se agrega líneas para pedir los importes de percepciones
                    y reducciones del trabajador.
                */
                try
                {
                    int cate = Convert.ToInt32(this.cve_categ.Substring(2, 2));
                    if (cate > 16)
                    {
                        //  MessageBox.Show("La categoría es mayor a 16", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch
                {
                    //MessageBox.Show("El empleado no contiene categoría..Favor de verificar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                PER = (double.Parse(txtSueldoBase.Text, NumberStyles.Currency)) == 0 ? 0 : double.Parse(txtSueldoBase.Text, NumberStyles.Currency);
                DED = 0.00;
                D = "N";
                DED1 = DED;
                PER2 = PER;

                PER3 = 0.00;
                PER4 = 0.00;
                PER5 = 0.00;
                PER6 = 0.00;

                DED3 = 0.00;
                DED4 = 0.00;
                DED5 = 0.00;
                DED6 = 0.00;
                DED7 = 0.00;
                DED8 = 0.00;
                DED9 = 0.00;
                DED10 = 0.00;

                regresa:
                frmDescuentosDePensiones descuentos = new frmDescuentosDePensiones();
                if (txtSecretaria.Text == "J" || txtSecretaria.Text == "P" || txtSecretaria.Text == "T") {
                    descuentos.ROY2.Checked = true;
                }
                descuentos.cambiar = cambiarTxtSueldoBase;
                descuentos.PER.Text = string.Format("{0:C}", PER);
                descuentos.DED3.Text = Convert.ToString(DED3);
                descuentos.DED4.Text = Convert.ToString(DED4);
                descuentos.DED5.Text = Convert.ToString(DED5);
                descuentos.DED6.Text = Convert.ToString(DED6);
                
                descuentos.ShowDialog();
                if (!descuentos.esAceptar) return;
                if (string.IsNullOrWhiteSpace(txtSueldoBase.Text) || txtSueldoBase.Text == "0")
                {
                    globales.MessageBoxExclamation("Se debe ingresar el sueldo base, favor de ingresarlo para continuar", "Aviso", globales.menuPrincipal);
                    goto regresa;
                }

                PER = double.Parse(descuentos.PER.Text, NumberStyles.Currency);
                DED3 = double.Parse(descuentos.DED3.Text, NumberStyles.Currency);
                DED4 = Convert.ToDouble(descuentos.DED4.Text);
                DED5 = Convert.ToDouble(descuentos.DED5.Text);
                DED6 = Convert.ToDouble(descuentos.DED6.Text);

                if (descuentos.esAceptar)
                {
                    if (!descuentos.ROY2.Checked)
                    {
                        DED1 = DED1 / 2;
                        DED3 = DED3 / 2;
                        DED4 = DED4 / 2;
                        DED5 = DED5 / 2;
                        DED6 = DED6 / 2;
                        DED7 = DED7 / 2;
                        DED8 = DED8 / 2;
                        DED9 = DED9 / 2;
                        DED10 = DED10 / 2;

                        PER2 = PER2 / 2;
                        PER3 = PER3 / 2;
                        PER4 = PER4 / 2;
                        PER5 = PER5 / 2;
                        PER6 = PER6 / 2;
                    }

                    DED1 = DED1 + DED3 + DED4 + DED5 + DED6 + DED7 + DED8 + DED9 + DED10;
                    PER2 = PER;
                    D = "S";
                }
                else
                {
                    if (!descuentos.ROY2.Checked)
                    {
                        DED1 = DED1 + DED3 + DED4 + DED5 + DED6 + DED7 + DED8 + DED9 + DED10;
                        DED1 = DED1 / 2;
                    }

                    DED1 = DED1 + DED3 + DED4 + DED5 + DED6 + DED7 + DED8 + DED9 + DED10;
                    PER2 = PER;
                }

                //************************fin de percepciones y reducciones del trabajador******
            }

            int movimientos = Convert.ToInt32(this.txtmeses_corres.Text);
            //if (movimientos != 0)
            //{
            //    DialogResult d = MessageBox.Show("Los mov. serás recalculados", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //    if (d == DialogResult.No)
            //        return;
            //}   código comentado para posterior utilización (PATY POR SAMV)

            this.auxiliar = datos;
            string secretaria = string.Empty;
            string descripcionProyecto = string.Empty;
            if (datos.Count != 0)
            {
                secretaria = Convert.ToString(datos["proy"]);
                descripcionProyecto = Convert.ToString(datos["descripcion"]);
            }


            txtSecretaria.Text = string.IsNullOrWhiteSpace(secretaria) ? txtSecretaria.Text : secretaria;
            txtAdscripcion.Text = string.IsNullOrWhiteSpace(descripcionProyecto) ? txtAdscripcion.Text : descripcionProyecto;

            if (string.IsNullOrWhiteSpace(txtSueldoBase.Text)) return;

            if (secretaria != "J" && secretaria != "P" && secretaria != "T")
            {
                txtSueldo_m.Text = string.Format("{0:C}", double.Parse(txtSueldoBase.Text, NumberStyles.Currency) * 2);
                int Qtotales = Convert.ToInt32(txtAntQ.Text);
                int AA = Convert.ToInt32((Qtotales) / 24);
                int QAux = Qtotales - (AA * 24);
                int AM = Convert.ToInt32((QAux / 2));
                int AQ = QAux - (AM * 2);

                this.Ant_A = AA;
                this.Ant_M = AM;
                this.Ant_Q = AQ;

                this.tipo_pago = "Q";
                if (Convert.ToDouble(txtAntQ.Text) >= 12 && Convert.ToDouble(txtAntQ.Text) < 24)
                {
                    this.meses_corres = 3;
                    this.plazo = 24;
                }
                else if (Convert.ToDouble(txtAntQ.Text) >= 24 && Convert.ToDouble(txtAntQ.Text) < 120)
                {
                    this.meses_corres = 4;
                    this.plazo = 36;

                }
                else if (Convert.ToDouble(txtAntQ.Text) >= 120 && Convert.ToDouble(txtAntQ.Text) < 240)
                {
                    this.meses_corres = 5;
                    this.plazo = 36;
                }
                else if (Convert.ToDouble(txtAntQ.Text) >= 240)
                {
                    this.meses_corres = 6;
                    this.plazo = 48;

                }
                else
                {
                    this.meses_corres = 0;
                    this.plazo = 0;
                }

            }
            else
            {
                this.Ant_A = 0;
                this.Ant_M = 0;
                this.Ant_Q = 0;
                txtSueldo_m.Text = string.Format("{0:C}", txtSueldoBase.Text);
                this.tipo_pago = "M";
                if (secretaria == "J")
                {
                    this.meses_corres = 6;
                    this.plazo = 24;
                }
                else
                {
                    this.meses_corres = 3;
                    this.plazo = 12;
                }
            }

            txtAntiguedad.Text = string.Format("{0}A {1}M {2}Q", Convert.ToString(this.Ant_A), Convert.ToString(this.Ant_M), Convert.ToString(this.Ant_Q));
            txtmeses_corres.Text = Convert.ToString(this.meses_corres);
            txtplazo.Text = Convert.ToString(this.plazo);
            txtTipoPago.Text = this.tipo_pago;

        }

        private void txtRfc_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = !globales.alfaNumerico(e.KeyChar);


        }

        private void limpiarCamposRFC()
        {
            this.txtRfc.Text = "";
            this.txtnombre_em.Text = "";
            this.txtProyecto.Text = "";
            this.txtSueldoBase.Text = string.Format("{0:C}", 0);
            this.txtNap.Text = "";
            this.txtDomicilio.Text = "";
            this.txtNue.Text = "";
            this.label48.Visible = false;
            this.txtelabora.Text = "";
            this.txtelabora.Visible = false;
            this.label48.Visible = false;
        }



        private void txtRfc_TextChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch
            {

            }
        }

        private void txtSecretaria_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
        }

        private void limpiarSecretariaCampos()
        {
            txtSecretaria.Text = "";
            txtAdscripcion.Text = "";
            txtAntiguedad.Text = "A M Q";
            txtmeses_corres.Text = "0";
            txtplazo.Text = "";
            txtTipoPago.Text = "";
            txtSueldo_m.Text = string.Format("{0:C}", 0);
            txtTrl.Text = "B";

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);

        }

        private void txtAntQ_TextChanged(object sender, EventArgs e)
        {
            this.boolAnt1 = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);

            if (e.KeyChar == '.')
                e.Handled = true;
        }

        private void txtliquido_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);

        }

        private void txtliquido_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSecretaria_TextChanged(object sender, EventArgs e)
        {
            this.boolSecretaria = true;
        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void txtAntQ_Leave(object sender, EventArgs e)
        {
            if (!this.boolAnt1) return;

            int valor = (string.IsNullOrWhiteSpace(txtAntQ.Text) ? 0 : Convert.ToInt32(txtAntQ.Text));
            if (valor < 12 && valor != 0)
            {
                globales.MessageBoxExclamation("No debe ser menor a 12 quincenas", "Aviso", globales.menuPrincipal);
                txtAntQ.Text = "0";
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAntQ.Text))
            {
                txtAntQ.Text = "0";

            }

            if (!string.IsNullOrWhiteSpace(txtSecretaria.Text))
            {
                rellenarCamposSecretarias(auxiliar, true);

            }
            this.boolAnt1 = false;
        }

        private void txtliquido_Leave(object sender, EventArgs e)
        {

        }

        private void limpiarLiquidoCampos()
        {
            txtF_primerdesc.Text = "";
            txtliquido.Text = string.Format("{0:C}", 0);
            txtFondo_g.Text = string.Format("{0:C}", 0);
            txtOtros_desc.Text = string.Format("{0:C}", 0);
            txtintereses.Text = string.Format("{0:C}", 0);
            txtImporte.Text = string.Format("{0:C}", 0);
            txtImpUnit.Text = string.Format("{0:C}", 0);
            txtultpago.Text = "";
            txtF_primerdesc.Text = "";
            lblmod.Text = "Base";
            txtPorc.Text = "0.00";

        }

        private void txtRfc1_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = !globales.alfaNumerico(e.KeyChar);
        }

        private void txtrfc2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
        }

        public void rellenarAval1(Dictionary<string, object> datos, bool externo = false)
        {

            if (Convert.ToString(datos["rfc"]) == txtrfc2.Text)
            {
                globales.MessageBoxExclamation("Aval repetido, porfavor ingresar otro aval", "Error aval", globales.menuPrincipal);
                return;
            }

            txtRfc1.Text = Convert.ToString(datos["rfc"]);
            txtProyect1.Text = Convert.ToString(datos["proyecto"]).Length > 12 ? Convert.ToString(datos["proyecto"]).Substring(0,12): Convert.ToString(datos["proyecto"]);
            txtNap1.Text = Convert.ToString(datos["nap"]);
            txtNombre1.Text = Convert.ToString(datos["nombre_em"]);
            txtdomicilio1.Text = Convert.ToString(datos["direccion"]);
            txtNue1.Text = Convert.ToString(datos["nue"]);
            txtAnti1.Text = Convert.ToString(datos["antig_q"]);
        }
        public void rellenarAval2(Dictionary<string, object> datos, bool externo = false)
        {
            if (Convert.ToString(datos["rfc"]) == txtRfc1.Text)
            {
                globales.MessageBoxExclamation("Aval repetido, porfavor ingresar otro aval", "Error aval", globales.menuPrincipal);
                return;
            }

            txtrfc2.Text = Convert.ToString(datos["rfc"]);
            txtproy2.Text = Convert.ToString(datos["proyecto"]).Length > 12 ? Convert.ToString(datos["proyecto"]).Substring(0,12):Convert.ToString(datos["proyecto"]);
            txtnap2.Text = Convert.ToString(datos["nap"]);
            txtnombre2.Text = Convert.ToString(datos["nombre_em"]);
            txtdomicilio2.Text = Convert.ToString(datos["direccion"]);
            txtnue2.Text = Convert.ToString(datos["nue"]);
            txtantg2.Text = Convert.ToString(datos["antig_q"]);
        }

        private void button3_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(txtRfc.Text))
            {
                globales.MessageBoxExclamation("Se debe insertar un RFC para continuar", "Atención",globales.menuPrincipal);
                txtRfc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSecretaria.Text))
            {
                globales.MessageBoxExclamation("Se debe insertar secretaria", "Atención", globales.menuPrincipal);
                txtSecretaria.Focus();
                return;
            }


            DialogResult p = globales.MessageBoxQuestion("¿Desea seguir con el proceso?","Aviso",globales.menuPrincipal);
            if (p == DialogResult.No) return;
            p_quirog obj = verificarObjeto();

            if (!guardar)
            {
                if (modificar(obj))
                {
                    globales.MessageBoxSuccess("Registro actualizado exitosamente!!", "Registro actualizado", globales.menuPrincipal);
                    DialogResult resultado = globales.MessageBoxQuestion("¿Desea imprimir la presente solicitud?", "Atención", globales.menuPrincipal);
                    if (resultado == DialogResult.No)
                    {
                        globales.MessageBoxInformation("Puede impirmir más adelante!!", "Impresión", globales.menuPrincipal);
                    }
                    else
                    {
                        this.Cursor = Cursors.WaitCursor;
                        int checador = (this.carta == "S") ? 5 : 4;
                        imprimir(obj, checador);

                    }
                    globales.MessageBoxInformation("Proceso terminado..", "Aviso", globales.menuPrincipal);
                    limpiarTodosCampos();
                    btnNuevo.Enabled = true;
                    btnsalir.Text = "SALIR";
                    btnModifica.Enabled = true;
                    btnModifica.Visible = true;
                    btnGuardar.Enabled = false;
                    btnGuardar.Visible = false;
                    btnImprimir.Enabled = true;
                    //  btnCalculo.Enabled = false;
                    txtFolio.Text = "AUTOGENERADO";
                }
                else
                    globales.MessageBoxError("Error al actualizar el registro, contactar al equipo de sistemas!!", "Error", globales.menuPrincipal);
            }
            else if (guardar)
            {
                if (insertarRegistro(obj))
                {
                    globales.MessageBoxSuccess("Registro guardado exitosamente!!", "Registro guardado", globales.menuPrincipal);
                    DialogResult resultado = globales.MessageBoxQuestion("¿Desea imprimir la presente solicitud?", "Atención",globales.menuPrincipal);
                    if (resultado == DialogResult.No)
                    {
                        globales.MessageBoxInformation("Puede imprimir más adelante!!", "Impresión", globales.menuPrincipal);
                    }
                    else
                    {
                        this.Cursor = Cursors.WaitCursor;
                        int checador = (this.carta == "S") ? 5 : 4;
                        imprimir(obj, checador);

                    }
                    globales.MessageBoxInformation("Proceso terminado..", "Aviso", globales.menuPrincipal);
                    limpiarTodosCampos();
                    btnNuevo.Enabled = true;
                    btnsalir.Text = "SALIR";
                    btnModifica.Enabled = true;
                    btnModifica.Visible = true;
                    btnGuardar.Enabled = false;
                    btnGuardar.Visible = false;
                    btnImprimir.Enabled = true;
                    //    btnCalculo.Enabled = false;
                    txtFolio.Text = "AUTOGENERADO";
                }
                else
                    globales.MessageBoxError("Error al guardar el registor, contactar al equipo de sistemas!!", "Error", globales.menuPrincipal);
            }

            this.Cursor = Cursors.Default;
        }


        private bool modificar(p_quirog obj)
        {
            bool registro = false;

            try
            {
                obj.folio = Convert.ToInt32(txtFolio.Text);
                obj.f_emischeq = Regex.Replace(obj.f_emischeq, @"\s+", "");
                obj.f_primdesc = Regex.Replace(obj.f_primdesc, @"\s+", "");
                obj.f_ultmode = Regex.Replace(obj.f_ultmode, @"\s+", "");
                string query = "update  datos.p_quirog set rfc = '{0}',nombre_em = '{1}',proyecto = '{2}',secretaria = '{3}',antig_q = {4},sueldo_base = {5},descripcion = '{6}',telefono = '{7}',extension = '{8}',direccion='{9}',nue = '{10}',nap = {11}," +
                   "sueldo_m = {12},ant_a = {13},ant_m = {14},ant_q = {15},meses_corres = {16},otros_desc = {17},trel = '{18}',porc = {19},plazo = {20},tipo_pago = '{21}',f_emischeq = {22},f_primdesc = {23},f_ultimode = {24},imp_unit = {25},importe = {26},interes = {27},fondo_g = {28},liquido = {29},carta = '{30}',f_solicitud = '{31}',secuen = {32},aceptado = '{33}',tipo_rel = '{35}',cve_categ = '{36}',sexo = '{37}' where folio = {34} ";

                query = string.Format(query, obj.rfc, obj.nombre_em, obj.proyecto, obj.secretaria, obj.antig_q, obj.sueldo_base, obj.descripcion, obj.telefono, obj.extencion, obj.direccion, obj.nue, obj.nap,
                   obj.sueldo_m, obj.ant_a, obj.ant_m, obj.ant_q, obj.meses_corres, obj.otros_desc, obj.trel, obj.porc, obj.plazo, obj.tipo_pago, obj.f_emischeq, obj.f_primdesc, obj.f_ultmode, obj.imp_unit, obj.importe, obj.interes, obj.fondo_g, obj.liquido,
                   obj.carta, obj.f_solicitud, obj.secuen, obj.aceptado, obj.folio,obj.tipo_rel,obj.cve_categ,obj.sexo);

                globales.consulta(query, true);

                string queryempleados = $"update datos.empleados  set direccion='{txtDomicilio.Text}' , nombre_em='{txtnombre_em.Text}'  where rfc = '{txtRfc.Text}';";
                globales.consulta(queryempleados, true);


                obj.lista = new List<d_quirog>();
                string status = (this.boolDeducciones) ? "S" : "N";
                query = string.Format("update datos.d_perded set status='{1}', secuen = {2}, rfc = '{3}',nombre = '{4}',percepciones = {5},deducciones = {6}, deduc_rec2 = {7}, deduc_rec1 = {8}, importe = {9}, desc_unit = {10}, porcentaje1 = {11}, porcentaje2 = {12}, per3 = {13}, per4 = {14}, per5 = {15}, per6 = {16}, ded3 = {17}, ded4 = {18}, ded5 = {19}, ded6 = {20}, ded7 = {21}, ded8 = {22}, ded9 = {23}, ded10 = {24} where folio = {0}"
                        , obj.folio, status, 1, obj.rfc, obj.nombre_em, this.PER2, this.DED1, this.RES, this.RES1, this.IMP, this.IM, this.RES3, this.RES2, this.PER3, this.PER4, this.PER5, this.PER6, this.DED3, this.DED4, this.DED5, this.DED6, this.DED7, this.DED8, this.DED9, this.DED10);
                globales.consulta(query,true);

                query = string.Format("select * from datos.d_quirog where folio = {0} order by id asc",txtFolio.Text);
                List<Dictionary<string,object>> resultado = globales.consulta(query);
                int cantidad = resultado.Count;
                query = "select count(folio) as cantidad from datos.d_quirog";
                int cantidadFolios = Convert.ToInt32(globales.consulta(query)[0]["cantidad"]);

                if (cantidad == 0)
                {
                    
                    d_quirog detalleQuirog = new d_quirog();
                    detalleQuirog.folio = obj.folio;
                    detalleQuirog.rfc = txtRfc1.Text;
                    detalleQuirog.nombre_em = txtNombre1.Text;
                    detalleQuirog.direccion = txtdomicilio1.Text;
                    detalleQuirog.proyecto = txtProyect1.Text;
                    detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtNap1.Text)) ? 0 : Convert.ToDouble(txtNap1.Text);
                    detalleQuirog.nue = txtNue1.Text;
                    detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtAnti1.Text)) ? 0 : Convert.ToInt32(txtAnti1.Text);

                    query = "insert into datos.D_QUIROG values({0},'{1}','{2}','{3}','{4}',{5},'{6}',{7},'',{8})";
                    query = string.Format(query, detalleQuirog.folio, detalleQuirog.rfc, detalleQuirog.nombre_em, detalleQuirog.direccion, detalleQuirog.proyecto, detalleQuirog.nap, detalleQuirog.nue, detalleQuirog.antig,cantidad+1);
                    globales.consulta(query, true);
                    obj.lista.Add(detalleQuirog);


                    detalleQuirog = new d_quirog();
                    detalleQuirog.folio = obj.folio;
                    detalleQuirog.rfc = txtrfc2.Text;
                    detalleQuirog.nombre_em = txtnombre2.Text;
                    detalleQuirog.direccion = txtdomicilio2.Text;
                    detalleQuirog.proyecto = txtproy2.Text;
                    detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtnap2.Text)) ? 0 : Convert.ToDouble(txtnap2.Text);
                    detalleQuirog.nue = txtnue2.Text;
                    detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtantg2.Text)) ? 0 : Convert.ToInt32(txtantg2.Text);
                    query = "insert into datos.D_QUIROG values({0},'{1}','{2}','{3}','{4}',{5},'{6}',{7},'',{8})";
                    query = string.Format(query, detalleQuirog.folio, detalleQuirog.rfc, detalleQuirog.nombre_em, detalleQuirog.direccion, detalleQuirog.proyecto, detalleQuirog.nap, detalleQuirog.nue, detalleQuirog.antig,cantidad+2);
                    globales.consulta(query, true);
                    obj.lista.Add(detalleQuirog);
                }
                else if (cantidad == 1)
                {
                    d_quirog detalleQuirog = new d_quirog();
                    detalleQuirog.folio = obj.folio;
                    detalleQuirog.rfc = txtRfc1.Text;
                    detalleQuirog.nombre_em = txtNombre1.Text;
                    detalleQuirog.direccion = txtdomicilio1.Text;
                    detalleQuirog.proyecto = txtProyect1.Text;
                    detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtNap1.Text)) ? 0 : Convert.ToDouble(txtNap1.Text);
                    detalleQuirog.nue = txtNue1.Text;
                    detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtAnti1.Text)) ? 0 : Convert.ToInt32(txtAnti1.Text);

                    query = "update  datos.D_QUIROG set rfc = '{1}', nombre_em = '{2}',direccion = '{3}',proyecto = '{4}',nap = {5},nue = '{6}',antig = {7} where folio = {0} and id = {8}";
                    query = string.Format(query, detalleQuirog.folio, detalleQuirog.rfc, detalleQuirog.nombre_em, detalleQuirog.direccion, detalleQuirog.proyecto, detalleQuirog.nap, detalleQuirog.nue, detalleQuirog.antig,resultado[0]["id"]);
                    globales.consulta(query, true);
                    obj.lista.Add(detalleQuirog);
                    detalleQuirog = new d_quirog();
                    detalleQuirog.folio = obj.folio;
                    detalleQuirog.rfc = txtrfc2.Text;
                    detalleQuirog.nombre_em = txtnombre2.Text;
                    detalleQuirog.direccion = txtdomicilio2.Text;
                    detalleQuirog.proyecto = txtproy2.Text;
                    detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtnap2.Text)) ? 0 : Convert.ToDouble(txtnap2.Text);
                    detalleQuirog.nue = txtnue2.Text;
                    detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtantg2.Text)) ? 0 : Convert.ToInt32(txtantg2.Text);
                    query = "insert into datos.D_QUIROG values({0},'{1}','{2}','{3}','{4}',{5},'{6}',{7},'',{8})";
                    query = string.Format(query, detalleQuirog.folio, detalleQuirog.rfc, detalleQuirog.nombre_em, detalleQuirog.direccion, detalleQuirog.proyecto, detalleQuirog.nap, detalleQuirog.nue, detalleQuirog.antig,cantidadFolios+1);
                    globales.consulta(query, true);
                    obj.lista.Add(detalleQuirog);
                }
                else {
                    d_quirog detalleQuirog = new d_quirog();
                    detalleQuirog.folio = obj.folio;
                    detalleQuirog.rfc = txtRfc1.Text;
                    detalleQuirog.nombre_em = txtNombre1.Text;
                    detalleQuirog.direccion = txtdomicilio1.Text;
                    detalleQuirog.proyecto = txtProyect1.Text;
                    detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtNap1.Text)) ? 0 : Convert.ToDouble(txtNap1.Text);
                    detalleQuirog.nue = txtNue1.Text;
                    detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtAnti1.Text)) ? 0 : Convert.ToInt32(txtAnti1.Text);

                    query = "update  datos.D_QUIROG set rfc = '{1}', nombre_em = '{2}',direccion = '{3}',proyecto = '{4}',nap = {5},nue = '{6}',antig = {7} where folio = {0} and id = {8}";
                    query = string.Format(query, detalleQuirog.folio, detalleQuirog.rfc, detalleQuirog.nombre_em, detalleQuirog.direccion, detalleQuirog.proyecto, detalleQuirog.nap, detalleQuirog.nue, detalleQuirog.antig,resultado[0]["id"]);
                    globales.consulta(query, true);
                    obj.lista.Add(detalleQuirog);
                    detalleQuirog = new d_quirog();
                    detalleQuirog.folio = obj.folio;
                    detalleQuirog.rfc = txtrfc2.Text;
                    detalleQuirog.nombre_em = txtnombre2.Text;
                    detalleQuirog.direccion = txtdomicilio2.Text;
                    detalleQuirog.proyecto = txtproy2.Text;
                    detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtnap2.Text)) ? 0 : Convert.ToDouble(txtnap2.Text);
                    detalleQuirog.nue = txtnue2.Text;
                    detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtantg2.Text)) ? 0 : Convert.ToInt32(txtantg2.Text);
                    query = "update  datos.D_QUIROG set rfc = '{1}', nombre_em = '{2}',direccion = '{3}',proyecto = '{4}',nap = {5},nue = '{6}',antig = {7} where folio = {0} and id = {8}";
                    query = string.Format(query, detalleQuirog.folio, detalleQuirog.rfc, detalleQuirog.nombre_em, detalleQuirog.direccion, detalleQuirog.proyecto, detalleQuirog.nap, detalleQuirog.nue, detalleQuirog.antig,resultado[1]["id"]);
                    globales.consulta(query, true);
                    obj.lista.Add(detalleQuirog);
                }

                
                registro = true;

            }
            catch
            {
                registro = false;
            }

            return registro;
        }

        private bool insertarRegistro(p_quirog obj)
        {
            bool registro = false;

            try
            {
                string query = "select max(folio) as maximo from datos.p_quirog";
                var resultado = globales.consulta(query);
                int maximo = Convert.ToInt32(string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["maximo"])) ? 0 : resultado[0]["maximo"]) + 1;

                obj.folio = maximo;

                query = "insert into datos.p_quirog(folio,rfc,nombre_em,proyecto,secretaria,antig_q,sueldo_base,descripcion,telefono,extension,direccion,nue,nap," +
                    "sueldo_m,ant_a,ant_m,ant_q,meses_corres,otros_desc,trel,porc,plazo,tipo_pago,f_emischeq,f_primdesc,f_ultimode,imp_unit,importe,interes,fondo_g,liquido,carta,f_solicitud,secuen,aceptado,tipo_rel,cve_categ,sexo,f_elabora) values({0},'{1}','{2}','{3}','{4}',{5},{6},'{7}','{8}','{9}'," +
                    "'{10}','{11}',{12},{13},{14},{15},{16},{17},{18},'{19}',{20},{21},'{22}',{23},{24},{25},{26},{27},{28},{29},{30},'{31}','{32}',{33},'{34}','{35}','{36}','{37}','{38}')";
                query = string.Format(query, obj.folio, obj.rfc, obj.nombre_em, obj.proyecto, obj.secretaria, obj.antig_q, obj.sueldo_base, obj.descripcion, obj.telefono, obj.extencion, obj.direccion, obj.nue, obj.nap,
                    obj.sueldo_m, obj.ant_a, obj.ant_m, obj.ant_q, obj.meses_corres, obj.otros_desc, obj.trel, obj.porc, obj.plazo, obj.tipo_pago, obj.f_emischeq, obj.f_primdesc, obj.f_ultmode, obj.imp_unit, obj.importe, obj.interes, obj.fondo_g, obj.liquido,
                    obj.carta, obj.f_solicitud, obj.secuen, obj.aceptado,obj.tipo_rel,obj.cve_categ,obj.sexo,string.Format("{0:yyyy-MM-dd}",DateTime.Now));

                obj.lista = new List<d_quirog>();
                if (globales.consulta(query, true))
                {
                    string queryempleados = $"update datos.empleados  set direccion='{txtDomicilio.Text}' , nombre_em='{txtnombre_em.Text}' , telefono='{txtTelefono.Text}' where rfc = '{txtRfc.Text}';";
                    globales.consulta(queryempleados, true);

                    registro = true;
                    //Parte que se agregar para las insercciones de la tabla de deducciones....
                    string status = (this.boolDeducciones) ? "S" : "N";
                    query = string.Format("insert into datos.d_perded values({0},'{1}',{2},'{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24})"
                        , obj.folio, status, 1, obj.rfc, obj.nombre_em, this.PER2, this.DED1, this.RES, this.RES1, this.IMP, this.IM, this.RES3, this.RES2, this.PER3, this.PER4, this.PER5, this.PER6, this.DED3, this.DED4, this.DED5, this.DED6, this.DED7, this.DED8, this.DED9, this.DED10);

                    globales.consulta(query, true);

                    
                        d_quirog detalleQuirog = new d_quirog();
                        detalleQuirog.folio = obj.folio;
                        detalleQuirog.rfc = txtRfc1.Text;
                        detalleQuirog.nombre_em = txtNombre1.Text;
                        detalleQuirog.direccion = txtdomicilio1.Text;
                        detalleQuirog.proyecto = txtProyect1.Text;
                        detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtNap1.Text)) ? 0 : Convert.ToDouble(txtNap1.Text);
                        detalleQuirog.nue = txtNue1.Text;
                        detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtAnti1.Text)) ? 0 : Convert.ToInt32(txtAnti1.Text);
                        query = "insert into datos.D_QUIROG values({0},'{1}','{2}','{3}','{4}',{5},'{6}',{7},'')";
                        query = string.Format(query, detalleQuirog.folio, detalleQuirog.rfc, detalleQuirog.nombre_em, detalleQuirog.direccion, detalleQuirog.proyecto, detalleQuirog.nap, detalleQuirog.nue, detalleQuirog.antig);
                        globales.consulta(query, true);
                        registro = true;
                        obj.lista.Add(detalleQuirog);
                    

                    
                        detalleQuirog = new d_quirog();
                        detalleQuirog.folio = obj.folio;
                        detalleQuirog.rfc = txtrfc2.Text;
                        detalleQuirog.nombre_em = txtnombre2.Text;
                        detalleQuirog.direccion = txtdomicilio2.Text;
                        detalleQuirog.proyecto = txtproy2.Text;
                        detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtnap2.Text)) ? 0 : Convert.ToDouble(txtnap2.Text);
                        detalleQuirog.nue = txtnue2.Text;
                        detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtantg2.Text)) ? 0 : Convert.ToInt32(txtantg2.Text);
                        query = "insert into datos.D_QUIROG values({0},'{1}','{2}','{3}','{4}',{5},'{6}',{7},'')";
                        query = string.Format(query, detalleQuirog.folio, detalleQuirog.rfc, detalleQuirog.nombre_em, detalleQuirog.direccion, detalleQuirog.proyecto, detalleQuirog.nap, detalleQuirog.nue, detalleQuirog.antig);
                        globales.consulta(query, true);
                        registro = true;
                        obj.lista.Add(detalleQuirog);
                    

                    //Sección de código que aumenta el tope de la fecha de emisión de cheque...... si no pasa a la fecha siguiente.... Santiago antonio mariscal velásquez

                    query = string.Format("update catalogos.progpq set utilizados = utilizados + 1 where fecha = {0}", obj.f_emischeq);
                    globales.consulta(query, true);

                }
                else
                {
                    registro = false;
                }

            }
            catch
            {
                registro = false;
            }


            return registro;
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            limpiarTodosCampos();

            //Está parte del código verifica la fecha de emisión de cheque.....................
            this.Cursor = Cursors.WaitCursor;

            List<Dictionary<string, object>> resultado;
            string query;
            DateTime hoy = DateTime.Now;

            string auxHoy = string.Format("{0}-{1}-{2}", hoy.Year, hoy.Month, hoy.Day);
            regresar:
            query = string.Format("select * from catalogos.progpq where fecha > '{0}' and inhabil <> '*' and utilizados <> programados  order by fecha asc limit 1", auxHoy);
            resultado = globales.consulta(query);
            if (resultado.Count == 0)
            {
                globales.MessageBoxExclamation("Para continuar las solicitudes se debe generar el mes siguiente..", "Fecha emisión de cheque", globales.menuPrincipal);
                DialogResult respuesta = globales.MessageBoxQuestion("¿Desea generar el mes siguiente ahora?", "Generar mes siguiente", globales.menuPrincipal);
                if (respuesta == DialogResult.No)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    frmdiacheque obj1 = new frmdiacheque();
                    obj1.ShowDialog();
                    resultado = globales.consulta(query);
                    if (resultado.Count == 0)
                    {
                        goto regresar;
                    }
                }
            }

            //----------------- fin de fecha de emisión de cheque-----------------------


            //Si todo esta bien se saca la fecha de emisión de cheque.......
            string fechaProgramacion = Convert.ToString(resultado[0]["fecha"]).Replace(" 12:00:00 a. m.", "");

            activarControlesBasicos();
            txtEmisionCheque.Text = txtFecha;

            btnNuevo.Enabled = false;
            btnGuardar.Visible = true;
            btnGuardar.Enabled = true;

            //btnCalculo.Enabled = true;


            btnModifica.Enabled = false;
            btnModifica.Visible = false;
            btnImprimir.Enabled = false;

            btnsalir.Text = "Cancelar";
            txtFolio.Text = "AUTOGENERADO";

            txtEmisionCheque.Text = fechaProgramacion;


            guardar = true;

            this.ActiveControl = txtRfc;
            this.Cursor = Cursors.Default;
            fechaSolicitud = string.Format("{0:d}", DateTime.Now);
        }

        private void activarControlesBasicos()
        {
            label48.Visible = true;
            activarControl(txtRfc);
            activarControl(txtelabora);
            activarControl(txtSecretaria);
            activarControl(txtAntQ);
            activarControl(txtTelefono);
            activarControl(txtExtencion);
            activarControl(txtRfc1);
            activarControl(txtrfc2);
            activarControl(txtdomicilio1);
            activarControl(txtdomicilio2);
            activarControl(txtantg2);
            activarControl(txtAnti1);
            activarControl(txtSueldoBase);
            activarControl(txtmeses_corres);
            activarControl(txtplazo);
            activarControl(txtProyecto);
            activarControl(txtDomicilio);
            activarControl(txtProyect1);
            activarControl(txtproy2);
            btnBuscarRfc.Enabled = true;
            btnSecretaria.Enabled = true;
            txtelabora.Visible = true;
            txtelabora.Enabled = false;
            btnRfc1.Enabled = true;
            btnRfc2.Enabled = true;
        }
        private void desactivarControlesBasicos()
        {
            desactivarControl(txtFolio);
            desactivarControl(txtRfc);
            desactivarControl(txtSecretaria);
            desactivarControl(txtAntQ);
            desactivarControl(txtTelefono);
            desactivarControl(txtExtencion);
            desactivarControl(txtRfc1);
            desactivarControl(txtrfc2);
            desactivarControl(txtAnti1);
            desactivarControl(txtantg2);
            desactivarControl(txtdomicilio1);
            desactivarControl(txtdomicilio2);
            desactivarControl(txtDomicilio);
            desactivarControl(txtSueldoBase);
            desactivarControl(txtmeses_corres);
            desactivarControl(txtPorc);
            desactivarControl(txtplazo);
            desactivarControl(txtProyecto);
            desactivarControl(txtProyect1);
            desactivarControl(txtproy2);
            btnFolio.Enabled = false;
            btnBuscarRfc.Enabled = false;
            btnSecretaria.Enabled = false;
            btnRfc1.Enabled = false;
            btnRfc2.Enabled = false;
        }

        public void desactivarControl(TextBox control)
        {
            control.Enabled = false;
        }
        public void activarControl(TextBox control)
        {
            control.Enabled = true;
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            limpiarTodosCampos();
            activarControl(txtFolio);
            //activarControlesBasicos();


            btnNuevo.Enabled = false;
            btnGuardar.Visible = true;
            btnGuardar.Enabled = true;


            btnModifica.Enabled = false;
            btnModifica.Visible = false;
            btnImprimir.Enabled = false;
            btnsalir.Text = "Cancelar";
            txtFolio.Text = "";

            txtFolio.Enabled = true;


            btnModifica.Enabled = true;

            // btnCalculo.Enabled = true;

            btnFolio.Enabled = true;
            this.ActiveControl = txtFolio;
            guardar = false;
        }
        private void rellenarModificarFolios(Dictionary<string, object> quirografario, List<Dictionary<string, object>> avales, bool externo = false)
        {
            txtelabora.Text=  Convert.ToString(quirografario["f_elabora"]).Replace("12:00:00 a. m.", ""); ;
            txtFolio.Text = Convert.ToString(quirografario["folio"]);
            txtRfc.Text = Convert.ToString(quirografario["rfc"]);
            txtnombre_em.Text = Convert.ToString(quirografario["nombre_em"]);
            txtProyecto.Text = Convert.ToString(quirografario["proyecto"]).Length > 12 ? Convert.ToString(quirografario["proyecto"]).Substring(0, 12) : Convert.ToString(quirografario["proyecto"]);
            txtSecretaria.Text = Convert.ToString(quirografario["secretaria"]);
            txtAntQ.Text = string.IsNullOrWhiteSpace(Convert.ToString(quirografario["antig_q"])) || Convert.ToString(quirografario["antig_q"]) == "0" ? "0" : Convert.ToDouble(quirografario["antig_q"]).ToString("#.##");
            txtSueldoBase.Text = string.IsNullOrWhiteSpace(Convert.ToString(quirografario["sueldo_base"])) ? string.Format("{0:C}", 0) : string.Format("{0:C}", quirografario["sueldo_base"]);
            txtAdscripcion.Text = Convert.ToString(quirografario["descripcion"]);
            txtTelefono.Text = Convert.ToString(quirografario["telefono"]);
            txtExtencion.Text = Convert.ToString(quirografario["extension"]);
            txtDomicilio.Text = Convert.ToString(quirografario["direccion"]);
            txtNue.Text = Convert.ToString(quirografario["nue"]);
            txtNap.Text = Convert.ToString(quirografario["nap"]);
            txtSueldo_m.Text = string.IsNullOrWhiteSpace(Convert.ToString(quirografario["sueldo_m"])) ? string.Format("{0:C}", 0) : string.Format("{0:C}", quirografario["sueldo_m"]);
            txtAntiguedad.Text = Convert.ToString(quirografario["ant_a"]) + " A" + Convert.ToString(quirografario["ant_m"]) + " M" + Convert.ToString(quirografario["ant_q"]) + " Q";
            txtmeses_corres.Text = string.IsNullOrWhiteSpace(Convert.ToString(quirografario["meses_corres"])) || Convert.ToString(quirografario["meses_corres"]) == "0" ? "0" : Convert.ToDouble(quirografario["meses_corres"]).ToString("#.##");
            txtOtros_desc.Text = string.IsNullOrWhiteSpace(Convert.ToString(quirografario["otros_desc"])) ? string.Format("{0:C}", 0) : string.Format("{0:C}", quirografario["otros_desc"]);
            txtPorc.Text = string.IsNullOrWhiteSpace(Convert.ToString(quirografario["porc"])) || Convert.ToString(quirografario["porc"]) == "0" ? "0" : globales.checarDecimales(quirografario["porc"]);
            txtplazo.Text = string.IsNullOrWhiteSpace(Convert.ToString(quirografario["plazo"])) || Convert.ToString(quirografario["plazo"]) == "0" ? "0" : Convert.ToString(quirografario["plazo"]);
            txtTipoPago.Text = Convert.ToString(quirografario["tipo_pago"]);
            txtTrl.Text = Convert.ToString(quirografario["trel"]);
            txtEmisionCheque.Text = Convert.ToString(quirografario["f_emischeq"]).Replace("12:00:00 a. m.", "");
            txtF_primerdesc.Text = Convert.ToString(quirografario["f_primdesc"]).Replace("12:00:00 a. m.", ""); 
            txtultpago.Text = Convert.ToString(quirografario["f_ultimode"]).Replace("12:00:00 a. m.", ""); 
            txtImpUnit.Text = string.IsNullOrWhiteSpace(Convert.ToString(quirografario["imp_unit"])) ? string.Format("{0:C}", 0) : string.Format("{0:C}", quirografario["imp_unit"]);
            txtImporte.Text = string.IsNullOrWhiteSpace(Convert.ToString(quirografario["importe"])) ? string.Format("{0:C}", 0) : string.Format("{0:C}", quirografario["importe"]);
            txtintereses.Text = string.IsNullOrWhiteSpace(Convert.ToString(quirografario["interes"])) ? string.Format("{0:C}", 0) : string.Format("{0:C}", quirografario["interes"]);
            txtFondo_g.Text = string.IsNullOrWhiteSpace(Convert.ToString(quirografario["fondo_g"])) ? string.Format("{0:C}", 0) : string.Format("{0:C}", quirografario["fondo_g"]);
            txtliquido.Text = string.IsNullOrWhiteSpace(Convert.ToString(quirografario["liquido"])) ? string.Format("{0:C}", 0) : string.Format("{0:C}", quirografario["liquido"]);
            this.Ant_A = string.IsNullOrWhiteSpace(Convert.ToString((quirografario["ant_a"])))?0: Convert.ToDouble(quirografario["ant_a"]);
            this.Ant_M = string.IsNullOrWhiteSpace(Convert.ToString((quirografario["ant_m"]))) ? 0 : Convert.ToDouble(quirografario["ant_m"]);
            this.Ant_Q = string.IsNullOrWhiteSpace(Convert.ToString((quirografario["ant_q"]))) ? 0 : Convert.ToDouble(quirografario["ant_q"]); 
            fechaSolicitud = Convert.ToString(quirografario["f_solicitud"]).Replace("12:00:00 a. m.", "");
            this.carta = Convert.ToString(quirografario["carta"]);
            this.tipo_rel = Convert.ToString(quirografario["tipo_rel"]);

            if (avales.Count == 1)
            {
                txtRfc1.Text = Convert.ToString(avales[0]["rfc"]);
                txtProyect1.Text = Convert.ToString(avales[0]["proyecto"]).Length > 12 ? Convert.ToString(avales[0]["proyecto"]).Substring(0,12): Convert.ToString(avales[0]["proyecto"]);
                txtNap1.Text = Convert.ToString(avales[0]["nap"]);
                txtNombre1.Text = Convert.ToString(avales[0]["nombre_em"]);
                txtdomicilio1.Text = Convert.ToString(avales[0]["direccion"]);
                txtNue1.Text = Convert.ToString(avales[0]["nue"]);
                txtAnti1.Text = Convert.ToString(avales[0]["antig"]);
            }
            else if (avales.Count == 2)
            {
                txtRfc1.Text = Convert.ToString(avales[0]["rfc"]);
                txtProyect1.Text = Convert.ToString(avales[0]["proyecto"]).Length > 12 ? Convert.ToString(avales[0]["proyecto"]).Substring(0, 12) : Convert.ToString(avales[0]["proyecto"]);
                txtNap1.Text = Convert.ToString(avales[0]["nap"]);
                txtNombre1.Text = Convert.ToString(avales[0]["nombre_em"]);
                txtdomicilio1.Text = Convert.ToString(avales[0]["direccion"]);
                txtNue1.Text = Convert.ToString(avales[0]["nue"]);
                txtAnti1.Text = Convert.ToString(avales[0]["antig"]);

                txtrfc2.Text = Convert.ToString(avales[1]["rfc"]);
                txtproy2.Text = Convert.ToString(avales[1]["proyecto"]).Length > 12 ? Convert.ToString(avales[1]["proyecto"]).Substring(0,12): Convert.ToString(avales[1]["proyecto"]);
                txtnap2.Text = Convert.ToString(avales[1]["nap"]);
                txtnombre2.Text = Convert.ToString(avales[1]["nombre_em"]);
                txtdomicilio2.Text = Convert.ToString(avales[1]["direccion"]);
                txtnue2.Text = Convert.ToString(avales[1]["nue"]);
                txtantg2.Text = Convert.ToString(avales[1]["antig"]);
            }
            buscarFolioPq(Convert.ToString(quirografario["folio"]));
            this.D = "N";
            string query = $"select cuenta,descripcion,proy from catalogos.dependencias where proy = '{txtSecretaria.Text}'";
            List<Dictionary<string, object>> res = globales.consulta(query);
            if (res.Count != 0)
                this.auxiliar = res[0];
            else {
                this.auxiliar = new Dictionary<string, object>();
                auxiliar.Add("proy",txtSecretaria.Text);
                auxiliar.Add("descripcion",quirografario["descripcion"]);
            }



            this.ActiveControl = txtRfc;
        }

        private void buscarFolioPq(string folio)
        {
            try
            {
                string query = string.Format("select * from datos.d_perded where folio = {0}", folio);
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count != 0)
                {
                    string status = Convert.ToString(resultado[0]["status"]);
                    if (status == "S")
                    {
                        this.PER = Convert.ToDouble(resultado[0]["percepciones"]);
                        this.DED = Convert.ToDouble(resultado[0]["deducciones"]);
                        this.PER3 = Convert.ToDouble(resultado[0]["per3"]);
                        this.PER4 = Convert.ToDouble(resultado[0]["per4"]);
                        this.PER5 = Convert.ToDouble(resultado[0]["per5"]);
                        this.PER6 = Convert.ToDouble(resultado[0]["per6"]);
                        this.DED3 = Convert.ToDouble(resultado[0]["ded3"]);
                        this.DED4 = Convert.ToDouble(resultado[0]["ded4"]);
                        this.DED5 = Convert.ToDouble(resultado[0]["ded5"]);
                        this.DED6 = Convert.ToDouble(resultado[0]["ded6"]);
                        this.DED7 = Convert.ToDouble(resultado[0]["ded7"]);
                        this.DED8 = Convert.ToDouble(resultado[0]["ded8"]);
                        this.DED9 = Convert.ToDouble(resultado[0]["ded9"]);
                        this.DED10 = Convert.ToDouble(resultado[0]["ded10"]);
                    }
                    else
                    {
                        PER = 0;
                        DED = 0;
                        PER3 = 0;
                        PER4 = 0;
                        PER5 = 0;
                        PER6 = 0;
                        DED3 = 0;
                        DED4 = 0;
                        DED5 = 0;
                        DED6 = 0;
                        DED7 = 0;
                        DED8 = 0;
                        DED9 = 0;
                        DED10 = 0;
                    }
                }
            }
            catch
            {
                globales.MessageBoxError("Error al recuperar datos de percepciones", "Aviso",globales.menuPrincipal);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            calculoLiquido();
        }

        private void calculoLiquido()
        {

            try
            {
                if (resultadoaux == null) return;
                if (resultadoaux.Count == 0) return;
                Dictionary<string, object> objeto = resultadoaux[0];
                txtTrl.Text = Convert.ToString(objeto["trel"]);
                lblmod.Text = modalidades[txtTrl.Text];
                t_interes = Convert.ToDouble(objeto["tasa"]);
                t_interes = (t_interes / 24) / 100;
                string mensaje = string.Format("Se aplico tasa del {0}", fechaSolicitud);
                globales.MessageBoxInformation(mensaje, "Aplicación de tasas", globales.menuPrincipal);
                DialogResult dialogo;

                double auxSueldoM = double.Parse(txtSueldo_m.Text, NumberStyles.Currency);
                this.meses_corres = double.Parse(txtmeses_corres.Text, NumberStyles.Currency);
                this.plazo = int.Parse(txtplazo.Text);

                txtImpUnit.Text = string.Format("{0:C}", Convert.ToInt32(((auxSueldoM * meses_corres) / plazo).ToString().Split('.')[0]));

                TOPE = 0.00;
                RES = 0.00;
                RESL = 0.00;
                RES3 = 0.00;
                RES1 = 0.00;
                Por = 0.00;
                RESD = 0.00;
                RES2 = 0.00;
                string RO = string.Empty;
                string NOM = string.Empty;
                IMP = 0.00;
                IM = 0.00;

                //if (!this.D.Equals("N"))
                //{

                    TOPE = PER2 / 2;
                    RES = DED1 + double.Parse(txtImpUnit.Text, NumberStyles.Currency);
                    RESL = double.Parse(txtImpUnit.Text, NumberStyles.Currency);
                    RES3 = RES / PER2;
                    RES3 = RES3 * 100;
                    RES1 = DED1 + double.Parse(txtImpUnit.Text, NumberStyles.Currency);
                    Por = RES3;
                    double xxx = (double.IsInfinity(RES3) ? 0 : RES3);
                    if (xxx!=0) {
                    if (TOPE < RES)
                    {
                        regresar1:
                        string cadena = string.Format("Este RFC se excede con un {0}%\n¿Desea ajustar?", double.IsInfinity(RES3) ? 0 : RES3);
                        dialogo = globales.MessageBoxQuestion(cadena, "Atención",globales.menuPrincipal);
                        if (dialogo == DialogResult.Yes)
                        {
                            txtImpUnit.Text = string.Format("{0:C}", (TOPE - DED1)).Split('.')[0];
                            RESD = double.Parse(txtImpUnit.Text, NumberStyles.Currency);
                            RES1 = DED1 + double.Parse(txtImpUnit.Text, NumberStyles.Currency);
                            RES2 = RES1 / PER2;
                            RES2 = RES2 * 100;
                            Por = RES2;
                        }
                        else
                        {
                            dialogo = globales.MessageBoxQuestion("¿Esta seguro de continuar?", "Atención",globales.menuPrincipal);
                            if (dialogo == DialogResult.Yes)
                            {
                                RES2 = RES / PER2;
                                RES2 = RES2 * 100;
                                Por = RES;
                            }
                            else
                            {
                                goto regresar1;
                            }
                        }

                    }
                    else
                    {
                        RES2 = RES / PER2;
                        RES2 = RES2 * 100;
                        Por = RES2;
                    }
                }
                //}
                //else
                //{
                //    TOPE = PER2 / 2;
                //    RES = DED + double.Parse(txtImpUnit.Text, NumberStyles.Currency);
                //    RES3 = RES / PER2;
                //    RES3 = RES3 * 100;
                //    Por = RES3;
                //    RES1 = DED + double.Parse(txtImpUnit.Text, NumberStyles.Currency);
                //    if (TOPE < RES)
                //    {
                //        regresa2:
                //        string cadena = string.Format("Este RFC se excede con un {0}%\n¿Desea ajustar?", double.IsInfinity(RES3) ? 0 : RES3);
                //        dialogo = MessageBox.Show(cadena, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                //        if (dialogo == DialogResult.Yes)
                //        {
                //            txtImpUnit.Text = string.Format("{0:C}", (TOPE - DED)).Split('.')[0]+".00";
                //            RES1 = DED + double.Parse(txtImpUnit.Text, NumberStyles.Currency);
                //            RES2 = RES1 / PER2;
                //            RES2 = RES2 * 100;
                //            Por = RES2;
                //        }
                //        else
                //        {
                //            dialogo = MessageBox.Show("¿Esta seguro de continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //            if (dialogo == DialogResult.Yes)
                //            {
                //                RES2 = RES1 / PER2;
                //                RES2 = RES2 * 100;
                //                Por = RES2;
                //            }
                //            else
                //            {
                //                goto regresa2;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        RES2 = RES1 / PER2;
                //        RES2 = RES2 * 100;
                //        Por = RES2;
                //    }
                //}

                //**********************termina el calculoi de txtimpUnit***********
                txtImporte.Text = string.Format("{0:C}", double.Parse(txtImpUnit.Text, NumberStyles.Currency) * plazo);
                //Agrega información a la base
                RO = txtRfc.Text;
                NOM = txtnombre_em.Text;
                IMP = double.Parse(txtImporte.Text, NumberStyles.Currency);
                IM = double.Parse(txtImpUnit.Text, NumberStyles.Currency);
                txtPorc.Text = (Convert.ToString(Math.Round(Por, 2)));
                txtPorc.Text = (txtPorc.Text == "∞") ? "0" : txtPorc.Text;
                double aux1 = double.Parse(txtImporte.Text, NumberStyles.Currency);
                if (txtTipoPago.Text == "Q")
                    txtintereses.Text = string.Format("{0:C3}", ((aux1 * ((plazo / 2) + 1)) * t_interes));
                else
                    txtintereses.Text = string.Format("{0:C3}", ((aux1 * ((plazo) + 1)) * t_interes));

                txtintereses.Text = txtintereses.Text.Substring(0,txtintereses.Text.Length-1);

                int auxAnti_q = Convert.ToInt32(txtAntQ.Text);
                if (auxAnti_q < 240 && txtSecretaria.Text != "J" && txtSecretaria.Text != "T" && txtSecretaria.Text != "P")
                {
                    txtFondo_g.Text = string.Format("{0:C}", double.Parse(txtImporte.Text, NumberStyles.Currency) * 0.02);
                }
                else
                {
                    txtFondo_g.Text = string.Format("{0:C}", 0);
                }

                aceptado = "S";
                Secuen = 1;

                txtliquido.Text = string.Format("{0:C}", double.Parse(txtImporte.Text, NumberStyles.Currency) - double.Parse(txtintereses.Text, NumberStyles.Currency) - double.Parse(txtFondo_g.Text, NumberStyles.Currency) - double.Parse(txtOtros_desc.Text, NumberStyles.Currency));
                dialogo = globales.MessageBoxQuestion("¿Se acepta el credito?", "Crédito", globales.menuPrincipal);
                this.boolDeducciones = false;
                if (dialogo == DialogResult.Yes)
                {
                    this.boolDeducciones = true;
                    globales.leftButton = true;
                    dialogo = globales.MessageBoxQuestion("¿Se modifico el plazo?", "Crédito", globales.menuPrincipal);
                    carta = (DialogResult.Yes == dialogo) ? "S" : "N";
                }

                //************ cálculo del primer descuento en relación al tipo de pago *************


                v_fecha = txtEmisionCheque.Text;
                string[] tmp1F = v_fecha.Split('/');
                DateTime auxF = new DateTime(Convert.ToInt32(tmp1F[2]), Convert.ToInt32(tmp1F[1]), Convert.ToInt32(tmp1F[0]));
                auxF = auxF.AddMonths(1);
                if (txtTipoPago.Text == "Q")
                {
                    auxF = new DateTime(auxF.Year, auxF.Month, 15);
                }
                else
                {
                    if (auxF.Month == 2)
                    {
                        auxF = new DateTime(auxF.Year, auxF.Month, 28);
                    }
                    else
                    {
                        auxF = new DateTime(auxF.Year, auxF.Month, 30);
                    }
                }


                f_primdesc = string.Format("{0:d}", auxF);
                txtF_primerdesc.Text = f_primdesc;

                if (string.IsNullOrWhiteSpace(txtRfc1.Text) && string.IsNullOrWhiteSpace(txtrfc2.Text))
                {
                    dialogo = globales.MessageBoxQuestion("La operación se efectuara sin un aval\n¿Desea agregar algún aval?", "Atención", globales.menuPrincipal);
                    if (dialogo == DialogResult.Yes)
                    {

                        this.ActiveControl = txtRfc1;
                    }
                    else
                    {
                        this.ActiveControl = btnGuardar;
                    }
                }
            }
            catch
            {
                globales.MessageBoxError("Error de calcular el liquido, favor de insertar todos los campos previos", "Aviso", globales.menuPrincipal);
            }
        }

        private void txtEmisionCheque_Leave(object sender, EventArgs e)
        {
            //calculoLiquido();
        }

        private void frmAltas_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);
        }

        public void cambiarTxtSueldoBase(string texto)
        {
            txtSueldoBase.Text = string.Format("{0:C}", texto);
        }

        private void imprimir(p_quirog obj, int checador = 4)
        {
            //Se necesita para sacar los meses de acuerdo al DataTime.............
            string[] meses = {
                "Enero",
                "Enero",
                "Febrero",
                "Marzo",
                "Abril",
                "Mayo",
                "Junio",
                "Julio",
                "Agosto",
                "Septiembre",
                "Octubre",
                "Noviembre",
                "Diciembre"
            };

            #region plazos del quirografario.....
            //Empieza el arreglo para sacar los plazoz del quirografario junto con su importe...
            string[] ar_fecharsve = new string[48];
            string[] ar_fechasca = new string[48];
            string fev = obj.f_primdesc.Replace(" ", "").Replace("'", "");
            try
            {
                fev = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(fev));
                int band = 15;
                for (int x = 0; x < obj.plazo; x++)
                {
                    fev = fev.Replace("'", "");
                    ar_fecharsve[x] = fev;
                    ar_fechasca[x] = string.Format("{0:C}", obj.imp_unit);
                    string[] split;
                    if (obj.tipo_pago == Convert.ToChar("M"))
                    {
                        split = fev.Split('/');
                        int año = Convert.ToInt32(split[2]);
                        int mes = Convert.ToInt32(split[1]);
                        DateTime fechaAux = new DateTime(año, mes, 1);
                        fechaAux = fechaAux.AddDays(35);
                        fev = string.Format("30/{1}/{0}", fechaAux.Year, fechaAux.Month < 10 ? "0" + fechaAux.Month.ToString() : fechaAux.Month.ToString());
                        split = fev.Split('/');
                        if (split[1] == "02")
                        {
                            fev = string.Format("28/02/{0}", split[2]);
                        }
                        else
                        {
                            fev = string.Format("30/{1}/{0}", split[2], split[1].Length == 2 ? split[1] : "0" + split[1]);
                        }
                    }
                    else
                    {
                        if (band == 15)
                        {
                            split = fev.Split('/');
                            if (split[1] == "02")
                            {
                                fev = string.Format("28/02/{0}", split[2]);
                            }
                            else
                            {
                                fev = string.Format("30/{1}/{0}", split[2], split[1].Length == 2 ? split[1] : "0" + split[1]);
                            }
                            band = 30;
                        }
                        else
                        {
                            split = fev.Split('/');
                            int año = Convert.ToInt32(split[2]);
                            int mes = Convert.ToInt32(split[1]);
                            DateTime fechaAux = new DateTime(año, mes, 1);
                            fechaAux = fechaAux.AddDays(35);
                            fev = string.Format("15/{1}/{0}", fechaAux.Year, fechaAux.Month < 10 ? "0" + fechaAux.Month.ToString() : fechaAux.Month.ToString());
                            band = 15;
                        }
                    }

                }
            }
            catch {
                fev = "";
            }







            #endregion


            #region Asignación de los reportes.........
            //desde

            //hasta
            string fecha;
            try
            {
                fecha = string.Format("{0:dd} de {1} del {0:yyyy}", DateTime.Parse(obj.f_solicitud),meses[DateTime.Parse(obj.f_solicitud).Month]);
            }
            catch {
                fecha = "";
            }

            fecha = string.Format("OAXACA DE JUÁREZ, OAX., {0}", fecha);
            
            string sql = "select * from datos.numletra('{0}')";
            string pasito = string.Format(sql, obj.importe.ToString(), true);
            List<Dictionary<string, object>> rs = globales.consulta(pasito);
            string letranum = Convert.ToString(rs[0]["numletra"]);
            string terminosYCondiciones = string.Format("Debo(emos) y Pagaré(mos) incondicionalmente por este pagaré mercantil, en esta plaza ( o en cualquier otro lugar a elección del" +
                " acreedor), a la orden de la \"OFICINA DE PENSIONES DEL ESTADO DE OAXACA\", él día: {0} la cantidad de ({1} {2}), valor recibido a mi entera sastifacción.", ar_fecharsve[Convert.ToInt32(obj.plazo) - 1], string.Format("{0:C}", obj.importe), globales.convertirNumerosLetras((string.Format("{0}", obj.importe)), true));
            var cnd = globales.justificar(terminosYCondiciones, 160);
            terminosYCondiciones = string.Empty;
            foreach (string aux1 in cnd)
            {
                terminosYCondiciones += aux1 + "\n";
            }
            terminosYCondiciones = terminosYCondiciones.Substring(0, terminosYCondiciones.Length - 1);
            object quir = null;
            object quir2 = null;

            string query = string.Format("select * from datos.d_perded where folio = {0}", obj.folio);

            List<Dictionary<string, object>> resultado = globales.consulta(query);

            string percepciones = string.Empty;
            string deducciones = string.Empty;

            string reduc1 = string.Empty; ;
            string porcentaje1 = string.Empty; ;

            string reduc2 = string.Empty; ;
            string porcentaje2 = string.Empty;

            if (resultado.Count != 0)
            {
                percepciones = Convert.ToDouble((resultado[0]["percepciones"])).ToString("#.##");
                deducciones = Convert.ToDouble(resultado[0]["deducciones"]).ToString("#.##");

                reduc1 = Convert.ToDouble(resultado[0]["deduc_rec1"]).ToString("#.##");
                porcentaje1 = Convert.ToDouble(resultado[0]["porcentaje1"]).ToString("#.##");

                reduc2 = Convert.ToDouble(resultado[0]["deduc_rec2"]).ToString("#.##");
                porcentaje2 = Convert.ToDouble(resultado[0]["porcentaje2"]).ToString("#.##");
            }

            percepciones = (string.IsNullOrWhiteSpace(percepciones)) ? "0" : percepciones;
            deducciones = (string.IsNullOrWhiteSpace(deducciones)) ? "0" : deducciones;

            reduc1 = (string.IsNullOrWhiteSpace(reduc1)) ? "0" : reduc1;
            porcentaje1 = (string.IsNullOrWhiteSpace(porcentaje1)) ? "0.00" : porcentaje1;

            reduc2 = (string.IsNullOrWhiteSpace(reduc2)) ? "0" : reduc2;
            porcentaje2 = (string.IsNullOrWhiteSpace(porcentaje2)) ? "0.00" : porcentaje2;

            obj.f_emischeq = obj.f_emischeq.Replace("'", "");
            if (!string.IsNullOrWhiteSpace(obj.f_emischeq))
            {
                if (obj.f_emischeq.Contains("-"))
                {
                    string[] ayx = obj.f_emischeq.Split('-');
                    obj.f_emischeq = string.Format("{0}/{1}/{2}", ayx[2], ayx[1], ayx[0]);
                }
            }

            if (obj.lista.Count == 1)
            {
                object[] quiro = {obj.folio,  fecha, obj.nombre_em, obj.rfc, obj.direccion, obj.proyecto, obj.descripcion, obj.telefono,obj.importe,obj.plazo, obj.tipo_pago,
                                  obj.interes,obj.fondo_g,obj.liquido,percepciones,deducciones,reduc1,porcentaje1,reduc2,porcentaje2,obj.sueldo_m,string.Format("{0}A {1}M {2}Q", obj.ant_a, obj.ant_m, obj.ant_q),obj.nue,obj.nap,obj.f_emischeq,obj.lista[0].nombre_em,
                                  obj.lista[0].direccion,obj.lista[0].rfc, obj.lista[0].proyecto,obj.lista[0].antig,obj.lista[0].nue,obj.lista[0].nap};
                object[] quiro2 = { obj.folio, obj.plazo, fecha, obj.importe, terminosYCondiciones, obj.nombre_em, obj.rfc, obj.proyecto, obj.nap, obj.direccion ,obj.lista[0].nombre_em,obj.lista[0].rfc,obj.lista[0].proyecto,obj.lista[0].nap,obj.lista[0].direccion,"","","","","", ar_fecharsve[0], ar_fecharsve[1] , ar_fecharsve[2] , ar_fecharsve[3] , ar_fecharsve[4] , ar_fecharsve[5],
                                    ar_fecharsve[6],ar_fecharsve[7],ar_fecharsve[8],ar_fecharsve[9],ar_fecharsve[10],ar_fecharsve[11],ar_fecharsve[12],ar_fecharsve[13],ar_fecharsve[14],ar_fecharsve[15],ar_fecharsve[16],ar_fecharsve[17],ar_fecharsve[18],ar_fecharsve[19],ar_fecharsve[20],ar_fecharsve[21],ar_fecharsve[22],
                                    ar_fecharsve[23],ar_fecharsve[24],ar_fecharsve[25],ar_fecharsve[26],ar_fecharsve[27],ar_fecharsve[28],ar_fecharsve[29],ar_fecharsve[30],ar_fecharsve[31],ar_fecharsve[32],ar_fecharsve[33],ar_fecharsve[34],ar_fecharsve[35],ar_fecharsve[36],ar_fecharsve[37],ar_fecharsve[38],ar_fecharsve[39],
                                    ar_fecharsve[40],ar_fecharsve[41],ar_fecharsve[42],ar_fecharsve[43],ar_fecharsve[44],ar_fecharsve[45],ar_fecharsve[46],ar_fecharsve[47],ar_fechasca[0],ar_fechasca[1],ar_fechasca[2],ar_fechasca[3],ar_fechasca[4],ar_fechasca[5],ar_fechasca[6],ar_fechasca[7],ar_fechasca[8],ar_fechasca[9],ar_fechasca[10],
                                    ar_fechasca[11],ar_fechasca[12],ar_fechasca[13],ar_fechasca[14],ar_fechasca[15],ar_fechasca[16],ar_fechasca[17],ar_fechasca[18],ar_fechasca[19],ar_fechasca[20],ar_fechasca[21],ar_fechasca[22],ar_fechasca[23],ar_fechasca[24],ar_fechasca[25],ar_fechasca[26],ar_fechasca[27],ar_fechasca[28],ar_fechasca[29],ar_fechasca[30],
                                    ar_fechasca[31],ar_fechasca[32],ar_fechasca[33],ar_fechasca[34],ar_fechasca[35],ar_fechasca[36],ar_fechasca[37],ar_fechasca[38],ar_fechasca[39],ar_fechasca[40],ar_fechasca[41],ar_fechasca[42],ar_fechasca[43],ar_fechasca[44],ar_fechasca[45],ar_fechasca[46],ar_fechasca[47]
                                    };
                quir = quiro;
                quir2 = quiro2;
            }
            else if (obj.lista.Count == 2)
            {
                object[] quiro = {obj.folio, fecha, obj.nombre_em, obj.rfc, obj.direccion, obj.proyecto, obj.descripcion, obj.telefono,obj.importe,obj.plazo, obj.tipo_pago,
                                  obj.interes,obj.fondo_g,obj.liquido,percepciones,deducciones,reduc1,porcentaje1,reduc2,porcentaje2,obj.sueldo_m,string.Format("{0}A {1}M {2}Q", obj.ant_a, obj.ant_m, obj.ant_q),obj.nue,obj.nap,obj.f_emischeq,obj.lista[0].nombre_em,obj.lista[0].direccion, obj.lista[0].rfc,
                                  obj.lista[0].proyecto,obj.lista[0].antig,obj.lista[0].nue,obj.lista[0].nap,obj.lista[1].nombre_em,obj.lista[1].direccion,obj.lista[1].rfc,obj.lista[1].proyecto,obj.lista[1].antig,obj.lista[1].nue,obj.lista[1].nap};
                object[] quiro2 = { obj.folio, obj.plazo, fecha, obj.importe, terminosYCondiciones, obj.nombre_em, obj.rfc, obj.proyecto, obj.nap, obj.direccion ,obj.lista[0].nombre_em,obj.lista[0].rfc,obj.lista[0].proyecto,obj.lista[0].nap,obj.lista[0].direccion,obj.lista[1].nombre_em,obj.lista[1].rfc,obj.lista[1].proyecto,obj.lista[1].nap,obj.lista[1].direccion, ar_fecharsve[0], ar_fecharsve[1] , ar_fecharsve[2] , ar_fecharsve[3] , ar_fecharsve[4] , ar_fecharsve[5],
                                    ar_fecharsve[6],ar_fecharsve[7],ar_fecharsve[8],ar_fecharsve[9],ar_fecharsve[10],ar_fecharsve[11],ar_fecharsve[12],ar_fecharsve[13],ar_fecharsve[14],ar_fecharsve[15],ar_fecharsve[16],ar_fecharsve[17],ar_fecharsve[18],ar_fecharsve[19],ar_fecharsve[20],ar_fecharsve[21],ar_fecharsve[22],
                                    ar_fecharsve[23],ar_fecharsve[24],ar_fecharsve[25],ar_fecharsve[26],ar_fecharsve[27],ar_fecharsve[28],ar_fecharsve[29],ar_fecharsve[30],ar_fecharsve[31],ar_fecharsve[32],ar_fecharsve[33],ar_fecharsve[34],ar_fecharsve[35],ar_fecharsve[36],ar_fecharsve[37],ar_fecharsve[38],ar_fecharsve[39],
                                    ar_fecharsve[40],ar_fecharsve[41],ar_fecharsve[42],ar_fecharsve[43],ar_fecharsve[44],ar_fecharsve[45],ar_fecharsve[46],ar_fecharsve[47],ar_fechasca[0],ar_fechasca[1],ar_fechasca[2],ar_fechasca[3],ar_fechasca[4],ar_fechasca[5],ar_fechasca[6],ar_fechasca[7],ar_fechasca[8],ar_fechasca[9],ar_fechasca[10],
                                    ar_fechasca[11],ar_fechasca[12],ar_fechasca[13],ar_fechasca[14],ar_fechasca[15],ar_fechasca[16],ar_fechasca[17],ar_fechasca[18],ar_fechasca[19],ar_fechasca[20],ar_fechasca[21],ar_fechasca[22],ar_fechasca[23],ar_fechasca[24],ar_fechasca[25],ar_fechasca[26],ar_fechasca[27],ar_fechasca[28],ar_fechasca[29],ar_fechasca[30],
                                    ar_fechasca[31],ar_fechasca[32],ar_fechasca[33],ar_fechasca[34],ar_fechasca[35],ar_fechasca[36],ar_fechasca[37],ar_fechasca[38],ar_fechasca[39],ar_fechasca[40],ar_fechasca[41],ar_fechasca[42],ar_fechasca[43],ar_fechasca[44],ar_fechasca[45],ar_fechasca[46],ar_fechasca[47]
                                    };
                quir = quiro;
                quir2 = quiro2;
            }
            else
            {
                object[] quiro = {obj.folio, fecha, obj.nombre_em, obj.rfc, obj.direccion, obj.proyecto, obj.descripcion, obj.telefono,obj.importe,obj.plazo, obj.tipo_pago,
                                  obj.interes,obj.fondo_g,obj.liquido,percepciones,deducciones,reduc1,porcentaje1,reduc2,porcentaje2,obj.sueldo_m,string.Format("{0}A {1}M {2}Q", obj.ant_a, obj.ant_m, obj.ant_q),obj.nue,obj.nap,obj.f_emischeq};

                object[] quiro2 = { obj.folio, obj.plazo, fecha, obj.importe, terminosYCondiciones, obj.nombre_em, obj.rfc, obj.proyecto, obj.nap, obj.direccion ,"","","","","","","","","","", ar_fecharsve[0], ar_fecharsve[1] , ar_fecharsve[2] , ar_fecharsve[3] , ar_fecharsve[4] , ar_fecharsve[5],
                                    ar_fecharsve[6],ar_fecharsve[7],ar_fecharsve[8],ar_fecharsve[9],ar_fecharsve[10],ar_fecharsve[11],ar_fecharsve[12],ar_fecharsve[13],ar_fecharsve[14],ar_fecharsve[15],ar_fecharsve[16],ar_fecharsve[17],ar_fecharsve[18],ar_fecharsve[19],ar_fecharsve[20],ar_fecharsve[21],ar_fecharsve[22],
                                    ar_fecharsve[23],ar_fecharsve[24],ar_fecharsve[25],ar_fecharsve[26],ar_fecharsve[27],ar_fecharsve[28],ar_fecharsve[29],ar_fecharsve[30],ar_fecharsve[31],ar_fecharsve[32],ar_fecharsve[33],ar_fecharsve[34],ar_fecharsve[35],ar_fecharsve[36],ar_fecharsve[37],ar_fecharsve[38],ar_fecharsve[39],
                                    ar_fecharsve[40],ar_fecharsve[41],ar_fecharsve[42],ar_fecharsve[43],ar_fecharsve[44],ar_fecharsve[45],ar_fecharsve[46],ar_fecharsve[47],ar_fechasca[0],ar_fechasca[1],ar_fechasca[2],ar_fechasca[3],ar_fechasca[4],ar_fechasca[5],ar_fechasca[6],ar_fechasca[7],ar_fechasca[8],ar_fechasca[9],ar_fechasca[10],
                                    ar_fechasca[11],ar_fechasca[12],ar_fechasca[13],ar_fechasca[14],ar_fechasca[15],ar_fechasca[16],ar_fechasca[17],ar_fechasca[18],ar_fechasca[19],ar_fechasca[20],ar_fechasca[21],ar_fechasca[22],ar_fechasca[23],ar_fechasca[24],ar_fechasca[25],ar_fechasca[26],ar_fechasca[27],ar_fechasca[28],ar_fechasca[29],ar_fechasca[30],
                                    ar_fechasca[31],ar_fechasca[32],ar_fechasca[33],ar_fechasca[34],ar_fechasca[35],ar_fechasca[36],ar_fechasca[37],ar_fechasca[38],ar_fechasca[39],ar_fechasca[40],ar_fechasca[41],ar_fechasca[42],ar_fechasca[43],ar_fechasca[44],ar_fechasca[45],ar_fechasca[46],ar_fechasca[47]
                                    };
                quir = quiro;
                quir2 = quiro2;
            }

            string txt1 = "Este pagaré si no fuera pagado en su vencimiento causará INTERESES a razón del 3% MENSUAL por todo el tiempo que dure insoluto y está sujeto a COBRO ANTICIPADO del saldo al no ser cubierto con PUNTUALIDAD cualquier abono especificado en las CONDICIONES, de acuerdo a los estipulado en la LEY GENERAL DE TÍTULOS Y OPERACIONES DE CRÉDITO. Así mismo acepto(amos) que en caso de baja definitiva de quien(es) sucribe(imos), al no ser cubierto el pago en las condiciones pactadas, sea el Fondo de Pensiones aportado el que se tome para tal fin, sin necesidad de trámite de mi(nuestra) parte. Del mismo modo, me comprometo a que en caso de licencia sin goce de sueldo, garantizaré previamente el pago oportuno de los importes establecidos en las CONDICIONES.";
            cnd = globales.justificar(txt1, 160);
            txt1 = string.Empty;
            foreach (string aux1 in cnd)
            {
                txt1 += aux1 + "\n";
            }
            txt1 = txt1.Substring(0, txt1.Length - 1);


            object[][] parametros1 = new object[2][];
            object[] header1 = { "texto1" };
            object[] body1 = { txt1 };
            parametros1[0] = header1;
            parametros1[1] = body1;


            string imagenlogo = globales.getImagen(globales.imagenesSispe.logoreportes);
            object[][] parameters = new object[2][];
            object[] herde = { "logo" };
            object[] bod = { imagenlogo };
            parameters[0] = herde;
            parameters[1] = bod;

            object[] objeto = { quir };
            object[] objeto2 = { quir2 };
            if (checador == 0) return;
            if (obj.porc > 50)
            {
                if (globales.MessageBoxQuestion(string.Format("Esta solicitud excede con {0}% ¿Desea imprimir?", obj.porc), "Atención", globales.menuPrincipal) == DialogResult.No)
                {
                    return;
                }
            }

            if (checador == 4 || checador == 5)
            {
                globales.reportes("p_quirogSolicitud", "p_quirog_solicitud", objeto, "", true, parameters);
                globales.reportes("reportePagareQuiro", "pagare_quirog", objeto2, "", true, parametros1);
                if (checador == 5)
                {
                    string pasa = "select * from datos.numletra('{0}')";
                    string letra = string.Format(pasa, obj.meses_corres.ToString(), true);
                    List<Dictionary<string, object>> lis = globales.consulta(letra);
                    string letras = Convert.ToString(lis[0]["numletra"]);
                    // string letras = globales.convertirNumerosLetras(obj.meses_corres.ToString(), true);
                    string texto = $"EL  QUE  SUSCRIBE  C. {obj.nombre_em}  RFC.  {obj.rfc}";

                    string f1 = string.Empty;

                    try
                    {
                        f1 = string.Format("{0:d}",DateTime.Parse(obj.f_solicitud));
                    }
                    catch {
                        f1 = "";
                    }

                    string mesesQ = (txtTipoPago.Text == "Q")? "QUINCENALES":"MENSUALES";

                    texto += $"CON DOMICILIO EN {obj.direccion}, ";
                    texto += "ME  DIRIJO  A  USTED  PARA  SOLICITAR  QUE  EL PRESTAMO TRAMITADO ";
                    texto += "ANTE  LA  OFICINA  QUE  USTED  REPRESENTA, SEA  AUTORIZADO POR LA ";
                    texto += $"CANTIDAD QUE CORRESPONDE A {obj.meses_corres} MESES DE  SUELDO  BASE, Y SEA ";
                    texto += $"APLICADO  EN  {obj.plazo}  DESCUENTOS  {mesesQ}   POR  ASI  CONVENIR  A ";
                    texto += $"MIS  INTERESES PERSONALES.";
                    texto += "AGRADEZCO  LA  ATENCION  PRESTADA Y  ESPERANDO  SU  APROBACION ME ";
                    texto += $"DESPIDO DE USTED.";


                    List<string> lstJustificar = globales.justificar(texto, 86);
                    texto = string.Empty;
                    foreach (string item in lstJustificar)
                    {
                        texto += item + "\n";
                    }
                    texto += $"\n\n\n\n\n\nADSCRIPCION: {obj.descripcion}\n\n\n";
                    texto += $"C. {obj.nombre_em}\n\n\nOAXACA DE JUAREZ, OAX., A {f1}.";

                    object[][] parametros = new object[2][];
                    object[] header = { "p1" };
                    object[] body = { texto };
                    parametros[0] = header;
                    parametros[1] = body;

                    globales.reportes("red_plazo", "p_quirog", new object[] { }, "", true, parametros);
                }
            }

            if (checador == 1)
            {
                globales.reportes("p_quirogSolicitud", "p_quirog_solicitud", objeto, "", true, parameters);
            }
            if (checador == 3)
            {
                globales.reportes("reportePagareQuiro", "pagare_quirog", objeto2, "", true,parametros1);
            }

            if (checador == 2)
            {
                string pasa = "select * from datos.numletra('{0}')";
                string letra = string.Format(pasa, obj.meses_corres.ToString(), true);
                List<Dictionary<string, object>> lis = globales.consulta(letra);
                string letras = Convert.ToString(lis[0]["numletra"]);
                // string letras = globales.convertirNumerosLetras(obj.meses_corres.ToString(), true);
                string texto = $"EL  QUE  SUSCRIBE  C. {obj.nombre_em}  RFC.  {obj.rfc}";
                string f1 = string.Empty;

                try
                {
                    f1 = string.Format("{0:d}", DateTime.Parse(obj.f_solicitud));
                }
                catch
                {
                    f1 = "";
                }
                
                texto += $"CON DOMICILIO EN {obj.direccion}, ";
                texto += "ME  DIRIJO  A  USTED  PARA  SOLICITAR  QUE  EL PRESTAMO TRAMITADO ";
                texto += "ANTE  LA  OFICINA  QUE  USTED  REPRESENTA, SEA  AUTORIZADO POR LA ";
                texto += $"CANTIDAD QUE CORRESPONDE A {obj.meses_corres} ({letras.Replace("00/100 PESOS M.N.", "")} MESES DE  SUELDO  BASE), Y SEA ";
                texto += $"CANTIDAD QUE CORRESPONDE A {obj.meses_corres} MESES DE  SUELDO  BASE, Y SEA ";
                texto += $"MIS  INTERESES PERSONALES.";
                texto += "AGRADEZCO  LA  ATENCION  PRESTADA Y  ESPERANDO  SU  APROBACION ME ";
                texto += $"DESPIDO DE USTED.";
                

                List<string> lstJustificar = globales.justificar(texto,86);
                texto = string.Empty;
                foreach (string item in lstJustificar) {
                    texto += item + "\n";
                }
                texto += $"\n\n\n\n\n\nADSCRIPCION: {obj.descripcion}\n\n\n";
                texto += $"C. {obj.nombre_em}\n\n\nOAXACA DE JUAREZ, OAX., A {f1}.";

                object[][] parametros = new object[2][];
                object[] header = { "p1" };
                object[] body = { texto };
                parametros[0] = header;
                parametros[1] = body;

                globales.reportes("red_plazo", "p_quirog", new object[] { }, "", true, parametros);
            }



            globales.MessageBoxInformation("Termino el proceso de impresión de Quirografarío", "Aviso",globales.menuPrincipal);
            #endregion


        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("hola desde todo", new Font("Arial", 40, FontStyle.Bold), Brushes.Black, 150, 125);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
            p_quirog.tablaConsultar = "p_quirog";
            p_quirog.enviar = rellenarModificarFolios;
            p_quirog.ShowDialog();
            if (!p_quirog.btnAceptarbool) return;

            
            frmImpresionQuirografario imp = new frmImpresionQuirografario(carta);
            imp.ShowDialog();
            int checador = imp.checador;
            tiposDeImpresion(checador);
        }
        private p_quirog verificarObjeto()
        {
            p_quirog obj = new p_quirog();
            obj.rfc = txtRfc.Text;
            obj.nombre_em = txtnombre_em.Text;
            obj.proyecto = txtProyecto.Text;
            obj.secretaria = txtSecretaria.Text;
            obj.antig_q = (string.IsNullOrWhiteSpace(txtAntQ.Text)) ? 0 : Convert.ToInt32(txtAntQ.Text);
            obj.sueldo_base = (string.IsNullOrWhiteSpace(txtSueldoBase.Text)) ? 0 : double.Parse(txtSueldoBase.Text, NumberStyles.Currency);
            obj.descripcion = txtAdscripcion.Text;
            obj.telefono = txtTelefono.Text;
            obj.extencion = txtExtencion.Text;
            obj.direccion = txtDomicilio.Text;
            obj.nue = txtNue.Text;
            obj.nap = (string.IsNullOrWhiteSpace(txtNap.Text)) ? 0 : Convert.ToDouble(txtNap.Text);
            obj.sueldo_m = (string.IsNullOrWhiteSpace(txtSueldo_m.Text)) ? 0 : double.Parse(txtSueldo_m.Text, NumberStyles.Currency);
            obj.ant_q = Convert.ToInt32(Ant_Q);
            obj.ant_m = Convert.ToInt32(Ant_M);
            obj.ant_a = Convert.ToInt32(Ant_A);
            obj.meses_corres = (string.IsNullOrWhiteSpace(txtmeses_corres.Text)) ? 0 : Convert.ToDouble(txtmeses_corres.Text);
            obj.otros_desc = (string.IsNullOrWhiteSpace(txtdesc.Text)) ? 0 : Convert.ToDouble(txtdesc.Text);
            obj.porc = (string.IsNullOrWhiteSpace(txtPorc.Text)) ? 0 : Convert.ToDouble(txtPorc.Text);
            obj.plazo = (string.IsNullOrWhiteSpace(txtplazo.Text)) ? 0 : Convert.ToDouble(txtplazo.Text);
            obj.tipo_pago = Convert.ToChar(txtTipoPago.Text);
            obj.trel = Convert.ToChar((string.IsNullOrWhiteSpace(txtTrl.Text) ? " " : txtTrl.Text));
            obj.f_emischeq = (string.IsNullOrWhiteSpace(txtEmisionCheque.Text)) ? "null" : txtEmisionCheque.Text;
            obj.f_primdesc = (string.IsNullOrWhiteSpace(txtF_primerdesc.Text)) ? "null" : txtF_primerdesc.Text;
            obj.f_ultmode = string.IsNullOrWhiteSpace(txtultpago.Text) ? "null" : txtultpago.Text;
            obj.imp_unit = double.Parse(txtImpUnit.Text, NumberStyles.Currency);
            obj.importe = (string.IsNullOrWhiteSpace(txtImporte.Text)) ? 0 : double.Parse(txtImporte.Text, NumberStyles.Currency);
            obj.interes = (string.IsNullOrWhiteSpace(txtintereses.Text)) ? 0 : double.Parse(txtintereses.Text, NumberStyles.Currency);
            obj.fondo_g = (string.IsNullOrWhiteSpace(txtFondo_g.Text)) ? 0 : double.Parse(txtFondo_g.Text, NumberStyles.Currency);
            obj.liquido = (string.IsNullOrWhiteSpace(txtliquido.Text)) ? 0 : double.Parse(txtliquido.Text, NumberStyles.Currency);
            obj.carta = (string.IsNullOrWhiteSpace(this.carta)) ? Convert.ToChar(" ") : Convert.ToChar(this.carta);
            obj.f_solicitud = this.fechaSolicitud;
            obj.aceptado = (string.IsNullOrWhiteSpace(this.aceptado)) ? Convert.ToChar(" ") : Convert.ToChar(this.aceptado);
            obj.secuen = this.Secuen;
            obj.tipo_rel = this.tipo_rel;
            obj.cve_categ = this.cve_categ;
            obj.sexo = this.sexo;

            if (obj.f_emischeq != "null")
            {
                string[] aux2 = obj.f_emischeq.Split('/');
                obj.f_emischeq = string.Format("'{0}-{1}-{2}'", aux2[2], aux2[1], aux2[0]);
            }
            if (obj.f_primdesc != "null")
            {
                string[] aux2 = obj.f_primdesc.Split('/');
                obj.f_primdesc = string.Format("'{0}-{1}-{2}'", aux2[2], aux2[1], aux2[0]);
            }

            if (obj.f_ultmode != "null")
            {
                string[] aux2 = obj.f_ultmode.Split('/');
                obj.f_ultmode = string.Format("'{0}-{1}-{2}'", aux2[2], aux2[1], aux2[0]);
            }
            return obj;
        }
        private void tiposDeImpresion(int checador)
        {
            p_quirog obj = verificarObjeto();
            obj.folio = Convert.ToInt32(txtFolio.Text);
            obj.lista = new List<d_quirog>();

            if (!string.IsNullOrWhiteSpace(txtRfc1.Text))
            {
                d_quirog detalleQuirog = new d_quirog();
                detalleQuirog.folio = obj.folio;
                detalleQuirog.rfc = txtRfc1.Text;
                detalleQuirog.nombre_em = txtNombre1.Text;
                detalleQuirog.direccion = txtdomicilio1.Text;
                detalleQuirog.proyecto = txtProyect1.Text;
                detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtNap1.Text)) ? 0 : Convert.ToDouble(txtNap1.Text);
                detalleQuirog.nue = txtNue1.Text;
                detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtAnti1.Text)) ? 0 : Convert.ToInt32(txtAnti1.Text);
                obj.lista.Add(detalleQuirog);
            }

            if (!string.IsNullOrWhiteSpace(txtrfc2.Text))
            {
                d_quirog detalleQuirog = new d_quirog();
                detalleQuirog.folio = obj.folio;
                detalleQuirog.rfc = txtrfc2.Text;
                detalleQuirog.nombre_em = txtnombre2.Text;
                detalleQuirog.direccion = txtdomicilio2.Text;
                detalleQuirog.proyecto = txtproy2.Text;
                detalleQuirog.nap = (string.IsNullOrWhiteSpace(txtnap2.Text)) ? 0 : Convert.ToDouble(txtnap2.Text);
                detalleQuirog.nue = txtnue2.Text;
                detalleQuirog.antig = (string.IsNullOrWhiteSpace(txtantg2.Text)) ? 0 : Convert.ToInt32(txtantg2.Text);
                obj.lista.Add(detalleQuirog);
            }
            this.Cursor = Cursors.WaitCursor;
            imprimir(obj, checador);
            this.Cursor = Cursors.Default;
        }

        private void txtSueldoBase_Leave(object sender, EventArgs e)
        {
            if (!boolsueldo_base) return;

            if (string.IsNullOrWhiteSpace(txtSueldoBase.Text) || ((txtSueldoBase.Text.Contains("$") || txtSueldoBase.Text.Contains(".")) && (txtSueldoBase.Text.Length == 1 || txtSueldoBase.Text.Length == 2)))
                txtSueldoBase.Text = string.Format("{0:C}", 0);
            else
            {
               txtSueldoBase.Text = string.Format("{0:C}", double.Parse(txtSueldoBase.Text, NumberStyles.Currency));

            }
            if (!string.IsNullOrWhiteSpace(txtSecretaria.Text))
            {
                rellenarCamposSecretarias(auxiliar);
                this.boolsueldo_base = false;
            }



        }

        private void txtSueldoBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);
        }

        private void txtSecretaria_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSecretaria.Text) || !boolSecretaria) return;


            string query = string.Format("select * from catalogos.cuentas where proy = '{0}'", txtSecretaria.Text);
            List<Dictionary<string, object>> a = globales.consulta(query);
            if (a.Count > 0)
            {
                boolRfc = false;
                auxiliar = a[0];
                rellenarCamposSecretarias(auxiliar, true);

            }
            else
            {
                auxiliar = new Dictionary<string, object>();
                globales.MessageBoxExclamation("No se encuentra SECRETARIA en el cátalogo", "Aviso", globales.menuPrincipal);
                limpiarSecretariaCampos();
                this.ActiveControl = txtSecretaria;
            }
            this.boolSecretaria = false;
        }

        private void txtRfc_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRfc.Text) || !boolRfc) return;


            string query = string.Format("select * from datos.empleados where rfc = '{0}'", txtRfc.Text);
            List<Dictionary<string, object>> listita = globales.consulta(query);
            if (listita.Count > 0)
            {
                boolRfc = false;
                rellenarCamposdeRFC(listita[0]);
            }
            else
            {

                globales.MessageBoxExclamation("No se encuentra RFC en el cátalogo", "Aviso", globales.menuPrincipal);
                limpiarCamposRFC();
                this.ActiveControl = txtRfc;

            }

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            frmEmpleados = new frmEmpleados();
            frmEmpleados.enviar = rellenarCamposdeRFC;
            frmEmpleados.ShowDialog();
            boolRfc = false;
            this.ActiveControl = txtProyecto;
        }

        private void txtRfc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                button1_Click_2(null, null);
            }
        }

        private void txtRfc_TextChanged_1(object sender, EventArgs e)
        {
            boolRfc = true;
        }

        private void btnBuscarRfc_MouseEnter(object sender, EventArgs e)
        {
            boolRfc = false;
        }

        private void btnSecretaria_Click(object sender, EventArgs e)
        {
            frmdependencias frm = new frmdependencias();
            frm.enviar = rellenarCamposSecretarias;
            frm.ShowDialog();
            this.ActiveControl = txtAntQ;
            boolSecretaria = false;
        }

        private void txtSecretaria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnSecretaria_Click(null, null);
            }
        }

        private void txtplazo_Leave(object sender, EventArgs e)
        {
            if (!tasaLaboral12) return;
            tasaLaboral12 = false;
            resultadoaux = (List<Dictionary<string, object>>)globales.seleccionaTasaDeInteres(fechaSolicitud);
            calculoLiquido();

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void txtdomicilio1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtdomicilio1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtrfc2.Focus();
        }

        private void txtdomicilio2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ActiveControl = btnGuardar;
        }

        private void txtRfc1_TextChanged(object sender, EventArgs e)
        {
            this.boolRfc1 = true;
        }

        private void btnRfc1_Click(object sender, EventArgs e)
        {
            frmEmpleados = new frmEmpleados();
            frmEmpleados.enviar = rellenarAval1;
            frmEmpleados.ShowDialog();
            boolRfc1 = false;
            this.ActiveControl = txtAnti1;
        }

        private void txtRfc1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.F1 == e.KeyCode)
                btnRfc1_Click(null, null);


        }

        private void txtRfc1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRfc1.Text) || !boolRfc1) return;


            string query = string.Format("select * from datos.empleados where rfc = '{0}'", txtRfc1.Text);
            List<Dictionary<string, object>> listita = globales.consulta(query);
            if (listita.Count > 0)
            {
                boolRfc1 = false;
                rellenarAval1(listita[0]);
            }
            else
            {

                globales.MessageBoxExclamation("No se encuentra RFC en el cátalogo", "Aviso", globales.menuPrincipal);
                txtRfc1.Text = "";
                txtProyect1.Text = "";
                txtNap1.Text = "";
                txtNombre1.Text = "";
                txtdomicilio1.Text = "";
                txtNue1.Text = "";
                txtAnti1.Text = "";
                this.ActiveControl = txtRfc1;

            }
        }

        private void btnRfc2_Click(object sender, EventArgs e)
        {
            this.frmEmpleados = new frmEmpleados();
            frmEmpleados.enviar = rellenarAval2;
            this.frmEmpleados.ShowDialog();
            boolRfc2 = false;
            this.ActiveControl = txtantg2;
        }

        private void txtrfc2_TextChanged(object sender, EventArgs e)
        {
            this.boolRfc2 = true;
        }

        private void txtrfc2_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.F1 == e.KeyCode)
                btnRfc2_Click(null, null);
        }

        private void txtrfc2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtrfc2.Text) || !boolRfc2) return;


            string query = string.Format("select * from datos.empleados where rfc = '{0}'", txtrfc2.Text);
            List<Dictionary<string, object>> listita = globales.consulta(query);
            if (listita.Count > 0)
            {
                boolRfc2 = false;
                rellenarAval2(listita[0]);
            }
            else
            {

                globales.MessageBoxExclamation("No se encuentra RFC en el cátalogo", "Aviso", globales.menuPrincipal);
                txtrfc2.Text = "";
                txtproy2.Text = "";
                txtnap2.Text = "";
                txtnombre2.Text = "";
                txtdomicilio2.Text = "";
                txtnue2.Text = "";
                txtantg2.Text = "";
                this.ActiveControl = txtrfc2;

            }
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            limpiarTodosCampos();
           
            txtEmisionCheque.Text = "";
            frmCatalogoP_quirog P_quirog = new frmCatalogoP_quirog();
            P_quirog.tablaConsultar = "p_quirog";
            P_quirog.enviar = rellenarModificarFolios;
            P_quirog.ShowDialog();
            this.ActiveControl = txtRfc;
            activarControlesBasicos();
            activarControl(txtFolio);
            btnFolio.Enabled = true;
            boolRfc = false;
            boolSecretaria = false;


            this.ActiveControl = txtRfc;
        }

        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                button1_Click_3(null, null);
            }
        }

        private void txtFolio_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFolio.Text) || !boolFolio) return;


            string query = string.Format("select * from datos.p_quirog where folio = {0}", txtFolio.Text);
            List<Dictionary<string, object>> listita = globales.consulta(query);
            if (listita.Count > 0)
            {
                boolFolio = false;
                query = string.Format("select * from datos.D_QUIROG where FOLIO = '{0}'", txtFolio.Text);
                List<Dictionary<string, object>> aux2 = null;
                aux2 = globales.consulta(query);
                limpiarTodosCampos();
                activarControlesBasicos();
                activarControl(txtFolio);
                txtEmisionCheque.Text = "";
                btnFolio.Enabled = true;
                rellenarModificarFolios(listita[0], aux2);
                this.ActiveControl = txtRfc;
                this.boolRfc = false;
                this.boolSecretaria = false;
                this.boolAnt1 = false;
                this.boolsueldo_base = false;
            }
            else
            {

                globales.MessageBoxExclamation("No se encuentra el FOLIO en el cátalogo", "Aviso", globales.menuPrincipal);
                limpiarCamposRFC();
                limpiarSecretariaCampos();
                limpiarLiquidoCampos();
                txtAntQ.Text = "0";
                limpiarAvales();
                txtTelefono.Text = "";
                txtExtencion.Text = "";
                txtdesc.Text = "0.00";


                Ant_Q = 0;
                Ant_M = 0;
                Ant_A = 0;
                carta = " ";
                aceptado = " ";
                Secuen = 0;
                this.ActiveControl = txtFolio;
                txtrfc2.Enabled = true;
                txtRfc1.Enabled = true;
            }
        }

        private void txtFolio_TextChanged_1(object sender, EventArgs e)
        {
            this.boolFolio = true;
        }

        private void txtplazo_Enter(object sender, EventArgs e)
        {
            tasaLaboral12 = true;
        }

        private void group_Enter(object sender, EventArgs e)
        {

        }

        private void frmAltas_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.F9 == e.KeyCode)
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "calc.exe";          
                process.StartInfo = startInfo;
                process.Start();
            }

            if (Keys.Escape == e.KeyCode)
                button4_Click(null, null);

            if (Keys.F2 == e.KeyCode)
                btnnuevo_Click(null, null);
            if (Keys.F3 == e.KeyCode)
                btnModifica_Click(null, null);
            if (Keys.F4 == e.KeyCode)
                button1_Click_1(null, null);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmAltas_Shown(object sender, EventArgs e)
        {
            button2.Focus();
        }

        private void txtSueldoBase_TextChanged(object sender, EventArgs e)
        {
            this.boolsueldo_base = true;
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            globales.showModal(new frmReporteSaldosEmisionCheque());
        }
    }
}
