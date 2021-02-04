using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas.forms
{
    public partial class frmImpresionQuirografario : Form
    {
        internal int checador = 0;
        internal bool aceptar = false;
        private string carta = string.Empty;
        public frmImpresionQuirografario(string carta = "N")
        {
            InitializeComponent();
            this.carta = carta;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (r1.Checked)
                checador = 1;

            if (r2.Checked)
                checador = 2;

            if (r3.Checked)
                checador = 3;

            if (r4.Checked)
                checador = 4;

            if (r5.Checked)
                checador = 5;

            this.aceptar = true;
            Close();
        }

        private void frmImpresionQuirografario_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void r1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(null,null);
        }

        private void frmImpresionQuirografario_Load(object sender, EventArgs e)
        {
            if (carta != "S") {
                r2.Visible = false;
                r5.Visible = false;
            }
        }
    }
}
