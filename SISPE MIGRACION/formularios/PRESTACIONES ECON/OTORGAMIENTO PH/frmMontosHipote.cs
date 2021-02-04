using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH
{
    public partial class frmMontosHipote : Form
    {
        public frmMontosHipote()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmMontosHipote_KeyDown(object sender, KeyEventArgs e)
        {
            if(Keys.F2 == e.KeyCode) this.Owner.Close();
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
