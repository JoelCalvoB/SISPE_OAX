using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.FINANCIEROS.CHEQUES
{
    public partial class frmfirmas : Form
    {
        public frmfirmas()
        {
            InitializeComponent();
        }

        private void frmfirmas_Load(object sender, EventArgs e)
        {
            string firmas = "select * from financieros.firmas";
            List<Dictionary<string, object>> resultado = globales.consulta(firmas);
            this.textEntrego.Text= Convert.ToString(resultado[0]["entrego"]);
            this.textRecibio.Text = Convert.ToString(resultado[0]["recibio"]);
            this.textVobo.Text = Convert.ToString(resultado[0]["vobo"]);
            this.textReviso.Text = Convert.ToString(resultado[0]["reviso"]);
            this.textAutorizo.Text = Convert.ToString(resultado[0]["autorizo"]);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string actualiza = "update financieros.firmas set entrego='{0}', recibio='{1}', vobo='{2}',reviso='{3}',autorizo='{4}'";
            string paso = string.Format(actualiza, textEntrego.Text, textRecibio.Text, textVobo.Text, textReviso.Text, textAutorizo.Text);
            globales.consulta(paso);
            globales.MessageBoxSuccess("Registros actualizados","Aviso",globales.menuPrincipal);
            this.Owner.Close();
        }

        private void textEntrego_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textRecibio.Select();
        }

        private void textRecibio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textVobo.Select();
        }

        private void textVobo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textReviso.Select();
        }

        private void textReviso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textAutorizo.Select();
        }

        private void textAutorizo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string actualiza = "update financieros.firmas set entrego='{0}', recibio='{1}', vobo='{2}',reviso='{3}',autorizo='{4}'";
                string paso = string.Format(actualiza, textEntrego.Text, textRecibio.Text, textVobo.Text, textReviso.Text, textAutorizo.Text);
                globales.consulta(paso);
                DialogResult dialo1 = globales.MessageBoxSuccess("FIRMAS ACTUALIZADAS CORRECTAMENTE", "", globales.menuPrincipal);
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
