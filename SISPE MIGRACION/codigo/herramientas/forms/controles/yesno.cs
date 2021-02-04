using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas.forms.controles
{
    public partial class yesno : Form
    {
        internal enviando enviando;
        private string mensaje = string.Empty;
        public yesno(Form ventana,string mensaje)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Owner = ventana;
            this.Size = new Size(ventana.Width,116);
            int x = ventana.Location.X;
            int y = (ventana.Size.Height /2 + ventana.Location.Y) - (this.Height / 2);
            this.Location = new Point(x,y);
            this.mensaje = mensaje;
            this.ShowInTaskbar = false;
        }

        private void yesno_Load(object sender, EventArgs e)
        {
            int widthButton = btn1.Size.Width;
            int widhtPanel = panel2.Size.Width;

            int mitad = widhtPanel / 2;

            int x1 = mitad - widthButton;

            btn1.Location = new Point(x1,btn1.Location.Y);

            btn2.Location = new Point(mitad,btn2.Location.Y);

            this.ActiveControl = this.btn1;
            this.label1.Text = this.mensaje;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        internal void btn1_Click(object sender, EventArgs e)
        {
            
            this.enviando(DialogResult.Yes);
            this.Close();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            this.enviando(DialogResult.No);
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
