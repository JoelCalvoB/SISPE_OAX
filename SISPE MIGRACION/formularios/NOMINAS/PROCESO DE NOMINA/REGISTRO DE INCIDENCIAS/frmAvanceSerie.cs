using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.REGISTRO_DE_INCIDENCIAS
{
    public partial class frmAvanceSerie : Form
    {
        public frmAvanceSerie()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult aumenta = globales.MessageBoxQuestion("¿Deseas avanzar los pagos de los prestamos?", "Aviso", globales.menuPrincipal);
            if (aumenta == DialogResult.Yes)
            {
                string avanza = "update  nominas_catalogos.nominew set pagon=pagon+1 where (pagon<= pagot)and pagon<>0 and pagot<>0 and tipo_nomina='N';  delete FROM nominas_catalogos.nominew where pagon > pagot and tipo_nomina='N';";

                avanza += " delete from nominas_catalogos.nominew where tipopago = 'R'; ";
                if (globales.consulta(avanza, true)) {
                    globales.MessageBoxSuccess("Series de prestamos aplicados correctamente","Avances de prestamo",globales.menuPrincipal);
                }
                //nuevo
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
