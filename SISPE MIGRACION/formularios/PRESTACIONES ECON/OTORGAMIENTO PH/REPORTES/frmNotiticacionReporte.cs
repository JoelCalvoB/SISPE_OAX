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
    public partial class frmNotiticacionReporte : Form
    {

        string expediente = string.Empty;
        bool bandera;
        int numdoc = 0;

        public frmNotiticacionReporte()
        {
            InitializeComponent();
            //  textBox1.Enabled = true;
            DateTime fechaActual = DateTime.Now;
            txtFecha.Text = Convert.ToString(fechaActual);
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
             if (string.IsNullOrWhiteSpace(txtFecha.Text) && textBox1.Enabled == false && string.IsNullOrWhiteSpace( txtNdoc.Text)) { DialogResult dialogo = globales.MessageBoxExclamation("INGRESA UNA FECHA Y NÚMERO DE DOC", "VERIFICAR", globales.menuPrincipal); return; };
            string finalidad = string.Empty;
            string query = string.Empty;
            string qry = string.Empty;
            string nombre_not = string.Empty;
            string n_notario = string.Empty;
            string f_autorizacion = string.Empty;
            string nombre_em = string.Empty;
            string descripcion = string.Empty;
            string cap_prest = string.Empty;
            string int_prest = string.Empty;
            string tot_prest = string.Empty;
            string tot_prim = string.Empty;
            string tot_unit = string.Empty;
            string expediente = string.Empty;
            string sec = string.Empty;
            string dir = string.Empty;
            string direc_inmu = string.Empty;
            string documento = string.Empty;
            string letraimporte = string.Empty;
            string plazo = string.Empty;
            string plazoa = string.Empty;
            double diferencia = 0.0;
            int c_quincenas = 0;
            string quincmen = string.Empty;
            string fechaletra = string.Empty;

            DateTime fecha = Convert.ToDateTime(txtFecha.Text);
            if (radioButton1.Checked) finalidad = "in ('04','05','07')";
            if (radioButton2.Checked) finalidad = "in ('01','02','03')";
            if (radioButton3.Checked) finalidad = "in ('06')";

            Cursor.Current = Cursors.WaitCursor;


            if (chkfolio.Checked == true)
            {
                query = "select a2.finalidad,a2.expediente,a2.sec,a1.nombre_em,a1.descripcion,a1.direc_inmu,a2.f_autorizacion,a2.cap_prest,a2.int_prest,a2.tot_prest,a2.tot_prim,a2.tot_unit,a2.plazo,a2.plazoa,a3.n_notif,a3.c_notif  from datos.p_hipote a1 LEFT JOIN datos.h_solici a2 ON a1.folio=a2.expediente LEFT JOIN datos.h_snotif a3 ON a2.expediente=a3.expediente where a2.expediente='{0}' and a2.finalidad {1}";
                qry = string.Format(query, textBox1.Text, finalidad);

            }
            else
            {
                query = "select a2.finalidad,a2.expediente,a2.sec,a1.nombre_em,a1.descripcion,a1.direc_inmu,a2.f_autorizacion,a2.cap_prest,a2.int_prest,a2.tot_prest,a2.tot_prim,a2.tot_unit,a2.plazo,a2.plazoa,a3.n_notif,a3.c_notif from datos.p_hipote a1 LEFT JOIN datos.h_solici a2 ON a1.folio=a2.expediente LEFT JOIN datos.h_snotif a3 ON a2.expediente=a3.expediente where a2.f_autorizacion='{0}' and a2.finalidad {1} order by expediente";
                qry = string.Format(query, string.Format("{0:yyyy-MM-dd}", fecha), finalidad);
            }

            List<Dictionary<string, object>> resnotif = globales.consulta(qry);
            if (resnotif.Count <= 0) { DialogResult dialogo1 = globales.MessageBoxExclamation("NO SE ENCUNETRA INFORMACIÓN CON LOS PARAMETROS SELECCIONADOS", "VERIFICAR", globales.menuPrincipal); return; }

                foreach (var item in resnotif)
                {
                    this.expediente = Convert.ToString(item["expediente"]);
                    sec = Convert.ToString(item["sec"]);
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    descripcion = Convert.ToString(item["descripcion"]);
                    direc_inmu = Convert.ToString(item["direc_inmu"]);
                    f_autorizacion = Convert.ToString(item["f_autorizacion"]);
                    string frecortada = "select * from datos.fechaletra('{0}')";
                    string pasa = string.Format(frecortada, string.Format("{0:d}", Convert.ToDateTime(fecha)));
                    List<Dictionary<string, object>> frec = globales.consulta(pasa);
                    frecortada = Convert.ToString(frec[0]["fechaletra"]).Replace("Oaxaca de Juárez,Oax.,a ", "");
                    cap_prest = Convert.ToString(item["cap_prest"]);
                    letraimporte = globales.convertirNumerosLetras(cap_prest, true);  // letra importe
                    int_prest = Convert.ToString(item["int_prest"]);
                    tot_prest = Convert.ToString(item["tot_prest"]);
                    tot_prim = Convert.ToString(item["tot_prim"]);
                    diferencia = Convert.ToDouble(tot_prest) - Convert.ToDouble(tot_prim);
                    tot_unit = Convert.ToString(item["tot_unit"]);
                    nombre_not = Convert.ToString(item["n_notif"]);
                    n_notario = Convert.ToString(item["c_notif"]);
                    plazoa = Convert.ToString(item["plazoa"]);    // cantidad
                    plazo = Convert.ToString(item["plazo"]);   // periodo
                    if (plazo == "Q")
                    {
                        quincmen = "quincenales";
                        c_quincenas = (Convert.ToInt32(plazoa) * 24) - 1;
                    }
                    if (plazo == "M")
                    {
                        quincmen = "mensuales";

                        c_quincenas = (Convert.ToInt32(plazoa) * 12) - 1;

                    }
                    query = "select nombre from catalogos.firmas where clave='DIR'";
                    List<Dictionary<string, object>> fir = globales.consulta(query);
                    dir = Convert.ToString(fir[0]["nombre"]);

                    query = "select * from datos.fechaletra('{0}')";
                    DateTime fechaActual = DateTime.Now;
                    qry = string.Format(query, string.Format("{0:yyyy-MM-dd}", fechaActual));
                    List<Dictionary<string, object>> fec = globales.consulta(qry);
                    fechaletra = Convert.ToString(fec[0]["fechaletra"]);

                    query = "SELECT * FROM datos.h_sdocum WHERE expediente='{0}' and sec='{1}' and ubicacion in ('N','X');";
                    qry = string.Format(query, this.expediente, sec);
                    List<Dictionary<string, object>> documentos = globales.consulta(qry);
                    object[] aux2 = new object[documentos.Count];
                    int contador = 0;
                    foreach (var item1 in documentos)
                    {
                        documento = Convert.ToString(item1["documento"]);

                    if ($"{item1["original"]}".Contains("X")) {
                        documento += " (ORIGINAL)";
                    } else if (string.IsNullOrWhiteSpace($"{item1["original"]}") || $"{item1["copia"]}".Contains("X")) {
                        documento += " (COPIA)";
                    }

                    object[] t1 = {"", documento };
                        aux2[contador] = t1;
                        contador++;
                    }

                    // selección de reporte
                    
                    string trabajadorjubilado = plazo == "Q" ? "TRABAJADOR" : "JUBILADO";

                    string claus = $"SELECT * FROM datos.h_snotif where expediente='{this.expediente}';";
                    List<Dictionary<string, object>> lleva = globales.consulta(claus);
                     string clausula2C = Convert.ToString(lleva[0]["t_notif"]);

                if (bandera==false)
                {
                     numdoc = Convert.ToInt32(txtNdoc.Text);
                    bandera = true;
                }
                else
                {
                    numdoc++;
                }


                if (radioButton1.Checked)    // COMPRA
                    {


                        string cuerpo = "Con fundamentos en los artículos 72, 73, 74, 75, 89 Fracción I, II inciso f, y 90 de la Ley de Pensiones para los Trabajadores del Gobierno del Estado de Oaxaca, en relación con el artículo 5 inciso V, XII del Reglamento" +
                     " Interno de la Oficina de Pensiones del Estado de Oaxaca y el artículo 95 del Reglamento de Operación de la Oficina de Pensiones del Estado de Oaxaca, me permito informar a usted, para fines de otorgamiento de la escritura de la hipoteca respectiva" +
                     ", que el Consejo Directivo de Pensiones en sesión celebrada el día " + frecortada + " ,acordó conceder a  " + nombre_em + " ,quién presta sus servicios en " + descripcion + " del Gobierno del Estado, un préstamo Hipotecario por la cantidad de " + string.Format("{0:C}", Convert.ToDouble(cap_prest)) +
                     " (" + letraimporte + "), a " + plazoa + " años de plazo para pagarlos, quedando de la siguiente manera:";

                        string clausula1 = "El importe del préstamo será entregado en UNA emisión.";
                        string clausula2 = clausula2C;
                        string clausula3 = $"En el caso de que el {trabajadorjubilado} dejara definitivamente de cotizar al Fondo de Pensiones, se le podrá conceder un plazo máximo de DOCE MESES para liquidar el saldo que tenga" +
                             " pasado ese tiempo se darán por vencidas todas las mensualidades pendientes del presente contrato y a partir de ese momento le será cargado el DOS PUNTO CINCO POR" +
                             "CIENTO de interés MENSUAL hasta la terminación del pago de la hipoteca. Los gastos de dicho trámite serán por cuenta del intersado.";
                        string clausula4 = $"Si el {trabajadorjubilado} deudor  estando en nómina o en perido de una licencia, se atrasa más de dos mensualidades, también se considerará vencido el plazo de los abonos pendientes de" +
                             "cubrir y se porcederá en la vía legal que corresponda.";
                        string clausula5 = $"Si el {trabajadorjubilado} utiliza el préstamo para otros fines que no sea la compra de Terreno, se procederá de inmediato a la cancelación de la hipoteca, exigiéndosele la devolución del mismo";
                        string clausula6 = $"En este acto el deudor otorga su pleno consentimiento para que los descuentos de su préstamo hipotecario le sean descontados de su sueldo que percibe como {trabajadorjubilado} de Gobierno del Estado" +
                            "Si no se efecturara el supuesto previsto en el párrafo anterior, el deudor realizará el pago o los pagos a que esté obligado en la quincena siguiente a cobrar o directamente en la caja de la Oficina " +
                            "de Pensiones del Gobierno del Estado de Oaxaca, siempre y cuando se encuentre en los siguientes casos";
                        string clausula6A = "A).- Cuando la Dependencia u Orgamismo correspondiente le suspenda el descuento, durante el tiempo que permanezca la suspensión";
                        string clausula6B = "B).- Cuando vaya a solicitar licencia sin goce de sueldo, en cuyo caso cubrirá con antelación los pagos que corresponden al perido de la licencia, salvo quien vaya a cubrir su plaza, se comprometa a realizarlos" +
                            "oportunamente previa firma de convenio ante el Departamento Jurídico de esta Oficina";
                        string clausula6C = "C).- Si tuviera pagos vencidos, independientemente de tener operando sus descuentos en el Organismo donde labora.";
                        string clausula7 = "La hipoteca recerá sobre la propiedad que se localizan en " + direc_inmu + ".";
                        string anexo8 = "Para tal efecto, me permito anexar los siguiente documentos:";


                        string cuerpo2 = $"Si el {trabajadorjubilado} no exhibe el testimonio de hipoteca debidamente inscrito ante el Instituto de la Función Registral del Estado de Oaxaca, dentro de los noventa días hábiles contados " +
                            "a partir de la fecha en que se reciba el presente oficio, quedará sin efecto la autorización de dicho crédito";
                        string cuerpo3 = "Los gastos de la escrituración serán por cuenta del interesado y a nombre y representación tanto del Consejo Directivo como de la Oficina de Pensiones" +
                            "  del Estado de Oaxaca, firmará el Director General de esta última, " + dir+".";

                        List<string> listaAux = globales.justificar(cuerpo, 90);
                        cuerpo = string.Empty;
                        listaAux.ForEach(o => cuerpo += o + "\n");

                        List<string> listaAux1 = globales.justificar(clausula1, 65);
                        clausula1 = string.Empty;
                        listaAux1.ForEach(o => clausula1 += o + "\n");

                        List<string> listaAux2 = globales.justificar(clausula2, 80);
                        clausula2 = string.Empty;
                        listaAux2.ForEach(o => clausula2 += o + "\n");

                        List<string> listaAux3 = globales.justificar(clausula3, 80);
                        clausula3 = string.Empty;
                        listaAux3.ForEach(o => clausula3 += o + "\n");

                        List<string> listaAux4 = globales.justificar(clausula4, 80);
                        clausula4 = string.Empty;
                        listaAux4.ForEach(o => clausula4 += o + "\n");

                        List<string> listaAux5 = globales.justificar(clausula5, 80);
                        clausula5 = string.Empty;
                        listaAux5.ForEach(o => clausula5 += o + "\n");

                        List<string> listaAux6 = globales.justificar(clausula6A, 80);
                        clausula6A = string.Empty;
                        listaAux6.ForEach(o => clausula6A += o + "\n");

                        List<string> listaAux7 = globales.justificar(clausula6B, 80);
                        clausula6B = string.Empty;
                        listaAux7.ForEach(o => clausula6B += o + "\n");

                        List<string> listaAux8 = globales.justificar(clausula6C, 80);
                        clausula6C = string.Empty;
                        listaAux8.ForEach(o => clausula6C += o + "\n");

                        List<string> listaAux9 = globales.justificar(clausula6, 80);
                        clausula6 = string.Empty;
                        listaAux9.ForEach(o => clausula6 += o + "\n");

                        List<string> listaAux10 = globales.justificar(clausula7, 80);
                        clausula7 = string.Empty;
                        listaAux10.ForEach(o => clausula7 += o + "\n");

                        List<string> listaAux11 = globales.justificar(anexo8, 80);
                        anexo8 = string.Empty;
                        listaAux11.ForEach(o => anexo8 += o + "\n");


                        DateTime anio = DateTime.Now;
                        string año = Convert.ToString(anio.Year);

                    string logo = globales.getImagen(globales.imagenesSispe.logoreportes);
                    object[] parametros = { "fechaletra", "nombre_not", "n_notario", "cuerpo", "cap_prest", "int_prest", "quincmen", "tot_prest", "tot_prim", "tot_unit", "diferencia", "clausula1", "clausula2", "clausula3", "clausula4", "clausula5", "clausula6", "clausula6A", "clausula6B", "clausula6C", "clausula7", "cuerpo2", "cuerpo3", "dir", "c_quincenas", "anexo8", "folioarch" , "logo" };
                        object[] valor = { fechaletra, nombre_not, n_notario, cuerpo, cap_prest, int_prest, quincmen, tot_prest, tot_prim, tot_unit, Convert.ToString(diferencia), clausula1, clausula2, clausula3, clausula4, clausula5, clausula6, clausula6A, clausula6B, clausula6C, clausula7, cuerpo2, cuerpo3, dir, Convert.ToString(c_quincenas), anexo8, numdoc + "/" + año, logo };
                        object[][] enviarParametros = new object[2][];

                        enviarParametros[0] = parametros;
                        enviarParametros[1] = valor;

                        globales.reportes("ReporteCompra", "notificacion", aux2, "", false, enviarParametros);
                        this.Cursor = Cursors.Default;
                    
                    }

                    if (radioButton2.Checked)    // CONSTRUCCIÓN
                    {
                        string cuerpo = "Con fundamentos en los artículos 72, 73, 74, 75, 89 Fracción I, II inciso f, y 90 de la Ley de Pensiones para los Trabajadores del Gobierno del Estado de Oaxaca, en relación con el artículo 5 fracciones V, XII del Reglamento" +
                       " Interno de la Oficina de Pensiones del Estado de Oaxaca y el artículo 95 del Reglamento de Operación de la Oficina de Pensiones del Estado de Oaxaca, me permito informar a usted, para fines de otorgamiento de la escritura de la hipoteca respectiva" +
                       ", que el Consejo Directivo de Pensiones en sesión celebrada el día " + frecortada + " ,acordó conceder a " + nombre_em + " ,quién presta sus servicios  en " + descripcion + " del Gobierno del Estado de Oaxaca, un préstamo Hipotecario por la cantidad de " + string.Format("{0:C}", Convert.ToDouble(cap_prest)) +
                       " (" + letraimporte + "), a " + plazoa + " años de plazo para pagarlos, quedando de la siguiente manera:";

                        string clausula1 = "El importe del préstamo será entregado en TRES emisiones.";
                        string clausula2 = $"En el caso de que el {trabajadorjubilado} dejara definitivamente de cotizar al Fondo de Pensiones, se le podrá conceder un plazo máximo de DOCE MESES para liquidar el saldo que tenga ," +
                             "pasado ese tiempo se darán por vencidas todas las mensualidades pendientes del presente contrato y a partir de ese momento le será cargado el DOS PUNTO CINCO POR" +
                             "CIENTO de interés MENSUAL hasta la terminación del pago de la hipoteca. Los gastos de dicho trámite serán por cuenta del intersado.";
                        string clausula3 = "El capital mutuado causará intereses ordinarios sobre saldos insolutos, a una tasa de interés anual fija del OCHO POR CIENTO.";
                        string clausula4 = $"Si la o el {trabajadorjubilado} estando en servicio o en periodo de una licencia, se atrasa más de dos mensualidades, también se considerará vencido el plazo de los abonos pendientes de cubrir y se procederá en la vía legal que corresponda.";
                        string clausula5 = $"Si el {trabajadorjubilado} utiliza el préstamo para otros fines que no sea Construcción, se procederá de inmediato a la cancelación de la hipoteca, exigiéndosele la devolución del mismo.";

                        string clausula6 = $"La o El {trabajadorjubilado} se obliga respetar íntegramente el proyecto arquitectónico que presentó en la Oficina de Pensiones del Estado de Oaxaca para este trámite. En caso de modificación alguna, perderá el derecho de obtener las emisiones posteriores " +
                            "del crédito ; salvo que se trate de las modificaciones técnicas indispensables, en cuyo caso estará obligado a solicitar la autorización a esta Oficina.";
                        string clausula7 = $"En este acto la o el {trabajadorjubilado} otorga su pleno consentimiento para que los descuentos de su préstamo hipotecario le sean descontados de su sueldo que percibe como empleado del Gobierno del Estado de Oaxaca. Si no se efectuara el supuesto previsto" +
                            $" en el párrafo anterior, el {trabajadorjubilado} realizará el pago o los pagos a que este obligado , en la quincena siguiente a cobrar o directamente en la caja de la Oficina" +
                            " de Pensiones  del Estado de Oaxaca , siempre y cuando se encuentren en los siguientes casos:";
                        string clausula7A = "A).- Cuando la Dependencia u Organismo correspondiente le suspenda el descuento,durante el tiempo que permaneza la suspensión.";
                        string clausula7B = "B).- Cuando vaya a solicitar licencia sin goce de sueldo, en cuyo caso cubrirá  con antelación los pagos que correspondan al periodo de licencia , salvo quien vaya a cubrir su plazo se comprometa a realizarlos " +
                            "oportunamente previa firma de convenio ante el Departamento Jurídico de esta Oficina. ";
                        string clausula7C = "C).- Si tuviera pagos vencidos, independientemente de tener operando sus descuentos en el Organismo donde labora." +
                            "La hipoteca recaerá sobre la propiedad que se localiza en: " + direc_inmu;
                        string clausula8 = "Si La o El trabajador, a partir de la fecha en que se reciba el recurso de la tercerá y última emisión no comprueba en tiempo y forma, se le aplicarán descuentos dobles vía nómina hasta regularizar dicho crédito";

                        string cuerpo2 = $"Si el {trabajadorjubilado} no exhibe el testimonio de hipoteca debidamente inscrito ante el Instituto de la Función Registral del Estado de Oaxaca, dentro de los noventa días hábiles contados" +
                            " a partir de la fecha en que se reciba el presente oficio, quedará sin efecto la autorización de dicho crédito";
                        string cuerpo3 = "Los gastos de la escrituración serán por cuenta del interesado y a nombre y representación tanto del Consejo Directivo como de la Oficina de Pensiones" +
                            "  del Estado de Oaxaca, firmará el Director General de esta última, " + dir+".";

                        string anexo8 = "Para tal efecto, me permito anexar los siguiente documentos:";
                        Cursor.Current = Cursors.WaitCursor;


                        List<string> listaAux = globales.justificar(cuerpo, 90);
                        cuerpo = string.Empty;
                        listaAux.ForEach(o => cuerpo += o + "\n");

                        List<string> listaAux1 = globales.justificar(clausula1, 65);
                        clausula1 = string.Empty;
                        listaAux1.ForEach(o => clausula1 += o + "\n");

                        List<string> listaAux2 = globales.justificar(clausula2, 80);
                        clausula2 = string.Empty;
                        listaAux2.ForEach(o => clausula2 += o + "\n");

                        List<string> listaAux3 = globales.justificar(clausula3, 80);
                        clausula3 = string.Empty;
                        listaAux3.ForEach(o => clausula3 += o + "\n");

                        List<string> listaAux4 = globales.justificar(clausula4, 80);
                        clausula4 = string.Empty;
                        listaAux4.ForEach(o => clausula4 += o + "\n");

                        List<string> listaAux5 = globales.justificar(clausula5, 80);
                        clausula5 = string.Empty;
                        listaAux5.ForEach(o => clausula5 += o + "\n");

                        List<string> listaAux6 = globales.justificar(clausula6, 80);
                        clausula6 = string.Empty;
                        listaAux6.ForEach(o => clausula6 += o + "\n");

                        List<string> listaAux7 = globales.justificar(clausula7, 80);
                        clausula7 = string.Empty;
                        listaAux7.ForEach(o => clausula7 += o + "\n");

                        List<string> listaAux7A = globales.justificar(clausula7A, 80);
                        clausula7A = string.Empty;
                        listaAux7A.ForEach(o => clausula7A += o + "\n");

                        List<string> listaAux7B = globales.justificar(clausula7B, 80);
                        clausula7B = string.Empty;
                        listaAux7B.ForEach(o => clausula7B += o + "\n");

                        List<string> listaAux7C = globales.justificar(clausula7C, 80);
                        clausula7C = string.Empty;
                        listaAux7C.ForEach(o => clausula7C += o + "\n");

                        List<string> listaAux8 = globales.justificar(clausula8, 80);
                        clausula8 = string.Empty;
                        listaAux8.ForEach(o => clausula8 += o + "\n");

                        List<string> listaAux11 = globales.justificar(anexo8, 80);
                        anexo8 = string.Empty;
                        listaAux11.ForEach(o => anexo8 += o + "\n");
                        DateTime anio = DateTime.Now;
                        string año = Convert.ToString(anio.Year);

                    string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                    object[] parametros = { "fechaletra", "nombre_not",     "n_notario", "cuerpo", "cap_prest", "int_prest", "quincmen", "tot_prest", "tot_prim", "tot_unit",            "diferencia",    "clausula1", "clausula2", "clausula3", "clausula4", "clausula5", "clausula6", "clausula7", "clausula7A", "clausula7B", "clausula7C", "clausula8", "cuerpo2", "cuerpo3", "dir",              "c_quincenas",   "anexo8", "folioarch", "logo" };
                        object[] valor = { fechaletra,      nombre_not,         n_notario,     cuerpo, cap_prest,   int_prest,    quincmen,   tot_prest,   tot_prim,   tot_unit, Convert.ToString(diferencia), clausula1,    clausula2, clausula3,     clausula4,  clausula5,   clausula6,   clausula7,   clausula7A,   clausula7B,   clausula7C,   clausula8,   cuerpo2,   cuerpo3,   dir, Convert.ToString(c_quincenas), anexo8, numdoc + "/" + año, logo };
                        object[][] enviarParametros = new object[2][];

                        enviarParametros[0] = parametros;
                        enviarParametros[1] = valor;

                        globales.reportes("ReporteConstruccion", "notificacion", aux2, "", false, enviarParametros);
                        this.Cursor = Cursors.Default;
                    }

                    if (radioButton3.Checked)    /// GRAVAMEN
                    {
                        string cuerpo = "Con fundamentos en los artículos 72, 73, 74, 75, 89 Fracción I, II inciso f, y 90 de la Ley de Pensiones para los Trabajadores del Gobierno del Estado de Oaxaca, en relación con el artículo 5 inciso V, XII del Reglamento" +
                         " Interno de la Oficina de Pensiones del Estado de Oaxaca y el artículo 95 del Reglamento de Operación de la Oficina de Pensiones del Estado de Oaxaca, me permito informar a usted, para fines de otorgamiento de la escritura de la hipoteca respectiva" +
                         ", que el Consejo Directivo de Pensiones en sesión celebrada el día " + frecortada + " ,acordó conceder a " + nombre_em + " ,quién presta sus servicios  en " + descripcion + " del Gobierno del Estado, un préstamo Hipotecario por la cantidad de " + string.Format("{0:C}", Convert.ToDouble(cap_prest)) +
                         " (" + letraimporte + "), a " + plazoa + "años de plazo para pagarlos, quedando de la siguiente manera:";

                        string clausula1 = "El importe del préstamo será entregado en UNA emisión.";
                        string clausula2 = $"En el caso de que el {trabajadorjubilado} dejara definitivamente de cotizar al Fondo de Pensiones, se le podrá conceder un plazo máximo de DOCE MESES para liquidar el saldo que tenga " +
                             " pasado ese tiempo se darán por vencidas todas las mensualidades pendientes del presente contrato y a partir de ese momento le será cargado el DOS PUNTO CINCO POR" +
                             " CIENTO de interés MENSUAL hasta la terminación del pago de la hipoteca. Los gastos de dicho trámite serán por cuenta del intersado.";
                        string clausula3 = $"Si el {trabajadorjubilado} deudor  estando en nómina o en perido de una licencia, se atrasa más de dos mensualidades, también se considerará vencido el plazo de los abonos pendientes de" +
                             " cubrir y se porcederá en la vía legal que corresponda.";
                        string clausula4 = $"Si el {trabajadorjubilado} utiliza el préstamo para otros fines que no sea Redimir Gravamen, se procederá de inmediato a la cancelación de la hipoteca, exigiéndosele la devolución del mismo.";
                        string clausula5 = $"En este actor el deudor otorga su pleno consentimiento parqa que los descuentos de su préstamo hipotecario le sean descontados de su sueldo que percibe como {trabajadorjubilado} de Gobierno del Estado" +
                            " Si no se efecturara el supuesto previsto en el párrafo anterior, el deudor realizará el pago o los pagos a que esté obligado en la quincena siguiente a cobrar o directamente en la caja de la Oficina " +
                            " de Pensiones del Gobierno del Estado de Oaxaca, siempre y cuando se encuentre en los siguientes casos";
                        string clausula5A = "A).- Cuando la Dependencia u Orgamismo correspondiente le suspenda el descuento, durante el tiempo que permanezca la suspensión";
                        string clausula5B = "B).- Cuando vaya a solicitar licencia sin goce de sueldo, en cuyo caso cubrirá con antelación los pagos que corresponden al perido de la licencia, salvo quien vaya a cubrir su plaza, se comprometa a realizarlos" +
                            " oportunamente previa firma de convenio ante el Departamento Jurídico de esta Oficina.";
                        string clausula5C = "C).- Si tuviera pagos vencidos, independientemente de tener operando sus descuentos en el Organismo donde labora.";
                        string clausula6 = "La hipoteca recerá sobre la propiedad que se localizan en " + direc_inmu + ".";

                        string cuerpo2 = $"Si el {trabajadorjubilado} no exhibe el testimonio de hipoteca debidamente inscrito ante el Instituto de la Función Registral del Estado de Oaxaca, dentro de los noventa días hábiles contados" +
                            " a partir de la fecha en que se reciba el presente oficio, quedará sin efecto la autorización de dicho crédito";
                        string cuerpo3 = "Los gastos de la escrituración serán por cuenta del interesado y a nombre y representación tanto del Consejo Directivo como de la Oficina de Pensiones" +
                            " del Estado de Oaxaca, firmará el Director General de esta ultima, " + dir +".";
                        string anexo8 = "Para tal efecto, me permito anexar los siguiente documentos:";

                        Cursor.Current = Cursors.WaitCursor;

                        List<string> listaAux = globales.justificar(cuerpo, 90);
                        cuerpo = string.Empty;
                        listaAux.ForEach(o => cuerpo += o + "\n");

                        List<string> listaAux1 = globales.justificar(clausula1, 65);
                        clausula1 = string.Empty;
                        listaAux1.ForEach(o => clausula1 += o + "\n");

                        List<string> listaAux2 = globales.justificar(clausula2, 80);
                        clausula2 = string.Empty;
                        listaAux2.ForEach(o => clausula2 += o + "\n");

                        List<string> listaAux3 = globales.justificar(clausula3, 80);
                        clausula3 = string.Empty;
                        listaAux3.ForEach(o => clausula3 += o + "\n");

                        List<string> listaAux4 = globales.justificar(clausula4, 80);
                        clausula4 = string.Empty;
                        listaAux4.ForEach(o => clausula4 += o + "\n");

                        List<string> listaAux5 = globales.justificar(clausula5, 80);
                        clausula5 = string.Empty;
                        listaAux5.ForEach(o => clausula5 += o + "\n");

                        List<string> listaAux6 = globales.justificar(clausula5A, 80);
                        clausula5A = string.Empty;
                        listaAux6.ForEach(o => clausula5A += o + "\n");

                        List<string> listaAux7 = globales.justificar(clausula5B, 80);
                        clausula5B = string.Empty;
                        listaAux7.ForEach(o => clausula5B += o + "\n");

                        List<string> listaAux8 = globales.justificar(clausula5C, 80);
                        clausula5C = string.Empty;
                        listaAux8.ForEach(o => clausula5C += o + "\n");

                        List<string> listaAux9 = globales.justificar(clausula6, 80);
                        clausula6 = string.Empty;
                        listaAux9.ForEach(o => clausula6 += o + "\n");

                        List<string> listaAux11 = globales.justificar(anexo8, 80);
                        anexo8 = string.Empty;
                        listaAux11.ForEach(o => anexo8 += o + "\n");



                        DateTime anio = DateTime.Now;
                        string año = Convert.ToString(anio.Year);

                    string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                    object[] parametros = { "fechaletra", "nombre_not", "n_notario", "cuerpo", "cap_prest", "int_prest", "quincmen", "tot_prest", "tot_prim", "tot_unit", "diferencia", "clausula1", "clausula2", "clausula3", "clausula4", "clausula5", "clausula5A", "clausula5B", "clausula5C", "clausula6", "cuerpo2", "cuerpo3", "dir", "c_quincenas", "anexo8", "folioarch", "logo" };
                        object[] valor = { fechaletra, nombre_not, n_notario, cuerpo, cap_prest, int_prest, quincmen, tot_prest, tot_prim, tot_unit, Convert.ToString(diferencia), clausula1, clausula2, clausula3, clausula4, clausula5, clausula5A, clausula5B, clausula5C, clausula6, cuerpo2, cuerpo3, dir, Convert.ToString(c_quincenas), anexo8, numdoc + "/" + año, logo };
                        object[][] enviarParametros = new object[2][];

                        enviarParametros[0] = parametros;
                        enviarParametros[1] = valor;

                        globales.reportes("ReportGravamen", "notificacion", aux2, "", false, enviarParametros);
                        this.Cursor = Cursors.Default;
                    }
                    
                
                
                
            }
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkfolio.Checked)
            {
                txtFecha.Enabled = false;
                textBox1.Enabled = true;
                label1.Enabled = true;
                textBox1.Select();
                label1.Visible = true;

            }

            if (chkfolio.Checked == false)
            {
                txtFecha.Enabled = true;
                textBox1.Enabled = false;
                label1.Enabled = false;
                txtFecha.Select();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
