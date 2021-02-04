using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Seguros
{
    public partial class frmactualizaseguro : Form
    {
        public frmactualizaseguro()
        {
            InitializeComponent();
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Enter)
            {
                bool opcion = (opcionqh.SelectedIndex == 0);
                globales.showModal(new frmactquiro(opcion));
            }
        }

        private void frmactualizaseguro_Load(object sender, EventArgs e)
        {
        
        }

        private void opcionqh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
