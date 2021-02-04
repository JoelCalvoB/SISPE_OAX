using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.PRINCIPAL_PH.extras
{
    public partial class frmAutorizaCreditos : Form
    {
        private List<Dictionary<string, object>> resultado;
        public frmAutorizaCreditos(List<Dictionary<string,object>> resultado)
        {
            InitializeComponent();
            this.resultado = resultado;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmAutorizaCreditos_Load(object sender, EventArgs e)
        {

            foreach (Dictionary<string,object> item in this.resultado) {
                string nombre = Convert.ToString(item["nombre_em"]);
                string expediente = Convert.ToString(item["folio"]);
                string fechaEmischeq = Convert.ToString(item["f_emischeq"]).Replace(" 12:00:00 a. m.","");
                string importe = globales.convertMoneda(globales.convertDouble(Convert.ToString(item["importe"])));


                dtggrid.Rows.Add(expediente,nombre,fechaEmischeq,importe);

            }

        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
