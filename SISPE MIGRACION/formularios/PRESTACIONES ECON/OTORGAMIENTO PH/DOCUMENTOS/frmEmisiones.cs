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
    public partial class frmEmisiones : Form
    {
        private string expediente;
        private int secuencia;
        private bool nuevaemision = false;

        public frmEmisiones()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cerrar();
        }
        private void cerrar()
        {
            this.Close();
            globales.showModal(new frmDocumentos(7));
        }

        private void btnP_hipote_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas guardar los cambios?", "Aviso", globales.menuPrincipal);
            if (dialogo == DialogResult.No) return;

            if (string.IsNullOrWhiteSpace(txtExpediente.Text))
            {
                globales.MessageBoxExclamation("Favor de elegir un expediente", "Aviso", globales.menuPrincipal);
                return;
            }


            if (guardarHipote()) {
                if (guardaEnotar()) {
                    if (guardaSemisi()) {
                        if (guardaSantec()) {
                            globales.MessageBoxSuccess("Registro actualizado","Aviso",globales.menuPrincipal);
                            limpiacampos();
                        }
                    }
                }
            }

        }

        private bool guardaSantec()
        {
            h_santec obj = new h_santec();
            obj.expediente = globales.convertInt(txtExpediente.Text);
            obj.sec = Convert.ToString(this.secuencia);
            obj.tomo_inscr = globales.convertInt(txtTomo_Inscr.Text);
            obj.f_inscr_rp = globales.convertDatetime(txtF_Inscr_Rp.Text);
            obj.libr_inscr = globales.convertInt(txtLibr_Incr.Text);
            obj.dist_judic = txtDist_Jud.Text;

            dbaseORM orm = new dbaseORM();
            string query = $"select expediente from datos.h_santec where expediente = {txtExpediente.Text} and sec = '{this.secuencia}'";
            List<Dictionary<string, object>> resultado = orm.query(query);
            bool actualiza;
            if (resultado.Count == 0)
                actualiza = orm.insert<h_santec>(obj);
            else
                actualiza = orm.update<h_santec>(obj);

            return actualiza;


        }

        private bool guardaSemisi()
        {
            h_semisi obj = new h_semisi();
            obj.expediente = globales.convertInt(txtExpediente.Text);
            obj.sec = Convert.ToString(this.secuencia);
            obj.n_emision = txtN_Emision.Text;
            obj.f_recibo = globales.convertDatetime(txtF_Recibo.Text);
            obj.importe = globales.convertDouble(txtImporte.Text);

            dbaseORM orm = new dbaseORM();
            string query = $"select * from datos.h_semisi where expediente = {txtExpediente.Text} and sec = '{this.secuencia}'";
            List<Dictionary<string, object>> resultado = orm.query(query);
            

            bool actualiza;
            if (this.nuevaemision) {
                actualiza = orm.insert<h_semisi>(obj);
            }
            else {
                if (resultado.Count == 0)
                    actualiza = orm.insert<h_semisi>(obj);
                else
                    actualiza = orm.update<h_semisi>(obj);
            }

            this.nuevaemision = false;
            this.button1.Visible = true;

            return actualiza;
        }

        private bool guardaEnotar()
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
                boolResultado = orm.update<h_enotar>(obj);

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
            this.secuencia = opcion;
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

            txtCap_Prest.Text = Convert.ToString(diccionario["cap_prest"]);
            txtInt_prest.Text = Convert.ToString(diccionario["int_prest"]);
            txtTot_Prest.Text = Convert.ToString(diccionario["tot_prest"]);

            dbaseORM orm = new dbaseORM();

            query = $"select *  from datos.h_enotar where expediente = {txtExpediente.Text}";
            h_enotar enotar = orm.queryForMap<h_enotar>(query);

            txtN_Notario.Text = Convert.ToString(enotar.n_notario);
            txtNombre_Not.Text = enotar.nombre_not;
            txtNActa_N.Text = Convert.ToString(enotar.n_acta_n);
            txtN_Volu_N.Text = Convert.ToString(enotar.n_volu_n);
            txtF_Inscr_N.Text = globales.parseDateTime(enotar.f_inscr_n);

            query = $"select * from datos.h_semisi where expediente = {txtExpediente.Text} and sec = '{this.secuencia}'";
            List<Dictionary<string, object>> objresultado = globales.consulta(query);

            if (objresultado.Count != 0) {

                Dictionary<string, object> auxdiccio = objresultado[objresultado.Count - 1];

                txtN_Emision.Text = Convert.ToString(auxdiccio["n_emision"]);
                txtF_Recibo.Text = globales.parseDateTime(globales.convertDatetime(Convert.ToString(auxdiccio["f_recibo"])));
                txtImporte.Text = Convert.ToString(auxdiccio["importe"]);
            }
            if (!string.IsNullOrWhiteSpace(txtN_Emision.Text))
            {
                this.button1.Visible = true;
            }
            else {
                this.button1.Visible = false;
            }


            query = $"select * from datos.h_santec where expediente = {txtExpediente.Text} and sec = '{this.secuencia}'";
            h_santec san = orm.queryForMap<h_santec>(query);
            txtTomo_Inscr.Text = Convert.ToString(san.tomo_inscr);
            txtF_Inscr_Rp.Text = globales.parseDateTime(san.f_inscr_rp);
            txtLibr_Incr.Text = Convert.ToString(san.libr_inscr);
            txtDist_Jud.Text = san.dist_judic;


        }

        private void limpiacampos()
        {
            limpiaHipote();
            limpiaH_solici();
            limpiaEnotar();
            limpiarSemisi();
            limpiarSantec();
        }

        private void limpiarSantec()
        {
            txtTomo_Inscr.Text = "";
            txtF_Inscr_Rp.Text = "";
            txtLibr_Incr.Text = "";
            txtDist_Jud.Text = "";
        }

        private void limpiarSemisi()
        {
            txtN_Emision.Text = "";
            txtF_Recibo.Text = "";
            txtImporte.Text = "";
            button1.Visible = false;
        }

        private void limpiaEnotar()
        {
            txtN_Notario.Text = "";
            txtNombre_Not.Text = "";
            txtNActa_N.Text = "";
            txtN_Volu_N.Text = "";
            txtF_Inscr_N.Text = "";
        }

        private void limpiaH_solici()
        {
            txtTot_Prest.Text = "";
            txtInt_prest.Text = "";
            txtCap_Prest.Text = "";
        }

        private void limpiaHipote()
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

        private void frmEmisiones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                cerrar();
            }
        }

        private void frmEmisiones_Shown(object sender, EventArgs e)
        {
            ActiveControl = button2;
        }

        private void frmEmisiones_Load(object sender, EventArgs e)
        {

        }

        private void panel7_Enter(object sender, EventArgs e)
        {
            
               
            
        }

        private void panel3_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre_Not.Text)) {
                DialogResult dialogo = globales.MessageBoxQuestion("¿Desea que se escriban datos predeterminados?", "Aviso",globales.menuPrincipal);
                if (dialogo == DialogResult.Yes) {
                    string query = "select nombre from catalogos.firmas where clave = 'NOT'";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);

                    if (resultado.Count != 0) {
                        txtNombre_Not.Text = Convert.ToString(resultado[0]["nombre"]);
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Visible = false;
            txtN_Emision.Text = "";
            txtF_Recibo.Text = "";
            txtImporte.Text = "";
            this.nuevaemision = true;
        }

        private void txtN_Emision_Leave(object sender, EventArgs e)
        {

        }

        private void txtN_Emision_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtN_Emision.Text)) {
                double importe = globales.convertDouble(txtImporte.Text);
                txtImporte.Text = globales.convertMoneda(importe);
               
                    DialogResult dialogo = globales.MessageBoxQuestion("¿Desea que se escriban datos predeterminados?", "Aviso", globales.menuPrincipal);
                    if (dialogo == DialogResult.Yes)
                    {
                        txtF_Recibo.Text = string.Format("{0:d}", DateTime.Now);
                        double capita_prestado = globales.convertDouble(txtCap_Prest.Text);
                        txtImporte.Text = globales.convertMoneda(capita_prestado / 2);
                    }
                
            }
        }

        private void txtF_Recibo_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
