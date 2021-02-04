using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA
{
    public partial class frmElaboracionNomina : Form
    {
        string mes = string.Empty;
        string archivo = string.Empty;
        public object[] aux2;
        public  string compara = string.Empty;

        public frmElaboracionNomina()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
        //por 1
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar la nomina?", "Aviso", globales.menuPrincipal);
            if (dialogo == DialogResult.No)
                return;
            string jpp = string.Empty;
            string jppdescripcion = string.Empty;
            mes = seleccionarMes();



            if (jubilados.Checked)
            {
                jpp = "JUB";
                jppdescripcion = "JUBILADOS";
            }
            else if (pensionado.Checked)
            {
                jpp = "PDO";
                jppdescripcion = "PENSIONADOS";
            }
            else if (pensionistas.Checked)
            {
                jpp = "PTA";
                jppdescripcion = "PENSIONISTAS";
            }
            else if (alimenticia.Checked)
            {
                jpp = "PEA";
                jppdescripcion = "PENSION ALIMENTICIA";
            }
            string query = string.Empty;

            string tipo_nomina = string.Empty;
            string titulo = "OFICINA DE PENSIONES DEL ESTADO DE OAXACA";

            if (radioButton1.Checked)
            {

                if (jubilados.Checked)
                {
                    jpp = "JUB";
                    tipo_nomina = "AG";
                }
                else if (pensionado.Checked)
                {
                    jpp = "PDO";
                    tipo_nomina = "AG";

                }
                else if (pensionistas.Checked)
                {
                    jpp = "PTA";
                    tipo_nomina = "AG";

                }
                else if (alimenticia.Checked)
                {
                    jpp = "PEA";
                    tipo_nomina = "AG";

                }

            }
            if (radioButton2.Checked) tipo_nomina = "CA";
            if (radioButton3.Checked) tipo_nomina = "DM";
            if (radioButton4.Checked) tipo_nomina = "UT";
            if (radioButton5.Checked) tipo_nomina = "CAN2"; //PDOPTA
            if (radioButton6.Checked) tipo_nomina = "NR";
            if (rbExtraordinaria.Checked) tipo_nomina = "E";
            if (radioButton7.Checked) tipo_nomina = "PF";



            if (groupBox2.Visible == false) tipo_nomina = "N";

            switch (tipo_nomina)
            {
                case "DM":
                    titulo = $"SECRETARIA DE ADMINISTRACION\nRESUMEN CONTABLE: NOMINA ELECTRONICA DEL DIA DE LAS MADRES CORRESPONDIENTE AL EJERCICIO {txtAño.Text}\n";
                    //  $"PROYECTO 106{txtproyecto.text}";
                    break;
                case "AG":
                    titulo = $"OFICINA DE PENSIONES\nRESUMEN CONTABLE: NOMINA ELECTRONICA PARA EL PAGO  DE AGUINALDO PARA {jppdescripcion} CORRESPONDIENTE DEL 01 DE ENERO AL 31 DE OCTUBRE DEL 2020\n";
                    break;
                case "NR":
                    titulo = $"OFICINA DE PENSIONES\nRESUMEN CONTABLE: NOMINA ELECTRONICA PARA EL PAGO  DE RETROACTIVO PARA {jppdescripcion} CORRESPONDIENTE DEL 01 DE ENERO AL 31 DE OCTUBRE DEL 2020\n";
                    break;

                case "CA":
                    titulo = $"SECRETARIA DE ADMINISTRACION\nRESUMEN CONTABLE: NOMINA ELECTRONICA DE CANASTA NAVIDEÑA CORRESPONDIENTE AL EJERCICIO {txtAño.Text}\n";
                    //  $"PROYECTO 106{txtproyecto.text}";
                    break;
                case "CAN2":
                    titulo = $"SECRETARIA DE ADMINISTRACION\nRESUMEN CONTABLE: NOMINA ELECTRONICA DE CANASTA NAVIDEÑA CORRESPONDIENTE AL EJERCICIO {txtAño.Text}\n";
                    //  $"PROYECTO 106{txtproyecto.text}";
                    break;
                case "E":
                    titulo = $"NOMINA PARA DAR CUMPLIMIENTO AL ACUERDO DEL CONSEJO DIRECTIVO DE LA OFICINA DE PENSIONES DEL ESTADO DE OAXACA, DE FECHA 04 DE FEBRERO DEL 2010."; //APUNTE
                    //  $"PROYECTO 106{txtproyecto.text}";
                    break;
             
            }

            if (chknomina.Checked)
            {
                query = "SELECT	CONCAT(a1.jpp,a1.num) as proyecto,	a1.nombre,	a1.curp,	a1.rfc,	a1.imss,	a1.categ,	a2.clave,	a2.descri,	a2.monto,	a2.pagon, " +
             " a2.pagot,	a2.leyen FROM	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp and a1.jpp = a2.jpp WHERE " +
             $" a1.superviven = 'S' AND a1.jpp = a2.jpp AND a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}' ORDER BY 	a1.jpp,a1.num,a2.clave asc ";
            }
            else
            {
                query = "SELECT	CONCAT(a1.jpp,a1.num) as proyecto,	a1.nombre,	a1.curp,	a1.rfc,	a1.imss,	a1.categ,	a2.clave,	a2.descri,	a2.monto,	a2.pagon, " +
             " a2.pagot,	a2.leyen FROM	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp and a1.jpp = a2.jpp WHERE " +
             $" a1.superviven in  ('S') AND a1.jpp = a2.jpp AND a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}' ORDER BY 	a1.jpp,a1.num,a2.clave asc ";
            }



            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0)
            {
                DialogResult dialog = globales.MessageBoxExclamation("NO SE ENCUENTRA CARGADA UNA NÓMINA ESPECIAL", "UPS", globales.menuPrincipal);
                return;
            }

            string que = $"CREATE TEMP TABLE BASE AS  (SELECT CONCAT (a1.jpp, a1.num) AS proyecto,a1.nombre,	a1.curp,	a1.rfc,	a1.imss,	a1.categ,	a2.clave,	a2.descri,	a2.monto,	a2.pagon,	 a2.pagot,	a2.leyen FROM	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp AND a1.jpp = a2.jpp WHERE  a1.superviven = 'S' AND a1.jpp = a2.jpp AND a2.tipo_nomina='{tipo_nomina}' ORDER BY a1.jpp, a1.num,a2.clave);"
                + "CREATE temp table per as SELECT proyecto , sum(monto) FROM BASE WHERE CLAVE >= 59  GROUP BY proyecto;" +
              " CREATE temp table ded as SELECT proyecto , sum(monto) FROM BASE WHERE CLAVE <= 60  GROUP BY proyecto;" +
            "  CREATE TEMP TABLE liquidos as select a1.proyecto, (a2.sum - a1.sum) as liquido from per a1 JOIN ded a2 on a1.proyecto = a2.proyecto order by a1.proyecto;" +
          "  select* from liquidos where liquido <= 1500 order by liquido asc; ";

            List<Dictionary<string, object>> liquidos = globales.consulta(que);

            if (liquidos.Count >= 1)

            {
                DialogResult dialo = globales.MessageBoxExclamation("HAY LIQUIDOS MENORES AL LIMITE, GENERANDO REPORTE", "VERIFICAR", globales.menuPrincipal);
                object[] aux1 = new object[liquidos.Count];
                int contador1 = 0;
                foreach (var teim in liquidos)
                {
                    string proyecto = Convert.ToString(teim["proyecto"]);
                    string liquido = Convert.ToString(teim["liquido"]);
                    object[] tt1 = { proyecto, liquido };
                    aux1[contador1] = tt1;
                    contador1++;

                }

                object[] parametros1 = { "vacio" };
                object[] valor1 = { "" };
                object[][] enviarParametros1 = new object[2][];

                enviarParametros1[0] = parametros1;
                enviarParametros1[1] = valor1;

                globales.reportes("valida_nomina", "va_nom", aux1, "", false, enviarParametros1);
                this.Cursor = Cursors.Default;

            }

                string valida_clave82 = $"select jpp, numjpp, count(*) as conteo from nominas_catalogos.nominew where clave in  (82) and tipo_nomina='N' and jpp <>'E' group by jpp, numjpp HAVING count(*) > 1";
                List<Dictionary<string, object>> listarev = globales.consulta(valida_clave82);
                object[] aux150 = new object[listarev.Count];
                if (listarev.Count > 1)
                {

                    int contador9 = 0;


                    foreach (var item1 in listarev)
                    {
                        string jppV = Convert.ToString(item1["jpp"]);
                        string numV = Convert.ToString(item1["numjpp"]);
                        object[] tt150 = { jpp, numV };
                        aux150[contador9] = tt150;

                        contador9++;


                    }

                    object[] parame = { "vacio" };
                    object[] val = { "" };
                    object[][] enviarPara = new object[2][];

                    enviarPara[0] = parame;
                    enviarPara[1] = val;

                    globales.reportes("valida_clave82", "verifica", aux150, "", false, enviarPara);
                    this.Cursor = Cursors.Default;

                }






            string valida_clave83 = $"select jpp, numjpp, count(*) as conteo from nominas_catalogos.nominew where clave in  (83) and tipo_nomina='N' and jpp <>'E' group by jpp, numjpp HAVING count(*) > 1";
            List<Dictionary<string, object>> lista83 = globales.consulta(valida_clave82);
            object[] aux83 = new object[lista83.Count];
            if (lista83.Count > 1)
            {

                int contador10 = 0;


                foreach (var item1 in listarev)
                {
                    string jppV = Convert.ToString(item1["jpp"]);
                    string numV = Convert.ToString(item1["numjpp"]);
                    object[] tt83 = { jpp, numV };
                    aux83[contador10] = tt83;

                    contador10++;


                }

                object[] parame = { "vacio" };
                object[] val = { "" };
                object[][] enviarPara = new object[2][];

                enviarPara[0] = parame;
                enviarPara[1] = val;

                globales.reportes("valida_clave82", "verifica", aux83, "", false, enviarPara);
                this.Cursor = Cursors.Default;

            }





            string valida_clave85 = $"select jpp, numjpp, count(*) as conteo from nominas_catalogos.nominew where clave in  (85) and tipo_nomina='N' and jpp <>'E' group by jpp, numjpp HAVING count(*) > 1";
            List<Dictionary<string, object>> lista85 = globales.consulta(valida_clave82);
            object[] aux85 = new object[lista85.Count];
            if (lista85.Count > 1)
            {

                int contador11 = 0;


                foreach (var item1 in listarev)
                {
                    string jppV = Convert.ToString(item1["jpp"]);
                    string numV = Convert.ToString(item1["numjpp"]);
                    object[] tt85 = { jpp, numV };
                    aux85[contador11] = tt85;

                    contador11++;


                }

                object[] parame = { "vacio" };
                object[] val = { "" };
                object[][] enviarPara = new object[2][];

                enviarPara[0] = parame;
                enviarPara[1] = val;

                globales.reportes("valida_clave82", "verifica", aux85, "", false, enviarPara);
                this.Cursor = Cursors.Default;

            }






            string quer = string.Empty;
            if (chknomina.Checked)

            {
                quer = $"SELECT DISTINCT(rfc),a1.num FROM 	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp AND a1.jpp = a2.jpp WHERE 	a1.superviven = 'S' AND a1.jpp = a2.jpp AND a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}'  ORDER BY a1.num";

            }
            else
            {
                quer = $"SELECT DISTINCT(rfc),a1.num FROM 	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp AND a1.jpp = a2.jpp WHERE 	a1.superviven = 'S' AND a1.jpp = a2.jpp AND a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}' ORDER BY a1.num";

            }

            List<Dictionary<string, object>> ado = globales.consulta(quer); ///JOEL
            int cantidad = ado.Count();
            query = "select  clave,descri from nominas_catalogos.perded order by clave";
            string persona = string.Empty;

            List<Dictionary<string, object>> perded = globales.consulta(query);
            try
            {

                resultado.ForEach(o =>
                {
                    persona = Convert.ToString(o["proyecto"]);
                    o["descri"] = perded.Where(p => Convert.ToString(o["clave"]) == Convert.ToString(p["clave"])).First()["descri"];
                    //  o["descri"] += " (RETROACTIVO)";
                });

            }
            catch
            {
                DialogResult dialogo1 = globales.MessageBoxError($"VERIFICAR CLAVE DEL {persona} FAVOR DE CORREGIR PARA PODER GENERAR LA NÓMINA", "ERROR", globales.menuPrincipal);
                return;
            }

            object[] aux2 = new object[resultado.Count];
            int contadorPercepcion = 0;
            int contadorDeduccion = 0;



            string archivoPrimero = string.Empty;


            archivoPrimero = resultado[0]["proyecto"].ToString();


            foreach (var item in resultado)
            {
                string proyecto = string.Empty;
                string nombre = string.Empty;
                string curp = string.Empty;
                string rfc = string.Empty;
                string imss = string.Empty;
                string categ = string.Empty;
                string clave = string.Empty;
                string descri = string.Empty;
                double monto = 0;
                //  fecha = fec2.ToString();
                string archivo = string.Empty;
                string pago4 = string.Empty;
                string pagot = string.Empty;

                string descripcionleyenda = string.IsNullOrWhiteSpace(Convert.ToString(item["leyen"])) ? "" : "(" + Convert.ToString(item["leyen"] + ")");

                proyecto = Convert.ToString(item["proyecto"]);
                nombre = Convert.ToString(item["nombre"]);
                curp = Convert.ToString(item["curp"]);

                rfc = Convert.ToString(item["rfc"]);

                imss = Convert.ToString(item["imss"]);
                categ = Convert.ToString(item["categ"]);
                clave = Convert.ToString(item["clave"]);

                if (tipo_nomina=="N")
                {
                    if (clave == "1")
                    {
                        this.compara = proyecto;

                    }
                    else
                    {
                        if (this.compara != proyecto)
                        {
                            if (clave == "43" ||  clave =="34"  )
                            {

                            }
                            else
                            {
                                DialogResult falta = globales.MessageBoxExclamation($"EL {proyecto} , no contiene clave mensual ", "AVISO", globales.menuPrincipal);
                                break;
                            }


                        }
                    }
                }
        
                descri = Convert.ToString(item["descri"]) + $"{descripcionleyenda}";
                monto = globales.convertDouble(Convert.ToString(item["monto"]));
                archivo = Convert.ToString(item["proyecto"]);
                pago4 = Convert.ToString(item["pagon"]);
                pagot = Convert.ToString(item["pagot"]);




                object[] tt1 = { proyecto, rfc, nombre, categ, "", "", "", "", "", "", "" };

                if (archivoPrimero != archivo)
                {
                    archivoPrimero = archivo;
                    int tope = contadorDeduccion <= contadorPercepcion ? contadorPercepcion : contadorDeduccion;
                    contadorDeduccion = tope;
                    contadorPercepcion = tope;
                }

                if (Convert.ToInt32(clave) < 60 || Convert.ToInt32(clave) == 106 || Convert.ToInt32(clave) == 107)
                {
                    if (aux2[contadorPercepcion] == null)
                    {
                        tt1[4] = clave;
                        tt1[5] = descri;
                        tt1[6] = monto;
                        aux2[contadorPercepcion] = tt1;
                    }
                    else
                    {
                        object[] tmp = (object[])aux2[contadorPercepcion];
                        tmp[4] = clave;
                        tmp[5] = descri;
                        tmp[6] = monto;
                    }
                    contadorPercepcion++;
                }
                else
                {
                    if (Convert.ToInt32(clave) == 106) continue;
                    if (aux2[contadorDeduccion] == null)
                    {
                        tt1[7] = clave;
                      
                        tt1[8] = descri;
                        tt1[10] = (string.IsNullOrWhiteSpace(pago4) || pago4 == "0") ? "" : $"{pago4}/{pagot}";
                        tt1[9] = monto;
                        aux2[contadorDeduccion] = tt1;
                    }
                    else
                    {
                        object[] tmp = (object[])aux2[contadorDeduccion];
                        tmp[7] = clave;
                        tmp[8] = descri;
                        tmp[10] = (string.IsNullOrWhiteSpace(pago4) || pago4 == "0") ? "" : $"{pago4}/{pagot}";
                        tmp[9] = monto;
                    }
                    contadorDeduccion++;
                }

            }



            int contador = 0;

            List<object> lista = new List<object>();
            foreach (object item in aux2)
            {
                if (item == null)
                    break;
                lista.Add(item);
            }


            aux2 = new object[lista.Count];

            int x = 0;
            foreach (object item in lista)
            {
                aux2[x] = item;
                x++;
            }



            string descripcion = $"NOMINA ELECTRONICA PARA EL PAGO A {jppdescripcion} DEL MES DE {mes} DE {txtAño.Text}";

            if (chknomina.Checked)
            {
                descripcion = "";
            }



            object[] parametros = { "descripcion", "titulo" };
            object[] valor = { descripcion, titulo.Replace("RESUMEN CONTABLE: ", "") };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;
            //Restablece los objetos para evitar el break del reporteador
            globales.reportes("reporteGeneracionNominas", "nomina", aux2, "", false, enviarParametros);

            string queryresumen = string.Empty;
            if (chknomina.Checked)
            {
                queryresumen = $"SELECT a2.clave,a2.tipopago, max(a3.descri) as descri, sum(monto) as monto FROM nominas_catalogos.maestro a1 LEFT JOIN nominas_catalogos.nominew a2 ON a1.jpp = a2.jpp JOIN nominas_catalogos.perded a3 ON a3.clave=a2.clave AND a1.num = a2.numjpp WHERE 	a1.superviven = 'S' AND a1.jpp = '{jpp}' AND a2.tipopago in  ('N','R') AND a2.tipo_nomina='{tipo_nomina}' GROUP BY a2.clave,a2.tipopago ORDER BY a2.clave,a2.tipopago;";

            }
            else
            {
                queryresumen = $"SELECT a2.clave,a2.tipopago, max(a3.descri) as descri, sum(monto) as monto FROM nominas_catalogos.maestro a1 LEFT JOIN nominas_catalogos.nominew a2 ON a1.jpp = a2.jpp JOIN nominas_catalogos.perded a3 ON a3.clave=a2.clave AND a1.num = a2.numjpp WHERE 	a1.superviven = 'S' AND a1.jpp = '{jpp}' AND a2.tipopago in  ('N','R') AND a2.tipo_nomina ='{tipo_nomina}' GROUP BY a2.clave,a2.tipopago ORDER BY a2.clave,a2.tipopago;";

            }

            List<Dictionary<string, object>> resumen = globales.consulta(queryresumen);
            object[] aux4 = new object[resumen.Count];
            int cont = 0;
            int veces = resumen.Count;
            double sumap = 0;
            double sumad = 0;

            foreach (var itemr in resumen)
            {

                string claveR = string.Empty;
                string deducR = string.Empty;
                string MontoR = string.Empty;
                string MooR = string.Empty;

                string LeyenR = string.Empty;
                string tipopago = string.Empty;
                string retro = "RETROACTIVO";

                object[] tt1 = { "", "", "", "", "", "", "", "" };

                claveR = Convert.ToString(itemr["clave"]);
                deducR = Convert.ToString(itemr["descri"]);
                MooR = Convert.ToString(itemr["monto"]);
                MontoR = string.Format("{0:c}", Convert.ToDouble(MooR));
                tipopago = Convert.ToString(itemr["tipopago"]);


                if (Convert.ToInt32(claveR) < 60 || Convert.ToInt32(claveR) == 106 || Convert.ToInt32(claveR) == 107)
                {
                    tt1[0] = claveR;
                    tt1[1] = deducR;
                    tt1[2] = MontoR;
                    if (tipopago == "R")
                        tt1[3] = retro;
                    aux4[cont] = tt1;
                    sumap = Convert.ToDouble(MooR) + sumap;
                }
                else
                {
                    if (Convert.ToInt32(claveR) == 106) continue;
                    tt1[4] = claveR;
                    tt1[5] = deducR;
                    tt1[6] = MontoR;
                    if (tipopago == "R")
                        tt1[7] = retro;
                    aux4[cont] = tt1;
                    sumad = Convert.ToDouble(MooR) + sumad;

                }
                cont++;

            }

            List<object> listaResumen = new List<object>();
            foreach (object item in aux4)
            {
                if (item == null)
                    break;
                listaResumen.Add(item);
            }


            aux4 = new object[listaResumen.Count];

            int y = 0;
            foreach (object item in listaResumen)
            {
                aux4[y] = item;
                y++;
            }

            string desc = $"RESUMEN CONTABLE DE LA NOMINA ELECTRÓNICA PARA EL PAGO A {jppdescripcion}  DEL MES DE {comboBox1.Text}";
            if (chknomina.Checked)
            {
                desc = "";
            }

            string fecha = "";
            double operacion = Convert.ToDouble(sumap) - Convert.ToDouble(sumad);
            string letra = $"LA PRESENTE NOMINA AMPARA LA CANTIDAD DE:{globales.convertirNumerosLetras(Convert.ToString(operacion), true)}";
            object[] parametrosR = { "descripcion", "sumap", "sumad", "liquido", "conteo", "letra", "titulo" };
            object[] valorR = { desc, string.Format("{0:C}", Convert.ToDouble(sumap)), string.Format("{0:C}", Convert.ToDouble(sumad)), string.Format("{0:C}", Convert.ToDouble(sumap) - Convert.ToDouble(sumad)), Convert.ToString(cantidad), letra, titulo };
            object[][] enviarParametrosR = new object[2][];

            enviarParametrosR[0] = parametrosR;
            enviarParametrosR[1] = valorR;
            //Restablece los objetos para evitar el break del reporteador
            globales.reportes("frmResumenNomina", "resumenNomina", aux4, "", false, enviarParametrosR);

           // Timbres();
        }

        private string seleccionarMes()
        {



            return this.comboBox1.Text;
        }

        private void frmElaboracionNomina_Load(object sender, EventArgs e)
        {
            txtAño.Text = DateTime.Now.Year.ToString();
            comboBox1.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string mes = string.Empty;
            if (comboBox1.Text == "ENERO " +txtAño.Text) mes = "01";
            if (comboBox1.Text == "FEBRERO " + txtAño.Text) mes = "02";
            if (comboBox1.Text == "MARZO " + txtAño.Text) mes = "03";
            if (comboBox1.Text == "ABRIL " + txtAño.Text) mes = "04";
            if (comboBox1.Text == "MAYO " + txtAño.Text) mes = "05";
            if (comboBox1.Text == "JUNIO " + txtAño.Text) mes = "06";
            if (comboBox1.Text == "JULIO " + txtAño.Text) mes = "07";
            if (comboBox1.Text == "AGOSTO " + txtAño.Text) mes = "08";
            if (comboBox1.Text == "SEPTIEMBRE " + txtAño.Text) mes = "09";
            if (comboBox1.Text == "OCTUBRE " + txtAño.Text) mes = "10";
            if (comboBox1.Text == "DICIEMBRE " + txtAño.Text) mes = "12";
            if (comboBox1.Text == "NOVIEMBRE " + txtAño.Text) mes = "11";

            string tipo_nomina = string.Empty;

            if (radioButton1.Checked) tipo_nomina = "AG";
            if (radioButton2.Checked) tipo_nomina = "CA";
            if (radioButton3.Checked) tipo_nomina = "DM";
            if (radioButton4.Checked) tipo_nomina = "UT";
            if (radioButton5.Checked) tipo_nomina = "CAN2";
            if (radioButton7.Checked) tipo_nomina = "PF";

            if (rbExtraordinaria.Checked) tipo_nomina = "E";


            if (groupBox2.Visible == false) tipo_nomina = "N";

            DateTime anio = DateTime.Now;
            //   string anios = Convert.ToString(anio.Year).Substring(2, 2);
            string a = txtAño.Text;
            string anios = a.Substring(2, 2);

            string query = string.Empty;
            if (chknomina.Checked)
            {
                query = $"SELECT a2.jpp,a2.numjpp,a2.clave,a2.secuen,a2.descri,a2.pagon as npago,a2.pagot,a2.tipopago as tipo_pago,a2.leyen,a2.fechaini,a2.fechafin,a2.monto,a2.folio FROM nominas_catalogos.maestro a1 LEFT JOIN nominas_catalogos.nominew a2 ON a1.jpp = a2.jpp AND a1.num = a2.numjpp WHERE a1.superviven = 'S'  AND a2.tipopago in ('N','R') AND tipo_nomina='{tipo_nomina}'";

            }
            else
            {
                query = "SELECT a2.jpp,a2.numjpp,a2.clave,a2.secuen,a2.descri,a2.pagon as npago,a2.pagot,a2.tipopago as tipo_pago,a2.leyen,a2.fechaini,a2.fechafin,a2.monto,a2.folio FROM nominas_catalogos.maestro a1 LEFT JOIN nominas_catalogos.nominew a2 ON a1.jpp = a2.jpp AND a1.num = a2.numjpp WHERE a1.superviven = 'S'  AND a2.tipopago in ('N','R') AND tipo_nomina='N'";

            }

            List<Dictionary<string, object>> resultado = globales.consulta(query);
            string entrada = Convert.ToString(resultado.Count);
            DialogResult p = globales.MessageBoxQuestion($"DESEA CERRAR NÓMINA.SE GENERARA CIERRE DEFINITIVO Y RESPAlDARÁ INFORMACIÓN, SE IMPORTARÁN {entrada} MOVIMIENTOS", "AVISO IMPORTANTE", globales.menuPrincipal);
            if (p == DialogResult.No) return;
            string inserta = string.Empty;
            foreach (var item in resultado)
            {
                string jpp = Convert.ToString(item["jpp"]);
                string numjpp = Convert.ToString(item["numjpp"]);
                string clave = Convert.ToString(item["clave"]);
                string secuen = Convert.ToString(item["secuen"]);
                string descri = Convert.ToString(item["descri"]);
                string pagon = Convert.ToString(item["npago"]);
                string pagot = Convert.ToString(item["pagot"]);
                if (pagon == "") pagon = "0"; if (pagot == "") pagot = "0";
                string tipo_pago = Convert.ToString(item["tipo_pago"]);
                string leyen = Convert.ToString(item["leyen"]);
                string fechaini = Convert.ToString(item["fechaini"]).Replace(" 12:00:00 a. m.", "");
                fechaini = $"'{fechaini}'";

                if (fechaini == "''") fechaini = "null";

                string fechafin = Convert.ToString(item["fechafin"]).Replace(" 12:00:00 a. m.", "");
                fechafin = $"'{fechafin}'";

                if (fechafin == "''") fechafin = "null";

                string monto = Convert.ToString(item["monto"]);

                this.archivo = anios + mes;

                string folio = Convert.ToString(item["folio"]);
                if (folio == "") folio = "0";



                if (chknomina.Checked)
                {
                    inserta = $"INSERT INTO nominas_catalogos.respaldos_nominas (jpp,numjpp,clave,secuen,descri,pago4,pagot,leyen,fechaini,fechafin,archivo,tipo_nomina,monto,tipo_pago,folio)" +
                                                                                $"values ('{jpp}',{numjpp},{clave},{secuen},'{descri}',{pagon},{pagot},'{leyen}',{fechaini},{fechafin},'{this.archivo}','{tipo_nomina}',{monto},'{tipo_pago}',{folio});";

                }
                else
                {
                    inserta = $"INSERT INTO nominas_catalogos.respaldos_nominas (jpp,numjpp,clave,secuen,descri,pago4,pagot,leyen,fechaini,fechafin,archivo,tipo_nomina,monto,tipo_pago,folio)" +
                                                                                $"values ('{jpp}',{numjpp},{clave},{secuen},'{descri}',{pagon},{pagot},'{leyen}',{fechaini},{fechafin},'{this.archivo}','N',{monto},'{tipo_pago}',{folio});";

                }

                Cursor.Current = Cursors.WaitCursor;

                globales.consulta(inserta);


            }
            string confirma = string.Empty;

            if (chknomina.Checked)
            {
                confirma = $"select count(*)as conteo  from nominas_catalogos.respaldos_nominas where archivo='{this.archivo}' and tipo_nomina='{tipo_nomina}'";

            }
            else
            {
                confirma = $"select count(*)as conteo  from nominas_catalogos.respaldos_nominas where archivo='{this.archivo}' and tipo_nomina='N'";

            }
            List<Dictionary<string, object>> res = globales.consulta(confirma);
            string salida = Convert.ToString(res[0]["conteo"]);
            if (entrada == salida)
            {
                DialogResult dia = globales.MessageBoxSuccess("SE RESPALDO TODA LA INFORMACIÓN DE FORMA CORRECTA", "TERMIANDO", globales.menuPrincipal);


            }
            else
            {
                DialogResult dialogerror = globales.MessageBoxError("SE PRESENTO UN ERROR , NO SE TRAPASO TODA LA INFORMACIÓN", "CONTACTE A SISTEMAS", globales.menuPrincipal);
            }
            Cursor.Current = Cursors.Default;



        }

        private void chknomina_CheckedChanged(object sender, EventArgs e)
        {
            if (chknomina.Checked)
            {
                groupBox2.Visible = true;

            }
            else
            {
                groupBox2.Visible = false;
            }
        }

        private void frmElaboracionNomina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                button4.Visible = true;
            }

            if (e.KeyCode == Keys.F12)
            {
                Timbres();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }



        private void Timbres()
        {

            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar el archivo de timbrado?", "Aviso", globales.menuPrincipal);
            if (dialogo == DialogResult.No)
                return;
            string jpp = string.Empty;
            string jppdescripcion = string.Empty;
            mes = seleccionarMes();



            if (jubilados.Checked)
            {
                jpp = "JUB";
                jppdescripcion = "JUBILADOS";
            }
            else if (pensionado.Checked)
            {
                jpp = "PDO";
                jppdescripcion = "PENSIONADOS";
            }
            else if (pensionistas.Checked)
            {
                jpp = "PTA";
                jppdescripcion = "PENSIONISTAS";
            }
            else if (alimenticia.Checked)
            {
                jpp = "PEA";
                jppdescripcion = "PENSION ALIMENTICIA";
            }
            string query = string.Empty;

            string tipo_nomina = string.Empty;
            string titulo = "OFICINA DE PENSIONES DEL ESTADO DE OAXACA";

            if (radioButton1.Checked)
            {

                if (jubilados.Checked)
                {
                    jpp = "JUB";
                    tipo_nomina = "AG";
                }
                else if (pensionado.Checked)
                {
                    jpp = "PDO";
                    tipo_nomina = "AG";

                }
                else if (pensionistas.Checked)
                {
                    jpp = "PTA";
                    tipo_nomina = "AG";

                }
                else if (alimenticia.Checked)
                {
                    jpp = "PEA";
                    tipo_nomina = "AG";

                }

            }
            if (radioButton2.Checked) tipo_nomina = "CA";
            if (radioButton3.Checked) tipo_nomina = "DM";
            if (radioButton4.Checked) tipo_nomina = "UT";
            if (radioButton5.Checked) tipo_nomina = "CAN2"; //PDOPTA
            if (rbExtraordinaria.Checked) tipo_nomina = "N";
            if (radioButton7.Checked) tipo_nomina = "PF";


            if (groupBox2.Visible == false) tipo_nomina = "N";

            switch (tipo_nomina)
            {
                case "DM":
                    titulo = $"SECRETARIA DE ADMINISTRACION\nRESUMEN CONTABLE: NOMINA ELECTRONICA DEL DIA DE LAS MADRES CORRESPONDIENTE AL EJERCICIO {txtAño.Text}\n";
                    //  $"PROYECTO 106{txtproyecto.text}";
                    break;
                case "AG":
                    titulo = $"OFICINA DE PENSIONES\nRESUMEN CONTABLE: NOMINA ELECTRONICA PARA EL PAGO  DE RETROACTIVO PARA {jppdescripcion} CORRESPONDIENTE DEL 01 DE ENERO AL 31 DE OCTUBRE DEL 2020\n";
                    break;
            }

            if (chknomina.Checked)
            {
                query = "SELECT	CONCAT(a1.jpp,a1.num) as proyecto,	a1.nombre,	a1.curp,	a1.rfc,	a1.imss,	a1.categ,	a2.clave,	a2.descri,	a2.monto,	a2.pagon, " +
             " a2.pagot,	a2.leyen FROM	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp and a1.jpp = a2.jpp WHERE " +
             $" a1.superviven = 'S' AND a1.jpp = a2.jpp AND a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}' ORDER BY 	a1.jpp,a1.num,a2.clave ";
            }
            else
            {
                query = "SELECT	CONCAT(a1.jpp,a1.num) as proyecto,	a1.nombre,	a1.curp,	a1.rfc,	a1.imss,	a1.categ,	a2.clave,	a2.descri,	a2.monto,	a2.pagon, " +
             " a2.pagot,	a2.leyen FROM	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp and a1.jpp = a2.jpp WHERE " +
             $" a1.superviven = 'S' AND a1.jpp = a2.jpp AND a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}' ORDER BY 	a1.jpp,a1.num,a2.clave ";
            }



            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0)
            {
                DialogResult dialog = globales.MessageBoxExclamation("NO SE ENCUENTRA CARGADA UNA NÓMINA ESPECIAL", "UPS", globales.menuPrincipal);
                return;
            }

            string querys = $"create temp table tempo as (SELECT	CONCAT (a1.jpp, a1.num) AS proyecto,a1.num,a1.nombre,a1.curp,a1.rfc,a1.imss,a1.categ,a2.clave,a2.descri,a2.monto,a2.pagon,a2.pagot,a2.leyen" +
