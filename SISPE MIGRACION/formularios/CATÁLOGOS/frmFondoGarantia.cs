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
    public partial class frmFondoGarantiaCatalogo : Form
    {
        private int numeroMaximo = 0;
        private List<Dictionary<string, object>> resultado = new List<Dictionary<string, object>>();
        internal setDiccionario enviar;
        private bool btnAceptarbool;
        private string folio;

        public frmFondoGarantiaCatalogo()
        {
            InitializeComponent();
        }

        private void frmFondoGarantia_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from datos.d_fondo LIMIT 100";
                resultado = globales.consulta(query);
                resultado.ForEach(o => datos.Rows.Add(o["folio"], o["folio_recibo"], o["importe"]));

                query = string.Format("select MAX(FOLIO) from datos.D_FONDO");

                List<Dictionary<string, object>> resultado2 = globales.consulta(query);
                string maximo = Convert.ToString(resultado2[0]["max"]);
                numeroMaximo = maximo.Length;
            }
            catch
            {

            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            if (resultado.Count == 0) return;
            Close();
            this.btnAceptarbool = true;
            Dictionary<string, object> aux = null;
            foreach (var item in resultado)
            {
                if (Convert.ToString(item["folio"]) == this.folio)
                {
                    aux = item;
                    break;
                }
            }
            enviar(aux);
            this.Close();
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
            if (!string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                if (txtBusqueda.Text.ElementAt(0) >= '0' && txtBusqueda.Text.ElementAt(0) <= '9')
                {
                    if ((e.KeyChar <= '9' && e.KeyChar >= '0' || e.KeyChar == 8))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    txtBusqueda.MaxLength = numeroMaximo;
                }
                else
                {
                    txtBusqueda.MaxLength = 13;
                }
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string query = string.Format("select * from datos.d_fondo");
            if (!string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                query += " where  ";
                if (txtBusqueda.Text.ElementAt(0) >= '0' && txtBusqueda.Text.ElementAt(0) <= '9')
                {
                    string cadenaAux = txtBusqueda.Text;
                    string cadenaAux2 = cadenaAux;
                    if (txtBusqueda.Text.Length != numeroMaximo)
                    {
                        for (int x = txtBusqueda.Text.Length; x <= numeroMaximo; x++)
                        {
                            query += string.Format("  folio >= {0} AND folio <= {1}  OR", cadenaAux, cadenaAux2);
                            cadenaAux += "0";
                            cadenaAux2 += "9";
                        }
                        query = query.Substring(0, query.Length - 2);
                    }
                    else
                    {
                        query += string.Format("  FOLIO = {0} ", txtBusqueda.Text);
                    }

                }
                else
                {
                    query += string.Format("  RFC LIKE '{0}%' OR NOMBRE_EM LIKE '{1}%'", txtBusqueda.Text, txtBusqueda.Text);
                }
            }
            query += " order by FOLIO DESC limit 100";

            resultado = globales.consulta(query);
            datos.Rows.Clear();
            resultado.ForEach(o => datos.Rows.Add(o["folio"], o["folio_recibo"], o["importe"]));
        }

        private void datos_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            folio = Convert.ToString(datos.Rows[e.Cell.RowIndex].Cells[0].Value);
        }

        private void datos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void datos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
