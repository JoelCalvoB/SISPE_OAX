using SISPE_MIGRACION.formularios.sobres;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnviarCorreo
{
    public partial class EnviarMensaje : Form
    {
        Dictionary<string, string> token;
        private bool bandera;
        private string ruta = string.Empty;
        private string correo_select;


        public EnviarMensaje(Dictionary<string, string> token)
        {
            InitializeComponent();
            this.token = token;
        }

        private void enviarMensaje_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult p = open.ShowDialog();
            if (p == DialogResult.OK)
            {
                //string archivo = open.FileName;
                //txtImagen.Text = archivo;
                //byte[] archivobytes = File.ReadAllBytes(archivo);
                //string codificado = Convert.ToBase64String(archivobytes);
                //this.codificado = codificado;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(txtruta.Text)) {
                globales.MessageBoxExclamation("Seleccionar el triptico a enviar del mes.","Aviso",globales.menuPrincipal);
                return;                
            }

            bandera = false;
            string directorio = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\pdfjubilados";

            //Sentencia para nominas especiales
            bool aguinaldo = Directory.Exists(directorio+"\\AG");

            string directorioError = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Errorcorreoelectronico.txt";

            if (File.Exists(directorioError))
                File.Delete(directorioError);

            if (!Directory.Exists(directorio))
                bandera = true;
            else
                if (Directory.GetFiles(directorio).Count() == 0)
                bandera = true;



            if (bandera)
            {
                DialogResult p = globales.MessageBoxExclamation("Se debe generar los archivos PDF a enviar\n¿Desea generarlos ahora?", "Aviso", this);
                if (p == DialogResult.No)
                    return;

                this.Dispose();
                new frmsobres().ShowDialog();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAsunto.Text))
            {
                globales.MessageBoxExclamation("Ingresar asunto","Aviso",this);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMensaje.Text))
            {
                globales.MessageBoxExclamation("Ingresar el mensaje a enviar","Aviso",this);
                return;
            }
            //Se agrega el mensaje con la norma RFC 2822
            /*
                From: John Doe <jdoe@machine.example>
                To: Mary Smith <mary@example.net>
                Subject: Saying Hello
                Date: Fri, 21 Nov 1997 09:55:06 -0600
                Message-ID: <1234@local.machine.example>

                This is a message just to say hello.
                So, "Hello".
            */

            string query = "select   concat(jpp,num) as proyecto,correo from nominas_catalogos.maestro WHERE correo is not null and correo <> ''";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            this.correo_select = comboBox1.Text;
            if (string.IsNullOrWhiteSpace(correo_select)) return;
            string uri =$"https://www.googleapis.com/upload/gmail/v1/users/" +$"{correo_select}%40pensionesoaxaca.com/messages/send?uploadType=multipart";


            WebClient cliente = new WebClient();
            cliente.Headers[HttpRequestHeader.Authorization] = $"{"Bearer".Trim()} {this.token["access_token"].Trim()}";
            cliente.Headers[HttpRequestHeader.ContentType] = "message/rfc822";

            this.Cursor = Cursors.WaitCursor;
            int contador = 1;
            StreamWriter escribiendo = null;



            foreach (string item in Directory.GetFiles(directorio)) {
                //Sección para codificar el archivo a enviar..


          

                if (contador == 500) { //Envios máximos por día en GMAIL
                    globales.MessageBoxExclamation("Se ha llegado al límite de correos por día (500 correos) intentar el otro tanto al siguiente día","Límite de correos enviados",globales.menuPrincipal);
                    break;
                }
                int index = item.LastIndexOf('\\');
                string nombreArchivo = item.Substring(index + 1);
                nombreArchivo = nombreArchivo.Substring(0, nombreArchivo.IndexOf('.'));
                //if (nombreArchivo == "JUB3344")
                //{

                //}

                bool encontrado = resultado.Any(o => Convert.ToString(o["proyecto"]) == nombreArchivo);
                if (!encontrado) {
                    continue;
                }
                
                List<Dictionary<string, object>> obj = resultado.Where(o => Convert.ToString(o["proyecto"]) ==(nombreArchivo)).ToList<Dictionary<string,object>>();
                string correo = Convert.ToString(obj[0]["correo"]);
                if (string.IsNullOrWhiteSpace(correo)) {
                    continue;
                }
                byte[] archivobytes = File.ReadAllBytes(item);
                string codificado = Convert.ToBase64String(archivobytes);


                string itemAG = item.Substring(0,item.LastIndexOf('\\')) + "\\AG\\" + nombreArchivo + ".pdf";
                byte[] archivobytesAG = null;
                string codificadoAG = string.Empty;

                if (aguinaldo)
                {
                    if (File.Exists(itemAG))
                    {
                        archivobytesAG = File.ReadAllBytes(itemAG);
                        codificadoAG = Convert.ToBase64String(archivobytesAG);
                    }
                    else {

                    }
                }
               

                string formarMensaje = enviandoCorreo(correo,codificado, codificadoAG);

                string cuerpoMensajeEnviar = formarMensaje;

                try
                {
                var ver =  cliente.UploadString(uri, cuerpoMensajeEnviar);
                }
                catch(Exception ee) {
                    string jubiladosIncorrectos = nombreArchivo;
                    if (escribiendo == null)
                        escribiendo = new StreamWriter(directorioError);
                    
                    escribiendo.WriteLine(jubiladosIncorrectos);
                    escribiendo.NewLine = "";
                }
                string enviados = string.Format("Correo enviado {0}/{1}",contador, Directory.GetFiles(directorio).Count());
                System.Diagnostics.Debug.WriteLine(enviados);
                File.Delete(item);
                contador++;
            }
            globales.MessageBoxSuccess("Proceso terminado, correos enviados a todo registrado en el sistema", "Aviso",this);
            this.Cursor = Cursors.Default;
            if (escribiendo != null) {
                globales.MessageBoxInformation("Los pdf con error al envíar se encuentran en un archivo de texto creado en el escritorio automaticante, favor de verificar", "Errores de archivos de envío", this);
                escribiendo.Close();
            }
        }

        private string enviandoCorreo(string correo,string codigo,string codigoAG)
        {
            string formarMensaje = $"From: PENSIONES <{correo_select}@pensionesoaxaca.com>\n";//el correo from jamás debe cambiar

            formarMensaje += $"to: {correo}\n";
            formarMensaje += $"Subject: {txtAsunto.Text}\n";
            formarMensaje += "MIME-Version: 1.0\n";
            formarMensaje += "Content-Type: multipart/mixed;\n";
            formarMensaje += "        boundary=\"limite1\"\n\n";
            formarMensaje += "En esta sección se prepara el mensaje\n\n";
            formarMensaje += "--limite1\n";
            formarMensaje += "Content-Type: text/plain\n";
            formarMensaje += $"{txtMensaje.Text}\n\n";
            formarMensaje += "--limite1\n";
            formarMensaje += "Content-Type: application/pdf;\n\tname=Recibo.pdf;\n";
            formarMensaje += "Content-Transfer-Encoding: BASE64;\n\n";
            formarMensaje += codigo;
            if (!string.IsNullOrWhiteSpace(txtruta.Text)) {
                byte[] archivobytes = File.ReadAllBytes(this.ruta);
                string codificado = Convert.ToBase64String(archivobytes);
                formarMensaje += "\n\n--limite1\n";
                formarMensaje += "Content-Type: application/pdf;\n\tname=Tríptico.pdf;\n";
                formarMensaje += "Content-Transfer-Encoding: BASE64;\n\n";
                formarMensaje += codificado;
            }

            if (!string.IsNullOrWhiteSpace(codigoAG))
            {
                
                formarMensaje += "\n\n--limite1\n";
                formarMensaje += "Content-Type: application/pdf;\n\tname=aguinaldo.pdf;\n";
                formarMensaje += "Content-Transfer-Encoding: BASE64;\n\n";
                formarMensaje += codigoAG;
            }
            //  System.Diagnostics.Debug.WriteLine(formarMensaje); //Se diagnostica como se enviara el archivo por medio del correo electrónico
            return formarMensaje;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult p = open.ShowDialog();
            if (p == DialogResult.OK) {
                ruta = open.FileName;
                txtruta.Text = ruta.Substring(ruta.LastIndexOf('\\')+1);
                btnLimpiar.Visible = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtruta.Text = string.Empty;
            this.ruta = string.Empty;
            btnLimpiar.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
