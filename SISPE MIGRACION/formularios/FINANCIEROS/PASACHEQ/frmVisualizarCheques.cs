using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.FINANCIEROS.PASACHEQ
{
    public partial class frmVisualizarCheques : Form
    {
        public frmVisualizarCheques()
        {
            InitializeComponent();
        }

        private void frmVisualizarCheques_Load(object sender, EventArgs e)
        {
            string query = "SELECT * from financieros.datoscheques order by numcheque asc";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            resultado.ForEach(o => datos.Rows.Add(o["fecha"],o["numpoliz"], o["numcheque"], o["benefic"], o["concep1"], string.Format("{0:C}", o["impcheque"])));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
