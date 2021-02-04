using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS.ESTUDIO_SOCIOECONO;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS.ESTUDIO_TÉCNICO;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.PRINCIPAL_PH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH
{
    public partial class frmDocumentos : Form
    {
        internal menuPrincipal ventana;
        private int index;

        public frmDocumentos(int index = 0)
        {
            InitializeComponent();
            this.ventana = (menuPrincipal)globales.menuPrincipal;
            this.index = index;
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
                    ventana.abrirFormulario<frmdocusolic>();
                    this.Owner.Close();

                    break;
                case 1:
                    ventana.abrirFormulario<frmavaluo>();
                    this.Owner.Close();
                    break;
                case 2:
                    ventana.abrirFormulario<frmSocioEconomico>();
                    this.Owner.Close();
                    break;
                case 3:
                    ventana.abrirFormulario<frmEstudioTecnico_>();
                    this.Owner.Close();
                    break;
                case 4:
                    ventana.abrirFormulario<frmNotificacion>();
                    this.Owner.Close();
                    break;
                case 5:
                    ventana.abrirFormulario<frmconveniomodificatorio>();
                    this.Owner.Close();
                    break;
                case 6:
                    ventana.abrirFormulario<frmNegativo>();
                    this.Owner.Close();
                    break;
                case 7:
                    ventana.abrirFormulario<frmEmisiones>();
                    this.Owner.Close();
                    break;
                case 8:
                    ventana.abrirFormulario<frmPagare>();
                    this.Owner.Close();
                    break;
                case 9:
                    ventana.abrirFormulario<frmComplementoConsejo>();
                    this.Owner.Close();
                    break;
                case 10:
                    globales.showModal(new frmOficioPContinuar());
                    this.Owner.Close();
                    break;
                default:
                    break;

            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            opciones();
        }

        private void frmDocumentos_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = this.index;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmDocumentos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.Owner.Close();
            }
        }
    }
}

