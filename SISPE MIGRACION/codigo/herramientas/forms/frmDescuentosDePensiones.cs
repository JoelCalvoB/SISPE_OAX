using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas.forms
{
    public partial class frmDescuentosDePensiones : Form
    {
        public bool esAceptar = true;
        internal cambiarDatos cambiar;
        public frmDescuentosDePensiones()
        {
            InitializeComponent();
            PER.PreviewKeyDown += new PreviewKeyDownEventHandler(pasar);
            DED3.PreviewKeyDown += new PreviewKeyDownEventHandler(pasar);
            DED4.PreviewKeyDown += new PreviewKeyDownEventHandler(pasar);
            DED5.PreviewKeyDown += new PreviewKeyDownEventHandler(pasar);
            DED6.PreviewKeyDown += new PreviewKeyDownEventHandler(pasar);
            ROY1.PreviewKeyDown += new PreviewKeyDownEventHandler(pasar);
            ROY2.PreviewKeyDown += new PreviewKeyDownEventHandler(pasar);
        }

        private void pasar(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                SendKeys.Send("{TAB}");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            esAceptar = false;
            Close();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            esAceptar = true;
            Close();
        }

        private void frmDescuentosDePensiones_Load(object sender, EventArgs e)
        {
            PER.Text =  (PER.Text);
            DED3.Text = (DED3.Text);
            DED4.Text = (DED4.Text);
            DED5.Text = (DED5.Text);
            DED6.Text = (DED6.Text);
        }

        private void PER_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);
        }

        private void PER_TextChanged(object sender, EventArgs e)
        {
            cambiar(((TextBox)PER).Text);
        }

        private void PER_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(PER.Text) || ((PER.Text.Contains("$") || PER.Text.Contains(".")) && (PER.Text.Length == 1 || PER.Text.Length == 2)))
                PER.Text = string.Format("{0:C}", 0);
            else
            {
                PER.Text = string.Format("{0:C}", double.Parse(PER.Text, NumberStyles.Currency));

            }
        }
    }
}
