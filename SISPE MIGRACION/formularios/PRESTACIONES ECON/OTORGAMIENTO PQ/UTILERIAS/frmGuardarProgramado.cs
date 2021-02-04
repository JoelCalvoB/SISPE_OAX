using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.UTILERIAS
{
    public partial class frmGuardarProgramado : Form
    {
        private List<Dictionary<string, object>> resultado;
        private int cantidad { get; set; }
        public frmGuardarProgramado(List<Dictionary<string, object>> resultado, int cantidad)
        {
            InitializeComponent();
            this.cantidad = cantidad;
            this.resultado = resultado;
        }

        private void frmGuardarProgramado_Load(object sender, EventArgs e)
        {
            resultado.ForEach(o => gridcheques.Rows.Add(Convert.ToString(o["fecha"]).Replace(" 12:00:00 a. m.", ""), (o["enable"]=="*")? true :false,o["dia"]));
          //foreach(var item in resultado)
          //  {
          //      string fecha = Convert.ToString(item["fecha"]).Replace(" 12:00:00 a. m.", "");
          //      string enable = Convert.ToString(item["enable"]);
          //      bool enab = false; ;
          //      if (enable=="*")
          //      {
          //           enab = true;
          //      }
          //      string dia = Convert.ToString(item["dia"]);

          //      gridcheques.Rows.Add(fecha, enab, dia);

          //  }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int contador = 0;
            foreach (DataGridViewRow item in gridcheques.Rows) {
                bool boleano = Convert.ToBoolean(item.Cells[1].Value);
                resultado[contador]["enable"] = (boleano) ? "*" : "";
                contador++;
            }


            contador = 0;
            for (int x = 0; x < Convert.ToInt32(cantidad); x++)
            {
                regresar:
                if (contador == resultado.Count)
                    contador = 0;

                if (Convert.ToString(resultado[contador]["enable"]) == "*")
                {
                    contador++;
                    goto regresar;
                }

                resultado[contador]["cantidad"] = Convert.ToInt32(resultado[contador]["cantidad"]) + 1;
                contador++;
            }

            gridcheques.Rows.Clear();

            resultado.ForEach(o => gridcheques.Rows.Add(o["fecha"], (o["enable"] == "*") ? true : false,  o["dia"], o["cantidad"]));
            foreach (Dictionary<string, object> item in resultado) {
                string[] fechaA = Convert.ToString(item["fecha"]).Replace(" 12:00:00 a. m.", "").Split('/');
                string fecha = string.Format("{0}-{1}-{2}", fechaA[2], fechaA[1], fechaA[0]);
                string habilitado = Convert.ToString(item["enable"]);
                string cantidad = Convert.ToString(item["cantidad"]);
                string query = string.Format("insert into catalogos.progpq(fecha,inhabil,utilizados,programados) values('{0}','{1}',{2},{3})", fecha , habilitado, 0,cantidad);
                globales.consulta(query,true);
            }
            button1.Enabled = false;
            gridcheques.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
