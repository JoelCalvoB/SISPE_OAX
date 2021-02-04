using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.RELOJ_CHECADOR
{
    public partial class frmReporteReloj : Form
    {
        string quincena;
        int del;
        int al;
        String mesA;
        int anio;
        int cuentadia;
        int mesS;
        string mesLetra;
        

        public frmReporteReloj()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string barre = $"SELECT a1. ID, rfc, nombre, adscripcion, modalidad, a2.descripcion FROM 	reloj.empleados a1 LEFT JOIN reloj.horarios a2 ON CAST (a1. idhorario as INTEGER) = CAST (a2.ID as INTEGER) WHERE 	status = TRUE ORDER BY 	ID";
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


                string busca = $"select id,fecha , hentrada , hsalida,insidencias  from reloj.vempleadoes where id='{id_compuesto}'  and fecha between '{(dateTimePicker1.Text).Replace(" 12:00:00 a. m.", "")}' and  '{(dateTimePicker2.Text).Replace(" 12:00:00 a. m.", "")}'  order by fecha asc";
                List<Dictionary<string, object>> lista1 = globales.consulta(busca);
                if (lista1.Count <= 0) continue;
                object[] aux = new object[lista1.Count];

                int primero = this.del;
                int termina = this.al;


                int tamaño = (termina - primero) + 1;

                
                int contador = 0;
                int contadorobj = 0;

                for (int x = primero; x <= termina; x++) {
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
                    else {
                        if (x == 29) {

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

            for (int x =0; x < objreporte.Length; x++) {
                objreporte[x] = listaenviar[x];
            }



            object[] parametros = { "titulo" };
            object[] valor = { "" };
            object[][] enviarParametros = new object[11][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;


            globales.reportes("tarjeta_reloj", "reloj", objreporte, "", false, enviarParametros);
            this.Cursor = Cursors.Default;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmReporteReloj_Shown(object sender, EventArgs e)
        {
            DateTime fecha = new DateTime();
            DateTime fecha1 = new DateTime();


            DateTime f1 = DateTime.Now;

            if (f1.Day >= 1 && f1.Day <= 14)
            {
                f1 = new DateTime(f1.Year, f1.Month, 16);
                f1 = f1.AddMonths(-1);
            }
            else {
                f1 = new DateTime(f1.Year, f1.Month, 1);
            }

            int dia = f1.Day;
            if (dia >= 1 && dia <= 14)
            {
                int mes = f1.Month; 
                int año = f1.Year;

                fecha = new DateTime(año, mes, 1);
                fecha1 = new DateTime(año, mes, 15);



                fecha = fecha.AddDays(-1);
                dateTimePicker2.Text = Convert.ToString(fecha);
                fecha1 = fecha1.AddMonths(-1);

                dateTimePicker1.Text = Convert.ToString(fecha1);

                this.quincena = "PRIMERA";
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

                fecha = f1;
                fecha1 = new DateTime(año, mesS, 1);
                fecha1 = fecha1.AddMonths(1);
                fecha1 = fecha1.AddDays(-1);


              //  fecha1 = fecha1.AddMonths(-1);
                dateTimePicker2.Text = Convert.ToString(fecha1);
              //  fecha = fecha.AddMonths(-1);
                dateTimePicker1.Text = Convert.ToString(fecha);

                this.quincena = "SEGUNDA";
                this.del = Convert.ToInt32(fecha.Day);
                this.al = Convert.ToInt32(fecha1.Day);
                this.anio = Convert.ToInt32(fecha.Year);
                this.mesA = Convert.ToString(fecha.Month);
                if (mesA == "1") this.mesLetra="ENERO";
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
    }
}
