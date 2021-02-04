using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS
{
    delegate void enviarMetodo(string expediente,int opcion,Dictionary<string,object> datos);
    public partial class frmAmpliacion : Form
    {
        string[] items;
        private string nombre;
        private string expediente;
        internal bool aceptar = false; 
        internal int opcion = 0;
        internal enviarMetodo enviar;
        private Dictionary<string, object> diccionario;
        public frmAmpliacion(string nombre,string expediente, Dictionary<string, object> datos,params string[]  items)
        {
            InitializeComponent();
            this.items = items;
            this.nombre = nombre;
            this.expediente = expediente;
            this.diccionario = datos;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmAmpliacion_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (string item in items) {
                listBox1.Items.Add(item);
            }

            lbltitulo.Text = this.nombre;
            txtexpediente.Text = this.expediente;

        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            this.aceptar = true;
            this.opcion = listBox1.SelectedIndex;
            this.enviar(txtexpediente.Text,opcion,diccionario);
            this.Owner.Close();
        }

        private void frmAmpliacion_Shown(object sender, EventArgs e)
        {
            ActiveControl = listBox1;
            listBox1.SelectedIndex = 0;

            
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode) {
                btbuscar_Click(null,null);
            }
        }
    }
}
