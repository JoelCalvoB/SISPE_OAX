using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.FINANCIEROS.CHEQUES
{
    public partial class ventanaPrincipal : Form
    {
        private bool menuDeslizado = false;

        public ventanaPrincipal()
        {
            InitializeComponent();
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
        
        }

        private void ventanaPrincipal_Load(object sender, EventArgs e)
        {
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ventanaPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
         
        }

        private void btnRuta_Click(object sender, EventArgs e)
        {
            ofdArchivo.Filter = "DBF files (*.DBF)|*.dbf|All files (*.*)|*.*";
            DialogResult dialogo = ofdArchivo.ShowDialog();
            try
            {
                if (dialogo == DialogResult.OK)
                {
                    string nombreArchivo = ofdArchivo.FileName;
                    
                }
            }
            catch
            {

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿SE ENCUENTRA ABIERTO EL MES EN EL CG?", "VERIFICAR", globales.menuPrincipal);
            if (dialogo == DialogResult.No) return;

            this.Cursor = Cursors.WaitCursor;
            procesoDbf proceso = new procesoDbf();
            string cadena = proceso.convertirDBFtoText(radioButton1.Checked);
            this.Cursor = Cursors.Default;
            //***************** GUARDAR ARCHIVO *********************
            DialogResult resultado = sfdGuardar.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                string ruta = sfdGuardar.FileName;
                StreamWriter escribir = new StreamWriter(ruta);

                string user = "sa";
                string password = "cg";
                DateTime fecha = DateTime.Now;
                string año = (Convert.ToString(fecha.Year).Substring(2, 2));
                string database = "fondos" + año;
                string server = "PC\\CG,1089";

                String sConexion = "Asynchronous Processing=true;"
              + " Pooling=false;User ID=" + user + "; "
              + " password= " + password + "; "
              + " Initial Catalog=" + database + "; "
              + " Data Source=" + server;// + ",1433";
           

                string[] arreglo = cadena.Split(';');


             //   SqlConnection conexion = new SqlConnection(sConexion);
              //  conexion.Open();

                foreach (string cade in arreglo) {
                    
               //     SqlCommand insertar = new SqlCommand($"{cade}", conexion);
                 //   insertar.ExecuteNonQuery();
                    
                }
                //conexion.Close();

                foreach (char item in cadena)
                {
                    escribir.Write(item);             

                }
                this.Cursor = Cursors.Default;
                MessageBox.Show("Reslpado e Inserción al CG terminado", "Proceso finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
               // conexion.Close();
                escribir.Close();
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
           
        }

        private void btn2_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
        

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

      
    }
}
