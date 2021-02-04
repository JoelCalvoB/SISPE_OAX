using SISPE_MIGRACION.codigo.repositorios;
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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS.ESTUDIO_SOCIOECONO
{
    delegate void metodoCambioVentanas(bool cambio);
    public partial class frmSocioEconomico : Form
    {
        private frmSocioEconomicoprincipal ventanaPrincipal;
        private frmSocioEconomicoSecundario secundario;
        private string expediente;
        private bool principalActiva = true;
        private bool esInsertar;
        private string secuencia;
        private bool teclaEnter;
        private int row;
        private int column;
        private bool editadoprogramadamente;
        private bool conExpediente;

        public frmSocioEconomico()
        {
            InitializeComponent();
        }

        private void frmSocioEconomico_Load(object sender, EventArgs e)
        {
            ventanaPrincipal = new frmSocioEconomicoprincipal();
            secundario = new frmSocioEconomicoSecundario();
            ventanaPrincipal.cambio = cambiarVentana;
            ventanaPrincipal.Dock = DockStyle.Fill;
            principalPanel.Controls.Add(ventanaPrincipal);
            this.ventanaPrincipal.btnP_hipote.Click += new EventHandler(guardarHipote);
            this.ventanaPrincipal.dtggrid.KeyDown += new KeyEventHandler(gridKey);
            this.ventanaPrincipal.dtggrid.CellEndEdit += new DataGridViewCellEventHandler(cellEnEdit);
            this.ventanaPrincipal.dtggrid.CellEnter += new DataGridViewCellEventHandler(cellenter);
            this.ventanaPrincipal.dtggrid.CellValueChanged += new DataGridViewCellEventHandler(cellvalueChanged);
            this.ventanaPrincipal.dtggrid.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(showcontrol);

            this.secundario.btnGuardarSecundario.Click += new EventHandler(guardarSecundario);
            this.principalActiva = true;

            this.secundario.txtDifer_I_E.Leave += new EventHandler(entrandoFoco);
        }

        private void entrandoFoco(object sender, EventArgs e)
        {
            calculadiferencias();
        }

        private void calculadiferencias()
        {
            this.secundario.txtI_Sueldo.Text = string.Format("{0:C}",globales.convertDouble(this.secundario.txtI_Sueldo.Text));
            this.secundario.txtI_Ayudas.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtI_Ayudas.Text));
            this.secundario.txtI_Quinq.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtI_Quinq.Text));
            this.secundario.txtI_Otros.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtI_Otros.Text));
            this.secundario.txtI_Conyu.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtI_Conyu.Text));
            this.secundario.txtI_Hijos.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtI_Hijos.Text));
            this.secundario.txtI_OtrosF.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtI_OtrosF.Text));

            this.secundario.txtI_Total.Text = string.Format("{0:C}",
                globales.convertDouble(this.secundario.txtI_Sueldo.Text)+
                globales.convertDouble(this.secundario.txtI_Ayudas.Text)+
                globales.convertDouble(this.secundario.txtI_Quinq.Text)+
                globales.convertDouble(this.secundario.txtI_Otros.Text)+
                globales.convertDouble(this.secundario.txtI_Conyu.Text)+
                globales.convertDouble(this.secundario.txtI_Hijos.Text)+
                globales.convertDouble(this.secundario.txtI_OtrosF.Text));


            this.secundario.txtE_Quiro.Text = string.Format("{0:C}",globales.convertDouble(this.secundario.txtE_Quiro.Text));
            this.secundario.txtE_Hipo.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Hipo.Text));
            this.secundario.txtE_Direc.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Direc.Text));
            this.secundario.txtE_Lbca.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Lbca.Text));
            this.secundario.txtE_Fpens.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Fpens.Text));
            this.secundario.txtE_Ss.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Ss.Text));
            this.secundario.txtE_Csindic.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Csindic.Text));
            this.secundario.txtE_Ispt.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Ispt.Text));
            this.secundario.txtE_Otros.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Otros.Text));
            this.secundario.txtE_Alim.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Alim.Text));
            this.secundario.txtE_Vestu.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Vestu.Text));
            this.secundario.txtE_Transp.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Transp.Text));
            this.secundario.txtE_Renta.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Renta.Text));
            this.secundario.txtE_Agua.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Agua.Text));
            this.secundario.txtE_Luz.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Luz.Text));
            this.secundario.txtE_Gas.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Gas.Text));
            this.secundario.txtE_Tel.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Tel.Text));
            this.secundario.txtE_Coleg.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Coleg.Text));
            this.secundario.txtE_Servid.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_Servid.Text));
            this.secundario.txtE_OtrosF.Text = string.Format("{0:C}", globales.convertDouble(this.secundario.txtE_OtrosF.Text));

            this.secundario.txtE_Total.Text = string.Format("{0:C}",
              globales.convertDouble(this.secundario.txtE_Quiro.Text)+
              globales.convertDouble(this.secundario.txtE_Hipo.Text)+
              globales.convertDouble(this.secundario.txtE_Direc.Text)+
              globales.convertDouble(this.secundario.txtE_Lbca.Text)+
              globales.convertDouble(this.secundario.txtE_Fpens.Text)+
              globales.convertDouble(this.secundario.txtE_Ss.Text)+
              globales.convertDouble(this.secundario.txtE_Csindic.Text)+
              globales.convertDouble(this.secundario.txtE_Ispt.Text)+
              globales.convertDouble(this.secundario.txtE_Otros.Text)+
              globales.convertDouble(this.secundario.txtE_Alim.Text)+
              globales.convertDouble(this.secundario.txtE_Vestu.Text)+
              globales.convertDouble(this.secundario.txtE_Transp.Text)+
              globales.convertDouble(this.secundario.txtE_Renta.Text)+
              globales.convertDouble(this.secundario.txtE_Agua.Text)+
              globales.convertDouble(this.secundario.txtE_Luz.Text)+
              globales.convertDouble(this.secundario.txtE_Gas.Text)+
              globales.convertDouble(this.secundario.txtE_Tel.Text)+
              globales.convertDouble(this.secundario.txtE_Coleg.Text)+
              globales.convertDouble(this.secundario.txtE_Servid.Text)+
              globales.convertDouble(this.secundario.txtE_OtrosF.Text)
              );

            this.secundario.txtDifer_I_E.Text = string.Format("{0:C}",(globales.convertDouble(this.secundario.txtI_Total.Text) - globales.convertDouble(this.secundario.txtE_Total.Text)));

        }

        private void guardarSecundario(object sender, EventArgs e)
        {
            calculadiferencias();

            string query = $"select expediente from datos.h_sestse where expediente = {txtExpediente.Text} and sec = '{this.secuencia}'";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            h_sestse ses = new h_sestse();
            ses.expediente = globales.convertInt(txtExpediente.Text);
            ses.sec = this.secuencia;
            ses = rellenaSecundarioObj(ses);
            if (resultado.Count != 0)
            {
                actualizaSecundario(ses);
            }
            else {
                insertaSecundario(ses);
            }
        }

        private h_sestse rellenaSecundarioObj(h_sestse obj)
        {
            obj.a_condic = this.secundario.txtA_Condic.Text;
            obj.a_ncuartos = globales.convertInt(this.secundario.txtA_Ncuartos.Text);
            obj.a_pisos = this.secundario.txtA_pisos.Text;
            obj.a_ilumin = this.secundario.txtA_Ilumin.Text;
            obj.a_ventil = this.secundario.txtA_Ventil.Text;
            obj.a_pared = this.secundario.txtA_Pared.Text;
            obj.a_techo = this.secundario.txtA_Techo.Text;
            obj.a_servsanit = this.secundario.txtA_Servsanit.Text;
            obj.a_patio = this.secundario.txtA_Patio.Text;
            obj.f_elab = globales.convertDatetime(this.secundario.txtF_Elab.Text);

            obj.b_estufa = this.secundario.chkB_estufa.Checked ? "S" : "N";
            obj.b_refri = this.secundario.chkB_Refri.Checked ? "S" : "N";
            obj.b_sala = this.secundario.chkB_Sala.Checked ? "S" : "N";
            obj.b_gabinete = this.secundario.chkB_Gabinete.Checked ? "S" : "N";
            obj.b_roperos = this.secundario.chkB_Roperos.Checked ? "S" : "N";
            obj.b_camas = this.secundario.chkB_Camas.Checked ? "S" : "N";
            obj.b_licuad = this.secundario.chkB_Licuad.Checked ? "S" : "N";
            obj.b_tv = this.secundario.chkB_Tv.Checked ? "S" : "N";
            obj.b_radio = this.secundario.chkB_Radio.Checked ? "S" : "N";
            obj.b_lavad = this.secundario.chkB_Lavad.Checked ? "S" : "N";
            obj.b_videoc = this.secundario.chkB_Videoc.Checked ? "S" : "N";
            obj.b_ventil = this.secundario.chkB_Ventil.Checked ? "S" : "N";
            obj.b_maqcoser = this.secundario.chk_Maqcoser.Checked ? "S" : "N";
            obj.b_vehic = this.secundario.chkB_Vehic.Checked ? "S" : "N";
            obj.b_comedor = this.secundario.chkB_Comedor.Checked ? "S" : "N";
            obj.b_otros = this.secundario.txtB_Otros.Text;

            obj.i_sueldo = globales.convertDouble(this.secundario.txtI_Sueldo.Text);
            obj.i_ayuda = globales.convertDouble(this.secundario.txtI_Ayudas.Text);
            obj.i_quinq = globales.convertDouble(this.secundario.txtI_Quinq.Text);
            obj.i_otros = globales.convertDouble(this.secundario.txtI_Otros.Text);
            obj.i_conyu = globales.convertDouble(this.secundario.txtI_Conyu.Text);
            obj.i_hijos = globales.convertDouble(this.secundario.txtI_Hijos.Text);
            obj.i_otrosf = globales.convertDouble(this.secundario.txtI_OtrosF.Text);
            obj.i_total = globales.convertDouble(this.secundario.txtI_Total.Text);

            obj.e_quiro = globales.convertDouble(this.secundario.txtE_Quiro.Text);
            obj.e_hipo = globales.convertDouble(this.secundario.txtE_Hipo.Text);
            obj.e_direc = globales.convertDouble(this.secundario.txtE_Direc.Text);
            obj.e_lbca = globales.convertDouble(this.secundario.txtE_Lbca.Text);
            obj.e_fpens = globales.convertDouble(this.secundario.txtE_Fpens.Text);
            obj.e_ss = globales.convertDouble(this.secundario.txtE_Ss.Text);
            obj.e_csindic = globales.convertDouble(this.secundario.txtE_Csindic.Text);
            obj.e_ispt = globales.convertDouble(this.secundario.txtE_Ispt.Text);
            obj.e_otros = globales.convertDouble(this.secundario.txtE_Otros.Text);
            obj.e_alim = globales.convertDouble(this.secundario.txtE_Alim.Text);
            obj.e_vestu = globales.convertDouble(this.secundario.txtE_Vestu.Text);
            obj.e_transp = globales.convertDouble(this.secundario.txtE_Transp.Text);
            obj.e_renta = globales.convertDouble(this.secundario.txtE_Renta.Text);
            obj.e_agua = globales.convertDouble(this.secundario.txtE_Agua.Text);
            obj.e_luz = globales.convertDouble(this.secundario.txtE_Luz.Text);
            obj.e_gas = globales.convertDouble(this.secundario.txtE_Gas.Text);
            obj.e_tel = globales.convertDouble(this.secundario.txtE_Tel.Text);
            obj.e_coleg = globales.convertDouble(this.secundario.txtE_Coleg.Text);
            obj.e_servid = globales.convertDouble(this.secundario.txtE_Servid.Text);
            obj.e_otrosf = globales.convertDouble(this.secundario.txtE_OtrosF.Text);
            obj.e_total = globales.convertDouble(this.secundario.txtE_Total.Text);

            obj.observ = this.secundario.txtObserv.Text;
            obj.conclus = this.secundario.txtConclus.Text;
            obj.difer_i_e = globales.convertDouble(this.secundario.txtDifer_I_E.Text);

            return obj;
        }

        private void insertaSecundario(h_sestse obj)
        {
            dbaseORM orm = new dbaseORM();
            bool insertado = orm.insert<h_sestse>(obj);
            if (insertado)
                globales.MessageBoxSuccess("Registro actualizado correctamente","Aviso",globales.menuPrincipal);
        }

        private void actualizaSecundario(h_sestse obj)
        {
            dbaseORM orm = new dbaseORM();
            bool actualizado =  orm.update<h_sestse>(obj);

            if (actualizado)
                globales.MessageBoxSuccess("Registro actualizado correctamente", "Aviso", globales.menuPrincipal);
        }

        private void showcontrol(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion);
        }

        private void viendoEdicion(object sender, PreviewKeyDownEventArgs e)
        {
            this.teclaEnter = e.KeyCode == Keys.Enter;
        }

        private void cellvalueChanged(object sender, DataGridViewCellEventArgs e)
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

            editadoprogramadamente = true;
            this.ventanaPrincipal.dtggrid.Rows[this.row].Cells[1].Value = globales.convertInt(Convert.ToString(this.ventanaPrincipal.dtggrid.Rows[this.row].Cells[1].Value));
            editadoprogramadamente = false;

            h_sdepec h = new h_sdepec();
            h.expediente = Convert.ToInt32(txtExpediente.Text);
            h.sec = this.secuencia;
            h.id =Convert.ToInt32( this.ventanaPrincipal.dtggrid.Rows[this.row].Cells[5].Value);
            h.nom_depec = this.ventanaPrincipal.dtggrid.Rows[this.row].Cells[0].Value.ToString();
            h.edad = Convert.ToInt32(this.ventanaPrincipal.dtggrid.Rows[this.row].Cells[1].Value);
            h.parentesco = Convert.ToString(this.ventanaPrincipal.dtggrid.Rows[this.row].Cells[2].Value);
            h.escolaridad = Convert.ToString(this.ventanaPrincipal.dtggrid.Rows[this.row].Cells[3].Value);
            h.ocupacion = Convert.ToString(this.ventanaPrincipal.dtggrid.Rows[this.row].Cells[4].Value);

            dbaseORM orm = new dbaseORM();
            bool actualizado = orm.update<h_sdepec>(h);


            editadoprogramadamente = false;
        }

        private void cellenter(object sender, DataGridViewCellEventArgs e)
        {
            this.row = e.RowIndex;
            this.column = e.ColumnIndex;
        }

        private void cellEnEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.teclaEnter)
            {
                var x = this.row + 1;
                var y = this.ventanaPrincipal.dtggrid.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                this.teclaEnter = false;
            }
        }

        private void gridKey(object sender, KeyEventArgs e)
        {
            if (this.ventanaPrincipal.dtggrid.Rows.Count != 0)
            {
                h_sdepec sdepec = new h_sdepec();
                int rowactual = this.ventanaPrincipal.dtggrid.Rows.Count;
                if (e.KeyCode == Keys.Insert && !globales.boolConsulta)
                {

                    if (!this.conExpediente)
                    {
                        globales.MessageBoxExclamation("No se puede insertar registros a la solicitud sin expediente", "Aviso", globales.menuPrincipal);
                        return;
                    }

                    DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar una nuevo registro?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;
                    this.esInsertar = true;

                    this.ventanaPrincipal.dtggrid.Rows.Insert(rowactual);
                    this.ventanaPrincipal.dtggrid.Rows[rowactual].Cells[0].Value = "";
                    this.ventanaPrincipal.dtggrid.Rows[rowactual].Cells[1].Value = "";
                    this.ventanaPrincipal.dtggrid.Rows[rowactual].Cells[2].Value = "";
                    this.ventanaPrincipal.dtggrid.Rows[rowactual].Cells[3].Value = "";
                    this.ventanaPrincipal.dtggrid.Rows[rowactual].Cells[4].Value = "";
                    this.ventanaPrincipal.dtggrid.Rows[rowactual].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);

                    h_sdepec depec = new h_sdepec();
                    depec.expediente = Convert.ToInt32(txtExpediente.Text);
                    depec.sec = this.secuencia;

                    dbaseORM orm = new dbaseORM();
                    depec = orm.insert<h_sdepec>(depec, true);

                    this.ventanaPrincipal.dtggrid.Rows[rowactual].Cells[5].Value = depec.id;
                    this.ventanaPrincipal.dtggrid.CurrentCell = this.ventanaPrincipal.dtggrid.Rows[rowactual].Cells[0];
                }

            }

            this.esInsertar = false;

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                try
                {
                    e.SuppressKeyPress = true;
                    int iColumn = this.ventanaPrincipal.dtggrid.CurrentCell.ColumnIndex;
                    int iRow = this.ventanaPrincipal.dtggrid.CurrentCell.RowIndex;
                    if (iColumn == this.ventanaPrincipal.dtggrid.ColumnCount - 1)
                    {
                        if (this.ventanaPrincipal.dtggrid.RowCount > (iRow + 1))
                        {
                            this.ventanaPrincipal.dtggrid.CurrentCell = this.ventanaPrincipal.dtggrid[1, iRow + 1];
                        }
                        else
                        {
                            //focus next control
                        }
                    }
                    else
                        this.ventanaPrincipal.dtggrid.CurrentCell = this.ventanaPrincipal.dtggrid[iColumn + 1, iRow];
                }
                catch
                {

                }
            }
        }

        private void guardarHipote(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas actualizar el registro?","Aviso",globales.menuPrincipal);
            if (dialogo == DialogResult.No) return;

            p_hipote p = new dbaseORM().queryForMap<p_hipote>("select * from datos.p_hipote where folio = "+txtExpediente.Text);
            p.folio = globales.convertInt(this.txtExpediente.Text);
            p.rfc = this.ventanaPrincipal.txtRfc.Text;
            p.nombre_em = this.ventanaPrincipal.txtNombre_em.Text;
            p.tel_ofici = this.ventanaPrincipal.txtTel_ofic.Text;
            p.direccion = this.ventanaPrincipal.txtDireccion.Text;
            p.secretaria = this.ventanaPrincipal.txtSecretaria.Text;
            p.descripcion = this.ventanaPrincipal.txtDescripcion.Text;
            p.direc_inmu = this.ventanaPrincipal.txtDirec_inmueb.Text;

            dbaseORM orm = new dbaseORM();
            bool resultado = orm.update(p);
            if (resultado) {
                globales.MessageBoxSuccess("Registro actualizado correctamente","Aviso",globales.menuPrincipal);
            }

        }

        private void cambiarVentana(bool siguiente) {
            if (!siguiente)
            {
                ventanaPrincipal.cambio = cambiarVentana;
                ventanaPrincipal.Dock = DockStyle.Fill;
                principalPanel.Controls.Clear();
                principalPanel.Controls.Add(ventanaPrincipal);
                this.principalActiva = true;
            }
            else {
                secundario.cambiar = cambiarVentana;
                secundario.Dock = DockStyle.Fill;
                principalPanel.Controls.Clear();
                principalPanel.Controls.Add(secundario);
                this.principalActiva = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cerrar();
        }
        private void cerrar()
        {
            this.Close();
            globales.showModal(new frmDocumentos(2));
        }


        private void btnFolio_Click(object sender, EventArgs e)
        {
            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
            p_quirog.enviar2 = llenacampos;
            p_quirog.tablaConsultar = "p_hipote";
            p_quirog.ShowDialog();
        }

        private void llenacampos(Dictionary<string, object> datos)
        {
            frmAmpliacion ampliacion = new frmAmpliacion("Documentación P/Solicitud", datos["folio"].ToString(), datos, "Solicitud inicial", "1° Ampliación", "2° Ampliación", "3° Ampliación");
            ampliacion.enviar = recibiendoampliacion;
            globales.showModal(ampliacion);
        }

        private void recibiendoampliacion(string expediente, int opcion, Dictionary<string, object> datos)
        {
            limpiacampos();
            this.expediente = expediente;
            txtExpediente.Text = expediente;
            string query = $"select expediente,sec from datos.h_solici where expediente = {expediente} and sec = '{opcion}'";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            string tramite = string.Empty;
            tramite = (opcion == 0) ? "Solicitud inicial" : opcion + "° Ampliación";
            this.secuencia = opcion.ToString();

            this.txtamplia.Text = tramite;

            query = "select * from datos.p_hipote where folio = "+expediente;
            p_hipote hipo = new dbaseORM().queryForMap<p_hipote>(query);
            this.ventanaPrincipal.txtRfc.Text = hipo.rfc;
            this.ventanaPrincipal.txtNombre_em.Text = hipo.nombre_em;
            this.ventanaPrincipal.txtDireccion.Text = hipo.direccion;
            this.ventanaPrincipal.txtSecretaria.Text = hipo.secretaria;
            this.ventanaPrincipal.txtDirec_inmueb.Text = hipo.direc_inmu;
            this.ventanaPrincipal.txtDescripcion.Text = hipo.descripcion;
            this.ventanaPrincipal.txtTel_ofic.Text = hipo.tel_ofici;

            if (resultado.Count == 0)
            {

                this.conExpediente = false;
                globales.MessageBoxExclamation($"Expediente N° {expediente} \nNo se encontro {tramite}", "Aviso", globales.menuPrincipal);
                return;
            }

            this.conExpediente = true;

            query = $"select * from datos.h_sdepec where expediente = {hipo.folio} and sec = '{resultado[0]["sec"]}' order by id ";
            List<h_sdepec> sdepec = new dbaseORM().queryForList<h_sdepec>(query);
            sdepec.ForEach(o => {
                this.ventanaPrincipal.dtggrid.Rows.Add(
                    o.nom_depec,o.edad,o.parentesco,o.escolaridad,o.ocupacion,o.id
                    );
            });

            query = $"select * from datos.h_sestse where expediente = {hipo.folio} and sec = '{resultado[0]["sec"]}'";
            h_sestse sestse = new dbaseORM().queryForMap<h_sestse>(query);
            rellenarSecundario(sestse);

        }

        private void rellenarSecundario(h_sestse obj)
        {
            this.secundario.txtA_Condic.Text = obj.a_condic;
            this.secundario.txtA_Ncuartos.Text = obj.a_ncuartos.ToString();
            this.secundario.txtA_pisos.Text = obj.a_pisos;
            this.secundario.txtA_Ilumin.Text = obj.a_ilumin;
            this.secundario.txtA_Ventil.Text = obj.a_ventil;
            this.secundario.txtA_Pared.Text = obj.a_pared;
            this.secundario.txtF_Elab.Text = globales.parseDateTime(obj.f_elab);
            this.secundario.txtA_Techo.Text = obj.a_techo;
            this.secundario.txtA_Servsanit.Text = obj.a_servsanit;
            this.secundario.txtA_Patio.Text = obj.a_patio;

            this.secundario.chkB_estufa.Checked = obj.b_estufa == "S" ? true :false;
            this.secundario.chkB_Refri.Checked = obj.b_refri == "S" ? true : false;
            this.secundario.chkB_Comedor.Checked = obj.b_comedor == "S" ? true : false;
            this.secundario.chkB_Sala.Checked = obj.b_sala == "S" ? true : false;
            this.secundario.chkB_Gabinete.Checked = obj.b_gabinete == "S" ? true : false;
            this.secundario.chkB_Roperos.Checked = obj.b_roperos == "S" ? true : false;
            this.secundario.chkB_Camas.Checked = obj.b_camas == "S" ? true : false;
            this.secundario.chkB_Licuad.Checked = obj.b_licuad == "S" ? true : false;
            this.secundario.chkB_Tv.Checked = obj.b_tv == "S" ? true : false;
            this.secundario.chkB_Radio.Checked = obj.b_radio == "S" ? true : false;
            this.secundario.chkB_Lavad.Checked = obj.b_lavad == "S" ? true : false;
            this.secundario.chkB_Videoc.Checked = obj.b_videoc == "S" ? true : false;
            this.secundario.chkB_Ventil.Checked = obj.b_ventil == "S" ? true : false;
            this.secundario.chk_Maqcoser.Checked = obj.b_maqcoser == "S" ? true : false;
            this.secundario.chkB_Vehic.Checked = obj.b_vehic == "S" ? true : false;
            this.secundario.txtB_Otros.Text = obj.b_otros;

            this.secundario.txtI_Sueldo.Text = string.Format("{0:C}", obj.i_sueldo).Replace("$", "");
            this.secundario.txtI_Ayudas.Text = string.Format("{0:C}", obj.i_ayuda).Replace("$", "");
            this.secundario.txtI_Quinq.Text = string.Format("{0:C}", obj.i_quinq).Replace("$", "");
            this.secundario.txtI_Otros.Text = string.Format("{0:C}", obj.i_otros).Replace("$", "");
            this.secundario.txtI_Conyu.Text = string.Format("{0:C}", obj.i_conyu).Replace("$", "");
            this.secundario.txtI_Hijos.Text = string.Format("{0:C}", obj.i_hijos).Replace("$", "");
            this.secundario.txtI_OtrosF.Text = string.Format("{0:C}", obj.i_otrosf).Replace("$", "");
            this.secundario.txtI_Total.Text = string.Format("{0:C}", obj.i_total).Replace("$", "");

            this.secundario.txtE_Quiro.Text = string.Format("{0:C}",obj.e_quiro).Replace("$","");
            this.secundario.txtE_Hipo.Text = string.Format("{0:C}", obj.e_hipo).Replace("$", "");
            this.secundario.txtE_Direc.Text = string.Format("{0:C}", obj.e_direc).Replace("$", "");
            this.secundario.txtE_Lbca.Text = string.Format("{0:C}", obj.e_lbca).Replace("$", "");
            this.secundario.txtE_Fpens.Text = string.Format("{0:C}", obj.e_fpens).Replace("$", "");
            this.secundario.txtE_Ss.Text = string.Format("{0:C}", obj.e_ss).Replace("$", "");
            this.secundario.txtE_Csindic.Text = string.Format("{0:C}", obj.e_csindic).Replace("$", "");
            this.secundario.txtE_Ispt.Text = string.Format("{0:C}", obj.e_ispt).Replace("$", "");
            this.secundario.txtE_Otros.Text = string.Format("{0:C}", obj.e_otros).Replace("$", "");
            this.secundario.txtE_Alim.Text = string.Format("{0:C}", obj.e_alim).Replace("$", "");
            this.secundario.txtE_Vestu.Text = string.Format("{0:C}", obj.e_vestu).Replace("$", "");
            this.secundario.txtE_Transp.Text = string.Format("{0:C}", obj.e_transp).Replace("$", "");
            this.secundario.txtE_Renta.Text = string.Format("{0:C}", obj.e_renta).Replace("$", "");
            this.secundario.txtE_Agua.Text = string.Format("{0:C}", obj.e_agua).Replace("$", "");
            this.secundario.txtE_Luz.Text = string.Format("{0:C}", obj.e_luz).Replace("$", "");
            this.secundario.txtE_Gas.Text = string.Format("{0:C}", obj.e_gas).Replace("$", "");
            this.secundario.txtE_Tel.Text = string.Format("{0:C}", obj.e_tel).Replace("$", "");
            this.secundario.txtE_Coleg.Text = string.Format("{0:C}", obj.e_coleg).Replace("$", "");
            this.secundario.txtE_Servid.Text = string.Format("{0:C}", obj.e_servid).Replace("$", "");
            this.secundario.txtE_OtrosF.Text = string.Format("{0:C}", obj.e_otrosf).Replace("$", "");
            this.secundario.txtE_Total.Text = string.Format("{0:C}", obj.e_total).Replace("$", "");

            this.secundario.txtObserv.Text = obj.observ;
            this.secundario.txtConclus.Text = string.Format("{0:C}", obj.conclus).Replace("$", "");
            this.secundario.txtDifer_I_E.Text = string.Format("{0:C}", obj.difer_i_e).Replace("$", "");


        }

        private void txtExpediente_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtExpediente.Text))
            {
                if (expediente == txtExpediente.Text) return;
                expediente = txtExpediente.Text;
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

        private void limpiacampos()
        {

            limpiarPrinciapal();
            limpiaSecundario();
            
        }

        private void limpiarPrinciapal()
        {
            this.ventanaPrincipal.dtggrid.Rows.Clear();
            this.txtamplia.Text = "";
            this.txtExpediente.Text = "";

            this.ventanaPrincipal.txtRfc.Text = "";
            this.ventanaPrincipal.txtNombre_em.Text = "";
            this.ventanaPrincipal.txtDireccion.Text = "";
            this.ventanaPrincipal.txtSecretaria.Text = "";
            this.ventanaPrincipal.txtDirec_inmueb.Text = "";
            this.ventanaPrincipal.txtDescripcion.Text = "";
            this.ventanaPrincipal.txtTel_ofic.Text = "";
        }

        private void limpiaSecundario()
        {
            this.secundario.txtA_Condic.Text = "";
            this.secundario.txtA_Ncuartos.Text = "";
            this.secundario.txtA_pisos.Text = "";
            this.secundario.txtA_Ilumin.Text = "";
            this.secundario.txtA_Ventil.Text = "";
            this.secundario.txtA_Pared.Text = "";
            this.secundario.txtF_Elab.Text = "";
            this.secundario.txtA_Techo.Text = "";
            this.secundario.txtA_Servsanit.Text = "";
            this.secundario.txtA_Patio.Text = "";

            this.secundario.chkB_estufa.Checked = false;
            this.secundario.chkB_Refri.Checked = false;
            this.secundario.chkB_Comedor.Checked = false;
            this.secundario.chkB_Sala.Checked = false;
            this.secundario.chkB_Gabinete.Checked = false;
            this.secundario.chkB_Roperos.Checked = false;
            this.secundario.chkB_Camas.Checked = false;
            this.secundario.chkB_Licuad.Checked = false;
            this.secundario.chkB_Tv.Checked = false;
            this.secundario.chkB_Radio.Checked = false;
            this.secundario.chkB_Lavad.Checked = false;
            this.secundario.chkB_Videoc.Checked = false;
            this.secundario.chkB_Ventil.Checked = false;
            this.secundario.chk_Maqcoser.Checked = false;
            this.secundario.chkB_Vehic.Checked = false;
            this.secundario.txtB_Otros.Text = "";

            this.secundario.txtI_Sueldo.Text = "";
            this.secundario.txtI_Ayudas.Text = "";
            this.secundario.txtI_Quinq.Text = "";
            this.secundario.txtI_Otros.Text = "";
            this.secundario.txtI_Conyu.Text = "";
            this.secundario.txtI_Hijos.Text = "";
            this.secundario.txtI_OtrosF.Text = "";
            this.secundario.txtI_Total.Text = "";

            this.secundario.txtE_Quiro.Text = "";
            this.secundario.txtE_Hipo.Text = "";
            this.secundario.txtE_Direc.Text = "";
            this.secundario.txtE_Lbca.Text = "";
            this.secundario.txtE_Fpens.Text = "";
            this.secundario.txtE_Ss.Text = "";
            this.secundario.txtE_Csindic.Text = "";
            this.secundario.txtE_Ispt.Text = "";
            this.secundario.txtE_Otros.Text = "";
            this.secundario.txtE_Alim.Text = "";
            this.secundario.txtE_Vestu.Text = "";
            this.secundario.txtE_Transp.Text = "";
            this.secundario.txtE_Renta.Text = "";
            this.secundario.txtE_Agua.Text = "";
            this.secundario.txtE_Luz.Text = "";
            this.secundario.txtE_Gas.Text = "";
            this.secundario.txtE_Tel.Text = "";
            this.secundario.txtE_Coleg.Text = "";
            this.secundario.txtE_Servid.Text = "";
            this.secundario.txtE_OtrosF.Text = "";
            this.secundario.txtE_Total.Text = "";

            this.secundario.txtObserv.Text = "";
            this.secundario.txtConclus.Text = "";
            this.secundario.txtDifer_I_E.Text = "";
        }

        private void txtExpediente_Leave_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtExpediente.Text))
            {
                if (expediente == txtExpediente.Text) return;
                expediente = txtExpediente.Text;
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

        private void txtExpediente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void frmSocioEconomico_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F2)
            {
                cerrar();
            }
            if (this.principalActiva)
            {
                if (this.ventanaPrincipal.dtggrid.Rows.Count == 0) {
                    h_sdepec sdepec = new h_sdepec();
                    int rowactual = this.ventanaPrincipal.dtggrid.Rows.Count;
                    if (e.KeyCode == Keys.Insert && !globales.boolConsulta)
                    {
                        if (this.ventanaPrincipal.dtggrid.Rows.Count != 0) return;

                        if (!this.conExpediente) {
                            globales.MessageBoxExclamation("No se puede insertar registros a la solicitud sin expediente","Aviso",globales.menuPrincipal);
                            return;
                        }

                        DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar una nuevo registro?", "Aviso", globales.menuPrincipal);
                        if (p == DialogResult.No) return;
                        this.esInsertar = true;

                        this.ventanaPrincipal.dtggrid.Rows.Insert(0);
                        this.ventanaPrincipal.dtggrid.Rows[0].Cells[0].Value = "";
                        this.ventanaPrincipal.dtggrid.Rows[0].Cells[1].Value = "";
                        this.ventanaPrincipal.dtggrid.Rows[0].Cells[2].Value = "";
                        this.ventanaPrincipal.dtggrid.Rows[0].Cells[3].Value = "";
                        this.ventanaPrincipal.dtggrid.Rows[0].Cells[4].Value = "";
                        this.ventanaPrincipal.dtggrid.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);

                        h_sdepec depec = new h_sdepec();
                        depec.expediente = Convert.ToInt32(txtExpediente.Text);
                        depec.sec = this.secuencia;

                        dbaseORM orm = new dbaseORM();
                        depec = orm.insert<h_sdepec>(depec, true);

                        this.ventanaPrincipal.dtggrid.Rows[0].Cells[5].Value = depec.id;
                        this.ventanaPrincipal.dtggrid.CurrentCell = this.ventanaPrincipal.dtggrid.Rows[rowactual].Cells[0];
                    }

                }
                this.esInsertar = false;
            }
            else {
                
            }
        }

        private void frmSocioEconomico_KeyPress(object sender, KeyPressEventArgs e)
        {
            char S;

            S = Char.ToUpper(e.KeyChar);

            e.KeyChar = S;
        }

        private void frmSocioEconomico_Shown(object sender, EventArgs e)
        {
            ActiveControl = button2;
        }
    }
}
