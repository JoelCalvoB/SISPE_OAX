using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA.REPORTES.REGISTROS_MANUALES
{
    public partial class frmQuirografarioRegistroManuales : Form
    {

        private bool esHipotecario { get; set; }
        public frmQuirografarioRegistroManuales(bool esHipotecario = false)
        {
            InitializeComponent();
            this.esHipotecario = esHipotecario;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult p = globales.MessageBoxQuestion("¿Deseas realizar la operación?","Aviso",this);
                if (p == DialogResult.No) 
                    return;


                

                string tabla =  !esHipotecario ? "p_edocta":"p_edocth";
                string t_prestamo = !esHipotecario ? "Q" : "H";

                this.Cursor = Cursors.WaitCursor;
                string query = $"select p.folio,p.nombre_em,d.proyecto,d.f_descuento,d.rfc,d.importe,d.fum from datos.descuentos d inner join datos.{tabla} p on p.folio = d.folio where (d.f_descuento BETWEEN '{string.Format("{0:yyyy-MM-dd}",globales.convertDatetime(dtPago1.Text))}' and '{string.Format("{0:yyyy-MM-dd}", globales.convertDatetime(dtPago2.Text))}') and (d.fum between '{string.Format("{0:yyyy-MM-dd}", globales.convertDatetime(dtMod1.Text))}' and '{string.Format("{0:yyyy-MM-dd}", globales.convertDatetime(dtMod2.Text))}') and d.t_prestamo = '{t_prestamo}' and hum <> ''  order by p.proyecto ASC";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count == 0) {
                    globales.MessageBoxExclamation("No se encontró información","Aviso",this);
                    this.Cursor = Cursors.Default;
                    return;
                }
                object[][] cuerpo = new object[resultado.Count][];
                int contador = 0;
                foreach (Dictionary<string,object> item in resultado) {
                    cuerpo[contador] = new object[] {
                        item["folio"],item["rfc"],item["nombre_em"],item["proyecto"],string.Format("{0:d}",item["f_descuento"]),string.Format("{0:d}",item["fum"]),item["importe"]
                    };
                    contador++;
                }
                string[] meses = globales.getMeses();

                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);


                string titulo = $"Pagos registrados manualmente en el estado de Cuenta de {(!esHipotecario? "Quirografarios":"Hipotecarios")}";

                string cadena = $"En el mes de {meses[globales.convertDatetime(dtPago1.Text).Month]} del año {globales.convertDatetime(dtPago1.Text).Year} modificados en el mes de {meses[globales.convertDatetime(dtMod1.Text).Month]} del año {globales.convertDatetime(dtMod1.Text).Year}";
                object[][] parametros = new object[2][];
                object[] header = { "fechas","titulo", "logo" };
                object[] body = { cadena,titulo, logo };

                parametros[0] = header;
                parametros[1] = body;

                globales.reportes("reportePagosModificadosRModif", "p_quirog", cuerpo, "", false, parametros);
                this.Cursor = Cursors.Default;
            }
            catch {
                globales.MessageBoxError("Error en el sistema, favor de contactar a los de sistemas","Aviso",globales.menuPrincipal);
            }

            this.Cursor = Cursors.Default;
        }

        private void frmQuirografarioRegistroManuales_Load(object sender, EventArgs e)
        {
            dtPago1.Text = string.Format("{0:d}", DateTime.Now);
            dtPago2.Text = string.Format("{0:d}", DateTime.Now);
            dtMod1.Text = string.Format("{0:d}", DateTime.Now);
            dtMod2.Text = string.Format("{0:d}", DateTime.Now);


        }

        private void frmQuirografarioRegistroManuales_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void frmQuirografarioRegistroManuales_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
