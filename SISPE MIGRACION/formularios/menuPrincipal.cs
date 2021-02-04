using SISPE_MIGRACION.codigo.herramientas.forms.menu;
using SISPE_MIGRACION.codigo.repositorios.datos;
using SISPE_MIGRACION.formularios.ADMINISTRACION;
using SISPE_MIGRACION.formularios.CATÁLOGOS;
using SISPE_MIGRACION.formularios.CONSTANCIAS.REGISTRO;
using SISPE_MIGRACION.formularios.ETICA;
using SISPE_MIGRACION.formularios.FINANCIEROS.CHEQUES;
using SISPE_MIGRACION.formularios.FINANCIEROS.CHEQUES.PRESUPUESTO;
using SISPE_MIGRACION.formularios.FINANCIEROS.PASACHEQ;
using SISPE_MIGRACION.formularios.Fondo_de_Pensiones;
using SISPE_MIGRACION.formularios.Fondo_de_Pensiones.DISKETTES;
using SISPE_MIGRACION.formularios.Fondo_de_Pensiones.DISKETTES.RESUMEN;
using SISPE_MIGRACION.formularios.MIGRACIÓN_CG;
using SISPE_MIGRACION.formularios.MIGRACIÓN_CG.CONSULTAS;
using SISPE_MIGRACION.formularios.MIGRACIÓN_CG.POLIZAS;
using SISPE_MIGRACION.formularios.NOMINAS.CATALOGOS;
using SISPE_MIGRACION.formularios.NOMINAS.CORREO_ELECTRONICO.EMAIL;
using SISPE_MIGRACION.formularios.NOMINAS.HISTORICO;
using SISPE_MIGRACION.formularios.NOMINAS.PENSION_ALIMENTICIA;
using SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA;
using SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.NOMINAS_ESPECIALES;
using SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.REGISTRO_DE_INCIDENCIAS;
using SISPE_MIGRACION.formularios.NOMINAS.REPORTES;
using SISPE_MIGRACION.formularios.NOMINAS.SUPERVIVENCIA;
using SISPE_MIGRACION.formularios.OFICIALIA_DE_PARTES;
using SISPE_MIGRACION.formularios.OTROS.SERVIDOR102;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CITAS;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.HIPOTECARIO;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.QUIROGRAFARIO;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.QUIROGRAFARIO.FONDO_GARANTIA.CAPTURA;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.REPORTES;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.VALIDACIONES;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA.REPORTES.SALDOS;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.PRINCIPAL_PH;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.REPORTES;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.TASAS;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.CAJA;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.PAGO_DE_MARCHA;
using SISPE_MIGRACION.formularios.RELOJ_CHECADOR;
using SISPE_MIGRACION.formularios.Seguros;
using SISPE_MIGRACION.formularios.sobres;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

//************************************************
//**       SECCIÓN DE CÓDIGO DONDÉ SE DECLARA   **
//**       LOS DELEGADOS...                     **
//************************************************
public delegate void setLista(List<Dictionary<string, object>> obj);
public delegate void setDiccionario(Dictionary<string, object> obj);
public delegate void setString(string cadena);
public delegate void regresa();

namespace SISPE_MIGRACION.formularios
{

    public partial class menuPrincipal : Form
    {
        private bool saliendo;
        private List<Dictionary<string, object>> objeto;
        private bool pasarMenu;
        public regresa regresar;
        public menuPrincipal()
        {
            InitializeComponent();
        }

        private void menuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void altasDeSolicitudesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void modificarSolicitudesToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void altasDeSolicitudesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            new frmAltas().ShowDialog();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmCatemplea().ShowDialog();
        }

