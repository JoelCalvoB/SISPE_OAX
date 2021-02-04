using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SISPE_MIGRACION.codigo.repositorios.datos;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS.ESTUDIO_SOCIOECONO
{
    public partial class frmSocioEconomicoprincipal : UserControl
    {
        internal metodoCambioVentanas cambio;
        public frmSocioEconomicoprincipal()
        {
            InitializeComponent();
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            this.cambio(true);
        }

        private void principal_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