" FROM nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp AND a1.jpp = a2.jpp WHERE a1.superviven = 'S' AND a1.jpp = a2.jpp AND a1.jpp = 'JUB'" +
" AND a2.tipo_nomina = 'N' ORDER BY a1.jpp, a1.num, a2.clave);" +
            "select proyecto, nombre,num, curp, rfc, imss, categ, sum(monto)FILTER(where clave >= 1 and clave <= 60) as Totper, COALESCE(sum(monto)FILTER(where clave >= 61), 0) as Totded" +
            " from tempo GROUP BY proyecto, nombre, curp, rfc, imss,num, categ ORDER BY  num; ";

            List<Dictionary<string, object>> result = globales.consulta(querys);

            Cursor.Current = Cursors.WaitCursor;

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel no se encuentra Instalado");
                return;
            }

            int numero = comboBox1.SelectedIndex +1;
            DateTime anio = DateTime.Now;
            string año = Convert.ToString(anio.Year);
            int limite = result.Count();
            


            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "procesar";
            xlWorkSheet.Cells[1, 2] = "num_empleado";
            xlWorkSheet.Cells[1, 3] = "nombre";
            xlWorkSheet.Cells[1, 4] = "rfc";
            xlWorkSheet.Cells[1, 5] = "curp";
            xlWorkSheet.Cells[1, 6] = "tipo_contrato";
            xlWorkSheet.Cells[1, 7] = "tipo_regimen";
            xlWorkSheet.Cells[1, 8] = "rfc_patron_origen";
            xlWorkSheet.Cells[1, 9] = "clave_entfed_labora";
            xlWorkSheet.Cells[1, 10] = "serie";
            xlWorkSheet.Cells[1, 11] = "lugar_expedición";
            xlWorkSheet.Cells[1, 12] = "dias_trabajados";
            xlWorkSheet.Cells[1, 13] = "importe_excento";
            xlWorkSheet.Cells[1, 14] = "total_percepciones";
            xlWorkSheet.Cells[1, 15] = "total_deducciones";
            xlWorkSheet.Cells[1, 16] = "total_neto";
            xlWorkSheet.Cells[1, 17] = "origen_recurso";
            xlWorkSheet.Cells[1, 18] = "total_una_exhibicion";
            xlWorkSheet.Cells[1, 19] = "ingreso_acu ingreso_noacumulable";

            int c = 2;
            foreach (var item in result)
            {
                xlWorkSheet.Cells[c, 1] = "si";
                xlWorkSheet.Cells[c, 2] = Convert.ToString(item["proyecto"]);
                xlWorkSheet.Cells[c, 3] = Convert.ToString(item["nombre"]);
                xlWorkSheet.Cells[c, 4] = Convert.ToString(item["rfc"]);
                xlWorkSheet.Cells[c, 5] = Convert.ToString(item["curp"]);
                xlWorkSheet.Cells[c, 6] = "10";
                xlWorkSheet.Cells[c, 7] = "12";
                xlWorkSheet.Cells[c, 8] = "OPE631216S18";
                xlWorkSheet.Cells[c, 9] = "OAX";
                xlWorkSheet.Cells[c, 10] = año + Convert.ToString(numero) + "JUB";
                xlWorkSheet.Cells[c, 11] = "68050";
                xlWorkSheet.Cells[c, 12] = "30";
                xlWorkSheet.Cells[c, 13] = Convert.ToDouble(item["totper"]);
                xlWorkSheet.Cells[c, 14] = Convert.ToDouble(item["totper"]);
                xlWorkSheet.Cells[c, 15] = Convert.ToDouble(item["totded"]);
                double per = Convert.ToDouble(item["totper"]);
                double ded = Convert.ToDouble(item["totded"]);
                double total_neto = per - ded;

                xlWorkSheet.Cells[c, 16] = total_neto;
                xlWorkSheet.Cells[c, 17] = "IP";
                xlWorkSheet.Cells[c, 18] = total_neto;
                xlWorkSheet.Cells[c, 19] = total_neto;
                c++;
            }









            xlWorkBook.SaveAs("C:\\ESTRUCTURA.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            string directorio = "C:\\ESTRUCTURA.xls";
            DialogResult dia = globales.MessageBoxSuccess("EN BREVE SE ABRIRÁ EL ARCHIVO", "MENSAJE", globales.menuPrincipal);

            string xlsPath = Path.Combine(Application.StartupPath, directorio);


            Process.Start(xlsPath);
            Cursor.Current = Cursors.Default;


        }

    }
}