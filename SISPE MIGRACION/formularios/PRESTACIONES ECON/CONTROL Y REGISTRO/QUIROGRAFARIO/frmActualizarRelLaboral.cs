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
    public partial class frmActualizarRelLaboral : Form
    {
        private bool esHipote { get; set; }
        public frmActualizarRelLaboral(bool esHipote = false)
        {
            InitializeComponent();
            this.esHipote = esHipote;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime datos = fe1.Value;
                int dia = datos.Day;
                if (dia > 15) {
                    globales.MessageBoxExclamation("Fecha pertenece a segunda quincena.", "Fecha quincena", this);
                    return;
                }

                Cursor = Cursors.WaitCursor;

                string tipoRelacion = this.esHipote ? "H" : "Q";

                globales.MessageBoxInformation("Se seleccionara el último tipo de relación laboral de aportaciones al fondo..", "Seleccionando", this);
                string query = string.Format("select rfc,tipo_rel,folio from datos.solicitud_dependencias where (tipo_rel is null or tipo_rel = '') and  f_descuento = '{0}' and t_prestamo = '{1}'", string.Format("{0:yyyy-MM-dd}", datos),tipoRelacion);
                List<Dictionary<string, object>> resultado = globales.consulta(query);

                if (resultado.Count == 0) { goto ir; }

                string query2 = "select rfc,tipo_rel from datos.empleados where (tipo_rel is not null or tipo_rel <> '') and rfc in (";
                foreach (Dictionary<string, object> item in resultado) {
                    string rfc = Convert.ToString(item["rfc"]);
                    query2 += $"'{rfc}',";
                }

                query2 = query2.Substring(0, query2.Length - 1) + ");";

                List<Dictionary<string, object>> resultado2 = globales.consulta(query2);
                query = "";

                foreach (Dictionary<string, object> item in resultado2) {
                    List<Dictionary<string, object>> seleccion = resultado.Where(o => Convert.ToString(o["rfc"]) == Convert.ToString(item["rfc"])).ToList<Dictionary<string, object>>();
                    foreach (Dictionary<string, object> item2 in seleccion) {
                        string folio = Convert.ToString(item2["folio"]);
                        string tipo_rel = Convert.ToString(item["tipo_rel"]);
                        query += string.Format("update datos.solicitud_dependencias set tipo_rel = '{0}' where folio = '{1}' and t_prestamo = '{2}';", tipo_rel, folio,tipoRelacion);
                    }
                }
                ir:
                if (globales.consulta(query, true))
                {
                    
                    globales.MessageBoxSuccess("Se actualizó Tipo de Relación Laboral de los FOLIOS a descontar desde el " + fe1.Text, "Terminado", this);
                }
                else {
                    globales.MessageBoxExclamation("No se a podido actualizar la relación laboral","Aviso",this);
                }
            
                
            }
            catch {
                globales.MessageBoxError("Error en el proceso de actualizar","Error en el proceso de actualizar",this);
            }
            this.Cursor = Cursors.Default;
            this.Owner.Close();
        }

        private void frmActualizarRelLaboral_Load(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            fecha = fecha.AddMonths(1);
            if (fecha.Day > 15)
            {
                fecha = new DateTime(fecha.Year, fecha.Month, (fecha.Month == 2) ? 28 : 30);
            }
            else {
                fecha = new DateTime(fecha.Year, fecha.Month, 15);
            }
            fecha = new DateTime(fecha.Year, fecha.Month, 15);//nueva modificacion SAMV
            fe1.Value = fecha;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
