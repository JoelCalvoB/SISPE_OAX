using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.REPORTES
{
    public partial class frmConvenioMod : Form
    {
        public frmConvenioMod()
        {
            InitializeComponent();
        }

        private void Genera()
        {
            string query = string.Empty;
            string qry = string.Empty;

            qry = "SELECT a1.nombre_em,a1.direc_inmu,a2.f_solicitud,a2.f_autorizacion,a2.sec,a2.cap_prest,a2.int_prest,a2.tot_prest,a2.plazoa,a2.plazo,a3.tomo_inscr,a3.f_inscr_rp,a3.libr_inscr,a3.dist_judic,a4.nombre_not,a4.n_notario,a4.f_inscr_n FROM DATOS.p_hipote a1 left JOIN datos.h_solici a2 ON a1.folio = a2.expediente left JOIN datos.h_santec a3 ON a2.expediente=a3.expediente left JOIN datos.h_enotar a4 ON a3.expediente=a4.expediente where a2.f_autorizacion='2002-12-09'AND a2.sec <> '0'";
            query = string.Format(qry, txtFecha.Text);
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            if (resultado.Count <= 0) { DialogResult dialogo = globales.MessageBoxExclamation("NO SE ENCUENTRA LA INFORMACIÓN COMPLETA DE ESTE EXPEDIENTE", "FAVOR DE VERIFICAR PARÁMETROS INGRESADOS", globales.menuPrincipal); return; }

            foreach (var item in resultado)
            {

                object[] aux2 = new object[resultado.Count];
                int contador = 0;
                string nombre_em = Convert.ToString(item["nombre_em"]);
                string direc_inmu = Convert.ToString(item["direc_inmu"]);
                string f_solcictud = Convert.ToString(item["f_solicitud"]);
                string f_autorizacion = Convert.ToString(item["f_autorizacion"]);
                query = "select * from datos.fechaletra('{0}')";
                qry = string.Format(query, string.Format("{0:d}", Convert.ToDateTime(f_autorizacion)));
                List<Dictionary<string, object>> fecha = globales.consulta(qry);
                f_autorizacion = Convert.ToString(fecha[0]["fechaletra"]).Replace("Oaxaca de Juárez,Oax.,a ", "");

                string cap_prest = Convert.ToString(item["cap_prest"]);
                string int_prest = Convert.ToString(item["int_prest"]);
                string tot_prest = Convert.ToString(item["tot_prest"]);
                string tomo_inscr = Convert.ToString(item["tomo_inscr"]);
                string libr_inscr = Convert.ToString(item["libr_inscr"]);
                string dist_judic = Convert.ToString(item["dist_judic"]);
                string nombre_not = Convert.ToString(item["nombre_not"]);
                string n_notario = Convert.ToString(item["n_notario"]);
                // ULTIMA AMPLIACIÓN
                string f_inscr_n = Convert.ToString(item["f_inscr_n"]);
                string f_inscr_rp = Convert.ToString(item["f_inscr_rp"]);
                string query2 = "select * from datos.fechaletra('{0}')";
                string qry2 = string.Format(query2, string.Format("{0:d}", Convert.ToDateTime(f_inscr_n)));
                List<Dictionary<string, object>> fecha1 = globales.consulta(qry2);
                f_inscr_n = Convert.ToString(fecha1[0]["fechaletra"]).Replace("Oaxaca de Juárez,Oax.,a ", "");
                string f_solicitudU = Convert.ToString(item["f_solicitud"]);

                string query3 = "select * from datos.fechaletra('{0}')";
                string qry3 = string.Format(query3, string.Format("{0:d}", Convert.ToDateTime(f_solicitudU)));
                List<Dictionary<string, object>> fecha12 = globales.consulta(qry3);
                f_solicitudU = Convert.ToString(fecha12[0]["fechaletra"]).Replace("Oaxaca de Juárez,Oax.,a ", "");

                string f_autorizacionU = Convert.ToString(item["f_autorizacion"]);
                string query1 = "select * from datos.fechaletra('{0}')";
                string qry1 = string.Format(query1, string.Format("{0:d}", Convert.ToDateTime(f_autorizacionU)));
                List<Dictionary<string, object>> fecha10 = globales.consulta(qry1);
                f_autorizacionU = Convert.ToString(fecha10[0]["fechaletra"]).Replace("Oaxaca de Juárez,Oax.,a ", "");


                string quer = "select * from datos.fechaletra('{0}')";
                string qry20 = string.Format(query1, string.Format("{0:d}", Convert.ToDateTime(f_inscr_rp)));
                List<Dictionary<string, object>> fecha07 = globales.consulta(qry20);
                f_inscr_rp = Convert.ToString(fecha07[0]["fechaletra"]).Replace("Oaxaca de Juárez,Oax.,a ", "");


                string cap_prestU = Convert.ToString(item["cap_prest"]);
                string tot_prestU = Convert.ToString(item["tot_prest"]);
                string int_prestU = Convert.ToString(item["int_prest"]);

                string plazo = Convert.ToString(resultado[0]["plazo"]);
                int factor = 0;
                if (plazo == "Q") factor = 24; plazo = "quincenales";
                if (plazo == "M") factor = 12; plazo = "mensuales";
                int plazoa = (Convert.ToInt32(item["plazoa"]) * factor) - 1;

                query = "select nombre from catalogos.firmas where clave='DIR'";
                List<Dictionary<string, object>> firma = globales.consulta(query);
                string dir = Convert.ToString(firma[0]["nombre"]);


                object[] tt1 = { };
                aux2[contador] = tt1;
                contador++;

                string cuerpo1 = "CONVENIO MODIFICATORIO DEL CONTRATO MUTUO CON GARANTÍA HIPOTECARIA FORMULADO POR LA OFICINA DE PENSIONES DEL ESTADO DE OAXACA REPRESENTADA DEBIDAMENTE POR EL "
                    + dir + " DE LA OFICINA DE PENSIONES DEL ESTADO DE OAXACA Y POR OTRA PARTE LA / EL " + nombre_em + " EMPLEADO DEL GOBIERNO DEL ESTADO.";

                string cuerpo2 = "Con fecha " + f_inscr_n + " la Oficina de Pensiones del Gobierno del Estado celebró Contrato de mutuo con garantía hipotecaria ante la fe de mi Notario Público No." + n_notario +
                    " " + nombre_not + " y que ampara la cantidad de " + string.Format("{0:c}", Convert.ToDouble(cap_prest)).Replace("$", "") + " " + globales.convertirNumerosLetras(cap_prest, true) + " "
                    + "como capital y " + string.Format("{0:c}", Convert.ToDouble(int_prest)).Replace("$", "") + " " + globales.convertirNumerosLetras(int_prest, true) + " , de interes haciendo un total de "
                    + "(" + string.Format("{0:c}", Convert.ToDouble(tot_prest)).Replace("$", "") + " " + globales.convertirNumerosLetras(tot_prest, true) + ") .El mencionado contrato fue debidamente inscrito en el registro " +
                    "numero " + tomo_inscr + " del Libro " + libr_inscr + " de la sección segunda del Instito de la Función Registral del Estado de Oaxaca, del Distrito Judicial de " + dist_judic + " ,OAXACA con fecha " + f_inscr_rp + ".";

                string cuerpo3 = "Mediante la solicitud de fecha " + f_solicitudU + " la o el trabajador pidió ampliación de crédito hipotecario, mismo que fue autorizado en sesión de consejo " +
                    "Directivo el día " + f_autorizacionU + ".";
                string cuerpo4 = "I.- La o El acreditado reconoce expresamente el carácter y personalidad del Director General de la Oficina de Pensiones del Estado de Oaxaca y Representante Legal " +
                    " del Consejo Directivo, quien suscribe el presente Contrato Modificatorio derivado del Contrato de Mutuo con Garantía Hipotecaria referido en el antecedente de este documento.";
                string cuerpo5 = "2.-Están de acuerdo los contratantes en modificar el contrato mutuo con Garantía Hipotecaria en razón de que la Oficina de Pensiones le va a ampliar el crédito que le fue " +
                    "otorgado " + f_autorizacion + " y por cuya razón y por convenir así a las partes, han pactado modificar el dicho Contrato bajo las siguientes:";
                string clausulaPrimera = "PRIMERA.- La Oficina de Pensiones del Gobierno del Estado por Conducto del Director de dicha Oficina, dan en Mutuo con Garantía Hipotecaria a " + nombre_em +
                    " la cantidad de (" + string.Format("{0:c}", Convert.ToDouble(cap_prestU)).Replace("$", "") + " (" + globales.convertirNumerosLetras(cap_prestU, true) + ".) " +
                    " como apliación del crédito que por la cantidad de " + string.Format("{0:c}", Convert.ToDouble(tot_prest)).Replace("$", "") + " " + globales.convertirNumerosLetras(tot_prest, true) +
                     "tiene concedido según escritura relacionada en los antecedentes";

                string clausulaSegunda = "Los intereses correspondientes a esta operación serán de " + string.Format("{0:c}", Convert.ToDouble(int_prestU)).Replace("$", "") + " (" + globales.convertirNumerosLetras(int_prestU, true) + ".)";
                string clausulaTercera = "El capital e intereses los cubrira la o el deudor mediante 1 pago en caja y " + plazoa + " pagos " + plazo + " iguales y sucesivos debiéndose efectuarlos los días 15 y 30 de cada mes.";
                string clausulaCaurta = "Como es obvio y en ampliación con la garantía previamente establecida en garantía del puntual pago de la suma de " + string.Format("{0:c}", Convert.ToDouble(tot_prestU)).Replace("$", "") + ")" + "(" + globales.convertirNumerosLetras(tot_prestU, true) + ")" +
                    " estando incluidos los intereses ya que el capital se menciona en la claúsula primera, " + nombre_em + "con el consentimiento de su conyugue" + "                   " + "hipoteca el mismo predio que en ´primer lugar otorga " +
                    "a favor de la Oficina de Pensiones y que se ubica en " + direc_inmu;

                string clausulaQuinta = "Declaran los contratantes que firman el presente Convenio Modificatorio a su entera Satisfacción y sin presión alguna que marque invalidez a lo anteriormente declarado";

                string cuerpo6 = "EN LA CIUDAD DE OAXACA DE JUÁREZ, OAXACA , SIENDO LAS     DEL DÍA   DE        DEL AÑO        ANTE LA PRESENCIA DE                , REGISTRADOR DEL INSTITUTO DE LA FUNCIÓN REGISTRAL, DEL DISTRITO JUDICIAL DEL CENTRO "+
                    $", COMPARECE A RACTIFICAR EL CONTENIDO DEL  CONVENIO MODIFICATORIO, QUE CELEBRAN POR UNA PARTE EL CONTADOR PUBLICO {dir}, EN SU CARÁCTER DE DIRECTOR GENERAL DE LA OFICINA DE PENSIONES DEL ESTADO DE OAXACA, QUIEN SE IDENTIFICA CON SU CREDENCIAL "+
                    $"PARA VOTAR CON FOTOGRAFÍA CON FOLIO 1687058559817 OTORGADA A SU FAVOR POR EL INSTITUTO NACIONAL ELECTORAL , Y EL NOMBRAMIENTO EXPEDIDO POR EL GOBERNADOR CONSTITUCIONAL DEL ESTADO , MAESTRO ALEJANDRO ISMAEL MURAT HINOJOSA; ASI MISMO LA C.{nombre_em}"
                    +" COMO ACREDITADA QUIEN SE IDENTIFICA CON SU CREDENCIAL PARA VOTAR CON FOTOGRAFÍA CON FOLIO           ,LA IDENTIFICACIÓN SE TIENE A LA VISTA EN ORIGINAL CON UNA FOTOGRAFÍA AL MARGEN DERECHO, EN LA QUE CONSTAN LOS RASGOS FISIONOMICOS DE QUIENES APARECEN EN EL CONVENIO MODIFICATORIO DE FECHA"
                    +", LOS TESTIGOS                                    QUE SE IDENTIFICAN CON CREDENCIAL PARA VOTAR CON FOTOGRAFÍA CON NÚMERO DE FOLIO  000000000 Y 000000000  RESPECTIVAMENTE ; EN USO DE LA PALABARA LA O EL C. REGISTRADORA PÚBLICA, DIJO "+
                    "QUE EN ESTE ACTO PONE A LA VISTA EL CONVENIO MODIFICATORIO PARA SU RATIFICACIÓN DE CONTENIDO Y MANIFIESTEN SI SON SUYAS LAS FIRMAS QUE CALZAN EN EL MISMO. EN USO DE LA PALABRA LOS COMPARECIENTES MANIFIESTAN: UNO EN POS DE OTRO QUE RECONOCEN COMO SUYAS LAS FIRMAS "+
                    " QUE CALZAN EN EL PRESENTE CONVENIO MODIFICATORIO POR HABER SIDO PUESTAS DE SU PUÑO Y LETRA , POR LO QUE EN TÉRMINOS DEL ARTÍCULO 2890 FRACCCCIÓN III DEL CÓDIGO CIVIL EN EL ESTADO DE OAXACA , ME CERCIORÉ DE LA AUTENTICIDAD DE LAS FIRMAS Y DE LA VOLUNTAD DE LAS PARTES "+
                    ", ASI COMO DEL CONTENIDO DEL MISMO, PREVIO EL PAGO DE DERECHOS DE ARANCEL AL FISCO DEL ESTADO SEGÚN COMPROBANTE EXPEDIDO DE LA SECRETAÍA DE FINANZAS DEL GOBIERNO DEL ESTADO DE OAXACA, DOY FE...........";

                List<string> listaAux = globales.justificar(cuerpo1, 100);
                cuerpo1 = string.Empty;
                listaAux.ForEach(o => cuerpo1 += o + "\n");

                List<string> listaAux2 = globales.justificar(cuerpo2, 100);
                cuerpo2 = string.Empty;
                listaAux2.ForEach(o => cuerpo2 += o + "\n");

                List<string> listaAux3 = globales.justificar(cuerpo3, 100);
                cuerpo3 = string.Empty;
                listaAux3.ForEach(o => cuerpo3 += o + "\n");

                List<string> listaAux4 = globales.justificar(cuerpo4, 100);
                cuerpo4 = string.Empty;
                listaAux.ForEach(o => cuerpo4 += o + "\n");

                List<string> listaAux5 = globales.justificar(cuerpo5, 95);
                cuerpo5 = string.Empty;
                listaAux5.ForEach(o => cuerpo5 += o + "\n");


                List<string> listaAux7 = globales.justificar(clausulaPrimera, 100);
                clausulaPrimera = string.Empty;
                listaAux7.ForEach(o => clausulaPrimera += o + "\n");

                List<string> listaAux8 = globales.justificar(clausulaSegunda, 100);
                clausulaSegunda = string.Empty;
                listaAux8.ForEach(o => clausulaSegunda += o + "\n");

                List<string> listaAux9 = globales.justificar(clausulaTercera, 100);
                clausulaTercera = string.Empty;
                listaAux9.ForEach(o => clausulaTercera += o + "\n");

                List<string> listaAux10 = globales.justificar(clausulaCaurta, 100);
                clausulaCaurta = string.Empty;
                listaAux10.ForEach(o => clausulaCaurta += o + "\n");

                List<string> listaAux11 = globales.justificar(clausulaQuinta, 100);
                clausulaQuinta = string.Empty;
                listaAux11.ForEach(o => clausulaQuinta += o + "\n");

                List<string> listaAux13 = globales.justificar(cuerpo6, 100);
                cuerpo6 = string.Empty;
                listaAux13.ForEach(o => cuerpo6 += o + "\n");

                object[] parametros = { "cuerpo1", "cuerpo2", "cuerpo3", "cuerpo4", "cuerpo5", "clausulaPrimera", "clausulaSegunda", "clausulaTercera", "clausulaCaurta", "clausulaQuinta", "DIR", "nombre_em", "f_autorizacionU","cuerpo6" };
                object[] valor = {      cuerpo1,  cuerpo2,   cuerpo3,   cuerpo4,   cuerpo5,    clausulaPrimera,    clausulaSegunda,   clausulaTercera,  clausulaCaurta,    clausulaQuinta, dir, nombre_em, "Oaxaca de Juárez,Oax.,a " + f_autorizacionU,cuerpo6};
                object[][] enviarParametros = new object[27][];

                enviarParametros[0] = parametros;
                enviarParametros[1] = valor;

                globales.reportes("frmConvenioModif", "pagareHipo", new object[0], "", false, enviarParametros);
                this.Cursor = Cursors.Default;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Genera();
        }

        private void txtFecha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                Genera();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
