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
    public partial class frmNotificacion : Form
    {
        private string expediente;
        private int secuencia;

        public frmNotificacion()
        {
            InitializeComponent();

        }

        private void inicializa()
        {


        }




        private void oficio()
        {


        }

        private void desglose()
        {


        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void txttel_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmNotificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                cerrar();
            }
        }

      
        private void frmNotificacion_Load(object sender, EventArgs e)
        {
            inicializa();

        }

        private void frmNotificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cerrar();
        }
        private void cerrar()
        {
            this.Close();
            globales.showModal(new frmDocumentos(4));
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
            this.secuencia = opcion;
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

            query = $"select * from datos.h_snotif where expediente = {txtExpediente.Text} and sec = '{opcion}'";
            h_snotif notif = new dbaseORM().queryForMap<h_snotif>(query);

            txtF_Notif.Text = globales.parseDateTime(notif.f_notif);
            txtN_Notif.Text = notif.n_notif;
            txtC_Notif.Text = notif.c_notif;
            txtNumemis.Text = notif.numemis;
            txtT_Notif.Text = notif.t_notif;

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

            dtgrid.Rows.Add("PRESTAMO", capPrestado, int_prest, totPrest);
            dtgrid.Rows.Add("PAGO UNICO", capPrim, int_prim, totPrim);
            dtgrid.Rows.Add("POR NOMINA", capUnit, int_unit, totUnit);

        }

        private void limpiacampos()
        {
            limpiarHipote();
            limpiarNotifi();
            dtgrid.Rows.Clear();
        }

        private void limpiarNotifi()
        {
            txtF_Notif.Text = "";
            txtN_Notif.Text = "";
            txtC_Notif.Text = "";
            txtNumemis.Text = "";
            txtT_Notif.Text = "";

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
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas actualizar el registro?", "Aviso", globales.menuPrincipal);
            if (dialogo == DialogResult.No) return;

            if (string.IsNullOrWhiteSpace(txtExpediente.Text)) {
                globales.MessageBoxExclamation("Favor de elegir un expediente","Aviso",globales.menuPrincipal);
                return;
            }

            if (guardarHipote()) {
                if (guardarNotifi()) {
                    globales.MessageBoxSuccess("Registros actualizados correctamente","Aviso",globales.menuPrincipal);
                    limpiacampos();
                }
            }
        }

        private bool guardarNotifi()
        {
            h_snotif obj = new h_snotif();
            obj.expediente = globales.convertInt(txtExpediente.Text);
            obj.sec = Convert.ToString(this.secuencia);

            obj.f_notif = globales.convertDatetime(txtF_Notif.Text);
            obj.n_notif = txtN_Notif.Text;
            obj.c_notif = txtC_Notif.Text;
            obj.numemis = txtNumemis.Text;
            obj.t_notif = txtT_Notif.Text;

            dbaseORM orm = new dbaseORM();
            string query = $"select expediente from datos.h_snotif where expediente = {txtExpediente.Text} and sec = '{this.secuencia}'";
            List<Dictionary<string, object>> resultado = orm.query(query);

            bool actualizando;
            if (resultado.Count == 0)
                actualizando = orm.insert<h_snotif>(obj);
            else
                actualizando = orm.update<h_snotif>(obj);

            return actualizando;
        }

        private bool guardarHipote()
        {
            string query = $"update datos.p_hipote set rfc='{txtRfc.Text}' , nombre_em='{txtNombre_em.Text}' , tel_ofici='{txtTel_ofic.Text}' , direccion='{txtDireccion.Text}' , secretaria='{txtSecretaria.Text}', descripcion='{txtDescripcion.Text}' ,  direc_inmu='{txtDirec_inmueb.Text}' where folio = {txtExpediente.Text}";
            globales.consulta(query);
            return true;
            //p_hipote obj = new p_hipote();
            //obj.folio = globales.convertInt(txtExpediente.Text);
            //obj.rfc = txtRfc.Text;
            //obj.nombre_em = txtNombre_em.Text;
            //obj.tel_ofici = txtTel_ofic.Text;
            //obj.direccion = txtDireccion.Text;
            //obj.secretaria = txtSecretaria.Text;
            //obj.descripcion = txtDescripcion.Text;
            //obj.direc_inmu = txtDirec_inmueb.Text;

            //return new dbaseORM().update<p_hipote>(obj);
        }

        private void frmNotificacion_Shown(object sender, EventArgs e)
        {
            ActiveControl = button2;
        }

        private void panel9_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtN_Notif.Text)) {
                DialogResult dialogo = globales.MessageBoxQuestion("¿Desea que se escriban datos predeterminados?", "Aviso", globales.menuPrincipal);
                if (dialogo == DialogResult.Yes) {
                    string query = "select * from catalogos.firmas where clave = 'NOT'";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    if (resultado.Count != 0) {
                        txtF_Notif.Text = string.Format("{0:d}",DateTime.Now);
                        txtN_Notif.Text = Convert.ToString(resultado[0]["nombre"]);
                        txtC_Notif.Text = Convert.ToString(resultado[0]["cargo"]);
                    }
                }
            }

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtExpediente_KeyDown(object sender, KeyEventArgs e)
        {
         
        }
    }
}
