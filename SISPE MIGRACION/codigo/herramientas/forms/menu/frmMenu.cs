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
    public partial class frmMenu : Form
    {
        private List<Dictionary<string, object>> lista;
        private string titulo = string.Empty;
        private setString enviar;
        private string tituloMenu;
        private Form ventana;
        internal frmFondo_Click cerrando;
        internal cerrandometod cerrarTodas;
        public frmMenu(Form ventana, List<Dictionary<string, object>> lista,string titulo,setString enviar,string tituloMenu)
        {
            InitializeComponent();
            int width = this.Size.Width;
            int height = ventana.Size.Height;
            this.Owner = ventana;
            int x = ventana.Location.X;
            int y = ventana.Location.Y;

            this.Size = new Size(width, height - 100);
            this.Location = new Point(x + 10, (y + (ventana.Height / 2) - (this.Height / 2)));
            this.lista = lista;
            this.titulo = titulo;
            this.enviar = enviar;
            this.tituloMenu = tituloMenu;
            this.ventana = ventana;
            this.ShowInTaskbar = false;
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            Button boton = btnPrueba;
            Label titulo = lblTitulo;
            int x = boton.Location.X;
            int y = boton.Location.Y;
            int tag = 0;
            panel1.Controls.Clear();
            panel1.Controls.Add(lblTitulo);
            lblTitulo.Text = this.titulo;
            foreach (Dictionary<string,object> item in lista) {
                string nombre = Convert.ToString(item["nombre"]);
                bool activo = Convert.ToBoolean(item["activo"]);
                List<Dictionary<string, object>> tmpLista = (List<Dictionary<string, object>>)item["submenu"];

                Button btnNuevo = new Button();
                btnNuevo.Text = nombre.ToUpper();
                if(tmpLista.Count == 0)
                    btnNuevo.Click += new EventHandler(evento);
                else
                    btnNuevo.Click += new EventHandler(submenu);
                btnNuevo.Location = new Point(x,y);
                btnNuevo.BackColor = boton.BackColor;
                btnNuevo.Font = boton.Font;
                btnNuevo.ForeColor = boton.ForeColor;
                btnNuevo.Padding = boton.Padding;
                btnNuevo.Margin = boton.Margin;
                btnNuevo.Cursor = Cursors.Hand;
                btnNuevo.Size = boton.Size;
                btnNuevo.Tag = tag;
                panel1.Controls.Add(btnNuevo);
                y += 35;
                tag++;
            }

        }

        private void submenu(object sender, EventArgs e)
        {
            Button btnaux = sender as Button;
            int tag = Convert.ToInt32(btnaux.Tag);
            Dictionary<string, object> obj = lista[tag];
            List<Dictionary<string, object>> tmpLista = (List<Dictionary<string, object>>)obj["submenu"];
            frmFondo f = new frmFondo(this, tmpLista, btnaux.Text, enviar, this.tituloMenu+btnaux.Text);
            f.cerrando = this.cerrando;
            f.cerrarTodas = this.cerrarTodas;
            f.ShowDialog();
            
        }

        private void evento(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            this.tituloMenu += boton.Text;
            enviar(this.tituloMenu);
            cerrarTodas();
        }

        private void label4_DragEnter(object sender, DragEventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.BackColor = Color.White;
        }

        private void label4_Enter(object sender, EventArgs e)
        {

        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.BackColor = Color.White;
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            Label etiqueta = sender as Label;
            etiqueta.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void frmMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) {
                cerrando(null,null);
            }

            if (e.Control && Keys.Right == e.KeyCode)
            {
                globales.derecha = true;
                this.Owner.Close();
            }
            if (e.Control && Keys.Left == e.KeyCode)
            {
                globales.izquierda = true;
                this.Owner.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
