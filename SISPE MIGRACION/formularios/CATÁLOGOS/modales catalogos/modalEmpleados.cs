using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.CATÁLOGOS.modales_catalogos
{
    public partial class modalEmpleados : Form
    {
        internal metodoEnviar enviar;
        private string rfc = string.Empty;
        public modalEmpleados()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void modalEmpleados_Shown(object sender, EventArgs e)
        {
            ActiveControl = txtBuscar;
        }

        private void modalEmpleados_Load(object sender, EventArgs e)
        {
            string query = $"select rfc,nombre_em from datos.empleados where  pendiente = 'f' order by rfc asc limit 100";
            List<Dictionary<string, object>> obj = new dbaseORM().query(query);
            obj.ForEach(o => {
                dtggrid.Rows.Add(o["rfc"], o["nombre_em"]);
            });
        }

        private void modalEmpleados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                ActiveControl = panel2;
                dtggrid.Focus();
            }

            if (e.KeyCode == Keys.F2)
                this.Owner.Close();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{TAB}");
                SendKeys.Send("{DOWN}");
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                aceptar();
            }
        }

        private void aceptar()
        {
            if (dtggrid.Rows.Count != 0)
            {
                dbaseORM orm = new dbaseORM();
                enviar(orm.query($"select * from datos.empleados where  pendiente = 'f' and (rfc = '{this.rfc}')")[0]);
                this.Owner.Close();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string query = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    if (txtBuscar.Text.Contains("..") || txtBuscar.Text.Contains("."))
                    {
                        string texto = txtBuscar.Text.Replace("..", ".");
                        string[] split = texto.Split('.');

                        string nombre_em = string.Empty;

                        foreach (string i in split)
                        {
                            if (string.IsNullOrWhiteSpace(i)) continue;
                            nombre_em += $" nombre_em like '%{i}%' ,";
                        }
                        nombre_em = nombre_em.Substring(0, nombre_em.Length - 1).Replace(",", " and ");

                        query = $"select * from datos.empleados where  pendiente = 'f' and  (rfc like '{txtBuscar.Text}%' or {nombre_em})  order by rfc,nombre_em desc limit 100";

                    }
                    else
                    {
                        query = $"select * from datos.empleados where  pendiente = 'f' and (rfc like '{txtBuscar.Text}%' or nombre_em like '%{txtBuscar.Text}%')  order by  rfc,nombre_em desc limit 100";
                    }
                }
                else
                    query = $"select rfc,nombre_em from datos.empleados where pendiente = 'f' order by rfc,nombre_em asc limit 100";

            }
            catch
            {
            }

            dtggrid.Rows.Clear();

            List<Dictionary<string, object>> obj = new dbaseORM().query(query);
            obj.ForEach(o => {
                dtggrid.Rows.Add(o["rfc"], o["nombre_em"]);
            });
        }

        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            rfc = Convert.ToString(dtggrid.Rows[e.RowIndex].Cells[0].Value);
        }

        private void dtggrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                aceptar();
            }
        }

        private void dtggrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                if (e.KeyChar != 13) txtBuscar.Text += e.KeyChar.ToString();
                txtBuscar.Focus();
                txtBuscar.SelectionStart = txtBuscar.Text.Length;
            }
        }

        private void btnGuardarSecundario_Click(object sender, EventArgs e)
        {
            aceptar();
        }
    }
}
