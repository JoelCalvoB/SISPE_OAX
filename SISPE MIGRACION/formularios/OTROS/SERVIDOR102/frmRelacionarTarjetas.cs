using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.OTROS.SERVIDOR102
{
    public partial class frmRelacionarTarjetas : Form
    {
        private string rfc = string.Empty;
        private List<Dictionary<string, object>> resultado;
        public frmRelacionarTarjetas(string rfc,List<Dictionary<string,object>> resultado)
        {
            InitializeComponent();
            this.rfc = rfc;
            this.resultado = resultado;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmRelacionarTarjetas_Load(object sender, EventArgs e)
        {
            txtrfc.Text = this.rfc;
            string query = $"select rfc,nombre_em from datos.empleados where rfc = '{this.rfc}'";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            Dictionary<string, object> obj = resultado[0];
            txtnombre.Text = Convert.ToString(obj["nombre_em"]);
            foreach (Dictionary<string,object> item in this.resultado) {
                datos.Rows.Add(item["inicio"],item["final"],item["new_tipo"],item["aportacion"],0,"","",string.Format("{0:d}",DateTime.Now));
            }

            txtFechaRegistro.Text = string.Format("{0:d}",DateTime.Now);
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar estas aportaciones en la base de datos?","Aviso",this);
            if (p == DialogResult.No) return;

            string query = "";
            foreach (DataGridViewRow item in datos.Rows) {
                string inicio = string.Format("'{0:yyyy-MM-dd}'",DateTime.Parse(Convert.ToString(item.Cells[0].Value)));
                string final = string.Format("'{0:yyyy-MM-dd}'", DateTime.Parse(Convert.ToString(item.Cells[1].Value)));
                string new_mov = Convert.ToString(item.Cells[2].Value);
                double entrada = globales.convertDouble(Convert.ToString(item.Cells[3].Value));
                string salida = Convert.ToString(item.Cells[4].Value);
                string cuenta = Convert.ToString(item.Cells[5].Value);
                string comentario = Convert.ToString(item.Cells[6].Value);
                string fecharegistro = string.Format("{0:yyyy-MM-dd}",DateTime.Parse(txtFechaRegistro.Text));

                if (inicio == "16/11/2013") {

                }

                string aux = "insert into datos.aportaciones(rfc,inicio,final,new_tipo,movimiento,entrada,salida,status,fecharegistro)"+
                                                 " values ('{0}',{1},   {2},  '{3}',      '',      {4},      0,    'n',     '{5}');";
                aux = string.Format(aux,txtrfc.Text,inicio,final,new_mov,entrada,fecharegistro);

                query += aux;
            }

            if (globales.consulta(query,true)) {
                globales.MessageBoxSuccess("Registros agregados correctamente","Aviso",this);
                this.Owner.Close();
            }
        }
    }
}
