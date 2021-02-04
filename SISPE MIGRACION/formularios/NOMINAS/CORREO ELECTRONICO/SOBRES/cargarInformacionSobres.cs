using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace SISPE_MIGRACION.formularios.sobres
{
    public partial class cargarInformacionSobres : Form
    {
        private DataSet resultado;


        public cargarInformacionSobres()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void realizarOperacion(bool aportacion, string nombre, string ruta)
        {



        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {



           

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void datos1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            globales.MessageBoxInformation("Selecciona el DBF Maestro para enlazar a la nómina","Aviso",globales.menuPrincipal);
            open1.Filter = "dbf files (*.dbf)|*.dbf";
            DialogResult p = open1.ShowDialog();
            if (p == DialogResult.OK)
            {
                string ruta = open1.FileName;
                string[] arreglo = ruta.Split('\\');
                string nombreArchivo = arreglo[arreglo.Length - 1];
                string letra = nombreArchivo.First().ToString().ToUpper();
                if (letra == "M" || letra == "m")
                {
                    bool aportacion = (letra == "M" || letra == "");
                    realizarOperacion(aportacion, nombreArchivo, ruta);

                }
                else
                {
                    globales.MessageBoxError("Archivo seleccionado invalido", "Error archivo", globales.menuPrincipal);
                }
            }
        }

        private void frmsobress_Load(object sender, EventArgs e)
        {

        }

        private void btnsalir_Click_1(object sender, EventArgs e)
        {
            button1_Click_2(null,null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult p = open1.ShowDialog();
            if (p == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    string ruta = open1.FileName;
                    string[] arreglo = ruta.Split('\\');
                    string nombreArchivo = arreglo[arreglo.Length - 1];
                    string letra = nombreArchivo.ToString().ToLower();
                    string nombre = letra.Substring(0, 7);

                    if (nombre == "maestro" || nombre == "nominew")
                    {

                        string texto = letra.Substring(7);
                        int anio = Convert.ToInt32(texto.Substring(0, 2));
                        int mes = Convert.ToInt32(texto.Substring(2, 2));
                        enviarInformacion(texto, anio, mes, (nombre == "maestro")?"MAESTRO":"NOMINEW");
                        txtRuta.Text = ruta;
                        resultado = globales.leerDbf(ruta);
                        datos1.DataSource = resultado.Tables[0]; 
                    }
                    else
                    {
                        globales.MessageBoxError("El archivo debe tener por nombre maestro o nominew seguido del año a dos dígitos y mes a dos dígitos\nEjemplo: nominew1712 o maestro1809.", "Nombre archivo", globales.menuPrincipal);
                    }
                }
                catch
                {
                    globales.MessageBoxError("El archivo debe tener por nombre maestro o nominew seguido del año a dos dígitos y mes a dos dígitos\nEjemplo: nominew1712 o maestro1809.", "Nombre archivo", globales.menuPrincipal);
                }
            }
            this.Cursor = Cursors.Default;

        }



        private void enviarInformacion(string texto, int anio, int mes, string aux)
        {
            txtArchivo.Text = aux;
            txtAnio.Text = anio.ToString();
            txtMes.Text = mes.ToString();
            if (mes >= 1 && mes <= 12)
            {
                if (mes < 10)
                {
                    txtMes.Text = $"0{txtMes.Text}";
                }

            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Desea realizar la operación?","Aviso",globales.menuPrincipal);
            if (p == DialogResult.No) return;

            if (string.IsNullOrWhiteSpace(txtRuta.Text)) {
                globales.MessageBoxExclamation("Se debe elegir el archibo DBF a insertar","Aviso",globales.menuPrincipal);
                return;
            }
            
         
            

                this.Cursor = Cursors.WaitCursor;
                string query = "";
                string archivo = $"{txtArchivo.Text}{txtAnio.Text}{txtMes.Text}";
                if (txtArchivo.Text.ToUpper() == "MAESTRO")
                {

                    query = " truncate table nominas.maestro;";
                    foreach (DataRow item in this.resultado.Tables[0].Rows) {
                        string jpp = Convert.ToString(item["jpp"]).Trim();
                        string clave = Convert.ToString(item["clave"]).Trim();
                        string num = Convert.ToString(item["num"]).Trim();
                        string rfc =  Convert.ToString(item["rfc"]).Trim();
                        string nombre = Convert.ToString(item["nombre"]).Trim();
                        string categ = Convert.ToString(item["categ"]).Trim();
                        string curp = Convert.ToString(item["curp"]).Trim();
                        string proyecto = Convert.ToString(item["proyecto"]).Trim();                        
                        string imss = Convert.ToString(item["imss"]);
                        string nivel = Convert.ToString(item["nivel"]).Trim();
                        string nomelec = Convert.ToString(item["nomelec"]).Trim();
                        string superviven = Convert.ToString(item["superviven"]).Trim();
                        string direSuper = Convert.ToString(item["dire_super"]).Trim();
                        string fsupervive = string.Format("{0:yyyy-MM-dd}", item["fsupervive"]);
                        string f_sobre = "";
                        string correo = Convert.ToString(item["correo"]).Trim();

                        query += string.Format("insert into nominas.maestro values ('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}');",
                            jpp,
                            clave,
                            num,
                            rfc,
                            nombre,
                            categ,
                            curp,
                            proyecto,
                            imss,
                            nivel,
                            nomelec,
                            superviven,
                            direSuper,
                            fsupervive,
                            f_sobre,
                            correo,
                            archivo);

                    }
                }
                else {
                    query = " truncate table nominas.nominew;";
                    foreach (DataRow item in this.resultado.Tables[0].Rows) {
                        string jpp = Convert.ToString(item["jpp"]).Trim();
                        string numjpp =(string.IsNullOrWhiteSpace(Convert.ToString(item["numjpp"])))?"0": Convert.ToString(item["numjpp"]).Trim();
                        string clave = (string.IsNullOrWhiteSpace(Convert.ToString(item["clave"])))?"0":Convert.ToString(item["clave"]).Trim();
                        string secuen = (string.IsNullOrWhiteSpace(Convert.ToString(item["secuen"])))?"0":Convert.ToString(item["secuen"]).Trim();
                        string descri = Convert.ToString(item["descri"]).Trim();
                        string monto = (string.IsNullOrWhiteSpace(Convert.ToString(item["monto"])))?"0":Convert.ToString(item["monto"]).Trim();
                        string leyen = Convert.ToString(item["leyen"]).Trim();
                        string nomeelec = Convert.ToString(item["nomelec"]).Trim();
                        string pago4 = Convert.ToString(item["pago4"]).Trim();
                        string pagot = Convert.ToString(item["pagot"]).Trim();

                        query += string.Format("insert into nominas.nominew values ('{0}',{1},{2},{3},'{4}',{5},'{6}','{7}','{8}','{9}','{10}');",
                            jpp,
                            numjpp,
                            clave,
                            secuen,
                            descri,
                            monto,
                            leyen,
                            nomeelec,
                            pago4,
                            pagot,
                            archivo);
                    }
                }
                if (globales.consulta(query,true)) {
                    globales.MessageBoxSuccess("Registros insertados correctamente","Aviso",globales.menuPrincipal);
                    this.Cursor = Cursors.Default;
                }
            
            
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas cerrar el modulo?", globales.menuPrincipal);
            if (p == DialogResult.Yes)
                this.Close();
        }
    }
}
