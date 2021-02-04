using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.FINANCIEROS.PASACHEQ
{
    public partial class frmEscogerImportacion : Form
    {
        private bool esFondo = false;
        private bool btnCerrar = true;
        public bool boolCerrar {
            get {
                return btnCerrar;
            }
        }

        public bool boolFondo {
            get {
                return esFondo;
            }
        }
        public frmEscogerImportacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnCerrar = false;
            int index = lstSeleccionar.SelectedIndex;
            esFondo = (index == 0) ? false : true;

            this.Close();
        }

        private void lstSeleccionar_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void frmEscogerImportacion_Load(object sender, EventArgs e)
        {
            lstSeleccionar.Focus();
            lstSeleccionar.SelectedIndex = 0;
        }

        private void lstSeleccionar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                button1_Click(null, null);
            }
        }
    }
}
