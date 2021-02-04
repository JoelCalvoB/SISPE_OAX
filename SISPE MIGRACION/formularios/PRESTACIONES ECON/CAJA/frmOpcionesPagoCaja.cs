using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.CAJA
{
    public partial class frmOpcionesPagoCaja : Form
    {
        public frmOpcionesPagoCaja()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rdEdicion.Checked)
            {
                    globales.showModal(new p_caja(rdHipotecario.Checked));
            }
            else {
                    globales.showModal(new frmImpresion(rdHipotecario.Checked));
            }
        }

        private void frmOpcionesPagoCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
                button1_Click(null,null);
        }

        private void frmOpcionesPagoCaja_Load(object sender, EventArgs e)
        {
            this.ActiveControl = rdQuiro;
        }

        private void frmOpcionesPagoCaja_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
                this.Owner.Close();
        }
    }
}
