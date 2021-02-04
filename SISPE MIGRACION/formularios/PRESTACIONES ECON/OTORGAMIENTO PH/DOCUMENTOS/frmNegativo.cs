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
    public partial class frmNegativo : Form
    {
        private int secuencia;
        private string expediente;

        public frmNegativo()
        {
            InitializeComponent();
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

            query = $"select * from datos.h_simpro where expediente = {txtExpediente.Text} and sec = '{this.secuencia}'";
            h_simpro obj = new dbaseORM().queryForMap<h_simpro>(query);

            txtF_Noproc.Text = globales.parseDateTime(obj.f_noproc);
            txtT_Noproc.Text = obj.t_noproc;
        }

        private void limpiacampos()
        {
            limpiarHipote();
            limpiarSimpro();
        }

        private void limpiarSimpro()
        {
            txtF_Noproc.Text = "";
            txtT_Noproc.Text = "";
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
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas actualizar el registro?","Aviso",globales.menuPrincipal);
            if (dialogo == DialogResult.No) return;

            if (string.IsNullOrWhiteSpace(txtExpediente.Text))
            {
                globales.MessageBoxExclamation("Favor de elegir un expediente", "Aviso", globales.menuPrincipal);
                return;
            }

            if (guardarHipote())
            {
                if (guardarNegativo())
                {
                    globales.MessageBoxSuccess("Registros actualizados correctamente", "Aviso", globales.menuPrincipal);
                    limpiacampos();
                }
            }
        }

        private bool guardarNegativo()
        {
            h_simpro obj = new h_simpro();
            obj.expediente = globales.convertInt(txtExpediente.Text);
            obj.sec = Convert.ToString(this.secuencia);
            obj.f_noproc = globales.convertDatetime(txtF_Noproc.Text);
            obj.t_noproc = txtT_Noproc.Text;

            dbaseORM orm = new dbaseORM();

            string query = $"select * from datos.h_simpro where expediente = {txtExpediente.Text} and sec = '{this.secuencia}'";
            List<Dictionary<string, object>> resultado = orm.query(query);
            bool actualizado;
            if (resultado.Count == 0)
                actualizado = orm.insert<h_simpro>(obj);
            else
                actualizado = orm.update<h_simpro>(obj);

            return actualizado;

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
            globales.showModal(new frmDocumentos(6));
        }

        private void frmNegativo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                cerrar();
            }
        }

        private void frmNegativo_Shown(object sender, EventArgs e)
        {
            ActiveControl = button2;
        }
    }
}
