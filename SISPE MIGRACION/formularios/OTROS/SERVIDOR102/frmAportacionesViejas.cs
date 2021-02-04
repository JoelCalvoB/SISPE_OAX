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
    public partial class frmAportacionesViejas : Form
    {
        List<Dictionary<string, object>> resultado2;
        Dictionary<string, object> resultado;
        List<Dictionary<string, object>> listaAportaciones;
        public frmAportacionesViejas()
        {
            InitializeComponent();
        }

        private void frmAportacionesViejas_Load(object sender, EventArgs e)
        {
            
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frm ventana = new frm();
            ventana.enviar = rellenarDatos;
            ventana.ShowDialog();

        }
        private void rellenarDatos(Dictionary<string,object> obj) {
            string rfc = Convert.ToString(obj["rfc"]);
            txtrfc.Text = rfc;
            this.panelTarjeta.Visible = true;
            this.resultado = obj;
            string query = $"select * from israel.aportaciones where rfc like '%{rfc}%' order by desde asc";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            this.resultado2 = resultado;
            select.Checked = true;
            datos.Rows.Clear();
            lista1.Items.Clear();
            resultado.ForEach(o => datos.Rows.Add(true, o["rfc"], string.Format("{0:d}", o["desde"]), string.Format("{0:d}", o["hasta"]), "A" + o["tipo"], string.Format("{0:C}", o["aportacion"]).Replace("$","")));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select id from datos.aportaciones ";
            List<Dictionary<string, object>> identificadores = globales.consulta2(query);
            int contador = 1;
            foreach (Dictionary<string, object> item in identificadores)
            {
                string ide = Convert.ToString(item["id"]);
                query = $"select * from datos.aportaciones where id = {ide}";
                List<Dictionary<string, object>> resultado = globales.consulta2(query);
                string rfc = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["rfc"])) ? "null" : "'" + Convert.ToString(resultado[0]["rfc"]) + "'";
                string inicio = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["inicio"])) ? "null" : "'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(Convert.ToString(resultado[0]["inicio"]))) + "'";
                string final = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["final"])) ? "null" : "'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(Convert.ToString(resultado[0]["final"]))) + "'";
                string new_tipo = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["new_tipo"])) ? "null" : "'" + Convert.ToString(resultado[0]["new_tipo"]) + "'";
                string movimiento = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["movimiento"])) ? "null" : "'" + Convert.ToString(resultado[0]["movimiento"]) + "'";
                string entrada = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["entrada"])) ? "0" : Convert.ToString(resultado[0]["entrada"]);
                string salida = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["salida"])) ? "0" : Convert.ToString(resultado[0]["salida"]);
                string status = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["status"])) ? "null" : "'" + Convert.ToString(resultado[0]["status"]) + "'";
                string cuenta = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["cuenta"])) ? "null" : "'" + Convert.ToString(resultado[0]["cuenta"]) + "'";
                string fum = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["fum"])) ? "null" : "'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(Convert.ToString(resultado[0]["fum"]))) + "'";
                string fac = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["fac"])) ? "null" : "'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(Convert.ToString(resultado[0]["fac"]))) + "'";
                string mac = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["mac"])) ? "null" : "'" + Convert.ToString(resultado[0]["mac"]) + "'";
                string fecharegistro = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["fecharegistro"])) ? "null" : "'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(Convert.ToString(resultado[0]["fecharegistro"]))) + "'";
                string id = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["id"])) ? "null" : Convert.ToString(resultado[0]["id"]);
                string curp = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["curp"])) ? "null" : "'" + Convert.ToString(resultado[0]["curp"]) + "'";
                string archivo = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["archivo"])) ? "null" : "'" + Convert.ToString(resultado[0]["archivo"]) + "'";
                string idusuario = string.IsNullOrWhiteSpace(Convert.ToString(resultado[0]["idusuario"])) ? "null" : Convert.ToString(resultado[0]["idusuario"]);
                query = "INSERT INTO datos.aportaciones (rfc, inicio, final, new_tipo, movimiento, entrada, salida, status, cuenta, fum, fac, mac, fecharegistro, id, curp, archivo, idusuario) VALUES " +
                  $" ({rfc},{inicio},{final},{new_tipo},{movimiento},{entrada},{salida},{status},{cuenta},{fum},{fac},{mac}," +
                  $"{fecharegistro},{id},{curp},{archivo},{idusuario});";

                globales.consulta(query, true);
                System.Diagnostics.Debug.WriteLine(contador);
                contador++;
            }
        }

        private void frmAportacionesViejas_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmAportacionesViejas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6) {
                new frmGrafica(this.resultado,this.resultado2).ShowDialog();
            }

            if (Keys.F5 == e.KeyCode) {
                btnNuevo_Click(null,null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAportacionesViejas_Shown(object sender, EventArgs e)
        {
            btnCerrar.Focus();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (lista1.Items.Count == 0) {
                globales.MessageBoxExclamation("Se debe buscar la tarjeta a relacionar.","Aviso",globales.menuPrincipal);
                return;
            }

            if (lista1.SelectedIndex == -1) {
                globales.MessageBoxExclamation("Seleccionar la tarjeta del empleado a relacionar", "Aviso", globales.menuPrincipal);
                return;
            }
            string rfcSeleccionado = Convert.ToString(lista1.SelectedItem);
            DialogResult p = globales.MessageBoxQuestion($"Desea relacionar el RFC {txtrfc.Text} con el RFC {rfcSeleccionado}?","Aviso",globales.menuPrincipal);
            if (p == DialogResult.No)
                return;


            List<Dictionary<string, object>> resultado = new List<Dictionary<string, object>>();
            bool entro = false;
            foreach (DataGridViewRow item in datos.Rows) {
                bool checado = Convert.ToBoolean(item.Cells[0].Value);
                if (checado){
                    entro = true;
                    Dictionary<string, object> obj = new Dictionary<string, object>();
                    string rfc = Convert.ToString(item.Cells[1].Value);
                    string new_tipo = Convert.ToString(item.Cells[4].Value);
                    string inicio = Convert.ToString(item.Cells[2].Value);
                    string final = Convert.ToString(item.Cells[3].Value);
                    string aportacion = Convert.ToString(item.Cells[5].Value);
                    obj.Add("rfc",rfc);
                    obj.Add("new_tipo", new_tipo);
                    obj.Add("inicio", inicio);
                    obj.Add("final", final);
                    obj.Add("aportacion", aportacion);
                    resultado.Add(obj);
                }
            }
            if (!entro) {
                globales.MessageBoxExclamation("No se selecciono nunguna aportación","Aviso",globales.menuPrincipal);
                return;
            }
            globales.showModal(new frmRelacionarTarjetas(rfcSeleccionado,resultado));
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtrfc.Text)) {
                globales.MessageBoxExclamation("Favor de ingresar RFC","Aviso",globales.menuPrincipal);
                return;
            }

            string query = $"select rfc from datos.empleados where rfc like '%{txtrfc.Text.Trim()}%'";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            lista1.Items.Clear();
            foreach (Dictionary<string,object> item in resultado) {
                lista1.Items.Add(item["rfc"]);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in datos.Rows)
            {
                item.Cells[0].Value = select.Checked;
            }
        }
    }
}
