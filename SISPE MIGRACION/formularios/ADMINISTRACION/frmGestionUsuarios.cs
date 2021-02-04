using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.ADMINISTRACION
{
    public partial class frmGestionUsuarios : Form
    {
        public frmGestionUsuarios()
        {
            InitializeComponent();
        }

        private void frmGestionUsuarios_Load(object sender, EventArgs e)
        {
            try
            {
                dgDatos.Rows.Clear();
                string query = "select * from catalogos.usuarios order by idusuario asc";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                resultado.ForEach(o => dgDatos.Rows.Add(o["idusuario"], o["usuario"], o["nombre"], o["puesto"], o["activo"], "Modificar", "Eliminar",o["tipomenu"]));
            }
            catch {

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void txtFolio_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                dgDatos.Rows.Clear();
                string query = $"select * from catalogos.usuarios where usuario like '{txtFolio.Text}%' order by idusuario asc";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                resultado.ForEach(o => dgDatos.Rows.Add(o["idusuario"], o["usuario"], o["nombre"], o["puesto"], o["activo"], "Modificar", "Eliminar"));
            }
            catch
            {

            }
        }

        private void dgDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5) {
                    modificar(e.RowIndex, e.ColumnIndex);
                } else if (e.ColumnIndex == 6) {
                    DialogResult p = globales.MessageBoxQuestion("¿Deseas eliminar el registro?","Aviso",globales.menuPrincipal);
                    if (p == DialogResult.Yes)
                    {
                        string id = Convert.ToString(dgDatos.Rows[e.RowIndex].Cells[0].Value);
                        string query = $"delete from catalogos.usuarios where idusuario = {id};delete from catalogos.detalle_usuario_menu where id_usuario = {id}";
                        if (globales.consulta(query,true)) {
                            globales.MessageBoxSuccess("Registro eliminado correctamente","Aviso",globales.menuPrincipal);
                            frmGestionUsuarios_Load(null,null);
                        }
                    }
                }
            }
            catch {

            }
        }

        private void modificar(int row,int col) {

            string id = Convert.ToString(dgDatos.Rows[row].Cells[0].Value);
            string tipomenu = Convert.ToString(dgDatos.Rows[row].Cells[7].Value);
            List<Dictionary<string, object>> listaFinal =(List<Dictionary<string, object>>) globales.getMenuUsuario(id,tipomenu);
            frmNuevoUsuario p = new frmNuevoUsuario(listaFinal, true, id);
            p.lbl1.Text = "Modificar usuario";
            globales.showModal(p);
            frmGestionUsuarios_Load(null,null);
        }

        private void dgDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgDatos_CellBorderStyleChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmNuevoUsuario p = new frmNuevoUsuario();
            p.lbl1.Text = "Nuevo usuario";
            globales.showModal(p);
            frmGestionUsuarios_Load(null, null);
        }

        private void dgDatos_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) e.SuppressKeyPress = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmGestionUsuarios_Shown(object sender, EventArgs e)
        {
            txtFolio.Focus();
        }
    }
}
