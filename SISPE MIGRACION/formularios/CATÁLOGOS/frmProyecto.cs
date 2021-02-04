using SISPE_MIGRACION.codigo.baseDatos;
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
    public partial class frmProyecto : Form

    {
        private DataGridViewRow elemento1;
        private string cprocve = string.Empty;
        string proyecto;
        string descripcion;
        bool vacio;
        public frmProyecto()
        {
            string proyectoglobal;
            string descripgloba;
            this.proyecto = proyecto;
            this.descripcion = descripcion;
            this.vacio = vacio;
            InitializeComponent();
            proyectoglobal = proyecto;
            descripgloba = descripcion;
            label2.Visible = false;
            label4.Visible = false;
            txtdes.Visible = false;
            txtproy.Visible = false;
        }

        private void frmProyecto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                button2_Click(null, null);
            }
           if (e.KeyCode == Keys.F5)
            {
                txtproy.Focus();
                label2.Visible = true;
                label4.Visible = true;
                txtdes.Visible = true;
                txtproy.Visible = true;



            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmProyecto_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void frmProyecto_Load(object sender, EventArgs e)

        {
            CARGA();
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {




        }

        private void btnmodifica_Click(object sender, EventArgs e)
        {





        }

        private void CARGA()
        {
            string query = "select cprocve,cprodes from catalogos.proyecto order by cprocve" ;
            var elemento = baseDatos.consulta(query);
            datos02.Rows.Clear();
            foreach (var item in elemento)
            {
                string cprocve = Convert.ToString(item["cprocve"]);
                string cprodes = Convert.ToString(item["cprodes"]);
                datos02.Rows.Add(cprocve, cprodes);


            }
        }

        private void datos02_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = datos02.Rows[c];
            proyecto = Convert.ToString(row.Cells[0].Value);
            descripcion = Convert.ToString(row.Cells[1].Value);
            try
            {

                string consulta = "SELECT * FROM catalogos.proyecto where cprocve='{0}' ";
                string pasaconsulta = string.Format(consulta, proyecto);
                List<Dictionary<string, object>> resultado = globales.consulta(pasaconsulta);
                if (resultado.Count <= 0)
                {
                    return;
                }
                else
                {

                    string query = "update catalogos.proyecto set cprodes='{1}' where cprocve='{0}'";
                    string pasa = string.Format(query, proyecto, descripcion);
                    globales.consulta(pasa);
                    DialogResult dialogo1 = globales.MessageBoxSuccess("INFORMACIÓN ACTUALIZADA CORRECTAMENTE", "HECHO", globales.menuPrincipal);
                    CARGA();
                }
            }
            catch
            {
                DialogResult dialogo = globales.MessageBoxExclamation("VERIFICAR LOS DATOS INGRESADOS, NO SE PUEDE DUPLICAR PROYECTOS", "ATENCIÓN", this);
            }

        }

        private void datos02_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datos02_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnok_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas cerrar el modulo?", globales.menuPrincipal);
            if (p == DialogResult.Yes)
                this.Close();
        }

        private void datos02_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Delete)
            {
                string query = "delete from catalogos.proyecto where cprocve='{0}'";
                string paso = string.Format(query, proyecto);
                 globales.consulta(paso);
                CARGA();
                DialogResult dialogo4 = globales.MessageBoxSuccess("SE ELIMINO DE FORMA CORRECTA EL PROYECTO", "", globales.menuPrincipal);

            }
        }

        private void datos02_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = datos02.Rows[c];
            proyecto = Convert.ToString(row.Cells[0].Value);
            descripcion = Convert.ToString(row.Cells[1].Value);

            if (string.IsNullOrWhiteSpace(proyecto) || string.IsNullOrWhiteSpace(descripcion))
            {
                vacio = true;
            }
        }

        private void datos02_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtproy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtdes.Select();
                txtdes.Focus();

            }
        }

        private void txtdes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!(string.IsNullOrWhiteSpace(txtdes.Text)) && !(string.IsNullOrWhiteSpace(txtdes.Text)))  // nuevo
                {
                    string query = "select * from catalogos.proyecto where cprocve='{0}'";
                    string paso = string.Format(query, proyecto);
                    List<Dictionary<string, object>> list = globales.consulta(paso);
                    if (list.Count >= 0)

                    {
                        string nuevo = "INSERT INTO catalogos.proyecto (cprocve,cprodes) VALUES ('{0}','{1}')";
                        string pasanuevo = string.Format(nuevo, txtproy.Text, txtdes.Text);
                        globales.consulta(pasanuevo);
                        DialogResult dialogonuevo = globales.MessageBoxSuccess("NUEVO PROYECTO INSERTADO", "", globales.menuPrincipal);
                        CARGA();
                        label2.Visible = false;
                        label4.Visible = false;
                        txtdes.Visible = false;
                        txtproy.Visible = false;
                        txtdes.Clear();
                        txtproy.Clear();

                    }
                    else
                    {
                        DialogResult dialogo3 = globales.MessageBoxError("ESTA DUPLICANDO PROYECTO, VERIFIQUE SU INFORMACIÓN", "ERROR", globales.menuPrincipal);
                        label2.Visible = false;
                        label4.Visible = false;
                        txtdes.Visible = false;
                        txtproy.Visible = false;
                        txtdes.Clear();
                        txtproy.Clear();
                        return;
                    }
                }
            }
        }
    }
}

