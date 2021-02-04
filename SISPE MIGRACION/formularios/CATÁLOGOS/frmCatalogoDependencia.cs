using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    public partial class frmCatalogoDependencia : Form
    {
        public frmCatalogoDependencia()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas cerrar el modulo?", globales.menuPrincipal);
            if (p == DialogResult.Yes)
                this.Close();
        }

        private void frmCatalogoDependencia_Load(object sender, EventArgs e)
        {
            string query = "select * from catalogos.cuentas order by cuenta ";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            resultado.ForEach(o => datos.Rows.Add(o["cuenta"],o["descripcion"],o["proy"]));
        }

        private void frmCatalogoDependencia_KeyDown(object sender, KeyEventArgs e)
        {
            button2_Click(null, null);
        }
    }
}
