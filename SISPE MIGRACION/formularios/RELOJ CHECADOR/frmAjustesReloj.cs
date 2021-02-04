using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.RELOJ_CHECADOR
{
    public partial class frmAjustesReloj : Form
    {

        private List<Dictionary<string, object>> resultado;
        int c;
        string fecha_c = string.Empty;
        int r;
        string desde;
        string hasta;
        String fecha_a = string.Empty;


        string quincena;
        int del;
        int al;
        String mesA;
        int anio;
        int cuentadia;
        int mesS;
        string mesLetra;
        public frmAjustesReloj()
        {
            InitializeComponent();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void comboNombres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Delete)
            {
                comboNombres.Items.Clear();
            }
        }

        private void comboNombres_DropDown(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM reloj.empleados where nombre like '%{comboNombres.Text}%';";
            this.resultado = globales.consulta(query);
                comboNombres.Items.Clear();
            foreach (var item in this.resultado)
            {
                string nombre = Convert.ToString(item["nombre"]);
                comboNombres.Items.Add(nombre);
            }
        }

        private void comboNombres_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(var item in this.resultado)
            {
                string nombre = Convert.ToString(item["nombre"]);
                if (nombre==comboNombres.Text)
                {
                    String id = Convert.ToString(item["id"]);
            

                    txtNoEmpleado.Text = id;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            rellenaGrid();
        }


        private void rellenaGrid()
        {
            DateTime fecha_actual = DateTime.Now;
            this. fecha_a = string.Format("{0:yyyy-MM-dd}", this.desde).Replace(" 12:00:00 a. m.", "");
            this.fecha_c = string.Format("{0:yyyy-MM-dd}", this.hasta).Replace(" 12:00:00 a. m.", "");

            string id = txtNoEmpleado.Text;
            string id_compuesto = string.Empty;
            if (id.Length == 1) id_compuesto = "0000" + id;
            if (id.Length == 2) id_compuesto = "000" + id;
            if (id.Length == 3) id_compuesto = "00" + id;





            string query = $"SELECT * FROM reloj.vempleadoes where  (id::numeric) ='{id_compuesto}' and fecha BETWEEN '{string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value)}' and '{string.Format("{0:yyyy-MM-dd}", dateTimePicker2.Value)}' order by fecha ASC";
            List<Dictionary<string, object>> lista = globales.consulta(query);
            dataGridView1.Rows.Clear();
            foreach (var item in lista)
            {
                string id_ = Convert.ToString(item["id"]);
                string fecha = Convert.ToString(item["fecha"]).Replace(" 12:00:00 a. m.", ""); ;
                string hentrada = Convert.ToString(item["hentrada"]);
                string hsalida = Convert.ToString(item["hsalida"]);
                string insidencias = Convert.ToString(item["insidencias"]);
                string serial = Convert.ToString(item["serial"]);

                dataGridView1.Rows.Add(id, fecha, hentrada, hsalida, insidencias, serial);
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            c = e.ColumnIndex;
            r = e.RowIndex;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = Convert.ToString(dataGridView1.Rows[r].Cells[5].Value);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string fecha = Convert.ToString(dataGridView1.Rows[r].Cells[1].Value);
                if (fecha.Length <=2)
                {
                    string cuerpo = string.Format("{0:/MM/yyyy}", DateTime.Parse(this.fecha_a));
                    fecha = fecha + cuerpo;
                    dataGridView1.Rows[r].Cells[1].Value = fecha;
                }
                string hentrada = Convert.ToString(dataGridView1.Rows[r].Cells[2].Value);
                if (!hentrada.Contains("AM"))
                {
                    hentrada = hentrada + "AM";
                    dataGridView1.Rows[r].Cells[2].Value = hentrada;
                }
                string hsalida = Convert.ToString(dataGridView1.Rows[r].Cells[3].Value);
                if (!hsalida.Contains("PM"))
                {
                    hsalida = hsalida + "PM";
                    dataGridView1.Rows[r].Cells[3].Value = hsalida;
                }
                string insidencias = Convert.ToString(dataGridView1.Rows[r].Cells[4].Value);

                string serial = Convert.ToString(dataGridView1.Rows[r].Cells[5].Value);
                if (string.IsNullOrWhiteSpace(fecha) || string.IsNullOrWhiteSpace(hentrada) || string.IsNullOrWhiteSpace(hsalida) && string.IsNullOrWhiteSpace(serial))
                {
                    return;
                }

                string query = $"update reloj.vempleadoes set hentrada='{hentrada}' , hsalida='{hsalida}' , fecha='{fecha}', insidencias='{insidencias}' where serial ={serial};";
                globales.consulta(query, true);
            }
            catch
            {
                DialogResult dialogo = globales.MessageBoxError("OCURRIO UN ERROR EN EL INGRESO DE REGISTROS", "AVISO", globales.menuPrincipal);
            }


       
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Insert)
            {

                string id = txtNoEmpleado.Text;
                string id_compuesto = string.Empty;
                if (id.Length == 1) id_compuesto = "0000" + id;
                if (id.Length == 2) id_compuesto = "000" + id;
                if (id.Length == 3) id_compuesto = "00" + id;

                try
                {



                    string inserta = $"insert into reloj.vempleadoes (id , fecha , hentrada , hsalida ) values ('{id_compuesto}', '{string.Format("{0:yyyy-MM-dd}",DateTime.Parse(this.fecha_a))}','12:00AM','12:00PM')";
                    globales.consulta(inserta, true);

                    rellenaGrid();
                    dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);
                }
                catch
                {
                    DialogResult dialogo = globales.MessageBoxError("OCURRIO UN ERROR EN EL INGRESO DE REGISTROS", "AVISO", globales.menuPrincipal);

                }




            }

            if (e.KeyCode==Keys.Delete)
            {
                string serial = Convert.ToString(dataGridView1.Rows[r].Cells[5].Value);
                string query = $"delete from reloj.vempleadoes  where serial ={serial}";
                if (string.IsNullOrWhiteSpace(serial)) return;
                try
                {
                    globales.consulta(query);
                    rellenaGrid();

                }
                catch
                {
                    DialogResult dialogo = globales.MessageBoxError("OCURRIO UN ERROR EN EL INGRESO DE REGISTROS", "AVISO", globales.menuPrincipal);

                }
            }
        }

        private void frmAjustesReloj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F5)
            {
                if (!string.IsNullOrWhiteSpace(txtNoEmpleado.Text))
                {
                    rellenaGrid();
                }
            }
        }

        private void comboNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                char S;

                S = Char.ToUpper(e.KeyChar);

                e.KeyChar = S;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }


        private void SacaFechas()
        {
            DateTime fecha = new DateTime();
            DateTime fecha1 = new DateTime();


            DateTime f1 = DateTime.Now;
            int dia = f1.Day;
            if (dia >= 1 && dia <= 14)
            {
                int mes = f1.Month;
                int año = f1.Year;
                if (mes == 12)
                    fecha = new DateTime(año + 1, 1, 1);
                else
                    fecha = new DateTime(año, mes, 1);
                fecha1 = new DateTime(año, mes, 15);



                fecha = fecha.AddDays(-1);   //hasta
                this.hasta = Convert.ToString(fecha);
                fecha1 = fecha1.AddMonths(-1);  //desde
                this.desde = Convert.ToString(fecha1);

                


            }

            if (dia >= 16 && dia <= 30)
            {
                int mes = f1.Month;
                int año = f1.Year;
                if (mes == 12)
                    fecha = new DateTime(año + 1, 1, 1);
                else
                    fecha = new DateTime(año, mes, 1);
                fecha1 = new DateTime(año, mes, 16);


            //    fecha1 = fecha1.AddMonths(-1);    // hasta  
                this.hasta = Convert.ToString(fecha1);

            //    fecha = fecha.AddMonths(-1);   //desde
                this.desde = Convert.ToString(fecha);
            }
        }

        private void frmAjustesReloj_Shown(object sender, EventArgs e)
        {
         //   SacaFechas();


            DateTime fecha = new DateTime();
            DateTime fecha1 = new DateTime();


            DateTime f1 = DateTime.Now;
            int dia = f1.Day;
            if (dia >= 1 && dia <= 15)
            {
                int mes = f1.Month;
                int año = f1.Year;
                if (mes == 12)
                    fecha = new DateTime(año + 1, 1, 1);
                else
                    fecha = new DateTime(año, mes, 1);
                fecha1 = new DateTime(año, mes, 16);



                fecha = fecha.AddDays(-1);
                dateTimePicker2.Text = Convert.ToString(fecha);
                this.hasta = Convert.ToString(fecha);

                fecha1 = fecha1.AddMonths(-1);

                dateTimePicker1.Text = Convert.ToString(fecha1);
                this.desde = Convert.ToString(fecha1);

                this.quincena = "SEGUNDA";
                this.del = Convert.ToInt32(fecha1.Day);
                this.al = Convert.ToInt32(fecha.Day);
                this.anio = Convert.ToInt32(fecha.Year);
                this.mesA = Convert.ToString(fecha.Month);
                if (mesA == "1") this.mesLetra = "ENERO";
                if (mesA == "2") this.mesLetra = "FEBRERO";
                if (mesA == "3") this.mesLetra = "MARZO";
                if (mesA == "4") this.mesLetra = "ABRIL";
                if (mesA == "5") this.mesLetra = "MAYO";
                if (mesA == "6") this.mesLetra = "JUNIO";
                if (mesA == "7") this.mesLetra = "JULIO";
                if (mesA == "8") this.mesLetra = "AGOSTO";
                if (mesA == "9") this.mesLetra = "SEPTIEMBRE";
                if (mesA == "10") this.mesLetra = "OCTUBRE";
                if (mesA == "11") this.mesLetra = "NOVIEMBRE";
                if (mesA == "12") this.mesLetra = "DICIEMBRE";

            }

            if (dia >= 16 && dia <= 30)
            {
                this.mesS = f1.Month;
                int año = f1.Year;
                if (mesS == 12)
                    fecha = new DateTime(año + 1, 1, 1);
                else
                    fecha = new DateTime(año, mesS, 1);
                fecha1 = new DateTime(año, mesS, 15);


          //      fecha1 = fecha1.AddMonths(-1);
                dateTimePicker2.Text = Convert.ToString(fecha1);
                this.hasta = Convert.ToString(fecha1);

                //    fecha = fecha.AddMonths(-1);
                dateTimePicker1.Text = Convert.ToString(fecha);
                this.desde = Convert.ToString(fecha);


                this.quincena = "PRIMERA";
                this.del = Convert.ToInt32(fecha.Day);
                this.al = Convert.ToInt32(fecha1.Day);
                this.anio = Convert.ToInt32(fecha.Year);
                this.mesA = Convert.ToString(fecha.Month);
                if (mesA == "1") this.mesLetra = "ENERO";
                if (mesA == "2") this.mesLetra = "FEBRERO";
                if (mesA == "3") this.mesLetra = "MARZO";
                if (mesA == "4") this.mesLetra = "ABRIL";
                if (mesA == "5") this.mesLetra = "MAYO";
                if (mesA == "6") this.mesLetra = "JUNIO";
                if (mesA == "7") this.mesLetra = "JULIO";
                if (mesA == "8") this.mesLetra = "AGOSTO";
                if (mesA == "9") this.mesLetra = "SEPTIEMBRE";
                if (mesA == "10") this.mesLetra = "OCTUBRE";
                if (mesA == "11") this.mesLetra = "NOVIEMBRE";
                if (mesA == "12") this.mesLetra = "DICIEMBRE";
            

                        }

    }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string barre = $"SELECT a1. ID, rfc, nombre, adscripcion, modalidad, a2.descripcion FROM 	reloj.empleados a1 LEFT JOIN reloj.horarios a2 ON CAST (a1. idhorario as INTEGER) = CAST (a2.ID as INTEGER) WHERE 	status = TRUE and a1.id={txtNoEmpleado.Text} ";
            List<Dictionary<string, object>> resultado = globales.consulta(barre);

            List<object[]> listaenviar = new List<object[]>();

            foreach (var item in resultado)
            {
                string id = Convert.ToString(item["id"]);
                string rfc = Convert.ToString(item["rfc"]);
                string nombre = Convert.ToString(item["nombre"]);
                string adscripcion = Convert.ToString(item["adscripcion"]);
                string modalidad = Convert.ToString(item["modalidad"]);
                string descripcion = Convert.ToString(item["descripcion"]);
                string id_compuesto = string.Empty;
                if (id.Length == 1) id_compuesto = "0000" + id;
                if (id.Length == 2) id_compuesto = "000" + id;
                if (id.Length == 3) id_compuesto = "00" + id;



                string busca = $"select id,fecha , hentrada , hsalida,insidencias  from reloj.vempleadoes where  id='{id_compuesto}'  and fecha between '{(dateTimePicker1.Text).Replace(" 12:00:00 a. m.", "")}' and  '{(dateTimePicker2.Text).Replace(" 12:00:00 a. m.", "")}'  order by fecha asc";
                List<Dictionary<string, object>> lista1 = globales.consulta(busca);
                if (lista1.Count <= 0) continue;
                object[] aux = new object[lista1.Count];

                int primero = this.del;
                int termina = this.al;


                int tamaño = (termina - primero) + 1;


                int contador = 0;
                int contadorobj = 0;

                for (int x = primero; x <= termina; x++)
                {
                    DateTime d = new DateTime(this.anio, dateTimePicker1.Value.Month, x);
                    bool encuentra = lista1.Any(o => Convert.ToDateTime(o["fecha"]).Equals(d));



                    object[] tt1 = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                    if (encuentra)
                    {
                        tt1[0] = string.Format("{0:d}", lista1[contador]["fecha"]);
                        tt1[1] = lista1[contador]["hentrada"];
                        tt1[2] = lista1[contador]["hsalida"];
                        tt1[3] = lista1[contador]["insidencias"];
                        tt1[4] = lista1[contador]["id"];
                        tt1[5] = rfc;
                        tt1[6] = nombre;
                        tt1[7] = this.quincena;
                        tt1[8] = this.del;
                        tt1[9] = this.al;
                        tt1[10] = adscripcion;
                        tt1[11] = modalidad;
                        tt1[12] = descripcion;
                        tt1[13] = this.anio;
                        tt1[14] = mesLetra;







                        contador++;
                    }
                    else
                    {
                        if (x == 29)
                        {

                        }
                        string dianombre = string.Format("{0:dddd}", d);
                        tt1[0] = string.Format("{0:d}", d);
                        tt1[1] = "---";
                        tt1[2] = "---";
                        tt1[3] = (dianombre.ToUpper().Contains("Á") || dianombre.ToUpper().Contains("G")) ? dianombre.ToUpper() : "";
                        tt1[4] = id_compuesto;
                        tt1[5] = rfc;
                        tt1[6] = nombre;
                        tt1[7] = this.quincena;
                        tt1[8] = this.del;
                        tt1[9] = this.al;
                        tt1[10] = adscripcion;
                        tt1[11] = modalidad;
                        tt1[12] = descripcion;
                        tt1[13] = this.anio;
                        tt1[14] = mesLetra;




                    }


                    //objreporte[contadorobj] = tt1;
                    listaenviar.Add(tt1);

                    contadorobj++;
                }



            }


            object[] objreporte = new object[listaenviar.Count];

            for (int x = 0; x < objreporte.Length; x++)
            {
                objreporte[x] = listaenviar[x];
            }



            object[] parametros = { "titulo" };
            object[] valor = { "" };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;


            globales.reportes("tarjeta_reloj", "reloj", objreporte, "", false, enviarParametros);
            this.Cursor = Cursors.Default;

        
    }
    }
}
