using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas.forms.huella
{
    public partial class frmEncontrarDispositivo : Form
    {
        ReaderCollection dispositivos { get; set; }
        public frmEncontrarDispositivo()
        {
            InitializeComponent();
        }

        private void frmEncontrarDispositivo_Load(object sender, EventArgs e)
        {
            dispositivos = ReaderCollection.GetReaders();
            listFingers.Items.Clear();
            foreach (Reader item in dispositivos) {
                listFingers.Items.Add(item.Description.Name);
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listFingers.SelectedIndex == -1)
            {
                DialogResult dialogonohay = globales.MessageBoxExclamation("Selecciona antes un dispositivo", "ALERTA", globales.menuPrincipal);
                return;
            }

            Reader seleccionado = dispositivos[listFingers.SelectedIndex];
            globales.dispositivo = seleccionado;
            conectado = true;
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmEncontrarDispositivo_Load(null, null);
        }
    }
}
