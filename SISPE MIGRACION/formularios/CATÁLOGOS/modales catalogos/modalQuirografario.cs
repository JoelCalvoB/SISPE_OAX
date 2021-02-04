using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

delegate void metodoEnviar(Dictionary<string,object> obj);
namespace SISPE_MIGRACION.formularios.CATÁLOGOS.modales_catalogos
{
    public partial class modalQuirografario<clase> : Form
    {
        internal metodoEnviar enviar;
        private string tabla = string.Empty;
        private string esquema = string.Empty;
        private string folio = string.Empty;
        private int totalFolio = 0;
        private string t_prestamo { get; set; }
        private string bandera { get; set; }


        private string buscar { get; set; }
        
        
        public modalQuirografario(string t_prestamo = "",string buscar = ""  , string bandera= "")
        {
            InitializeComponent();
            this.t_prestamo = t_prestamo;
            this.bandera = bandera;

            if (!string.IsNullOrWhiteSpace(t_prestamo))
            {
                this.t_prestamo = $" and t_prestamo = '{t_prestamo.ToUpper()}'";
            }



            this.buscar = buscar;
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void btnGuardarSecundario_Click(object sender, EventArgs e)
        {
            aceptar();
        }

        private void aceptar()
        {
            if (dtggrid.Rows.Count != 0) {
                dbaseORM orm = new dbaseORM();
                enviar(orm.query($"select * from {this.esquema}.{this.tabla} where folio = {this.folio} {this.t_prestamo}")[0]);
                this.Owner.Close();
            }
        }

        private void modalQuirografario_Load(object sender, EventArgs e)
        {
            
            
            MemberInfo informacion = typeof(clase);
            Type tipoAssemby = typeof(clase);
            this.tabla = informacion.Name;
            label45.Text = this.tabla == "p_edocta" ? "Quirografarios" : "Hipotecarios";
            string[] nombreEsquemaArray = tipoAssemby.Namespace.Split('.');
            this.esquema = nombreEsquemaArray[nombreEsquemaArray.Length - 1];

            string query = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.t_prestamo))
            {
                query = $"select * from {esquema}.{tabla} where {t_prestamo.Replace("and","")} order by folio desc limit 100";
            }
            else {

                if (this.bandera=="true")
                {
                    query = $"select * from {esquema}.{tabla} where historial_pagado is null order by folio desc limit 100";
                }
                else
                {
                    query = $"select * from {esquema}.{tabla} order by folio desc limit 100";

                }
            }
          

            List<Dictionary<string,object>> obj = new dbaseORM().query(query);
            obj.ForEach(o => {
                dtggrid.Rows.Add(o["folio"], o["rfc"], o["nombre_em"]);
            });

            if (!string.IsNullOrWhiteSpace(this.t_prestamo))
            {
                query = $"select length(COALESCE(max(folio),0)::text) as cantidad from {this.esquema}.{this.tabla} where {t_prestamo.Replace("and","")}";
            }
            else {
                query = $"select length(COALESCE(max(folio),0)::text) as cantidad from {this.esquema}.{this.tabla}";
            }
            //                query = $"select length(COALESCE(max(folio),0)::text) as cantidad from {this.esquema}.{this.tabla} where historial_pagado is null";

            obj = new dbaseORM().query(query);
            this.totalFolio = globales.convertInt(Convert.ToString(obj[0]["cantidad"]));


            txtBuscar.Text = this.buscar;
        }

        private void modalQuirografario_Shown(object sender, EventArgs e)
        {
            ActiveControl = dtggrid;
        }

        private void dtggrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                aceptar();
            }
            
        }

        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            folio = Convert.ToString(dtggrid.Rows[e.RowIndex].Cells[0].Value);
        }

        private void dtggrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) )
            {
                if(e.KeyChar != 13) txtBuscar.Text += e.KeyChar.ToString();
                txtBuscar.Focus();
                txtBuscar.SelectionStart = txtBuscar.Text.Length;
            }
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) {
                SendKeys.Send("{TAB}");
                SendKeys.Send("{DOWN}");
            }
        }

        private void modalQuirografario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                ActiveControl = panel2;
                dtggrid.Focus();
            }

            if (e.KeyCode == Keys.F2)
                this.Owner.Close();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        { 




            string query = string.Empty;
            if (string.IsNullOrWhiteSpace(txtBuscar.Text)) {
                if (!string.IsNullOrWhiteSpace(t_prestamo))
                {
                    query = $"select * from {esquema}.{tabla} where {t_prestamo.Replace("and","")} order by folio desc limit 100";
                }
                else {
                    query = $"select * from {esquema}.{tabla} order by folio desc limit 100";
                }
            } else if (char.IsNumber(txtBuscar.Text.First())) {

                if (this.bandera == "true")
                {
                    query = $"select * from {this.esquema}.{this.tabla} where  historial_pagado is null and ";
                }
                else
                {
                    query = $"select * from {this.esquema}.{this.tabla} where  ";   ///

                }
                string aux = string.Empty;
                if (this.totalFolio == txtBuscar.Text.Length)
                {
                    aux = " folio = " + txtBuscar.Text;
                }
                else {
                    string strFolio = txtBuscar.Text;
                    string desde = strFolio;
                    string hasta = strFolio;
                    string between = "folio = " + strFolio;
                    for (int x = txtBuscar.Text.Length; x < this.totalFolio; x++) {
                        desde += "0";
                        hasta += "9";
                        between += $" or folio between {desde} and {hasta} ";
                    }
                    aux = between + $" {t_prestamo}   order by folio asc limit 100";
                }
                query += aux ;
            }
            else {
                if (txtBuscar.Text.Contains("..") || txtBuscar.Text.Contains("."))
                {
                    string texto = txtBuscar.Text.Replace("..",".");
                    string[] split = texto.Split('.');

                    string nombre_em = string.Empty;

                    foreach (string i in split) {
                        if (string.IsNullOrWhiteSpace(i)) continue;
                        nombre_em += $" nombre_em like '%{i}%' ,";
                    }
                    nombre_em = nombre_em.Substring(0,nombre_em.Length-1).Replace(","," and ");

                    query = $"select * from {this.esquema}.{this.tabla} where  rfc like '{txtBuscar.Text}%' or {nombre_em} {t_prestamo} order by folio desc limit 100";

                }
                else {

                    if (this.bandera == "true")
                    {
                        query = $"select * from {this.esquema}.{this.tabla} where  rfc like '{txtBuscar.Text}%' or nombre_em like '%{txtBuscar.Text}%' {t_prestamo} and historial_pagado is null order by folio desc limit 100";

                    }
                    else
                    {
                        query = $"select * from {this.esquema}.{this.tabla} where  rfc like '{txtBuscar.Text}%' or nombre_em like '%{txtBuscar.Text}%' {t_prestamo}  order by folio desc limit 100";

                    }
                }
            }
            dtggrid.Rows.Clear();

            List<Dictionary<string, object>> obj = new dbaseORM().query(query);
            obj.ForEach(o=> {
                dtggrid.Rows.Add(o["folio"],o["rfc"],o["nombre_em"]);
            });
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {
                e.Handled = true;
                aceptar();
            }

            if (!string.IsNullOrWhiteSpace(txtBuscar.Text)) {
                if (char.IsNumber(txtBuscar.Text.First()))
                {
                    e.Handled = !char.IsNumber(e.KeyChar);
                    if (e.KeyChar == 8) e.Handled = false;
                }
            }
        }
    }
}
