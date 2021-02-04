using SISPE_MIGRACION.codigo.repositorios.cg_fondos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.MIGRACIÓN_CG.CONSULTAS
{
    public partial class frmLibrodiario : Form
    {

       

        private string queryOficial = "SELECT *  FROM cg_fondos.des " + 
            " WHERE entidad = '801' AND subsistema = 'P' AND   polmes = '{0}' and  poltipo = '{1}' and anio = '{2}' " +
            "  ORDER BY poltipo, prefijo, polnumero ";
        private int row;
        private string meses;

        public frmLibrodiario()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            //  this.dt.Dock = DockStyle.Fill;
            dtggrid.Rows.Clear();

            string mes = string.Empty;
          

            mes = tabControl1.SelectedIndex < 10 ? $"0{tabControl1.SelectedIndex}": $"{tabControl1.SelectedIndex}";

            string tipopol = string.Empty;
            if (comboBox1.SelectedIndex == 0) {
                tipopol = "D";
            } else if (comboBox1.SelectedIndex == 1) {
                tipopol = "E";
            } else if (comboBox1.SelectedIndex == 2) {
                tipopol = "I";
            }

            string query = string.Format(this.queryOficial, mes, tipopol, txtanio.Text);
            
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            double debe = 0;
            double haber = 0;

                foreach (Dictionary<string,object> o in resultado) {
                    int modificado = this.dtggrid.Rows.Add($"{o["poltipo"]}-{o["prefijo"]}{o["polnumero"]}", string.Format("{0:dd}", DateTime.Parse(Convert.ToString(o["fecha"]))), o["concepto"], string.Format("{0:c}", o["debe"]), string.Format("{0:c}", o["haber"]), o["grupo"], o["status"], o["polnumero"], o["prefijo"], string.Format("{0:dd/MM/yyyy}", DateTime.Parse(Convert.ToString(o["fecha"]))), o["elaboro"]);
                    if (dtggrid.Rows[modificado].Cells[2].Value.ToString().ToUpper().Contains("CANCELADO"))
                        this.dtggrid.Rows[modificado].DefaultCellStyle.ForeColor = Color.Red;

                debe += globales.convertDouble(Convert.ToString(o["debe"]));
                haber += globales.convertDouble(Convert.ToString(o["haber"]));
            }

            txtDebe.Text = string.Format("{0:c}",debe);
            txtHaber.Text = string.Format("{0:c}", haber);


            if (!string.IsNullOrWhiteSpace(meses)) {
                string apertura = meses.Substring(tabControl1.SelectedIndex, 1);
                lblabierto.Visible = apertura == "1";
                btnCerrarmes.Visible = apertura == "1";
            }

            Cursor = Cursors.Default;
        }

        private void frmLibrodiario_Load(object sender, EventArgs e)
        {
            string query = $"select * from cg_fondos.cierremeses where anio = {DateTime.Now.Year}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count != 0)
            {
                meses = Convert.ToString(resultado[0]["mes"]);
            }
            Cursor = Cursors.WaitCursor;
            txtanio.Text = DateTime.Now.Year.ToString();




            comboBox1.SelectedIndex = 0;


            tabControl1.SelectedIndex = DateTime.Now.Month;
            if (!string.IsNullOrWhiteSpace(meses))
            {
                string apertura = meses.Substring(DateTime.Now.Month, 1);
                lblabierto.Visible = apertura == "1";
                btnCerrarmes.Visible = apertura == "1";
            }



            Cursor = Cursors.Default;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl1_Click(null,null);
        }

        private void dtggrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
             row       =      e.RowIndex;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (this.dtggrid.Rows.Count == 0) return;
            DataGridViewRow dato =  this.dtggrid.Rows[this.row];
            string numpoliza = Convert.ToString(dato.Cells[7].Value);
            string fecha = Convert.ToString(dato.Cells[9].Value);
            string polmes = this.tabControl1.SelectedIndex < 10 ? $"0{this.tabControl1.SelectedIndex}" : $"{this.tabControl1.SelectedIndex}";
            string tipopol = string.Empty;
            string concepto = Convert.ToString(dato.Cells[2].Value);

            string nombreTipoPol = string.Empty;
            if (comboBox1.SelectedIndex == 0) {
                tipopol = "D";
                nombreTipoPol = "Diario";
            } else if (comboBox1.SelectedIndex == 1) {
                tipopol = "E";
                nombreTipoPol = "Egresos";
            } else {
                tipopol = "I";
                nombreTipoPol = "Ingresos";
            }

            string query = $"select nombre from catalogos.usuarios where idusuario = {dato.Cells[9].Value}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            string elaboro = "USUARIO ANTERIOR CG";
            if (resultado.Count != 0) {
                elaboro = Convert.ToString(resultado[0]["nombre"]);
            }

            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("tipopol",nombreTipoPol);
            obj.Add("prefijo", (dato.Cells[8].Value));
            obj.Add("fecha",fecha);
            obj.Add("elaboro",elaboro);
            obj.Add("concepto", concepto);

            



            frmLibroDiarioDetalle detalle = new frmLibroDiarioDetalle(polmes, Convert.ToInt16(this.txtanio.Text), numpoliza, tipopol, obj, this.lblabierto.Visible);
            globales.showModal(detalle);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            globales.MessageBoxInformation("Se creara el reporte de polizas", "Aviso", globales.menuPrincipal);

            if (this.dtggrid.Rows.Count == 0) {
                globales.MessageBoxExclamation("No hay datos que visualizar","Aviso",globales.menuPrincipal);

                return;
            }

            DataGridViewRow item = this.dtggrid.Rows[this.row];
            string descripcionpol = Convert.ToString(item.Cells[2].Value);
            string numeropoliza = Convert.ToString(item.Cells[7].Value);
            string prefijo = Convert.ToString(item.Cells[8].Value);
            string fecha = Convert.ToString(item.Cells[9].Value);
            string polmes = tabControl1.SelectedIndex < 10 ? $"0{tabControl1.SelectedIndex}" : $"{tabControl1.SelectedIndex}";
            string tipopol = string.Empty;
            string nombretipo = string.Empty;
            if (comboBox1.SelectedIndex == 0)
            {
                tipopol = "D";
                nombretipo = "DIARIO";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                tipopol = "E";
                nombretipo = "EGRESOS";
            }
            else
            {
                tipopol = "I";
                nombretipo = "INGRESOS";
            }

            string query = $"select * from cg_fondos.pol where polnumero = '{numeropoliza}' and polmes = '{polmes}' and anio = {txtanio.Text} and poltipo = '{tipopol}' order by naturaleza, cuenta";

            List<pol> obj = new dbaseORM().queryForList<pol>(query);


            object[] arreglo = new object[obj.Count];
            int contador = 0;


            foreach (pol ii in obj) {

                string cuentamayor = (ii.cuenta.Replace(".", "").Length > 5) ? ii.cuenta.Replace(".", "").Substring(0, 5) : ii.cuenta.Replace(".", "").Substring(0, ii.cuenta.Length);

                query = $"select  replace(cuenta,'.','') as cuenta,descripcion from cg_fondos.cuentas where replace(cuenta,'.','') like '{cuentamayor}%' order by cuenta limit 1";

                string descripcion = string.Empty;

                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count != 0) {
                    descripcion = Convert.ToString(resultado[0]["descripcion"]);
                    cuentamayor = Convert.ToString(resultado[0]["cuenta"]);
                }

                object[] tt1 = { ii.cuenta, ii.doccomentario, ii.doctipo, (ii.naturaleza == "D" ? Convert.ToString(ii.importe) : "0"), (ii.naturaleza == "H" ? Convert.ToString(ii.importe) : "0"), cuentamayor, descripcion };


                arreglo[contador] = tt1;

                contador++;


            }


            string imglogo = globales.getImagen(globales.imagenesSispe.logoreportes);


            object[][] parametros = new object[2][];
            object[] header = { "logo","tipo","prefijo","polnumero","fecha","elaboro","descripcion" };
            object[] body = { imglogo,nombretipo,prefijo,numeropoliza,fecha,"CG", descripcionpol };


            parametros[0] = header;
            parametros[1] = body;


            globales.reportes("frmCG_polizasLibrodiario", "poltabla", arreglo,"",false,parametros);
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dtggrid.Rows.Count == 0) {
                globales.MessageBoxExclamation("No hya reporte que mostrar", "Aviso", globales.menuPrincipal);
            }


            int contador = 0;
            object[] obj = new object[this.dtggrid.Rows.Count];

            foreach (DataGridViewRow item in this.dtggrid.Rows) {
                string poliza = Convert.ToString(item.Cells[0].Value);
                string fecha = Convert.ToString(item.Cells[9].Value);
                string descripcion = Convert.ToString(item.Cells[2].Value);
                string debe = Convert.ToString(item.Cells[3].Value);
                string haber = Convert.ToString(item.Cells[4].Value);
                string grupo = Convert.ToString(item.Cells[5].Value);
                string x = Convert.ToString(item.Cells[6].Value);

                object[] tt1 = { poliza, fecha, descripcion, debe, haber, grupo, x };
                obj[contador] = tt1;

                contador++;
            }


            DateTime timpo = new DateTime(Convert.ToInt32(txtanio.Text),tabControl1.SelectedIndex,1);
            timpo = timpo.AddMonths(1);
            timpo = timpo.AddDays(-1);
            string meses = globales.getMeses()[tabControl1.SelectedIndex];

            string imagen = globales.getImagen(globales.imagenesSispe.logoreportes);
            string titulo = $"LIBRO DIARIO DEL 01 al {timpo.Day} de {meses} del {txtanio.Text}";


            object[][] parametros = new object[2][];
            object[] header = { "logo", "titulo" };
            object[] body = { imagen, titulo };

            parametros[0] = header;
            parametros[1] = body;

            globales.reportes("frmCG_librodiario", "poltabla", obj,"",false,parametros);
            




        }

        private void btnCerrarmes_Click(object sender, EventArgs e)
        {
            string[] arreglomes = globales.getMeses();
            DialogResult dialogo = globales.MessageBoxQuestion($"¿Desea cerrar el mes de {arreglomes[tabControl1.SelectedIndex + 1]}?","Aviso",globales.menuPrincipal);
            if (DialogResult.No == dialogo) return;

            string nuevaapertura = string.Empty;
            char[] caracteres = meses.ToCharArray();
            for (int x = 0; x < caracteres.Length; x++) {
                nuevaapertura += (x == tabControl1.SelectedIndex)?'0': caracteres[x];
            }

            meses = nuevaapertura;

            string query = $"update cg_fondos.cierremeses set mes = '{meses}' where anio = 2020";
            globales.consulta(query);


            lblabierto.Visible = false;



            btnCerrarmes.Visible = false;

            globales.MessageBoxSuccess("Cierre de mes correcto","Aviso",globales.menuPrincipal);


        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (lblabierto.Visible == false) {


                globales.MessageBoxExclamation("Mes cerrado, no se puede eliminar del catalogo","Aviso",globales.menuPrincipal);
                return;
            }

            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas eliminar la poliza existente?","Aviso",globales.menuPrincipal);
            if (dialogo == DialogResult.No) return;



            DataGridViewRow item = this.dtggrid.Rows[this.row];

            string numeropoliza = Convert.ToString(item.Cells[7].Value);
            string polmes = tabControl1.SelectedIndex < 10 ? $"0{tabControl1.SelectedIndex}" : $"{tabControl1.SelectedIndex}";
            string tipopol = string.Empty;
            if (comboBox1.SelectedIndex == 0)
            {
                tipopol = "D";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                tipopol = "E";
            }
            else
            {
                tipopol = "I";
            }

            string query = $"delete from cg_fondos.des where polnumero = '{numeropoliza}' and polmes = '{polmes}' and anio = {txtanio.Text} and poltipo = '{tipopol}'";
            if (globales.consulta(query,true)) {

                query = $"delete from cg_fondos.pol where polnumero = '{numeropoliza}' and polmes = '{polmes}' and anio = {txtanio.Text} and poltipo = '{tipopol}'";
                this.dtggrid.Rows.RemoveAt(this.row);
                if (globales.consulta(query,true)) {
                    globales.MessageBoxSuccess("La poliza fue eliminada correctamente","Aviso",globales.menuPrincipal);
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void txtanio_Leave(object sender, EventArgs e)
        {
            string query = $"select * from cg_fondos.cierremeses where anio = {txtanio.Text}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count != 0)
            {
                meses = Convert.ToString(resultado[0]["mes"]);
            }
            tabControl1_Click(null,null);
        }
    }
}

