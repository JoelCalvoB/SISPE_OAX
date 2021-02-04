using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.codigo.herramientas;
using SISPE_MIGRACION.codigo.herramientas.forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DPUruNet;
using Microsoft.Reporting.WinForms;
using System.Drawing;
using System.IO;

class globales
{
    internal static string id_usuario;
    internal static string nombre;
    internal static Form menuPrincipal = null;
    internal static bool esReporte;
    internal static bool sinPagos;
    internal static bool esConsulta;
    internal enum imagenesSispe
    {
        logoreportes,
        logosispelogin
    }

    internal static frmReporte reportesParaPanel(string v1, string v2, object[] v3, string v4, bool v5, object p, bool v6, string v7, bool v8)
    {
        throw new NotImplementedException();
    }

    internal static bool leftButton = false;

    public static bool alfaNumerico(char caracter)
    {
        return validaciones.alfaNumericos(caracter);
    }

    public static bool numerico(char caracter)
    {
        return validaciones.numerico(caracter);
    }
    public static bool alfa(char caracter) {
        return validaciones.alfa(caracter);
    }
    public static DateTime sacarFechaHabil(int dias, string fechaEspecifica = "") {
        return herramientas.sacarFechaHabil(dias, fechaEspecifica);
    }

    internal static bool actualizacion()
    {
         return herramientas.actualizacion();
        //return false;
    }

    public static object seleccionaTasaDeInteres(string fecha,bool hipotecario = false,bool leyvieja = false,string trel = "") {
        if (!hipotecario)
        {
            return herramientas.SeleccionaTasaInteres(fecha);
        }
        else {
            return herramientas.SeleccionaTasaInteresH(fecha,leyvieja,trel);
        }
    }

    internal static void descargarArchivo()
    {
        herramientas.descargarArchivo();
    }

    internal static void actualizando()
    {
        desintalador.actualizando();
    }

    internal static string convertMoneda(double importe)
    {
        string aux = string.Format("{0:C}",importe).Replace("$","");
        return aux;
    }

    public static dynamic consulta(string consulta , bool tipoSelect = false,bool eliminando = false) {
        return baseDatos.consulta(consulta, tipoSelect,eliminando);
    }
    public static dynamic consulta2(string consulta, bool tipoSelect = false)
    {
        return baseDatos.consulta2(consulta, tipoSelect);
    }
    public static string ShowDialog(string text, string caption) {
        return Prompt.ShowDialog(text, caption);
    }
    public static void imprimiendo() {
        herramientas.imprimirDocumento();
    }

    public static void reportes(string nombreReporte, string tablaSetNombre, object[] objeto, string mensaje = "", bool imprimir = false, object[] parametros = null, bool espdf = false, string nombrePdf = "",string subcarpeta = "") {
        herramientas.reportes(nombreReporte, tablaSetNombre, objeto, mensaje, imprimir, parametros, espdf, nombrePdf,subcarpeta);
    }

    public static ReportViewer reportesParaPanel(string nombreReporte, string tablaSetNombre, object[] objeto, string mensaje = "", bool imprimir = false, object[] parametros = null, bool espdf = false, string nombrePdf = "")
    {
       return  herramientas.reportesParaPanel(nombreReporte, tablaSetNombre, objeto, mensaje, imprimir, parametros, espdf, nombrePdf);
    }


    public static string convertirNumerosLetras(string numero, bool mayusculas) {

        
        return Convert.ToString(globales.consulta($"select * from datos.numletra({globales.convertDouble(numero)})")[0]["numletra"]);
    }

    public static string checarDecimales(object texto) {
        return herramientas.checarDecimales(texto);
    }
    public static string[] getMeses() {
        return herramientas.meses;
    }

    public static string formatoFecha(string fecha, char formato = '-') {
        return herramientas.formatoFecha(fecha, formato);
    }

    internal static void insertarRoles(List<Dictionary<string, object>> listaFinal, string idUsuario)
    {
        herramientas.insertarRoles(listaFinal, idUsuario);
    }

    public static dynamic leerDbf2(string ruta, bool verTipo = false) {
        return herramientas.leerDbf2(ruta, verTipo);
    }

    public static DataSet leerDbf(string ruta)
    {
        return herramientas.leerDbf(ruta);
    }

    internal static string justificar(string texto)
    {
        return herramientas.justificar(texto);
    }

    internal static object getMenu(int opcion = 0)
    {
        return herramientas.getMenu(opcion);
    }
    internal static object getMenuUsuario(string id_usuario = "",string tipoMenu1 = "")
    {
        if (!string.IsNullOrWhiteSpace(tipoMenu1)) {
            globales.tipomenu = globales.convertInt(Convert.ToString(tipoMenu1));
        }
        return herramientas.getMenuUsuario((string.IsNullOrWhiteSpace(id_usuario) ? globales.id_usuario : id_usuario));
    }

