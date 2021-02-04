using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones
{
    public partial class frmVentanaImpConstancias : Form
    {
        private string monto = "$0.00";
        internal string concepto1, concepto2, concepto3 = string.Empty;
        internal string monto1, monto2, monto3, total = string.Empty , nombre_f=string.Empty,nombre_benefi=string.Empty;
        internal bool continuaVentana = false;
        private string nombre = string.Empty;

        private void txtImporte1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);
        }

        private void txtImporte1_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            string texto = txt.Text;
            double numero = 0;


            try
            {
                numero = double.Parse(texto, System.Globalization.NumberStyles.Currency);
            }
            catch
            {
                numero = 0;
            }

            txt.Text = string.Format("{0:C}", numero);


            double t1 = double.Parse(txtMonto.Text, System.Globalization.NumberStyles.Currency);
            double t2 = double.Parse(txtImporte1.Text, System.Globalization.NumberStyles.Currency);
            double t3 = double.Parse(txtImporte2.Text, System.Globalization.NumberStyles.Currency);
            double t4 = double.Parse(txtImporte3.Text, System.Globalization.NumberStyles.Currency);


            double total = t1 - (t2 + t3 + t4);
            txtTotal.Text = string.Format("{0:C}", total);

        }

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            this.p1.Visible = this.chk1.Checked;
        }

        private void txtImporte1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        public frmVentanaImpConstancias(string monto, string nombre)
        {
            InitializeComponent();
            this.monto = monto;
            this.nombre = nombre;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmVentanaImpConstancias_Load(object sender, EventArgs e)
        {
            txtMonto.Text = this.monto;
            txtFallecido.Text = this.nombre;
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Seguro continuar con la operacion?", "Aviso", globales.menuPrincipal);
            if (p == DialogResult.No) return;

            if (chk1.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtBenefi.Text))
                {
                    globales.MessageBoxExclamation("Ingresar el nombre del beneficiario", "Aviso", globales.menuPrincipal);
                    this.ActiveControl = txtBenefi;
                    return;
                }
            }

            double t1 = double.Parse(txtMonto.Text, System.Globalization.NumberStyles.Currency);
            double t2 = double.Parse(txtImporte1.Text, System.Globalization.NumberStyles.Currency);
            double t3 = double.Parse(txtImporte2.Text, System.Globalization.NumberStyles.Currency);
            double t4 = double.Parse(txtImporte3.Text, System.Globalization.NumberStyles.Currency);


            double total = t1 - (t2 + t3 + t4);
            txtTotal.Text = string.Format("{0:C}", total);

            this.concepto1 = txtConcepto1.Text;
            this.concepto2 = txtConcepto2.Text;
            this.concepto3 = txtConcepto3.Text;

            this.monto1 = txtImporte1.Text;
            this.monto2 = txtImporte2.Text;
            this.monto3 = txtImporte3.Text;
            this.nombre_f = txtFallecido.Text;
            this.nombre_benefi = txtBenefi.Text;

            this.total = txtTotal.Text;
            this.continuaVentana = true;
            this.Close();
        }
    }
}
