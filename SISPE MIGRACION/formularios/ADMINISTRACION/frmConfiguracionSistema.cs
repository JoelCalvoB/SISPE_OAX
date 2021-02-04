using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.ADMINISTRACION
{
    public partial class frmConfiguracionSistema : Form
    {
        public frmConfiguracionSistema()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {

                txtLogoGobierno.Text = openFileDialog1.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                txtLogoIniciarSesion.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas cargar nuevo logo del gobierno para afectar reportes?","Aviso",globales.menuPrincipal);
            if (dialogo == DialogResult.Yes) {
                string base64 = globales.ObtenerImagen_base64(txtLogoGobierno.Text);
                string query = $"update catalogos.imagenes set imagen = '{base64}' where nombre = 'logogobierno'";
                if (globales.consulta(query,true)) {
                    globales.MessageBoxSuccess("Imagen cargada con exito al sistema","Aviso",globales.menuPrincipal);
                    query = "update catalogos.\"version\" set imgversion = imgversion::numeric + 1 ";
                    globales.consulta(query,true);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas cargar nuevo logo del gobierno para afectar reportes?", "Aviso", globales.menuPrincipal);
            if (dialogo == DialogResult.Yes)
            {
                string base64 = globales.ObtenerImagen_base64(txtLogoIniciarSesion.Text);
                string query = $"update catalogos.imagenes set imagen = '{base64}' where nombre = 'logoiniciarsesion'";
                if (globales.consulta(query, true))
                {
                    globales.MessageBoxSuccess("Imagen cargada con exito al sistema", "Aviso", globales.menuPrincipal);
                    query = "update catalogos.\"version\" set imgversion = imgversion::numeric + 1 ";
                    globales.consulta(query, true);
                }
            }
        }
    }
}
