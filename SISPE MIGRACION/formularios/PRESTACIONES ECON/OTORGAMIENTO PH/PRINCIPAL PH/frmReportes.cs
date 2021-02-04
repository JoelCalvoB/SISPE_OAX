using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS.ESTUDIO_SOCIOECONO;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.REPORTES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.PRINCIPAL_PH
{
    public partial class frmReportes : Form
    {
        internal menuPrincipal ventana;
        string opcionselecciona;
        public frmReportes(menuPrincipal ventana)
        {
            InitializeComponent();
            this.ventana = ventana;      

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) opciones();
        }

        private void opciones()
        {
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    globales.showModal(new frmBuscador(listBox1.SelectedIndex));
                    this.Owner.Close();

                    break;
                case 1:
                    globales.showModal(new frmBuscador(listBox1.SelectedIndex));
                    this.Owner.Close();
                    break;
                case 2:
                    globales.showModal(new frmBuscador(listBox1.SelectedIndex));
                    this.Owner.Close();
                    break;
                case 3:
                    globales.showModal(new frmBuscador(listBox1.SelectedIndex));
                    this.Owner.Close();
                    break;
                case 4:
                    globales.showModal(new frmBuscador(listBox1.SelectedIndex));

                    this.Owner.Close();
                    break;
                case 5:
                    globales.showModal(new frmConcentrado());
                    this.Owner.Close();
                    break;
                case 6:
                    globales.showModal(new frmBuscador(listBox1.SelectedIndex));
                    this.Owner.Close();
                    break;
                case 7:
                    globales.showModal(new frmNotiticacionReporte());
                    this.Owner.Close();
                    break;
                case 8:
                    globales.showModal(new frmConvenioMod());
                    this.Owner.Close();
                    break;
                case 9:
                    globales.showModal(new frmBuscador(listBox1.SelectedIndex));
                    this.Owner.Close();
                    break;
                case 10:
                    globales.showModal(new frmBuscador(listBox1.SelectedIndex));
                    this.Owner.Close();
                    break;
                case 11:
                    globales.showModal(new frmBuscador(listBox1.SelectedIndex));
                    this.Owner.Close();
                    break;
                case 12:
                    globales.showModal(new frmAlfaHipo());
                    this.Owner.Close();
                    break;
                case 13:
                    globales.showModal(new frmBuscador(listBox1.SelectedIndex));
                    this.Owner.Close();
                    break;
                default:
                    break;

            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            opciones();
            opcionselecciona = listBox1.Text;

        }

        private void frmDocumentos_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
        
        }
    }
}

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.PRINCIPAL_PH
{
   
}