using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    public partial class frmdependencias : Form
    {
        private bool band;
        private bool aceptar { get; set; }
        internal enviarDatos enviar;
        private List<Dictionary<string, object>> resultado;
        private string proyecto = string.Empty;
        public frmdependencias( )
        {
            InitializeComponent();
        }

        private void frmdependencias_Load(object sender, EventArgs e)
        {

            string query = "select * from catalogos.cuentas";
            resultado = baseDatos.consulta(query);

            foreach (Dictionary<string,object> item in resultado)
            {
                string descripcion = Convert.ToString(item["descripcion"]);
                string cuenta = Convert.ToString(item["cuenta"]);
                string proy = Convert.ToString(item["proy"]);
                datos.Rows.Add(cuenta, descripcion,proy);
            }

        }

        private void datos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnseleccionar_Click(object sender, EventArgs e)

        {
            if (this.resultado.Count == 0) return;
           
            this.aceptar = true;
            string query = string.Empty;
        
         
                query = $"select * from catalogos.cuentas where cuenta = '{this.proyecto}' or proy='{this.proyecto}'";

            

            List<Dictionary<string, object>> resultado = globales.consulta(query);
            Dictionary<string, object> valor = new Dictionary<string, object>();
            if (resultado.Count != 0) {
                valor = resultado[0];
            }
            limpiar();
            
            enviar(valor,true);
            this.Close();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            limpiar();
            
        }

        private void limpiar()
        {
            txtBusqueda.Text = "";
        }

        private void txtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            string query = string.Format("select * from catalogos.cuentas where descripcion like '{0}%' or cuenta like '{0}%' or proy like'{0}%' LIMIT 100", txtBusqueda.Text);
            resultado = baseDatos.consulta(query);
            datos.Rows.Clear();
            foreach (Dictionary<string,object> item in resultado)
            {
                string descripcion = Convert.ToString(item["descripcion"]);
                string cuenta = Convert.ToString(item["cuenta"]);
                string proy = Convert.ToString(item["proy"]);
                datos.Rows.Add(cuenta, descripcion,proy);
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

        private void datos_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            proyecto = Convert.ToString(datos.Rows[e.Cell.RowIndex].Cells[0].Value);
        }

        private void frmdependencias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {//
                
            }
        }

        private void frmdependencias_FormClosing(object sender, FormClosingEventArgs e)
        {
         
        }

        private void datos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                btnseleccionar_Click(null, null);
            }
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Down)
            {
                datos.Select();
            }

        }
    }
}
