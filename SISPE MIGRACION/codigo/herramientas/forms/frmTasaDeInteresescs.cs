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
    public partial class frmTasaDeInteresescs : Form
    {
        private string tasa_ { get; set; }
        private bool esQuirog { get; set; }
        public List<Dictionary<string, object>> resultado;
        private string fecha { get; set; }
        public string tasa {
            get {
                return tasa_;
            }
        }

        public frmTasaDeInteresescs(string fecha,bool esQuirog = true)
        {
            InitializeComponent();
            this.esQuirog = esQuirog;
            this.fecha = fecha;
        }

        private void frmTasaDeInteresescs_Load(object sender, EventArgs e)
        {
            //Se obtiene los tipos de relación de catalogos.tasas de la DB
            string query = "select * from datos.c_tasai  ";
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            

            foreach (Dictionary<string,object> item in resultado) {
                RadioButton elementos = new RadioButton();
                elementos.Text = Convert.ToString(item["descripcion"]);
                elementos.Name = Convert.ToString(item["trel"]);
                elementos.PreviewKeyDown += new PreviewKeyDownEventHandler(pasar);
                elementos.KeyDown += new KeyEventHandler(apretar);
                groupRadio.Controls.Add(elementos);
            }
            RadioButton c =(RadioButton) groupRadio.Controls[0];
            c.Checked = true;
            c.Focus();
        }

        private void apretar(object sender, KeyEventArgs e)
        {
            button1_Click(null,null);
        }

        private void pasar(object sender, PreviewKeyDownEventArgs e)
        {
            SendKeys.Send("{TAB}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control item in  groupRadio.Controls) {
                RadioButton control = (RadioButton)item;
                if (control.Checked) {
                    tasa_ = control.Name;
                    break;
                }
            }
            string query = string.Format("select * from catalogos.tasa where t_prestamo = '{0}' and trel = '{1}' and fmodif is not null and fmodif <='{2}' " +
                "order by fmodif desc limit 1;",((this.esQuirog)?"Q":"H"),tasa,fecha);

            resultado = globales.consulta(query);
            
            Close();
        }
        
    }
}
