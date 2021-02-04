using Microsoft.Reporting.WinForms;
using SISPE_MIGRACION.codigo.herramientas.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.HISTORICO
{
    public partial class frmHistoricoNomina : Form
    {
        private string titulo;

        public frmHistoricoNomina()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar la nomina?", "Aviso", globales.menuPrincipal);
            if (dialogo == DialogResult.No)
                return;

            Cursor = Cursors.WaitCursor;

            string jpp = string.Empty;
            string jppdescripcion = string.Empty;
            string mes = cmbMes.Text;
            int mesInt = cmbMes.SelectedIndex;


            titulo = "OFICINA DE PENSIONES DEL ESTADO DE OAXACA";
            if (cmbtipo.SelectedIndex == 0)
            {
                jpp = "JUB";
                jppdescripcion = "JUBILADOS";
            }
            else if (cmbtipo.SelectedIndex == 1)
            {
                jpp = "PDO";
                jppdescripcion = "PENSIONADOS";
            }
            else if (cmbtipo.SelectedIndex == 2)
            {
                jpp = "PTA";
                jppdescripcion = "PENSIONISTA";
            }
            else if (cmbtipo.SelectedIndex == 3)
            {
                jpp = "PEA";
                jppdescripcion = "PENSION ALIMENTICIA";
            }
            string query = string.Empty;

            string tipo_nomina = string.Empty;

            string descripcion2 = string.Empty;

            if (radioButton1.Checked) {
                tipo_nomina = "AG";
                descripcion2 = "DE AGUINALDO";
            }
            if (radioButton2.Checked) {
                tipo_nomina = "CA";
                descripcion2 = "DE CANASTA NAVIDEÑA";
                titulo = "SECRETARIA DE ADMINISTRACION";
            }
            if (radioButton3.Checked) {
                tipo_nomina = "DM";
                descripcion2 = "DEL DIA DE LAS MADRES";
                titulo = "SECRETARIA DE ADMINISTRACION";

            }
            if (radioButton4.Checked) {
                tipo_nomina = "UT";
                descripcion2 = "DE UTILES ESCOLARES";
                titulo = "SECRETARIA DE ADMINISTRACION";
            }
            if (radioButton5.Checked) {
                tipo_nomina = "CAN2"; //PDOPTA
                descripcion2 = "DE CANASTA";
                titulo = "SECRETARIA DE ADMINISTRACION";
            }

            if (rbExtra.Checked)
            {
                tipo_nomina = "E"; //PDOPTA
                descripcion2 = " DE APOYO EXTRAORDINARIO ARCON NAVIDEÑO";
                titulo = "SECRETARIA DE ADMINISTRACION";
            }


            if (radioButton6.Checked)
            {
                tipo_nomina = "PF"; //PDOPTA
                descripcion2 = "DE CANASTA NAVIDEÑA";
                titulo = "SECRETARIA DE ADMINISTRACION";
            }

            tipo_nomina = this.chknomina.Checked ? tipo_nomina : "N";
            descripcion2 = this.chknomina.Checked ? descripcion2 : "";
            string archivo = string.Empty;


            archivo += txtAño.Text.Substring(2);
            archivo += cmbMes.SelectedIndex+1 <= 9 ? $"0{ cmbMes.SelectedIndex+1}" : (cmbMes.SelectedIndex+1).ToString();

            if (chknomina.Checked)
            {
                query = "SELECT	CONCAT(a1.jpp,a1.num) as proyecto,	a1.nombre,	a1.curp,	a1.rfc,	a1.imss,	a1.categ,	a2.clave,	a2.descri,	a2.monto,	a2.pago4 as pagon, " +
             " a2.pagot,	a2.leyen FROM	nominas_catalogos.maestro a1 inner JOIN nominas_catalogos.respaldos_nominas a2 ON a1.num = a2.numjpp and a1.jpp = a2.jpp WHERE " +
             $"  a1.jpp = a2.jpp AND a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}' and a2.archivo = '{archivo}' ORDER BY 	a1.jpp,a1.num,a2.clave ";
            }
            else
            {
                query = $"create temp table temp2 as select * from nominas_catalogos.respaldos_nominas where archivo = '{archivo}';  SELECT	CONCAT(a1.jpp,a1.num) as proyecto,	a1.nombre,	a1.curp,	a1.rfc,	a1.imss,	a1.categ,	a2.clave,	a2.descri,	a2.monto,	a2.pago4 as pagon, " +
             " a2.pagot,	a2.leyen FROM	nominas_catalogos.maestro a1 inner JOIN temp2 a2 ON a1.num = a2.numjpp and a1.jpp = a2.jpp WHERE " +
             $"   a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}'  ORDER BY 	a1.jpp,a1.num,a2.clave ";
            }



            


            switch (cmbSalida.SelectedIndex) {
                case 0:
                    sacarNomina(query, jppdescripcion, jpp, mes, descripcion2);
                    break;
                case 1:
                    resumenomina(tipo_nomina,jpp,archivo,jppdescripcion, descripcion2);
                    break;
                case 2:
                    sacarLiquidos(tipo_nomina);
                    break;
                default:
                    break;
            }

            string quer = string.Empty;



            Cursor = Cursors.Default;

        }

        private void resumenomina(string tipo_nomina,string jpp,string archivo,string jppdescripcion,string descripcion2)
        {

            string quer = string.Empty;
            if (chknomina.Checked)

            {
                quer = $"SELECT DISTINCT(a1.rfc),a1.num FROM 	nominas_catalogos.maestro a1 inner JOIN nominas_catalogos.respaldos_nominas a2 ON a1.num = a2.numjpp AND a1.jpp = a2.jpp WHERE  a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}' and a2.archivo = '{archivo}'  ORDER BY a1.num";

            }
            else
            {
                quer = $"SELECT DISTINCT(a1.rfc),a1.num FROM 	nominas_catalogos.maestro a1 inner JOIN nominas_catalogos.respaldos_nominas a2 ON a1.num = a2.numjpp AND a1.jpp = a2.jpp WHERE  a1.jpp = '{jpp}' and a2.tipo_nomina='{tipo_nomina}' and a2.archivo = '{archivo}' ORDER BY a1.num";

            }

            List<Dictionary<string, object>> ado = globales.consulta(quer);
            int cantidad = ado.Count();

            string queryresumen = string.Empty;

            if (chknomina.Checked)
            {
                queryresumen = $"SELECT a2.clave,a2.tipo_pago, max(a3.descri) as descri, sum(monto) as monto FROM nominas_catalogos.maestro a1 LEFT JOIN nominas_catalogos.respaldos_nominas a2 ON a1.jpp = a2.jpp and a2.archivo = '{archivo}' JOIN nominas_catalogos.perded a3 ON a3.clave=a2.clave AND a1.num = a2.numjpp WHERE 	 a1.jpp = '{jpp}' AND a2.tipo_pago in  ('N','R') AND a2.tipo_nomina='{tipo_nomina}' GROUP BY a2.clave,a2.tipo_pago ORDER BY a2.clave,a2.tipo_pago;";

            }
            else
            {
                queryresumen = $"SELECT a2.clave,a2.tipo_pago, max(a3.descri) as descri, sum(monto) as monto FROM nominas_catalogos.maestro a1 LEFT JOIN nominas_catalogos.respaldos_nominas a2 ON a1.jpp = a2.jpp and a2.archivo = '{archivo}' JOIN nominas_catalogos.perded a3 ON a3.clave=a2.clave AND a1.num = a2.numjpp WHERE 	 a1.jpp = '{jpp}' AND a2.tipo_pago in  ('N','R') AND a2.tipo_nomina ='{tipo_nomina}' GROUP BY a2.clave,a2.tipo_pago ORDER BY a2.clave,a2.tipo_pago;";

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
                tipopago = Convert.ToString(itemr["tipo_pago"]);


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

            string desc = $"RESUMEN CONTABLE DE LA NOMINA ELECTRÓNICA PARA EL PAGO {descripcion2} A {jppdescripcion}  DEL MES DE {cmbMes.Text} DEL AÑO {txtAño.Text}";

            string fecha = "";
            double operacion = Convert.ToDouble(sumap) - Convert.ToDouble(sumad);
            string letra = $"LA PRESENTE NOMINA AMPARA LA CANTIDAD DE:{globales.convertirNumerosLetras(Convert.ToString(operacion), true)}";
            object[] parametrosR = { "descripcion", "sumap", "sumad", "liquido", "conteo", "letra" ,"titulo"};
            object[] valorR = { desc, string.Format("{0:C}", Convert.ToDouble(sumap)), string.Format("{0:C}", Convert.ToDouble(sumad)), string.Format("{0:C}", Convert.ToDouble(sumap) - Convert.ToDouble(sumad)), Convert.ToString(cantidad), letra ,titulo};
            object[][] enviarParametrosR = new object[2][];

            enviarParametrosR[0] = parametrosR;
            enviarParametrosR[1] = valorR;
            //Restablece los objetos para evitar el break del reporteador
            ReportViewer reporte =  globales.reportesParaPanel("frmResumenNomina", "resumenNomina", aux4, "", false, enviarParametrosR);
            reporte.Dock = DockStyle.Fill;
            panelreporte.Controls.Clear();
            panelreporte.Controls.Add(reporte);
        }

        private void sacarNomina(string query,string jppdescripcion,string jpp,string mes,string descripcion2)
        {
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0)
            {
                DialogResult dialog = globales.MessageBoxExclamation("NO SE ENCUENTRA CARGADA UNA NÓMINA ESPECIAL", "UPS", globales.menuPrincipal);
                return;
            }


            query = "select  clave,descri from nominas_catalogos.perded order by clave";
            List<Dictionary<string, object>> perded = globales.consulta(query);

            resultado.ForEach(o =>
            {
                o["descri"] = perded.Where(p => Convert.ToString(o["clave"]) == Convert.ToString(p["clave"])).First()["descri"];
                //  o["descri"] += " (RETROACTIVO)";
            });

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

            

            string descripcion = $"NOMINA ELECTRONICA PARA EL PAGO {descripcion2} A {jppdescripcion} DEL MES DE {mes} DE {txtAño.Text}";

            object[] parametros = { "descripcion","titulo" };
            object[] valor = { descripcion,titulo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;
            //Restablece los objetos para evitar el break del reporteador
            ReportViewer reporte = globales.reportesParaPanel("reporteGeneracionNominas", "nomina", aux2, "", false, enviarParametros);
            reporte.Dock = DockStyle.Fill;
            panelreporte.Controls.Clear();
            panelreporte.Controls.Add(reporte);
        }

        private void sacarLiquidos(string tipo_nomina)
        {

            string que = $"CREATE TEMP TABLE BASE AS  (SELECT CONCAT (a1.jpp, a1.num) AS proyecto,a1.nombre,	a1.curp,	a1.rfc,	a1.imss,	a1.categ,	a2.clave,	a2.descri,	a2.monto,	a2.pagon,	 a2.pagot,	a2.leyen FROM	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp AND a1.jpp = a2.jpp WHERE  a1.superviven = 'S' AND a1.jpp = a2.jpp AND a2.tipo_nomina='{tipo_nomina}' ORDER BY a1.jpp, a1.num,a2.clave);"
                + "CREATE temp table per as SELECT proyecto , sum(monto) FROM BASE WHERE CLAVE >= 59  GROUP BY proyecto;" +
              " CREATE temp table ded as SELECT proyecto , sum(monto) FROM BASE WHERE CLAVE <= 60  GROUP BY proyecto;" +
            "  CREATE TEMP TABLE liquidos as select a1.proyecto, (a2.sum - a1.sum) as liquido from per a1 JOIN ded a2 on a1.proyecto = a2.proyecto order by a1.proyecto;" +
          "  select* from liquidos where liquido <= 1500; ";

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

                ReportViewer reporte = globales.reportesParaPanel("valida_nomina", "va_nom", aux1, "", false, enviarParametros1);
                reporte.Dock = DockStyle.Fill;
                this.panelreporte.Controls.Clear();
                this.panelreporte.Controls.Add(reporte);
                this.Cursor = Cursors.Default;

                //  return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHistoricoNomina_Load(object sender, EventArgs e)
        {
            this.cmbtipo.SelectedIndex = 0;
            txtAño.Text = DateTime.Now.Year.ToString();
            cmbMes.SelectedIndex = DateTime.Now.Month - 1;
            cmbSalida.SelectedIndex = 0;
        }

        private void panelreporte_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chknomina_CheckedChanged(object sender, EventArgs e)
        {
            panel5.Enabled = chknomina.Checked;
        }
    }
}
