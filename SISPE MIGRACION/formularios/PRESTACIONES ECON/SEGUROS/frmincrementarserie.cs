using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Seguros
{
    public partial class frmincrementarserie : Form
    {
        public frmincrementarserie()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "update datos.s_quirog set num_desc= (num_desc +1) where num_desc < plazo";
            globales.consulta(query);
            globales.MessageBoxInformation("INCREMENTANDO SERIE DE SEGUROS.","Aviso",globales.menuPrincipal);
            globales.MessageBoxInformation("ELIMINANDO PRESTAMOS TERMINADOS DE PAGAR","aviso",globales.menuPrincipal);
            string query1 = "delete from datos.s_quirog where num_desc>=plazo";
            globales.consulta(query1);
            string query3 = "SELECt COUNT (folio) as cantidad from datos.s_quirog where num_desc>=plazo";
            List<Dictionary<string, object>> resultado = globales.consulta(query3);
            string cuenta = Convert.ToString(resultado[0]["cantidad"]);
            globales.MessageBoxInformation("SE TERMINARON DE PAGAR " + cuenta +"PRESTAMOS DE QUIROGRAFARIOS","Aviso",globales.menuPrincipal);
            // QUIROGRAFARIOS

            string query4 = "update datos.s_hipote set num_desc= (num_desc +1) where num_desc < plazo";
            globales.consulta(query4);
            string query5 = "delete from datos.s_hipote where num_desc>=plazo";
            globales.consulta(query5);
            string query6 = "SELECt COUNT (folio) as cantidad from datos.s_hipote where num_desc>=plazo";
            List<Dictionary<string, object>> resultado1 = globales.consulta(query6);
            string cuenta1 = Convert.ToString(resultado1[0]["cantidad"]);
            globales.MessageBoxInformation("SE TERMINARON DE PAGAR " + cuenta1 + "PRESTAMOS DE HIPOTECARIOS","Aviso",globales.menuPrincipal);

        }

        private void frmincrementarserie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.Close();
            }
        }

        private void frmincrementarserie_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void frmincrementarserie_Load(object sender, EventArgs e)
        {
            btnok.Focus();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
