using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.CATALOGOS
{
    public partial class frmFirmasNomina : Form
    {
        public frmFirmasNomina()
        {
            InitializeComponent();
        }

        private void bloquea()
        {
            txtAutorizo.Enabled = false;
            txtElaboro.Enabled = false;
            txtRecursos.Enabled = false;
            txtReviso.Enabled = false;

        }

        private void frmFirmas_Shown(object sender, EventArgs e)
        {
            inicia();
        }

        private void inicia()
        {
            bloquea();
            string query = "SELECT * FROM nominas_catalogos.firmas;";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0) return;


            Dictionary<string, object> diccionario = resultado[0];

                txtAutorizo.Text = Convert.ToString(diccionario["autorizo"]);
            txtElaboro.Text = Convert.ToString(diccionario["elaboro"]);
            txtRecursos.Text = Convert.ToString(diccionario["rec_hum"]);
            txtReviso.Text = Convert.ToString(diccionario["reviso"]);
            button1.Text = "MODIFICAR";

        }

        private void frmFirmas_Load(object sender, EventArgs e)
        {
            string query = "select * from catalogos.categorias";

            List<Dictionary<string, object>> lista = globales.consulta(query); ;

            foreach (Dictionary<string, object> variable in lista) {
                string clave = Convert.ToString(variable["ccatcve"]);
                string descripcion = Convert.ToString(variable["ccatdes"]);
                int elentero = Convert.ToInt32(variable["ccatsue"]);

                string cadena = $"|  {clave}    |   {descripcion}   |   {elentero}";

                System.Diagnostics.Debug.WriteLine(cadena);
            }

             
        }

        private void habilita()
        {
            txtAutorizo.Enabled = true;
            txtElaboro.Enabled = true;
            txtRecursos.Enabled = true;
            txtReviso.Enabled = true;
        }

        private void txtElaboro_KeyDown(object sender, KeyEventArgs e)
       {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "MODIFICAR")
            {
                DialogResult p = globales.MessageBoxQuestion("¿Deseas modificar un registro?", "Aviso", globales.menuPrincipal);
                if (p == DialogResult.No) return;
                // habilita 
                habilita();
                button1.Text = "GUARDAR";
                return;
            }
            if (button1.Text == "GUARDAR")
            {
                if (string.IsNullOrWhiteSpace(txtAutorizo.Text)&& (string.IsNullOrWhiteSpace(txtElaboro.Text)&&string.IsNullOrWhiteSpace(txtRecursos.Text)&string.IsNullOrWhiteSpace(txtReviso.Text)))
                {
                    DialogResult dialogo = globales.MessageBoxExclamation("UN REGISTRO SE ENCUENTRA VACIO", "VERIFICAR", globales.menuPrincipal);
                    return;
                }
                try {
                    string query = $"update nominas_catalogos.firmas set elaboro='{txtElaboro.Text}' , reviso='{txtReviso.Text}' , autorizo='{txtAutorizo.Text}' , rec_hum='{txtRecursos.Text}' ";
                    globales.consulta(query);
                    DialogResult dialogo3 = globales.MessageBoxSuccess("SE ACTUALIZARON LOS REGISTROS", "PROCESO TERMINADO", globales.menuPrincipal);
                    limpia();
                }
                catch
                {
                    DialogResult dialogo2 = globales.MessageBoxError("ERROR EN CAPTURA, CONTACTAR A SISTEMAS", "ERROR", globales.menuPrincipal);
                }
            }


        }
        
        private void limpia()
        {
            inicia();
        }
    }
}
