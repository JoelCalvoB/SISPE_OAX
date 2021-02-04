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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS
{
    public partial class frmconveniomodificatorio : Form
    {
        private string expediente;
        private object secuencia;

        public frmconveniomodificatorio()
        {
            InitializeComponent();
        }

        private void primeretapa()
        {
          
        }

  

        private void llenanotario()
        {
           


        }

       

        private void desglose()
        {
        

        }

        private void frmconveniomodificatorio_Load(object sender, EventArgs e)
        {
            primeretapa();
        }

        private void frmconveniomodificatorio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void frmconveniomodificatorio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2){
                cerrar();
            }
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
            string folio = Convert.ToString(datos["folio"]);
            frmAmpliacion ampliacion = new frmAmpliacion("Documentación P/Solicitud", folio, datos, "Solicitud inicial", "1° Ampliación", "2° Ampliación", "3° Ampliación");
            ampliacion.enviar = recibiendoampliacion;
            globales.showModal(ampliacion);
        }

        public void recibiendoampliacion(string expediente, int opcion, Dictionary<string, Object> datos)
        {
            limpiacampos();
            this.expediente = expediente;
            string query = $"select * from datos.h_solici where expediente = {expediente} and sec = '{opcion}'";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            string tramite = string.Empty;
            tramite = (opcion == 0) ? "Solicitud inicial" : opcion + "° Ampliación";


            this.txtamplia.Text = tramite;

            this.txtRfc.Text = Convert.ToString(datos["rfc"]);
            this.txtNombre_em.Text = Convert.ToString(datos["nombre_em"]);
            this.txtExpediente.Text = Convert.ToString(datos["folio"]);
            this.txtDireccion.Text = Convert.ToString(datos["direccion"]);
            this.txtDescripcion.Text = Convert.ToString(datos["descripcion"]);
            this.txtSecretaria.Text = Convert.ToString(datos["secretaria"]);
            this.txtDirec_inmueb.Text = Convert.ToString(datos["direc_inmu"]);
            this.txtTel_ofic.Text = Convert.ToString(datos["tel_ofici"]);

            
            if (resultado.Count == 0)
            {
                globales.MessageBoxExclamation($"Expediente N° {expediente} \nNo se encontro {tramite}", "Aviso", globales.menuPrincipal);
                return;
            }

            Dictionary<string, object> diccionario = resultado[0];

            string capPrestado = Convert.ToString(diccionario["cap_prest"]);
            string capPrim = Convert.ToString(diccionario["cap_prim"]);
            string capUnit = Convert.ToString(diccionario["cap_unit"]);

            string int_prest = Convert.ToString(diccionario["int_prest"]);
            string int_prim = Convert.ToString(diccionario["int_prim"]);
            string int_unit = Convert.ToString(diccionario["int_unit"]);

            string totPrest = Convert.ToString(diccionario["tot_prest"]);
            string totPrim = Convert.ToString(diccionario["tot_prim"]);
            string totUnit = Convert.ToString(diccionario["tot_unit"]);

            dtgrid.Rows.Add("PRESTAMO",capPrestado, int_prest, totPrest);
            dtgrid.Rows.Add("PAGO UNICO",capPrim, int_prim, totPrim);
            dtgrid.Rows.Add("POR NOMINA",capUnit, int_unit, totUnit);



            dbaseORM orm = new dbaseORM();

            query = $"select *  from datos.h_enotar where expediente = {txtExpediente.Text}";
            h_enotar enotar = orm.queryForMap<h_enotar>(query);

            query = $"select *  from datos.h_sconvm WHERE expediente = {txtExpediente.Text} and sec = '{opcion}'";
            h_sconvm sconvm = orm.queryForMap<h_sconvm>(query);

            txtN_Notario.Text = Convert.ToString(enotar.n_notario);
            txtNombre_Not.Text = enotar.nombre_not;
            txtNActa_N.Text = Convert.ToString(enotar.n_acta_n);
            txtN_Volu_N.Text = Convert.ToString(enotar.n_volu_n);
            txtF_Inscr_N.Text = globales.parseDateTime(enotar.f_inscr_n);

            txtF_Convm.Text = globales.parseDateTime(sconvm.f_convm);
            txtNombre_T1.Text = sconvm.nombre_t1;
            txtDirec_1.Text = sconvm.direc_1;
            txtNombre_T2.Text = sconvm.nombre_t2;
            txtDirec_2.Text = sconvm.direc_2;


            this.secuencia = opcion;
        }

        private void limpiacampos()
        {
            limpiarHipote();
            limpiarHEnotar();
            limpiarHSconven();
            dtgrid.Rows.Clear();
        }

        private void limpiarHSconven()
        {
            txtF_Convm.Text = "";
            txtNombre_T1.Text = "";
            txtDirec_1.Text = "";
            txtNombre_T2.Text = "";
            txtDirec_2.Text = "";
        }

        private void limpiarHEnotar()
        {
            txtN_Notario.Text = "";
            txtNombre_Not.Text = "";
            txtNActa_N.Text = "";
            txtN_Volu_N.Text = "";
            txtF_Inscr_N.Text = "";
        }

        private void limpiarHipote()
        {
            this.txtRfc.Text = string.Empty;
            this.txtNombre_em.Text = string.Empty;
            this.txtExpediente.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtSecretaria.Text = string.Empty;
            this.txtDirec_inmueb.Text = string.Empty;
            this.txtTel_ofic.Text = string.Empty;
        }

        private void btnP_hipote_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas guardar los cambios?","Aviso",globales.menuPrincipal);
            if (dialogo == DialogResult.No) return;

            if (string.IsNullOrWhiteSpace(txtExpediente.Text))
            {
                globales.MessageBoxExclamation("Favor de elegir un expediente", "Aviso", globales.menuPrincipal);
                return;
            }

            if (guardarHipote()) {
                if (guardarEnotar()) {
                    if (guardarHsconvm()) {
                        globales.MessageBoxSuccess("Registro actualizado correctamente","Aviso",globales.menuPrincipal);
                        limpiacampos();
                    }
                }
            }

        }

        private bool guardarHsconvm()
        {
            h_sconvm obj = new h_sconvm();
            obj.expediente = globales.convertInt(txtExpediente.Text);
            obj.sec = Convert.ToString(this.secuencia);
            obj.f_convm = globales.convertDatetime(txtF_Convm.Text);
            obj.nombre_t1 = txtNombre_T1.Text;
            obj.nombre_t2 = txtNombre_T2.Text;
            obj.direc_1 = txtDirec_1.Text;
            obj.direc_2 = txtDirec_2.Text;

            dbaseORM orm = new dbaseORM();

            string query = $"select * from datos.h_sconvm where expediente = {txtExpediente.Text} and sec = '{this.secuencia}'";
            List<Dictionary<string, object>> resultado = orm.query(query);

            bool actualizado;
            if (resultado.Count == 0)
                actualizado = orm.insert<h_sconvm>(obj);
            else
                actualizado = orm.update<h_sconvm>(obj);

            return actualizado;
        }

        private bool guardarEnotar()
        {
            h_enotar obj = new h_enotar();
            obj.expediente = Convert.ToInt32(txtExpediente.Text);
            obj.n_notario = globales.convertInt(txtN_Notario.Text);
            obj.nombre_not = txtNombre_Not.Text;
            obj.n_acta_n = globales.convertDouble(txtNActa_N.Text);
            obj.n_volu_n = globales.convertDouble(txtN_Volu_N.Text);
            obj.f_inscr_n = globales.convertDatetime(txtF_Inscr_N.Text);

            dbaseORM orm = new dbaseORM();

            string query = $"select expediente from datos.h_enotar where expediente = {txtExpediente.Text}";
            List<Dictionary<string, object>> resultado = orm.query(query);

            bool boolResultado;
            if (resultado.Count == 0)
                boolResultado = orm.insert<h_enotar>(obj);
            else
               boolResultado =  orm.update<h_enotar>(obj);

            return boolResultado;
        }

        private bool guardarHipote()
        {
            p_hipote obj = new p_hipote();
            obj.folio = globales.convertInt(txtExpediente.Text);
            obj.rfc = txtRfc.Text;
            obj.nombre_em = txtNombre_em.Text;
            obj.tel_ofici = txtTel_ofic.Text;
            obj.direccion = txtDireccion.Text;
            obj.secretaria = txtSecretaria.Text;
            obj.descripcion = txtDescripcion.Text;
            obj.direc_inmu = txtDirec_inmueb.Text;

            return new dbaseORM().update<p_hipote>(obj);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cerrar();
        }
        private void cerrar()
        {
            this.Close();
            globales.showModal(new frmDocumentos(5));
        }

        private void frmconveniomodificatorio_Shown(object sender, EventArgs e)
        {
            ActiveControl = button2;
        }
    }

}



