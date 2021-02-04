using SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.REGISTRO_DE_INCIDENCIAS.OPCIONES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PENSION_ALIMENTICIA.OPCIONES
{
    public partial class frmModalPorcentaje : Form
    {

        private string cuentasvinculadas = string.Empty;
        private string rfc = string.Empty;
        private string nombre = string.Empty;
        private int numjub = 0;
        private double porcentaje { get; set; }
        private bool existe { get; set; }
        public frmModalPorcentaje(string claves_vinculadas,string rfc,string nombre,int numjpp,double porcentaje,bool existe)
        {
            InitializeComponent();
            this.cuentasvinculadas = claves_vinculadas;
            this.rfc = rfc;
            this.nombre = nombre;
            this.numjub = numjpp;
            this.porcentaje = porcentaje;
            this.existe = existe;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmModalPorcentaje_Load(object sender, EventArgs e)
        {
            txtrfc1.Text = this.rfc;
            txtnombre1.Text = this.nombre;

            string query = string.Empty;

            if (string.IsNullOrWhiteSpace(this.cuentasvinculadas))
            {
                query = $"select COALESCE(sum(monto),0)  as suma  from nominas_catalogos.nominew where jpp = 'JUB' and numjpp = {this.numjub} and tipo_nomina = 'N' and clave <= 69 ";
            }
            else {
                query = $"select COALESCE(sum(monto),0)  as suma  from nominas_catalogos.nominew where jpp = 'JUB' and numjpp = {this.numjub} and tipo_nomina = 'N' and clave <= 69 and clave in ({this.cuentasvinculadas}) ";
            }


            List<Dictionary<string, object>> resultado = globales.consulta(query);
            txtsueldo1.Text = globales.convertMoneda(globales.convertDouble(Convert.ToString(resultado[0]["suma"])));
            txtporcentaje1.Text = Convert.ToString(this.porcentaje) ;


            



        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtsueldo1_TextChanged(object sender, EventArgs e)
        {
            txtporcentaje1_Leave(null, null);
        }

        private void txtporcentaje1_Leave(object sender, EventArgs e)
        {
            txtporcentaje1.Text = globales.convertDouble(txtporcentaje1.Text.Replace("%", "")).ToString() + "%";

            double porcentaje = globales.convertDouble(txtporcentaje1.Text.Replace("%", ""));
            double sueldo = globales.convertDouble(txtsueldo1.Text);

            double total = (porcentaje * sueldo) / 100;

            txttotal1.Text = globales.convertMoneda(total);
        }

        private void txtporcentaje1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtporcentaje1.Text))
            {
                return;
            }

            double porcentaje = globales.convertDouble(txtporcentaje1.Text.Replace("%", ""));
            double sueldo = globales.convertDouble(txtsueldo1.Text);

            double total = (porcentaje * sueldo) / 100;

            txttotal1.Text = globales.convertMoneda(total);
        }

        private void button9_Click(object sender, EventArgs e)
        {
           
                frmOpcionesPercepciones frm = new frmOpcionesPercepciones(null, "JUB"+this.numjub, this.cuentasvinculadas);
                frm.enviar = this.insertartotal;
                globales.showModal(frm);
            
        }

        private void insertartotal(string total, string clavesViculadas)
        {
            this.txtsueldo1.Text = total;
            this.cuentasvinculadas = clavesViculadas;
        }

        private void button8_Click(object sender, EventArgs e)
        {

            this.enviar(txtrfc1.Text,txtnombre1.Text,txtsueldo1.Text,txtporcentaje1.Text,txttotal1.Text, this.cuentasvinculadas,existe);
            this.Owner.Close();
        }

        internal datos enviar;
    }

    delegate void datos(string rfc, string nombre, string sueldo, string porcentaje, string total,string clavesVinculadas,bool existe);
}
