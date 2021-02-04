using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.codigo.herramientas.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Se mopdifico desde la maquina
namespace SISPE_MIGRACION.formularios
{
    public partial class login : Form
    {
        private const string formulario = "login";
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
#if DEBUG
            txtUsuario.Text = "";
            txtPassword.Text = "";
#endif

            label4.Text = $"Version: {Properties.Resources.version}";




            //string x = string.Empty;
            //foreach (string nombre in Directory.GetFiles(@"C:\Users\samv\Desktop\INFORMATICA2"))
            //{
            //    string[] split = nombre.Split('\\');
            //    string x1 = split[split.Length - 1].Replace(".dbf", "").Replace(".DBF", "");
            //    x += $"'{x1}'" + ",";
            //}

            string ruta = Environment.GetFolderPath((Environment.SpecialFolder.UserProfile)) + @"\imgSispe";
            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);


            string rutaimgsispe = ruta+ @"\imgSispe.txt";
            if (!File.Exists(rutaimgsispe))
            {
                StreamWriter write = new StreamWriter(rutaimgsispe);
                List<Dictionary<string, object>> rsultado = globales.consulta("select imgversion from catalogos.\"version\" ");
                string versiononline = Convert.ToString(rsultado[0]["imgversion"]);
                write.WriteLine(versiononline);
                write.Close();
                descargarImagenes(ruta);
            }
            else {
                //Se verifica que las imagenes esten en la actualización correspondiente...
                List<Dictionary<string, object>> rsultado = globales.consulta("select imgversion from catalogos.\"version\" ");
                string versiononline = Convert.ToString(rsultado[0]["imgversion"]);

                StreamReader leer = new StreamReader(rutaimgsispe);
                string versionarchivo = leer.ReadLine();
                leer.Close();

                if (!versiononline.Contains(versionarchivo)) {
                    StreamWriter escribir = new StreamWriter(rutaimgsispe);
                    escribir.WriteLine(versiononline);
                    escribir.Close();
                    descargarImagenes(ruta);
                }
            }


            try
            {
                string base64 = globales.getImagen(globales.imagenesSispe.logosispelogin);
                byte[] imageBytes = Convert.FromBase64String(base64);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    Image image = Image.FromStream(ms, true);
                    pictureBox1.Image = image;
                }
            }
            catch {

            }
        }

        private void descargarImagenes(string ruta)
        {
            List<Dictionary<string, object>> resultado = globales.consulta("select * from catalogos.imagenes");
            foreach (Dictionary<string,object> item in resultado) {
                string nombre = Convert.ToString(item["nombre"]);
                string imagen64 = Convert.ToString(item["imagen"]);
                if (string.IsNullOrWhiteSpace(imagen64)) continue;
                byte[] bytes = Convert.FromBase64String(imagen64);
                if (File.Exists($@"{ruta}\{nombre}.JPEG")) File.Delete($@"{ruta}\{nombre}.JPEG");
                MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
                ms.Write(bytes, 0, bytes.Length);
                Image image = Image.FromStream(ms, true);
                image.Save($@"{ruta}\{nombre}.JPEG");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (globales.actualizacion())
            {
                DialogResult p = MessageBox.Show("Hay una actualización nueva en el sistema, ¿Desea Actualizar?", "Actualización", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (p == DialogResult.No) goto continuar;
                globales.descargarArchivo();
                globales.actualizando();
                MessageBox.Show("El sistema se ha actualizado con exito, vuelva abrir el programa", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Eliminando el archivo generado de la instalación...
                string rutaInstalador = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\instalador.msi";
                if (File.Exists(rutaInstalador))
                    File.Delete(rutaInstalador);
                Application.Exit();
            }


            continuar:
            string metodo = "btnIngresar_Click";
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    MessageBox.Show("Favor de ingresar el usuario.","Ingresar usuario",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Favor de ingresar la contraseña.", "Ingresar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                string usuario = txtUsuario.Text;
                string password = txtPassword.Text;
                string query = string.Format("Select usuario,password,idusuario,tipomenu from catalogos.usuarios where usuario = '{0}' and password = '{1}'", usuario, password);
                var respuesta = baseDatos.consulta(query);
                if (respuesta.Count > 0)
                {
                    globales.id_usuario = Convert.ToString(respuesta[0]["idusuario"]);
                    globales.usuario = Convert.ToString(respuesta[0]["usuario"]);
                    globales.password = Convert.ToString(respuesta[0]["password"]);
                    globales.tipomenu = globales.convertInt(Convert.ToString(respuesta[0]["tipomenu"]));

                    if (chk.Checked) {
                         
                        string ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"/credenciales.txt";
                        if (File.Exists(ruta))
                            File.Delete(ruta);
                            string guardar = $"usuario:{globales.usuario.Trim()}|password:{globales.password.Trim()}|";
                        StreamWriter escribir = new StreamWriter(ruta);
                        escribir.WriteLine(guardar);
                        escribir.Close();

                    }

                    globales.menuPrincipal = new menuPrincipal();
                    globales.menuPrincipal.Show();
                    menuPrincipal menu = (menuPrincipal)globales.menuPrincipal;
                    menu.regresar = regresar;
                    Hide();
                }
                else
                    MessageBox.Show("Usuario y/o contraseña invalida","Error ingresar sistema",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

                
            }
            catch {
                MessageBox.Show(string.Format("Error formulario {0} método {1}",formulario,metodo));
            }
            this.Cursor = Cursors.Default;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) btnIngresar_Click(null, null);
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnIngresar_Click(null,null);
        }

        private void login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void login_Shown(object sender, EventArgs e)
        {

            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"/credenciales.txt";
            if (File.Exists(ruta))
            {
                try
                {
                    StreamReader leer = new StreamReader(ruta);
                    string texto = leer.ReadToEnd();
                    leer.Close();
                    string[] split = texto.Split('|');
                    string usuario = split[0].Split(':')[1];
                    string pass = split[1].Split(':')[1];

                    txtUsuario.Text = usuario;
                    txtPassword.Text = pass;
                    btnIngresar_Click(null, null);
                }
                catch { }
            }
        }

        public void regresar() {
            this.Visible = true;
        }
    }
}
