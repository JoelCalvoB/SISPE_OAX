using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.codigo.repositorios.catalogos;
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
    public partial class frmestados : Form
    {
        bool seCambiar = false;
        private bool consulta { get; set; }
        private List<string> filasEliminadas = new List<string>();
        private string idString = string.Empty;
        private int filaActual = 0;
        private int columnaActual = 0;
        private List<Dictionary<string, object>> parecidos;
        private int contadorGlobal = 0;
        private bool esInsertar;
        private int row;
        private bool error;
        private string secretaria;
        private bool teclaEnter;
        private bool editadoprogramadamente;
        private int column;

        public frmestados(bool consulta = false)
        {
            InitializeComponent();

            this.consulta = consulta;
            this.consulta = globales.boolConsulta;
        }



        public frmestados()
        {
            InitializeComponent();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bool banderita = true;
            modalQuirografario<p_edocta> quirog = new modalQuirografario<p_edocta>("" , "", "true");
            quirog.enviar = rellenarConsulta;
            globales.showModal(quirog);
            this.ActiveControl = txtimporte;
            btbuscar.Enabled = true;
            Cursor = Cursors.Default;
        }

        public void rellenarConsulta(Dictionary<string, object> datos)
        {

            Cursor = Cursors.WaitCursor;
            dtggrid.Rows.Clear();

            dbaseORM orm = new dbaseORM();

            string query = $"select * from catalogos.cuentas where proy = '{datos["secretaria"]}'";
            cuentas cuenta = orm.queryForMap<cuentas>(query);

            string descripcion = cuenta.descripcion;
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                query = $"select * from catalogos.cuentas where cuenta = '{datos["secretaria"]}'";
                cuenta = orm.queryForMap<cuentas>(query);
                descripcion = cuenta.descripcion;
            }

            p_edocta estadoCuenta = orm.getObject<p_edocta>(datos);

            this.txtrfc.Text = estadoCuenta.rfc;
            this.txtnombre.Text = estadoCuenta.nombre_em;
            this.txtproyecto.Text = estadoCuenta.proyecto;
            this.txtfolio.Text = Convert.ToString(estadoCuenta.folio);
            this.txtdirec.Text = estadoCuenta.direccion;
            this.txtcheque.Text = globales.parseDateTime(estadoCuenta.f_emischeq);
            this.txtpago.Text = estadoCuenta.tipo_pago;
            this.txtimporte.Text = string.Format("{0:C}", estadoCuenta.imp_unit);
            this.txtubicacion.Text = estadoCuenta.ubic_pagare;
            this.txttotal.Text = string.Format("{0:C}", estadoCuenta.importe);
            this.txtsecretaria.Text = estadoCuenta.secretaria + "        " + descripcion;
            this.txtpagocuenta.Text = globales.parseDateTime(estadoCuenta.f_primdesc);
            this.txtfechasolicitud.Text = globales.parseDateTime(estadoCuenta.f_solicitud);
            this.txtplazo.Text = Convert.ToString(estadoCuenta.plazo);
            string fecha = Convert.ToString(estadoCuenta.fallecio);
                if (!string.IsNullOrWhiteSpace(fecha)) 
            {
                checFallecio.Checked = true;
                checFallecio.Visible = true;
                fec_falle.Visible = true;
                fec_falle.Text = fecha;
            }
                else
            {
                checFallecio.Checked = false;
                fec_falle.Visible = false;

            }
            secretaria = estadoCuenta.secretaria;

            //el código para llenar el dagrid...
            string aux = Convert.ToString(datos["folio"]);

            query = string.Format("select hum,f_descuento,numdesc,totdesc,importe,rfc,cuenta,proyecto,tipo_rel,id from datos.descuentos where  folio = {0} AND t_prestamo='Q'  order by f_descuento asc, numdesc asc", aux);
            List<descuentos> descuentos = orm.queryForList<descuentos>(query);

            descuentos.ForEach(o => {
                dtggrid.Rows.Add(globales.parseDateTime(o.f_descuento), o.numdesc, o.totdesc, string.Format("{0:C}",o.importe).Replace("$",""), o.rfc, o.cuenta, o.proyecto, o.tipo_rel, o.id, o.fum);
            });


            string nombreAux = txtnombre.Text;
            nombreAux = (nombreAux.Contains("(")) ? nombreAux.Substring(0, nombreAux.IndexOf("(")) : nombreAux;

            this.parecidos = orm.query($"select * from datos.p_edocta where nombre_em like '{nombreAux}%' and historial_pagado is null");
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


            if (!globales.boolConsulta)
            {
                txtimporte.Cursor = Cursors.IBeam;
                txtimporte.ReadOnly = false;
                txtubicacion.Cursor = Cursors.IBeam;
                txtubicacion.ReadOnly = false;
                txtplazo.Cursor = Cursors.IBeam;
                txtplazo.ReadOnly = false;
                txtcheque.Cursor = Cursors.IBeam;
                txtcheque.ReadOnly = false;
                txttotal.Cursor = Cursors.IBeam;
                txttotal.ReadOnly = false;
                txtpagocuenta.Cursor = Cursors.IBeam;
                txtpagocuenta.ReadOnly = false;
                txtnombre.Cursor = Cursors.IBeam;
                txtnombre.ReadOnly = false;
                checFallecio.Visible = true;
               // fec_falle.Visible = true;
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
            Cursor = Cursors.Default;
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {


            Close();
        }

        private void frmconsulta_Load(object sender, EventArgs e)
        {
            dtggrid.ReadOnly = globales.boolConsulta;
            if (globales.boolConsulta)
            {
                label1.Text = "CONSULTAS QUIROGRAFARIOS";
                this.Text = "Consultas";
                this.btbuscar.Visible = false;
                txtimporte.Cursor = Cursors.No;
                txtimporte.ReadOnly = true;

                txtubicacion.Cursor = Cursors.No;
                txtubicacion.ReadOnly = true;

                txtplazo.Cursor = Cursors.No;
                txtplazo.ReadOnly = true;

                txtcheque.Cursor = Cursors.No;
                txtcheque.ReadOnly = true;

                txttotal.Cursor = Cursors.No;
                txttotal.ReadOnly = true;

                txtpagocuenta.Cursor = Cursors.No;
                txtpagocuenta.ReadOnly = true;

            }
            else
            {
                label1.Text = "Quirografarios";
                this.Text = "Estados de Cuenta";
                this.btbuscar.Visible = true;
            }


        }

        public void adicional()
        {



        }

        private void frmconsulta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {

                try
                {
                    if (string.IsNullOrWhiteSpace(txtfolio.Text))
                    {
                        globales.MessageBoxExclamation("Seleccionar un folio para visualizar resultados", "Aviso", globales.menuPrincipal);
                        return;
                    }

                    string query = $"select sum(importe) as acumulado from datos.descuentos where t_prestamo = 'Q' and folio = {txtfolio.Text} ";
                    double cantidad = 0;
                    string cantidadStr = Convert.ToString(globales.consulta(query)[0]["acumulado"]);

                    cantidad = string.IsNullOrWhiteSpace(cantidadStr) ? 0 : globales.convertDouble(cantidadStr);
                    adicionalconsulta forma = new adicionalconsulta();
                    forma.txttotal.Text = txttotal.Text;
                    forma.txtacumulado.Text = string.Format("{0:C}", cantidad);
                    forma.txtsaldo.Text = string.Format("{0:C}", (double.Parse(txttotal.Text, NumberStyles.Currency) - Convert.ToDouble(cantidad)));
                    globales.showModal(forma); ;

                }
                catch
                {
                    globales.MessageBoxExclamation("Es necesario guardar para visualizar resultados.\nDar clic en guardar o bien presionar F3", "Aviso", globales.menuPrincipal);
                }

                if (e.KeyCode == Keys.F5)
                {
                    frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
                    p_quirog.enviar2 = rellenarConsulta;
                    p_quirog.tablaConsultar = "p_edocta";
                    p_quirog.ShowDialog();
                    this.ActiveControl = txtimporte;
                    btbuscar.Enabled = true;
                }

            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }


            if (e.KeyCode == Keys.F2)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.F5)
            {
                btnNuevo_Click(null, null);
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
                    string de = txtplazo.Text;
                    string importe = "0.00";
                    string rfc = txtrfc.Text;
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
                    string query = "insert into datos.descuentos(folio,f_descuento,rfc,numdesc,totdesc,importe,cuenta,proyecto,tipo_rel,t_prestamo,f_registro,idusuario,hum,fum) values({0},'{1}','{2}',{3},{4},0,'','','','Q','{5}',{6},'{7}','{8}') returning id";

                    query = string.Format(query, folio, string.Format("{0:yyyy-MM-dd}", DateTime.Parse(fecha)), rfc, 0, de, string.Format("{0:yyyy-MM-dd}", DateTime.Now), globales.id_usuario, string.Format("{0:hh:mm}", DateTime.Now), string.Format("{0:yyyy-MM-dd}", DateTime.Now));

                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    dtggrid.Rows[0].Cells[8].Value = resultado[0]["id"];
                    dtggrid.CurrentCell = dtggrid.Rows[0].Cells[0];

                }
            }

        }



        private void frmconsulta_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult p = globales.MessageBoxQuestion("¿Desea guardar cambios?", "Aviso", globales.menuPrincipal);
                if (p == DialogResult.No) return;

                if (string.IsNullOrWhiteSpace(txtfolio.Text))
                {
                    globales.MessageBoxExclamation("Favor de introducir número de folio", "Aviso", globales.menuPrincipal);
                    return;
                }

                Cursor = Cursors.WaitCursor;
                string fecha = string.Empty;
                
                if (checFallecio.Checked==true)
                {
                    fecha = "'" +fec_falle.Text+ "'";
                }
                else
                {
                    fecha = "null";
                }

                string query = $"update datos.p_edocta set imp_unit = {double.Parse(txtimporte.Text, NumberStyles.Currency)},ubic_pagare = '{txtubicacion.Text}',importe = {double.Parse(txttotal.Text, NumberStyles.Currency)}, f_emischeq = '{txtcheque.Text}',plazo={txtplazo.Text}, nombre_em = '{txtnombre.Text}' , fallecio={fecha}   where folio = {txtfolio.Text}";
                if (globales.consulta(query, true))
                {
                    globales.MessageBoxSuccess("Registros actualizados correctamente.", "Aviso", globales.menuPrincipal);

                }
            }
            catch
            {
                globales.MessageBoxError("Error en el sistema, favor de contactar a los de sistemas", "Aviso", globales.menuPrincipal);
            }

            Cursor = Cursors.Default;
        }

        private void datosgb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
                    try
                    {
                        dtggrid.CurrentCell = dtggrid[iColumn + 1, iRow];
                    }
                    catch
                    {
                    }
            }
            if (e.Control && e.KeyCode == Keys.D)
            {
                if (this.filaActual != 0)
                {
                    DataGridViewRow dato = dtggrid.Rows[this.filaActual - 1];
                    string rfc = Convert.ToString(dato.Cells[4].Value);
                    string dependencia = Convert.ToString(dato.Cells[5].Value);
                    string proyecto = Convert.ToString(dato.Cells[6].Value);
                    string tipoRelacion = Convert.ToString(dato.Cells[7].Value);
                    dato = dtggrid.Rows[this.filaActual];
                    dato.Cells[4].Value = rfc;
                    dato.Cells[5].Value = dependencia;
                    dato.Cells[6].Value = proyecto;
                    dato.Cells[7].Value = tipoRelacion;
                }
            }

            if (e.KeyCode == Keys.F1)
            {
                if (this.columnaActual == 5)
                {
                    frmdependencias dependencias = new frmdependencias();
                    dependencias.enviar = rellenarDependencias;
                    dependencias.ShowDialog();
                }
            }
        }

        public void rellenarDependencias(Dictionary<string, object> obj, bool externo = false)
        {
            string proyecto = Convert.ToString(obj["proy"]);
            string cuenta = Convert.ToString(obj["cuenta"]);

            dtggrid.Rows[this.filaActual].Cells[5].Value = cuenta;
            dtggrid.Rows[this.filaActual].Cells[6].Value = proyecto;

        }
        private void txtimporte_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtimporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);
        }

        private void txtimporte_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            this.seCambiar = false;
        }

        private void txtimporte_Leave(object sender, EventArgs e)
        {
            try
            {
                TextBox texto = (TextBox)sender;
                double cantidad = double.Parse(texto.Text, NumberStyles.Currency);
                string strCantidad = string.Format("{0:C}", cantidad);
                texto.Text = strCantidad;
            }
            catch
            {
                globales.MessageBoxError("Error al convertir la moneda, ingresarla nuevamente", "Aviso", globales.menuPrincipal);
            }

        }

        private void datosgb_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {

                string id = this.idString;
                if (!string.IsNullOrWhiteSpace(id))
                    filasEliminadas.Add(id);
            }
            catch
            {

            }
        }

        private void txtcheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActiveControl = dtggrid;
            }
        }

        private void datosgb_Click(object sender, EventArgs e)
        {

        }

        private void datosgb_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            this.filaActual = e.RowIndex;
            this.columnaActual = e.ColumnIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas cerrar el modulo?", globales.menuPrincipal);
            if (p == DialogResult.Yes)
                this.Close();
        }

        private void frmestados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                //  SendKeys.Send("{TAB}");
            }
            char S;

            S = Char.ToUpper(e.KeyChar);

            e.KeyChar = S;
        }

        private void frmestados_Shown(object sender, EventArgs e)
        {
            btnNuevo.Select();
        }

        private void datosgb_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //SendKeys.Send("{UP}");
            //SendKeys.Send("{TAB}");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (contadorGlobal != 0)
            {
                contadorGlobal--;
            }

            rellenarConsulta(this.parecidos[contadorGlobal]);
            this.Cursor = Cursors.Default;
            checarbotones();
        }

        private void checarbotones()
        {
            btn1.Enabled = (this.contadorGlobal == 0) ? false : true;
            btn2.Enabled = (this.contadorGlobal == this.parecidos.Count - 1) ? false : true;
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            if (contadorGlobal != this.parecidos.Count - 1)
            {
                contadorGlobal++;
            }
            rellenarConsulta(this.parecidos[contadorGlobal]);
            this.Cursor = Cursors.Default;
            checarbotones();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void group_Enter(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtfolio_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtrfc_TextChanged(object sender, EventArgs e)
        {

        }

        private void datosgb_KeyDown_1(object sender, KeyEventArgs e)
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
                    string query = "insert into datos.descuentos(folio,f_descuento,rfc,numdesc,totdesc,importe,cuenta,proyecto,tipo_rel,t_prestamo,hum,idusuario,fum,f_registro) values({0},'{1}','{2}',{3},{4},0,'','','','Q','{5}',{6},'{7}','{8}') returning id";

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
                    string query = "delete from datos.descuentos where folio = {0} and id = {1} and t_prestamo = 'Q'";
                    query = string.Format(query, txtfolio.Text, id);
                    if (globales.consulta(query, true))
                    {
                        globales.MessageBoxSuccess("Descuento eliminado correctamente", "Aviso", globales.menuPrincipal);
                    }
                }

                if (e.KeyCode == Keys.F8 && e.Control)
                {
                    string id = dtggrid.Rows[row].Cells[8].Value.ToString();
                    string query = $"select fum,f_registro,idusuario from datos.descuentos where id = {id}";
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

                if (e.KeyCode == Keys.F1 ) {
                    if (column == 5) {
                        frmdependencias dependencia = new frmdependencias();
                        dependencia.enviar = rellenando;
                        dependencia.ShowDialog();
                        return;
                    }
                }

                if (e.Control && e.KeyCode == Keys.D) {
                    if (this.row > 1) {
                        dtggrid.Rows[row].Cells[this.column].Value = dtggrid.Rows[row-1].Cells[this.column].Value;
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
                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[0].Value = globales.parseDateTime(globales.convertDatetime(Convert.ToString(dtggrid.Rows[c].Cells[0].Value).Replace(".", "/")));
                editadoprogramadamente = false;
                f_descuento = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(Convert.ToString(dtggrid.Rows[c].Cells[0].Value)));
            }
            catch
            {
                globales.MessageBoxError("Ingresar fecha con formato correcto:\ndd/mm/yyyy.", "Error en fecha", globales.menuPrincipal);
                globales.MessageBoxExclamation("Los registros no se actualizaran hasta que se ingrese la fecha de pago correctamente", "Aviso", globales.menuPrincipal);

                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[0].Value = "";
                editadoprogramadamente = false;
                f_descuento = "";
                this.teclaEnter = false;
                SendKeys.Send("{UP}");
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


            string query = $"update datos.descuentos set f_descuento = '{f_descuento}',rfc = '{rfc}',numdesc = {serie1},totdesc={hasta},importe={importe},cuenta = '{dependencia}',proyecto = '{proyecto}',tipo_rel='{tipo_relacion}',idusuario={globales.id_usuario},fum='{string.Format("{0:yyyy-MM-dd}", DateTime.Now)}',hum='{string.Format("{0:hh:mm}", DateTime.Now)}' where  id = {id} and folio = {txtfolio.Text} and t_prestamo = 'Q'";
            globales.consulta(query, true);
            editadoprogramadamente = false;
        }

        private void dtggrid_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            var valor = e.Value;
        }

        private void dtggrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion);
        }

        private void viendoEdicion(object sender, PreviewKeyDownEventArgs e)
        {
            this.teclaEnter = e.KeyCode == Keys.Enter;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            string query = "select folio,cuenta,id from datos.descuentos where t_prestamo = 'Q' and cuenta = 'XXX'";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            int contador = 1;
            resultado.ForEach(o => {
                query = $"select cuenta from datos.descuentos where folio = {o["folio"]} and cuenta <> 'XXX' order by f_descuento desc limit 1";
                List<Dictionary<string, object>> r = globales.consulta(query);
                if (r.Count != 0) {
                    Dictionary<string, object> diccionario = r[0];
                    query = $"update datos.descuentos set cuenta = '{diccionario["cuenta"]}' where id = {o["id"]} and folio = {o["folio"]}";
                    globales.consulta(query,true);
                    System.Diagnostics.Debug.WriteLine(contador);
                    contador++;
                }
            });
        }

        private void btbuscar_Click_1(object sender, EventArgs e)
        {
            button1_Click(null,null);
        }

        private void checFallecio_CheckedChanged(object sender, EventArgs e)
        {
            if (checFallecio.Checked)
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

