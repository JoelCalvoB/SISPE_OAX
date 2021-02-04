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
    public partial class frmfechareg : Form
    {
        frmaclaraciones forma;
        public frmfechareg(frmaclaraciones forma)
        {
            InitializeComponent();
            this.forma = forma;
        }

        private void frmfechareg_Load(object sender, EventArgs e)
        {
            fecha1.Text = string.Format("{0:d}",DateTime.Now);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fec1 = DateTime.Parse(fecha1.Text);
            this.forma.set_Fecha(string.Format("{0:yyyy-MM-dd}",fec1));
            
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void fecha1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                DateTime fec1 = DateTime.Parse(fecha1.Text); ;
                this.forma.set_Fecha(string.Format("{0:yyyy-MM-dd}", fec1));

                this.Close();
            }
        }
    }
}
