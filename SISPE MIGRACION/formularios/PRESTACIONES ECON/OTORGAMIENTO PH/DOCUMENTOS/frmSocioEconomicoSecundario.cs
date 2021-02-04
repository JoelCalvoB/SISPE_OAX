using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS.ESTUDIO_SOCIOECONO
{
    public partial class frmSocioEconomicoSecundario : UserControl
    {
        internal metodoCambioVentanas cambiar;
        public frmSocioEconomicoSecundario()
        {
            InitializeComponent();
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            this.cambiar(false);
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
