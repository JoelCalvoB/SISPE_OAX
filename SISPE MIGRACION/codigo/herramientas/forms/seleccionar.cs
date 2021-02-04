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
    public partial class seleccionar : Form
    {

        internal bool seleccionado = false;
        internal bool esDbf = false;
        public seleccionar()
        {
            InitializeComponent();
        }

        private void seleccionar_Load(object sender, EventArgs e)
        {
            ActiveControl = this.lista;
            this.lista.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.seleccionado = true;
            this.esDbf = lista.SelectedIndex == 1;
            this.Close(); 
        }

        private void lista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                button1_Click(null,null);
            }
        }
    }
}
