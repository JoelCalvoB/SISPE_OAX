using SISPE_MIGRACION.codigo.herramientas.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ
{
    
    public partial class frmTasas : Form
    {
        private List<string> relacionesLaborales;
        public frmTasas()
        {
            InitializeComponent();
        }

        private void frmTasas_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string query = "select * from datos.c_tasai";
                List<Dictionary<string, object>> resultado = globales.consulta(query);

                if (resultado.Count == 0) {
                    globales.MessageBoxExclamation("No existen tasas de interes, favor de ingresar la nueva tasa", "Aviso", this);
                    return;
                }

                txtTrel.Text = Convert.ToString(resultado[0]["trel"]);
                txtTasas_q.Text = Convert.ToString(resultado[0]["tasa_q"]);
                resultado.ForEach(o => cmbDescripcion.Items.Add(o["descripcion"]));
                cmbDescripcion.SelectedIndex = 0;
                cmbDescripcion.Focus();
                relacionesLaborales = new List<string>();
                resultado.ForEach(o => relacionesLaborales.Add(Convert.ToString(o["trel"])));

            }
            catch {
                globales.MessageBoxError("Error, favor de contactar a los de sistemas", "Aviso", this);
            }

            this.Cursor = Cursors.Default;
        }

        private void cmbDescripcion_TextChanged(object sender, EventArgs e)
        {
            string query = string.Format("select * from datos.c_tasai where descripcion = '{0}'",cmbDescripcion.Text);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            txtTrel.Text = Convert.ToString(resultado[0]["trel"]);
            txtTasas_q.Text = Convert.ToString(resultado[0]["tasa_q"]);
            datos.Rows.Clear();
            query = string.Format("select * from catalogos.tasa where trel = '{0}' and t_prestamo = 'Q' order by id desc",txtTrel.Text);
            resultado = globales.consulta(query);
            resultado.ForEach(o => datos.Rows.Add(o["fmodif"].ToString().Replace(" 12:00:00 a. m.", ""), string.Format("{0:C4}", o["tasa"]).Replace("$",""), o["fum"].ToString().Replace(" 12:00:00 a. m.", ""), o["hum"], o["uuqm"],cmbDescripcion.Text));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAgregarTasaInteres tasa;
            tasa = (frmAgregarTasaInteres) globales.showModalReturning(new frmAgregarTasaInteres());
            string txt1Fecha = tasa.txtFecha;
            string txt1Interes = tasa.txtInteres;
            DateTime auxFechaActual = DateTime.Now;
            string fechaActual = string.Format("{0}-{1}-{2}", auxFechaActual.Year, auxFechaActual.Month, auxFechaActual.Day).Replace(" 12:00:00 a. m.", ""); ; 
            string horaActual = string.Format("{0}:{1}",auxFechaActual.Hour,auxFechaActual.Minute);
            if (tasa.aceptar) {
                DialogResult resultado = globales.MessageBoxQuestion("¿Desea que todos las relaciones laborales sea afectado con la nueva tasa de interes?","Tasa de intereses",this);
                string query = string.Empty;
                if (resultado == DialogResult.Yes)
                {
                    foreach (string item in this.relacionesLaborales) {
                        query = string.Format("insert into catalogos.tasa(trel,fmodif,tasa,uuqm,fum,hum,t_prestamo) values ('{0}','{1}',{2},'{3}','{4}','{5}','{6}')", item, txt1Fecha, txt1Interes, "T", fechaActual, horaActual, "Q");
                        if (!globales.consulta(query, true))
                            globales.MessageBoxExclamation("Error en el sistema, contacte a los de sistemas", "Error en la consulta", this);
                    }
                    globales.MessageBoxSuccess("Tasas afectadas correctamente","Proceso terminado",this);
                }
                else {
                    query = string.Format("insert into catalogos.tasa(trel,fmodif,tasa,uuqm,fum,hum,t_prestamo) values ('{0}','{1}',{2},'{3}','{4}','{5}','{6}')",txtTrel.Text,txt1Fecha,txt1Interes,"T",fechaActual,horaActual,"Q");
                    if (!globales.consulta(query, true)) globales.MessageBoxError("Error en el sistema, contacte a los de sistemas", "Error en la consulta", this);
                    else
                       globales.MessageBoxSuccess(string.Format("Relación laboral {0} afectada por la tasa de interes actual",this.cmbDescripcion.Text),"Proceso terminado",this);
                }
                cmbDescripcion_TextChanged(null,null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           globales.showModal(new frmImprimirReporteTasas());
        }

        private void cmbDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void datos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1) return;
                int fila = e.RowIndex;
                int columna = e.ColumnIndex;

                DataGridViewRow row = datos.Rows[fila];
                string tipoRelacion = txtTrel.Text;
                string fecha = (string)row.Cells[0].Value;
                double tasaAnual = Convert.ToDouble(row.Cells[1].Value);
                string query = $"update  catalogos.tasa set tasa = {tasaAnual} where fmodif = '{fecha}' and trel = '{tipoRelacion}' and t_prestamo = 'Q'";
                if (globales.consulta(query, true)) {
                    globales.MessageBoxSuccess("Registro actualizado correctamente","Aviso",this);
                }
            }
            catch(Exception ex) {
                globales.MessageBoxError(ex.ToString(),"Aviso",this);
            }

        }

        private void frmTasas_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
         
                this.Owner.Close();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void datos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
