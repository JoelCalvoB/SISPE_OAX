using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.OTROS.SERVIDOR102
{
    public partial class frm : Form
    {
        internal setDiccionario enviar;
        private List<Dictionary<string,object>> resultado;
        private Dictionary<string, object> valor;
        private string rfc = string.Empty;
        public frm()
        {
            InitializeComponent();
        }

        private void frmCatalogosAportaciones_Load(object sender, EventArgs e)
        {
            string query = "select * from israel.empleados ORDER BY RFC ASC limit 100";
            resultado = globales.consulta(query);
            resultado.ForEach(o => datos.Rows.Add(o["rfc"],o["nombre_em"]));
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            string busqueda = txtBusqueda.Text;
            string query = $"select * from israel.empleados where rfc like '%{busqueda}%' or nombre_em like '%{busqueda}%' ORDER BY RFC ASC limit 100";
            resultado = globales.consulta(query);
            datos.Rows.Clear();
            resultado.ForEach(o => datos.Rows.Add(o["rfc"],o["nombre_em"]));
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
        }

        private void limpiar()
        {
            txtBusqueda.Text = "";
        }

        private void datos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void datos_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            rfc = Convert.ToString(datos.Rows[e.Cell.RowIndex].Cells[0].Value);
        }
    }
}
