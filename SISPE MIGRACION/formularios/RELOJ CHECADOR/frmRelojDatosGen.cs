using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.RELOJ_CHECADOR
{
    public partial class frmRelojDatosGen : Form
    {
        int contador = 0;
        private List<Dictionary<string, object>> resultado;
        public frmRelojDatosGen()
        {
            InitializeComponent();
        }

        private void frmRelojDatosGen_Shown(object sender, EventArgs e)
        {
            string QUERY = $"select * from  reloj.empleados ";
            this.resultado = globales.consulta(QUERY);
            int longitud = resultado.Count();

            txtxNoEmpleado.Text = Convert.ToString(resultado[0]["id"]);
            txtNombre.Text = Convert.ToString(resultado[0]["nombre"]);
            txtRFc.Text = Convert.ToString(resultado[0]["rfc"]);
            txtModalidad.Text = Convert.ToString(resultado[0]["modalidad"]);
            txtDepato.Text = Convert.ToString(resultado[0]["adscripcion"]);
            txtHorario.Text = Convert.ToString(resultado[0]["idhorario"]);
        }

        private void limpiaForm()
        {
            txtxNoEmpleado.Text = "";
            txtNombre.Text = "";
            txtRFc.Text= "";
            txtDepato.Text = "";
            txtModalidad.Text = "";
            txtHorario.Text = "";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int max = this.resultado.Count();
        if (contador==max)
            {
                return;
            }        
                txtxNoEmpleado.Text = Convert.ToString(resultado[contador]["id"]);
                txtNombre.Text = Convert.ToString(resultado[contador]["nombre"]);
                txtRFc.Text = Convert.ToString(resultado[contador]["rfc"]);
                txtModalidad.Text = Convert.ToString(resultado[contador]["modalidad"]);
                txtDepato.Text = Convert.ToString(resultado[contador]["adscripcion"]);
                txtHorario.Text = Convert.ToString(resultado[contador]["idhorario"]);
                contador++;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int max = this.resultado.Count();
            if (contador == max)
            {
                return;
            }
            txtxNoEmpleado.Text = Convert.ToString(resultado[contador]["id"]);
            txtNombre.Text = Convert.ToString(resultado[contador]["nombre"]);
            txtRFc.Text = Convert.ToString(resultado[contador]["rfc"]);
            txtModalidad.Text = Convert.ToString(resultado[contador]["modalidad"]);
            txtDepato.Text = Convert.ToString(resultado[contador]["adscripcion"]);
            txtHorario.Text = Convert.ToString(resultado[contador]["idhorario"]);
            contador--;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtxNoEmpleado.Text)) return;
            string valida = $"select id from reloj.empleados where id={txtxNoEmpleado.Text}";
            List<Dictionary<string, object>> resulta = globales.consulta(valida);



            if (string.IsNullOrWhiteSpace(txtHorario.Text)) {
                globales.MessageBoxExclamation("Debe insertar un horario", "Aviso", this);
                return;
            }

            if (resulta.Count != 0)
            {

                string catualiz = $"update reloj.empleados set rfc ='{txtRFc.Text}', nombre='{txtNombre.Text}',adscripcion='{txtDepato.Text}', idhorario = {txtHorario.Text} where id={txtxNoEmpleado.Text} ";
                globales.consulta(catualiz);
                DialogResult dialogo = globales.MessageBoxSuccess("SE REALIZO LA OPERACIÓN CON ÉXITO", "CORRECTO", globales.menuPrincipal);

            }
            else {
                string insertsa = $"INSERT into reloj.empleados (id , nombre , rfc , adscripcion , modalidad , fecha , status,idhorario) values ({txtxNoEmpleado.Text},'{txtNombre.Text}','{txtRFc.Text}','{txtDepato.Text}','{txtModalidad.Text}',now(),true,{txtHorario.Text});";
                try
                {
                    globales.consulta(insertsa);

                    DialogResult dialogo = globales.MessageBoxSuccess(" SE INSERTO CORRECTAMENTE EL REGISTRO A LA BASE DE EMPLEADOS", "TERMINADO", globales.menuPrincipal);
                }
                catch
                {
                    DialogResult DIALOGO = globales.MessageBoxError("OCURRIO UN ERROR CONTACTE A SISTEMAS", "UPSS", globales.menuPrincipal);
                    return;
                }
            }

            }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void btnFolio_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtxNoEmpleado.Text)) {
                globales.MessageBoxExclamation("Debe insertar un numero de empleado","Aviso",this);
                return;
            }


            string query = $"select * from reloj.empleados where id::numeric = {txtxNoEmpleado.Text} order by id desc";
            List<Dictionary<string, object>> resultaod = globales.consulta(query);
            if (resultaod.Count != 0)
            {
                txtxNoEmpleado.Text = Convert.ToString(resultaod[0]["id"]);
                txtNombre.Text = Convert.ToString(resultaod[0]["nombre"]);
                txtRFc.Text = Convert.ToString(resultaod[0]["rfc"]);
                txtModalidad.Text = Convert.ToString(resultaod[0]["modalidad"]);
                txtDepato.Text = Convert.ToString(resultaod[0]["adscripcion"]);
                txtHorario.Text = Convert.ToString(resultaod[0]["idhorario"]);
            }
            else {
                globales.MessageBoxExclamation("No hay registro del empleado a buscar", "Aviso", this);
            }
        }
    }
    }

