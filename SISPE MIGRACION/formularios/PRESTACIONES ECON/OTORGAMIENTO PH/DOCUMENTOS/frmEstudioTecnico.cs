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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS.ESTUDIO_TÉCNICO
{
    public partial class frmEstudioTecnico_ : Form
    {
        private int secuencia;
        private string expediente;

        public frmEstudioTecnico_()
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
            globales.showModal(new frmDocumentos(3));
        }


        private void frmEstudioTecnico__Load(object sender, EventArgs e)
        {

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
            string query = $"select expediente,sec from datos.h_solici where expediente = {expediente} and sec = '{opcion}'";
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

            query = $"select * from datos.h_sretec where expediente = {txtExpediente.Text} and sec = '{opcion}'";
            h_sretec obj = new dbaseORM().queryForMap<h_sretec>(query);

            this.txtF_Elab.Text = globales.parseDateTime(obj.f_elab);
            this.txtAreapredio.Text = Convert.ToString(obj.areapredio);
            this.txtNiveles.Text = Convert.ToString(obj.niveles);
            this.txtHabitac.Text = Convert.ToString(obj.habitac);
            this.txtDesghabit.Text = obj.desghabit;
            this.txtArea_Const.Text = Convert.ToString(obj.area_const);
            this.txtAvaObrNeg.Text = Convert.ToString(obj.avaobneg);
            this.txtAvaacab.Text = Convert.ToString(obj.avaacab);
            this.txtImp_estimt.Text = Convert.ToString(obj.imp_estimt);
            this.txtImp_Avance.Text = Convert.ToString(obj.imp_avance);
            this.txtObserv.Text = obj.observ;
            this.txtImp_Faltante.Text = Convert.ToString(obj.imp_faltante);
            this.txtDiagnostico.Text = obj.diagnostico;

            this.secuencia = opcion;
        }

        private void limpiacampos()
        {
            limpiarPrinciapal();
            limpiarDatosTecnicos();
        }

        private void limpiarDatosTecnicos()
        {
            this.txtF_Elab.Text = "";
            this.txtAreapredio.Text = "";
            this.txtNiveles.Text = "";
            this.txtHabitac.Text = "";
            this.txtDesghabit.Text = "";
            this.txtArea_Const.Text = "";
            this.txtAvaObrNeg.Text = "";
            this.txtAvaacab.Text = "";
            this.txtImp_estimt.Text = "";
            this.txtImp_Avance.Text = "";
            this.txtObserv.Text = "";
            this.txtImp_Faltante.Text = "";
        }

        private void limpiarPrinciapal()
        {

            this.txtamplia.Text = "";
            this.txtExpediente.Text = "";

            this.txtRfc.Text = "";
            this.txtNombre_em.Text = "";
            this.txtDireccion.Text = "";
            this.txtSecretaria.Text = "";
            this.txtDirec_inmueb.Text = "";
            this.txtDescripcion.Text = "";
            this.txtTel_ofic.Text = "";
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

            dbaseORM orm = new dbaseORM();
            p_hipote hipote = llenarHipote();

            bool actualizado = orm.update<p_hipote>(hipote);
            if (actualizado)
            {
                string query = $"select expediente from datos.h_sretec where expediente = {txtExpediente.Text} and sec = '{this.secuencia}'";
                List<Dictionary<string, object>> resultado = orm.query(query);
                h_sretec sretec = new h_sretec();
                sretec.expediente = globales.convertInt(txtExpediente.Text);
                sretec.sec = Convert.ToString(this.secuencia);
                sretec = llenarSretec(sretec);

                if (resultado.Count == 0)
                    actualizado = orm.insert<h_sretec>(sretec);
                else
                    actualizado = orm.update<h_sretec>(sretec);

                if (actualizado)
                    globales.MessageBoxSuccess("Registro actualizado correctamente", "Aviso", globales.menuPrincipal);
            }
        }

        private h_sretec llenarSretec(h_sretec obj)
        {
            obj.f_elab = globales.convertDatetime(this.txtF_Elab.Text);
            obj.areapredio = globales.convertDouble(txtAreapredio.Text);
            obj.niveles = globales.convertInt(txtNiveles.Text);
            obj.habitac = globales.convertInt(txtHabitac.Text);
            obj.desghabit = txtDesghabit.Text;
            obj.area_const = globales.convertDouble(txtArea_Const.Text);
            obj.avaobneg = globales.convertDouble(txtAvaObrNeg.Text);
            obj.avaacab = globales.convertDouble(txtAvaacab.Text);
            obj.imp_estimt = globales.convertDouble(txtImp_estimt.Text);
            obj.imp_avance = globales.convertDouble(txtImp_Avance.Text);
            obj.observ = txtObserv.Text;
            obj.imp_faltante = globales.convertDouble(txtImp_Faltante.Text);
            obj.diagnostico = txtDiagnostico.Text;

            return obj;
        }

        private p_hipote llenarHipote()
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


            return obj;
        }

        private void frmEstudioTecnico__KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                cerrar();
            }
        }

        private void frmEstudioTecnico__Shown(object sender, EventArgs e)
        {
            ActiveControl = button2;
        }
    }
}
