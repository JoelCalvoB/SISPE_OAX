using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.codigo.herramientas.forms
{
    public partial class ingresarFecha : Form
    {
        private bool _aceptar;
        private string _fecha;
        public bool aceptar
        {
            get
            {
                return _aceptar;
            }
        }

        public string fecha
        {
            get
            {
                return _fecha;
            }
        }
        public ingresarFecha()
        {
            InitializeComponent();
            this._aceptar = false;
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            this._aceptar = true;
            this._fecha = string.Format("{0:yyyy-MM-dd}",this.fecha1.Value);
            this.Owner.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void fecha1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                btbuscar_Click(null,null);
            }
        }
    }
}
