using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.NOMINAS_ESPECIALES
{
    public partial class frmActivacionTempo : Form
    {

        string jpp = string.Empty;
        string num = string.Empty;
        public frmActivacionTempo()
        {
            InitializeComponent();
        }


     
        private void button1_Click(object sender, EventArgs e)
        {

            this.jpp = comboBox1.Text;
            this.num = textBox1.Text;

            string query = string.Empty;
            if (String.IsNullOrWhiteSpace(comboBox1.Text) && String.IsNullOrWhiteSpace(textBox1.Text))
            {
                return;
            }
            else
            {

                query = $"SELECT superviven as supervive,dire_super as dire FROM nominas_catalogos.maestro  where jpp={this.jpp} and num='{this.num}';";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (Convert.ToString(resultado[0]["supervive"]) == "F")
                {
                    inicia();
                    DialogResult dialogo = globales.MessageBoxInformation("SE ACTIVO EL EMPLEADO, IMPRIMA EL SOBRE Y DESACTIVELO DE FAVOR", "AVISO", globales.menuPrincipal);
                }
                else
                {
                    salida();
                    DialogResult dialogo = globales.MessageBoxInformation("SE DESACTIVO EL EMPLEADO", "AVISO", globales.menuPrincipal);

                }


            }


    }

        public void inicia()
        {
            string quey = $"update  nominas_catalogos.maestro set superviven='S' , dire_super='S' where jpp={this.jpp} and num='{this.num}';";
            globales.consulta(quey);
        }
        public void salida()
        {
            string quey = $"update  nominas_catalogos.maestro set superviven='F' , dire_super='X' where jpp={this.jpp} and num='{this.num}';";
            globales.consulta(quey);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
