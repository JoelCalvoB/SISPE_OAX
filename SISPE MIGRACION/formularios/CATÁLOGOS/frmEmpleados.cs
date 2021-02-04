using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ;
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

namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    public partial class frmEmpleados : Form
    {
        internal enviarDatos enviar;
        private List<Dictionary<string, object>> resultado;
        private Dictionary<string, object> valor;
        private string rfc = string.Empty;

        public frmEmpleados()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            if (resultado.Count == 0) return; 
            foreach (Dictionary<string, object> item in resultado)
            {
                if (item["rfc"].Equals(rfc))
                {
                    valor = item;
                    break;
                }
            }
            limpiar();
            Hide();
            enviar(valor);
         //   string nombre_actual = Convert.ToString (valor["nombre_em"]);
        ///    Properties.Resources.nombre= Convert.ToString(nombre_actual) ;
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from datos.empleados where pendiente = 'f' order by rfc asc limit 25";
                resultado = baseDatos.consulta(query);
                resultado.ForEach(o => datos.Rows.Add(o["rfc"],o["nombre_em"]));
                
            }
            catch {
                MessageBox.Show("Error en la consulta, favor de contactar a sistemas para dar solución","Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            limpiar();
            Close();
        }

        private void limpiar()
        {
            txtBusqueda.Text = "";
        }

        private void txtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                datos.Focus();
                return;
            }

            
           
       
        }

        private void datos_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            rfc = Convert.ToString(datos.Rows[e.Cell.RowIndex].Cells[0].Value);            
        }

        private void datos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)

            {

                string VALOR = Clipboard.GetText();
                string PASA = Convert.ToString(VALOR);
                txtBusqueda.Text = PASA;
                datos.Select();
            }
            if (e.Control && e.KeyCode == Keys.C)

            {
                Clipboard.SetText(txtBusqueda.Text);

            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
            if (e.KeyChar == 13)
                btnseleccionar_Click(null,null);
        }

        private void datos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar))
            {
                txtBusqueda.Focus();
            }
            
        }

        private void datos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           
        }

        private void datos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnseleccionar_Click(null, null);
            }
            if (e.KeyCode==Keys.Back)
            {
                txtBusqueda.Focus();
            }
        }

        private void datos_DoubleClick(object sender, EventArgs e)
        {
            btnseleccionar_Click(null, null);
        }

        private void frmEmpleados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) {
                this.Close();
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text;
            string query = string.Format("select * from datos.empleados where (rfc like '{0}%' OR nombre_em LIKE  '%{0}%') and pendiente = 'f' order by rfc asc limit 30;", busqueda);
            resultado = baseDatos.consulta(query);
            datos.Rows.Clear();
            resultado.ForEach(o => datos.Rows.Add(o["rfc"], o["nombre_em"]));
        }
    }
}
