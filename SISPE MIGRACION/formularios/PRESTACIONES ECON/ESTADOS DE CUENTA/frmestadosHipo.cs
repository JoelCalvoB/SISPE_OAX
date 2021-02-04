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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA
{
    public partial class frmestadosHipo : Form
    {
        private bool teclaEnter;
        private int row;
        private int column;
        private bool editadoprogramadamente;
        private bool esInsertar;
        private string secretaria;
        private List<Dictionary<string, object>> parecidos;
        private int contadorGlobal = 0;

        private p_edocth estadoCuenta { get; set; }
        public frmestadosHipo()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ejecutarCatalogo();
        }

        private void ejecutarCatalogo()
        {
            modalQuirografario<p_edocth> quiro = new modalQuirografario<p_edocth>();
            quiro.enviar = rellenarDatos;
            globales.showModal(quiro);
        }

        private void rellenarDatos(Dictionary<string, object> obj)
        {
            limpiar();

            dbaseORM orm = new dbaseORM();
            estadoCuenta = orm.getObject<p_edocth>(obj);

            txtfolio.Text = Convert.ToString(estadoCuenta.folio);
            txtsecretaria.Text = estadoCuenta.secretaria;
            this.secretaria = estadoCuenta.secretaria;
            txtRfc.Text = estadoCuenta.rfc;
            txtnombre.Text = estadoCuenta.nombre_em;
            txtproyecto.Text = estadoCuenta.proyecto;
            txtdireccion.Text = estadoCuenta.direccion;
            txtTipo_pago.Text = estadoCuenta.tipo_pago;
            txtPlazo.Text = Convert.ToString(estadoCuenta.plazo);
            txtUbic_pagare.Text = estadoCuenta.ubic_pagare;
            txtimporte.Text = globales.convertMoneda(estadoCuenta.importe);
            txtF_Solicitud.Text = globales.parseDateTime(estadoCuenta.f_solicitud);
            txtF_emischeq.Text = globales.parseDateTime(estadoCuenta.f_emischeq);
            txtImp_unit.Text = globales.convertMoneda(estadoCuenta.imp_unit);
            txtInt_Morat.Text = globales.convertMoneda(estadoCuenta.int_morat);
            txtF_Convenio.Text = globales.parseDateTime(estadoCuenta.f_convenio);
            txtInt_Conv.Text = globales.convertMoneda(estadoCuenta.int_conv);
            txtConvenio.Text = estadoCuenta.convenio;
            DateTime fecha = estadoCuenta.fallecio;

            if (!string.IsNullOrWhiteSpace(Convert.ToString(fecha)) || Convert.ToString(fecha) != "null")
            {
                

                try
                {
                    fec_falle.Value = fecha;
                    checFallecio.Checked = true;
                    checFallecio.Visible = true;
                    fec_falle.Visible = true;
                }
                catch {
                    checFallecio.Checked = false;
                    fec_falle.Visible = false;
                }
            }
            else
            {
               
            }

            string query = $"select * from datos.descuentos where t_prestamo = 'H' and folio = {txtfolio.Text} order by f_descuento,numdesc";
            List<descuentos> descuentoHipotecario = orm.queryForList<descuentos>(query);

            descuentoHipotecario.ForEach(o => {
                dtggrid.Rows.Add(globales.parseDateTime(o.f_descuento),o.numdesc,o.totdesc,globales.convertMoneda(o.importe),o.rfc,o.cuenta,o.proyecto,o.tipo_rel,o.id,o.fum);
            });

            query = $"select * from datos.descuentos where t_prestamo = 'M' and folio = {txtfolio.Text} order by f_descuento,numdesc";
            List<descuentos> descuentoMoratorio = orm.queryForList<descuentos>(query);

            descuentoMoratorio.ForEach(o => {
                dtgridintereses.Rows.Add(globales.parseDateTime(o.f_descuento), o.numdesc, o.totdesc, globales.convertMoneda(o.importe), o.rfc, o.cuenta, o.proyecto, o.tipo_rel, o.id, o.fum);
            });


            if (!globales.boolConsulta) {
                habilitarBotones(true);
            }

            foreach (DataGridViewRow item in dtggrid.Rows)
            {
                double importe = string.IsNullOrWhiteSpace(Convert.ToString(item.Cells[3].Value)) ? 0 : Convert.ToDouble(item.Cells[3].Value);
                if (importe < 0)
                {

                    item.Cells[3].Style.BackColor = Color.FromArgb(180, 0, 0);
                    item.Cells[3].Style.ForeColor = Color.White;

                }
            }

            foreach (DataGridViewRow item in dtgridintereses.Rows)
            {
                double importe = string.IsNullOrWhiteSpace(Convert.ToString(item.Cells[3].Value)) ? 0 : Convert.ToDouble(item.Cells[3].Value);
                if (importe < 0)
                {

                    item.Cells[3].Style.BackColor = Color.FromArgb(180, 0, 0);
                    item.Cells[3].Style.ForeColor = Color.White;

                }
            }

            int index = txtnombre.Text.IndexOf('(');
            index = index == -1 ? txtnombre.Text.Length : index;

            string cons = $"select * from datos.p_edocth where nombre_em like '%{txtnombre.Text.Trim().Substring(0, index).Trim()}%'";
            this.parecidos = orm.query(cons);
            this.contadorGlobal = 0;
            foreach (Dictionary<string, object> item in this.parecidos)
            {
                if (Convert.ToString(item["folio"]) == txtfolio.Text)
                {
                    break;
                }
                contadorGlobal++;
            }

            checarbotones();

            Cursor = Cursors.Default;

        }

        private void checarbotones()
        {
            btn1.Enabled = (this.contadorGlobal == 0) ? false : true;
            btn2.Enabled = (this.contadorGlobal == this.parecidos.Count - 1) ? false : true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas actualizar el registro?", "Aviso", globales.menuPrincipal);
            if (dialogo == DialogResult.No) return;

            string fecha = string.Empty;

        

            estadoCuenta.tipo_pago = txtTipo_pago.Text;
            estadoCuenta.plazo = globales.convertDouble(txtPlazo.Text);
            estadoCuenta.ubic_pagare = txtUbic_pagare.Text;
            estadoCuenta.importe = globales.convertDouble(txtimporte.Text);
            estadoCuenta.f_solicitud = globales.convertDatetime(txtF_Solicitud.Text);
            estadoCuenta.f_emischeq = globales.convertDatetime(txtF_emischeq.Text);
            estadoCuenta.imp_unit = globales.convertDouble(txtImp_unit.Text);
            estadoCuenta.int_morat = globales.convertDouble(txtInt_Morat.Text);
            estadoCuenta.f_convenio = globales.convertDatetime(txtF_Convenio.Text);
            estadoCuenta.int_conv = globales.convertDouble(txtInt_Conv.Text);
            estadoCuenta.convenio = txtConvenio.Text;
            estadoCuenta.nombre_em = txtnombre.Text;
            estadoCuenta.fallecio = checFallecio.Checked? globales.convertDatetime(fec_falle.Text):new DateTime();
            
         //   string quer = $"update datos.p_edocth set fallecio={fecha} where folio={estadoCuenta.folio}";

            dbaseORM orm = new dbaseORM();
       //     globales.consulta(quer, true);
            bool resultado = orm.update<p_edocth>(estadoCuenta);
            if (resultado) globales.MessageBoxSuccess("Registro actualizado correctamente","Aviso",globales.menuPrincipal);

            habilitarBotones(false);
        }

        private void limpiar()
        {
            txtfolio.Text = "";
            txtsecretaria.Text = "";
            txtRfc.Text = "";
            txtnombre.Text = "";
            txtproyecto.Text = "";
            txtdireccion.Text = "";
            txtPlazo.Text = "";
            txtUbic_pagare.Text = "";
            txtTipo_pago.Text = "";
            txtimporte.Text = "";
            txtF_Solicitud.Text = "";
            txtF_emischeq.Text = "";
            txtImp_unit.Text = "";
            txtInt_Morat.Text = "";
            txtF_Convenio.Text = "";
            txtInt_Conv.Text = "";
            txtConvenio.Text = "";

            dtggrid.Rows.Clear();
            dtgridintereses.Rows.Clear();
        }

        public void habilitarBotones(bool habilitar) {
            txtTipo_pago.Enabled = habilitar;
            txtPlazo.Enabled = habilitar;
            txtUbic_pagare.Enabled = habilitar;
            txtimporte.Enabled = habilitar;
            txtF_Solicitud.Enabled = habilitar;
            txtF_emischeq.Enabled = habilitar;
            txtImp_unit.Enabled = habilitar;
            txtInt_Morat.Enabled = habilitar;
            txtF_Convenio.Enabled = habilitar;
            txtInt_Conv.Enabled = habilitar;
            txtConvenio.Enabled = habilitar;
            btnGuardar.Enabled = habilitar;
            checFallecio.Visible = habilitar;
          //  fec_falle.Visible = habilitar;
        }

        private void frmestadosHipo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) {
                ejecutarCatalogo();
            }

            if (e.KeyCode == Keys.F2)
                this.Close();

            if (e.KeyCode == Keys.F6)
            {

                try
                {
                    if (string.IsNullOrWhiteSpace(txtfolio.Text))
                    {
                        globales.MessageBoxExclamation("Seleccionar un folio para visualizar resultados", "Aviso", globales.menuPrincipal);
                        return;
                    }

                    string query1 = $"select sum(importe) as importe from datos.descuentos where t_prestamo = 'M' and folio = {txtfolio.Text} ";
                    List<Dictionary<string, object>> res = globales.consulta(query1);
                     string  importe = Convert.ToString(res[0]["importe"]);
                    if (importe=="")
                    {
                        importe = "0";
                    }
                    
                    string query = $"select sum(importe) as acumulado from datos.descuentos where t_prestamo = 'H' and folio = {txtfolio.Text} ";
                    double cantidad = 0;
                    string cantidadStr = Convert.ToString(globales.consulta(query)[0]["acumulado"]);

                    cantidad = globales.convertDouble(cantidadStr);
                    adicionalconsulta forma = new adicionalconsulta();
                    forma.txtMoratorios.Text = string.Format("{0:c}",Convert.ToDouble(importe));
                    forma.txttotal.Text = txtimporte.Text;
                    forma.txtacumulado.Text = string.Format("{0:C}",Convert.ToDouble (cantidad));
                    forma.txtsaldo.Text = string.Format("{0:C}", (double.Parse(txtimporte.Text, NumberStyles.Currency) - Convert.ToDouble(cantidad)));

                    globales.showModal(forma); ;



                }
                catch
                {
                    globales.MessageBoxExclamation("Es necesario guardar para visualizar resultados.\nDar clic en guardar o bien presionar F3", "Aviso", globales.menuPrincipal);
                }

            }

            if ((e.Control && e.KeyCode == Keys.Insert) && !globales.boolConsulta)
            {
                if (string.IsNullOrWhiteSpace(txtfolio.Text)) return;
                if (dtgridintereses.Rows.Count == 0)
                {
                    DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar intereses moratorios?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;
                    this.esInsertar = true;

                    string fecha = "";
                    if (secretaria == "P" || secretaria == "J" || secretaria == "T")
                    {
                        if (DateTime.Now.Month != 2)
                        {
                            fecha = string.Format("{0:d}", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30));
                        }
                        else
                        {
                            fecha = string.Format("{0:d}", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 28));
                        }
                    }
                    else
                    {
                        fecha = string.Format("{0:d}", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15));
                    }
                    string de = txtPlazo.Text;
                    string importe = "0.00";
                    string rfc = txtRfc.Text;
                    string folio = txtfolio.Text;


                    dtgridintereses.Rows.Insert(0);
                    dtgridintereses.Rows[0].Cells[0].Value = fecha;
                    dtgridintereses.Rows[0].Cells[1].Value = "0";
                    dtgridintereses.Rows[0].Cells[2].Value = de;
                    dtgridintereses.Rows[0].Cells[3].Value = importe;
                    dtgridintereses.Rows[0].Cells[4].Value = rfc;
                    dtgridintereses.Rows[0].Cells[5].Value = "";
                    dtgridintereses.Rows[0].Cells[6].Value = "";
                    dtgridintereses.Rows[0].Cells[7].Value = "";
                    dtgridintereses.Rows[0].Cells[8].Value = "";
                    dtgridintereses.Rows[0].Cells[9].Value = "mo";
                    dtgridintereses.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);
                    string query = "insert into datos.descuentos(folio,f_descuento,rfc,numdesc,totdesc,importe,cuenta,proyecto,tipo_rel,t_prestamo,f_registro,idusuario,hum,fum) values({0},'{1}','{2}',{3},{4},0,'','','','M','{5}',{6},'{7}','{8}') returning id";

                    query = string.Format(query, folio, string.Format("{0:yyyy-MM-dd}", DateTime.Parse(fecha)), rfc, 0, de, string.Format("{0:yyyy-MM-dd}", DateTime.Now), globales.id_usuario, string.Format("{0:hh:mm}", DateTime.Now), string.Format("{0:yyyy-MM-dd}", DateTime.Now));

                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    dtgridintereses.Rows[0].Cells[8].Value = resultado[0]["id"];
                    dtgridintereses.CurrentCell = dtgridintereses.Rows[0].Cells[0];
                    this.esInsertar = false;
                }
                return;
            }


            if (e.KeyCode == Keys.Insert && !globales.boolConsulta)
            {
                if (string.IsNullOrWhiteSpace(txtfolio.Text)) return;
                if (dtggrid.Rows.Count == 0)
                {
                    DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar una nuevo descuento?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;
                    this.esInsertar = true;

                    string fecha = "";
                    if (secretaria == "P" || secretaria == "J" || secretaria == "T")
                    {
                        if (DateTime.Now.Month != 2)
                        {
                            fecha = string.Format("{0:d}", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30));
                        }
                        else
                        {
                            fecha = string.Format("{0:d}", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 28));
                        }
                    }
                    else
                    {
                        fecha = string.Format("{0:d}", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15));
                    }
                    string de = txtPlazo.Text;
                    string importe = "0.00";
                    string rfc = txtRfc.Text;
                    string folio = txtfolio.Text;


                    dtggrid.Rows.Insert(0);
                    dtggrid.Rows[0].Cells[0].Value = fecha;
                    dtggrid.Rows[0].Cells[1].Value = "0";
                    dtggrid.Rows[0].Cells[2].Value = de;
                    dtggrid.Rows[0].Cells[3].Value = importe;
                    dtggrid.Rows[0].Cells[4].Value = rfc;
                    dtggrid.Rows[0].Cells[5].Value = "";
                    dtggrid.Rows[0].Cells[6].Value = "";
                    dtggrid.Rows[0].Cells[7].Value = "";
                    dtggrid.Rows[0].Cells[8].Value = "";
                    dtggrid.Rows[0].Cells[9].Value = "mo";
                    dtggrid.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);
                    string query = "insert into datos.descuentos(folio,f_descuento,rfc,numdesc,totdesc,importe,cuenta,proyecto,tipo_rel,t_prestamo,f_registro,idusuario,hum,fum) values({0},'{1}','{2}',{3},{4},0,'','','','H','{5}',{6},'{7}','{8}') returning id";

                    query = string.Format(query, folio, string.Format("{0:yyyy-MM-dd}", DateTime.Parse(fecha)), rfc, 0, de, string.Format("{0:yyyy-MM-dd}", DateTime.Now), globales.id_usuario, string.Format("{0:hh:mm}", DateTime.Now), string.Format("{0:yyyy-MM-dd}", DateTime.Now));

                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    dtggrid.Rows[0].Cells[8].Value = resultado[0]["id"];
                    dtggrid.CurrentCell = dtggrid.Rows[0].Cells[0];
                    this.esInsertar = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtggrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.teclaEnter)
            {
                var x = this.row + 1;
                var y = dtggrid.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                this.teclaEnter = false;
            }
        }

        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.row = e.RowIndex;
            this.column = e.ColumnIndex;
        }

        private void dtggrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (editadoprogramadamente)
            {
                editadoprogramadamente = false;
                return;
            }
            int c = e.RowIndex;
            if (c == -1) return;



            if (this.esInsertar)
            {
                return;
            }



            string f_descuento = string.Empty;
            int serie1 = 0;
            int hasta = 0;
            double importe = 0;

            try
            {
                dtggrid.Rows[c].Cells[0].Value = Convert.ToString(dtggrid.Rows[c].Cells[0].Value).Replace(".", "/");
                f_descuento = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(Convert.ToString(dtggrid.Rows[c].Cells[0].Value)));
            }
            catch
            {
                globales.MessageBoxError("Ingresar fecha con formato correcto:\ndd/mm/yyyy.", "Error en fecha", globales.menuPrincipal);
                globales.MessageBoxExclamation("Los registros no se actualizaran hasta que se ingrese la fecha de pago correctamente", "Aviso", globales.menuPrincipal);

                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[0].Value = "";
                f_descuento = "";
                return;
            }



            try
            {
                int dato = Convert.ToInt32(dtggrid.Rows[c].Cells[1].Value);
                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[1].Value = dato;
                serie1 = dato;
            }
            catch
            {
                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[1].Value = "0";
                serie1 = 0;
                return;
            }
            try
            {
                int dato = Convert.ToInt32(dtggrid.Rows[c].Cells[2].Value);
                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[2].Value = dato;
                hasta = dato;
            }
            catch
            {
                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[2].Value = "0";
                hasta = 0;
                return;
            }






            try
            {
                importe = double.Parse(Convert.ToString(dtggrid.Rows[c].Cells[3].Value), System.Globalization.NumberStyles.Currency);
                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[3].Value = string.Format("{0:C}", importe).Replace("$", "");

            }
            catch
            {
                globales.MessageBoxError("Ingresar el importe en el formato correcto:\n$0.00", "Error entrada", globales.menuPrincipal);
                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[3].Value = string.Format("{0:C}", 0).Replace("$", "");
                importe = 0;
            }





            if (string.IsNullOrWhiteSpace(f_descuento))
            {
                globales.MessageBoxExclamation("Para actualizar información ingresar la fecha de inicio correctamente", "Aviso", globales.menuPrincipal);
                return;
            }





            string rfc = Convert.ToString(dtggrid.Rows[c].Cells[4].Value).ToUpper();
            string dependencia = Convert.ToString(dtggrid.Rows[c].Cells[5].Value).ToUpper();
            string proyecto = Convert.ToString(dtggrid.Rows[c].Cells[6].Value).ToUpper();
            string tipo_relacion = Convert.ToString(dtggrid.Rows[c].Cells[7].Value).ToUpper();
            string id = Convert.ToString(dtggrid.Rows[c].Cells[8].Value);


            dtggrid.Rows[c].Cells[4].Value = rfc;
            dtggrid.Rows[c].Cells[5].Value = dependencia;
            dtggrid.Rows[c].Cells[6].Value = proyecto;
            dtggrid.Rows[c].Cells[7].Value = tipo_relacion;


            string fum = Convert.ToString(dtggrid.Rows[c].Cells[9].Value);
            if (string.IsNullOrWhiteSpace(fum))
            {
                globales.MessageBoxExclamation("No se puede modificar descuento ingresado por disco", "Aviso", globales.menuPrincipal);
                string consult = "select * from datos.descuentos where id = {0}";
                consult = string.Format(consult, dtggrid.Rows[c].Cells[8].Value);

                return;
            }


            if (importe < 0)
            {

                dtggrid.Rows[c].Cells[3].Style.BackColor = Color.FromArgb(180, 0, 0);
                dtggrid.Rows[c].Cells[3].Style.ForeColor = Color.White;

            }
            else
            {
                dtggrid.Rows[c].Cells[3].Style.BackColor = dtggrid.Rows[c].Cells[2].Style.BackColor;

                dtggrid.Rows[c].Cells[3].Style.ForeColor = dtggrid.Rows[c].Cells[2].Style.ForeColor;
            }


            string query = $"update datos.descuentos set f_descuento = '{f_descuento}',rfc = '{rfc}',numdesc = {serie1},totdesc={hasta},importe={importe},cuenta = '{dependencia}',proyecto = '{proyecto}',tipo_rel='{tipo_relacion}',idusuario={globales.id_usuario},fum='{string.Format("{0:yyyy-MM-dd}", DateTime.Now)}',hum='{string.Format("{0:hh:mm}", DateTime.Now)}' where  id = {id} and folio = {txtfolio.Text} and t_prestamo = 'H'";
            globales.consulta(query, true);
            editadoprogramadamente = false;
        }

        private void dtggrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion2);
        }

        private void viendoEdicion2(object sender, PreviewKeyDownEventArgs e)
        {
            this.teclaEnter = e.KeyCode == Keys.Enter;
        }

        private void dtggrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (dtggrid.Rows.Count == 0) return;

            try
            {
                int rowactual = dtggrid.Rows.Count;
                if (e.KeyCode == Keys.Insert && !globales.boolConsulta)
                {
                    DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar una nuevo descuento?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;
                    this.esInsertar = true;

                    string fecha = "";
                    if (secretaria == "P" || secretaria == "J" || secretaria == "T")
                    {
                        if (DateTime.Now.Month != 2)
                        {
                            fecha = string.Format("{0:d}", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30));
                        }
                        else
                        {
                            fecha = string.Format("{0:d}", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 28));
                        }
                    }
                    else
                    {
                        fecha = string.Format("{0:d}", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15));
                    }
                    string de = Convert.ToString(dtggrid.Rows[row].Cells[2].Value);
                    string importe = "0.00";
                    string rfc = Convert.ToString(dtggrid.Rows[row].Cells[4].Value);
                    string folio = txtfolio.Text;


                    dtggrid.Rows.Insert(rowactual);
                    dtggrid.Rows[rowactual].Cells[0].Value = fecha;
                    dtggrid.Rows[rowactual].Cells[1].Value = "0";
                    dtggrid.Rows[rowactual].Cells[2].Value = de;
                    dtggrid.Rows[rowactual].Cells[3].Value = importe;
                    dtggrid.Rows[rowactual].Cells[4].Value = rfc;
                    dtggrid.Rows[rowactual].Cells[5].Value = "";
                    dtggrid.Rows[rowactual].Cells[6].Value = "";
                    dtggrid.Rows[rowactual].Cells[7].Value = "";
                    dtggrid.Rows[rowactual].Cells[8].Value = "";
                    dtggrid.Rows[rowactual].Cells[9].Value = "modifica";
                    dtggrid.Rows[rowactual].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);
                    string query = "insert into datos.descuentos(folio,f_descuento,rfc,numdesc,totdesc,importe,cuenta,proyecto,tipo_rel,t_prestamo,hum,idusuario,fum,f_registro) values({0},'{1}','{2}',{3},{4},0,'','','','H','{5}',{6},'{7}','{8}') returning id";

                    query = string.Format(query, folio, string.Format("{0:yyyy-MM-dd}", DateTime.Parse(fecha)), rfc, 0, de, string.Format("{0:hh:mm}", DateTime.Now), globales.id_usuario, string.Format("{0:yyyy-MM-dd}", DateTime.Now), string.Format("{0:yyyy-MM-dd}", DateTime.Now));
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    dtggrid.Rows[rowactual].Cells[8].Value = resultado[0]["id"];
                    dtggrid.CurrentCell = dtggrid.Rows[rowactual].Cells[0];
                }

                if (e.KeyCode == Keys.Delete && !globales.boolConsulta)
                {
                    DialogResult p = globales.MessageBoxQuestion("¿Desea eliminar el descuento?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;

                    string fum = dtggrid.Rows[row].Cells[9].Value.ToString();
                    if (string.IsNullOrWhiteSpace(fum))
                    {
                        globales.MessageBoxExclamation("No se puede eliminar un registro ingresado por disco", "Aviso", globales.menuPrincipal);
                        return;
                    }

                    string id = dtggrid.Rows[row].Cells[8].Value.ToString();
                    dtggrid.Rows.RemoveAt(row);
                    string query = "delete from datos.descuentos where folio = {0} and id = {1} and t_prestamo = 'H'";
                    query = string.Format(query, txtfolio.Text, id);
                    if (globales.consulta(query, true))
                    {
                        globales.MessageBoxSuccess("Descuento eliminado correctamente", "Aviso", globales.menuPrincipal);
                    }
                }

                if (e.KeyCode == Keys.F8 && e.Control)
                {
                    string id = dtggrid.Rows[row].Cells[8].Value.ToString();
                    string query = $"select fum,f_registro,idusuario from datos.descuentos where id = {id} and t_prestamo = 'H'";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    string fecha = Convert.ToString(resultado[0]["fum"]).Replace(" 12:00:00 a. m.", "");
                    string fecharegistro = Convert.ToString(resultado[0]["f_registro"]).Replace(" 12:00:00 a. m.", "");
                    if (string.IsNullOrWhiteSpace(fecha))
                    {
                        globales.MessageBoxInformation($"Fecha de registro: {fecharegistro}", "Aviso", globales.menuPrincipal);
                    }
                    else
                    {
                        string iduser = Convert.ToString(resultado[0]["idusuario"]);
                        if (!string.IsNullOrWhiteSpace(iduser))
                        {
                            List<Dictionary<string, object>> r = globales.consulta($"select usuario from catalogos.usuarios where idusuario = {iduser}");
                            if (r.Count != 0)
                            {
                                string usuario = Convert.ToString(r[0]["usuario"]);
                                globales.MessageBoxInformation($"Fecha de registro: {fecharegistro} \nUltima modificación:{fecha}\nUsuario: {usuario}", "Aviso", globales.menuPrincipal);
                            }
                            else
                            {
                                globales.MessageBoxInformation($"Fecha de registro: {fecharegistro} \nUltima modificación:{fecha}", "Aviso", globales.menuPrincipal);
                            }
                        }
                        else
                        {
                            globales.MessageBoxInformation($"Fecha de registro: {fecharegistro} \nUltima modificación:{fecha}", "Aviso", globales.menuPrincipal);
                        }
                    }
                }

                if (e.KeyCode == Keys.F1)
                {
                    if (column == 5)
                    {
                        frmdependencias dependencia = new frmdependencias();
                        dependencia.enviar = rellenando;
                        dependencia.ShowDialog();
                        return;
                    }
                }

                if (e.Control && e.KeyCode == Keys.D)
                {
                    if (this.row > 1)
                    {
                        dtggrid.Rows[row].Cells[this.column].Value = dtggrid.Rows[row - 1].Cells[this.column].Value;
                    }
                }
            }
            catch
            {

            }

            this.esInsertar = false;

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                try
                {
                    e.SuppressKeyPress = true;
                    int iColumn = dtggrid.CurrentCell.ColumnIndex;
                    int iRow = dtggrid.CurrentCell.RowIndex;
                    if (iColumn == dtggrid.ColumnCount - 1)
                    {
                        if (dtggrid.RowCount > (iRow + 1))
                        {
                            dtggrid.CurrentCell = dtggrid[1, iRow + 1];
                        }
                        else
                        {
                            //focus next control
                        }
                    }
                    else
                        dtggrid.CurrentCell = dtggrid[iColumn + 1, iRow];
                }
                catch
                {

                }
            }
        }

        private void rellenando(Dictionary<string, object> datos, bool externo)
        {
            string cuenta = Convert.ToString(datos["cuenta"]);
            this.dtggrid.Rows[this.row].Cells[this.column].Value = cuenta;
        }

        private void dtgridintereses_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.row = e.RowIndex;
            this.column = e.ColumnIndex;
        }

        private void dtgridintereses_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.teclaEnter)
            {
                var x = this.row + 1;
                var y = dtgridintereses.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                this.teclaEnter = false;
            }
        }

        private void dtgridintereses_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (editadoprogramadamente)
            {
                editadoprogramadamente = false;
                return;
            }
            int c = e.RowIndex;
            if (c == -1) return;



            if (this.esInsertar)
            {
                return;
            }



            string f_descuento = string.Empty;
            int serie1 = 0;
            int hasta = 0;
            double importe = 0;

            try
            {
                dtgridintereses.Rows[c].Cells[0].Value = Convert.ToString(dtgridintereses.Rows[c].Cells[0].Value).Replace(".", "/");
                f_descuento = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(Convert.ToString(dtgridintereses.Rows[c].Cells[0].Value)));
            }
            catch
            {
                globales.MessageBoxError("Ingresar fecha con formato correcto:\ndd/mm/yyyy.", "Error en fecha", globales.menuPrincipal);
                globales.MessageBoxExclamation("Los registros no se actualizaran hasta que se ingrese la fecha de pago correctamente", "Aviso", globales.menuPrincipal);

                editadoprogramadamente = true;
                dtgridintereses.Rows[c].Cells[0].Value = "";
                f_descuento = "";
                return;
            }



            try
            {
                int dato = Convert.ToInt32(dtgridintereses.Rows[c].Cells[1].Value);
                editadoprogramadamente = true;
                dtgridintereses.Rows[c].Cells[1].Value = dato;
                serie1 = dato;
            }
            catch
            {
                editadoprogramadamente = true;
                dtgridintereses.Rows[c].Cells[1].Value = "0";
                serie1 = 0;
                return;
            }
            try
            {
                int dato = Convert.ToInt32(dtgridintereses.Rows[c].Cells[2].Value);
                editadoprogramadamente = true;
                dtgridintereses.Rows[c].Cells[2].Value = dato;
                hasta = dato;
            }
            catch
            {
                editadoprogramadamente = true;
                dtgridintereses.Rows[c].Cells[2].Value = "0";
                hasta = 0;
                return;
            }






            try
            {
                importe = double.Parse(Convert.ToString(dtgridintereses.Rows[c].Cells[3].Value), System.Globalization.NumberStyles.Currency);
                editadoprogramadamente = true;
                dtgridintereses.Rows[c].Cells[3].Value = string.Format("{0:C}", importe).Replace("$", "");

            }
            catch
            {
                globales.MessageBoxError("Ingresar el importe en el formato correcto:\n$0.00", "Error entrada", globales.menuPrincipal);
                editadoprogramadamente = true;
                dtgridintereses.Rows[c].Cells[3].Value = string.Format("{0:C}", 0).Replace("$", "");
                importe = 0;
            }





            if (string.IsNullOrWhiteSpace(f_descuento))
            {
                globales.MessageBoxExclamation("Para actualizar información ingresar la fecha de inicio correctamente", "Aviso", globales.menuPrincipal);
                return;
            }





            string rfc = Convert.ToString(dtgridintereses.Rows[c].Cells[4].Value).ToUpper();
            string dependencia = Convert.ToString(dtgridintereses.Rows[c].Cells[5].Value).ToUpper();
            string proyecto = Convert.ToString(dtgridintereses.Rows[c].Cells[6].Value).ToUpper();
            string tipo_relacion = Convert.ToString(dtgridintereses.Rows[c].Cells[7].Value).ToUpper();
            string id = Convert.ToString(dtgridintereses.Rows[c].Cells[8].Value);


            dtgridintereses.Rows[c].Cells[4].Value = rfc;
            dtgridintereses.Rows[c].Cells[5].Value = dependencia;
            dtgridintereses.Rows[c].Cells[6].Value = proyecto;
            dtgridintereses.Rows[c].Cells[7].Value = tipo_relacion;


            string fum = Convert.ToString(dtgridintereses.Rows[c].Cells[9].Value);
            if (string.IsNullOrWhiteSpace(fum))
            {
                globales.MessageBoxExclamation("No se puede modificar descuento ingresado por disco", "Aviso", globales.menuPrincipal);
                string consult = "select * from datos.descuentos where id = {0}";
                consult = string.Format(consult, dtgridintereses.Rows[c].Cells[8].Value);

                return;
            }


            if (importe < 0)
            {

                dtgridintereses.Rows[c].Cells[3].Style.BackColor = Color.FromArgb(180, 0, 0);
                dtgridintereses.Rows[c].Cells[3].Style.ForeColor = Color.White;

            }
            else
            {
                dtgridintereses.Rows[c].Cells[3].Style.BackColor = dtgridintereses.Rows[c].Cells[2].Style.BackColor;

                dtgridintereses.Rows[c].Cells[3].Style.ForeColor = dtgridintereses.Rows[c].Cells[2].Style.ForeColor;
            }


            string query = $"update datos.descuentos set f_descuento = '{f_descuento}',rfc = '{rfc}',numdesc = {serie1},totdesc={hasta},importe={importe},cuenta = '{dependencia}',proyecto = '{proyecto}',tipo_rel='{tipo_relacion}',idusuario={globales.id_usuario},fum='{string.Format("{0:yyyy-MM-dd}", DateTime.Now)}',hum='{string.Format("{0:hh:mm}", DateTime.Now)}' where  id = {id} and folio = {txtfolio.Text} and t_prestamo = 'M'";
            globales.consulta(query, true);
            editadoprogramadamente = false;
        }

        private void dtgridintereses_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion2);
        }

        private void dtgridintereses_KeyDown(object sender, KeyEventArgs e)
        {
            if (dtgridintereses.Rows.Count == 0) return;

            try
            {
                int rowactual = dtgridintereses.Rows.Count;
                if (e.KeyCode == Keys.Insert && !globales.boolConsulta)
                {
                    DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar interes moratorio?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;
                    this.esInsertar = true;

                    string fecha = "";
                    if (secretaria == "P" || secretaria == "J" || secretaria == "T")
                    {
                        if (DateTime.Now.Month != 2)
                        {
                            fecha = string.Format("{0:d}", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30));
                        }
                        else
                        {
                            fecha = string.Format("{0:d}", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 28));
                        }
                    }
                    else
                    {
                        fecha = string.Format("{0:d}", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15));
                    }
                    string de = Convert.ToString(dtgridintereses.Rows[row].Cells[2].Value);
                    string importe = "0.00";
                    string rfc = Convert.ToString(dtgridintereses.Rows[row].Cells[4].Value);
                    string folio = txtfolio.Text;


                    dtgridintereses.Rows.Insert(rowactual);
                    dtgridintereses.Rows[rowactual].Cells[0].Value = fecha;
                    dtgridintereses.Rows[rowactual].Cells[1].Value = "0";
                    dtgridintereses.Rows[rowactual].Cells[2].Value = de;
                    dtgridintereses.Rows[rowactual].Cells[3].Value = importe;
                    dtgridintereses.Rows[rowactual].Cells[4].Value = rfc;
                    dtgridintereses.Rows[rowactual].Cells[5].Value = "";
                    dtgridintereses.Rows[rowactual].Cells[6].Value = "";
                    dtgridintereses.Rows[rowactual].Cells[7].Value = "";
                    dtgridintereses.Rows[rowactual].Cells[8].Value = "";
                    dtgridintereses.Rows[rowactual].Cells[9].Value = "modifica";
                    dtgridintereses.Rows[rowactual].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);
                    string query = "insert into datos.descuentos(folio,f_descuento,rfc,numdesc,totdesc,importe,cuenta,proyecto,tipo_rel,t_prestamo,hum,idusuario,fum,f_registro) values({0},'{1}','{2}',{3},{4},0,'','','','M','{5}',{6},'{7}','{8}') returning id";

                    query = string.Format(query, folio, string.Format("{0:yyyy-MM-dd}", DateTime.Parse(fecha)), rfc, 0, de, string.Format("{0:hh:mm}", DateTime.Now), globales.id_usuario, string.Format("{0:yyyy-MM-dd}", DateTime.Now), string.Format("{0:yyyy-MM-dd}", DateTime.Now));
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    dtgridintereses.Rows[rowactual].Cells[8].Value = resultado[0]["id"];
                    dtgridintereses.CurrentCell = dtgridintereses.Rows[rowactual].Cells[0];
                }

                if (e.KeyCode == Keys.Delete && !globales.boolConsulta)
                {
                    DialogResult p = globales.MessageBoxQuestion("¿Desea eliminar el descuento?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;

                    string fum = dtgridintereses.Rows[row].Cells[9].Value.ToString();
                    if (string.IsNullOrWhiteSpace(fum))
                    {
                        globales.MessageBoxExclamation("No se puede eliminar un registro ingresado por disco", "Aviso", globales.menuPrincipal);
                        return;
                    }

                    string id = dtgridintereses.Rows[row].Cells[8].Value.ToString();
                    dtgridintereses.Rows.RemoveAt(row);
                    string query = "delete from datos.descuentos where folio = {0} and id = {1} and t_prestamo = 'M'";
                    query = string.Format(query, txtfolio.Text, id);
                    if (globales.consulta(query, true))
                    {
                        globales.MessageBoxSuccess("Descuento eliminado correctamente", "Aviso", globales.menuPrincipal);
                    }
                }

                if (e.KeyCode == Keys.F8 && e.Control)
                {
                    string id = dtgridintereses.Rows[row].Cells[8].Value.ToString();
                    string query = $"select fum,f_registro,idusuario from datos.descuentos where id = {id} and t_prestamo = 'M'";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    string fecha = Convert.ToString(resultado[0]["fum"]).Replace(" 12:00:00 a. m.", "");
                    string fecharegistro = Convert.ToString(resultado[0]["f_registro"]).Replace(" 12:00:00 a. m.", "");
                    if (string.IsNullOrWhiteSpace(fecha))
                    {
                        globales.MessageBoxInformation($"Fecha de registro: {fecharegistro}", "Aviso", globales.menuPrincipal);
                    }
                    else
                    {
                        string iduser = Convert.ToString(resultado[0]["idusuario"]);
                        if (!string.IsNullOrWhiteSpace(iduser))
                        {
                            List<Dictionary<string, object>> r = globales.consulta($"select usuario from catalogos.usuarios where idusuario = {iduser}");
                            if (r.Count != 0)
                            {
                                string usuario = Convert.ToString(r[0]["usuario"]);
                                globales.MessageBoxInformation($"Fecha de registro: {fecharegistro} \nUltima modificación:{fecha}\nUsuario: {usuario}", "Aviso", globales.menuPrincipal);
                            }
                            else
                            {
                                globales.MessageBoxInformation($"Fecha de registro: {fecharegistro} \nUltima modificación:{fecha}", "Aviso", globales.menuPrincipal);
                            }
                        }
                        else
                        {
                            globales.MessageBoxInformation($"Fecha de registro: {fecharegistro} \nUltima modificación:{fecha}", "Aviso", globales.menuPrincipal);
                        }
                    }
                }

                if (e.KeyCode == Keys.F1)
                {
                    if (column == 5)
                    {
                        frmdependencias dependencia = new frmdependencias();
                        dependencia.enviar = rellenando;
                        dependencia.ShowDialog();
                        return;
                    }
                }

                if (e.Control && e.KeyCode == Keys.D)
                {
                    if (this.row > 1)
                    {
                        dtgridintereses.Rows[row].Cells[this.column].Value = dtgridintereses.Rows[row - 1].Cells[this.column].Value;
                    }
                }
            }
            catch
            {

            }

            this.esInsertar = false;

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                try
                {
                    e.SuppressKeyPress = true;
                    int iColumn = dtgridintereses.CurrentCell.ColumnIndex;
                    int iRow = dtgridintereses.CurrentCell.RowIndex;
                    if (iColumn == dtgridintereses.ColumnCount - 1)
                    {
                        if (dtgridintereses.RowCount > (iRow + 1))
                        {
                            dtgridintereses.CurrentCell = dtgridintereses[1, iRow + 1];
                        }
                        else
                        {
                            //focus next control
                        }
                    }
                    else
                        dtgridintereses.CurrentCell = dtgridintereses[iColumn + 1, iRow];
                }
                catch
                {

                }
            }
        }

        private void dtgridintereses_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion3);
        }

        private void viendoEdicion3(object sender, PreviewKeyDownEventArgs e)
        {
            this.teclaEnter = e.KeyCode == Keys.Enter;
        }

        private void frmestadosHipo_Load(object sender, EventArgs e)
        {
            dtggrid.ReadOnly = globales.boolConsulta;
            dtgridintereses.ReadOnly = globales.boolConsulta;

           
        }

        private void frmestadosHipo_Shown(object sender, EventArgs e)
        {
            if (!globales.boolConsulta)
            {
                globales.MessageBoxInformation("La primera cantidad de moratorios generados debe registrarse en los campos:\nInt.Morat. y en detalle de intereses.", "Aviso", globales.menuPrincipal);
            }

            ActiveControl = button2;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (contadorGlobal != 0)
            {
                contadorGlobal--;
            }

            rellenarDatos(this.parecidos[contadorGlobal]);
            this.Cursor = Cursors.Default;
            checarbotones();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (contadorGlobal != this.parecidos.Count - 1)
            {
                contadorGlobal++;
            }
            rellenarDatos(this.parecidos[contadorGlobal]);
            this.Cursor = Cursors.Default;
            checarbotones();
        }

        private void checFallecio_CheckedChanged(object sender, EventArgs e)
        {
            if(checFallecio.Checked)
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