        private void dependenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmdependencias().ShowDialog();
        }

        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmMovimientos().ShowDialog();
        }

        private void categoríasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmCategorias().ShowDialog();
        }

        private void proyectosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmProyecto().ShowDialog();
        }

        private void firmasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            globales.showModal(new frmFirmas());
        }

        private void prestacionesEconómicasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void actualizacionFechasDeChequesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmprogchq().ShowDialog();
        }

        private void verProgramaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmdiacheque().ShowDialog();
        }

        private void solicEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            globales.showModal(new SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes.frmSolicitudEntrega());
        }

        private void menuPrincipal_Load(object sender, EventArgs e)
        {
            //Código para traer datos generales del usuario



            string query = string.Empty;

            List<Dictionary<string, object>> resultado;

            lblVersion.Text = "Versión: " + Properties.Resources.version;

            string id = globales.id_usuario;
            query = $"select usuario,nombre,puesto from catalogos.usuarios where idusuario = {id}";
            resultado = globales.consulta(query);
            Dictionary<string, object> obj = resultado[0];
            string nombre = Convert.ToString(obj["nombre"]);
            string usuario = Convert.ToString(obj["usuario"]);
            string puesto = Convert.ToString(obj["puesto"]);
            lblUsuario.Text = usuario;
            lblNombre.Text = nombre;
            string server = Properties.Resources.servidor;
            int largo = server.Length;
            String Var_Sub = server.Substring((largo - 4), 4);
            objeto = (List<Dictionary<string, object>>)globales.getMenuUsuario();
            lblservidor.Text = Var_Sub;

            //Aquí se forma el menú dinamico

            Button boton = btnEjemplo;
            panelMenuprincipal.Controls.Clear();
            int posicionX = boton.Location.X;
            foreach (Dictionary<string, object> item in objeto)
            {
                List<Dictionary<string, object>> listaSubmenu = (List<Dictionary<string, object>>)item["submenu"];
                Button btnNuevo = new Button();
                btnNuevo.Size = boton.Size;
                btnNuevo.Location = new Point(posicionX, boton.Location.Y);
                btnNuevo.BackColor = boton.BackColor;
                btnNuevo.ForeColor = boton.ForeColor;
                btnNuevo.Font = boton.Font;
                btnNuevo.TextAlign = boton.TextAlign;
                btnNuevo.Margin = boton.Margin;
                btnNuevo.Padding = boton.Padding;
                btnNuevo.Cursor = Cursors.Hand;
                btnNuevo.Text = Convert.ToString(item["nombre"]).ToUpper();
                btnNuevo.Tag = Convert.ToString(item["id"]);
                if (listaSubmenu.Count == 0)
                    btnNuevo.Click += new EventHandler(elegir);
                else
                    btnNuevo.Click += new EventHandler(submenu);

                panelMenuprincipal.Controls.Add(btnNuevo);
                posicionX += 150;
            }

            this.ActiveControl = panelMenuprincipal.Controls[0];

        }

        private void elegir(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            string texto = boton.Text;
            escogerMenu(texto);
        }

        private frmFondo f;
        private void submenu(object sender, EventArgs e)
        {
            Button auxBotton = sender as Button;
            string id = Convert.ToString(auxBotton.Tag);
            Dictionary<string, object> obj = objeto.Where(o => Convert.ToString(o["id"]) == id).ToList<Dictionary<string, object>>()[0];
            List<Dictionary<string, object>> lista = (List<Dictionary<string, object>>)obj["submenu"];
            f = new frmFondo(this, lista, auxBotton.Text, escogerMenu, auxBotton.Text);
            f.KeyPreview = true;
            f.KeyDown += new KeyEventHandler(pasar);
            f.ShowDialog();
            int contador = 0;
            foreach (Dictionary<string, object> item in objeto)
            {
                if (Convert.ToString(item["id"]) == id)
                    break;
                contador++;
            }
            Button boton = new Button();
            try
            {
                if (!globales.derecha && globales.izquierda)
                {
                    globales.izquierda = false;
                    Dictionary<string, object> objAnterior = objeto[contador - 1];
                    boton.Tag = objAnterior["id"];
                    boton.Text = Convert.ToString(objAnterior["nombre"]).ToUpper();
                }
                else if (globales.derecha && !globales.izquierda)
                {
                    globales.derecha = false;
                    Dictionary<string, object> objSiguiente = objeto[contador + 1];
                    boton.Tag = objSiguiente["id"];
                    boton.Text = Convert.ToString(objSiguiente["nombre"]).ToUpper();
                }
                submenu(boton, null);
            }
            catch
            {

            }
        }

        private void pasar(object sender, KeyEventArgs e)
        {

        }

        private void escogerMenu(string nombreMenu)
        {
            string verdaderoTitulo = string.Empty;
            foreach (char c in nombreMenu.ToCharArray())
            {
                if (c >= 'A' && c <= 'Z')
                {
                    verdaderoTitulo += c;
                }


            }
            switch (globales.tipomenu)
            {
                case 1:
                    verdaderoTitulo = "NOMINASJUB" + verdaderoTitulo;
                    break;
                case 2:
                    verdaderoTitulo = "NOMINASACTIVO" + verdaderoTitulo;
                    break;
                case 3:
                    verdaderoTitulo = "CG" + verdaderoTitulo;
                    break;
            }
            System.Diagnostics.Debug.WriteLine(verdaderoTitulo);
            switch (verdaderoTitulo)
            {
                case "CATLOGOSMOVIMIENTOS":
                    abrirFormulario<frmMovimientos>();
                    break;
                case "CATLOGOSDEPENDENCIAS":
                    abrirFormulario<frmCatalogoDependencia>();
                    break;
                case "CATLOGOSEMPLEADOS":
                    abrirFormulario<frmCatemplea>();
                    break;
                case "CATLOGOSCATEGORIAS":
                    abrirFormulario<frmCategorias>();
                    break;
                case "CATLOGOSPROYECTOS":
                    abrirFormulario<frmProyecto>();
                    break;
                case "CATLOGOSFIRMAS":
                    abrirFormulario<frmFirmas>();
                    break;
                case "FONDODEPENSIONESAPORTACIONES":
                    abrirFormulario<frmaportaciones>();
                    break;
                case "FONDODEPENSIONESCONSULTA":
                    abrirFormulario<frmconsultas>();
                    break;
                case "FONDODEPENSIONESREPORTES":
                    globales.showModal(new frmreporlisaporta());
                    break;
                case "FONDODEPENSIONESDISKETTEACTUALIZAR":
                    abrirFormulario<frmActualizar>();
                    break;
                case "FONDODEPENSIONESREPORTEDESALDOSREPORTEDESALDOS":
                    globales.showModal(new frmreposaldos());
                    break;
                case "FONDODEPENSIONESREPORTEDESALDOSREPORTEDEPENDIENTES":
                    globales.showModal(new frmsaldospendientes());
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPQGENERARSOLICITUDES":
                    abrirFormulario<frmAltas>();
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPQACTUALIZACINFECHASDECHEQUES":
                    this.abrirFormulario<frmprogchq>();
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPQUTILERIASPROGRAMACINDECHEQUES":
                    abrirFormulario<frmdiacheque>();
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPQREPORTESSOLICENTREGA":
                    globales.showModal(new SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes.frmSolicitudEntrega());
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPQREPORTESPAGARES":
                    globales.showModal(new SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes.frmPagares());
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPQREPORTESALFBETICO":
                    globales.showModal(new SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes.frmAlfabet());
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPQREPORTESMONTOS":
                    globales.showModal(new SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.reportes.frmMontos());
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPQTASAS":
                    globales.showModal(new frmTasas());
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPHEXPEDIENTE":
                    abrirFormulario<frmexpediente>();
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPHSOLICITUDES":
                    abrirFormulario<frmSolicitudesHipotecario>();
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPHDOCUMENTOS":
                    frmDocumentos documentos = new frmDocumentos();
                    documentos.ventana = this;
                    globales.showModal(documentos);
                    break;
                case "PRESTACIONESECONMICASESTADOSDECUENTACONSULTAQUIROGRAFARIOS":
                    globales.boolConsulta = true;
                    abrirFormulario<frmestados>();
                    break;
                case "PRESTACIONESECONMICASESTADOSDECUENTACONSULTAHIPOTECARIOS":
                    globales.boolConsulta = true;
                    abrirFormulario<frmestadosHipo>();
                    break;
                case "PRESTACIONESECONMICASESTADOSDECUENTACONSULTACONSULTAPDEV":
                    new frmdevol().ShowDialog();
                    break;
                case "PRESTACIONESECONMICASESTADOSDECUENTAQUIROGRAFARIO":
                    globales.boolConsulta = false;
                    abrirFormulario<frmestados>();
                    break;
                case "PRESTACIONESECONMICASESTADOSDECUENTAREPORTESSALDOS":
                    globales.showModal(new frmsaldos());
                    break;
                case "PRESTACIONESECONMICASESTADOSDECUENTAREPORTESESTADOSDECUENTA":
                    globales.showModal(new SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA.REPORTES.ESTADOS_DE_CUENTA.frmEstadosCuenta());
                    break;
                case "PRESTACIONESECONMICASESTADOSDECUENTAREPORTESREGISTROSMANUALES":
                    globales.showModal(new PRESTACIONES_ECON.ESTADOS_DE_CUENTA.REPORTES.REGISTROS_MANUALES.frmSeleccionarRegistro());
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROQUIROGRAFARIOGENERARPORFECHA":
                    globales.showModal(new frmGenerarPorFecha());
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROQUIROGRAFARIOALTASCAMBIOS":
                    abrirFormulario<frmAltasCambios>(); ;
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROQUIROGRAFARIOACTUALIZARRELLABORAL":
                    globales.showModal(new frmActualizarRelLaboral());
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROQUIROGRAFARIOVALIDARSITUACINLABORAL":
                    globales.showModal(new validarSituacionLaboral());
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROQUIROGRAFARIOFONDODEGARANTIADEVFDODEGARANTIA":
                    globales.showModal(new frmDevolucionFondo());
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROQUIROGRAFARIOFONDODEGARANTIACONCEPTO":

                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROHIPOTECARIOSGENERARPORFECHA":
                    generarPorFecha();
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROHIPOTECARIOSALTASCAMBIOS":
                    abrirFormulario<frmAltasCambiosHipote>();
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROHIPOTECARIOSACTUALIZARRELLABORAL":
                    globales.showModal(new frmActualizarRelLaboral(true));
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROHIPOTECARIOSVALIDARSITUACINLABORAL":
                    globales.showModal(new validarSituacionLaboral(true));
                    break;
                case "FONDODEPENSIONESCONSULTAACLARACIONES":
                    new frmConsultaAclaraciones().ShowDialog();
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROREPORTES":
                    globales.esReporte = false;
                    globales.showModal(new frmMenuOpciones());
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTRODISKETTES":
                    globales.esReporte = true;
                    globales.showModal(new SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.REPORTES.frmMenuOpciones(true));
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROHIPOTECARIOPATY":
                    new hipotecarios().ShowDialog();
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROVALIDACIONESSINPAGOS":
                    globales.showModal(new frmSinPagos());
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROVALIDACIONESTERMINARDEPAGAR":
                    globales.showModal(new frmSinPagos(true));
                    break;
                case "PRESTACIONESECONMICASCONTROLYREGISTROVALIDACIONESSINESTADOSDECTA":
                    globales.showModal(new frmSalida());
                    break;
                case "PRESTACIONESECONMICASSEGUROSGENERACINDEARCHIVO":
                    globales.showModal(new frmseguro());
                    break;
                case "PRESTACIONESECONMICASGASTOSDEFUNERALALTASYCAMBIOS":
                    abrirFormulario<frmpagomarcha>();
                    break;
                case "PRESTACIONESECONMICASPAGOPORCAJA":
                    globales.showModal(new frmOpcionesPagoCaja());
                    break;
                case "ACERCADE":
                    globales.showModal(new acerca());
                    break;
                case "ADMINISTRACINADMINISTRACINDEUSUARIOS":
                    abrirFormulario<frmGestionUsuarios>();
                    break;
                case "FONDODEPENSIONESACLARACIONES":
                    abrirFormulario<frmaclaraciones>();

                    break;
                case "PRESTACIONESECONMICASSEGUROSINCREMENTARSERIES":
                    globales.showModal(new frmincrementarserie());
                    break;
                case "PRESTACIONESECONMICASSEGUROSIMPORTARINFORMACIN":
                    globales.showModal(new frmimportarseguros());
                    break;

                case "PRESTACIONESECONMICASSEGUROSACTUALIZACIN":
                    globales.showModal(new frmactualizaseguro());
                    break;

                case "PRESTACIONESECONMICASCONTROLYREGISTROQUIROGRAFARIOFONDODEGARANTIACAPTURA":
                    globales.showModal(new frmFondoGarantia());
                    break;
                case "PRESTACIONESECONMICASSEGUROSREPORTESGENERAR":
                    globales.showModal(new frmgenerarseguro());
                    break;
                case "FONDODEPENSIONESCONSTANCIAREPORTEDECONSTANCIAS":
                    globales.showModal(new frmreporconsta());
                    break;
                case "NMINASENVOELECTRNICOSOBRES":
                    globales.showModal(new frmsobres());
                    break;
                case "PRESTACIONESECONMICASSEGUROSLTIMOSASEGURADOS":
                    globales.showModal(new frmultimoasegurado());
                    break;
                case "CATLOGOSSOBRES":
                    globales.showModal(new frmsobres());
                    break;
                case "FONDODEPENSIONESAPORTACIONESDELAOAL":
                    abrirFormulario<frmAportacionesViejas>();
                    break;
                case "NMINASENVOELECTRNICOENVODECORREOELECTRNICO":
                    globales.showModal(new frmAutentificacion());
                    break;
                case "PRESTACIONESECONMICASSEGUROSSEGUROSEDADYSALDO":
                    globales.showModal(new frmseguro());
                    break;
                case "FONDODEPENSIONESDISKETTEREPORTES":
                    globales.showModal(new frmFondoReportes());
                    break;
                case "NMINASENVOELECTRNICOCARGADEARCHIVOSPARAGENERACINDESOBRES":
                    abrirFormulario<cargarInformacionSobres>();
                    break;
                case "FONDODEPENSIONESCONSTANCIAGENERARCONSTANCIA":
                    abrirFormulario<frmconstancias>();
                    break;
                case "FONDODEPENSIONESCONSTANCIAREIMPRIMIRCONSTANCIAS":
                    globales.showModal(new imprimirconsta());
                    break;
                case "FINANCIEROSPASACHEQUESDATOSGENERALES":
                    abrirFormulario<frmDatosGenerales>(); ;
                    break;
                case "FONDODEPENSIONESDISKETTERESUMEN":
                    abrirFormulario<frmResumen>();
                    break;
                case "FINANCIEROSCHEQUESDATOSGENERALESFIRMAS":
                    globales.showModal(new frmfirmas());
                    break;
                case "FINANCIEROSCHEQUESDATOSGENERALESCUENTAS":
                    new frmcuentas().ShowDialog();
                    break;
                case "FINANCIEROSCHEQUESALTAS":
                    new frmaltaschq().ShowDialog();
                    break;
                case "FINANCIEROSPASACHEQUESIMPORTAROBTENER":
                    this.Cursor = Cursors.WaitCursor;
                    importarPrestacionesEconomicasACheques();
                    this.Cursor = Cursors.Default;
                    break;
                case "FINANCIEROSCHEQUESREPORTESCHEQUESACAJA":
                    new frmreportecaja().ShowDialog();
                    break;
                case "FINANCIEROSCHEQUESCIERRE":
                    new frmcierrechq().ShowDialog();
                    break;
                case "FINANCIEROSPASACHEQUESIMPORTARCONSULTAR":
                    globales.showModal(new frmVisualizarCheques());
                    break;
                case "HERRAMIENTAS":

                    break;
                case "FINANCIEROSCHEQUESCONVERTIDORSQL":
                    new ventanaPrincipal().ShowDialog();
                    break;
                case "FINANCIEROSCHEQUESREPORTESEMISINDECHEQUES":
                    new frmrepocheques().ShowDialog();
                    break;
                case "FINANCIEROSPRESUPUESTODATOSGENERALESPRECUENTASPRE":
                    new frmcuentapresu().ShowDialog();
                    break;
                case "FINANCIEROSPRESUPUESTODATOSGENERALESPREFIRMASPRE":
                    new frmfirmas().ShowDialog();
                    break;
                case "FINANCIEROSPRESUPUESTOALTASPRE":
                    new frmaltapresupuesto().ShowDialog();
                    break;
                case "FINANCIEROSPRESUPUESTOREPORTESPREEMISINDECHEQUESPRE":
                    new frmrepochqpresup().ShowDialog();
                    break;
                case "FINANCIEROSPRESUPUESTOREPORTESPRECHEQUESACAJAPRE":
                    new frmcajapresupuesto().ShowDialog();
                    break;
                case "FONDODEPENSIONESACTUARIO":
                    globales.showModal(new frmactuario());
                    break;

                case "FONDODEPENSIONESFUSIONAR":
                    abrirFormulario<frmaclaraciones>();
                    break;

                case "PRESTACIONESECONMICASESTADOSDECUENTAHIPOTECARIO":
                    globales.boolConsulta = false;
                    abrirFormulario<frmestadosHipo>();
                    break;
                case "FINANCIEROSPASACHEQUESEXPORTAR":
                    generarDes();
                    generarPol();
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPHTASAS":
                    globales.showModal(new frmTasaHipote());
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPHREPORTEHIPOTECARIOS":
                    globales.showModal(new frmOtorgadosHip());
                    break;

                case "PRESTACIONESECONMICASOTORGAMIENTOPHCAMBIODESTATUS":
                    globales.showModal(new frmFecha_statust());
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPHFECHADEAUTORIZACIN":
                    globales.showModal(new frmFechaAutorizacion());
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPHCOMPLEMENTOPCONSEJO":
                    globales.showModal(new frmComplementoAutorizacion());
                    break;

                case "PRESTACIONESECONMICASOTORGAMIENTOPHREPORTES":
                    globales.showModal(new frmReportes(this));
                    break;
                case "PRESTACIONESECONMICASESTADOSDECUENTAHIPOTECARIOESTADOSDECUENTA":
                    globales.boolConsulta = false;
                    abrirFormulario<frmestadosHipo>();
                    break;
                case "PRESTACIONESECONMICASESTADOSDECUENTAHIPOTECARIOINTERESESMORATORIOS":
                    calcularInteresesMoratorios();
                    break;
                case "NOMINASJUBSUPERVIVENCIAFIRMASUPERVIVENCIA":
                    abrirFormulario<frmSupervivencia>();
                    break;
                case "NOMINASJUBCATLOGOSGESTINUSUARIOSFIRMAS":
                    frmFirmasNomina ventana1 = new frmFirmasNomina();
                    ventana1.ShowDialog();
                    break;
                case "NOMINASJUBCATLOGOSGESTINUSUARIOSPERCEPCIONESYDEDUCCIONES":
                    frmPerded ventana2 = new frmPerded();
                    ventana2.ShowDialog();
                    break;
                case "NOMINASJUBNOMINASSOBRESPAGO":
                    abrirFormulario<frmSobres>();
                    break;

                case "NOMINASJUBNOMINASGENERACINLISTADOS":
                    frmgenlist ventana4 = new frmgenlist();
                    globales.showModal(ventana4);
                    break;

                case "NOMINASJUBNOMINASDATOSGENCDULA":
                    frminfojub ventana5 = new frminfojub();
                    ventana5.ShowDialog();
                    break;

                case "NOMINASJUBSUPERVIVENCIALISTADOS":
                    globales.showModal(new frmListadosSupervicencia());
                    break;

                case "NOMINASJUBCATLOGOSGESTINUSUARIOSALTADEJUBILADOS":
                    globales.showModal(new frminfojub());
                    break;


                case "NOMINASJUBNOMINASADMINISTRACINDENMINA":
                    globales.showModal(new frmconsulmodfnomina());
                    break;

                case "NOMINASJUBCATLOGOSPERCEPCIONESYDEDUCCIONES":
                    globales.showModal(new frmPerded());
                    break;

                case "NOMINASJUBNOMINASVALIDACININCIDENCIAS":
                    globales.showModal(new frmValidacionDescuentosFondo());
                    break;

                case "NOMINASJUBNOMINASGENERACINNOMINA":
                    globales.showModal(new frmElaboracionNomina());
                    break;
                case "NOMINASJUBNOMINASHERRAMIENTASADICIONALNMINAESPECIAL":
                    globales.showModal(new frmNomEspecial());
                    break;
                case "NOMINASJUBNOMINASHERRAMIENTASCREDENCIALES":
                    globales.showModal(new frmCredencial());
                    break;

                case "NOMINASJUBNOMINASHERRAMIENTASADICIONALMOVIMIENTOSMASIVOS":
                    globales.showModal(new frmMasivoClave());
                    break;
                case "NOMINASJUBNOMINASHERRAMIENTASADICIONALACTIVACINTEMPORAL":
                    globales.showModal(new frmActivacionTempo());
                    break;


                case "NOMINASJUBNOMINASCOMPARATIVONOMINA":
                    globales.showModal(new frmComparativoNomina());
                    break;

                case "NOMINASJUBNOMINASHERRAMIENTASADICIONALAVANCEDEPRESTAMOS":
                    globales.showModal(new frmAvanceSerie());
                    break;

                case "CONSTANCIASREGISTROCONSTANCIALICENCIA":

                    abrirFormulario<frmConstanciaLicenciacs>();

                    break;
                case "CONSTANCIASREGISTROCONSTANCIAGENERAL":
                    abrirFormulario<frmConstanciaGeneral>();
                    break;

                case "CONSTANCIASREPORTESPORFECHA":
                    abrirFormulario<frmPorfecha>();
                    break;

                case "CONSTANCIASHERRAMIENTASUSUARIOS":
                    abrirFormulario<CONSTANCIAS.HERRAMIENTAS.frmUsuarios>();
                    break;

                case "CONSTANCIASHERRAMIENTASDATOS":
                    abrirFormulario<CONSTANCIAS.HERRAMIENTAS.frmDatos>();
                    break;

                case "NOMINASJUBNOMINASSALDOSDELFONDODEPENSIONES":
                    globales.showModal(new frmSaldoFondo());
                    break;


                case "NOMINASJUBCATLOGOSFIRMASDENOM":
                    globales.showModal(new frmFirmasNomina());
                    break;
                case "NOMINASJUBSUPERVIVENCIAPAGOSRETRO":
                    abrirFormulario<frmListadoRetroactivocs>();

                    break;
                case "OFICIALIADEPARTESREGISTRO":
                    abrirFormulario<frmPrincipalOficialia>();

                    break;

                case "NOMINASJUBHISTORICOHISTORICONOMINA":
                    abrirFormulario<frmHistoricoNomina>();

                    break;


                case "NOMINASJUBHISTORICOHISTORICOLISTADOS":
                    abrirFormulario<frmHistoricoListados>();
                    break;


                case "NOMINASJUBSUPERVIVENCIAVERIFICACINSUPERVIVENCIA":
                    abrirFormulario<frmListadoNoNomina>();
                    break;
                case "OFICIALIADEPARTESSEGUIMIENTO":
                    abrirFormulario<frmRecepción>();
                    break;

                case "NOMINASJUBPENSIONALIMENTICIAACTUALIZAR":
                    abrirFormulario<frmActualizacionNomina>();
                    break;
                case "NOMINASJUBPENSIONALIMENTICIAREPORTES":
                    globales.showModal(new frmReporteRelacion());
                    break;
                case "OFICIALIADEPARTESREPORTESPORFECHA":
                    // globales.showModal(new frmReporteXdía());
                    break;

                case "RELOJREGISTRO":
                    globales.showModal(new frmRelojDatosGen());
                    break;
                case "RELOJEDICIN":
                    globales.showModal(new frmAjustesReloj());
                    break;
                case "RELOJREPORTESPORFECHA":
                    globales.showModal(new frmReporteReloj());
                    break;
                case "NOMINASJUBNOMINASHERRAMIENTASADICIONALAGUINALDO":
                    globales.showModal(new frmAguinaldo());
                    break;
                case "CGCATALOGOSCATALOGOCUENTAS":

                    SISPE_MIGRACION.formularios.MIGRACIÓN_CG.CUENTAS.frmCuentas cuentas = new MIGRACIÓN_CG.CUENTAS.frmCuentas();
                    globales.showModal(cuentas);
                    break;
                case "NOMINASJUBOFICIALIADEPARTESSEGUIMIENTO":
                    abrirFormulario<frmRecepción>();
                    break;
                case "WEBPROGRAMARCITAS":
                    abrirFormulario<frmCitas_web>();
                    break;
                case "PRESTACIONESECONMICASOTORGAMIENTOPQSALDOSPEND":
                    abrirFormulario<frmSaldoPend>();
                    break;

                case "NOMINASJUBWEBPROGRAMARCITAS":
                    abrirFormulario<frmCitas_web>();
                    break;


                case "ADMINISTRACINACTULIZARPROGRAMA":
                    frmActualizador actu = new frmActualizador();
                    globales.showModal(actu);
                    break;


                case "CGCONSULTASLIBROMAYOR":
                    abrirFormulario<frmLibroMayor>();
                    break;
                case "CGPOLIZASNUEVAPOLIZA":
                    abrirFormulario<frmPolizas>();
                    break;




                case "ADMINISTRACINCONFIGURACINSISTEMA":
                    abrirFormulario<frmConfiguracionSistema>();
                    break;
                case "CGCONSULTASLIBRODIARIO":
                    abrirFormulario<frmLibrodiario>();
                    break;
                case "CGCONSULTASESTADOSITFINANCIERA":
                    globales.estadoResultado = false;
                    abrirFormulario<frmBalanzaGeneral>();

                    break;
                case "CGCONSULTASESTADORESUL":

                    globales.estadoResultado = true;
                    abrirFormulario<frmBalanzaGeneral>();

                    break;
                default:
                    break;
                case "ETICAREGISTRO":
                    abrirFormulario<frmEtica>();
                    break;
                case "ETICASEGUMIENTO":
                    abrirFormulario<frmSeguimiento>();
                    break;
                case "CGCONSULTASBALANZACOMPRO":
                    abrirFormulario<frmBalanzaComprobacion>();
                    break;
                case "CGCONSULTASMOVXDOCFUENTE":
                    abrirFormulario<frmMovimientosXdocumento>();
                    break;

                    


                    //NOMINASJUBNOMINASHERRAMIENTASADICIONALDAMADRESYTILES


            }
        }

        private void generarPorFecha()
        {
            DateTime time = DateTime.Now;
            time = time.AddMonths(1);
            DateTime desde = new DateTime(time.Year, time.Month, 15);

            time = desde;
            time = time.AddMonths(1);
            time = new DateTime(time.Year, time.Month, 1);

            DateTime hasta = time.AddDays(-1);

            string mensaje = $"¿Deseas importar las solicitudes de P.H para descontar el {string.Format("{0:d}", desde)} y el {string.Format("{0:d}", hasta)}?";
            DialogResult dialogo = globales.MessageBoxQuestion(mensaje, "Estado de cuenta", globales.menuPrincipal);
            if (dialogo == DialogResult.No) return;

            string query = "create temp table uniontablas1 as (select (concat(expediente,sec)::NUMERIC) as folio,expediente,sec,min(f_recibo) as f_emischeq from datos.h_semisi where importe is not null or importe <> 0 group by expediente,sec " +
                " union select (concat(expediente,sec)::NUMERIC) as folio,expediente,sec,min(f_pagare) as f_emischeq from datos.h_spagar where f_pagare is not null group by expediente,sec); create temp table uniontablas as select folio,expediente,sec,min(f_emischeq) as f_emischeq from uniontablas1  group by folio,expediente,sec; " +
                " create temp table exportarfolios as select folio from uniontablas EXCEPT select folio from datos.p_edocth;  " +
                " create temp table crosstab as select uniontablas.* from exportarfolios inner join uniontablas on exportarfolios.folio = uniontablas.folio; " +
                " select crosstab.*,p.rfc,p.nombre_em,p.direccion,p.proyecto,p.tipo_rel,p.secretaria,p.descripcion,(p.ant_a * 24) as antig_q,now() as f_primdesc,hs.plazoa as plazo,hs.tot_prest as importe,hs.f_solicitud,tot_unit as imp_unit,plazo as tipo_pago from crosstab inner join datos.p_hipote  p on crosstab.expediente = p.folio " +
                " inner join datos.h_solici  hs on hs.expediente = crosstab.expediente and hs.sec = crosstab.sec; ";

            dbaseORM orm = new dbaseORM();
            List<Dictionary<string, object>> resultado = orm.query(query);


            List<p_edocth> listaEdocth = new List<p_edocth>();
            List<solicitud_dependencias> listaSolicitud = new List<solicitud_dependencias>();
            foreach (Dictionary<string, object> item in resultado)
            {
                string tipoPago = Convert.ToString(item["tipo_pago"]).ToUpper();
                if (tipoPago == "M")
                {
                    item["f_primdesc"] = hasta;
                    item["plazo"] = globales.convertInt(Convert.ToString(item["plazo"])) * 12;
                }
                else
                {
                    item["f_primdesc"] = desde;
                    item["plazo"] = globales.convertInt(Convert.ToString(item["plazo"])) * 24;
                }


                p_edocth obj = new p_edocth();
                obj.folio = globales.convertInt(Convert.ToString(item["folio"]));
                obj.rfc = Convert.ToString(item["rfc"]);
                obj.nombre_em = Convert.ToString(item["nombre_em"]);
                obj.direccion = Convert.ToString(item["direccion"]);
                obj.proyecto = Convert.ToString(item["proyecto"]);
                obj.secretaria = Convert.ToString(item["secretaria"]);
                obj.descripcion = Convert.ToString(item["descripcion"]);
                obj.f_solicitud = globales.convertDatetime(Convert.ToString(item["f_solicitud"]));
                obj.f_emischeq = globales.convertDatetime(Convert.ToString(item["f_emischeq"]));
                obj.f_primdesc = globales.convertDatetime(Convert.ToString(item["f_primdesc"]));
                obj.antig_q = globales.convertInt(Convert.ToString(item["antig_q"]));
                obj.tipo_pago = Convert.ToString(item["tipo_pago"]).Substring(0, 1);
                obj.plazo = globales.convertDouble(Convert.ToString(item["plazo"]));
                obj.imp_unit = globales.convertDouble(Convert.ToString(item["imp_unit"]));
                obj.importe = globales.convertDouble(Convert.ToString(item["importe"]));

                obj.f_primdesc = obj.f_primdesc.Day == 31 ? obj.f_primdesc.AddDays(-1): obj.f_primdesc;

                listaEdocth.Add(obj);


                solicitud_dependencias dependencia = new solicitud_dependencias();
                dependencia.folio = obj.folio;
                dependencia.tipo_mov = "AN";
                dependencia.sec = "1";
                dependencia.tipo_rel = Convert.ToString(item["tipo_rel"]);
                dependencia.f_descuento = obj.f_primdesc;
                dependencia.numdesc = 2;
                dependencia.totdesc = obj.plazo;
                dependencia.imp_unit = obj.imp_unit;
                dependencia.rfc = obj.rfc;
                dependencia.nombre_em = obj.nombre_em;
                dependencia.proyecto = obj.proyecto;
                dependencia.t_prestamo = "H";

                dependencia.f_descuento = dependencia.f_descuento.Day == 31 ? dependencia.f_descuento.AddDays(-1) : dependencia.f_descuento;
                listaSolicitud.Add(dependencia);

            }

            if (listaEdocth.Count != 0) {
                bool result = orm.insertAll<p_edocth>(listaEdocth);
                if (result)
                {
                    result = orm.insertAll<solicitud_dependencias>(listaSolicitud);
                    if (result)
                    {
                        globales.MessageBoxSuccess("Estados de cuenta generados correctamente", "Aviso", globales.menuPrincipal);
                    }
                }
            }

        }

        private void calcularInteresesMoratorios()
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Desea calcular los intereses de este mes?", "Aviso", this);
            if (dialogo == DialogResult.No) return;

            this.Cursor = Cursors.WaitCursor;

            dbaseORM orm = new dbaseORM();

            double tasa = 0.025;

            string query = "create temp table edo as select * from datos.p_edocth where int_morat > 0 and (int_conv is null or int_conv = 0);" +
                "create temp table des as select folio,sum(importe) as pagado from datos.descuentos where t_prestamo = 'H' group by folio;" +
                "create temp table pagado as select edo.*,des.pagado from edo left join des on edo.folio = des.folio;" +
                "create temp table saldo as select *,ROUND((((COALESCE(importe,0)::numeric) - (COALESCE(pagado,0)::numeric))::numeric),2) as saldo from pagado;" +
                $"select *,ROUND((saldo * {tasa}),2) as calculotasa from saldo where saldo > 0;";

            List<Dictionary<string, object>> resultado = orm.query(query);
            List<descuentos> listaDescuentos = new List<descuentos>();
            foreach (Dictionary<string, object> item in resultado)
            {
                descuentos d = new descuentos();
                d.folio = globales.convertInt(Convert.ToString(item["folio"]));
                d.f_descuento = DateTime.Now;
                d.numdesc = 1;
                d.totdesc = 1;
                d.importe = (-1) * globales.convertDouble(Convert.ToString(item["calculotasa"]));
                d.proyecto = "MOR.CAUS.";
                d.fum = DateTime.Today;
                d.t_prestamo = "M";
                listaDescuentos.Add(d);
            }

            bool respuesta = orm.insertAll<descuentos>(listaDescuentos);

            if (respuesta)
            {
                query = "create temp table edo as select * from datos.p_edocth where int_morat > 0 and int_conv > 0;" +
                "create temp table des as select folio,sum(importe) as pagado from datos.descuentos where t_prestamo = 'H' group by folio;" +
                "create temp table pagado as select edo.*,des.pagado from edo left join des on edo.folio = des.folio;" +
                "create temp table saldo as   select *,ROUND((((COALESCE(importe,0)::numeric) - (COALESCE(pagado,0)::numeric))::numeric),2) as saldo from pagado;" +
                $"select *,((int_conv / 100)*saldo) as calculotasa from saldo where saldo > 0;";

                resultado = orm.query(query);
                listaDescuentos = new List<descuentos>();
                foreach (Dictionary<string, object> item in resultado)
                {
                    descuentos d = new descuentos();
                    d.folio = globales.convertInt(Convert.ToString(item["folio"]));
                    d.f_descuento = DateTime.Now;
                    d.numdesc = 1;
                    d.totdesc = 1;
                    d.importe = (-1) * globales.convertDouble(Convert.ToString(item["calculotasa"]));
                    d.proyecto = "MOR.CAUS.";
                    d.fum = DateTime.Today;
                    d.t_prestamo = "M";
                    listaDescuentos.Add(d);
                }



                respuesta = orm.insertAll<descuentos>(listaDescuentos);

                if (respuesta)
                    globales.MessageBoxSuccess("Se actualizaron intereses moratorios de este mes", "Aviso", globales.menuPrincipal);
            }
            else
            {
                globales.MessageBoxError("Hubo un problema al actualizar intereses moratorios con int_conv en blanco.Contactar a sistemas", "Aviso", globales.menuPrincipal);
            }

            this.Cursor = Cursors.Default;
        }

        private void generarPol()
        {
            try
            {
                string query = "select * from financieros.pol";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                query = string.Empty;

                string directorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\pol.sql";
                StreamWriter escribir = new StreamWriter(directorio);

                foreach (Dictionary<string, object> item in resultado)
                {
                    string entidad = Convert.ToString(item["entidad"]);
                    string subsistema = Convert.ToString(item["subsistema"]);
                    string polmes = Convert.ToString(item["polmes"]);
                    string poltipo = Convert.ToString(item["poltipo"]);
                    string polnumero = Convert.ToString(item["polnumero"]);
                    string fecha = string.Format("{0:d}", item["fecha"]);
                    string cuenta = Convert.ToString(item["cuenta"]);
                    string naturaleza = Convert.ToString(item["naturaleza"]);
                    string importe = Convert.ToString(item["importe"]);
                    string grupo = Convert.ToString(item["grupo"]);
                    string prefijo = Convert.ToString(item["prefijo"]);
                    string doctipo = Convert.ToString(item["doctipo"]);
                    string docnumero = Convert.ToString(item["docnumero"]);
                    string docfecha = string.Format("{0:d}", item["docfecha"]);
                    string docrfc = Convert.ToString(item["docrfc"]);
                    string doccomentario = Convert.ToString(item["doccomentario"]);
                    string docgrupo = Convert.ToString(item["docgrupo"]);
                    string status = Convert.ToString(item["status"]);
                    string linea = Convert.ToString(item["linea"]);

                    string sentencia = "insert into Pol values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}');";
                    sentencia = string.Format(sentencia, entidad, subsistema, polmes, poltipo, polnumero,
                       fecha, cuenta, naturaleza, importe, grupo, prefijo, doctipo, docnumero, docfecha,
                       docrfc, doccomentario, docgrupo, status, linea);
                    escribir.WriteLine(sentencia);
                    escribir.NewLine = "\n";

                }
                escribir.Close();

                globales.MessageBoxSuccess("Pol exportado correctamente", "Aviso", globales.menuPrincipal);
            }
            catch
            {
                globales.MessageBoxError("Error en exportar Pol", "Aviso", globales.menuPrincipal);
            }
        }

        private void generarDes()
        {
            try
            {
                string query = "select * from financieros.des;";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                query = string.Empty;
                string directorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\des.sql";
                StreamWriter escribir = new StreamWriter(directorio);
                foreach (Dictionary<string, object> item in resultado)
                {

                    string entidad = Convert.ToString(item["entidad"]);
                    string subsistema = Convert.ToString(item["subsistema"]);
                    string polmes = Convert.ToString(item["polmes"]);
                    string poltipo = Convert.ToString(item["poltipo"]);
                    string polnumero = Convert.ToString(item["polnumero"]);
                    string fecha = string.Format("{0:d}", item["fecha"]);
                    string debe = Convert.ToString(item["debe"]);
                    string haber = Convert.ToString(item["haber"]);
                    string concepto = Convert.ToString(item["concepto"]);
                    string grupo = Convert.ToString(item["grupo"]);
                    string prefijo = Convert.ToString(item["prefijo"]);
                    string elaboro = Convert.ToString(item["elaboro"]);
                    string status = Convert.ToString(item["status"]);

                    string sentencia = "insert into Des values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},'{8}','{9}','{10}','{11}','{12}');";
                    sentencia = string.Format(sentencia, entidad, subsistema, polmes, poltipo, polnumero, fecha, debe, haber, concepto,
                        grupo, prefijo, elaboro, status);
                    escribir.WriteLine(sentencia);
                    escribir.NewLine = "\n";

                }
                escribir.Close();
                globales.MessageBoxSuccess("Des exportado correctamente", "Aviso", globales.menuPrincipal);

            }
            catch
            {
                globales.MessageBoxError("Error en exportar Des", "Aviso", globales.menuPrincipal);
            }
        }

        internal void abrirFormulario<MiForm>() where MiForm : Form, new()
        {

            Form formulario = panel1.Controls.OfType<MiForm>().FirstOrDefault();
            foreach (Form item in panel1.Controls)
            {
                item.Close();
            }
            if (formulario != null)
                formulario.Close();
            formulario = new MiForm();
            formulario.TopLevel = false;
            formulario.Dock = DockStyle.Fill;
            formulario.Location = new Point();
            formulario.BringToFront();
            panel1.Controls.Add(formulario);
            formulario.Show();
        }


        private ToolStripMenuItem[] formarSubMenus(List<Dictionary<string, object>> resultado, string nombre)
        {
            ToolStripMenuItem[] lista = new ToolStripMenuItem[resultado.Count];
            int contador = 0; ///
            foreach (Dictionary<string, object> item in resultado)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem();
                menu.Text = Convert.ToString(item["nombre"]);
                menu.Font = new Font("Perpetua", 12, FontStyle.Bold);
                menu.DropDownItems.AddRange(formarSubMenus((List<Dictionary<string, object>>)item["submenu"], nombre + unirCadenas(item["nombre"])));
                menu.Name = nombre + unirCadenas(item["nombre"]);
                //menu.Click += new System.EventHandler(escogerMenu);
                lista[contador] = menu;
                contador++;
            }
            return lista;
        }
        private string unirCadenas(object cadena)
        {
            string aux = string.Empty;
            aux = Convert.ToString(cadena);
            string vacia = "";
            foreach (char item in aux.ToCharArray())
            {
                if ((item >= 'A' && item <= 'Z') || (item >= 'a' && item <= 'z'))
                {
                    vacia += item;
                }
            }
            return vacia;
        }

        private void importarPrestacionesEconomicasACheques()
        {

            //Ventana de dialogo para escoger que es lo que se importara
            frmEscogerImportacion seleccion = new frmEscogerImportacion();
            seleccion.ShowDialog();
            if (seleccion.boolCerrar) return;


            //Se elige el numero de cheque y numero de poliza a continuar..
            string query = "select numcheque,numpoliz from financieros.datos";
            List<Dictionary<string, object>> maximos = globales.consulta(query);
            int numCheque = string.IsNullOrWhiteSpace(Convert.ToString(maximos[0]["numcheque"])) ? 1 : Convert.ToInt32(maximos[0]["numcheque"]);
            int numPoliza = string.IsNullOrWhiteSpace(Convert.ToString(maximos[0]["numpoliz"])) ? 1 : Convert.ToInt32(maximos[0]["numpoliz"]);

            //Se selecciona el número de folios a procesarse en su totalidad...
            query = "select * from financieros.folios";
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            //Se selecciona todos los datos basicos para la importación
            query = "select * from financieros.datos";
            List<Dictionary<string, object>> tmp = globales.consulta(query);

            if (resultado.Count == 0)
            {
                globales.MessageBoxExclamation("No existen folios a procesarse", "Aviso", this);
                return;
            }

            string where = string.Empty;
            foreach (Dictionary<string, object> item in resultado)
            {
                string strFolio1 = Convert.ToString(item["desde"]);
                string strFolio2 = Convert.ToString(item["hasta"]);

                int folio1 = string.IsNullOrWhiteSpace(strFolio1) ? 0 : Convert.ToInt32(strFolio1);
                int folio2 = string.IsNullOrWhiteSpace(strFolio2) ? 0 : Convert.ToInt32(strFolio2);
                if (folio2 == 0)
                {
                    folio2 = folio1;
                }
                if (folio1 == 0)
                {
                    folio1 = folio2;
                }
                if (folio1 != 0 && folio2 != 0)
                {
                    for (int x = folio1; x <= folio2; x++)
                    {
                        where += string.Format("{0},", x);
                    }
                }
            }

            where = where.Substring(0, where.Length - 1);

            string CHCHEI = Convert.ToString(maximos[0]["numcheque"]);
            string CHPOLI = Convert.ToString(maximos[0]["numpoliz"]);
            string CHFA = Convert.ToString(tmp[0]["fecha"]);
            string CHBANCO = Convert.ToString(tmp[0]["banco"]);
            string CHCHEQUERA = Convert.ToString(tmp[0]["chequera"]);
            string CHCONCEP1 = Convert.ToString(tmp[0]["concep1"]);
            string CHELAB = Convert.ToString(tmp[0]["elaboro"]);
            string CHREVIS = Convert.ToString(tmp[0]["reviso"]);
            string CHAUTOR = Convert.ToString(tmp[0]["autorizo"]);
            string CHCTAPQ = Convert.ToString(tmp[0]["ctapq"]);
            string CHCTAFG = Convert.ToString(tmp[0]["ctafg"]);
            string CHCTAIN = Convert.ToString(tmp[0]["ctain"]);
            string CHCTABA = Convert.ToString(tmp[0]["ctaba"]);
            string ctaDebe = Convert.ToString(tmp[0]["ctadebe"]);
            string ctahaber = Convert.ToString(tmp[0]["ctahaber"]);

            List<Dictionary<string, object>> datosCuenta = globales.consulta("select folio,esfondo from financieros.datoscheques where benefic <> 'CANCELADO'");

            //Sección para importar datos de fondo de garantía..
            if (seleccion.boolFondo)
            {
                List<Dictionary<string, object>> tmppp = globales.consulta($"select count(folio) as cantidad from datos.p_quirog where folio in ({where});");
                string mensaje = $"Se procesaran {tmppp[0]["cantidad"]} para cheques iniciando en el cheque {numCheque} poliza {numPoliza} para el {string.Format("{0:d}", tmp[0]["fecha"])}";
                globales.MessageBoxInformation(mensaje, "IMPORTANDO INFORMACIÓN", this);
                query = string.Format("select fondo.*,edo.nombre_em from datos.d_fondo fondo inner join datos.p_edocta edo on edo.folio = fondo.folio where fondo.folio in ({0})", where);
                resultado = globales.consulta(query);

                if (resultado.Count == 0)
                {
                    globales.MessageBoxExclamation("No existe datos de fondo de garantía en el sistema", "Aviso", this);
                    return;
                }
                string insertar = string.Empty;
                int CHCHEI_entero = string.IsNullOrEmpty(CHCHEI) ? 1 : Convert.ToInt32(CHCHEI);
                int CHPOLI_entero = string.IsNullOrEmpty(CHPOLI) ? 1 : Convert.ToInt32(CHPOLI);
                foreach (Dictionary<string, object> item in resultado)
                {
                    bool encontrado = datosCuenta.Any(o => Convert.ToString(o["folio"]) == Convert.ToString(item["folio"]) && Convert.ToBoolean(o["esfondo"]));
                    if (encontrado) continue;

                    string aux = "insert into financieros.datoscheques(banco,chequera,fecha,numcheque,benefic,impcheque,concep1,elaboro,reviso,autorizo,numpoliz,folio,esfondo) values ('{0}','{1}','{2}',{3},'{4}',{5},'{6}','{7}','{8}','{9}',{10},{11},{12}); ";
                    aux = string.Format(aux, CHBANCO, CHCHEQUERA, string.Format("{0:yyyy-MM-dd}", DateTime.Parse(CHFA)), CHCHEI_entero, $"{item["nombre_em"]}", item["importe"], $"REINTEGRO DEL FONDO DE GARANTIA DE LA SOLICITUD No. : {item["folio"]}", CHELAB, CHREVIS, CHAUTOR, CHPOLI_entero, item["folio"], true);
                    insertar += aux;

                    CHCHEI_entero++;
                    CHPOLI_entero++;
                }
                if (globales.consulta(insertar, true))
                {
                    string actualizar = string.Format("update financieros.datos set numcheque = {0},numpoliz = {1}", CHCHEI_entero, CHPOLI_entero);
                    globales.consulta(actualizar, true);
                    //Se debe insertar en la cuenta principal para empezar con la insercion de los demás elementos en su tabla detalle
                    CHCHEI_entero = string.IsNullOrEmpty(CHCHEI) ? 1 : Convert.ToInt32(CHCHEI);
                    CHPOLI_entero = string.IsNullOrEmpty(CHPOLI) ? 1 : Convert.ToInt32(CHPOLI);
                    string sentencia = string.Empty;
                    foreach (Dictionary<string, object> item in resultado)
                    {
                        bool encontrado = datosCuenta.Any(o => Convert.ToString(o["folio"]) == Convert.ToString(item["folio"]) && Convert.ToBoolean(o["esfondo"]));
                        if (encontrado) continue;
                        string aux = "insert into financieros.detalle_cheque(numcheque,secuencia,ctacontab,concepto,debe_haber,importe,referencia) values ({0},{1},'{2}','{3}','{4}',{5},'{6}'); ";
                        aux = string.Format(aux, CHCHEI_entero, 1, ctaDebe, "REINTEGRO DEL FONDO DE GARANTIA", "D", string.IsNullOrWhiteSpace(Convert.ToString(item["importe"])) ? 0 : Convert.ToDouble(item["importe"]), "RECIBO");

                        string aux2 = "insert into financieros.detalle_cheque(numcheque,secuencia,ctacontab,concepto,debe_haber,importe,referencia) values ({0},{1},'{2}','{3}','{4}',{5},'{6}'); ";
                        aux2 = string.Format(aux2, CHCHEI_entero, 2, ctahaber, item["nombre_em"], "H", string.IsNullOrWhiteSpace(Convert.ToString(item["importe"])) ? 0 : Convert.ToDouble(item["importe"]), CHCHEI_entero);


                        sentencia += aux + aux2;

                        CHCHEI_entero++;
                        CHPOLI_entero++;
                    }
                    globales.consulta(sentencia, true);
                }
                globales.MessageBoxSuccess("Información fondo de garantía importada correctamente", "Información importada", this);
            }
            else
            { //Sección para importar datos de quirografarios..

                List<Dictionary<string, object>> tmppp = globales.consulta($"select count(folio) as cantidad from datos.p_quirog where folio in ({where});");
                string mensaje = $"Se procesaran {tmppp[0]["cantidad"]} para cheques iniciando en el cheque {numCheque} poliza {numPoliza} para el {string.Format("{0:d}", tmp[0]["fecha"])}";
                globales.MessageBoxInformation(mensaje, "IMPORTANDO INFORMACIÓN", this);
                query = string.Format("select p.folio,p.rfc,p.nombre_em,p.secretaria,p.descripcion,p.f_emischeq,p.importe,p.interes,p.fondo_g,p.liquido,c.cuenta from datos.p_quirog p LEFT JOIN catalogos.cuentas c on c.proy = p.secretaria where folio in ({0})", where);
                //Procesamiento de los datos...

                resultado = globales.consulta(query);
                if (resultado.Count == 0)
                {
                    globales.MessageBoxExclamation("No se encuentran datos de quirografarios en el sistema", "Aviso", this);
                    return;
                }

                globales.MessageBoxInformation("Verificando cuentas.", "VERIFICANDO CUENTAS", this);
                string insertar = string.Empty;
                int CHCHEI_entero = string.IsNullOrEmpty(CHCHEI) ? 1 : Convert.ToInt32(CHCHEI);
                int CHPOLI_entero = string.IsNullOrEmpty(CHPOLI) ? 1 : Convert.ToInt32(CHPOLI);
                foreach (Dictionary<string, object> item in resultado)
                {
                    string cuenta = Convert.ToString(item["cuenta"]);
                    if (cuenta == "450")
                    {
                        item["cuenta"] = "430";
                    }
                    bool encontrado = datosCuenta.Any(o => Convert.ToString(o["folio"]) == Convert.ToString(item["folio"]) && !Convert.ToBoolean(o["esfondo"]));
                    if (encontrado) continue;

                    string aux = "insert into financieros.datoscheques(banco,chequera,fecha,numcheque,benefic,impcheque,concep1,elaboro,reviso,autorizo,numpoliz,folio,esfondo) values ('{0}','{1}','{2}',{3},'{4}',{5},'{6}','{7}','{8}','{9}',{10},{11},{12}); ";
                    aux = string.Format(aux, CHBANCO, CHCHEQUERA, string.Format("{0:yyyy-MM-dd}", DateTime.Parse(CHFA)), CHCHEI_entero, $"{item["nombre_em"]}", item["liquido"], $"{CHCONCEP1} DE {string.Format("{0:C}", item["importe"])} DE {item["descripcion"]}", CHELAB, CHREVIS, CHAUTOR, CHPOLI_entero, item["folio"], false);
                    insertar += aux;

                    CHCHEI_entero++;
                    CHPOLI_entero++;
                }
                if (globales.consulta(insertar, true))
                {
                    string actualizar = string.Format("update financieros.datos set numcheque = {0},numpoliz = {1}", CHCHEI_entero, CHPOLI_entero);
                    globales.consulta(actualizar, true);
                    //Se debe insertar en la cuenta principal para empezar con la insercion de los demás elementos en su tabla detalle
                    CHCHEI_entero = string.IsNullOrEmpty(CHCHEI) ? 1 : Convert.ToInt32(CHCHEI);
                    CHPOLI_entero = string.IsNullOrEmpty(CHPOLI) ? 1 : Convert.ToInt32(CHPOLI);
                    string sentencia = string.Empty;
                    foreach (Dictionary<string, object> item in resultado)
                    {
                        bool encontrado = datosCuenta.Any(o => Convert.ToString(o["folio"]) == Convert.ToString(item["folio"]) && !Convert.ToBoolean(o["esfondo"]));
                        if (encontrado) continue;
                        string aux = "insert into financieros.detalle_cheque(numcheque,secuencia,ctacontab,concepto,referencia,debe_haber,importe) values ({0},{1},'{2}','{3}','{4}','{5}',{6}); ";
                        aux = string.Format(aux, CHCHEI_entero, 1, CHCTAPQ, item["nombre_em"], item["folio"], "D", string.IsNullOrWhiteSpace(Convert.ToString(item["importe"])) ? 0 : Convert.ToDouble(item["importe"]));
                        string aux2 = " ";
                        double fondo_garantia = string.IsNullOrWhiteSpace(Convert.ToString(item["fondo_g"])) ? 0 : Convert.ToDouble(item["fondo_g"]);
                        if (fondo_garantia != 0)
                        {
                            aux2 = "insert into financieros.detalle_cheque(numcheque,secuencia,ctacontab,concepto,referencia,debe_haber,importe) values ({0},{1},'{2}','{3}','{4}','{5}',{6}); ";
                            aux2 = string.Format(aux2, CHCHEI_entero, 2, CHCTAFG, "PRESTAMOS SOBRE DOCUMENTOS", "PAGARE", "H", string.IsNullOrWhiteSpace(Convert.ToString(item["fondo_g"])) ? "0" : Convert.ToString(item["fondo_g"]));
                        }

                        string aux3 = "insert into financieros.detalle_cheque(numcheque,secuencia,ctacontab,concepto,referencia,debe_haber,importe) values ({0},{1},'{2}','{3}','{4}','{5}',{6}); ";
                        aux3 = string.Format(aux3, CHCHEI_entero, 3, CHCTAIN, "POR PRESTAMO S/DOCTOS", "PAGARE", "H", string.IsNullOrWhiteSpace(Convert.ToString(item["interes"])) ? "0" : Convert.ToString(item["interes"]));

                        string aux4 = "insert into financieros.detalle_cheque(numcheque,secuencia,ctacontab,concepto,referencia,debe_haber,importe) values ({0},{1},'{2}','{3}','{4}','{5}',{6}); ";
                        aux4 = string.Format(aux4, CHCHEI_entero, 4, CHCTABA, item["nombre_em"], CHCHEI_entero, "H", string.IsNullOrWhiteSpace(Convert.ToString(item["liquido"])) ? "0" : Convert.ToString(item["liquido"]));

                        sentencia += aux + aux2 + aux3 + aux4;

                        CHCHEI_entero++;
                        CHPOLI_entero++;
                    }
                    globales.consulta(sentencia, true);
                }

                globales.MessageBoxSuccess("Información importada correctamente", "Información importada", this);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas cerrar la aplicación?", globales.menuPrincipal);
            if (p == DialogResult.Yes)
                Application.Exit();
        }



        private void btnEjemplo_Click_1(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuPrincipal_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.M)
            {
                panelMenuprincipal.Controls[0].Focus();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Seguro desea cerrar sesión?", "Aviso", globales.menuPrincipal);
            if (p == DialogResult.Yes)
            {
                string ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\credenciales.txt";
                if (File.Exists(ruta))
                {
                    File.Delete(ruta);
                }
                this.Close();
                this.regresar();
            }
        }
    }
}
