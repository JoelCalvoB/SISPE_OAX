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
    public partial class frmCatalogoGeneral : Form
    {
        private int row;
        internal setDiccionario metodo;
        internal string tabla = "";
        public frmCatalogoGeneral()
        {
            InitializeComponent();
        }

        private void frmFinalidad_Load(object sender, EventArgs e)
        {
            string query = $"select * from catalogos.{tabla} limit 100";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            resultado.ForEach(o => {
                datos.Rows.Add(o["clave"],o["descripcion"]);
            });
        }

        private void datos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnseleccionar_Click(null, null);
                
            }
        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>();
            diccionario.Add("clave",datos.Rows[row].Cells[0].Value);
            diccionario.Add("descripcion", datos.Rows[row].Cells[1].Value);
            metodo(diccionario);
            Close();
        }
         
        private void datos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }
    }
}
