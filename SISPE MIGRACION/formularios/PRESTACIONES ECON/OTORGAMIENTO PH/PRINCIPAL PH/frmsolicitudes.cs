using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.codigo.repositorios.datos;
using SISPE_MIGRACION.formularios.CATÁLOGOS;
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
    public partial class frmSolicitudesHipotecario : Form
    {
        string sueldo, solonumerosueldo;
        private bool teclaEnter;
        private int row;
        private int column;
        private bool editadoprogramadamente;
        private bool esInsertar;
        private bool escontrolshowging;

        public frmSolicitudesHipotecario()
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

            this.txtDire_inmueb.Text = Convert.ToString(datos["direc_inmu"]);

            string query = $"select * from datos.h_solici where expediente = {txtExpediente.Text} order by sec";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            dtggrid.Rows.Clear();
            resultado.ForEach(o =>
            {
                string sec = Convert.ToString(o["sec"]);
                string f_solicitud = Convert.ToString(o["f_solicitud"]).Replace(" 12:00:00 a. m.", "");
                string finalidad = Convert.ToString(o["finalidad"]);
                string descri_finalid = Convert.ToString(o["descri_finalid"]);
                string trel = Convert.ToString(o["trel"]);
                string plazoa = Convert.ToString(o["plazoa"]);
                string cap_prest = Convert.ToString(o["cap_prest"]);
                cap_prest = string.Format("{0:c}", Convert.ToDouble(cap_prest));
                string plazo = Convert.ToString(o["plazo"]);
                string status = Convert.ToString(o["status"]);
                string f_autorizacion = Convert.ToString(o["f_autorizacion"]).Replace(" 12:00:00 a. m.", "");
                string id = Convert.ToString(o["id"]);

                dtggrid.Rows.Add(sec, f_solicitud, finalidad, descri_finalid, trel, plazoa, cap_prest,
                    plazo, status, f_autorizacion, id);
            });

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
            txtExpediente.Clear();

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmexpediente_KeyDown(object sender, KeyEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtExpediente.Text)) return;
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }

            if (e.KeyCode == Keys.F12)
            {
                limpiacampos();
            }

            if (e.KeyCode == Keys.F9)
            {

            }

            if (e.KeyCode == Keys.F5) {
                btnFolio_Click(null,null);
            }

            int rowactual = dtggrid.Rows.Count;
            if (e.KeyCode == Keys.Insert && !globales.boolConsulta)
            {
                if (dtggrid.Rows.Count != 0) return;
                DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar una nuevo registro?", "Aviso", globales.menuPrincipal);
                if (p == DialogResult.No) return;
                this.esInsertar = true;

                string fecha = string.Format("{0:d}", DateTime.Now);




                dtggrid.Rows.Insert(0);
                dtggrid.Rows[0].Cells[0].Value = "";
                dtggrid.Rows[0].Cells[1].Value = fecha;
                dtggrid.Rows[0].Cells[2].Value = "";
                dtggrid.Rows[0].Cells[3].Value = "";
                dtggrid.Rows[0].Cells[4].Value = "";
                dtggrid.Rows[0].Cells[5].Value = "";
                dtggrid.Rows[0].Cells[6].Value = "";
                dtggrid.Rows[0].Cells[7].Value = "";
                dtggrid.Rows[0].Cells[8].Value = "";
                dtggrid.Rows[0].Cells[9].Value = "";
                dtggrid.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);

                fecha = string.Format("'{0:yyyy-MM-dd}'", DateTime.Now);

                h_solici solicitud = new h_solici();
                solicitud.f_solicitud = DateTime.Now;
                solicitud.f_autorizacion = DateTime.Now;
                solicitud.expediente = Convert.ToInt32(txtExpediente.Text);

                dbaseORM orm = new dbaseORM();
                solicitud = orm.insert<h_solici>(solicitud, true);

                dtggrid.Rows[rowactual].Cells[10].Value = solicitud.id;
                dtggrid.CurrentCell = dtggrid.Rows[rowactual].Cells[0];
            }
        }


        private void frmexpediente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }

            e.KeyChar =  Char.ToUpper(e.KeyChar);

             
        }

        private void frmexpediente_FormClosing(object sender, FormClosingEventArgs e)
        {

        }



        private void emplea()
        {
            frmEmpleados empleados = new frmEmpleados();
            empleados.enviar = rellenarConsulta;
            empleados.ShowDialog();

        }

        private void rellenarConsulta(Dictionary<string, object> resultado, bool externo = false)
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





        }

        private void nuevo()
        {


        }

        private void actualiza()
        {


        }





        private void frmexpediente_Load(object sender, EventArgs e)
        {
            
        }

        private void txtexpediente_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {


            }

            if (e.KeyCode == Keys.F5)  // BUSCAR FOLIO YA HECHO 
            {


                frmCatalogoP_quirog p_hipo = new frmCatalogoP_quirog();
                p_hipo.enviar2 = llenacampos;
                p_hipo.tablaConsultar = "p_hipote";
                p_hipo.ShowDialog();
            }
        }

        private void txtRfc_KeyDown(object sender, KeyEventArgs e)
        {


        }



        private void txtteléfono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFolio_Click(object sender, EventArgs e)
        {
            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
            p_quirog.enviar2 = llenacampos;
            p_quirog.tablaConsultar = "p_hipote";
            p_quirog.ShowDialog();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

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



            string f_solicitud = string.Empty;
            string f_autorizacion = string.Empty;

            try
            {
                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[1].Value = Convert.ToString(dtggrid.Rows[c].Cells[1].Value).Replace(".", "/");
                f_solicitud = (string.IsNullOrWhiteSpace(Convert.ToString(dtggrid.Rows[c].Cells[1].Value))) ? "" : string.Format("{0:yyyy-MM-dd}", DateTime.Parse(Convert.ToString(dtggrid.Rows[c].Cells[1].Value)));
                editadoprogramadamente = false;
            }
            catch
            {
                globales.MessageBoxError("Ingresar fecha con formato correcto:\ndd/mm/yyyy.", "Error en fecha", globales.menuPrincipal);
                globales.MessageBoxExclamation("Los registros no se actualizaran hasta que se ingrese la fecha de pago correctamente", "Aviso", globales.menuPrincipal);

                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[1].Value = "";
                editadoprogramadamente = false;
                f_solicitud = "";
                return;
            }

            try
            {
                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[0].Value = Convert.ToInt32(dtggrid.Rows[c].Cells[0].Value);
                editadoprogramadamente = false;
            }
            catch
            {
                globales.MessageBoxError("Formato incorrecto para el campo tipo", "Error en fecha", globales.menuPrincipal);
                globales.MessageBoxExclamation("Solo de acepta 1°, 2° y 3° ampliación.", "Aviso", globales.menuPrincipal);

                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[0].Value = "";
                editadoprogramadamente = false;
                return;
            }

            editadoprogramadamente = true;
            dtggrid.Rows[c].Cells[5].Value = globales.convertInt(Convert.ToString(dtggrid.Rows[c].Cells[5].Value));
            editadoprogramadamente = false;

            editadoprogramadamente = true;
            dtggrid.Rows[c].Cells[6].Value = string.Format ("{0:C}", globales.convertDouble(Convert.ToString(dtggrid.Rows[c].Cells[6].Value)));
            //  dtggrid.Rows[c].Cells[6].Value = globales.convertDouble(Convert.ToString(dtggrid.Rows[c].Cells[6].Value));
            editadoprogramadamente = false;


            try
            {
                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[9].Value = Convert.ToString(dtggrid.Rows[c].Cells[9].Value).Replace(".", "/");
                f_autorizacion = (string.IsNullOrWhiteSpace(Convert.ToString(dtggrid.Rows[c].Cells[9].Value))) ? "" : string.Format("{0:yyyy-MM-dd}", DateTime.Parse(Convert.ToString(dtggrid.Rows[c].Cells[9].Value)));
                editadoprogramadamente = false;
            }
            catch
            {
                globales.MessageBoxError("Ingresar fecha con formato correcto:\ndd/mm/yyyy.", "Error en fecha", globales.menuPrincipal);
                globales.MessageBoxExclamation("Los registros no se actualizaran hasta que se ingrese la fecha de pago correctamente", "Aviso", globales.menuPrincipal);

                editadoprogramadamente = true;
                dtggrid.Rows[c].Cells[9].Value = "";
                f_autorizacion = "";
            }


            if (string.IsNullOrWhiteSpace(f_solicitud))
            {
                globales.MessageBoxExclamation("Para actualizar información ingresar la fecha de solicitud correctamente", "Aviso", globales.menuPrincipal);
                return;
            }

            //if (string.IsNullOrWhiteSpace(f_autorizacion))
            //{
            //    globales.MessageBoxExclamation("Para actualizar información ingresar la fecha de autorización correctamente", "Aviso", globales.menuPrincipal);
            //    return;
            //}





            string sec = Convert.ToString(dtggrid.Rows[c].Cells[0].Value).ToUpper();
            string finalidad = Convert.ToString(dtggrid.Rows[c].Cells[2].Value).ToUpper();
            if (column == 2)
            {
                string consulta = $"select * from catalogos.finalidad where clave = '{finalidad}'";
                List<Dictionary<string, object>> res = globales.consulta(consulta);
                if (res.Count == 0)
                {
                    globales.MessageBoxExclamation("Finalidad no se encuentra en el catálogo, presione F1 para seleccionar alguno", "Aviso", globales.menuPrincipal);
                    this.editadoprogramadamente = true;
                    dtggrid.Rows[c].Cells[3].Value = "";
                    finalidad = "";
                    this.editadoprogramadamente = true;
                    dtggrid.Rows[c].Cells[2].Value = "";
                    this.editadoprogramadamente = false;
                    
                }
                else {
                    Dictionary<string, object> dicc = res[0];
                    string descripcion = Convert.ToString(dicc["descripcion"]);
                    this.editadoprogramadamente = true;
                    dtggrid.Rows[c].Cells[3].Value = descripcion;
                    this.editadoprogramadamente = false;
                }
            }
            string descri_finalid = Convert.ToString(dtggrid.Rows[c].Cells[3].Value).ToUpper();
            string trel = Convert.ToString(dtggrid.Rows[c].Cells[4].Value).ToUpper();
            double plazoa = string.IsNullOrWhiteSpace(Convert.ToString(dtggrid.Rows[c].Cells[5].Value).ToUpper()) ? 0 : Convert.ToDouble(dtggrid.Rows[c].Cells[5].Value);
            double cap_prest = 00;
            double entrada = 00;
         
            cap_prest = string.IsNullOrWhiteSpace(Convert.ToString(dtggrid.Rows[c].Cells[6].Value).ToUpper()) ? 0 : cap_prest = double.Parse(Convert.ToString(dtggrid.Rows[c].Cells[6].Value), System.Globalization.NumberStyles.Currency); ;
            string plazo = Convert.ToString(dtggrid.Rows[c].Cells[7].Value).ToUpper();
            string status = Convert.ToString(dtggrid.Rows[c].Cells[8].Value).ToUpper();
            string id = Convert.ToString(dtggrid.Rows[c].Cells[10].Value);

            f_autorizacion = globales.parseDateTime(globales.convertDatetime(Convert.ToString(dtggrid.Rows[c].Cells[9].Value)));

            f_autorizacion = string.IsNullOrWhiteSpace(f_autorizacion) ? "null" : $"'{string.Format("{0:yyyy-MM-dd}",DateTime.Parse(f_autorizacion))}'";

            string query = $"update datos.h_solici set sec = '{sec}',f_solicitud='{f_solicitud}',finalidad='{finalidad}',descri_finalid='{descri_finalid}',trel='{trel}',plazoa={plazoa},cap_prest={cap_prest},plazo = '{plazo}',status='{status}',f_autorizacion={f_autorizacion} where expediente = {txtExpediente.Text} and id = {id}";
            globales.consulta(query, true);

            //Parse para editar los campos necesarios//....
            if (!string.IsNullOrWhiteSpace(trel))
            {
                if (plazoa == 0)
                    return;
                if (cap_prest == 0)
                    return;  

                double T_INTERESH = Convert.ToDouble(globales.seleccionaTasaDeInteres(f_solicitud, true, chk1.Checked, trel));
                double int_prest = Math.Round(cap_prest * ((plazoa * 12) + 1) * T_INTERESH,2);
                double tot_prest = cap_prest + int_prest;
                double cap_unit = 0;
                double int_unit = 0;
                double tot_unit = 0;

                double cap_prim = 0;
                double int_prim = 0;
                double tot_prim = 0;
                
                int tipo = (plazo.ToUpper().Trim() == "Q") ? 24 : 12;
                cap_unit = Math.Floor(cap_prest / (plazoa * tipo));
                int_unit = Math.Floor(int_prest / (plazoa * tipo));
                tot_unit = cap_unit + int_unit;

                cap_prim = cap_prest - (((plazoa * tipo) -1) * cap_unit);
                int_prim = Math.Round(int_prest - (((plazoa * tipo) - 1) * int_unit),2);
                tot_prim = Math.Round(cap_prim + int_prim,2);

                

                query = $"update datos.h_solici set int_prest={int_prest},tot_prest={tot_prest},cap_unit={cap_unit},int_unit={int_unit},tot_unit={tot_unit},cap_prim={cap_prim},int_prim={int_prim},tot_prim={tot_prim} where expediente = {txtExpediente.Text} and id = {id}";
                globales.consulta(query, true);



            }

            editadoprogramadamente = false;
        }

        private void dtggrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown -= new PreviewKeyDownEventHandler(viendoEdicion);
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion);

            e.Control.KeyDown -= dtggrid_KeyDown;
            e.Control.KeyDown += dtggrid_KeyDown;

        }

        private void viendoEdicion(object sender, PreviewKeyDownEventArgs e)
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
                    DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar una nuevo registro?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;
                    this.esInsertar = true;

                    string fecha = string.Format("{0:d}", DateTime.Now);




                    dtggrid.Rows.Insert(rowactual);
                    dtggrid.Rows[rowactual].Cells[0].Value = "";
                    dtggrid.Rows[rowactual].Cells[1].Value = fecha;
                    dtggrid.Rows[rowactual].Cells[2].Value = "";
                    dtggrid.Rows[rowactual].Cells[3].Value = "";
                    dtggrid.Rows[rowactual].Cells[4].Value = "";
                    dtggrid.Rows[rowactual].Cells[5].Value = "";
                    dtggrid.Rows[rowactual].Cells[6].Value = "";
                    dtggrid.Rows[rowactual].Cells[7].Value = "";
                    dtggrid.Rows[rowactual].Cells[8].Value = "";
                    dtggrid.Rows[rowactual].Cells[9].Value = "";
                    dtggrid.Rows[rowactual].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);

                    fecha = string.Format("'{0:yyyy-MM-dd}'", DateTime.Now);

                    h_solici solicitud = new h_solici();
                    solicitud.f_solicitud = DateTime.Now;
                    solicitud.f_autorizacion = DateTime.Now;
                    solicitud.expediente = Convert.ToInt32(txtExpediente.Text);


                    dbaseORM orm = new dbaseORM();
                    solicitud = orm.insert<h_solici>(solicitud,true);

                    dtggrid.Rows[rowactual].Cells[10].Value = solicitud.id;
                    dtggrid.CurrentCell = dtggrid.Rows[rowactual].Cells[0];
                }

                if (e.KeyCode == Keys.Delete && !globales.boolConsulta)
                {
                    DialogResult p = globales.MessageBoxQuestion("¿Desea eliminar el registro?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;



                    string id = dtggrid.Rows[row].Cells[10].Value.ToString();
                    dtggrid.Rows.RemoveAt(row);
                    string query = "delete from datos.h_solici where expediente = {0} and id = {1} ";
                    query = string.Format(query, txtExpediente.Text, id);
                    if (globales.consulta(query, true))
                    {
                        globales.MessageBoxSuccess("Registro eliminado correctamente", "Aviso", globales.menuPrincipal);
                    }
                }



                if (e.Control && e.KeyCode == Keys.D)
                {
                    if (this.row > 1)
                    {
                        dtggrid.Rows[row].Cells[this.column].Value = dtggrid.Rows[row - 1].Cells[this.column].Value;
                    }
                }

                if (e.KeyCode == Keys.F6) {
                    string id = dtggrid.Rows[row].Cells[10].Value.ToString();
                    string query = $"select sec,tot_prest,tot_prim,plazo,plazoa,tot_unit from datos.h_solici where expediente = {txtExpediente.Text} and id = {id}";
                    var resultado = globales.consulta(query);
                    var diccionario = resultado[0];

                    double tot_prest = string.IsNullOrWhiteSpace(Convert.ToString(diccionario["tot_prest"])) ? 0 : Convert.ToDouble(diccionario["tot_prest"]);
                    double tot_prim = string.IsNullOrWhiteSpace(Convert.ToString(diccionario["tot_prim"])) ? 0 : Convert.ToDouble(diccionario["tot_prim"]);
                    string plazo = Convert.ToString(diccionario["plazo"]);
                    int plazoa = string.IsNullOrWhiteSpace(Convert.ToString(diccionario["plazoa"])) ? 0 : Convert.ToInt32(diccionario["plazoa"]);
                    double tot_unit = string.IsNullOrWhiteSpace(Convert.ToString(diccionario["tot_unit"])) ? 0 : Convert.ToDouble(diccionario["tot_unit"]);

                    int tiempo = (plazo == "Q") ? ((plazoa * 24) - 1 ): ((plazoa * 24) - 1);

                    string cabecera = $"{diccionario["sec"]}° Ampliación";
                    string contenido = $"TOTAL DEL PRESTAMO : {string.Format("{0:C}",tot_prest)}\nPrimer pago: {string.Format("{0:c}",tot_prim)}\n{tiempo} PAG.POR NOMINA.: {string.Format("{0:c}",tot_unit)}";
                    globales.MessageBoxInformation(contenido,cabecera,globales.menuPrincipal);
                }

                if (e.KeyCode == Keys.F1) {
                    if (this.column == 2) {
                        frmCatalogoGeneral finalida = new frmCatalogoGeneral();
                        finalida.tabla = "finalidad";
                        finalida.metodo = llenarFinalidad;
                        finalida.ShowDialog();
                        SendKeys.Send("{TAB}");
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

        public void llenarFinalidad(Dictionary<string,object> obj) {
            dtggrid.Rows[row].Cells[2].Value = obj["clave"];
            dtggrid.Rows[row].Cells[3].Value = obj["descripcion"];
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

                }
                else
                {
                    globales.MessageBoxExclamation($"No existe el expediente N°: {txtExpediente.Text}", "Aviso", globales.menuPrincipal);
                    limpiacampos();

                }
            }
        }

        private void frmSolicitudesHipotecario_Shown(object sender, EventArgs e)
        {
            ActiveControl = button2;
        }

        private void dtggrid_EditModeChanged(object sender, EventArgs e)
        {

        }

        private void txtsueldob_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double numero = Convert.ToDouble(txtSueldobase.Text);
                sueldo = string.Format("{0:C}", (numero));
                this.txtSueldobase.Text = sueldo;

            }
        }


    }
}
