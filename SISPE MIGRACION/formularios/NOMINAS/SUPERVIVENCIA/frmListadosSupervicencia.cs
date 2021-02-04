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
    public partial class frmListadosSupervicencia : Form
    {
        public frmListadosSupervicencia()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar el listado correspondiente?","Aviso",globales.menuPrincipal);
            if (dialogo == DialogResult.No)
                return;


            string whereFirma = "";

            if (rdPresencial.Checked) {
                whereFirma = " and ssu.firma = 1 ";
            }else if (rdCorreo.Checked)
            {
                whereFirma = " and ssu.firma = 2 ";
            }
            else if (rdVoz.Checked)
            {
                whereFirma = " and ssu.firma = 3 ";
            }
            else if (rdVisita.Checked)
            {
                whereFirma = " and ssu.firma = 4 ";
            }

            string query = "";

            string titulo2 = string.Empty;

            int periodo = 0;
            int periodoSiguiente = 0;
            int periodoanterior = 0;
            if (rdPrimer.Checked)
            {
                titulo2 = "SUPERVIVENCIA EN EL PERIODO DEL 01 AL 21 DE ENERO DEL "+txtAño.Text;
                periodoanterior = 3;
                periodo = 1;
                periodoSiguiente = 2;
            }
            else if (rdSegundo.Checked)
            {
                titulo2 = "SUPERVIVENCIA EN EL PERIODO DEL 01 AL 21 DE MAYO DEL " + txtAño.Text;
                periodoanterior = 1;
                periodo = 2;
                periodoSiguiente = 3;
            }
            else {
                titulo2 = "SUPERVIVENCIA EN EL PERIODO DEL 01 AL 21 DE SEPTIEMBRE DEL " + txtAño.Text;
                periodoanterior = 2;
                periodo = 3;
                periodoSiguiente = 1;
            }


            query = "select * from nominas_catalogos.periodo where clave = "+periodo;
            List<Dictionary<string,object>> r1 = globales.consulta(query);
            int diainicial = Convert.ToInt32(r1[0]["dia_inicial"]);
            int diafinal = Convert.ToInt32(r1[0]["dia_final"]);
            int mes = Convert.ToInt32(r1[0]["mes"]);

            DateTime dtFecha1 = new DateTime(Convert.ToInt32(txtAño.Text),mes,diainicial);
            DateTime dtFecha2 = new DateTime(Convert.ToInt32(txtAño.Text), mes, diafinal);

            string fecha1 = string.Format("{0:yyyy-MM-dd}",dtFecha1);
            string fecha2 = string.Format("{0:yyyy-MM-dd}", dtFecha2);


            string titulo1 = string.Empty;
            if (rd1.Checked)
            {

                titulo1 = "RELACIÓN DE JUBILADOS QUE SE PRESENTARON A FIRMAR";

                query = "select mms.jpp,mms.num,mms.nombre,ssu.fecha,'' as extemporaneo from nominas_catalogos.maestro mms " +
                    " inner JOIN " +
                    " nominas_catalogos.supervive ssu on ssu.jpp = mms.jpp and ssu.numjpp = mms.num  " +
                    $" where ssu.periodo = {periodo} and ssu.anio = {txtAño.Text} and (mms.superviven = 'S' or mms.superviven = 'N')  " +
                    $" and mms.jpp <> 'PEA' and fecha BETWEEN '{fecha1}' and '{fecha2}' {whereFirma} order by jpp , num ";
            }
            else if (rd2.Checked)
            {
                titulo1 = "RELACIÓN DE JUBILADOS QUE SE PRESENTARON A FIRMAR FUERA DEL PERIODO";
                query = "select * from nominas_catalogos.periodo where clave = " + periodoSiguiente;
                r1 = globales.consulta(query);
                int diainicialSiguiente = Convert.ToInt32(r1[0]["dia_inicial"]);
                int diafinalSiguiente = Convert.ToInt32(r1[0]["dia_final"]);
                int messiguiente = Convert.ToInt32(r1[0]["mes"]);

                int añosiguiente = Convert.ToInt32(txtAño.Text);
                if (periodoSiguiente == 1) añosiguiente++;

                DateTime dtFecha1Siguiente = new DateTime(Convert.ToInt32(añosiguiente), messiguiente, diainicialSiguiente);
                dtFecha1Siguiente = dtFecha1Siguiente.AddDays(-1);

                string fecha1siguiente = string.Format("{0:yyyy-MM-dd}", dtFecha1Siguiente);
                string fecha2aux = string.Format("{0:yyyy-MM-dd}", dtFecha2);
                dtFecha2 = dtFecha2.AddDays(1);
                fecha2 = string.Format("{0:yyyy-MM-dd}", dtFecha2);


                query = $"select mms.jpp,mms.num,mms.nombre,ssu.fecha,(CASE WHEN ssu.fecha>'{fecha2aux}' THEN 'EXTEMPORANEO' END) as extemporaneo from nominas_catalogos.maestro mms " +
                   " inner JOIN " +
                   " nominas_catalogos.supervive ssu on ssu.jpp = mms.jpp and ssu.numjpp = mms.num  " +
                   $" where ssu.periodo = {periodo} and ssu.anio = {txtAño.Text} and (mms.superviven = 'S' or mms.superviven = 'N')  " +
                   $" and mms.jpp <> 'PEA' and fecha BETWEEN '{fecha2}' and '{fecha1siguiente}' {whereFirma} order by jpp , num ";

            }
            else if (rd3.Checked)
            {
                titulo1 = "RELACIÓN DE JUBILADOS QUE SE PRESENTARON A FIRMAR EN EL PERIODO Y EXTEMPORANEOS";
                query = "select * from nominas_catalogos.periodo where clave = " + periodoSiguiente;
                r1 = globales.consulta(query);
                int diainicialSiguiente = Convert.ToInt32(r1[0]["dia_inicial"]);
                int diafinalSiguiente = Convert.ToInt32(r1[0]["dia_final"]);
                int messiguiente = Convert.ToInt32(r1[0]["mes"]);

                int añosiguiente = Convert.ToInt32(txtAño.Text);
                if (periodoSiguiente == 1) añosiguiente++;

                DateTime dtFecha1Siguiente = new DateTime(Convert.ToInt32(añosiguiente), messiguiente, diainicialSiguiente);
                dtFecha1Siguiente = dtFecha1Siguiente.AddDays(-1);

                string fecha1siguiente = string.Format("{0:yyyy-MM-dd}", dtFecha1Siguiente);




                query = $"select mms.jpp,mms.num,mms.nombre,ssu.fecha,(CASE WHEN ssu.fecha>'{fecha2}' THEN 'EXTEMPORANEO' END) as extemporaneo from nominas_catalogos.maestro mms " +
                   " inner JOIN " +
                   " nominas_catalogos.supervive ssu on ssu.jpp = mms.jpp and ssu.numjpp = mms.num  " +
                   $" where ssu.periodo = {periodo} and ssu.anio = {txtAño.Text} and (mms.superviven = 'S' or mms.superviven = 'N')  " +
                   $" and mms.jpp <> 'PEA' and fecha BETWEEN '{fecha1}' and '{fecha1siguiente}' {whereFirma} order by jpp , num ";
            }
            else if (rd4.Checked)
            {
                titulo1 = "RELACIÓN DE JUBILADOS QUE NO SE PRESENTARON A FIRMAR EN EL PERIODO";
                query = "select * from nominas_catalogos.periodo where clave = " + periodoSiguiente;
                r1 = globales.consulta(query);
                int diainicialSiguiente = Convert.ToInt32(r1[0]["dia_inicial"]);
                int diafinalSiguiente = Convert.ToInt32(r1[0]["dia_final"]);
                int messiguiente = Convert.ToInt32(r1[0]["mes"]);

                int añosiguiente = Convert.ToInt32(txtAño.Text);
                if (periodoSiguiente == 1) añosiguiente++;

                DateTime dtFecha1Siguiente = new DateTime(Convert.ToInt32(añosiguiente), messiguiente, diainicialSiguiente);
                dtFecha1Siguiente = dtFecha1Siguiente.AddDays(-1);

                string fecha1siguiente = string.Format("{0:yyyy-MM-dd}", dtFecha1Siguiente);
                string fecha2aux = string.Format("{0:yyyy-MM-dd}", dtFecha2);
                dtFecha2 = dtFecha2.AddDays(1);
                fecha2 = string.Format("{0:yyyy-MM-dd}", dtFecha2);


                query = $"SELECT mms.jpp,	mms.num,mms.nombre,ssu.fecha,(CASE WHEN ssu.fecha >= '{fecha2}' THEN 'EXTEMPORANEO' END) AS extemporaneo" +
                    " FROM nominas_catalogos.maestro mms LEFT JOIN nominas_catalogos.supervive ssu ON ssu.jpp = mms.jpp AND ssu.numjpp = mms.num and  " +
                    $" ssu.periodo = {periodo} and (ssu.fecha BETWEEN '{fecha1}' AND '{fecha1siguiente}') WHERE (mms.superviven = 'S'OR mms.superviven = 'N') " +
                    $" AND mms.jpp <> 'PEA' and ( (ssu.fecha BETWEEN '{fecha2}' AND '{fecha1siguiente}') or fecha is null) ORDER BY	jpp,	num ";


            }
            else if (rd5.Checked) {
                titulo1 = "RELACIÓN DE JUBILADOS QUE FIRMARON Y NO FIRMARON EN EL PERIODO";
                query = "select * from nominas_catalogos.periodo where clave = " + periodoSiguiente;
                r1 = globales.consulta(query);
                int diainicialSiguiente = Convert.ToInt32(r1[0]["dia_inicial"]);
                int diafinalSiguiente = Convert.ToInt32(r1[0]["dia_final"]);
                int messiguiente = Convert.ToInt32(r1[0]["mes"]);

                int añosiguiente = Convert.ToInt32(txtAño.Text);
                if (periodoSiguiente == 1) añosiguiente++;

                DateTime dtFecha1Siguiente = new DateTime(Convert.ToInt32(añosiguiente), messiguiente, diainicialSiguiente);
                dtFecha1Siguiente = dtFecha1Siguiente.AddDays(-1);

                string fecha1siguiente = string.Format("{0:yyyy-MM-dd}", dtFecha1Siguiente);
                string fecha2aux = string.Format("{0:yyyy-MM-dd}", dtFecha2);
                dtFecha2 = dtFecha2.AddDays(1);
                fecha2 = string.Format("{0:yyyy-MM-dd}", dtFecha2);


                query = $" SELECT	mms.jpp,	mms.num,	mms.nombre,	ssu.fecha,	(		CASE		WHEN ssu.fecha >= '{fecha2}' THEN			'EXTEMPORANEO'		END" +
                    " ) AS extemporaneo FROM	nominas_catalogos.maestro mms left JOIN  nominas_catalogos.supervive ssu ON ssu.jpp = mms.jpp " +
                    $" AND ssu.numjpp = mms.num WHERE	(ssu.periodo = {periodo} or ssu.periodo is null) AND (ssu.anio = {txtAño.Text} or ssu.anio is null) AND ( " +
                    $" mms.superviven = 'S' 	OR mms.superviven = 'N') AND mms.jpp <> 'PEA' AND (fecha BETWEEN '{fecha1}' AND '{fecha1siguiente}' or fecha is null) " +
                    " AND (ssu.firma = 1 or ssu.firma is null) ORDER BY	jpp,	num ";
            } else if (rd6.Checked) {
                titulo1 = "RELACIÓN DE JUBILADOS QUE NO SE ASISTIERON A LA FIRMA SUPERVIVENCIA DEL PERIODO";
                query = "select * from nominas_catalogos.periodo where clave = " + periodoSiguiente;
                r1 = globales.consulta(query);
                int diainicialSiguiente = Convert.ToInt32(r1[0]["dia_inicial"]);
                int diafinalSiguiente = Convert.ToInt32(r1[0]["dia_final"]);
                int messiguiente = Convert.ToInt32(r1[0]["mes"]);

                int añosiguiente = Convert.ToInt32(txtAño.Text);
                if (periodoSiguiente == 1) añosiguiente++;

                DateTime dtFecha1Siguiente = new DateTime(Convert.ToInt32(añosiguiente), messiguiente, diainicialSiguiente);
                dtFecha1Siguiente = dtFecha1Siguiente.AddDays(-1);

                string fecha1siguiente = string.Format("{0:yyyy-MM-dd}", dtFecha1Siguiente);
                string fecha2aux = string.Format("{0:yyyy-MM-dd}", dtFecha2);
                dtFecha2 = dtFecha2.AddDays(1);
                fecha2 = string.Format("{0:yyyy-MM-dd}", dtFecha2);

                query = $"create temp table tt1 as select nn1.JPP,mma.num as num,mma.nombre from nominas_catalogos.respaldos_nominas nn1 inner join nominas_catalogos.maestro mma on mma.jpp = nn1.jpp and mma.num = nn1.numjpp where nn1.archivo = '2001' and nn1.jpp <> 'PEA' GROUP BY nn1.JPP,mma.num,mma.nombre;" +
                           "create temp table universo as SELECT	tt1.jpp,	tt1.num,tt1.nombre,	ssu.fecha,	(		CASE		WHEN ssu.fecha >= '2020-01-22' THEN			'EXTEMPORANEO'		END	) AS extemporaneo FROM	tt1 LEFT JOIN nominas_catalogos.supervive ssu ON ssu.jpp = tt1.jpp AND ssu.numjpp = tt1.num AND ( 	ssu.fecha BETWEEN '2020-01-01' 	AND '2020-04-30' );" +
                           " select * from universo where (fecha is null)  order by jpp,num  ";

                //query = $"SELECT mms.jpp,	mms.num,mms.nombre,ssu.fecha,(CASE WHEN ssu.fecha >= '{fecha2}' THEN 'EXTEMPORANEO' END) AS extemporaneo" +
                //    " FROM nominas_catalogos.maestro mms LEFT JOIN nominas_catalogos.supervive ssu ON ssu.jpp = mms.jpp AND ssu.numjpp = mms.num and  " +
                //    $" ssu.periodo = {periodo} and (ssu.fecha BETWEEN '{fecha1}' AND '{fecha1siguiente}') WHERE (mms.superviven = 'S'OR mms.superviven = 'N') " +
                //    $" AND mms.jpp <> 'PEA' and ( (ssu.fecha BETWEEN '{fecha2}' AND '{fecha1siguiente}') or fecha is null) AND (CASE WHEN ssu.fecha >= '2019-09-22' THEN 'EXTEMPORANEO' END) IS NULL ORDER BY	jpp,	num ";

            }




            List<Dictionary<string, object>> resultado = globales.consulta(query);
            int contador = 0;
            int pagina = 1;

            object[] objeto = new object[resultado.Count];
            foreach (Dictionary<string,object> item in resultado) {

                string numero = ( contador  ).ToString();
                string jpp = Convert.ToString(item["jpp"]);
                string numjpp = Convert.ToString(item["num"]);
                string nombre = Convert.ToString(item["nombre"]);
                string extemporaneos = Convert.ToString(item["extemporaneo"]);
                string fecha = globales.parseDateTime(globales.convertDatetime(Convert.ToString(item["fecha"])));
                

                object[] tt1 = { jpp,numjpp,nombre,fecha,extemporaneos,pagina};
                objeto[contador] = tt1;
                pagina++;
                contador++;


            }

            object[][] parametros = new object[2][];
            object[] header = { "titulo1","titulo2" };
            object[] body = { titulo1,titulo2  };

            parametros[0] = header;
            parametros[1] = body;

            globales.reportes("nominasListadoSupervivencia", "supervivencia",objeto,"",false,parametros);


        }

        private void frmListadosSupervicencia_Load(object sender, EventArgs e)
        {

            maskedTextBox1.Text = string.Format("{0:d}",DateTime.Now);
            maskedTextBox2.Text = string.Format("{0:d}", DateTime.Now);

            txtAño.Text = DateTime.Now.Year.ToString();
            DateTime ahora = DateTime.Now;


            string query = $"select clave from nominas_catalogos.periodo where  mes <= {ahora.Month} order by clave desc limit 1";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            Dictionary<string, object> diccionario;
            if (resultado.Count != 0) {
                diccionario = resultado[0];

                int clave = globales.convertInt(Convert.ToString(diccionario["clave"]));
                switch (clave) {
                    case 1:
                        rdPrimer.Checked = true;
                        break;
                    case 2:
                        rdSegundo.Checked = true;
                        break;
                    case 3:
                        rdTercero.Checked = true;
                        break;
                    default:
                        break;
                }
            }






            
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                maskedTextBox1.Visible = true;
                label3.Visible = true;
                button4.Visible = true;
                maskedTextBox2.Visible = true;
            }
            else
            {
                maskedTextBox1.Visible = false;
                label3.Visible = false;
                maskedTextBox2.Visible = false;
                button4.Visible = false;
            }
        }

        private void maskedTextBox1_Enter(object sender, EventArgs e)
        {
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                maskedTextBox2.Focus();

            }
        }

        private void maskedTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar el listado correspondiente?", "Aviso", globales.menuPrincipal);
            if (dialogo == DialogResult.No)
                return;
            try
            {

                string whereFirma = "";

                if (rdPresencial.Checked)
                {
                    whereFirma = " and ssu.firma = 1 ";
                }
                else if (rdCorreo.Checked)
                {
                    whereFirma = " and ssu.firma = 2 ";
                }
                else if (rdVoz.Checked)
                {
                    whereFirma = " and ssu.firma = 3 ";
                }
                else if (rdVisita.Checked)
                {
                    whereFirma = " and ssu.firma = 4 ";
                }

                string query = "";

                string titulo2 = string.Empty;

                int periodo = 0;
                int periodoSiguiente = 0;
                int periodoanterior = 0;
                if (rdPrimer.Checked)
                {
                    titulo2 = "SUPERVIVENCIA EN EL PERIODO DEL 01 AL 21 DE ENERO DEL " + txtAño.Text;
                    periodoanterior = 3;
                    periodo = 1;
                    periodoSiguiente = 2;
                }
                else if (rdSegundo.Checked)
                {
                    titulo2 = "SUPERVIVENCIA EN EL PERIODO DEL 01 AL 21 DE MAYO DEL " + txtAño.Text;
                    periodoanterior = 1;
                    periodo = 2;
                    periodoSiguiente = 3;
                }
                else
                {
                    titulo2 = "SUPERVIVENCIA EN EL PERIODO DEL 01 AL 21 DE SEPTIEMBRE DEL " + txtAño.Text;
                    periodoanterior = 2;
                    periodo = 3;
                    periodoSiguiente = 1;
                }


                query = "select * from nominas_catalogos.periodo where clave = " + periodo;
                List<Dictionary<string, object>> r1 = globales.consulta(query);
                int diainicial = Convert.ToInt32(r1[0]["dia_inicial"]);
                int diafinal = Convert.ToInt32(r1[0]["dia_final"]);
                int mes = Convert.ToInt32(r1[0]["mes"]);



                string fecha1 = string.Format("{0:yyyy-MM-dd}", maskedTextBox1.Text);
                string fecha2 = string.Format("{0:yyyy-MM-dd}", maskedTextBox2.Text);


                titulo2 = $"LISTADO DE SUPERVIVENCIA DE LA FECHA {fecha1} A {fecha2}";


                string titulo1 = string.Empty;
                if (rd1.Checked)
                {

                    titulo1 = "RELACIÓN DE JUBILADOS QUE SE PRESENTARON A FIRMAR";

                    //query = "select mms.jpp,mms.num,mms.nombre,ssu.fecha,'' as extemporaneo from nominas_catalogos.maestro mms " +
                    //    " inner JOIN " +
                    //    " nominas_catalogos.supervive ssu on ssu.jpp = mms.jpp and ssu.numjpp = mms.num  " +
                    //    $" where ssu.periodo = {periodo} and ssu.anio = {txtAño.Text} and (mms.superviven = 'S' or mms.superviven = 'N')  " +
                    //    $" and mms.jpp <> 'PEA' and fecha BETWEEN '{fecha1}' and '{fecha2}' {whereFirma} order by jpp , num ";


                    query = "select mms.jpp,mms.num,mms.nombre,ssu.fecha,'' as extemporaneo from nominas_catalogos.maestro mms " +
                        " inner JOIN " +
                        " nominas_catalogos.supervive ssu on ssu.jpp = mms.jpp and ssu.numjpp = mms.num  " +
                        $" where ssu.periodo = {periodo} and ssu.anio = {txtAño.Text} and (mms.superviven = 'S' or mms.superviven = 'N')  " +
                        $" and mms.jpp <> 'PEA' and fecha BETWEEN '{fecha1}' and '{fecha2}' {whereFirma}  order by jpp , num ";
                }

                List<Dictionary<string, object>> resultado = globales.consulta(query);
                int contador = 0;
                int contadorPagina = 1;

                object[] objeto = new object[resultado.Count];
                bool primero = true;
                string jppstr = string.Empty;
                foreach (Dictionary<string, object> item in resultado)
                {
                    if (primero) {
                        primero = false;
                        jppstr = Convert.ToString(item["jpp"]);
                    }



                    string numero = (contadorPagina).ToString();
                    string jpp = Convert.ToString(item["jpp"]);
                    string numjpp = Convert.ToString(item["num"]);
                    string nombre = Convert.ToString(item["nombre"]);
                    string extemporaneos = Convert.ToString(item["extemporaneo"]);
                    string fecha = globales.parseDateTime(globales.convertDatetime(Convert.ToString(item["fecha"])));

                    if (jpp != jppstr) {
                        jppstr = jpp;
                        contadorPagina = 1;
                        numero = "1";
                    }

                    object[] tt1 = { jpp, numjpp, nombre, fecha, extemporaneos, numero };
                    objeto[contador] = tt1;

                    contador++;
                    contadorPagina++;


                }


                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                object[][] parametros = new object[2][];
                object[] header = { "titulo1", "titulo2", "logo" };
                object[] body = { titulo1, titulo2 , logo };

                parametros[0] = header;
                parametros[1] = body;

                globales.reportes("nominasListadoSupervivencia", "supervivencia", objeto, "", false, parametros);
            }
            catch(Exception er)
            {
                DialogResult dialog = globales.MessageBoxExclamation("OCURRIO UN ERROR, SOLICITAR SOPORTE ", "UPS", globales.menuPrincipal);
                return;
            }
        }
    }
}
