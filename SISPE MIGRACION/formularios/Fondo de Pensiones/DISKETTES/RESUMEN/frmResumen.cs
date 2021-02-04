using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones.DISKETTES.RESUMEN
{
    public partial class frmResumen : Form
    {
        public frmResumen()
        {
            InitializeComponent();
        }

        private void frmResumen_Load(object sender, EventArgs e)
        {

            cmbTipo.SelectedIndex = 0;           


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            string query = string.Empty;
            List<Dictionary<string, object>> resultado;
            if (cmbTipo.SelectedIndex == 0)
            {
                query = "select archivo,left(right(archivo,4),2) as año, right(right(archivo,4),2) as quincena,SUBSTR(archivo, 0, LENGTH(archivo) - 3) as tipo from datos.aportaciones where archivo is not null and left(archivo,1) in ('A','D','a','d') GROUP BY archivo order by left(right(archivo,4),2) asc,right(right(archivo,4),2) asc";
                resultado = globales.consulta(query);
            }
            else
            {
                query = "select archivo,left(right(archivo,4),2) as año, right(right(archivo,4),2) as quincena,SUBSTR(archivo, 0, LENGTH(archivo) - 3) as tipo from datos.descuentos where archivo is not null and left(archivo,1) in ('A','D','a','d') GROUP BY archivo order by left(right(archivo,4),2) asc,right(right(archivo,4),2) asc";
                resultado = globales.consulta(query);
            }

            //Traer la descripción de las cuentas

            query = "select * from catalogos.disket";

            List<Dictionary<string, object>> cuentas = globales.consulta(query);

            p1.Controls.Clear();

            if (resultado.Count > 0) {

                List<List< Dictionary < string,object>>> grafica = new List<List<Dictionary<string, object>>>();
                int año = Convert.ToInt32(resultado[0]["año"]);
                int contador = 0;
                for (int x = año; x <= Convert.ToInt32(Convert.ToString(DateTime.Now.Year).Substring(2)); x++) {
                    List<Dictionary<string, object>> tmpLista = new List<Dictionary<string, object>>();
                    for (int y = 0; y < 24; y++) {
                        List<Dictionary<string, object>> aux = resultado.Where(o => Convert.ToInt32(o["año"]) == x)
                             .Where(o => Convert.ToInt32(o["quincena"]) == y).ToList<Dictionary<string,object>>();

                        foreach (Dictionary<string,object> item in aux) {
                            string tipo = Convert.ToString(item["tipo"]);
                            bool existe = tmpLista.Any(o => Convert.ToString(o["tipo"]) == Convert.ToString(item["tipo"]));
                            if (!existe)
                            {
                                string[] arreglo = { ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." };
                                Dictionary<string, object> diccionario = new Dictionary<string, object>();
                                diccionario.Add("tipo", item["tipo"]);
                                diccionario.Add("arreglo",arreglo);
                                tmpLista.Add(diccionario);
                            }

                            Dictionary<string, object> tmp = tmpLista.Where(o => Convert.ToString(o["tipo"]) == Convert.ToString(item["tipo"])).ToList().First();
                            string[] auxArreglo = (string[])tmp["arreglo"];
                            auxArreglo[y] = "■";

                        }
                    }
                    contador++;
                    grafica.Add(tmpLista);
                }


                contador = 36;
                int anio = año;
                int totalQuincenas = 0;
                foreach (List<Dictionary<string, object>> item2 in grafica)
                {

                    FlowLayoutPanel aux = new FlowLayoutPanel();
                    aux.Margin = new Padding(0);
                    aux.Size = new Size(1035, 30);
                    aux.BorderStyle = BorderStyle.FixedSingle;
                    aux.FlowDirection = FlowDirection.LeftToRight;
                    p1.Controls.Add(aux);
                    contador += 30;
                    p1.Size = new Size(1230, contador);
                    aux.Controls.Add(rellenarControl($"Archivos agregados del año 20{anio}", 1029, 28, 12));
                    foreach (Dictionary<string, object> obj in item2) {
                        string[] item = (string[])obj["arreglo"];
                        aux = new FlowLayoutPanel();
                        aux.Margin = new Padding(0);
                        aux.Size = new Size(1035, 30);
                        aux.BorderStyle = BorderStyle.FixedSingle;
                        aux.FlowDirection = FlowDirection.LeftToRight;
                        p1.Controls.Add(aux);
                        contador += 30;
                        p1.Size = new Size(1230, contador);

                        var tmp1 = cuentas.Where(o => Convert.ToString(o["cuenta"]) == Convert.ToString(obj["tipo"])).ToList();

                        string descripcionCuenta = string.Empty;

                        if (tmp1.Count != 0) {
                            Dictionary<string, object> auxtmp = tmp1[0];
                                if (tmp1.Count != 0)
                                    descripcionCuenta = Convert.ToString(auxtmp["descripcion"]);
                        }

                        int cantidadQuincenas = item.Count(o => Convert.ToString(o) == "■" || Convert.ToString(o) == "▌");
                        totalQuincenas += cantidadQuincenas;
                        aux.Controls.Add(rellenarControl( Convert.ToString(obj["tipo"]).Substring(1), 89, 28, 12));
                        aux.Controls.Add(rellenarControl($"{item[0]}|{item[1]}", false));
                        aux.Controls.Add(rellenarControl($"{item[2]}|{item[3]}", true));
                        aux.Controls.Add(rellenarControl($"{item[4]}|{item[5]}", false));
                        aux.Controls.Add(rellenarControl($"{item[6]}|{item[7]}", true));
                        aux.Controls.Add(rellenarControl($"{item[8]}|{item[9]}", false));
                        aux.Controls.Add(rellenarControl($"{item[10]}|{item[11]}", true));
                        aux.Controls.Add(rellenarControl($"{item[12]}|{item[13]}", false));
                        aux.Controls.Add(rellenarControl($"{item[14]}|{item[15]}", true));
                        aux.Controls.Add(rellenarControl($"{item[16]}|{item[17]}", false));
                        aux.Controls.Add(rellenarControl($"{item[18]}|{item[19]}", true));
                        aux.Controls.Add(rellenarControl($"{item[20]}|{item[21]}", false));
                        aux.Controls.Add(rellenarControl($"{item[22]}|{item[23]}", true));
                        aux.Controls.Add(rellenarControl(descripcionCuenta, 400, 28, 8,false,false,true));

                    }
                    anio++;
                }
            }


            this.Cursor = Cursors.Default;
        }
        public Panel rellenarControl(string texto, bool color)
        {

            FlowLayoutPanel aux = new FlowLayoutPanel();
            string txt1 = texto.Split('|')[0];
            string txt2 = texto.Split('|')[1];


            aux.BackColor = Color.White;
            aux.Margin = new Padding(0);
            aux.Width = 45;
            aux.BorderStyle = BorderStyle.FixedSingle;
            if (color) aux.BackColor = Color.SkyBlue;

            Label etiqueta = new Label();
            etiqueta.Text = txt1;
            etiqueta.Size = new Size(21, 20);
            etiqueta.Margin = new Padding(0, 4, 0, 0);
            etiqueta.Padding = new Padding(0);

            if (txt1 == "▌")
            {
                etiqueta.BackColor = Color.Black;
                etiqueta.Size = new Size(15, 20);
                etiqueta.Margin = new Padding(5, 4, 0, 0);
            }

            if (txt1 == "_")
            {
                etiqueta.Font = new Font("Adobe Kaiti Std", 12, FontStyle.Bold);
            }
            else
            {
                etiqueta.Font = new Font("Adobe Kaiti Std", 16, FontStyle.Bold);
            }


            Label etiqueta2 = new Label();
            etiqueta2.Text = txt2;
            etiqueta2.Size = new Size(21, 20);
            etiqueta2.Margin = new Padding(0, 4, 0, 0);
            etiqueta2.Padding = new Padding(0);
            if (txt2 == "▌")
            {
                etiqueta2.BackColor = Color.Black;
                etiqueta2.Size = new Size(15, 20);
                etiqueta2.Margin = new Padding(5, 4, 0, 0);
            }
            if (txt2 == "_")
            {
                etiqueta2.Font = new Font("Adobe Kaiti Std", 12, FontStyle.Bold);
            }
            else
            {
                etiqueta2.Font = new Font("Adobe Kaiti Std", 16, FontStyle.Bold);
            }


            aux.Controls.Add(etiqueta);
            aux.Controls.Add(etiqueta2);
            return aux;
        }
        public Label rellenarControl(string texto, int x, int y, int letra, bool fondo = false, bool color = false,bool des = false)
        {
            Label control = new Label();
            control.Text = texto;
            control.AutoSize = false;
            control.Size = new Size(x, y);
            control.Font = new Font("Adobe Kaiti Std", letra, FontStyle.Regular);
            control.Margin = new Padding(-5, 0, 0, 0);
            control.Padding = new Padding(-5, 0, 0, 0);
            control.BackColor = Color.White;
            control.TextAlign = (!des)? ContentAlignment.BottomCenter: ContentAlignment.MiddleLeft;
            control.BorderStyle = BorderStyle.FixedSingle;
            if (color) control.ForeColor = Color.Red;
            if (fondo) control.BackColor = Color.SkyBlue;
            return control;
        }

        private void frmResumen_FormClosing(object sender, FormClosingEventArgs e)
        {
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
        
                this.Close();
        }
    }
}
