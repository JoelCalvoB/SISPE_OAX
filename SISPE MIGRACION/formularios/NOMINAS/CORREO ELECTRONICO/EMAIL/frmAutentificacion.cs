using EnviarCorreo;
using SISPE_MIGRACION.codigo.herramientas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.CORREO_ELECTRONICO.EMAIL
{
    public partial class frmAutentificacion : Form
    {
        public frmAutentificacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //visualizadorWeb browse = new visualizadorWeb();
            //browse.ShowDialog();
            ////Dictionary<string, string> token = browse.auntentificacion;


            //Limpiando cookies del navegador
            //this.webBrowser1.Document.ExecCommand("ClearAuthenticationCache", false, null);
            //webBrowser1.Document.Cookie.Remove(0, webBrowser1.Document.Cookie.Length);
            //Armado de la url de envío de auntentificación...

            string[] scopes = {
                "https://mail.google.com/",
                "https://www.googleapis.com/auth/gmail.compose",
                "https://www.googleapis.com/auth/gmail.insert",
                "https://www.googleapis.com/auth/gmail.labels",
                "https://www.googleapis.com/auth/gmail.metadata",
                "https://www.googleapis.com/auth/gmail.modify",
                "https://www.googleapis.com/auth/gmail.readonly",
                "https://www.googleapis.com/auth/gmail.send",
                "https://www.googleapis.com/auth/gmail.settings.basic",
                "https://www.googleapis.com/auth/gmail.settings.sharing"
            };
            string uri = "https://accounts.google.com/o/oauth2/v2/auth?";
            string clienteId = "client_id=566628402915-85dqr65ias67p462e4l6b5l498q89gn3.apps.googleusercontent.com";
            string responseType = "response_type=code";
            string scope = "scope=";
            string uri_redireccionamiento = "redirect_uri=http://localhost:53908/redireccion";
            foreach (string item in scopes)
                scope += item + "%20";


            uri += $"{clienteId}&{responseType}&{scope}&{uri_redireccionamiento}";

            Process proceso = new Process();
            ProcessStartInfo informacion = new ProcessStartInfo("chrome.exe", uri);
            proceso.StartInfo = informacion;
            proceso.Start();


            Dictionary<string, string> token = new Dictionary<string, string>();
            string tokenstr = Prompt.ShowDialog("Insertar código obtenido", "Obtener token");

            visualizadorWeb visualizador = new visualizadorWeb();
            string tokenObtenido = visualizador.peticionToken(tokenstr);

            tokenObtenido = tokenObtenido.Replace("{", "").Replace("}", "").Replace("\n", "").Replace("\"", "");
            string[] valores = tokenObtenido.Split(',');
            string tokenString = valores[0];
            string tipoTokenString = valores[3];

            string tokenSacado = tokenString.Split(':')[1];
            string tipoTokenSacado = tipoTokenString.Split(':')[1];

            token.Add("access_token", tokenSacado);
            token.Add("token_type", "Bearer");



            globales.showModal(new EnviarMensaje(token));
            this.Owner.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmAutentificacion_Load(object sender, EventArgs e)
        {

        }
    }
}