    internal static string getImagen(globales.imagenesSispe img)
    {
        string ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\imgSispe\";
        switch (img)
        {
            case globales.imagenesSispe.logoreportes:
                ruta += "logogobierno.JPEG";
                break;
            case globales.imagenesSispe.logosispelogin:
                ruta += "logoiniciarsesion.JPEG";
                break;
        }
        FileStream archivo = File.OpenRead(ruta);
        ruta = globales.ObtenerImagen_base64(ruta);
        return ruta;
    }

    public static DialogResult MessageBoxQuestion(string mensaje1, Form ventana) {
        frmAvisoPregunta pregunta = new frmAvisoPregunta(ventana, mensaje1);
        pregunta.ShowDialog();
        return pregunta.resultado;
    }

    internal static string ObtenerImagen_base64(string ruta)
    {
        using (Image image = Image.FromFile(ruta))
        {
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }

    public static DialogResult MessageBoxInformation(string mensaje1, string titulo, Form ventana)
    {
        frmAvisoPregunta pregunta = new frmAvisoPregunta(ventana, mensaje1, titulo, "informacion");
        pregunta.ShowDialog();
        return pregunta.resultado;
    }
    public static DialogResult MessageBoxExclamation(string mensaje1, string titulo, Form ventana)
    {
        frmAvisoPregunta pregunta = new frmAvisoPregunta(ventana, mensaje1, titulo, "exclamation");
        pregunta.BringToFront();
        pregunta.ShowDialog();
        return pregunta.resultado;
    }
    public static DialogResult MessageBoxError(string mensaje1, string titulo, Form ventana)
    {
        frmAvisoPregunta pregunta = new frmAvisoPregunta(ventana, mensaje1, titulo, "error");
        pregunta.ShowDialog();
        return pregunta.resultado;
    }
    public static DialogResult MessageBoxSuccess(string mensaje1, string titulo, Form ventana)
    {
        frmAvisoPregunta pregunta = new frmAvisoPregunta(ventana, mensaje1, titulo, "success");
        pregunta.ShowDialog();
        return pregunta.resultado;
    }

    internal static string parseDateTime(DateTime f_elab)
    {
        DateTime tiempo = new DateTime(1900,01,01);
        return f_elab <= tiempo ? "" : string.Format("{0:d}",f_elab);
    }

    public static DialogResult MessageBoxQuestion(string mensaje1, string titulo, Form ventana)
    {
        frmAvisoPregunta pregunta = new frmAvisoPregunta(ventana, mensaje1, titulo, "question");
        pregunta.ShowDialog();
        return pregunta.resultado;
    }

    public static Form aux;
    internal static bool izquierda;
    internal static bool derecha;
    internal static string usuario;
    internal static string password;
    internal static bool boolConsulta;
    internal static bool ocultarEpilep;
    internal static string datosConexion;
    internal static int tipomenu;
    internal static Reader dispositivo;
    internal static bool estadoResultado;

    public static DialogResult showModal( Form ventana)
    {
        globales.aux = ventana;
        ventana.ShowInTaskbar = false;
        frmAvisoPregunta pregunta = new frmAvisoPregunta(globales.menuPrincipal, "", "", "modal",ventana);
        pregunta.Shown += new EventHandler(cargado);
        pregunta.Show();
        return pregunta.resultado;
    }

    public static Form showModalReturning(Form ventana)
    {
        globales.aux = ventana;
        ventana.ShowInTaskbar = false;
        frmAvisoPregunta pregunta = new frmAvisoPregunta(globales.menuPrincipal, "", "", "modal", ventana);
        pregunta.Shown += new EventHandler(cargado);
        pregunta.ShowDialog();
        return ventana;
    }

    private static void cargado(object sender, EventArgs e)
    {
        aux.Focus();
    }

    public static List<string> justificar(string text, int width) {
        return herramientas.justificar(text,width);
    }

    internal static object getNombre(string nombre = "")
    {
        return herramientas.getMenuUsuario((string.IsNullOrWhiteSpace(id_usuario) ? globales.id_usuario : id_usuario));
    }

    public static int convertInt(string numero) {
        string strNumero = (string.IsNullOrWhiteSpace(numero)) ? "0" : numero;
        int numeroaux = 0;
        try {
            numeroaux = int.Parse(strNumero, System.Globalization.NumberStyles.Currency);
        }
        catch {
            numeroaux = 0;
        }
        return numeroaux;
    }
    public static double convertDouble(string numero)
    {
        string strNumero = (string.IsNullOrWhiteSpace(numero)) ? "0" : numero;
        double dblNumero = 0;
        try
        {
            dblNumero = double.Parse(strNumero, System.Globalization.NumberStyles.Currency);
        }
        catch {
            dblNumero = 0;
        }
        return dblNumero;
    }

    public static DateTime convertDatetime(string fecha) {
        DateTime fecha1;
        DateTime.TryParse(fecha,out fecha1);
        return fecha1;
    }

    public static string convertNull(string cadena) {
        return string.IsNullOrWhiteSpace(cadena) ? "null" : cadena;
    }

    public static void crearDbf() {
        herramientas.crearDbf("");
    }
}

