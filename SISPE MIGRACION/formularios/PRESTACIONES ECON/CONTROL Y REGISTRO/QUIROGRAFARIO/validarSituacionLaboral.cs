using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.QUIROGRAFARIO
{
    public partial class validarSituacionLaboral : Form
    {
        private bool esHipote { get; set; }
        public validarSituacionLaboral(bool esHipote = false)
        {
            InitializeComponent();
            this.esHipote = esHipote;
        }

        private void validarSituacionLaboral_Load(object sender, EventArgs e)
        {


            DateTime aux = new DateTime(DateTime.Now.Year,DateTime.Now.Month,15);
            DateTime fecha1 = aux.AddMonths(1);            

            DateTime fecha2 = aux.AddMonths(-1);
            if (fecha2.Month == 2)
            {
                fecha2 = new DateTime(fecha2.Year, fecha2.Month, 28);
            }
            else
            {
                fecha2 = new DateTime(fecha2.Year, fecha2.Month, 30);
            }

            fe1.Value = fecha1;
            fe2.Value = fecha2;
        }

        private void fe1_ValueChanged(object sender, EventArgs e)
        {
            DateTime fecha = fe1.Value;
            fecha = fecha.AddDays(-30);
            if (fecha.Month == 2)
            {
                fecha = new DateTime(fecha.Year, fecha.Month, 28);
            }
            else
            {
                fecha = new DateTime(fecha.Year, fecha.Month, 30);
            }
            fe2.Value = fecha;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var n = globales.MessageBoxQuestion("¿Desea efectuar la operación?", "Seleccionar folios", this);
                if (DialogResult.No == n)
                {
                    globales.MessageBoxInformation("Operación cancelada", "Cancelado", this);
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                string tipoPrestamo = this.esHipote ? "H" : "Q";

                string fecha1 = string.Format("{0:yyyy-MM-dd}", fe1.Value);
                string query = $"create temp table solici1 as select folio,rfc,nombre_em,numdesc,totdesc,imp_unit,tipo_mov,sec,tipo_rel,f_descuento,'' as marca from datos.solicitud_dependencias where t_prestamo = '{tipoPrestamo}' and f_descuento = '{fecha1}';" +
                                "create temp table ultimo as select solici1.folio,max(a1.final) as ultimo from solici1 inner join datos.aportaciones a1 on a1.rfc = solici1.rfc group by solici1.folio;" +
                                "select solici1.*,ultimo.ultimo,d.descripcion from solici1 inner join ultimo on solici1.folio = ultimo.folio left join catalogos.disket d on d.cuenta = solici1.tipo_rel;";

                List<Dictionary<string, object>> resultado = globales.consulta(query);
                object[] objeto = new object[resultado.Count];
                int contador = 0;
                foreach (Dictionary<string, object> item in resultado)
                {
                    object[] tmp = {
                        item["folio"],item["sec"],item["tipo_mov"],item["rfc"],item["nombre_em"],string.Format("{0:d}",DateTime.Parse(Convert.ToString(item["f_descuento"]))),
                        $"{item["numdesc"]}/{item["totdesc"]}",item["imp_unit"],string.Format("{0:d}",DateTime.Parse(Convert.ToString(item["ultimo"]))),item["tipo_rel"],item["descripcion"]
                    };
                    objeto[contador] = tmp;
                    contador++;
                }

                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                string titulo = this.esHipote ? "PRESTAMOS HIPOTECARIOS":"PRESTAMOS QUIROGRAFARIOS" ;
                object[][] parametros = new object[2][];
                object[] header = { "titulo", "logo" };
                object[] body = { titulo, logo };
                parametros[0] = header;
                parametros[1] = body;

                globales.reportes("reporteSituacionLaboral", "situacionlaboral", objeto,"",false,parametros);

                this.Cursor = Cursors.Default;
            }
            catch {

            }
        }




        private void generarReporte(List<Dictionary<string, object>> family)
        {
            globales.MessageBoxInformation("Se va a generar el reporte", "Reporte", this);

            
        }

        private string agregarEspacios(string txt, int verdadero, int diferencia)
        {
            int cantidad =  verdadero - diferencia;
            for (int x = 0; x < cantidad; x++) {
                txt += " ";
            }
            return txt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
