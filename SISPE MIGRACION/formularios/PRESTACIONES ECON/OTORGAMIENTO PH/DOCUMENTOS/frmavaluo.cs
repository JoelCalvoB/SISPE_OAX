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
    public partial class frmavaluo : Form
    {
        private List<Dictionary<string, object>> resultado;
        private bool sePregunto;
        private string expediente;
        private bool conexpediente;

        public frmavaluo()
        {
            InitializeComponent();
        }

        private void frmavaluo_Load(object sender, EventArgs e)
        {
          


        }


      
     



        private void llenacampos(Dictionary<string, object> datos)
        {
            limpiacampos();

            this.txtExpediente.Text = Convert.ToString(datos["folio"]);
            this.expediente = txtExpediente.Text;
            this.txtRfc.Text = Convert.ToString(datos["rfc"]);
            this.txtnombre_em.Text = Convert.ToString(datos["nombre_em"]);
            this.txtsexo.Text = Convert.ToString(datos["sexo"]);
            this.txtDireccion.Text = Convert.ToString(datos["direccion"]);
            this.txtSecretaria.Text = Convert.ToString(datos["secretaria"]);
            this.txtTel_partic.Text = Convert.ToString(datos["tel_partic"]);
            this.txtCve_categ.Text = Convert.ToString(datos["cve_categ"]);
            this.txtDirec_inmueb.Text = Convert.ToString(datos["direc_inmu"]);
            this.txtProyecto.Text = Convert.ToString(datos["proyecto"]);
            this.txtdescripcion.Text = Convert.ToString(datos["descripcion"]);
            this.txtTel_ofic.Text = Convert.ToString(datos["tel_ofici"]);


            string query = "SELECT * FROM datos.h_eavalu where expediente='{0}' ";
            string con = string.Format(query, txtExpediente.Text);
            List<Dictionary<string, object>> resultado = globales.consulta(con);

            if (resultado.Count <= 0)
            {
                 globales.MessageBoxExclamation("NO SE ENCUENTRAN REGISTROS DE AVALUO RELACIONADO A ESTE EXPEDIENTE", "Aviso", globales.menuPrincipal);
            }
            else
            {
                globales.MessageBoxInformation("MOSTRANDO INFORMACIÓN DE AVALUO DEL EXPEDIENTE" + txtExpediente.Text, "Aviso", globales.menuPrincipal);

                DateTime fecha = Convert.ToDateTime(resultado[0]["f_solic"]);
                this.txtfecha_solic.Text = string.Format("{0:dd/MM/yyyy}", fecha);
                this.txtNombre.Text = Convert.ToString(resultado[0]["nombre"]);
                string avaluo = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["valor_bien"]))?"0": Convert.ToString(resultado[0]["valor_bien"]);
                double numero = Convert.ToDouble(avaluo);
                string sueldo = string.Format("{0:C}", (numero));
                this.txtvalor_bien.Text = sueldo;
                this.txtcargos_cat.Text = Convert.ToString(resultado[0]["cargo"]);
            }
            




        }

        private void frmavaluo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void frmavaluo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                cerrar();
            }

            if (e.KeyCode == Keys.F9)
            {
                double monto = double.Parse(txtvalor_bien.Text, System.Globalization.NumberStyles.Currency);
                string valor = Convert.ToString(monto);
                string actu = "update datos.p_hipote  set rfc ='{0}', nombre_em='{1}',sexo='{2}',direccion='{3}',cve_categ='{4}',secretaria='{5},',descripcion='{6}', tel_ofici='{7}',direc_inmu='{8}',tel_partic='{9}' where folio='{10}'";
                string actualiza = string.Format(actu, txtRfc.Text, txtnombre_em.Text, txtsexo.Text, txtDireccion.Text, txtCve_categ.Text, txtSecretaria.Text, txtdescripcion.Text, txtTel_ofic.Text, txtDirec_inmueb.Text, txtTel_partic.Text, txtExpediente.Text);
                globales.consulta(actualiza);
                string query = "SELECT * FROM datos.h_eavalu where expediente='{0}' ";
                string con = string.Format(query, txtExpediente.Text);
                List<Dictionary<string, object>> resultado = globales.consulta(con);
                if (resultado.Count > 0)
                {
                    string borra = "delete FROM datos.h_eavalu where expediente='{0}'";
                    string borrapasa = string.Format(borra, txtExpediente.Text);
                    globales.consulta(borrapasa);
                }

                string avaluo = "insert into datos.h_eavalu (expediente,f_solic,nombre,valor_bien,cargo) values ('{0}','{1}','{2}','{3}','{4}')";
                string pasaavaluo = string.Format(avaluo, txtExpediente.Text, txtfecha_solic.Text, txtNombre.Text, valor, txtcargos_cat.Text);
                globales.consulta(pasaavaluo);
                DialogResult dialogo = globales.MessageBoxSuccess("AVALUO REGISTRADO CORRECTAMENTE EN EL EXPEDIENTE : " + txtExpediente.Text, "", this);

                DialogResult dialogo1 = globales.MessageBoxQuestion("¿DESEA HACER OTRO MOVIMIENTO?", this);
                if (dialogo1 == DialogResult.Yes)
                {
                    
                }
                else
                {
                    Close();
                }
            }


        }

        private void txtvalor_KeyDown(object sender, KeyEventArgs e)
        {

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
            txtExpediente.Text = string.Empty;
            txtRfc.Text = string.Empty;
            txtnombre_em.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTel_partic.Text = string.Empty;
            txtsexo.Text = string.Empty;
            txtSecretaria.Text = string.Empty;
            txtdescripcion.Text = string.Empty;
            txtTel_ofic.Text = string.Empty;
            txtProyecto.Text = string.Empty;
            txtCve_categ.Text = string.Empty;
            txtDirec_inmueb.Text = string.Empty;

            this.txtfecha_solic.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtcargos_cat.Text = string.Empty;
            this.txtvalor_bien.Text = string.Empty;
        }

        private void btnFolio_Click(object sender, EventArgs e)
        {
            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
            p_quirog.enviar2 = llenacampos;
            p_quirog.tablaConsultar = "p_hipote";
            p_quirog.ShowDialog();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtExpediente.Text)) {
                globales.MessageBoxExclamation("Favor de elegir un expediente","Aviso",globales.menuPrincipal);
                return;
            }

           

            string query = string.Empty;
            DateTime fechaSolicitud = DateTime.Parse(txtfecha_solic.Text);
            string fecha_solicitud = string.Format("{0:yyyy-MM-dd}", fechaSolicitud);
            string nombre = txtNombre.Text;
            string cargos_cat = txtcargos_cat.Text;
            txtvalor_bien.Text = globales.convertDouble(txtvalor_bien.Text).ToString();
            double valor_bien = double.Parse(txtvalor_bien.Text, System.Globalization.NumberStyles.Currency);
            if (verifica())
            {
                query = "insert into datos.h_eavalu values ({0},'{1}','{2}',{3},'{4}');";
                query = string.Format(query,txtExpediente.Text,fecha_solicitud, nombre, valor_bien, cargos_cat);
                if (globales.consulta(query, true))
                {
                    globales.MessageBoxSuccess("Registro actualizado correctamente", "Aviso", globales.menuPrincipal);
                }
                else
                {
                    globales.MessageBoxExclamation("Error al actualizar el registro", "Aviso", globales.menuPrincipal);
                }
            }
            else {
                query = $"update datos.h_eavalu set f_solic = '{fecha_solicitud}',nombre = '{nombre}',valor_bien = {valor_bien},cargo = '{cargos_cat}' where expediente = {txtExpediente.Text}";
                if (globales.consulta(query,true))
                {
                    globales.MessageBoxSuccess("Registro actualizado correctamente", "Aviso", globales.menuPrincipal);
                }
                else
                {
                    globales.MessageBoxExclamation("Error al actualizar el registro", "Aviso", globales.menuPrincipal);
                }
            }
        }

        private bool verifica()
        {
            string query = $"select * from datos.h_eavalu where expediente = {txtExpediente.Text}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            return resultado.Count == 0;
        }

        private void txtfecha_solic_Enter(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtExpediente.Text)) return;

            if (string.IsNullOrEmpty(txtNombre.Text)) {
                if (!sePregunto)
                {
                    DialogResult pregunta = globales.MessageBoxQuestion("¿Desea que se escriban datos predeterminados?", "Aviso", globales.menuPrincipal);
                    if (DialogResult.Yes == pregunta)
                    {
                        txtfecha_solic.Text = string.Format("{0:d}", DateTime.Now);
                        string query = "select nombre from catalogos.firmas where clave = 'CAT'";
                        List<Dictionary<string, object>> resultado = globales.consulta(query);
                        if (resultado.Count != 0)
                        {
                            Dictionary<string, object> diccionario = resultado[0];
                            string nombre_catalogo = diccionario["nombre"].ToString();
                            txtNombre.Text = nombre_catalogo;
                        }
                    }
                }
                sePregunto = true;
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtExpediente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                SendKeys.Send("{TAB}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cerrar();
        }

        private void cerrar()
        {
            this.Close();
            globales.showModal(new frmDocumentos(1));
            
        }

        private void frmavaluo_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void frmavaluo_Shown(object sender, EventArgs e)
        {
            ActiveControl = button2;
        }
    }
}




