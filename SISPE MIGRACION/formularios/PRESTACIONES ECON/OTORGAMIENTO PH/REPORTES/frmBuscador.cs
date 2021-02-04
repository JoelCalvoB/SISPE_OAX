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
    public partial class frmBuscador : Form

    {
        Dictionary<string, object> diccionarioGlobal;
        List<Dictionary<string, Object>> ListaGlobal;

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtExpediente.Text))
            {
                globales.MessageBoxExclamation("sdf", "", globales.menuPrincipal);
                return;
            }
            visualizaReporte();
        }


        private int opcion;
        private int totalFolio;

        public frmBuscador(int opcion)
        {
            InitializeComponent();
            this.opcion = opcion;

        }


        public void visualizaReporte()
        {
            switch (opcion)
            {
                case 0:
                    metodo1();
                    break;
                case 1:
                    metodo2();
                    break;
                case 2:
                    metodo3();
                    break;
                case 3:
                    metodo4();
                    break;
                case 4:
                    metodo5();    //reporte técnico
                    break;
                case 6:
                    metodo6();    //acuerdo negativo
                    break;
                case 8:
                    metodo10();  ///convenioo
                    break;
                case 9:
                    metodo7();  // recibo
                    break;
                case 10:
                    metodo8();  ///pagare
                    break;
                case 11:
                    metodo9();  ///pagare
                    break;
                case 13:
                    metodo11();
                    break;
            }

            this.Owner.Close();
        }

        private void metodo1()
        {
            string query = "SELECT a1.rfc,a1.nombre_em,a1.direccion,a1.tel_partic,a1.descripcion,a1.ccatdes,a1.nomina,a1.f_nombram,a2.descri_finalid ,a2.plazoa,a2.f_solicitud  FROM datos.p_hipote a1 RIGHT JOIN datos.h_solici a2  ON a1.folio=a2.expediente WHERE a2.expediente='{0}' and a2.sec='{1}' limit 1;";
            string pasa = string.Format(query, txtExpediente.Text, comboBox1.SelectedIndex);
            List<Dictionary<string, object>> resultado = globales.consulta(pasa);
            if (resultado.Count >= 1)
            {
                string rfc = string.Empty;
                string nombre_em = string.Empty;
                string direccion = string.Empty;
                string tel_partic = string.Empty;
                string descripcion = string.Empty;
                string ccatdes = string.Empty;
                string nomina = string.Empty;
                string f_nombram = string.Empty;
                string descri_finalid = string.Empty;
                string plazoa = string.Empty;
                string f_solicitud = string.Empty;
                Cursor.Current = Cursors.WaitCursor;
                string f_autorizacion = string.Empty;

                foreach (var item in resultado)
                {
                    rfc = Convert.ToString(item["rfc"]);
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    direccion = Convert.ToString(item["direccion"]);
                    tel_partic = Convert.ToString(item["tel_partic"]);
                    descripcion = Convert.ToString(item["descripcion"]);
                    ccatdes = Convert.ToString(item["ccatdes"]);
                    nomina = Convert.ToString(item["nomina"]);
                    f_nombram = Convert.ToString(item["f_nombram"]);
                    descri_finalid = Convert.ToString(item["descri_finalid"]);
                    plazoa = Convert.ToString(item["plazoa"]);
                    f_solicitud = (string.IsNullOrWhiteSpace(Convert.ToString(item["f_solicitud"]))) ? string.Format("{0:yyyy-MM-dd}", DateTime.Now) : string.Format("{0:yyyy-MM-dd}",item["f_solicitud"]);
                    
                    Dictionary<string, object> diccionario = new Dictionary<string, object>();
                  
                        diccionario.Add("rfc", rfc);
                        diccionario.Add("nombre_em", nombre_em);
                        diccionario.Add("tel_partic", tel_partic);
                        diccionario.Add("direccion", direccion);
                        diccionario.Add("descripcion", descripcion);
                        diccionario.Add("ccatdes", ccatdes);
                        diccionario.Add("nomina", nomina);
                        diccionario.Add("f_nombram", f_nombram);
                        diccionario.Add("descri_finalid", descri_finalid);
                        diccionario.Add("plazoa", plazoa);
                        diccionario.Add("f_solicitud",f_solicitud);
                        this.diccionarioGlobal = diccionario;
                
                }
                query = "SELECT documento,original,copia FROM datos.h_sdocum where expediente='{0}' and sec='{1}' order by cve_docum asc;";
                pasa = string.Format(query, txtExpediente.Text, comboBox1.SelectedIndex);
                List<Dictionary<string, object>> resultado1 = globales.consulta(pasa);
                if (resultado1.Count >= 1)
                {
                    string status = "";
                    List<Dictionary<string, Object>> lista = new List<Dictionary<string, object>>();
                    foreach (var item in resultado1)
                    {
                        Dictionary<string, object> diccionario = new Dictionary<string, object>();
                        string documento = Convert.ToString(item["documento"]);
                        string original = Convert.ToString(item["original"]);
                        string copia = Convert.ToString(item["copia"]);
                        if (original == "X" && copia == "X") status = "ORIGINAL Y COPIA";
                        if (original == "X" && copia == "") status = "ORIGINAL";
                        if (copia == "X" && original == "") status = "COPIA";

                        diccionario.Add("documento", documento);
                        diccionario.Add("status", status);
                        lista.Add(diccionario);
                        this.ListaGlobal = lista;
                    }
                }
                else
                {
                    DialogResult dialog = globales.MessageBoxExclamation("NO SE ENCUENTRA LA INFORMACIÓN SOLICITADA", "VALIDAR", globales.menuPrincipal);
                    return;
                }
                object[] aux2 = new object[ListaGlobal.Count];
                int contador = 0;

                foreach (var item in ListaGlobal)
                {
                    string documento = string.Empty;
                    string status = string.Empty;
                    try
                    {
                        documento = Convert.ToString(item["documento"]);
                        status = Convert.ToString(item["status"]);
                    }
                    catch
                    {

                    }
                    object[] tt1 = { documento, status };
                    aux2[contador] = tt1;
                    contador++;
                }

                
                    rfc = Convert.ToString(diccionarioGlobal["rfc"]);
                    nombre_em = Convert.ToString(diccionarioGlobal["nombre_em"]);
                    direccion = Convert.ToString(diccionarioGlobal["direccion"]);
                    tel_partic = Convert.ToString(diccionarioGlobal["tel_partic"]);
                    descripcion = Convert.ToString(diccionarioGlobal["descripcion"]);
                    ccatdes = Convert.ToString(diccionarioGlobal["ccatdes"]);
                    nomina = Convert.ToString(diccionarioGlobal["nomina"]);
                    string f_nombrami = Convert.ToString(diccionarioGlobal["f_nombram"]);
                    descri_finalid = Convert.ToString(diccionarioGlobal["descri_finalid"]);
                    plazoa = Convert.ToString(diccionarioGlobal["plazoa"]);
                    string fe = $"select * from datos.fechaletra('{diccionarioGlobal["f_solicitud"]}') as fecha";
                    List<Dictionary<string, object>> res = globales.consulta(fe);
                    string fechaactual = Convert.ToString(res[0]["fecha"]);

                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);
                    object[] parametros = { "rfc", "nombre_em", "tel_partic", "direccion", "ccatdes", "nomina", "f_nombram", "descri_finalid", "plazoa", "descripcion", "fecha", "logo" };
                    object[] valor = { rfc, nombre_em, tel_partic, direccion, ccatdes, nomina, globales.parseDateTime(globales.convertDatetime(f_nombrami)), descri_finalid, plazoa, descripcion, fechaactual, logo };
                    object[][] enviarParametros = new object[2][];

                    enviarParametros[0] = parametros;
                    enviarParametros[1] = valor;
                

                    globales.reportes("reporteSolicitudHipote", "repoSoliHip", aux2, "", false, enviarParametros);
                    this.Cursor = Cursors.Default;
             
            }
            return;

        }

        private void metodo2()
        {
            string query = "SELECT a1.rfc,a1.nombre_em,a1.direccion,a1.tel_partic,a1.descripcion,a1.ccatdes,a1.nomina,a1.f_nombram,a2.descri_finalid ,a2.plazoa,a1.direc_inmu,a2.f_solicitud FROM datos.p_hipote a1 RIGHT JOIN datos.h_solici a2  ON a1.folio=a2.expediente WHERE a2.expediente='{0}' and a2.sec='{1}' limit 1;";
            string pasa = string.Format(query, txtExpediente.Text, comboBox1.SelectedIndex);
            List<Dictionary<string, object>> resultado = globales.consulta(pasa);
            if (resultado.Count >= 1)
            {
                string rfc = string.Empty;
                string nombre_em = string.Empty;
                string direccion = string.Empty;
                string tel_partic = string.Empty;
                string descripcion = string.Empty;
                string ccatdes = string.Empty;
                string nomina = string.Empty;
                string f_nombram = string.Empty;
                string descri_finalid = string.Empty;
                string plazoa = string.Empty;
                string direc_inmu = string.Empty;
                string sec = string.Empty;
                string reviso = string.Empty;
                DateTime f_solicitud;
                string tec = string.Empty;
                string jur = string.Empty;
                if (comboBox1.SelectedIndex == 0) sec = "INICIAL";
                if (comboBox1.SelectedIndex == 1) sec = "PRIMERA AMPLIACIÓN";
                if (comboBox1.SelectedIndex == 2) sec = "SEGUNDA AMPLIACIÓN";
                if (comboBox1.SelectedIndex == 3) sec = "TERCERA AMPLIACIÓN";

                Cursor.Current = Cursors.WaitCursor;
                

                foreach (var item in resultado)
                {
                    rfc = Convert.ToString(item["rfc"]);
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    direccion = Convert.ToString(item["direccion"]);
                    tel_partic = Convert.ToString(item["tel_partic"]);
                    descripcion = Convert.ToString(item["descripcion"]);
                    ccatdes = Convert.ToString(item["ccatdes"]);
                    nomina = Convert.ToString(item["nomina"]);
                    f_nombram = Convert.ToString(item["f_nombram"]);
                    descri_finalid = Convert.ToString(item["descri_finalid"]);
                    plazoa = Convert.ToString(item["plazoa"]);
                    direc_inmu = Convert.ToString(item["direc_inmu"]);
                    f_solicitud = Convert.ToDateTime(item["f_solicitud"]);
                    Dictionary<string, object> diccionario = new Dictionary<string, object>();
                    diccionario.Add("rfc", rfc);
                    diccionario.Add("nombre_em", nombre_em);
                    diccionario.Add("tel_partic", tel_partic);
                    diccionario.Add("direccion", direccion);
                    diccionario.Add("descripcion", descripcion);
                    diccionario.Add("ccatdes", ccatdes);
                    diccionario.Add("nomina", nomina);
                    diccionario.Add("f_nombram", f_nombram);
                    diccionario.Add("descri_finalid", descri_finalid);
                    diccionario.Add("plazoa", plazoa);
                    diccionario.Add("direc_inmu", direc_inmu);
                    diccionario.Add("sec", sec);
                    diccionario.Add("f_solicitud", f_solicitud);
                    this.diccionarioGlobal = diccionario;
                }
                query = "SELECT documento,original,copia FROM datos.h_sdocum where expediente='{0}' and sec='{1}';";
                pasa = string.Format(query, txtExpediente.Text, comboBox1.SelectedIndex);
                List<Dictionary<string, object>> resultado1 = globales.consulta(pasa);
                if (resultado1.Count >= 1)
                {
                    string status = "";
                    List<Dictionary<string, Object>> lista = new List<Dictionary<string, object>>();
                    foreach (var item in resultado1)
                    {
                        Dictionary<string, object> diccionario = new Dictionary<string, object>();
                        string documento = Convert.ToString(item["documento"]);
                        string original = Convert.ToString(item["original"]);
                        string copia = Convert.ToString(item["copia"]);
                        if (original == "X" && copia == "X") status = "ORIGINAL Y COPIA";
                        if (original == "X" && copia == "") status = "ORIGINAL";
                        if (copia == "X" && original == "") status = "COPIA";
                        reviso = "[ ]     [ ]     [ ]";

                        diccionario.Add("documento", documento);
                        diccionario.Add("status", status);
                        diccionario.Add("reviso", reviso);
                        lista.Add(diccionario);
                        this.ListaGlobal = lista;
                    }

                    string query3 = "SELECT nombre FROM catalogos.firmas WHERE CLAVE IN ('TEC','JUR');";
                    List<Dictionary<string, object>> firmas = globales.consulta(query3);

                    tec = Convert.ToString(firmas[0]["nombre"]);
                    jur = Convert.ToString(firmas[1]["nombre"]);
                }
                else
                {
                    DialogResult dialog2 = globales.MessageBoxExclamation("NO SE ENCUENTRA LA INFORMACIÓN SOLICITADA", "VALIDAR", globales.menuPrincipal);
                    return;
                }
                object[] aux2 = new object[ListaGlobal.Count];
                int contador = 0;

                foreach (var item in ListaGlobal)
                {
                    string documento = string.Empty;
                    string status = string.Empty;
                    try
                    {
                        documento = Convert.ToString(item["documento"]);
                        status = Convert.ToString(item["status"]);
                        reviso = Convert.ToString(item["reviso"]);
                    }
                    catch
                    {

                    }
                    object[] tt1 = { documento, status, reviso };
                    aux2[contador] = tt1;
                    contador++;
                }
                rfc = Convert.ToString(diccionarioGlobal["rfc"]);
                nombre_em = Convert.ToString(diccionarioGlobal["nombre_em"]);
                direccion = Convert.ToString(diccionarioGlobal["direccion"]);
                tel_partic = Convert.ToString(diccionarioGlobal["tel_partic"]);
                descripcion = Convert.ToString(diccionarioGlobal["descripcion"]);
                ccatdes = Convert.ToString(diccionarioGlobal["ccatdes"]);
                nomina = Convert.ToString(diccionarioGlobal["nomina"]);
                string f_nombrami = Convert.ToString(diccionarioGlobal["f_nombram"]); 
                descri_finalid = Convert.ToString(diccionarioGlobal["descri_finalid"]);
                plazoa = Convert.ToString(diccionarioGlobal["plazoa"]);
                direc_inmu = Convert.ToString(diccionarioGlobal["direc_inmu"]);
                sec = Convert.ToString(diccionarioGlobal["sec"]);
                f_solicitud = Convert.ToDateTime(diccionarioGlobal["f_solicitud"]);
                string fe = "select * from datos.fechaletra(CURRENT_DATE) as fecha";
                List<Dictionary<string, object>> res = globales.consulta(fe);
                string fechaactual = Convert.ToString(res[0]["fecha"]);

                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                object[] parametros = { "rfc", "nombre_em", "tel_partic", "direccion", "ccatdes", "descripcion", "direc_inmu", "sec", "f_solicitud", "tec", "jur", "logo" };
                object[] valor = { rfc, nombre_em, tel_partic, direccion, ccatdes, descripcion, direc_inmu, sec, string.Format("{0:dd/MM/yy}", f_solicitud), tec, jur , logo };
                object[][] enviarParametros = new object[2][];

                enviarParametros[0] = parametros;
                enviarParametros[1] = valor;

                globales.reportes("reporteCedulaPh", "repoSoliHip", aux2, "", false, enviarParametros);
                this.Cursor = Cursors.Default;

            }
            DialogResult dialog = globales.MessageBoxExclamation("NO SE ENCUENTRA INFORMACIÓN", "VERIFICAR", globales.menuPrincipal);

            return;
        }

        private void metodo3()
        {
            string query = "SELECT a1.nombre_em,a1.rfc,a1.direc_inmu,a2.nombre FROM datos.p_hipote a1 JOIN datos.h_eavalu a2 ON a1.folio=a2.expediente WHERE a2.expediente='{0}' LIMIT 1 ;";
            string pasa = string.Format(query, txtExpediente.Text);
            List<Dictionary<string, object>> resultado = globales.consulta(pasa);
            string nombre_em = string.Empty;
            string rfc = string.Empty;
            string direc_inmu = string.Empty;
            string nombre = string.Empty;
            string coor_nombre = string.Empty;
            string cat_nombre = string.Empty;
            string coor_cargo = string.Empty;
            string cat_cargo = string.Empty;
            string fechaletra = string.Empty;

            object[] aux2 = new object[resultado.Count];
            int contador = 0;

            Cursor.Current = Cursors.WaitCursor;


            if (resultado.Count > 0)
            {
                nombre_em = Convert.ToString(resultado[0]["nombre_em"]);
                rfc = Convert.ToString(resultado[0]["rfc"]);
                direc_inmu = Convert.ToString(resultado[0]["direc_inmu"]);
                query = "SELECT nombre,cargo FROM catalogos.firmas where clave in ('CAT','COOR');";
                List<Dictionary<string, object>> rest = globales.consulta(query);
                cat_nombre = Convert.ToString(rest[0]["nombre"]);
                cat_cargo = Convert.ToString(rest[0]["cargo"]);
                coor_nombre = Convert.ToString(rest[1]["nombre"]);
                coor_cargo = Convert.ToString(rest[1]["cargo"]);
                query = "SELECT * FROM datos.fechaletra(CURRENT_DATE) as fechaletra";
                List<Dictionary<string, object>> resul = globales.consulta(query);
                fechaletra = Convert.ToString(resul[0]["fechaletra"]);

                object[] tt1 = { };
                aux2[contador] = tt1;
                contador++;


                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                object[] parametros = { "nombre_em", "rfc", "direc_inmu", "cat_nombre", "cat_cargo", "coor_nombre", "coor_cargo", "fechaletra" , "logo" };
                object[] valor = { nombre_em, rfc, direc_inmu, cat_nombre, cat_cargo, coor_nombre, coor_cargo, fechaletra, logo };
                object[][] enviarParametros = new object[8][];

                enviarParametros[0] = parametros;
                enviarParametros[1] = valor;

                globales.reportes("ReporteAvaluos", "p_quirog", aux2, "", false, enviarParametros);
                this.Cursor = Cursors.Default;
            }
            else
            {
                DialogResult dialog = globales.MessageBoxExclamation("NO SE ENCUENTRA INFORMACIÓN", "VERIFICAR", globales.menuPrincipal);
            }

        }

        private void metodo4()
        {
            string nombre_em = string.Empty;
            string edad = string.Empty;
            string direccion = string.Empty;
            string edo_civil = string.Empty;
            string tel_ofici = string.Empty;
            string tel_partic = string.Empty;
            string descripcion = string.Empty;
            string ccatdes = string.Empty;
            string sec = string.Empty;
            string direc_inmu = string.Empty;  // datos generales

            //tablas hsetse
            string expediente = string.Empty;
            string f_elab = string.Empty;
            string a_condic = string.Empty;
            string a_ncuartos = string.Empty;
            string a_pisos = string.Empty;
            string a_ilumin = string.Empty;
            string a_ventil = string.Empty;
            string a_pared = string.Empty;
            string a_techo = string.Empty;
            string a_servsani = string.Empty;
            string a_patio = string.Empty;
            string b_estufa = string.Empty;
            string b_refri = string.Empty;
            string b_comedor = string.Empty;
            string b_sala = string.Empty;
            string b_gabinete = string.Empty;
            string b_roperos = string.Empty;
            string b_camas = string.Empty;
            string b_licuad = string.Empty;
            string b_tv = string.Empty;
            string b_radio = string.Empty;
            string b_lavad = string.Empty;
            string b_videoc = string.Empty;
            string b_ventil = string.Empty;
            string b_maqcoser = string.Empty;
            string b_vehic = string.Empty;
            string b_otros = string.Empty;
            string difer_i_e = string.Empty;
            string observ = string.Empty;
            string conclus = string.Empty;
            int i_sueldo = 0;
            int i_ayuda = 0;
            int i_quinq = 0;
            int i_otros = 0;
            int i_conyu = 0;
            int i_hijos = 0;
            int i_otrosf = 0;
            int i_total = 0;
            int e_quiro = 0;
            int e_hipo = 0;
            int e_direc = 0;
            int e_lbca = 0;
            int e_fpens = 0;
            int e_ss = 0;
            int e_csindic = 0;
            int e_ispt = 0;
            int e_otros = 0;
            int e_alim = 0;
            int e_vestu = 0;
            int e_transp = 0;
            int e_renta = 0;
            int e_agua = 0;
            int e_luz = 0;
            int e_gas = 0;
            int e_tel = 0;
            int e_coleg = 0;
            int e_servid = 0;
            int e_otrosf = 0;
            int e_total = 0;
            int i_suma = 0;

            // dependientes
            string nom_depec = string.Empty;
            string edad_depec = string.Empty;
            string escolaridad = string.Empty;
            string ocupacion = string.Empty;
            DateTime dia = DateTime.Now;

            string query = "SELECT * FROM datos.h_sestse where expediente='{0}' and sec='{1}';";
            string qry = string.Format(query, txtExpediente.Text, comboBox1.SelectedIndex);
            List<Dictionary<string, object>> res = globales.consulta(qry);

            Cursor.Current = Cursors.WaitCursor;


            if (res.Count >= 1) // validamos
            {
                query = "select * from datos.p_hipote where folio='{0}'";
                qry = string.Format(query, txtExpediente.Text);
                List<Dictionary<string, object>> datosgenerales = globales.consulta(qry); // comenzamos con datos generales

                nombre_em = Convert.ToString(datosgenerales[0]["nombre_em"]);
                edad = Convert.ToString(datosgenerales[0]["edad"]);
                direccion = Convert.ToString(datosgenerales[0]["direccion"]);
                edo_civil = Convert.ToString(datosgenerales[0]["edo_civil"]);
                tel_ofici = Convert.ToString(datosgenerales[0]["tel_ofici"]);
                tel_partic = Convert.ToString(datosgenerales[0]["tel_partic"]);
                descripcion = Convert.ToString(datosgenerales[0]["descripcion"]);
                ccatdes = Convert.ToString(datosgenerales[0]["ccatdes"]);
                direc_inmu = Convert.ToString(datosgenerales[0]["direc_inmu"]);


                expediente = Convert.ToString(res[0]["expediente"]);
                sec = Convert.ToString(res[0]["sec"]);
                f_elab = Convert.ToString(res[0]["f_elab"]);
                a_condic = Convert.ToString(res[0]["a_condic"]);
                a_ncuartos = Convert.ToString(res[0]["a_ncuartos"]);
                a_pisos = Convert.ToString(res[0]["a_pisos"]);
                a_ilumin = Convert.ToString(res[0]["a_ilumin"]);
                a_ventil = Convert.ToString(res[0]["a_ventil"]);
                a_pared = Convert.ToString(res[0]["a_pared"]);
                a_techo = Convert.ToString(res[0]["a_techo"]);
                a_servsani = Convert.ToString(res[0]["a_servsanit"]);
                a_patio = Convert.ToString(res[0]["a_patio"]);
                b_estufa = Convert.ToString(res[0]["b_estufa"]);
                b_refri = Convert.ToString(res[0]["b_refri"]);
                b_comedor = Convert.ToString(res[0]["b_comedor"]);
                b_sala = Convert.ToString(res[0]["b_sala"]);
                b_gabinete = Convert.ToString(res[0]["b_gabinete"]);
                b_roperos = Convert.ToString(res[0]["b_roperos"]);
                b_camas = Convert.ToString(res[0]["b_camas"]);
                b_licuad = Convert.ToString(res[0]["b_licuad"]);
                b_tv = Convert.ToString(res[0]["b_tv"]);
                b_radio = Convert.ToString(res[0]["b_radio"]);
                b_lavad = Convert.ToString(res[0]["b_lavad"]);
                b_videoc = Convert.ToString(res[0]["b_videoc"]);
                b_ventil = Convert.ToString(res[0]["b_ventil"]);
                b_maqcoser = Convert.ToString(res[0]["b_maqcoser"]);
                b_vehic = Convert.ToString(res[0]["b_vehic"]);
                b_otros = Convert.ToString(res[0]["b_otros"]);
                difer_i_e = Convert.ToString(res[0]["difer_i_e"]);
                observ = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["observ"])) ? "-" : Convert.ToString(res[0]["observ"]);
                conclus = Convert.ToString(res[0]["conclus"]);
                i_sueldo = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["i_sueldo"])) ? 0 : (Convert.ToInt32(res[0]["i_sueldo"]));
                i_ayuda = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["i_ayuda"])) ? 0 : (Convert.ToInt32(res[0]["i_ayuda"]));
                i_quinq = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["i_quinq"])) ? 0 : (Convert.ToInt32(res[0]["i_quinq"]));
                i_otros = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["i_otros"])) ? 0 : (Convert.ToInt32(res[0]["i_otros"]));
                i_conyu = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["i_conyu"])) ? 0 : (Convert.ToInt32(res[0]["i_conyu"]));
                i_hijos = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["i_hijos"])) ? 0 : (Convert.ToInt32(res[0]["i_hijos"]));
                i_otrosf = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["i_otrosf"])) ? 0 : (Convert.ToInt32(res[0]["i_otrosf"]));
                i_total = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["i_total"])) ? 0 : (Convert.ToInt32(res[0]["i_total"]));
                e_quiro = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_quiro"])) ? 0 : (Convert.ToInt32(res[0]["e_quiro"]));
                e_hipo = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_hipo"])) ? 0 : (Convert.ToInt32(res[0]["e_hipo"]));
                e_direc = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_direc"])) ? 0 : (Convert.ToInt32(res[0]["e_direc"]));
                e_lbca = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_lbca"])) ? 0 : (Convert.ToInt32(res[0]["e_lbca"]));
                e_fpens = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_fpens"])) ? 0 : (Convert.ToInt32(res[0]["e_fpens"]));
                e_ss = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_ss"])) ? 0 : (Convert.ToInt32(res[0]["e_ss"]));
                e_csindic = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_csindic"])) ? 0 : (Convert.ToInt32(res[0]["e_csindic"]));
                e_ispt = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_ispt"])) ? 0 : (Convert.ToInt32(res[0]["e_ispt"]));
                e_otros = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_otros"])) ? 0 : (Convert.ToInt32(res[0]["e_otros"]));
                e_alim = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_alim"])) ? 0 : (Convert.ToInt32(res[0]["e_alim"]));
                e_vestu = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_vestu"])) ? 0 : (Convert.ToInt32(res[0]["e_vestu"]));
                e_transp = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_transp"])) ? 0 : (Convert.ToInt32(res[0]["e_transp"]));
                e_renta = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_renta"])) ? 0 : (Convert.ToInt32(res[0]["e_renta"]));
                e_agua = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_agua"])) ? 0 : (Convert.ToInt32(res[0]["e_agua"]));
                e_luz = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_luz"])) ? 0 : (Convert.ToInt32(res[0]["e_luz"]));
                e_gas = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_gas"])) ? 0 : (Convert.ToInt32(res[0]["e_gas"]));
                e_tel = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_tel"])) ? 0 : (Convert.ToInt32(res[0]["e_tel"]));
                e_coleg = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_coleg"])) ? 0 : (Convert.ToInt32(res[0]["e_coleg"]));
                e_servid = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_servid"])) ? 0 : (Convert.ToInt32(res[0]["e_servid"]));
                e_otrosf = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_otrosf"])) ? 0 : (Convert.ToInt32(res[0]["e_otrosf"]));
                e_total = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["e_total"])) ? 0 : (Convert.ToInt32(res[0]["e_total"]));
                i_suma = i_conyu + i_hijos + i_otrosf;

                query = "SELECT * FROM datos.h_sdepec where expediente='{0}' and sec='{1}';";
                qry = string.Format(query, txtExpediente.Text, comboBox1.SelectedIndex);
                List<Dictionary<string, object>> dependientes = globales.consulta(qry);
                object[] aux2 = new object[dependientes.Count];
                int contador = 0;
                if (dependientes.Count >= 1)
                {


                    foreach (var item2 in dependientes)
                    {
                        nom_depec = Convert.ToString(item2["nom_depec"]);
                        edad_depec = Convert.ToString(item2["edad"]);
                        escolaridad = Convert.ToString(item2["escolaridad"]);
                        ocupacion = Convert.ToString(item2["ocupacion"]);

                        object[] tt1 = { nom_depec, edad_depec, escolaridad, ocupacion };
                        aux2[contador] = tt1;
                        contador++;
                    }
                }
                if (comboBox1.SelectedIndex == 0) sec = "INICIAL";
                if (comboBox1.SelectedIndex == 1) sec = "PRIMER AMPLIACIÓN";
                if (comboBox1.SelectedIndex == 2) sec = "SEGUNDA AMPLIACIÓN";
                if (comboBox1.SelectedIndex == 3) sec = "TERCER AMPLIACIÓN";

                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                object[] parametros = { "nombre_em", "edad", "direccion", "edo_civil", "tel_ofici", "tel_partic", "descripcion", "ccatdes", "direc_inmu", "sec", "a_condic", "a_ncuartos", "a_pisos", "a_ilumin", "a_ventil", "a_pared", "a_techo", "a_servsani", "a_patio", "b_estufa", "b_refri", "b_comedor", "b_sala", "b_gabinete", "b_roperos", "b_camas", "b_licuad", "b_tv", "b_radio", "b_lavad", "b_videoc", "b_ventil", "b_maqcoser", "b_vehic", "b_otros", "difer_i_e", "observ", "conclus", "i_sueldo", "i_ayuda", "i_quinq", "i_otros", "i_total", "e_quiro", "e_hipo", "e_direc", "e_lbca", "e_fpens", "e_ss", "e_csindic", "e_ispt", "e_otros", "e_alim", "e_vestu", "e_transp", "e_renta", "e_agua", "e_luz", "e_gas", "e_tel", "e_coleg", "e_servid", "e_otrosf", "e_total", "i_suma", "dia", "logo" };

                object[] valor = { nombre_em, edad, direccion, edo_civil, tel_ofici, tel_partic, descripcion, ccatdes, direc_inmu, sec, a_condic, a_ncuartos, a_pisos, a_ilumin, a_ventil, a_pared, a_techo, a_servsani, a_patio, b_estufa, b_refri, b_comedor, b_sala, b_gabinete, b_roperos, b_camas, b_licuad, b_tv, b_radio, b_lavad, b_videoc, b_ventil, b_maqcoser, b_vehic, b_otros, Convert.ToString(difer_i_e), observ, Convert.ToString(conclus), Convert.ToString(i_sueldo), Convert.ToString(i_ayuda), Convert.ToString(i_quinq), Convert.ToString(i_otros), Convert.ToString(i_total), Convert.ToString(e_quiro), Convert.ToString(e_hipo), Convert.ToString(e_direc), Convert.ToString(e_lbca), Convert.ToString(e_fpens), Convert.ToString(e_ss), Convert.ToString(e_csindic), Convert.ToString(e_ispt), Convert.ToString(e_otros), Convert.ToString(e_alim), Convert.ToString(e_vestu), Convert.ToString(e_transp), Convert.ToString(e_renta), Convert.ToString(e_agua), Convert.ToString(e_luz), Convert.ToString(e_gas), Convert.ToString(e_tel), Convert.ToString(e_coleg), Convert.ToString(e_servid), Convert.ToString(e_otrosf), Convert.ToString(e_total), Convert.ToString(i_suma), string.Format("{0:dd-MM-yyyy}", dia), logo };
                object[][] enviarParametros = new object[2][];

                enviarParametros[0] = parametros;
                enviarParametros[1] = valor;

                globales.reportes("ReporteEstudioSocio", "p_sdepec", aux2, "", false, enviarParametros);
                this.Cursor = Cursors.Default;

            }
            else
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO SE ENCUENTRA LA INFORMACIÓN SELECCIONADA", "VERIFICAR", globales.menuPrincipal);
            }

        }

        private void metodo5()
        {
            string qry = "SELECT a1.nombre_em,a1.rfc,a1.descripcion,a1.ccatdes,a1.direc_inmu,a2.descri_finalid,a3.sec,a3.areapredio,a3.area_const,a3.niveles,a3.habitac,a3.avaacab,a3.avaobneg,a3.imp_faltante,a3.imp_avance,a3.observ,(CURRENT_DATE) as fecha,a3.imp_estimt,a3.desghabit,a3.diagnostico,a3.f_elab    FROM datos.p_hipote a1 LEFT JOIN datos.h_solici a2 ON a1.folio=a2.expediente LEFT JOIN datos.h_sretec a3 ON a2.expediente=a3.expediente where a3.expediente='{0}' and a3.sec='{1}' limit 1";
            string query1 = string.Format(qry, txtExpediente.Text, comboBox1.SelectedIndex);
            List<Dictionary<string, object>> tecnico = globales.consulta(query1);
            string nombre_em = string.Empty;
            string rfc = string.Empty;
            string descripcion = string.Empty;
            string ccatdes = string.Empty;
            string direc_inmmu = string.Empty;
            string areapredio = string.Empty;
            string niveles = string.Empty;
            string habitac = string.Empty;
            string area_const = string.Empty;
            string avaobneg = string.Empty;
            string avaacab = string.Empty;
            string imp_estimt = string.Empty;
            string imp_avance = string.Empty;
            string imp_faltante = string.Empty;
            string sec = string.Empty;
            string descri_finalid = string.Empty;
            string observ = string.Empty;
            string tec = string.Empty;
            string desghabit = string.Empty;
            string diagnostico = string.Empty;
            string f_elab = string.Empty;
            DateTime fecha = DateTime.Now;

            Cursor.Current = Cursors.WaitCursor;


            if (tecnico.Count >= 1)
            {
                nombre_em = Convert.ToString(tecnico[0]["nombre_em"]);
                rfc = Convert.ToString(tecnico[0]["rfc"]);
                descripcion = Convert.ToString(tecnico[0]["descripcion"]);
                ccatdes = Convert.ToString(tecnico[0]["ccatdes"]);
                direc_inmmu = Convert.ToString(tecnico[0]["direc_inmu"]);
                areapredio = Convert.ToString(tecnico[0]["areapredio"]);
                niveles = Convert.ToString(tecnico[0]["niveles"]);
                habitac = Convert.ToString(tecnico[0]["habitac"]);
                area_const = Convert.ToString(tecnico[0]["area_const"]);
                avaobneg = Convert.ToString(tecnico[0]["avaobneg"]);
                avaacab = Convert.ToString(tecnico[0]["avaacab"]);
                imp_avance = Convert.ToString(tecnico[0]["imp_avance"]);
                imp_estimt = Convert.ToString(tecnico[0]["imp_estimt"]);
                desghabit = Convert.ToString(tecnico[0]["desghabit"]);
                imp_faltante = Convert.ToString(tecnico[0]["imp_faltante"]);
                diagnostico = Convert.ToString(tecnico[0]["diagnostico"]);
                f_elab= Convert.ToString(tecnico[0]["f_elab"]).Replace(" 12:00:00 a. m.","");
                sec = Convert.ToString(tecnico[0]["sec"]);
                if (sec == "0") sec = "INICIO";
                if (sec == "1") sec = "PRIMER AMPLIACIÓN";
                if (sec == "2") sec = "SEGUNDA AMPLIACIÓN";
                if (sec == "3") sec = "TERCERA AMPLIACIÓN";
                descri_finalid = Convert.ToString(tecnico[0]["descri_finalid"]);
                observ = Convert.ToString(tecnico[0]["observ"]);
                fecha = Convert.ToDateTime(tecnico[0]["fecha"]);
                query1 = "SELECT nombre AS TEC FROM catalogos.firmas where clave='TEC';";
                List<Dictionary<string, Object>> firma = globales.consulta(query1);
                tec = Convert.ToString(firma[0]["tec"]);
                query1 = "select* from datos.fechaletra('{0}')";
                string pasa = string.Format(query1, string.Format("{0:d}", f_elab));
                List<Dictionary<string, object>> f = globales.consulta(pasa);
                string fecha1 = Convert.ToString(f[0]["fechaletra"]);

                object[] aux2 = new object[tecnico.Count];
                int contador = 0;

                object[] tt1 = { };
                aux2[contador] = tt1;
                contador++;


                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                object[] parametros = { "nombre_em", "rfc", "descripcion", "ccatdes", "areapredio", "niveles", "habitac", "area_const", "avaobneg", "imp_avance", "imp_estimt", "imp_faltante", "sec", "fecha", "tec", "descri_finalid", "avaacab", "observ", "direc_inmmu", "fecha", "desghabit","diagnostico", "logo" };
                object[] valor = { nombre_em, rfc, descripcion, ccatdes, areapredio, niveles, habitac, area_const, avaobneg, imp_avance, imp_estimt, imp_faltante, sec, fecha1, tec, descri_finalid, avaacab, observ, direc_inmmu, fecha1, desghabit,diagnostico, logo };
                object[][] enviarParametros = new object[2][];

                enviarParametros[0] = parametros;
                enviarParametros[1] = valor;

                globales.reportes("ReportTecnicoHipote", "p_sdepec", aux2, "", false, enviarParametros);
                this.Cursor = Cursors.Default;

            }
            else
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO SE ENCUENTRA LA INFORMACIÓN BUSCADA", "VERIFICAR", globales.menuPrincipal);
            }
        }

        private void metodo6()
        {
            string query = "SELECT a2.nombre_em,a1.f_noproc,a1.sec,a1.t_noproc FROM datos.h_simpro a1 LEFT JOIN datos.p_hipote a2 ON a1.expediente=a2.folio where a1.expediente='{0}' and sec='{1}'";
            string paso = string.Format(query, txtExpediente.Text, comboBox1.SelectedIndex);
            List<Dictionary<string, object>> resultado = globales.consulta(paso);
            if (resultado.Count<=0)
            {
                globales.MessageBoxExclamation("NO SE ENCUENTRA LA INFORMACIÓN A BUSCAR", "VERIFICAR BUSQUEDA", globales.menuPrincipal);

                return;
            }
            object[] aux2 = new object[resultado.Count];

            Cursor.Current = Cursors.WaitCursor;



            int contador = 0;
            string nombre_em = Convert.ToString(resultado[0]["nombre_em"]);
            DateTime f_noproc = Convert.ToDateTime(resultado[0]["f_noproc"]);
            string t_noproc = Convert.ToString(resultado[0]["t_noproc"]);

            query = "select nombre from catalogos.firmas where clave= 'DIR'";
            List<Dictionary<string, object>> firma = globales.consulta(query);
            string dir = Convert.ToString(firma[0]["nombre"]);
            query = "select * from datos.fechaletra('{0}')";
            paso = string.Format(query, string.Format("{0:d}", f_noproc));
            List<Dictionary<string, object>> fec = globales.consulta(paso);
            string fechaletra = Convert.ToString(fec[0]["fechaletra"]);

            object[] tt1 = { };
            aux2[contador] = tt1;
            contador++;

            object[] parametros = { "dir", "nombre_em", "fechaletra", "t_noproc" };
            object[] valor = { dir, nombre_em, fechaletra, t_noproc };
            object[][] enviarParametros = new object[4][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reporteAcuerdoNegativo", "p_sdepec", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;

        }

        private void metodo7()
        {
            comboBox2.Visible = true;
            label3.Visible = true;

            string query = string.Empty;
            string qry = string.Empty;
            string importe = string.Empty;
            string letraimporte = string.Empty;
            string expediente = string.Empty;
            string tot_prest = string.Empty;
            string f_autorizacion = string.Empty;
            string nombre_not = string.Empty;
            string n_notario = string.Empty;
            string n_acta_n = string.Empty;
            string n_volu_n = string.Empty;
            string f_inscr_n = string.Empty;
            string nombre_em = string.Empty;
            DateTime f_recibo;
            string sec = string.Empty;
            string rfc = string.Empty;
            string f_autletra = string.Empty;
            string dir = string.Empty;
            string n_pago = string.Empty;
            string cap_prest = string.Empty;

            if (comboBox2.SelectedIndex == 0)
            {
                n_pago = "PRIMERA";
            }
            if (comboBox2.SelectedIndex == 1)

            {
                n_pago = "SEGUNDA";
            }
            if (comboBox2.SelectedIndex == 2)
            {
                n_pago = "TERCERA";
            }


             query = "SELECT a1.expediente,a1.n_emision,a1.f_recibo,a2.f_autorizacion,a2.sec,a2.tot_prest,a2.cap_prest,a1.importe,a3.nombre_not,a3.f_inscr_n,a3.n_acta_n,a3.n_notario,a3.n_volu_n,a4.nombre_em,a4.rfc FROM datos.h_semisi a1 JOIN datos.h_solici a2 ON a1.expediente=a2.expediente JOIN datos.h_enotar a3 ON A2.expediente=a3.expediente JOIN datos.p_hipote a4 ON a3.expediente=a4.folio where  a1.expediente='{0}' and a1.n_emision='{1}' AND a1.sec='{2}' and a2.sec='{2}' LIMIT 1";
            qry = string.Format(query, txtExpediente.Text, n_pago,comboBox1.SelectedIndex) ;
            List<Dictionary<string, object>> resultado = globales.consulta(qry);

            if (resultado.Count <= 0)
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO SE ENCUENTRA LA INFORMACIÓN", "VERIFICAR", globales.menuPrincipal);
                return;
            }
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            importe = Convert.ToString(resultado[0]["importe"]);
            letraimporte = globales.convertirNumerosLetras(importe, true);
            expediente = Convert.ToString(resultado[0]["expediente"]);
            sec = Convert.ToString(resultado[0]["sec"]);
            expediente = expediente + sec;
            tot_prest = Convert.ToString(resultado[0]["tot_prest"]);
            f_autorizacion = Convert.ToString(resultado[0]["f_autorizacion"]);
            cap_prest = Convert.ToString(resultado[0]["cap_prest"]);
            qry = "select * from datos.fechaletra('{0}')";
            query = string.Format(qry, string.Format("{0:yyyy/MM/dd}",globales.convertDatetime(f_autorizacion)));
            List<Dictionary<string, object>> fec1 = globales.consulta(query);
            f_autletra = Convert.ToString(fec1[0]["fechaletra"]).Replace("Oaxaca de Juárez,Oax.,a ", "").ToUpper();

            nombre_not = Convert.ToString(resultado[0]["nombre_not"]);
            rfc = Convert.ToString(resultado[0]["rfc"]);
            n_notario = Convert.ToString(resultado[0]["n_notario"]);
            n_acta_n = Convert.ToString(resultado[0]["n_acta_n"]);
            n_volu_n = Convert.ToString(resultado[0]["n_volu_n"]);
            f_inscr_n = Convert.ToString(resultado[0]["f_inscr_n"]);
            qry = "select * from datos.fechaletra('{0}')";
            query = string.Format(qry, string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(f_inscr_n)));
            List<Dictionary<string, object>> fec2 = globales.consulta(query);
            string f_inscr_n1 = Convert.ToString(fec2[0]["fechaletra"]).Replace("Oaxaca de Juárez,Oax.,a ", "").ToUpper();

            nombre_em = Convert.ToString(resultado[0]["nombre_em"]);
            string emision = Convert.ToString(resultado[0]["n_emision"]);
            if (emision == "1") emision = "PRIMERA";
            if (emision == "2") emision = "SEGUNDA";
            if (emision == "3") emision = "TERCERA";


            f_recibo = Convert.ToDateTime(resultado[0]["f_recibo"]);
            qry = "select * from datos.fechaletra('{0}')";
            query = string.Format(qry, string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(f_recibo)));
            List<Dictionary<string, object>> fec = globales.consulta(query);
            string f_reciboletra = String.Empty;
            try
            {
                 f_reciboletra = Convert.ToString(fec[0]["fechaletra"]);

            }
            catch
            {
                 f_reciboletra = Convert.ToString("2000/01/01");
                DialogResult dialogo = globales.MessageBoxExclamation("NO EXISTE FECHA CAPTURADA EN EMISIONES", "VERIFIQUE", globales.menuPrincipal);
            }

            query = "select nombre from catalogos.firmas where clave='DIR'";
            List<Dictionary<string, object>> firma = globales.consulta(query);
            dir = Convert.ToString(firma[0]["nombre"]);

            object[] tt1 = { };
            aux2[contador] = tt1;
            contador++;

            string cuerpo = "R E C I B I.- DE LA OFICINA DE PENSIONES DEL ESTADO LA CANTIDAD DE " + string.Format("{0:c}", Convert.ToDouble(importe)) + " (" + letraimporte + ") POR CONCEPTO DE MI " + emision +
                " EMISION A MI PRESTAMO HIPOTECARIO FOLIO NÚMERO " + expediente + " QUE POR LA CANTIDAD DE " + string.Format("{0:c}", Convert.ToDouble(cap_prest)) + " ME FUE CONCEDIDO POR EL CONSEJO DIRECTIVO" +
                " DE PENSIONES, EN SESIÓN CELEBRADA EL DÍA " + f_autletra + " , MISMO QUE QUEDO INSCRITO EN LA NOTARÍA PÚBLICA NÚMERO " + n_notario + " A CARGO DEL " + nombre_not +
                " SEGUN ACTA NÚMERO " + n_acta_n + " VOLUMEN NÚMERO " + n_volu_n + " DE FECHA " + f_inscr_n1 + ".";


            List<string> listaAux = globales.justificar(cuerpo, 70);
            cuerpo = string.Empty;
            listaAux.ForEach(o => cuerpo += o + "\n");

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[] parametros = { "importe", "cuerpo", "nombre_em", "rfc", "f_reciboletra", "dir", "logo" };
            object[] valor = { string.Format("{0:c}", Convert.ToDouble(importe)), cuerpo, nombre_em, rfc, f_reciboletra, dir, logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("ReciboPemision", "notificacion", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;

        }

        private void metodo8()
        {
            string query = "SELECT a1.nombre_em,a1.direccion,direc_inmu,a2.tot_prest,a2.cap_prest,a2.plazo,a2.int_prest,a2.plazoa,a2.f_autorizacion,a2.int_prim,a2.int_unit,a2.tot_prim,a2.tot_unit,(a2.tot_prest-a2.tot_prim) as resta,a2.cap_prim,a2.cap_unit,a3.n_acta_n,a3.nombre_not,n_notario,a4.n_emision,a4.f_recibo,a4.importe,a5.f_primdesc,a5.f_pagare FROM datos.p_hipote a1 left JOIN datos.h_solici a2 ON a1.folio=a2.expediente left JOIN datos.h_enotar a3 ON a2.expediente=a3.expediente left JOIN datos.h_semisi a4 ON a3.expediente=a4.expediente left JOIN datos.h_spagar a5 ON a2.expediente=a5.expediente  where a2.expediente='{0}' and a2.sec='{1}' and a5.sec='{1}' ORDER BY a4.f_recibo desc limit 1 ";
            string qry = string.Format(query, txtExpediente.Text, comboBox1.SelectedIndex);
            List<Dictionary<string, object>> resu = globales.consulta(qry);
            if (resu.Count <= 0)
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO SE ENCUENTRA LA INFORMACIÓN", "VERIFICAR", globales.menuPrincipal);
                return;
            }

            Cursor.Current = Cursors.WaitCursor;


            string nombre_em = string.Empty;
            string direc_inmu = string.Empty;
            string tot_prest = string.Empty;
            string cap_prest = string.Empty;
            string int_prest = string.Empty;
            string plazoa = string.Empty;
            DateTime f_autorizacion;
            string int_prim = string.Empty;
            string int_unit = string.Empty;
            string tot_prim = string.Empty;
            string tot_unit = string.Empty;
            string resta = string.Empty;
            string cap_prim = string.Empty;
            string cap_unit = string.Empty;
            string n_acta_n = string.Empty;
            string nombre_not = string.Empty;
            string n_notario = string.Empty;
            string n_emision = string.Empty;
            string plazo = string.Empty;
            string direccion = string.Empty;
            string f_recibo;
            string importe = string.Empty;
            double saldo = 0;
            int c_quincenas = 0;
            string letraTot_prest = string.Empty;
            string f_primdesc = string.Empty;
            string f_pagare = string.Empty;

            nombre_em = Convert.ToString(resu[0]["nombre_em"]);
            direc_inmu = Convert.ToString(resu[0]["direc_inmu"]);
            direccion = Convert.ToString(resu[0]["direccion"]);
            tot_prest = Convert.ToString(resu[0]["tot_prest"]);
            letraTot_prest = globales.convertirNumerosLetras(tot_prest, true);
            cap_prest = Convert.ToString(resu[0]["cap_prest"]);
            int_prest = Convert.ToString(resu[0]["int_prest"]);
            plazoa = Convert.ToString(resu[0]["plazoa"]);
            plazo = Convert.ToString(resu[0]["plazo"]);
            f_pagare = Convert.ToString(resu[0]["f_pagare"]).Replace("12:00:00 a. m.","");
            string fecha_pagare = $"SELECT * FROM datos.fechaletra('{f_pagare}')";
            List<Dictionary<string, object>> fecha = globales.consulta(fecha_pagare);
            string fecha_reporte = Convert.ToString(fecha[0]["fechaletra"]);

            if (plazo == "Q") c_quincenas = (Convert.ToInt32(plazoa) * 24) - 1;
            if (plazo == "M") c_quincenas = (Convert.ToInt32(plazoa) * 12) - 1;

            f_autorizacion = Convert.ToDateTime(resu[0]["f_autorizacion"]);
            f_primdesc = Convert.ToString(resu[0]["f_primdesc"]);
            int_prim = Convert.ToString(resu[0]["int_prim"]);
            int_unit = Convert.ToString(resu[0]["int_unit"]);
            tot_prim = Convert.ToString(resu[0]["tot_prim"]);
            tot_unit = Convert.ToString(resu[0]["tot_unit"]);
            resta = Convert.ToString(resu[0]["resta"]);
            cap_prim = Convert.ToString(resu[0]["cap_prim"]);
            cap_unit = Convert.ToString(resu[0]["cap_unit"]);
            n_acta_n = Convert.ToString(resu[0]["n_acta_n"]);
            nombre_not = Convert.ToString(resu[0]["nombre_not"]);
            n_notario = Convert.ToString(resu[0]["n_notario"]);
            n_emision = Convert.ToString(resu[0]["n_emision"]);
            if (n_emision == "1") n_emision = "EMISIÓN :  PRIMERA";
            if (n_emision == "2") n_emision = "EMISIÓN:   SEGUNDA";
            if (n_emision == "3") n_emision = "EMISIÓN:   TERCERA";
            string quer = "select * from datos.fechaletra('{0}')";
            string pasa = string.Format(quer, string.Format("{0:d}", Convert.ToDateTime(f_primdesc)));
            List<Dictionary<string, object>> fc = globales.consulta(pasa);
            string f_letraaut = Convert.ToString(fc[0]["fechaletra"]).Replace("Oaxaca de Juárez,Oax.,a ", "");
            if (plazo == "Q") plazo = "QUINCENALES";
            if (plazo == "M") plazo = "MENSUALES";
            f_recibo = Convert.ToString(resu[0]["f_recibo"]);
            importe = Convert.ToString(resu[0]["importe"]);



            string qr1 = "SELECT * FROM datos.h_semisi where expediente='{0}' and sec='{1}';";
            string pas = string.Format(qr1, txtExpediente.Text, comboBox1.SelectedIndex);
            List<Dictionary<string, object>> campo1 = globales.consulta(pas);
            List<Dictionary<string, object>> pagares = new List<Dictionary<string, object>>();  // GLOBAL PAGARES

            string Cf_recibo = string.Empty;
            string Cn_emision = string.Empty;
            string Cimporte = string.Empty;
            double sal_do = 0;
            sal_do = Convert.ToDouble(tot_prest) - Convert.ToDouble(tot_prim);
            double sumapagare = 0;
            double operacion = 0;
            foreach (var item4 in campo1)
            {
                Cf_recibo = "FECHA: " + Convert.ToString(item4["f_recibo"]).Replace(" 12:00:00 a. m", "");
                Cn_emision = "EMISIÓN: " + Convert.ToString(Convert.ToString(item4["n_emision"]));
                Cimporte = Convert.ToString(item4["importe"]);
                Dictionary<string, object> emisiones = new Dictionary<string, object>();

                sumapagare = Convert.ToDouble(item4["importe"]) + Convert.ToDouble(sumapagare);

                emisiones.Add("emision", Cn_emision);
                emisiones.Add("f_recibo", Cf_recibo);
                emisiones.Add("importe", Cimporte);
                pagares.Add(emisiones);

            }

            operacion = Convert.ToDouble(cap_prest) - sumapagare - Convert.ToDouble(tot_prim);

            int contador = 0;
            object[] aux2 = new object[pagares.Count];

            foreach (var item1 in pagares)
            {
                string cemision = Convert.ToString(item1["emision"]);
                string cf_recibo = Convert.ToString(item1["f_recibo"]);
                string cimporte = Convert.ToString(item1["importe"]);
                object[] tt1 = { cemision, cf_recibo, string.Format("{0:c}", Convert.ToDouble(cimporte)).Replace("$", "") };
                aux2[contador] = tt1;
                contador++;

            }

            string cuerpo1 = "POR ESTE PAGARÉ RECONOZCO DEBER Y ME OBLIGO A PAGAR INCONDICIONALMENTE A LA ORDEN DE LA OFICINA DE PENSIONES DEL ESTADO DE OAXACA, LA CANTIDAD DE--" +
                    string.Format("{0:c}", Convert.ToDouble(tot_prest)) + " " + letraTot_prest + " ";
            string cuerpo2 = "IMPORTE DEL PRÉSTAMO HIPOTECARIO QUE ME FUE CONCEDIDO PARA LIQUIDAR EN " + (c_quincenas+1) + "ABONOS " + plazo + " DURANTE " + plazoa + " AÑOS A PARTIR DE " + f_letraaut;

            string inicial = " 1   PAGO  INICIAL   ( CAPITAL:  " + string.Format("{0:c}", Convert.ToDouble(cap_prim)).Replace("$", "") + " + " + " INTERESES: " + string.Format("{0:c}", Convert.ToDouble(int_prim)).Replace("$", "") + " )   : " + string.Format("{0:c}", Convert.ToDouble(tot_prim));
            string parcialidad = c_quincenas + " PAGOS    " + plazo + ".    ( CAPITAL: " + string.Format("{0:c}", Convert.ToDouble(cap_unit)).Replace("$", "") + "  INTERESES: " + Convert.ToDouble((int_unit).Replace("$", "")) + " )   :" + string.Format("{0:c}", Convert.ToDouble(sal_do)).Replace("$", "");

            string cuerpo3 = "LA PRESENTE OPERACIÓN ESTA GARANTIZADA CON LA ESCRITURA " + n_acta_n + " OTORGADA ANTE LA FE DEL NOTARIO NO." + n_notario + " " + nombre_not + " CUYA PROPIEDAD ES: " +
                 direc_inmu + " HACIENDO FORMAL RENUNCIA DE LA PRESCRIPCIÓN QUE ESTABLECEN LOS ARTÍCULOS 170, 171, 172, 173, 174 DE LA LEY DE TÍTULOS Y OPERACIONES DE CRÉDITO, QUE SE REFIERE A LOS CONVENIOS ENTRE LOS FALLIDOS Y SUS ACREEDORES " +
                 "Y A LOS EFECTOS DE DICHOS CONVENIOS, SUJETANDOME EN TODO A LO INDICADO SOBRE PRESTAMOS HIPOTECARIOS POR LA LEY DE PENSIONES PARA LOS EMPLEADOS DEL GOBIERNO DEL ESTADO EN VIGOR.";

            List<string> listaAux = globales.justificar(cuerpo1, 243);
            cuerpo1 = string.Empty;
            listaAux.ForEach(o => cuerpo1 += o + "\n");

            List<string> listaAux1 = globales.justificar(cuerpo2, 243);
            cuerpo2 = string.Empty;
            listaAux1.ForEach(o => cuerpo2 += o + "\n");

            List<string> listaAux2 = globales.justificar(cuerpo3, 243);
            cuerpo3 = string.Empty;
            listaAux2.ForEach(o => cuerpo3 += o + "\n");

            List<string> listaAux3 = globales.justificar(inicial, 243);
            inicial = string.Empty;
            listaAux3.ForEach(o => inicial += o + "\n");

            List<string> listaAux4 = globales.justificar(parcialidad, 243);
            parcialidad = string.Empty;
            listaAux4.ForEach(o => parcialidad += o + "\n");

            query = "select nombre from catalogos.firmas where clave='DIR'";
            List<Dictionary<string, object>> direc = globales.consulta(query);
             string dir = Convert.ToString(direc[0]["nombre"]);

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[] parametros = { "cuerpo1", "cuerpo2", "cap_prest", "int_prest", "inicial", "parcialidad", "tot_prest", "operacion", "tot_prim", "tot_letra", "cuerpo3", "nombre_em", "direccion","fecha_letra", "dir", "logo" };
            object[] valor = { cuerpo1, cuerpo2, string.Format("{0:c}", Convert.ToDouble(cap_prest)).Replace("$", ""), int_prest, inicial, parcialidad, tot_prest, string.Format("{0:c}", Convert.ToDouble(operacion)).Replace("$", ""), string.Format("{0:c}", Convert.ToDouble(tot_prim)).Replace("$", ""), globales.convertirNumerosLetras(Convert.ToString(operacion), true), cuerpo3, nombre_em, direccion, fecha_reporte, dir , logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("pagareHipote", "pagareHipo", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;

        }

        private void metodo9()
        {
            string query = string.Empty;
            string qry = string.Empty;
            string nombre_em = string.Empty;
            string tot_prim = string.Empty;
            string f_autorizacion = string.Empty;
            string nombre_not = string.Empty;
            string n_acta_n = string.Empty;
            string f_recibo = string.Empty;
            string f_letra = string.Empty;
            string i_letra = string.Empty;
            string dir = string.Empty;
            string cuerpo = string.Empty;
            string sec = string.Empty;
            string f_pagare = string.Empty;


            query = "SELECT a1.tot_prim, a1.f_autorizacion, a2.nombre_em,a3.nombre_not,a3.n_acta_n,a4.f_pagare FROM	datos.h_solici a1 LEFT JOIN datos.p_hipote a2 ON a1.expediente = a2.folio LEFT JOIN datos.h_enotar a3 ON a2.folio = a3.expediente LEFT JOIN datos.h_spagar a4 ON a3.expediente = a4.expediente WHERE	a1.expediente = '{0}' AND a1.sec = '{1}'";
            qry =string.Format(query, txtExpediente.Text, comboBox1.SelectedIndex);
            List<Dictionary<string, object>> resultado = globales.consulta(qry);
            if (resultado.Count <=0)
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO SE ENCUENTRA LA INFORMACIÓN BUSCADA", "VERIFIQUE PARÁMETROS", globales.menuPrincipal);
                return;
            }

            Cursor.Current = Cursors.WaitCursor;


            tot_prim = string.Format("{0:c}", Convert.ToDouble(resultado[0]["tot_prim"])).Replace("$", "");
            nombre_em = Convert.ToString(resultado[0]["nombre_em"]);
            f_pagare = Convert.ToString(resultado[0]["f_pagare"]);
            i_letra = globales.convertirNumerosLetras(tot_prim, true);
            f_autorizacion = Convert.ToString(resultado[0]["f_autorizacion"]);
           // f_recibo = Convert.ToString(resultado[0]["f_recibo"]);
            n_acta_n = Convert.ToString(resultado[0]["n_acta_n"]);
            query = "select * from datos.fechaletra ('{0}')";
            qry = string.Format(query,string.Format("{0:yyyy-MM-dd}",Convert.ToDateTime(f_autorizacion))); 
            List<Dictionary<string, object>> f1 = globales.consulta(qry);
            f_autorizacion = Convert.ToString(f1[0]["fechaletra"]).Replace("Oaxaca de Juárez,Oax.,a ", "");
            qry = string.Format(query, string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(f_pagare)));
            List<Dictionary<string, object>> f2 = globales.consulta(qry);
            f_recibo = Convert.ToString(f2[0]["fechaletra"]);
            query = "select nombre from catalogos.firmas where clave='DIR'";
            List<Dictionary<string, object>> direc = globales.consulta(query);
            dir = Convert.ToString(direc[0]["nombre"]);
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            object[] tt1 = { };
            aux2[contador] = tt1;
            contador++;

            string quer = "select * from datos.fechaletra('{0}')";
            string pasa = string.Format(quer,string.Format("{0:yyyy-MM-dd}",Convert.ToDateTime(f_pagare)) );
            List<Dictionary<string,object>> fec= globales.consulta(pasa);
            f_pagare = Convert.ToString(fec[0]["fechaletra"]);


            cuerpo = "RECIBIMOS DE " +nombre_em+ " LA CANTIDAD DE: "+ string.Format("{0:c}",tot_prim).Replace("$","$ --")+ " (" +i_letra+ ") POR CONCEPTO DE PAGO INICIAL DEL PRESTAMO HIPOTECARIO NÚMERO "+txtExpediente.Text+comboBox1.SelectedIndex
               + " QUE SE LE OTORGO POR ACUERDO DEL CONSEJO DIRECTIVO DE PENSIONES DEL DÍA "+ f_autorizacion+ " , SEGUN ESCRITURA NÚMERO " +n_acta_n;

            List<string> listaAux3 = globales.justificar(cuerpo, 70);
            cuerpo = string.Empty;
            listaAux3.ForEach(o => cuerpo += o + "\n");

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[] parametros = { "tot_prim","cuerpo","f_recibo","dir", "logo" };
            object[] valor = { tot_prim, cuerpo, f_pagare, dir, logo };
            object[][] enviarParametros = new object[8][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reportePrimPago", "notificacion", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;

        }

        private void metodo10()
        {
           

        }

        public void metodo11()
        {
            string query = $"SELECT a2.nombre_em,a1.finalidad,a1.descri_finalid FROM	datos.h_solici a1 LEFT JOIN datos.p_hipote a2 ON a1.expediente = a2.folio  WHERE  a1.expediente ={txtExpediente.Text}  AND a1.sec ='{comboBox1.SelectedIndex}' ";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0) { return; globales.MessageBoxExclamation("NO SE ENCUENTRA EL FOLIO INGRESADO", "VERIFICAR", globales.menuPrincipal); }
            string query1 = "SELECT nombre FROM catalogos.firmas where clave in ('JUR','DPE');";
            List<Dictionary<string, Object>> firmas = globales.consulta(query1);

            Cursor.Current = Cursors.WaitCursor;


            string cuerpo = $"Los que suscribimos {Convert.ToString(firmas[0]["nombre"])} y {Convert.ToString(firmas[1]["nombre"])}, Jefes de los Departamentos Prestaciones Económicas y Jurídico de la Oficina de Pensiones,"+
                "con fundamento en lo dispuesto por el artículo 87 del Reglamento de Operación de la Oficina de Pensiones del Estado de Oaxaca, respectivamente; nos dirigimos a usted a efecto de "+
                $"poner a su consideración el Dictamen sobre la procedencia del PRÉSTAMO HIPOTECARIO solicitado por el ciudadano {Convert.ToString(resultado[0]["nombre_em"])} para {Convert.ToString(resultado[0]["descri_finalid"])} y para tal "+
                "efecto  de conformidad con lo dispuesto por el artículo 85 fracción II del referido Reglamento de Operación, presenta las siguientes documentales                                                          ";
            string primero = "___________Con los documentos antes descritos la Oficina de Pensiones a través del Departamento de Prestaciones Económicas dependiente de la Dirección de Prestaciones "+
                $"realizó una búsqueda en los registros del Sistema de Pensiones (SISPE) de esta Oficina y se puedo constatar que el ciudadano {Convert.ToString(resultado[0]["nombre_em"])}, no ha sido beneficiado "+
                "con un préstamo de la misma naturaleza; como consecuencia, con fundamento en lo dispuesto por el artículo 86 del Reglamento de Operación de la Oficina de Pensiones del Estado de Oaxaca , se realizó" +
                $" un reporte de estudio Técnico el cual corroboró la viabilidad de la solicitud del interesado cuyo fin es para {Convert.ToString(resultado[0]["descri_finalid"])}.";
            string segundo = $"__________Por lo anterior y con fundamento en lo dispuesto  por el artículo  87 del Reglamento  de Operación de la Oficina de Pensiones del Estado de Oaxaca, " +
                $" se dictamina la procedencia del  prestamo hipotecario a favor del ciudadano {Convert.ToString(resultado[0]["nombre_em"])}; toda vez que el expediente de solicitud se encuentra " +
                "dispuesto por los artículos 84 ,85 fracción II Y 86 del reglamento de Operación antes referidas, documentales con los cuales se concluye que se trata de una propiedad hipotecable, misma que se encuentra libre "+
                "debidamente integrado con base en los requisitos presentados de conformidad con lo  de gravamen tal y como se acredita con la documental que para tal efecto anexa el interesado, " +
                " elementos los cuales adminiculados con el reporte de estudio técnico se acredita plenamente la necesidad  de vivienda  que tiene el trabajador ; y se ";

            List<string> listaAux1 = globales.justificar(cuerpo, 80);
            cuerpo = string.Empty;
            listaAux1.ForEach(o => cuerpo += o + "\n");

            List<string> listaAux2 = globales.justificar(primero, 80);
            primero = string.Empty;
            listaAux2.ForEach(o => primero += o + "\n");

            List<string> listaAux3 = globales.justificar(segundo, 80);
            segundo = string.Empty;
            listaAux3.ForEach(o => segundo += o + "\n");

            primero = primero.Replace("_"," ");
            segundo = segundo.Replace("_", " ");
            

            string unico = $"_______Se dictamina la procedencia del crédito hipotecario a favor del ciudadano {Convert.ToString(resultado[0]["nombre_em"])} en los términos expuestos en el considerando primero y segundo del presente dictamen";
            string query2 = $"select documento,CASE WHEN  original = 'X' and copia='' THEN	'ORIGINAL' WHEN copia ='X' AND original= 'X' THEN 	'ORIGINAL Y COPIA' WHEN copia='X' and original='' THEN 'COPIA' END estado from datos.h_sdocum where expediente={txtExpediente.Text} and sec='{comboBox1.SelectedIndex}' ORDER BY  cve_docum asc";
            List<Dictionary<string, object>> doc = globales.consulta(query2);
            char letra = 'A';
            bool ENTRA = false;


            unico = unico.Replace("_", " ");

            object[] aux2 = new object[15];
            int contador = 0;


            for (int x = 0; x < 15; x++)
            {
                object[] tt1 = { "" , "" };
                if (x < doc.Count)
                {
                    string documentos = string.Empty;

                    try
                    {

                        string estado = string.Empty;
                        documentos = Convert.ToString(doc[x]["documento"])+ "  (" +Convert.ToString(doc[x]["estado"])+")";


                        if (ENTRA==false)
                        {
                            tt1[0] = letra +")";
                            tt1[1] = documentos;

                            ENTRA = true;
                            aux2[x] = tt1;
                            continue;
                        }
                        if (ENTRA==true)
                        {
                            letra++;
                            tt1[0] = letra + ")";
                            tt1[1] = documentos;


                        }

                    }
                    catch
                    {

                    }
                }
                aux2[x] = tt1;
            }
            string queryfecha = "select * from datos.fechaletra('{0}')";
            string pasa= string.Format(queryfecha, string.Format("{0:d}", DateTime.Now));
            List<Dictionary<string, object>> lisfecha = globales.consulta(pasa);
            string fecha = Convert.ToString(lisfecha[0]["fechaletra"]);

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);
            object[] parametros = { "cuerpo","primero","segundo","prestaciones","juridico","expediente","unico", "fecha", "logo" };
            object[] valor = { cuerpo,primero,segundo, Convert.ToString(firmas[0]["nombre"]) , Convert.ToString(firmas[1]["nombre"]),txtExpediente.Text ,unico, fecha, logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("ReporteDictamenPrest", "notificacion", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;

        }


        private void frmBuscador_Shown(object sender, EventArgs e)
        {
            
            txtExpediente.Focus();
            if (opcion == 2)
            {
                comboBox1.Visible = false;
                label2.Visible = false;
            }
            if (opcion == 9)
            {
                comboBox2.Visible = true;
                label3.Visible = true;
            }

          
        }


        private void txtExpediente_Leave(object sender, EventArgs e)
        {

           
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                if (comboBox2.Visible==true)
                {
                    comboBox2.Focus();
                    comboBox2.SelectedIndex=0;
                }

                if (!string.IsNullOrWhiteSpace(txtExpediente.Text)&&! string.IsNullOrWhiteSpace(comboBox1.Text)&& comboBox2.Visible==false)
                {
                    visualizaReporte();
                }
            }
        }

        private void txtExpediente_KeyDown(object sender, KeyEventArgs e)
        {
           
         

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtExpediente.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text))
                {
                    visualizaReporte();

                }
            }
        }

        private void frmBuscador_Load(object sender, EventArgs e)
        {
            string query = "select folio,rfc,nombre_em from datos.p_hipote  order by folio desc limit 100";
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            resultado.ForEach(o => dtggrid.Rows.Add(o["folio"], o["rfc"], o["nombre_em"]));

            if (resultado.Count != 0) {
                txtExpediente.Text = Convert.ToString(resultado[0]["folio"]);
            }

            comboBox1.SelectedIndex = 0;


            query = $"select length(COALESCE(max(folio),0)::text) as cantidad from datos.p_hipote";
            List<Dictionary<string,object>> obj = new dbaseORM().query(query);
            this.totalFolio = globales.convertInt(Convert.ToString(obj[0]["cantidad"]));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void dtggrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;


            if (c == -1) return;


            string folio = Convert.ToString(dtggrid.Rows[e.RowIndex].Cells[0].Value);

            txtExpediente.Text = folio;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string query = string.Empty;
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                query = $"select folio,rfc,nombre_em from datos.p_hipote order by folio desc limit 100";
            }
            else if (char.IsNumber(txtBuscar.Text.First()))
            {
                query = $"select * from datos.p_hipote where ";
                string aux = string.Empty;
                if (this.totalFolio == txtBuscar.Text.Length)
                {
                    aux = " folio = " + txtBuscar.Text;
                }
                else
                {
                    string strFolio = txtBuscar.Text;
                    string desde = strFolio;
                    string hasta = strFolio;
                    string between = "folio = " + strFolio;
                    for (int x = txtBuscar.Text.Length; x < this.totalFolio; x++)
                    {
                        desde += "0";
                        hasta += "9";
                        between += $" or folio between {desde} and {hasta} ";
                    }
                    aux = between + $" order by folio desc limit 100";
                }
                query += aux;
            }
            else
            {
                if (txtBuscar.Text.Contains("..") || txtBuscar.Text.Contains("."))
                {
                    string texto = txtBuscar.Text.Replace("..", ".");
                    string[] split = texto.Split('.');

                    string nombre_em = string.Empty;

                    foreach (string i in split)
                    {
                        if (string.IsNullOrWhiteSpace(i)) continue;
                        nombre_em += $" nombre_em like '%{i}%' ,";
                    }
                    nombre_em = nombre_em.Substring(0, nombre_em.Length - 1).Replace(",", " and ");

                    query = $"select * from datos.p_hipote where  rfc like '{txtBuscar.Text}%' or {nombre_em}  order by folio desc limit 100";

                }
                else
                {
                    query = $"select * from datos.p_hipote where  rfc like '{txtBuscar.Text}%' or nombre_em like '%{txtBuscar.Text}%'  order by folio desc limit 100";
                }
            }
            dtggrid.Rows.Clear();

            List<Dictionary<string, object>> obj = new dbaseORM().query(query);
            obj.ForEach(o => {
                dtggrid.Rows.Add(o["folio"], o["rfc"], o["nombre_em"]);
            });
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}

