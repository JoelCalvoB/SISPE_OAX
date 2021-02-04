using Microsoft.Reporting.WinForms;
using SISPE_MIGRACION.codigo.herramientas.forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SISPE_MIGRACION.codigo.herramientas
{
    class herramientas
    {
        public static string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        public static DateTime sacarFechaHabil(int dias, string fechaEspecifica = "")
        {
            DateTime tiempo = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(fechaEspecifica))
            {
                string[] aux = fechaEspecifica.Split('/');
                tiempo = new DateTime(Convert.ToInt32(aux[2]), Convert.ToInt32(aux[1]), Convert.ToInt32(aux[0]));
            }
            int contador = dias;
            for (int x = 1; x <= contador; x++)
            {
                DayOfWeek nombreDia = tiempo.DayOfWeek;
                string nombre = nombreDia.ToString();
                if (nombre == "Saturday" || nombre == "Sunday")
                {
                    contador++;
                }
                tiempo = tiempo.AddDays(1);
            }


            return tiempo;
        }

        internal static object SeleccionaTasaInteresH(string fecha,bool leyvieja = false,string trel = "")
        {
            string query = $"select * from catalogos.tasa where t_prestamo = 'H' and trel = '{trel}'  order by fmodif desc";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count == 0) return 0;
            double valor = 0;
            if (!leyvieja)
            {
                Dictionary<string, object> diccionario = resultado[0];
                valor = Convert.ToDouble(diccionario["tasa"])/24/100;
            }
            else {
                Dictionary<string, object> diccionario = resultado[0];
                double aux = Convert.ToDouble(diccionario["tasa"]);
                foreach (var item in resultado) {
                    if (aux != Convert.ToDouble(item["tasa"]))
                    {
                        aux = Convert.ToDouble(item["tasa"])/24/100;
                        break;
                    }
                }
                valor = aux;
            }
            return valor;
        }

        internal static void descargarArchivo()
        {
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\instalador.msi";
            List<Dictionary<string, object>> resultado = globales.consulta("select instalador from catalogos.version");
            string base64 = Convert.ToString(resultado[0]["instalador"]);
            byte[] MSIbytes = Convert.FromBase64String(base64);


            File.WriteAllBytes(ruta, MSIbytes);
        }

        internal static bool actualizacion()
        {
            try
            {
                List<Dictionary<string, object>> res = globales.consulta("select version from catalogos.version");
                string resultado = Convert.ToString(res[0]["version"]);
                string version1 = Properties.Resources.version;
                return resultado != version1;
            }
            catch
            {
                return false;
            }
        }

        internal static void reportes(string nombreReporte, string tablaDataSet, object[] objeto, string mensaje, bool imprimir = false, object[] parametros = null, bool esPdf = false, string nombreArchivo = "",string subcarpeta = "")
        {
            frmReporte reporte = new frmReporte(nombreReporte,subcarpeta, tablaDataSet);
            

            reporte.setParametrosExtra(esPdf, nombreArchivo);
            reporte.cargarDatos(tablaDataSet, objeto, mensaje, imprimir, parametros);
            reporte.ShowDialog();
        }

        internal static ReportViewer reportesParaPanel(string nombreReporte, string tablaDataSet, object[] objeto, string mensaje, bool imprimir = false, object[] parametros = null, bool esPdf = false, string nombreArchivo = "", string subcarpeta = "")
        {
            frmReporte reporte = new frmReporte(nombreReporte,subcarpeta, tablaDataSet);


            reporte.setParametrosExtra(esPdf, nombreArchivo);
            reporte.cargarDatos(tablaDataSet, objeto, mensaje, imprimir, parametros);
            return reporte.reportViewer1;
        }






        public static dynamic SeleccionaTasaInteres(string fecha)
        {
            object tasaDeInteres = null;

            frmTasaDeInteresescs tasa = new frmTasaDeInteresescs(fecha);
            tasa.ShowDialog();
            tasaDeInteres = tasa.resultado;

            return tasaDeInteres;
        }

        internal static void insertarRoles(List<Dictionary<string, object>> listaFinal, string idUsuario)
        {
            try
            {
                string query = "";
                string query2 = string.Empty;
                foreach (Dictionary<string, object> item in listaFinal)
                {
                    string idMenu = Convert.ToString(item["id"]);
                    bool activo = Convert.ToBoolean(item["activo"]);
                    query = $"select * from catalogos.detalle_usuario_menu where id_usuario = {idUsuario} and id_menu = {idMenu}";
                    List<Dictionary<string, object>> tmp = globales.consulta(query);
                    int cantidad = tmp.Count;

                    string xml = "<submenu>" + crearXml((List<Dictionary<string, object>>)item["submenu"]) + "</submenu>";
                    if (cantidad == 0)
                    {
                        query2 += $" insert into catalogos.detalle_usuario_menu values({idUsuario},{idMenu},{activo},'{xml}'); ";
                    }
                    else
                    {
                        query2 += $" update catalogos.detalle_usuario_menu set activo = {activo},submenu = '{xml}' where id_usuario = {idUsuario} and id_menu = {idMenu}; ";
                    }
                }
                globales.consulta(query2, true);
            }
            catch
            {

            }
        }

        internal static object getMenuUsuario(string id_usuario)
        {

            List<Dictionary<string, object>> listaFinal = new List<Dictionary<string, object>>();

            try
            {
                int menu = globales.tipomenu;
                string menutomar = string.Empty;

                switch (menu) {
                    case 0:
                        menutomar = "menu_principal";
                        break;
                    case 1:
                        menutomar = "menu_principal_nominajubilados";
                        break;
                    case 2:
                        menutomar = "menu_principal_nominaactivos";
                        break;
                    case 3:
                        menutomar = "menu_principal_cg";
                        break;
                    case 4:
                        menutomar = "menu_principal_superadmin";
                        break;

                }

                string query = $"select detalle.id_menu as id,menu.nombre,menu.activo,detalle.submenu from catalogos.detalle_usuario_menu  detalle inner join catalogos.{menutomar} menu on menu.id = detalle.id_menu ";
                query += $" where detalle.id_usuario = {id_usuario} and detalle.activo = true order by detalle.id_menu asc";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                foreach (Dictionary<string, object> item in resultado)
                {
                    Dictionary<string, object> diccionario = new Dictionary<string, object>();
                    foreach (string llave in item.Keys)
                    {
                        if (llave == "submenu") continue;
                        diccionario.Add(llave, item[llave]);
                    }
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(Convert.ToString(item["submenu"]));
                    XmlNode nodo = xml.FirstChild;
                    diccionario.Add("submenu", sacarSubMenus(nodo));
                    listaFinal.Add(diccionario);
                }
            }
            catch
            {
                MessageBox.Show("Error en el sistema", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return listaFinal;
        }

        private static string crearXml(List<Dictionary<string, object>> list)
        {
            string xml = string.Empty;

            int contador = 1;
            foreach (Dictionary<string, object> item in list)
            {
                xml += $"<menu{contador}>";
                xml += $"<nombre>{item["nombre"]}</nombre>";
                xml += $"<activo>{item["activo"]}</activo>";
                xml += $"<submenu>{crearXml((List<Dictionary<string, object>>)item["submenu"])}</submenu>";
                xml += $"</menu{contador}>";

                contador++;
            }
            return xml;
        }

        internal static string justificar(string texto)
        {
            int tamaño = 70;
            string[] arreglo = texto.Split((char)32);
            int cantidad = 0;
            List<int> posiciones = new List<int>();
            for (int x = 0; x < arreglo.Length; x++)
            {
                if (arreglo[x].Length == 1)
                    posiciones.Add(x);


            }
            return null;
        }

        internal static object getMenu(int menutablaopcion)
        {
            List<Dictionary<string, object>> listaFinalMenu = new List<Dictionary<string, object>>();
            try
            {
                string query = string.Empty;
                switch (menutablaopcion) {
                    case 0:
                        query = "select * from catalogos.menu_principal order by id asc";
                        break;
                    case 1:
                        query = "select * from catalogos.menu_principal_nominajubilados order by id asc";
                        break;
                    case 2:
                        query = "select * from catalogos.menu_principal_nominaactivos order by id asc";
                        break;
                    case 3:
                        query = "select * from catalogos.menu_principal_cg order by id asc";
                        break;
                    case 4:
                        query = "select * from catalogos.menu_principal_superadmin order by id asc";
                        break;
                }
                
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                foreach (Dictionary<string, object> item in resultado)
                {
                    Dictionary<string, object> diccionario = new Dictionary<string, object>();
                    foreach (string llave in item.Keys)
                    {
                        if (llave == "submenu") continue;
                        diccionario.Add(llave, item[llave]);
                    }
                    string stringxml = Convert.ToString(item["submenu"]);
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(stringxml);
                    XmlNode nodo = xml.FirstChild;
                    diccionario.Add("submenu", sacarSubMenus(nodo));
                    listaFinalMenu.Add(diccionario);
                }
            }
            catch
            {
                MessageBox.Show("Error al traer datos del menú", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return listaFinalMenu;
        }

        //Método recursivo...
        private static object sacarSubMenus(XmlNode nodo)
        {
            List<Dictionary<string, object>> listaSubMenu = new List<Dictionary<string, object>>();
            foreach (XmlNode item in nodo.ChildNodes)
            {
                Dictionary<string, object> diccionario = new Dictionary<string, object>();
                diccionario.Add("nombre", item["nombre"].InnerText);
                diccionario.Add("activo", item["activo"].InnerText);
                diccionario.Add("submenu", sacarSubMenus(item.LastChild));
                listaSubMenu.Add(diccionario);
            }
            return listaSubMenu;
        }

        public static void imprimirDocumento()
        {


        }
        public static string numerosALetras(int number)
        {
            if (number == 0)
                return "Cero";

            if (number < 0)
                return "Menos " + numerosALetras(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += numerosALetras(number / 1000000) + " Millones ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += numerosALetras(number / 1000) + " Mil ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += numerosALetras(number / 100) + " Cien ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "Y ";

                var unitsMap = new[] { "cero", "Uno", "Dos", "Tres", "Cuatro", "Cinco", "Seis", "Siete", "Ocho", "Nueve", "Diez", "Once", "Doce", "Trece", "Catorce", "Quince", "Dieciséis", "diecisiete", "dieciocho", "diecinueve" };
                var tensMap = new[] { "Cero", "Diez", "Veinte", "Treinta", "Cuarenta", "Cincuenta", "Sesenta", "Setenta", "Ochenta", "Noventa" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        public static string checarDecimales(object cadena)
        {
            string aux = "";

            aux = Convert.ToDouble(cadena).ToString("#.##");


            if (aux.Contains("."))
            {
                string[] tmp = aux.Split('.');
                if (tmp[1].Length == 1)
                {
                    aux += "0";
                }
            }
            else
            {
                aux += ".00";
            }



            return aux;
        }

        public static string formatoFecha(string fecha, char formato)
        {
            string f1 = string.Empty;
            if (string.IsNullOrWhiteSpace(fecha)) return "";

            string[] arreglo;
            if ('-' == formato)
            {
                arreglo = fecha.Split('/');
                if (arreglo.Length != 3) return "";

                f1 = string.Format("{0}-{1}-{2}", arreglo[2], arreglo[1], arreglo[0]);
            }
            else
            {
                arreglo = fecha.Split('-');
                if (arreglo.Length != 3) return "";

                f1 = string.Format("{0}/{1}/{2}", arreglo[2], arreglo[1], arreglo[0]);
            }
            return f1;
        }

        public static List<Dictionary<string, object>> leerDbf2(string ruta, bool verTipo)
        {
            DbfDataReader.DbfTable tabla = new DbfDataReader.DbfTable(ruta);

            DbfDataReader.DbfRecord filas = new DbfDataReader.DbfRecord(tabla);
            List<Dictionary<string, object>> lista = new List<Dictionary<string, object>>();
            string[] tipos = new string[tabla.Columns.Count];
            string[] nombreColumnas = new string[tabla.Columns.Count];
            int contador = 0;

            foreach (var dbfColumn in tabla.Columns)
            {
                string tipo = Convert.ToString(dbfColumn.ColumnType);
                string nombreColumna = Convert.ToString(dbfColumn.Name).ToLower();
                tipos[contador] = tipo;
                nombreColumnas[contador] = nombreColumna;
                contador++;
            }
            int aux = 0;
            while (tabla.Read(filas))
            {
                contador = 0;
                Dictionary<string, object> diccionario = new Dictionary<string, object>();
                foreach (var dbfValue in filas.Values)
                {
                    string obj = Convert.ToString(dbfValue.GetValue()).Replace(" 12:00:00 a. m.", "");
                    obj = (string.IsNullOrWhiteSpace(Convert.ToString(obj)) && tipos[contador].Equals("Number") ? "0" : obj);
                    if (verTipo)
                        obj = obj + "|" + ((tipos[contador].Equals("Number")) ? "N" : "C");

                    diccionario.Add(nombreColumnas[contador], obj);
                    contador++;
                }
                if (aux == 330)
                {

                }
                lista.Add(diccionario);
                aux++;
                System.Diagnostics.Debug.WriteLine(aux);
            }
            tabla.Close();
            return lista;
        }

        public static DataSet leerDbf(string ruta)
        {

            string cadena = "Provider=VFPOLEDB.1; Data Source= {0}\\; Extended Properties =dBase IV; ";
            string pasa = string.Format(cadena, ruta.Substring(0, ruta.LastIndexOf("\\")));
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = pasa;
            con.Open();

            string n1 = ruta.Substring(ruta.LastIndexOf('\\') + 1);
            string archivo = n1.Substring(0, n1.IndexOf('.'));

            string query = $"select * from {archivo}";
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
            
            DataSet dt = new DataSet();
            try
            {
                adapter.Fill(dt);
            }
            catch
            {
                MessageBox.Show("Error al leer el archivo DBF", "Error de lectura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
            return dt;

        }

        public static void crearDbf(string ruta)
        {
            ruta = @"C:\Users\samv\Desktop\";
            string cadena = "Provider=VFPOLEDB.1; Data Source= {0}\\; Extended Properties =dBase IV; ";
            string pasa = string.Format(cadena, ruta.Substring(0, ruta.LastIndexOf("\\")));
            using (OleDbConnection connection = new OleDbConnection(pasa))
            using (OleDbCommand command = connection.CreateCommand())
            {
                connection.Open();

                OleDbParameter script = new OleDbParameter("script", @"CREATE TABLE Test (Id I, Changed D, Name C(100))");

                //command.CommandType = CommandType.StoredProcedure;
                //command.CommandText = "ExecScript";
                //command.Parameters.Add(script);
                //command.ExecuteNonQuery();

                script = new OleDbParameter("script", @"INSERT INTO test (id,changed,name) VALUES (2,CTOD('01/01/2017'),'alejandr')");

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ExecScript";
                command.Parameters.Add(script);
                command.ExecuteNonQuery();

                connection.Close();
            }



        }

        public static List<string> justificar(string text, int width)
        {
            string[] palabras = text.Split(' ');
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            int length = palabras.Length;
            List<string> resultado = new List<string>();
            for (int i = 0; i < length; i++)
            {
                sb1.AppendFormat("{0} ", palabras[i]);
                if (sb1.ToString().Length > width)
                {
                    resultado.Add(sb2.ToString());
                    sb1 = new StringBuilder();
                    sb2 = new StringBuilder();
                    i--;
                }
                else
                {
                    sb2.AppendFormat("{0} ", palabras[i]);
                }
            }
            resultado.Add(sb2.ToString());

            List<string> resultado2 = new List<string>();
            string temp;

            int index1, index2, salto;
            string target;
            int limite = resultado.Count;
            foreach (var item in resultado)
            {
                target = " ";
                temp = item.ToString().Trim();
                index1 = 0; index2 = 0; salto = 2;

                if (limite <= 1)
                {
                    resultado2.Add(temp);
                    break;
                }
                while (temp.Length <= width)
                {
                    if (temp.IndexOf(target, index2) < 0)
                    {
                        index1 = 0; index2 = 0;
                        target = target + " ";
                        salto++;
                    }
                    index1 = temp.IndexOf(target, index2);
                    temp = temp.Insert(temp.IndexOf(target, index2), " ");
                    index2 = index1 + salto;

                }
                limite--;
                resultado2.Add(temp);
            }
            return resultado2;
        }
    }
}

