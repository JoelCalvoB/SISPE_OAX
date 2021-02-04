using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.CONSTANCIAS.REGISTRO
{
    public partial class frmConstanciaGeneral : Form
    {
        public frmConstanciaGeneral()
        {
            InitializeComponent();
        }

        private void frmConstanciaGeneral_Shown(object sender, EventArgs e)
        {
            //Se manda a llamar la tabla
            string query = "SELECT * FROM catalogos.constag order by folio desc;";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            dtggrid.Rows.Clear();
            foreach (var item in resultado)
            {
                //se inserta los campos de la tabla en el datagrew
                string folio = Convert.ToString(item["folio"]);
                string tipo = Convert.ToString(item["tipo"]);
                string fecha = string.Format("{0:d}",item["fecha"]);
                string nombre = Convert.ToString(item["nombre"]);
                string rfc = Convert.ToString(item["rfc"]);
                

                dtggrid.Rows.Add(folio, tipo, fecha, nombre, rfc);
            }
        }

        private void dtggrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtggrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = dtggrid.Rows[c];
            string folio = Convert.ToString(row.Cells[0].Value);
            

            frmmodificar2 modificar = new frmmodificar2(folio);
            globales.showModalReturning(modificar);


            frmConstanciaGeneral_Shown(null,null);
        }

        private void frmConstanciaGeneral_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmmodificar2 modificar = new frmmodificar2();
            globales.showModalReturning(modificar);
            frmConstanciaGeneral_Shown(null,null);
        }
    }
}
