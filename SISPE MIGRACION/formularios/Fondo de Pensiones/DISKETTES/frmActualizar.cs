using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones.DISKETTES
{
    public partial class frmActualizar : Form
    {
        private DbfDataReader.DbfTable tabla;
        private DataSet dbf;
        private bool aportacion;
        private Dictionary<int, Dictionary<string, string>> quincenas;
        private bool continuar = false;
        private string strCuentaaux;
        private double sumasueldo;
        private List<Dictionary<string, object>> empleados = null;
        private bool errorRfc;
        private bool fechasCambiadas = false;
        private bool btnFechabool;

        public frmActualizar()
        {
            InitializeComponent();
            this.sumasueldo = sumasueldo;
            quincenas = new Dictionary<int, Dictionary<string, string>>();
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/01/{0}");
            diccionario.Add("hasta", "15/01/{0}");
            quincenas.Add(1, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "16/01/{0}");
            diccionario.Add("hasta", "30/01/{0}");
            quincenas.Add(2, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/02/{0}");
            diccionario.Add("hasta", "15/02/{0}");
            quincenas.Add(3, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "16/02/{0}");
            diccionario.Add("hasta", "28/02/{0}");
            quincenas.Add(4, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/03/{0}");
            diccionario.Add("hasta", "15/03/{0}");
            quincenas.Add(5, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "16/03/{0}");
            diccionario.Add("hasta", "30/03/{0}");
            quincenas.Add(6, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/04/{0}");
            diccionario.Add("hasta", "15/04/{0}");
            quincenas.Add(7, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "16/04/{0}");
            diccionario.Add("hasta", "30/04/{0}");
            quincenas.Add(8, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/05/{0}");
            diccionario.Add("hasta", "30/05/{0}");
            quincenas.Add(9, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/05/{0}");
            diccionario.Add("hasta", "30/05/{0}");
            quincenas.Add(10, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/06/{0}");
            diccionario.Add("hasta", "15/06/{0}");
            quincenas.Add(11, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "16/06/{0}");
            diccionario.Add("hasta", "30/06/{0}");
            quincenas.Add(12, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/07/{0}");
            diccionario.Add("hasta", "15/07/{0}");
            quincenas.Add(13, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "15/07/{0}");
            diccionario.Add("hasta", "30/07/{0}");
            quincenas.Add(14, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/08/{0}");
            diccionario.Add("hasta", "15/08/{0}");
            quincenas.Add(15, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "16/08/{0}");
            diccionario.Add("hasta", "30/08/{0}");
            quincenas.Add(16, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/09/{0}");
            diccionario.Add("hasta", "15/09/{0}");
            quincenas.Add(17, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "16/09/{0}");
            diccionario.Add("hasta", "30/09/{0}");
            quincenas.Add(18, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/10/{0}");
            diccionario.Add("hasta", "15/10/{0}");
            quincenas.Add(19, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "16/10/{0}");
            diccionario.Add("hasta", "30/10/{0}");
            quincenas.Add(20, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/11/{0}");
            diccionario.Add("hasta", "15/11/{0}");
            quincenas.Add(21, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "16/11/{0}");
            diccionario.Add("hasta", "30/11/{0}");
            quincenas.Add(22, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/12/{0}");
            diccionario.Add("hasta", "30/12/{0}");
            quincenas.Add(23, diccionario);

            diccionario = new Dictionary<string, string>();
            diccionario.Add("desde", "01/12/{0}");
            diccionario.Add("hasta", "30/12/{0}");
            quincenas.Add(24, diccionario);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult p = open1.ShowDialog();
            if (p == DialogResult.OK)
            {
                string ruta = open1.FileName;
                string[] arreglo = ruta.Split('\\');
                string nombreArchivo = arreglo[arreglo.Length - 1];
                string letra = nombreArchivo.First().ToString().ToUpper();
                if (letra == "A" || letra == "D")
                {
                    txtTotalH.Visible = false;
                    txtTotalQ.Visible = false;

                    lbl1.Visible = false;
                    lbl2.Visible = false;

                    lblDependencia.Visible = false;
                    txtDependencia.Visible = false;
                    this.continuar = true;
                    bool aportacion = letra == "A";
                    btnFecha.Enabled = true;
                    realizarOperacion(aportacion, nombreArchivo, ruta);
                }
                else
                {
                    globales.MessageBoxError("Archivo seleccionado invalido, asegurase que cumpla con el nombre establecido empezando con A o D", "Error archivo", globales.menuPrincipal);
                }
            }


            if (!string.IsNullOrWhiteSpace(txtHasta.Text))
            {
                DateTime tiempo = DateTime.Parse(txtHasta.Text);
                dateTimePicker1.Value = tiempo;
            }
        }

        private void realizarOperacion(bool aportacion, string nombre, string ruta)
        {
            string queEs = aportacion ? "APORTACIÓN" : "DESCUENTOS";
            this.aportacion = aportacion;
            txtArchivo.Text = nombre.Split('.')[0];
            txtConcepto.Text = queEs;
            txtRuta.Text = ruta;



            dbf = globales.leerDbf(ruta);
            string claveDependencia = dbf.Tables[0].Rows[0]["proyecto"].ToString().Substring(0, 3);
            string desde = string.Empty;
            string hasta = string.Empty;
            string anio = string.Empty;
            string quincena = string.Empty;



            strCuentaaux = string.Empty;
            try
            {
                string archivo = txtArchivo.Text;
                string finalCadena = archivo.Substring(archivo.Length - 4);
                anio = finalCadena.Substring(0, 2);
                quincena = finalCadena.Substring(2);
                int integerquincena = Convert.ToInt32(quincena);
                desde = quincenas[integerquincena]["desde"];
                hasta = quincenas[integerquincena]["hasta"];
                desde = string.Format(desde, "20" + anio);
                hasta = string.Format(hasta, "20" + anio);

                strCuentaaux = archivo.Substring(0, archivo.Length - 4);
                strCuentaaux = strCuentaaux.Substring(1);
            }
            catch
            {

            }



            string query = string.Format("SELECT * FROM catalogos.disket where cuenta = '{0}'", strCuentaaux);

            List<Dictionary<string, object>> tmp1 = globales.consulta(query);

            txtDesde.Text = desde;
            txtHasta.Text = hasta;
            txtDependencia.Text = (tmp1.Count == 0) ? "" : Convert.ToString(tmp1[0]["descripcion"]);

            this.Cursor = Cursors.WaitCursor;
            datos1.DataSource = dbf.Tables[0];
            this.Cursor = Cursors.Default;

            if (queEs == "DESCUENTOS")
            {


                double sumatoriaQuirografario = 0;
                double sumatoriaHipotecario = 0;
                int contador = 0;
                foreach (DataRow item in dbf.Tables[0].Rows)
                {
                    txtTotalH.Visible = true;
                    txtTotalQ.Visible = true;

                    lbl1.Visible = true;
                    lbl2.Visible = true;
                    int tipo = Convert.ToInt32(item["cvedesc"]);
                    if (tipo == 205)
                    {
                        sumatoriaQuirografario += Convert.ToDouble(item["importe"]);
                    }
                    else
                    {
                        sumatoriaHipotecario += Convert.ToDouble(item["importe"]);
                    }

                    txtTotalQ.Text = string.Format("{0:C}", sumatoriaQuirografario);
                    txtTotalH.Text = string.Format("{0:C}", sumatoriaHipotecario);

                    if (queEs == "DESCUENTOS")
                    {
                        string folio = Convert.ToString(item["folio"]);
                        if (Convert.ToDouble(item["cvedesc"]) == 205)
                        {
                            string consulta = $"select * from datos.p_edocta where folio = {folio}";
                            List<Dictionary<string, object>> rst = globales.consulta(consulta);
                            if (rst.Count == 0)
                            {
                                this.continuar = false;
                                datos1.Rows[contador].DefaultCellStyle.BackColor = Color.Pink;
                            }
                        }
                    }
                    contador++;
                }
            }

            bool esCentralizado = false;
            string strCuenta = txtArchivo.Text.Substring(1, 3).ToUpper();
            switch (strCuenta)
            {
                case "F51":
                    esCentralizado = true;
                    break;
                case "M51":
                    esCentralizado = true;
                    break;
                case "MBN":
                    esCentralizado = true;
                    break;
                case "SBN":
                    esCentralizado = true;
                    break;
                case "511":
                    esCentralizado = true;
                    break;
                case "CMM":
                    esCentralizado = true;
                    break;
                case "FCO":
                    esCentralizado = true;
                    break;
                case "MCN":
                    esCentralizado = true;
                    break;
                case "SCN":
                    esCentralizado = true;
                    break;
                case "FMM":
                    esCentralizado = true;
                    break;
                case "MM5":
                    esCentralizado = true;
                    break;
                case "MMM":
                    esCentralizado = true;
                    break;
                case "MMS":
                    esCentralizado = true;
                    break;
                case "SMM":
                    esCentralizado = true;
                    break;
                default:
                    esCentralizado = false;
                    break;
            }
            if (esCentralizado)
            {
                lblDependencia.Visible = false;
                txtDependencia.Visible = false;
            }
            else
            {
                lblDependencia.Visible = true;
                txtDependencia.Visible = true;
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

            if (this.fechasCambiadas)
            {
                globales.MessageBoxExclamation("Se ha modificado las fechas desde y hasta del archivo a subir", "Aviso", globales.menuPrincipal);
                if (globales.MessageBoxQuestion("¿Desea continuar con la operación?", "Aviso", globales.menuPrincipal) == DialogResult.No)
                    return;
            }

                string ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\UNIDADRED.bat";

            string ip = SISPE_MIGRACION.Properties.Resources.servidor;

            //   CODIGO PARA ENCONTRAR ARCHIVOS DE NOMBRES EN RESPALDOS
            //string[] archivos = Directory.GetFiles($@"K:\");
            //foreach (string item in archivos)
            //{

            //    string archivo = item.Substring(item.Length - 8, 4);
            //    string a1 = item.Substring(item.Length - 12);
            //    string m = item.Substring(3, 1).ToUpper();
            //    if (m == "D")
            //    {
            //        if (archivo == "1903")
            //        {
            //            File.Copy(item, @"C:\Users\samv\Desktop\ayuda\" + a1);
            //        }
            //    }
            //}



            if (ip.Contains("192.168.100.101")) {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.UseShellExecute = false;
                psi.Arguments = "NET USE K: \\192.168.100.102\\RESPALDOS Pensiones2.. /USER:Administrador\root";
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.FileName = ruta;
                Process.Start(psi);
            }

            DialogResult p = globales.MessageBoxQuestion("¿Desea realizar la inserción de los registros?", "Aviso", globales.menuPrincipal);

            if (DialogResult.No == p)
            {
                return;
            }

            if (!this.continuar)
            {
                globales.MessageBoxExclamation("No se pude continuar porque no se encuentran folios en estado de cuenta.", "Aviso", globales.menuPrincipal);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtArchivo.Text))
            {
                globales.MessageBoxExclamation("Favor de elegir un archivo dbf", "Aviso", globales.menuPrincipal);
                return;
            }

            try
            {

                if (ip.Contains("192.168.100.101"))
                {
                    if (File.Exists($@"K:\{txtArchivo.Text}.dbf"))
                        File.Delete($@"K:\{txtArchivo.Text}.dbf");

                    File.Copy(txtRuta.Text, $@"K:\{txtArchivo.Text}.dbf");
                }
             




            }
            catch
            {
                globales.MessageBoxError("Favor de crear la unidad de red K:\\ en la dirección siguiente \\\\192.168.100.102\respaldos\nNOTA: Las credenciales se proporciona en el área de sistemas", "Aviso", globales.menuPrincipal);
                Cursor = Cursors.Default;
                return;
            }

            string query = string.Format("select count(archivo) as cantidad from datos.{1} where archivo = '{0}'", txtArchivo.Text, this.aportacion ? "aportaciones" : "descuentos");
            if (Convert.ToInt32(globales.consulta(query)[0]["cantidad"]) > 0)
            {
                globales.MessageBoxExclamation("Registros ya se encuentran registrados", "Aviso", globales.menuPrincipal);
                return;
            }



            query = string.Empty;
            int contador = 0;
            bool subir = false;
            string concate = string.Empty;
            sumasueldo = 0;

            Cursor = Cursors.WaitCursor;

            DateTime dt1 = new DateTime(DateTime.Now.Year-10,1,1);
            string consulta = $"select folio from datos.p_edocta where f_emischeq > '{string.Format("{0:yyyy-MM-dd}",dt1)}'";
            List<Dictionary<string, object>> quiro = globales.consulta(consulta);
            consulta = "select folio from datos.p_edocth where folio is not null";
            List<Dictionary<string, object>> hipo = globales.consulta(consulta);

            foreach (DataRow item in dbf.Tables[0].Rows)
            {
                contador++;
                if (this.aportacion)
                {

                    if (empleados == null)
                        empleados = globales.consulta("select rfc,pendiente from datos.empleados");
                    string desde = string.Empty;
                    string hasta = string.Empty;
                    try
                    {
                        string archivo = txtArchivo.Text;
                        string finalCadena = archivo.Substring(archivo.Length - 4);
                        string anio = finalCadena.Substring(0, 2);
                        string quincena = finalCadena.Substring(2);
                        int integerquincena = Convert.ToInt32(quincena);
                        desde = quincenas[integerquincena]["desde"];
                        hasta = quincenas[integerquincena]["hasta"];
                        hasta = string.Format(hasta, "20" + anio);
                        desde = string.Format(desde, "20" + anio);
                        desde = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(desde));
                        hasta = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(hasta));
                    }
                    catch
                    {

                    }
                    //  double sueldo = 0;


                    string rfc = Convert.ToString(item["rfc"]).Trim();
                    string categoria = Convert.ToString(item["categoria"]);
                    string nombre = Convert.ToString(item["nombre"]);
                    string aportacion = Convert.ToString(item["aportacion"]);
                    string proyectoResumido = Convert.ToString(item["proyecto"]).Substring(0, 3);
                    string proyecto = Convert.ToString(item["proyecto"]);
                    string tipoAporta = Convert.ToString(item["tipoaporta"]);
                    double sueldo = Convert.ToDouble(item["sueldo"]);
                    string curp = "";
                    string modalidad = "";
                  
                    try
                    {
                        curp = Convert.ToString(item["curp"]);
                       
                    }
                    catch
                    {

                    }

                    try
                    {
                        modalidad = Convert.ToString(item["modalidad"]);
                    }
                    catch {

                    }

                    sumasueldo = sumasueldo + sueldo;

                    if (string.IsNullOrWhiteSpace(rfc) ||
                        string.IsNullOrWhiteSpace(desde) ||
                        string.IsNullOrWhiteSpace(hasta) ||
                        string.IsNullOrWhiteSpace(aportacion) ||
                        string.IsNullOrWhiteSpace(proyecto) ||
                        string.IsNullOrWhiteSpace(tipoAporta))
                    {
                        subir = true;
                        datos1.Rows[contador - 1].DefaultCellStyle.BackColor = Color.LightPink;
                    }

                    if (rfc.Length > 13)
                    {
                        subir = true;
                        datos1.Rows[contador - 1].DefaultCellStyle.BackColor = Color.LightPink;
                        errorRfc = true;
                    }

                    if (tipoAporta.ToUpper().Trim() == "R")
                    {
                        desde = Convert.ToString(item["desde"]);
                        hasta = Convert.ToString(item["hasta"]);
                        if (string.IsNullOrWhiteSpace(desde) || string.IsNullOrWhiteSpace(hasta))
                        {
                            globales.MessageBoxError("Aportación retroactiva no contiene fechas", "Aviso", globales.menuPrincipal);
                            subir = true;
                            datos1.Rows[contador - 1].DefaultCellStyle.BackColor = Color.LightPink;
                        }
                        DateTime dtDesde = DateTime.Parse(desde);
                        DateTime dtHasta = DateTime.Parse(hasta);
                        if ((dtDesde < DateTime.Parse("01/01/1910")) || (dtHasta < DateTime.Parse("01/01/1910")))
                        {
                            globales.MessageBoxError("Aportación retroactiva no contiene fechas", "Aviso", globales.menuPrincipal);
                            subir = true;
                            datos1.Rows[contador - 1].DefaultCellStyle.BackColor = Color.LightPink;
                        }

                        desde = string.Format("{0:yyyy-MM-dd}", dtDesde);
                        hasta = string.Format("{0:yyyy-MM-dd}", dtHasta);
                    }
                    //"INSERT INTO historial.archivos(archivo, dependencia, desde, hasta, tipo, cuenta, anio, mes, normal, retroactivo, total, sueldo, fecha, hora, patronal, totaportadores, idusuario, totpq, totph) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}')";
                    //Verifica si contiene tarjeta del empleado antes de subir la información
                    string auxRfc = rfc.Trim();

                    bool encuentraTarjeta = empleados.Any(o => Convert.ToString(o["rfc"]) == auxRfc);
                    string queryEmpleado = "";
                    string a = "";
                    string q = "";


                    string strCuenta = txtArchivo.Text.Substring(1, 3).ToUpper();
                    string tipoRelacion = string.Empty;
                    bool esCentralizado = false;
                    switch (strCuenta)
                    {
                        case "F51":
                            esCentralizado = true;
                            tipoRelacion = "F51";//BAS
                            modalidad = "BASE";
                            break;
                        case "M51":
                            esCentralizado = true;
                            tipoRelacion = "M51";//BAS
                            modalidad = "BASE";
                            break;
                        case "MBN":
                            esCentralizado = true;
                            tipoRelacion = "M51";//BASE
                            modalidad = "BASE";
                            break;
                        case "SBN":
                            esCentralizado = true;
                            tipoRelacion = "M51";
                            modalidad = "BASE";
                            break;
                        case "511":
                            esCentralizado = true;
                            tipoRelacion = "511";
                            modalidad = "CONF";
                            break;
                        case "CMM":
                            esCentralizado = true;
                            tipoRelacion = "511";
                            modalidad = "CONF";
                            break;
                        case "FCO":
                            esCentralizado = true;
                            tipoRelacion = "FCO";
                            modalidad = "CONF";
                            break;
                        case "MCN":
                            esCentralizado = true;
                            tipoRelacion = "511";
                            modalidad = "CONF";
                            break;
                        case "SCN":
                            esCentralizado = true;
                            tipoRelacion = "511";
                            modalidad = "CONF";
                            break;
                        case "FMM":
                            esCentralizado = true;
                            tipoRelacion = "FMM";
                            modalidad = "MMS";
                            break;
                        case "MM5":
                            esCentralizado = true;
                            tipoRelacion = "MMS";
                            modalidad = "MMS";
                            break;
                        case "MMM":
                            esCentralizado = true;
                            tipoRelacion = "MMS";
                            modalidad = "MMS";
                            break;
                        case "MMS":
                            esCentralizado = true; //conf
                            tipoRelacion = "MMS";
                            modalidad = "MMS";
                            break;
                        case "SMM":
                            esCentralizado = true;  //cconf
                            tipoRelacion = "MMS";
                            modalidad = "MMS";
                            break;
                        default:
                            esCentralizado = false;
                            break;
                    }
                    this.strCuentaaux = tipoRelacion;
                    if (esCentralizado)
                    {
                        string proy = Convert.ToString(item["proyecto"]).Substring(0, 3);
                        string query2 = $"select * from catalogos.cuentas where proy = '{proy}'";
                        List<Dictionary<string, object>> resultado = globales.consulta(query2);
                        if (resultado.Count != 0)
                        {
                            string cuenta = Convert.ToString(resultado[0]["cuenta"]);
                            tipoRelacion = cuenta;
                        }
                    }
                    else
                    {
                        tipoRelacion = strCuenta;
                        this.strCuentaaux = tipoRelacion;
                    }

                    if (!encuentraTarjeta)
                    {

                        string sexo = string.Empty;
                        try
                        {
                            sexo = Convert.ToString(item["sexo"]);
                        }
                        catch
                        {
                            sexo = Convert.ToString(item["curp"]).Substring(10, 1);
                            sexo = sexo == "H" ? "M" : "F";
                        }

                        queryEmpleado = "insert into datos.empleados(rfc,nombre_em,proyecto,nap,pendiente,sexo,sueldo_base,tipo_rel,modalidad) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}');";
                        queryEmpleado = string.Format(queryEmpleado, auxRfc, Convert.ToString(item["nombre"]).Trim(), Convert.ToString(item["proyecto"]).Trim(), 0, 't', Convert.ToString(sexo).Trim(), sueldo, this.strCuentaaux,modalidad);
                        Dictionary<string, object> diccionario = new Dictionary<string, object>();
                        diccionario.Add("rfc", auxRfc);
                        diccionario.Add("pendiente", true);
                        empleados.Add(diccionario);


                        query += queryEmpleado;

                        if (this.fechasCambiadas) {
                            desde = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDesde.Text));
                            hasta = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtHasta.Text));
                        }

                        a = "insert into historial.aportaciones (rfc,inicio,final,new_tipo,movimiento,entrada,status,cuenta,fecharegistro,archivo,salida) values('{0}','{1}','{2}','{3}','APORTACION',{4},'{8}','{5}','{6}','{7}',0); ";
                        a = string.Format(a, auxRfc, desde, hasta, $"A{tipoAporta.Trim()}", aportacion, tipoRelacion, string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value), txtArchivo.Text, "p");

                        q = "insert into datos.aportaciones (rfc,inicio,final,new_tipo,movimiento,entrada,status,cuenta,fecharegistro,archivo,salida) values('{0}','{1}','{2}','{3}','APORTACION',{4},'{8}','{5}','{6}','{7}',0); ";
                        q = string.Format(q, auxRfc, desde, hasta, $"A{tipoAporta.Trim()}", aportacion, tipoRelacion, string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value), txtArchivo.Text, "p");
                    }
                    else
                    {

                        List<Dictionary<string, object>> objaa = empleados.Where(o => Convert.ToString(o["rfc"]) == auxRfc).Where(o => Convert.ToBoolean(o["pendiente"]) == false).ToList<Dictionary<string, object>>();
                        bool pendiente = !(objaa.Count != 0);

                        if (tipoAporta.Contains("N")) {
                            string dependencia = (proyecto.Length >= 3) ? proyecto.Substring(0, 3) : "";
                            queryEmpleado = $"update datos.empleados set sueldo_base = {sueldo} , cve_categ='{categoria}',proyecto ='{proyecto}',curp='{curp}',tipo_rel='{this.strCuentaaux}',nombre_em = '{nombre.Trim()}',depe='{dependencia}',modalidad='{modalidad}' where rfc = '{auxRfc}';";
                            query += queryEmpleado;
                        }

                        if (this.fechasCambiadas)
                        {
                            desde = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtDesde.Text));
                            hasta = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(txtHasta.Text));
                        }

                        a = "insert into historial.aportaciones (rfc,inicio,final,new_tipo,movimiento,entrada,status,cuenta,fecharegistro,archivo,salida) values('{0}','{1}','{2}','{3}','APORTACION',{4},'{8}','{5}','{6}','{7}',0); ";
                        a = string.Format(a, auxRfc, desde, hasta, $"A{tipoAporta.Trim()}", aportacion, tipoRelacion, string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value), txtArchivo.Text, pendiente ? "p" : "n");

                        q = "insert into datos.aportaciones (rfc,inicio,final,new_tipo,movimiento,entrada,status,cuenta,fecharegistro,archivo,salida) values('{0}','{1}','{2}','{3}','APORTACION',{4},'{8}','{5}','{6}','{7}',0); ";
                        q = string.Format(q, auxRfc, desde, hasta, $"A{tipoAporta.Trim()}", aportacion, tipoRelacion, string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value), txtArchivo.Text, pendiente ? "p" : "n");
                    }

                    query += q + a;
                }
                else
                {
                    string folio = Convert.ToString(item["folio"]);
                    string rfc = Convert.ToString(item["rfc"]).Trim();
                    string proyecto = Convert.ToString(item["proyecto"]);
                    string numdesc = Convert.ToString(item["numdesc"]);
                    string totdesc = Convert.ToString(item["totdesc"]);
                    string importe = Convert.ToString(item["importe"]);
                    string hasta = string.Empty;
                    try
                    {
                        string archivo = txtArchivo.Text;
                        string finalCadena = archivo.Substring(archivo.Length - 4);
                        string anio = finalCadena.Substring(0, 2);
                        string quincena = finalCadena.Substring(2);
                        int integerquincena = Convert.ToInt32(quincena);
                        hasta = quincenas[integerquincena]["hasta"];
                        hasta = string.Format(hasta, "20" + anio);
                        hasta = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(hasta));
                    }
                    catch
                    {

                    }
                    if (string.IsNullOrWhiteSpace(folio) ||
                        string.IsNullOrWhiteSpace(rfc) ||
                        string.IsNullOrWhiteSpace(proyecto) ||
                        string.IsNullOrWhiteSpace(numdesc) ||
                        string.IsNullOrWhiteSpace(totdesc) ||
                        string.IsNullOrWhiteSpace(importe) ||
                        string.IsNullOrWhiteSpace(hasta))
                    {
                        subir = true;
                        concate += contador.ToString() + ",";
                        datos1.Rows[contador - 1].DefaultCellStyle.BackColor = Color.LightPink;

                    }

                    if (rfc.Length > 13)
                    {
                        subir = true;
                        datos1.Rows[contador - 1].DefaultCellStyle.BackColor = Color.LightPink;
                        errorRfc = true;
                    }
                    string strCuenta = txtArchivo.Text.Substring(1, 3).ToUpper();
                    string tipoRelacion = string.Empty;
                    bool esCentralizado = false;
                    switch (strCuenta)
                    {
                        case "F51":
                            strCuenta = "F51";
                            esCentralizado = true;
                            break;
                        case "M51":
                            strCuenta = "M51";
                            esCentralizado = true;
                            break;
                        case "MBN":
                            strCuenta = "M51";
                            esCentralizado = true;
                            break;
                        case "SBN":
                            strCuenta = "M51";
                            esCentralizado = true;
                            break;
                        case "511":
                            strCuenta = "511";
                            esCentralizado = true;
                            break;
                        case "CMM":
                            strCuenta = "MMS";
                            esCentralizado = true;
                            break;
                        case "FCO":
                            strCuenta = "FCO";
                            esCentralizado = true;
                            break;
                        case "MCN":
                            strCuenta = "511";
                            esCentralizado = true;
                            break;
                        case "SCN":
                            strCuenta = "511";
                            esCentralizado = true;
                            break;
                        case "FMM":
                            strCuenta = "FMM";
                            esCentralizado = true;
                            break;
                        case "MM5":
                            strCuenta = "MMS";
                            esCentralizado = true;
                            break;
                        case "MMM":
                            strCuenta = "MMS";
                            esCentralizado = true;
                            break;
                        case "MMS":
                            strCuenta = "MMS";
                            esCentralizado = true;
                            break;
                        case "SMM":
                            strCuenta = "MMS";
                            esCentralizado = true;
                            break;
                        default:
                            esCentralizado = false;
                            break;
                    }

                    if (esCentralizado)
                    {
                        string proy = Convert.ToString(item["proyecto"]).Substring(0, 3);
                        string query2 = $"select * from catalogos.cuentas where proy = '{proy}'";
                        List<Dictionary<string, object>> resultado = globales.consulta(query2);
                        if (resultado.Count != 0)
                        {
                            string cuenta = Convert.ToString(resultado[0]["cuenta"]);
                            tipoRelacion = cuenta;
                        }
                    }
                    else
                    {
                        tipoRelacion = strCuenta;
                    }

                    int tipo = Convert.ToInt32(item["cvedesc"]);
                    string tipodescuento = (tipo == 205) ? "Q" : "H";

                    bool encontrado = false;
                    string cadena = string.Empty;

                    if (Convert.ToInt32(folio) == 91815) {

                    }

                    if (tipo == 205) {
                        encontrado = quiro.Any(o => Convert.ToInt32(o["folio"]) == Convert.ToInt32(folio) );
                        cadena = "quirografario";
                    }
                    else {
                        encontrado = hipo.Any(o => Convert.ToInt32(o["folio"]) == Convert.ToInt32(folio));
                        cadena = "hipotecario";
                    }

                    if (!encontrado) {
                        globales.MessageBoxExclamation($"No existe {cadena} en estado de cuenta\nFolio No. {folio}","No encontrado",globales.menuPrincipal);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    

                    string q = $"insert into datos.descuentos(folio,rfc,proyecto,numdesc,totdesc,importe,f_descuento,t_prestamo,f_registro,archivo,cuenta,tipo_rel) values ({folio},'{rfc}','{proyecto}',{numdesc},{totdesc},{importe},'{hasta}','{tipodescuento}','{string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value)}','{txtArchivo.Text}','{tipoRelacion.ToUpper()}','{strCuenta}'); ";
                    query += q;
                }
            }

            if (subir)
            {

                if (errorRfc)
                {
                    globales.MessageBoxError($"Error en los RFC's, porfavor de verificar", "Error en RFC's", globales.menuPrincipal);
                }
                else
                {
                    globales.MessageBoxError("Campos faltantes, verifique su archivo antes de subir la información", "Aviso", globales.menuPrincipal);
                    globales.MessageBoxExclamation($"Verifique las celdas rosas en la tabla presentada para verificar su error.", "Faltantes", globales.menuPrincipal);
                }
                Cursor = Cursors.Default;

                this.fechasCambiadas = false;
                this.btnFechabool = false;

                txtDesde.ReadOnly = true;
                txtDesde.Cursor = Cursors.No;


                txtHasta.ReadOnly = true;
                txtHasta.Cursor = Cursors.No;
                return;
            }

            if (globales.consulta(query, true))
            {
                globales.MessageBoxSuccess("Registros insertados correctamente", "Aviso", globales.menuPrincipal);
                if (this.aportacion)
                {
                    generarReporteAportacion();
                }
                  ((DataTable)datos1.DataSource).Rows.Clear();
                txtRuta.Text = "";
                txtArchivo.Text = "";
                txtDependencia.Text = "";
                txtDesde.Text = "";
                txtHasta.Text = "";
                txtConcepto.Text = "";
                txtTotalH.Text = "";
                txtTotalQ.Text = "";
                txtTotalQ.Visible = false;
                txtTotalH.Visible = false;
                lbl1.Visible = false;
                lbl2.Visible = false;
                this.fechasCambiadas = false;
                this.btnFechabool = false;

                txtDesde.ReadOnly = true;
                txtDesde.Cursor = Cursors.No;


                txtHasta.ReadOnly = true;
                txtHasta.Cursor = Cursors.No;
            }

            this.Cursor = Cursors.Default;

        }

        private void generarReporteAportacion()
        {
            string qry = "select count (rfc) as aportadores from datos.aportaciones where archivo='{0}'";
            string pasa = string.Format(qry, txtArchivo.Text);
            List<Dictionary<string, object>> resul = globales.consulta(pasa);
            string total_aportadores = Convert.ToString(resul[0]["aportadores"]);
            qry = "select SUM(entrada) as retro from datos.aportaciones where archivo='{0}' and new_tipo='AR'";
            string retro = string.Format(qry, txtArchivo.Text);
            List<Dictionary<string, object>> resul1 = globales.consulta(retro);
            string total_retro = string.IsNullOrWhiteSpace(Convert.ToString(resul1[0]["retro"])) ? "$0.00" : string.Format("{0:C}", Convert.ToDouble(resul1[0]["retro"]));

            qry = "select SUM(entrada) as normal from datos.aportaciones where archivo='{0}' and new_tipo='AN'";
            string pasanormal = string.Format(qry, txtArchivo.Text);
            List<Dictionary<string, object>> resul2 = globales.consulta(pasanormal);
            string total_normal = string.IsNullOrWhiteSpace(Convert.ToString(resul2[0]["normal"])) ? "$0.00" : string.Format("{0:C}", Convert.ToDouble(resul2[0]["normal"]));
            object[] aux2 = new object[resul2.Count];
            string totalsueldo = Convert.ToString(sumasueldo);

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[] parametros = { "totalaportadores", "totalretro", "totalnormal", "archivo", "dependencia", "sumasueldo" , "logo" };
            object[] valor = { total_aportadores, total_retro, total_normal, txtArchivo.Text, txtDependencia.Text, totalsueldo , logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reporte_de_subida", "p_quirog", new object[] { }, "", false, enviarParametros);
        }

        private void frmActualizar_Load(object sender, EventArgs e)
        {
            txtTotalH.Visible = false;
            txtTotalQ.Visible = false;

            lbl1.Visible = false;
            lbl2.Visible = false;

            lblDependencia.Visible = false;
            txtDependencia.Visible = false;

            //Generando el archivo Bat para la conexión de red...


            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\UNIDADRED.bat";
            if (File.Exists(ruta)) return;
            StreamWriter archivo = new StreamWriter(ruta);
            archivo.WriteLine("NET USE K: /D");
            archivo.NewLine = "";
            archivo.WriteLine(@"NET USE K: \\192.168.100.102\RESPALDOS Pensiones2.. /USER:Administrador\root");
            archivo.Close();
        }

        private void frmActualizar_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFecha_Click(object sender, EventArgs e)
        {
            txtDesde.Enabled = true;
            txtDesde.ReadOnly = false;
            txtDesde.Cursor = Cursors.Default;


            txtHasta.Enabled = true;
            txtHasta.ReadOnly = false;
            txtHasta.Cursor = Cursors.Default;


            this.btnFechabool = true;
        }

        private void txtDesde_TextChanged(object sender, EventArgs e)
        {
            if (this.btnFechabool)
                this.fechasCambiadas = true;

        }

        private void txtHasta_TextChanged(object sender, EventArgs e)
        {
            if (this.btnFechabool)
                this.fechasCambiadas = true;
        }
    }
}
