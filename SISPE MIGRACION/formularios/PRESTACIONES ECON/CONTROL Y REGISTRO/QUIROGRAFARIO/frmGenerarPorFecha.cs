using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.QUIROGRAFARIO
{
    public partial class frmGenerarPorFecha : Form
    {
        public frmGenerarPorFecha()
        {
            InitializeComponent();
        }

        private void frmGenerarPorFecha_Load(object sender, EventArgs e)
        {
            DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
            fecha = fecha.AddDays(30);
            fecha = new DateTime(fecha.Year,fecha.Month,15);
            txtFecha.Text = string.Format("{0:d}",fecha);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFecha.Text)) {
                    globales.MessageBoxExclamation("Favor ingresar gecha a generar","Aviso",this);
                    txtFecha.Focus();
                    return;
                } 


                Cursor = Cursors.WaitCursor;
             
                globales.MessageBoxInformation("Se va a generar el estado de cuenta y solicitudes de descuento", "Estados de cuenta", this);
                string fechaTexto = string.Format("{0:yyyy-MM-dd}", txtFecha.Value);
                string query = string.Format("select * from datos.p_quirog where f_primdesc = '{0}'", fechaTexto);
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                string queryGlobal = string.Empty;
                foreach (Dictionary<string, object> item in resultado)
                {
                 
                    query = string.Format("select * from datos.p_edocta where folio = {0}", item["folio"]);
                    List<Dictionary<string,object>> resultado2 = globales.consulta(query);
                    if (resultado2.Count != 0) continue;

                    string f_solicitud = string.Empty;
                    string f_emischeq = string.Empty;
                    string f_primdesc = string.Empty;

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(item["f_solicitud"])))
                    {
                        f_solicitud = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(Convert.ToString(item["f_solicitud"])));
                    }

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(item["f_emischeq"])))
                    {
                        f_emischeq = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(Convert.ToString(item["f_emischeq"])));
                    }

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(item["f_primdesc"])))
                    {
                        f_primdesc = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(Convert.ToString(item["f_primdesc"])));
                    }

                    string tipo_relacionlaboral = Convert.ToString(item["secretaria"]);

                    tipo_relacionlaboral = (tipo_relacionlaboral == "J" || tipo_relacionlaboral == "P" || tipo_relacionlaboral == "T") ? tipo_relacionlaboral : Convert.ToString(item["tipo_rel"]);



                    query = string.Format("insert into datos.p_edocta values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}',{12},{13},{14},'');",
                        item["folio"], item["rfc"], item["nombre_em"], item["direccion"], item["proyecto"], item["secretaria"], item["descripcion"], f_solicitud, f_emischeq, f_primdesc, string.IsNullOrWhiteSpace(Convert.ToString(item["antig_q"]))?"0": Convert.ToString(item["antig_q"]), item["tipo_pago"], item["plazo"], item["imp_unit"], item["importe"]);
                    queryGlobal += query;
                    query = string.Format("insert into datos.solicitud_dependencias(folio,tipo_mov,sec,tipo_rel,f_descuento,numdesc,totdesc,imp_unit,rfc,nombre_em,proyecto,t_prestamo) values({0},'{1}','{2}','{3}','{4}',{5},{6},{7},'{8}','{9}','{10}','Q');",
                                        item["folio"], "AN", "1", tipo_relacionlaboral, f_primdesc, 1, item["plazo"], item["imp_unit"], item["rfc"], item["nombre_em"], item["proyecto"]);

                    queryGlobal += query;
                }
                this.Cursor = Cursors.Default;
                globales.consulta(queryGlobal,true);
                globales.MessageBoxSuccess("Generación de fecha finalizada!","Proceso terminado",this);
                this.Owner.Close();
            }
            catch
            {
                Cursor = Cursors.Default;
                
                globales.MessageBoxError("Error en el sistema, porfavor contactar al departamento de sistemas","Error en el sistema",this);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtFecha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                button1_Click(null,null);
            }
        }

        private void txtFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == '/' || e.KeyChar == 8);
        }

        private void txtFecha_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}