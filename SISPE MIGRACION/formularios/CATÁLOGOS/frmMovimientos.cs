using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.codigo.repositorios;
using SISPE_MIGRACION.codigo.repositorios.catalogos;
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
    public partial class frmMovimientos : Form
    {
        private bool teclaEnter;
        private int row;
        private int column;
        private bool editadoprogramadamente;
        private bool esInsertar;
        private dbaseORM orm;

        public frmMovimientos()
        {
            InitializeComponent();
        }
      

        private void datos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmMovimientos_Load_1(object sender, EventArgs e)
        {
            orm = new dbaseORM();
            string query = "select * from catalogos.movimientos order by tipo_mov;";
            List<movimientos> resultado = orm.queryForList<movimientos>(query);

            foreach (movimientos item in resultado)
            {
                string tipo_mov = item.tipo_mov;
                string tipo = item.tipo;
                string movimiento = item.movimiento;
                int id = item.id;

                datos.Rows.Add(tipo_mov, tipo, movimiento,id);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmMovimientos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                button2_Click(null, null);
            }

            if (e.KeyCode == Keys.Insert) {
                if (datos.Rows.Count == 0) {
                    DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas insertar un nuevo movimiento?","Aviso",globales.menuPrincipal);
                    if (dialogo == DialogResult.Yes) {
                        this.esInsertar = true;

                        object[] obj = new object[] {"","","",""};
                        datos.Rows.Insert(0,obj);
                        datos.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);

                        movimientos m = new movimientos();
                        dbaseORM orm = new dbaseORM();
                        m = orm.insert<movimientos>(m,true);

                        datos.Rows[0].Cells[3].Value = m.id;
                        datos.CurrentCell = datos.Rows[0].Cells[0];

                        this.esInsertar = false;
                    }
                    
                }
            }
        }

        private void frmMovimientos_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void datos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.teclaEnter)
            {
                var x = this.row + 1;
                var y = datos.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                this.teclaEnter = false;
            }
        }

        private void datos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.row = e.RowIndex;
            this.column = e.ColumnIndex;
        }

        private void datos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (editadoprogramadamente)
            {
                editadoprogramadamente = false;
                return;
            }
            int c = e.RowIndex;
            if (c == -1) return;

            if (this.esInsertar) return;

            string tipomov = Convert.ToString(datos.Rows[this.row].Cells[0].Value);
            string tipo = Convert.ToString(datos.Rows[this.row].Cells[1].Value);
            string movimiento = Convert.ToString(datos.Rows[this.row].Cells[2].Value);
            int id = Convert.ToInt32(datos.Rows[this.row].Cells[3].Value);


            movimientos m = new movimientos();
            m.tipo_mov = tipomov;
            m.tipo = tipo;
            m.movimiento = movimiento;
            m.id = id;

            dbaseORM orm = new dbaseORM();
            orm.update<movimientos>(m);

            editadoprogramadamente = false;
        }

        private void datos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion);
        }

        private void viendoEdicion(object sender, PreviewKeyDownEventArgs e)
        {
            this.teclaEnter = e.KeyCode == Keys.Enter;
        }

        private void datos_KeyDown(object sender, KeyEventArgs e)
        {
            if (datos.Rows.Count == 0) return;

            try
            {
                int rowactual = datos.Rows.Count;
                if (e.KeyCode == Keys.Insert)
                {
                    if (datos.Rows.Count != 0)
                    {
                        DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas insertar un nuevo movimiento?", "Aviso", globales.menuPrincipal);
                        if (dialogo == DialogResult.Yes)
                        {
                            this.esInsertar = true;
                            int rowActual = this.row + 1;
                            object[] obj = new object[] { "", "", "", "" };
                            datos.Rows.Insert(rowActual, obj);
                            datos.Rows[rowActual].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);

                            movimientos m = new movimientos();
                            dbaseORM orm = new dbaseORM();
                            m = orm.insert<movimientos>(m, true);

                            datos.Rows[rowActual].Cells[3].Value = m.id;
                            datos.CurrentCell = datos.Rows[rowActual].Cells[0];

                            this.esInsertar = false;
                        }

                    }
                }

                if (e.KeyCode == Keys.Delete && !globales.boolConsulta)
                {
                    DialogResult p = globales.MessageBoxQuestion("¿Desea eliminar el movimiento?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;

                    int id = Convert.ToInt32(datos.Rows[this.row].Cells[3].Value);
                    string tipoMovimiento = Convert.ToString(datos.Rows[this.row].Cells[0].Value);

                    movimientos m = new movimientos();
                    m.id = id;
                    m.tipo_mov = tipoMovimiento;

                    dbaseORM orm = new dbaseORM();
                    bool eliminado = orm.delete<movimientos>(m);
                    datos.Rows.RemoveAt(this.row);
                    if (eliminado)
                        globales.MessageBoxSuccess("Movimiento eliminado correctamente","Aviso",globales.menuPrincipal);
                }
            }
            catch
            {

            }

            this.esInsertar = false;
        }

        private void datos_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void frmMovimientos_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
    }
}
