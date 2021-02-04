using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.REPORTES
{
    public partial class frmTiporelacion : Form
    {
        private List<Dictionary<string, string>> tmp;
        private string query { get; set; }
        private DateTime fecha { get; set; }
        private string tipoReporte { get; set; }
        private string altas { get; set; }

        private bool esQuiro { get; set; }

        public frmTiporelacion()
        {
            InitializeComponent();
        }

        private void frmTiporelacion_Load(object sender, EventArgs e)
        {
            try
            {
                if (tmp.Count == 0) {
                    globales.MessageBoxExclamation("Sin registros, consultar otro","Aviso",this.Owner);
                    this.Owner.Close();
                    return;
                }

                foreach (Dictionary<string, string> item in tmp)
                {
                    this.list.Items.Add(item["cuenta"] + " | " + item["descripcion"]);
                }
                this.list.SelectedIndex = 0;
            }
            catch {
                globales.MessageBoxError("Error en el sistema..","Aviso",this.Owner);
            }
        }
        internal void setLista(List<Dictionary<string, string>> resultado, string cadena, DateTime fecha,string tipoReporte,string altas,bool esQuiro) {
            this.tmp = resultado;
            this.altas = altas;
              this.query = cadena.Replace("DISTINCT tipo_rel", " * ").Replace("order by tipo_rel desc", "order by folio desc").Replace("order by", "and tipo_rel = '{0}'  order by");
            this.fecha = fecha;
            this.tipoReporte = tipoReporte;

            this.esQuiro = esQuiro;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string texto = list.Text;
            texto = texto.Split('|')[0].Trim();

            string descripcionTexto = list.Text.Split('|')[1].Trim();

            string temporal = string.Format(this.query, texto);
            List<Dictionary<string, object>> resultado = globales.consulta(temporal);
            object[] objeto = new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado) {
                string folio = Convert.ToString(item["folio"]);
                string rfc = Convert.ToString(item["rfc"]);
                string nombre = Convert.ToString(item["nombre_em"]);
                string proyecto = Convert.ToString(item["proyecto"]);
                string serie = Convert.ToString(item["numdesc"]) + "/" + Convert.ToString(item["totdesc"]);
                string imp_unit = globales.checarDecimales(Convert.ToString(item["imp_unit"]));
                string folio_ = Convert.ToString(item["folio_"]);
                string numdesc_ = Convert.ToString(item["numdesc_"]);
                string totdesc_ = Convert.ToString(item["totdesc_"]);
                string imp_unit_ = string.IsNullOrWhiteSpace(Convert.ToString(item["imp_unit_"]))?"0.00":string.Format("{0:C}",double.Parse(Convert.ToString(item["imp_unit_"])));
                string serie_ = numdesc_ + " / " + totdesc_;
                string _folio = Convert.ToString(item["folio_"]);

                object[] tmp = { folio, rfc, nombre, proyecto, serie, "", imp_unit ,folio_,serie_, serie_, imp_unit_, _folio };
                objeto[contador] = tmp;
                contador++;
            }

            string queEs = "RELACIÓN DE DESCUENTOS";
            if (this.altas == "B") {
                queEs = "BAJAS DE DESCUENTOS";
            } else if (this.altas == "C") {
                queEs = "RELACIÓN DE CAMBIOS";
            }

            string prestamos = this.esQuiro ? "QUIROGRAFARIOS": "HIPOTECARIOS";

            string[] meses = { "", "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
            string fecha = string.Format("OAXACA DE JUÁREZ, OAX., A {0} DE {1} DEL {2}", DateTime.Now.Day, meses[DateTime.Now.Month], DateTime.Now.Year);
            string descripcion1 = string.Format("{0} {1} DE PRESTAMOS {3} PARA APLICAR AL PERSONAL {2}",queEs, (texto == "J" || texto == "P" || texto == "T" || texto == "JF" || texto == "PF" || texto == "TF")?"MENSUALES":"QUINCENALES", (texto == "J" || texto == "P" || texto == "T" || texto == "JF" || texto == "PF" || texto == "TF") ? "" : "DE",prestamos);
            string descripcion2 = texto.Replace("|",":");
            string descripcion3 = string.Format("A PARTIR DEL {0} DE {1} DEL {2}",this.fecha.Day,meses[this.fecha.Month],this.fecha.Year);

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[][] parametros = new object[2][];
            object[] headers = { "fecha","descripcion1", "descripcion2", "descripcion3","tipoReporte","total", "logo" };
            object[] body = { fecha, descripcion1, descripcion2 + " : "+ descripcionTexto, descripcion3, this.tipoReporte, contador.ToString(), logo };
            parametros[0] = headers;
            parametros[1] = body;
            if (this.altas == "C")
            {
                globales.reportes("reporteImpCambiosQ", "impAltaQ", objeto, "", false, parametros);
            }
            else {
                globales.reportes("reporteImpAltasQ", "impAltaQ", objeto, "", false, parametros);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
