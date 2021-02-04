using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.SUPERVIVENCIA
{
    public partial class frmDebePersonas : Form
    {
        public frmDebePersonas()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmDebePersonas_Load(object sender, EventArgs e)
        {
            cargando("");

        }

        private void cargando(string where)
        {
            //Se obtiene el periodo actual 

            int periodo = 0;
            int periodoanterior = 0;
            int añoActual = DateTime.Now.Year;
            int añoAnterior = 0;

            string mes = "";

            if (DateTime.Now.Month >= 1 && DateTime.Now.Month < 5)
            {
                periodo = 1;
                periodoanterior = 3;
                añoAnterior = añoActual - 1;
                mes = "09";
            }
            else if (DateTime.Now.Month >= 5 && DateTime.Now.Month < 9)
            {
                periodo = 2;
                periodoanterior = 1;
                añoAnterior = añoActual;
                mes = "01";
            }
            else
            {
                periodo = 3;
                periodoanterior = 2;
                añoAnterior = añoActual;
                mes = "05";
            }

            string fechaExtemporaneaAnterior = $"{añoAnterior}-{mes}-21";

            //Se obtiene la lista de las personas que firmaron en la supervivencia actual pero no en la pasada
            string query = " create temp table p11 as  " +
                $" select jpp,numjpp from nominas_catalogos.supervive where anio = {añoActual} and periodo = {periodo} " +
                " except " +
                $" select jpp,numjpp from nominas_catalogos.supervive where anio = {añoAnterior} and periodo = {periodoanterior}; ";

            query += "create temp table p1 as select p11.*,ss1.fecha as fecha_ultimasupervivencia from p11 inner join nominas_catalogos.supervive ss1 ON" +
                      $" ss1.jpp = p11.jpp and ss1.numjpp = p11.numjpp and anio = '{añoActual}' and periodo = {periodo}; ";
            // Se obtiene el nombre,rfc,fecha de captura,fecha de ingreso de jubilado,pensionado o pensionista

            query += " create temp table p2 as select p1.*,mma.rfc,mma.nombre,mma.fching,mma.f_captura,mma.superviven from p1 inner join nominas_catalogos.maestro mma on mma.jpp = p1.jpp and mma.num = p1.numjpp WHERE superviven = 'S' ;";

            //Se saca dos tipos de listados, primero los que tienen fecha y los segundos son los que no tienen fecha

            query += " create temp table listado1 as select * from p2 where f_captura <> ''; ";
            query += " create temp table listado2 as select * from p2 where f_captura = '' ;";

            // Del listado que contiene fecha se destierran todos aquellos que su fecha de captura es despues a la firma de supervivencia 
            query += $" create temp table listado1final as select * from listado1 where f_captura::date <= '{fechaExtemporaneaAnterior}'::date; ";

            //Uno los dos listados

            query += " create temp table listadofinal as select * from listado1final union select * from listado2; ";

            //Se verifica su ultima supervivencia

            query += " create temp table ultima as select listadofinal.*,max(ss1.fecha) as fechasupervivencia from listadofinal left join nominas_catalogos.supervive ss1 " +
                $" on ss1.jpp = listadofinal.jpp and ss1.numjpp = listadofinal.numjpp and ss1.anio <> {añoActual} and ss1.periodo <> {periodo}  " +
                " group by listadofinal.jpp,listadofinal.numjpp,listadofinal.rfc,listadofinal.nombre,listadofinal.fching,listadofinal.f_captura,listadofinal.superviven,fecha_ultimasupervivencia " +
                " order by listadofinal.jpp,listadofinal.numjpp ;";

            query += $" select ultima.*,ss1.periodo,ss1.anio from ultima left join nominas_catalogos.supervive ss1 on ss1.jpp = ultima.jpp and ss1.numjpp = ultima.numjpp and ss1.fecha = ultima.fechasupervivencia {where}";

            List<Dictionary<string, object>> resultado = globales.consulta(query);


            string fecha = string.Empty;
            dtggrid.Rows.Clear();
            foreach (Dictionary<string, object> o in resultado)
            {
                if (globales.convertInt(Convert.ToString(o["periodo"])) == 1)
                {
                    fecha = $"01 al 21 ENERO DEL {o["anio"]} ";
                }
                else if (globales.convertInt(Convert.ToString(o["periodo"])) == 2)
                {
                    fecha = $"01 al 21 MAYO DEL {o["anio"]} ";
                }
                else
                {
                    fecha = $"01 al 21 SEPTIEMBRE DEL {o["anio"]} ";
                }
                dtggrid.Rows.Add(o["jpp"], o["numjpp"], o["rfc"], o["nombre"], o["fching"], fecha, o["periodo"], o["anio"]);
            }
        }

        private void frmDebePersonas_LocationChanged(object sender, EventArgs e)
        {

        }

        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            int periodo = Convert.ToInt32(dtggrid.Rows[e.RowIndex].Cells[6].Value);
            int anio = Convert.ToInt32(dtggrid.Rows[e.RowIndex].Cells[7].Value);

            int anioSiguiente = 0;

            DateTime fecha1 = new DateTime();
            DateTime fecha2 = new DateTime();

            if (periodo == 1)
            {

                anioSiguiente = anio;
                fecha1 = new DateTime(anio,06,01);
                fecha2 = new DateTime(anio,06,30);
            }
            else if (periodo == 2)
            {
                anioSiguiente = anio;
                fecha1 = new DateTime(anio, 10, 01);
                fecha2 = new DateTime(anio, 10, 31);
            }
            else {
                anioSiguiente = anio+1;
                fecha1 = new DateTime(anio, 02, 01);
                fecha1 = new DateTime(anio, 02, 28);
            }


            bool primero = true;


          


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = $" where fecha_ultimasupervivencia between '{string.Format("{0:yyyy-MM-dd}",dateTimePicker1.Value)}' and '{string.Format("{0:yyyy-MM-dd}", dateTimePicker2.Value)}'";
            cargando(query);
        }
    }
}
