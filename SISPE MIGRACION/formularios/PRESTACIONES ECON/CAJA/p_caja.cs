using SISPE_MIGRACION.codigo.baseDatos.repositorios;
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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.CAJA
{
    public partial class p_caja : Form
    {
        private bool esInsertar = false;
        private double numdesc = 0;
        private double descuentos = 0;
        private double delDescuento = 0;
        private double imp_unit_cap = 0;
        private double imp_unit = 0;
        private string imp_unit_capl = string.Empty;
        private string fum = string.Empty;
        private string hum = string.Empty;
        private string folioImprimir = string.Empty;
        private string fechaImprimir = string.Empty;
        private string imp_unit_intl = string.Empty;
        private double imp_unit_int = 0;

        private double total = 0;

        private string esHipote { get; set; }

        public p_caja(bool esHipote = false)
        {
            InitializeComponent();
            txtF_descuento.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtFolio.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtDescuentos.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtImp_unit.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtDelDescuento.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtPlazo.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtImp_unitCap.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtImp_unitIntereses.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            txtSecretaria.PreviewKeyDown += new PreviewKeyDownEventHandler(tabs);
            this.esHipote = esHipote ? "H" : "Q";
        }

        private void tabs(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas cerrar el modulo?", this);
            if (p == DialogResult.Yes)
                this.Owner.Close();
        }

        private void p_caja_Load(object sender, EventArgs e)
        {
            deshabilitar();
            txtTotal.Enabled = false;
            txtdescripcion.Enabled = false;
            txtNombre_em.Enabled = false;
            txtRfc.Enabled = false;

            txtF_descuento.Value = DateTime.Now;
            btnGuardar.Enabled = false;

            this.lblTitulo.Text = this.esHipote == "H" ? "HIPOTECARIO":"QUIROGRAFARIO";
        }

        private void deshabilitar(bool habilitar = false)
        {
            deshabilitarElemento(txtFolio, habilitar);
            deshabilitarElemento(txtF_descuento, habilitar);


            deshabilitarElemento(txtSecretaria, habilitar);

            deshabilitarElemento(txtDescuentos, habilitar);
            deshabilitarElemento(txtImp_unit, habilitar);
            deshabilitarElemento(txtDelDescuento, habilitar);
            deshabilitarElemento(txtPlazo, habilitar);
            deshabilitarElemento(txtImp_unitCap, habilitar);
            deshabilitarElemento(txtImp_unitIntereses, habilitar);
        }

        private void deshabilitarElemento(Control x, bool aux)
        {
            x.Enabled = aux;
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            try
            {
                llamarForma();
                this.ActiveControl = txtDescuentos;
                btnGuardar.Enabled = true;
                btnModifica.Enabled = false;
                btnNuevo.Enabled = false;
                this.esInsertar = true;
            }
            catch
            {

            }
        }

        public void rellenarcampos(Dictionary<string, object> datos)
        {
            limpiarCampos(true);

            string rfc = Convert.ToString(datos["rfc"]);
            string secretaria = Convert.ToString(datos["secretaria"]);
            string descripcion = Convert.ToString(datos["descripcion"]);
            string nombre = Convert.ToString(datos["nombre_em"]);
            string folio = Convert.ToString(datos["folio"]);
            string plazo = Convert.ToString(datos["plazo"]);
            string imp_unit = Convert.ToString(datos["imp_unit"]);

            txtFolio.Text = folio;
            txtRfc.Text = rfc;
            txtSecretaria.Text = secretaria;
            txtdescripcion.Text = descripcion;
            txtNombre_em.Text = nombre;
            txtPlazo.Text = plazo;
            txtImp_unit.Text = string.Format("{0:C}", Convert.ToDouble(imp_unit));

            txtDescuentos.Focus();
        }

        public void rellenarcampos2(Dictionary<string, object> datos)
        {
            limpiarCampos(true);

            string rfc = Convert.ToString(datos["rfc"]);
            string secretaria = Convert.ToString(datos["secretaria"]);
            string descripcion = Convert.ToString(datos["descripcion"]);
            string nombre = Convert.ToString(datos["nombre_em"]);
            string folio = Convert.ToString(datos["folio"]);
            string plazo = Convert.ToString(datos["plazo"]);
            string imp_unit = Convert.ToString(datos["imp_unit"]);
            this.fum = Convert.ToString(datos["fum"]);
            this.hum = Convert.ToString(datos["hum"]);

            txtFolio.Text = folio;
            txtRfc.Text = rfc;
            txtSecretaria.Text = secretaria;
            txtdescripcion.Text = descripcion;
            txtNombre_em.Text = nombre;
            txtPlazo.Text = plazo;
            txtImp_unit.Text = globales.checarDecimales(imp_unit);
            txtTotal.Text = string.Format("{0:C}", datos["total"]);
            txtLetra1.Text = $"({Convert.ToString(datos["imp_unit_capl"])})";
            txtLetra2.Text = "(" + Convert.ToString(datos["imp_unit_intl"]) + ")";
            txtDescuentos.Text = Convert.ToString(datos["descuentos"]);
            txtDelDescuento.Text = Convert.ToString(datos["deldesc"]);
            txtNumDesc.Text = Convert.ToString(datos["numdesc"]);
            txtImp_unitCap.Text = string.Format("{0:C}", datos["imp_unit_cap"]);
            txtImp_unitIntereses.Text = string.Format("{0:C}", datos["imp_unit_int"]);
        }

        private void limpiarCampos(bool limpiar)
        {
            deshabilitar(limpiar);
            txtFolio.Text = "";
            txtF_descuento.Text = "";
            txtTotal.Text = "";
            txtRfc.Text = "";
            txtNombre_em.Text = "";
            txtSecretaria.Text = "";
            txtdescripcion.Text = "";

            txtDescuentos.Text = "";
            txtImp_unit.Text = "";
            txtDelDescuento.Text = "";
            txtNumDesc.Text = "";
            txtPlazo.Text = "";

            txtImp_unitCap.Text = string.Format("{0:C}", 0);
            txtLetra1.Text = "";
            txtLetra2.Text = "";

            txtImp_unitIntereses.Text = string.Format("{0:C}", 0);
        }

        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void llamarForma()
        {
            if (this.esHipote == "Q")
            {
                modalQuirografario<p_edocta> cuenta = new modalQuirografario<p_edocta>();
                cuenta.enviar = rellenarcampos;
                globales.showModal(cuenta);
            }
            else {
                modalQuirografario<p_edocth> cuenta = new modalQuirografario<p_edocth>();
                cuenta.enviar = rellenarcampos;
                globales.showModal(cuenta);
            }
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            try
            {
                btnGuardar.Enabled = true;
                this.esInsertar = false;

                this.ActiveControl = txtDescuentos;
                btnGuardar.Enabled = true;
                btnModifica.Enabled = false;
                btnNuevo.Enabled = false;

                modalQuirografario<p_cajaq> cuenta = new modalQuirografario<p_cajaq>(this.esHipote);
                cuenta.enviar = rellenarcampos;
                globales.showModal(cuenta);

            }
            catch
            {

            }
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaciones()) return;
            p_cajaQ p = obtenerObjeto();
            if (this.esInsertar)
            {
                guardar(p);
            }
            else
            {
                modificar(p);
            }
        }

        private p_cajaQ obtenerObjeto()
        {
            p_cajaQ obj = new p_cajaQ();
            DateTime fecha = txtF_descuento.Value;
            string auxFecha = string.Format("{0}-{1}-{2}", fecha.Year, fecha.Month, fecha.Day);

            obj.folio = txtFolio.Text;
            obj.f_descuento = auxFecha;
            obj.rfc = txtRfc.Text;
            obj.nombre_em = txtNombre_em.Text;
            obj.secretaria = txtSecretaria.Text;
            obj.descripcion = txtdescripcion.Text;
            obj.descuentos = Convert.ToInt32(txtDescuentos.Text);
            obj.total = double.Parse(txtTotal.Text, NumberStyles.Currency);
            obj.deldescuentos = Convert.ToInt32(txtDelDescuento.Text);
            obj.numdesc = Convert.ToInt32(txtNumDesc.Text);
            obj.plazo = Convert.ToInt32(txtPlazo.Text);
            obj.imp_unit = double.Parse(txtImp_unit.Text, NumberStyles.Currency);
            obj.imp_unit_cap = double.Parse(txtImp_unitCap.Text, NumberStyles.Currency);
            obj.imp_unit_int = double.Parse(txtImp_unitIntereses.Text, NumberStyles.Currency);
            obj.imp_unit_intl = txtLetra2.Text;
            obj.imp_unit_capl = txtLetra1.Text;
            obj.fum = this.fum;
            obj.hum = this.hum;
            return obj;
        }


        private void modificar(p_cajaQ obj)
        {
            //Proceso para guardar datos.....

            DialogResult p = globales.MessageBoxQuestion("¿Desea actualizar los cambios?", "Aviso", this);
            if (p == DialogResult.No) return;


            string query = string.Format("update datos.p_cajaq set f_descuento = '{0}', rfc = '{1}',nombre_em = '{2}',secretaria = '{3}', descripcion = '{4}',descuentos = {5}, deldesc = {6},numdesc = {7}, plazo = {8}, imp_unit = {9}, imp_unit_cap = {10}, imp_unit_int = {11}, imp_unit_capl ='{12}', imp_unit_intl = '{13}', total = {14}, status = '{15}', fum = '{16}', hum = '{17}' where folio = {18} and t_prestamo = '{19}'",
                    obj.f_descuento, obj.rfc, obj.nombre_em, obj.secretaria, obj.descripcion, obj.descuentos, obj.deldescuentos, obj.numdesc, obj.plazo, obj.imp_unit, obj.imp_unit_cap, obj.imp_unit_int, obj.imp_unit_capl, obj.imp_unit_intl, obj.total, "T", string.Format("{0:yyyy-MM-dd}", DateTime.Parse(obj.fum)), obj.hum, obj.folio, this.esHipote);
            if (globales.consulta(query, true))
            {
                globales.MessageBoxSuccess("Registro actualizado existosamente!", "Aviso", this);
                limpiarCampos(false);
                btnGuardar.Enabled = false;
                btnNuevo.Enabled = true;
                btnModifica.Enabled = true;

                imprimir(obj);
            }



        }

        //        ;*** PAGOS POR CAJA***
        //;PROCESO QUE PERMITE EN LA EDICION DE RECIBOS QUIROGRAFARIOS
        //;O HIPOTECARIOS CALCULAR EL TOTAL Y EL IMPORTE EN LETRAS
        //; ADEMAS DEL PAGO FINAL DEL RECIBO.
        private void letr_recib(string nombre)
        {
            switch (nombre)
            {
                case "txtPlazo":
                    //this.numdesc = Convert.ToDouble(txtNumDesc.Text);
                    this.delDescuento = globales.convertDouble(txtDelDescuento.Text);
                    this.descuentos = globales.convertDouble(txtDescuentos.Text);
                    this.imp_unit = double.Parse(txtImp_unit.Text, NumberStyles.Currency);
                    if (this.numdesc != (this.delDescuento + (this.descuentos - 1)))
                    {
                        this.numdesc = this.delDescuento + (this.descuentos - 1);
                        this.imp_unit_cap = this.imp_unit * this.descuentos;
                        this.imp_unit_capl = "(" + globales.convertirNumerosLetras(this.imp_unit_cap.ToString(), true) + ")";
                        this.fum = string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        this.hum = string.Format("{0}:{1}", DateTime.Now.Hour, DateTime.Now.Minute);

                        txtImp_unitCap.Text = string.Format("{0:C}", this.imp_unit_cap);
                        txtNumDesc.Text = this.numdesc.ToString();
                        txtLetra1.Text = this.imp_unit_capl;
                        txtImp_unitIntereses.Text = string.Format("{0:C}", 0);
                    }
                    break;
                case "txtImp_unitIntereses":
                    this.imp_unit_int = (string.IsNullOrWhiteSpace(txtImp_unitCap.Text) ? 0 : double.Parse(txtImp_unitIntereses.Text, NumberStyles.Currency));
                    //this.total = Convert.ToDouble(txtTotal.Text);
                    this.imp_unit_intl = globales.convertirNumerosLetras(this.imp_unit_int.ToString(), true);
                    this.imp_unit_capl = globales.convertirNumerosLetras(double.Parse(txtImp_unitCap.Text, NumberStyles.Currency).ToString(), true);
                    this.total = this.imp_unit_cap + this.imp_unit_int;


                    txtTotal.Text = total.ToString();
                    break;
            }
        }


        private void guardar(p_cajaQ obj)
        {


            //Proceso para guardar datos.....
            DialogResult p = globales.MessageBoxQuestion("¿Desea guardar cambios?", "Aviso", this);
            if (p == DialogResult.No) return;



            string query = string.Empty;

            query = string.Format("insert into datos.p_cajaq values ({0},'{1}','{2}','{3}','{4}','{5}',{6},{7},{8},{9},{10},{11},{12},'{13}','{14}',{15},'{16}','{17}','{18}',null,'{19}')",
                obj.folio, obj.f_descuento, obj.rfc, obj.nombre_em, obj.secretaria, obj.descripcion, obj.descuentos, obj.deldescuentos, obj.numdesc, obj.plazo, obj.imp_unit, obj.imp_unit_cap, obj.imp_unit_int, obj.imp_unit_capl, obj.imp_unit_intl, obj.total, "T", obj.fum, obj.hum, this.esHipote);
            if (globales.consulta(query, true))
            {
                globales.MessageBoxSuccess("Registro insertado existosamente!", "Aviso", this);
                limpiarCampos(false);
                btnGuardar.Enabled = false;
                btnNuevo.Enabled = true;
                btnModifica.Enabled = true;

                imprimir(obj);
            }



        }

        private void imprimir(p_cajaQ obj)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Desea visualizar el reporte?", "Reporte", this);
            if (p == DialogResult.No) return;

            string[] meses = { "", "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };

            string total = globales.checarDecimales(obj.total);
            string nombre = string.Format("{0} ({1})", obj.nombre_em, obj.rfc);
            string descripcion = obj.descripcion;
            string query = globales.convertirNumerosLetras(Convert.ToString(total), true);
            string imp_unit_cap = string.Format("{0:C}", Convert.ToDouble(total)) + " (" + query+")";
            //string imp_unit_cap = string.Format("${0} ({1})",globales.checarDecimales(obj.imp_unit_cap),obj.imp_unit_capl);
            string texto1 = string.Format("POR CONCEPTO  DEL LOS NÚMERO{1} {2} AL {3}/{4} DE SU PRESTAMO QUIROGRAFARIO DE FOLIO {5} {6}", (obj.descuentos == 1) ? "DEL" : "DE LOS", (obj.descuentos == 1) ? "" : "S", obj.deldescuentos, obj.numdesc, obj.plazo, obj.folio, (obj.descuentos < 0) ? "MÁS" : "MENOS -");
            texto1 = this.esHipote == "H" ? texto1.Replace("QUIROGRAFARIO", "HIPOTECARIO"):texto1;
            string imp_unit_int = string.Format("${0} ({1})", obj.imp_unit_int, obj.imp_unit_intl);
            string moratorios = string.Format("{0}", (obj.descuentos < 0) ? "BONIFICADOS" : "MORATORIOS");
            string[] arreglo = new string[3];
            if (!string.IsNullOrWhiteSpace(obj.f_descuento))
            {
                obj.f_descuento = obj.f_descuento.Replace(" 12:00:00 a. m.", "");
                if (obj.f_descuento.Contains("/"))
                {
                    arreglo = obj.f_descuento.Split('/');
                }
                else
                {
                    arreglo = obj.f_descuento.Split('-');
                    string aux = arreglo[2];
                    arreglo[2] = arreglo[0];
                    arreglo[0] = aux;
                }
            }

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            string fecha = string.Format("OAXACA DE JUAREZ, OAX., {0} DE {1} DE {2}", arreglo[0], meses[Convert.ToInt32(arreglo[1])], arreglo[2]);
            string firma = "L.A.E PATRICIA CRUZ GOMEZ";
            string cargo = "JEFA DE DEPTO. DE PRESTACIONES ECONOMICAS";
            object[][] parametros = new object[2][];
            object[] header = { "total", "nombre", "descripcion", "impCap", "texto1", "imp_unit_int", "moratorios", "fecha", "firma", "cargo","logo" };
            object[] values = { total, nombre, descripcion, imp_unit_cap, texto1, imp_unit_int, moratorios, fecha, firma, cargo, logo };

            parametros[0] = header;
            parametros[1] = values;

            globales.reportes("reportePagoCajaQ", "p_marcha", new object[] { }, "", false, parametros);
        }

        private bool validaciones()
        {
            bool aux = false;
            if (string.IsNullOrWhiteSpace(txtDescuentos.Text))
            {
                globales.MessageBoxExclamation("Se debe ingresar la cantidad de pagos", "Aviso", this);
                txtDescuentos.Focus();
                return true;
            }

            if (string.IsNullOrWhiteSpace(txtImp_unit.Text))
            {
                globales.MessageBoxExclamation("Se debe ingresar el importe unitario", "Aviso", this);
                txtImp_unit.Focus();
                return true;
            }

            if (string.IsNullOrWhiteSpace(txtDelDescuento.Text))
            {
                globales.MessageBoxExclamation("Se debe ingresar el primer pago", "Aviso", this);
                txtDelDescuento.Focus();
                return true;
            }

            if (string.IsNullOrWhiteSpace(txtNumDesc.Text))
            {
                globales.MessageBoxExclamation("Se debe ingresar el número de descuento", "Aviso", this);
                txtNumDesc.Focus();
                return true;
            }

            if (string.IsNullOrWhiteSpace(txtPlazo.Text))
            {
                globales.MessageBoxExclamation("Se debe ingresar los plazos del prestamo", "Aviso", this);
                txtPlazo.Focus();
                return true;

            }

            if (string.IsNullOrWhiteSpace(txtImp_unitCap.Text))
            {
                globales.MessageBoxExclamation("Se debe ingresar el pago capital", "Aviso", this);
                txtImp_unitCap.Focus();
                return true;
            }
            return aux;
        }

        private void eventoEntrar(object sender, EventArgs e)
        {
            string nombre = ((TextBox)sender).Name;
            letr_recib(nombre);
        }

        private void txtImp_unitCap_Leave(object sender, EventArgs e)
        {
            letr_recib("txtImp_unitIntereses");
        }

        private void txtDelDescuento_TextChanged(object sender, EventArgs e)
        {

        }

        private void p_caja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                btnsalir_Click(null, null);
            if (e.KeyCode == Keys.F2)
                btnNuevo_Click_1(null, null);
            if (e.KeyCode == Keys.F3)
                btnModifica_Click(null, null);
            if (e.KeyCode == Keys.F4)
                btnGuardar_Click(null, null);
        }

        private void txtDescuentos_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !globales.numerico(e.KeyChar);
        }

        private void btnFolio_Click(object sender, EventArgs e)
        {
            frmCatalogoP_quirog cuenta = new frmCatalogoP_quirog();
            cuenta.enviar2 = rellenarcampos;
            cuenta.enviarBool = true;
            cuenta.tablaConsultar = "p_edocta";
            cuenta.ShowDialog();
        }

        private void txtLetra2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas cerrar el modulo?", this);
            if (p == DialogResult.Yes)
                this.Owner.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmCatalogoP_quirog cuenta = new frmCatalogoP_quirog();
            cuenta.enviar2 = eliminando;
            cuenta.tablaConsultar = "p_cajaq";
            cuenta.ShowDialog();
        }

        private void eliminando(Dictionary<string, object> resultado)
        {



            DialogResult p = globales.MessageBoxQuestion($"¿Desea eliminar el pago por caja con Folio N° {resultado["folio"]}\nFecha De Descuento: {resultado["f_descuento"]}\nHora de Modificación: {resultado["hum"]}", "Avisas", this);
            if (p == DialogResult.Yes)
            {
                int id = Convert.ToInt32(resultado["id"]);
                string query = $"delete from datos.p_cajaq where id = {id} and t_prestamo = '{esHipote}'";
                if (globales.consulta(query, true))
                {
                    globales.MessageBoxSuccess("Pago por caja eliminado correctamente", "Aviso", this);
                }
            }
        }

        private void txtImp_unitIntereses_Leave(object sender, EventArgs e)
        {
            double totalmora = globales.convertDouble(txtImp_unitIntereses.Text);
            txtImp_unitIntereses.Text = globales.convertMoneda(globales.convertDouble(txtImp_unitIntereses.Text));

            double total = globales.convertDouble(txtTotal.Text);

            total = (total) + (totalmora);

            txtTotal.Text = globales.convertMoneda(total);

          //  btnGuardar.Focus();
        }

        private void txtdescripcion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
