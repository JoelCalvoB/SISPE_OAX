using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ.UTILERIAS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ
{
    public partial class frmdiacheque : Form
    {
        private bool modificado2 { get; set; }
        private string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        public frmdiacheque()
        {
            InitializeComponent();
            txtCantidad.Focus();

        }

        private void frmprogcheque_Load(object sender, EventArgs e)
        {
          
            try
            {
                
                string cheques = ("SELECT fecha, inhabil, CASE WHEN to_char(fecha, 'd') = '1' then cast('Domingo' as char(10)) WHEN to_char(fecha,'d') = '2' then cast('Lunes' as char(10)) WHEN to_char(fecha,'d') = '3' then cast('Martes' as char(10)) WHEN to_char(fecha,'d') = '4'then cast('Miercoles' as char(10)) WHEN to_char(fecha,'d') = '5'then cast('Jueves' as char(10)) WHEN to_char(fecha,'d') = '6'then cast('Viernes' as char(10)) WHEN to_char(fecha,'d') = '7' then cast('Sabado' as char(10))END AS dia, programados FROM catalogos.progpq order by fecha desc limit 100");
                List<Dictionary<string,object>> elemento3 = baseDatos.consulta(cheques);
                cmbAño.Items.Clear();
                cmbMes.Items.Clear();
                List<Dictionary<string, object>> resultado = new List<Dictionary<string, object>>();
                List<Dictionary<string, object>> resultad2 = new List<Dictionary<string, object>>();
                int mes = 0;
                if (elemento3.Count != 0) {
                    mes = Convert.ToInt32(Convert.ToString(elemento3[0]["fecha"]).Split('/')[1]);
                }
                foreach (Dictionary<string,object> item in elemento3) {
                    int mes2 = Convert.ToInt32(Convert.ToString(item["fecha"]).Split('/')[1]);
                    intentar:
                    if (mes2 == mes)
                    {
                        Dictionary<string, object> diccionario = new Dictionary<string, object>();
                        diccionario.Add("fecha", item["fecha"]);
                        diccionario.Add("inhabil", item["inhabil"]);
                        diccionario.Add("dia", item["dia"]);
                        diccionario.Add("programados", item["programados"]);
                        resultado.Add(diccionario);
                    }
                    else {
                        resultado.Reverse();
                        resultado.ForEach(o => resultad2.Add(o));
                        resultado = new List<Dictionary<string, object>>();
                        mes = Convert.ToInt32(Convert.ToString(item["fecha"]).Split('/')[1]);
                        goto intentar;
                    }
                }
                resultado.Reverse();
                resultado.ForEach(o => resultad2.Add(o));
                foreach (Dictionary<string,object> item in resultad2)
                {
                    string fecha = Convert.ToString(item["fecha"]).Replace(" 12:00:00 a. m.", "");
                    bool inhabil = Convert.ToString(item["inhabil"]) == "*";
                    string dia = Convert.ToString(item["dia"]);
                    string programados = Convert.ToString(item["programados"]);
                    gridcheques.Rows.Add(fecha, inhabil, dia, programados);

                }


                //Consulta para sacar la fecha más pequeña hasta la fecha actual... combo box
                resultado = globales.consulta("select fecha from catalogos.progpq order by fecha asc limit 1");
                string año = Convert.ToString(resultado[0]["fecha"]).Replace(" 12:00:00 a. m.", "").Split('/')[2];
                int auxAño = Convert.ToInt32(año);
                for (int x = DateTime.Now.Year+1; x >= auxAño; x--)
                {
                    cmbAño.Items.Add(x);
                }
                for (int x = 1; x < this.meses.Length; x++)
                {
                    cmbMes.Items.Add(this.meses[x]);
                }


                //Saca el tope de las últimas fechas......
                DateTime hoy = DateTime.Now;

                string auxHoy = string.Format("{0}-{1}-{2}", hoy.Year, hoy.Month, hoy.Day);
                string query = string.Format("select * from catalogos.progpq where fecha > '{0}' and inhabil <> '*'   order by fecha asc ", auxHoy);
                resultado = globales.consulta(query);
                bool encontrado = false;
                if (resultado.Count > 0)
                {
                    foreach (Dictionary<string, object> item in resultado)
                    {
                        string fecha = Convert.ToString(item["fecha"]).Replace(" 12:00:00 a. m.", "");
                        cmbFecha.Items.Add(fecha);
                        if (!encontrado)
                        {
                            if (Convert.ToInt32(item["utilizados"]) != Convert.ToInt32(item["programados"]))
                            {
                                txtCantidad.Text = Convert.ToString(item["utilizados"]);
                                txtTotal.Text = Convert.ToString(item["programados"]);
                                cmbFecha.Text = Convert.ToString(fecha);
                                encontrado = true;
                            }
                        }
                    }
                }
            }
            catch
            {


            }

        }

        private void frmdiacheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void frmdiacheque_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void gridcheques_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool modificando = Convert.ToBoolean(gridcheques.Rows[e.RowIndex].Cells[1].Value);
                if (e.ColumnIndex == 3)
                {
                    if (modificando && !modificado2 && Convert.ToInt32(gridcheques.Rows[e.RowIndex].Cells[3].Value) != 0)
                    {
                        MessageBox.Show("No se puede programar cheques en un día no hábil", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        modificado2 = true;
                        gridcheques.Rows[e.RowIndex].Cells[3].Value = 0;
                        return;
                    }
                }
                else {
                    if (modificando) {
                        gridcheques.Rows[e.RowIndex].Cells[3].Value = 0;
                    }
                }
            
                modificado2 = false;

                string query = "update catalogos.progpq set inhabil = '{0}' , programados = '{1}' where fecha = '{2}'";
                string aux = (Convert.ToBoolean(gridcheques.Rows[e.RowIndex].Cells[1].Value)) ? "*" : "";
                string aux2 = Convert.ToString(gridcheques.Rows[e.RowIndex].Cells[3].Value);
                string[] sFechaArreglo = Convert.ToString(gridcheques.Rows[e.RowIndex].Cells[0].Value).Split('/');
                string sFecha = string.Format("{0}-{1}-{2}", sFechaArreglo[2],sFechaArreglo[1],sFechaArreglo[0]);
                
                string dia = string.Format(query, aux,aux2, sFecha);
                baseDatos.consulta(dia, true);
            }
            catch
            {

            }
        }

        private void Boxmeses_Enter(object sender, EventArgs e)
        {

        }

        private void gridcheques_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {

        }

        private void gridcheques_Click(object sender, EventArgs e)
        {

        }

        private void cmbAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }
        private void filtrar()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string query = string.Empty;
                gridcheques.Rows.Clear();
                if (!string.IsNullOrWhiteSpace(cmbAño.Text) && string.IsNullOrWhiteSpace(cmbMes.Text))
                {
                    query = string.Format("select *,CASE WHEN to_char(fecha, 'd') = '1' then cast('Domingo' as char(10)) WHEN to_char(fecha,'d') = '2' then cast('Lunes' as char(10)) WHEN to_char(fecha,'d') = '3' then cast('Martes' as char(10)) WHEN to_char(fecha,'d') = '4'then cast('Miercoles' as char(10)) WHEN to_char(fecha,'d') = '5'then cast('Jueves' as char(10)) WHEN to_char(fecha,'d') = '6'then cast('Viernes' as char(10)) WHEN to_char(fecha,'d') = '7' then cast('Sabado' as char(10))END AS dia from catalogos.progpq where fecha >= '{0}-01-01' AND fecha <= '{0}-12-31'", cmbAño.Text);
                }
                else if (string.IsNullOrWhiteSpace(cmbAño.Text) && !string.IsNullOrWhiteSpace(cmbMes.Text))
                {
                    for (int x = 0; x < cmbAño.Items.Count; x++)
                    {
                        query = "select *,CASE WHEN to_char(fecha, 'd') = '1' then cast('Domingo' as char(10)) WHEN to_char(fecha,'d') = '2' then cast('Lunes' as char(10)) WHEN to_char(fecha,'d') = '3' then cast('Martes' as char(10)) WHEN to_char(fecha,'d') = '4'then cast('Miercoles' as char(10)) WHEN to_char(fecha,'d') = '5'then cast('Jueves' as char(10)) WHEN to_char(fecha,'d') = '6'then cast('Viernes' as char(10)) WHEN to_char(fecha,'d') = '7' then cast('Sabado' as char(10))END AS dia from catalogos.progpq ";
                        string item = Convert.ToString(cmbAño.Items[x]);
                        if (cmbMes.SelectedIndex != 11)
                        {
                            query += string.Format("where fecha >= '{0}-{1}-01' AND fecha < '{0}-{2}-01'", item, cmbMes.SelectedIndex + 1, cmbMes.SelectedIndex + 2);
                        }
                        else
                        {
                            string año2 = (Convert.ToInt32(item) + 1).ToString();
                            query += string.Format("where fecha >= '{0}-12-01' AND fecha < '{1}-01-01'", item, año2);
                        }
                        List<Dictionary<string, object>> tmp = globales.consulta(query);
                        foreach (var item2 in tmp)
                        {
                            string fecha = Convert.ToString(item2["fecha"]).Replace(" 12:00:00 a. m.", "");
                            bool inhabil = Convert.ToString(item2["inhabil"]) != "*" ? false : true;
                            string dia = Convert.ToString(item2["dia"]);
                            string programados = Convert.ToString(item2["programados"]);
                            gridcheques.Rows.Add(fecha, inhabil, dia, programados);
                        }
                    }
                    this.Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    query = "select *,CASE WHEN to_char(fecha, 'd') = '1' then cast('Domingo' as char(10)) WHEN to_char(fecha,'d') = '2' then cast('Lunes' as char(10)) WHEN to_char(fecha,'d') = '3' then cast('Martes' as char(10)) WHEN to_char(fecha,'d') = '4'then cast('Miercoles' as char(10)) WHEN to_char(fecha,'d') = '5'then cast('Jueves' as char(10)) WHEN to_char(fecha,'d') = '6'then cast('Viernes' as char(10)) WHEN to_char(fecha,'d') = '7' then cast('Sabado' as char(10))END AS dia from catalogos.progpq ";
                    if (cmbMes.SelectedIndex != 11)
                    {
                        query += string.Format(" where fecha >= '{0}-{1}-01' AND fecha < '{0}-{2}-01';", cmbAño.Text, cmbMes.SelectedIndex + 1, cmbMes.SelectedIndex + 2);
                    }
                    else
                    {
                        string año2 = (Convert.ToInt32(cmbAño.Text) + 1).ToString();
                        query += string.Format(" where fecha >= '{0}-12-01' AND fecha < '{1}-01-01';", cmbAño.Text, año2);
                    }

                }
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                foreach (var item in resultado)
                {
                    string fecha = Convert.ToString(item["fecha"]).Replace(" 12:00:00 a. m.", "");
                    bool inhabil = Convert.ToString(item["inhabil"]) != "*" ? false : true;
                    string dia = Convert.ToString(item["dia"]);
                    string programados = Convert.ToString(item["programados"]);
                    gridcheques.Rows.Add(fecha, inhabil, dia, programados);

                }
            }
            catch
            {
                MessageBox.Show("Error en el sistema, contactar a los de informatica", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            globales.showModal(new frmGenerarMesCheque());
            gridcheques.Rows.Clear();
            frmprogcheque_Load(null, null);
            cmbMes.SelectedIndex = -1;
            cmbAño.SelectedIndex = -1;
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea efectuar los cambios?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Debes insertar los cheques emitidos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCantidad.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTotal.Text))
            {
                MessageBox.Show("Debes insertar el total de cheques a emitir..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTotal.Focus();
                return;
            }

            if (Convert.ToInt32(txtCantidad.Text) > Convert.ToInt32(txtTotal.Text)) {
                MessageBox.Show("La cantidad de cheques emitidos no debe ser mayor al limite de cheques..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCantidad.Focus();
                return;
            }
            string[] arregloSplit = cmbFecha.Text.Split('/');
            string fechaAplicar = string.Format("{0}-{1}-{2}", arregloSplit[2], arregloSplit[1], arregloSplit[0]);
            string query = string.Format("update catalogos.progpq set utilizados = {0},programados = {1} where fecha =  '{2}';", txtCantidad.Text, txtTotal.Text, fechaAplicar);
            globales.consulta(query);
            frmprogcheque_Load(null, null);
            MessageBox.Show("Proceso finalizado!!","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private void cmbFecha_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string[] arreglo = cmbFecha.Text.Split('/');
                string fechaAux = string.Format("{0}-{1}-{2}", arreglo[2], arreglo[1], arreglo[0]);
                string query = string.Format("select * from  catalogos.progpq where inhabil <> '*' and  fecha =  '{0}';",fechaAux);
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count > 0) {
                    txtCantidad.Text = Convert.ToString(resultado[0]["utilizados"]);
                    txtTotal.Text = Convert.ToString(resultado[0]["programados"]);
                }
            }
            catch {

            }
        }

        private void gridcheques_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            txtCantidad.Focus();
        }

        private void gridcheques_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas cerrar el modulo?", globales.menuPrincipal);
            if (p == DialogResult.Yes)
                this.Close();
        }
    }
}

