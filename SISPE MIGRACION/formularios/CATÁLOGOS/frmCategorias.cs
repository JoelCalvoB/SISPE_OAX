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
    public partial class frmCategorias : Form
    {
        private bool aceptar { get; set; }
        internal enviarDatos enviar;
        private List<Dictionary<string, object>> resultado;
        string ccatcve = "";
        public frmCategorias()
        {
            InitializeComponent();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            string query = "select* from catalogos.categorias";
            var elemento = baseDatos.consulta(query);

           
                foreach (var item in elemento)
                {
                try
                {
                    string ccatcve = item["ccatcve"];
                    string ccatdes = item["ccatdes"];
                    string aux1 = Convert.ToString(item["ccatsue"]);
                    double ccatsue = string.IsNullOrWhiteSpace(aux1) ? 0 : Convert.ToDouble(item["ccatsue"]);
                    datos02.Rows.Add(ccatcve, ccatdes, ccatsue);
                }
                catch
                {

                }
                }
            
            }

        

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCategorias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode ==Keys.F2)
            {
                button2_Click(null, null);
            }
        }

        private void frmCategorias_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void ntnmodifica_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas cerrar el modulo?", globales.menuPrincipal);
            if (p == DialogResult.Yes)
                this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void datos02_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnseleccionar_Click(null, null);
            }
        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {

            this.aceptar = true;
            string query = $"select * from catalogos.categorias where ccatcve = '{this.ccatcve}'";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            Dictionary<string, object> valor = new Dictionary<string, object>();
            if (resultado.Count != 0)
            {
                valor = resultado[0];
            }
            limpiar();
            //if (valor.Count <= 0)
            //{
            //    return;
            //    this.Close();
            //}
            enviar(valor, true);
            this.Close();
        }
        private void limpiar()
        {
            txtBusqueda.Text = "";
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                datos02.Select();
            }
        }

        private void txtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            string query = string.Format("select * from catalogos.categorias where ccatcve like '{0}%' or ccatdes like '{0}%' LIMIT 50", txtBusqueda.Text);
            resultado = baseDatos.consulta(query);
            datos02.Rows.Clear();
            foreach (Dictionary<string, object> item in resultado)
            {
                string ccatcve = Convert.ToString(item["ccatcve"]);
                string ccatdes = Convert.ToString(item["ccatdes"]);
                string ccatsue = Convert.ToString(item["ccatsue"]);
                datos02.Rows.Add(ccatcve, ccatdes, ccatsue);
            }
        }

        private void datos02_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            ccatcve = Convert.ToString(datos02.Rows[e.Cell.RowIndex].Cells[0].Value);

        }

        private void datos02_DoubleClick(object sender, EventArgs e)
        {
            btnseleccionar_Click(null, null);
        }
    }

}
