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
    public partial class frmSeleccionarRegistro : Form
    {
        public frmSeleccionarRegistro()
        {
            InitializeComponent();
        }

        private void frmSeleccionarRegistro_Load(object sender, EventArgs e)
        {
            list.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (list.SelectedIndex == 0)
            {
                globales.showModal(new frmQuirografarioRegistroManuales());
            }
            else {
                globales.showModal(new frmQuirografarioRegistroManuales(true));
            }
        }

        private void list_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void frmSeleccionarRegistro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                button1_Click(null,null);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
