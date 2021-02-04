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
    public partial class frmExclamacion : Form
    {
        internal enviando enviando;
        public frmExclamacion(string mensaje,string titulo,Form ventana)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Owner = ventana;
            int x = (ventana.Location.X + ventana.Size.Width / 2) - (this.Width / 2);
            int y = (ventana.Location.Y + ventana.Size.Height / 2) - (this.Height / 2);
            this.Location = new Point(x, y);
            this.lblTexto.Text = mensaje;
            this.lblTitulo.Text = titulo;
            this.ShowInTaskbar = false;
        }

        private void frmExclamacion_Load(object sender, EventArgs e)
        {

        }

        internal void btn1_Click(object sender, EventArgs e)
        {
            this.enviando(DialogResult.Yes);
            this.Close();
        }
    }
}
