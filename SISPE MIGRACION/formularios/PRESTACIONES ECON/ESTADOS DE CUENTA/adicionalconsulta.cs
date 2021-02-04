using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA
{
    public partial class adicionalconsulta : Form
    {
        public adicionalconsulta()
        {
            InitializeComponent();
        }

        public void txttotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void adicionalconsulta_Load(object sender, EventArgs e)
        {

            adicionalconsulta ventana = new adicionalconsulta();

            if (string.IsNullOrWhiteSpace(txtMoratorios.Text))
            {
                txtMoratorios.Visible = false;
                label4.Visible = false;
            }

        }

        private void adicionalconsulta_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.Escape == e.KeyCode)
            {
                this.Owner.Close();
            }
        }

        private void txtsaldo_TextChanged(object sender, EventArgs e)
        {
            double saldo = globales.convertDouble(txtsaldo.Text);
            if (saldo < 0)
            {
                txtsaldo.BackColor = Color.FromArgb(180, 0, 0);
                txtsaldo.ForeColor = Color.White;
            }


        }
    }
}
