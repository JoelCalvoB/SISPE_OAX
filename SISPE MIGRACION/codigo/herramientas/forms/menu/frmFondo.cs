using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas.forms.menu
{
    delegate void frmFondo_Click(object sender, EventArgs e);
    delegate void cerrandometod();
    public partial class frmFondo : Form
    {
        private List<Dictionary<string, object>> lista;
        private string titulo = string.Empty;
        private setString enviar;
        private string tituloMenu;
        private frmMenu menu;
        internal frmFondo_Click cerrando;
        internal cerrandometod cerrarTodas;
        public frmFondo(Form ventana, List<Dictionary<string, object>> lista,string titulo,setString enviar,string tituloMenu)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Owner = ventana;
            this.Size = ventana.Size;
            this.Location = ventana.Location;
            this.Padding = ventana.Padding;
            this.lista = lista;
            this.titulo = titulo;
            this.enviar = enviar;
            this.tituloMenu = tituloMenu;
            this.ShowInTaskbar = false;
        }

        private void frmFondo_Load(object sender, EventArgs e)
        {
            menu = new frmMenu(this,lista,titulo,this.enviar,this.tituloMenu);
            menu.cerrando = frmFondo_Click;
            menu.cerrarTodas = this.cerrandoTodas;
            menu.KeyPreview = true;
           
            menu.Show();
            
        }

        private void frmFondo_KeyDown(object sender, KeyEventArgs e)
        {
         
            menu.Focus();
            if (e.Control && Keys.Right == e.KeyCode)
            {
                globales.derecha = true;
                this.Close();
            }
            if (e.Control && Keys.Left == e.KeyCode)
            {
                globales.izquierda = true;
                this.Close();
            }

        }

        private void frmFondo_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }


        private void cerrandoTodas() {
            this.Close();
            if (this.cerrarTodas != null)
                cerrarTodas();
        }

        private void frmFondo_Shown(object sender, EventArgs e)
        {
            menu.Focus();
        }
    }
}
