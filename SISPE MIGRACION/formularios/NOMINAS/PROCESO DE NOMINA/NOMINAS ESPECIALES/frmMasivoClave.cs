using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.NOMINAS_ESPECIALES
{
    public partial class frmMasivoClave : Form
    {
        public frmMasivoClave()
        {
            InitializeComponent();
            txtClave.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = $"select * from nominas_catalogos.perded where clave = {txtClave.Text}";
            List<Dictionary<string, object>> res = globales.consulta(query);
            if (res.Count ==0)
            {
                DialogResult dialogo = globales.MessageBoxError("INGRESO UNA CLAVE NO EXISTENTE", "VERIFICAR", globales.menuPrincipal);
                return;
            }
            if (comboJpp.Text == "")
            {
                DialogResult dialogo2 = globales.MessageBoxExclamation("SELECCIONE EL TIPO DE JUB", "VERIFICAR", globales.menuPrincipal);
                return;
            }
            if (checInsert.Checked == false && checupdate.Checked == false && checdelete.Checked == false)
            {
                DialogResult DIALOGO3 = globales.MessageBoxExclamation("SELECCIONE AL MENOS UNA OPCIÓN PARA AFECTAR LAS CLAVES", "VERIFICAR", globales.menuPrincipal);
                return;
            }
             

            string jpp = string.Empty;
            if (comboJpp.Text == "PDO") jpp = "PDO";
            if (comboJpp.Text == "PTA") jpp = "PTA";
            if (comboJpp.Text == "PEA") jpp = "PEA";
            if (comboJpp.Text == "JUB") jpp = "JUB";
            string query4 = string.Empty;

            string tipo_nomina = string.Empty;

            if (checNormal.Checked)
            {
                tipo_nomina = "N";
            }
            else
            {
                if (comboNom.SelectedIndex == 0) tipo_nomina = "AG";
                if (comboNom.SelectedIndex == 1) tipo_nomina = "CA";
                if (comboNom.SelectedIndex == 2) tipo_nomina = "DM";
                if (comboNom.SelectedIndex == 3) tipo_nomina = "UT";
                if (comboNom.SelectedIndex == 4) tipo_nomina = "CAN2";//PTA,PDO
                if (comboNom.SelectedIndex == 5) tipo_nomina = maskedTextBox1.Text;//PTA,PDO

            }


            if (checInsert.Checked)
            {
                DialogResult pregunta = globales.MessageBoxQuestion("¿DESEA INSERTAR LA CLAVE A LA NÓMINA SELECCIONADA?", "", globales.menuPrincipal);
                if (pregunta== DialogResult.Yes )
                {
                    if (txtClave.Text=="82")
                    {
                        string monto = maskedMonto.Text.Replace("$", "");
                        string query5 = $"CREATE TEMP TABLE TABLA AS(SELECT * FROM nominas_catalogos.nominew where clave = 85 and tipo_nomina = '{tipo_nomina}' and jpp = '{jpp}'); "+
                        $"update  tabla set monto ={monto} , clave ={txtClave.Text} , descri ='AYUDA DE DEFUNCION'    ; INSERT into nominas_catalogos.nominew (select * from tabla)";
                        globales.consulta(query5);
                        DialogResult exito = globales.MessageBoxSuccess("PROCESO TERMINADO CORRECTAMENTE", "", globales.menuPrincipal);
                    }

                    if (txtClave.Text == "83")
                    {
                        string monto = maskedMonto.Text.Replace("$", "");
                        string query5 = $"CREATE TEMP TABLE TABLA AS(SELECT * FROM nominas_catalogos.nominew where clave = 85 and tipo_nomina = '{tipo_nomina}' and jpp = '{jpp}'); " +
                        $"update  TABLA set monto ={monto} , clave ={txtClave.Text} , descri ='CUOTA UNIÓN JUB'    ; INSERT into nominas_catalogos.nominew (select *from tabla)";
                        globales.consulta(query5);
                        DialogResult exito = globales.MessageBoxSuccess("PROCESO TERMINADO CORRECTAMENTE", "", globales.menuPrincipal);
                    }

                    if (txtClave.Text == "29")
                    {
                        string va = maskedMonto.Text;
                     double pesos = double.Parse(Convert.ToString(va), NumberStyles.Currency);
                        string query5 = $"CREATE TEMP TABLE TABLA AS(SELECT * FROM nominas_catalogos.nominew where clave = 1 and  secuen=1  and  tipo_nomina = 'N' and jpp = '{jpp}'); " +
                        $"update  TABLA set monto ={pesos} , clave ={txtClave.Text} , descri ='CANASTA NAVIDEÑA' , tipo_nomina= 'CA'   ; INSERT into nominas_catalogos.nominew (select *from tabla)";
                        globales.consulta(query5);
                        DialogResult exito = globales.MessageBoxSuccess("PROCESO TERMINADO CORRECTAMENTE", "", globales.menuPrincipal);
                    }

                }
            }


            if (checupdate.Checked)
            {
                DialogResult dialogo6 = globales.MessageBoxQuestion("¿DESEA ACTUALIZAR LA CLAVE SELECCIONADA?", "", globales.menuPrincipal);
                if (dialogo6 ==DialogResult.Yes)
                {
                    string monto = maskedMonto.Text.Replace("$", "");
                    string query7 = $"update nominas_catalogos.nominew set monto = {monto} where clave ={txtClave.Text} and jpp='{jpp}' and tipo_nomina ='{tipo_nomina}'";
                    try
                    {
                        globales.consulta(query7);
                        DialogResult exito = globales.MessageBoxSuccess("PROCESO TERMINADO CORRECTAMENTE", "", globales.menuPrincipal);

                    }
                    catch
                    {

                    }
                }
            }




            if (checdelete.Checked == true)
            {
                DialogResult dialo = globales.MessageBoxQuestion("DESEA ELIMINAR LA CLAVE?", "ELIMINAR", globales.menuPrincipal);
                if (dialo == DialogResult.No) return;
               
                  string  query8 = $"delete from nominas_catalogos.nominew where clave='{txtClave.Text}' and jpp ='{jpp}' and tipo_nomina='{tipo_nomina}' ";
                globales.consulta(query8);
                DialogResult exito = globales.MessageBoxSuccess("PROCESO TERMINADO CORRECTAMENTE", "", globales.menuPrincipal);

                //Y/////

            }

        }








        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checdelete.Checked)
            {
                checEspecial.Visible = true;
                checNormal.Visible = true;

            }
            else
            {
                if (checInsert.Checked)
                {
                    checEspecial.Visible = true;
                    checNormal.Visible = true;

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checInsert.Checked )
            {
                groupBox2.Visible = true;
                checEspecial.Visible = true;
                checNormal.Visible = true;

            }
            else
            {
                if (checdelete.Checked )
                {
                    groupBox2.Visible = true;
                    checEspecial.Visible = true;
                    checNormal.Visible = true;

                }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checEspecial.Checked)
            {
                comboNom.Visible = true;
            }
            else
            {
                comboNom.Visible = false;
            }
        }

   

        private void checkBox2_Click(object sender, EventArgs e)
        {
            checupdate.Checked = false;
            maskedMonto.Enabled = true;

            checdelete.Checked = false;
        }

        private void checupdate_Click(object sender, EventArgs e)
        {
            checInsert.Checked = false;
            checdelete.Checked = false;
            maskedMonto.Enabled = false;

        }

        private void checdelete_Click(object sender, EventArgs e)
        {
            checInsert.Checked = false;
            checupdate.Checked = false;
            maskedMonto.Enabled = true;

        }

        private void checkBox4_Click(object sender, EventArgs e)
        {
            checEspecial.Checked = false;
            comboNom.Enabled = false;
            maskedMonto.Enabled = true;

        }

        private void checEspecial_Click(object sender, EventArgs e)
        {
            checNormal.Checked = false;
        }

        private void maskedMonto_Leave(object sender, EventArgs e)
        {
            string m = maskedMonto.Text;
            if (m.Contains("$")) return;
            if (string.IsNullOrWhiteSpace(maskedMonto.Text)) return;
            double f =Convert.ToDouble(maskedMonto.Text);
            maskedMonto.Text = string.Format("{0:c}", f);
        }

        private void comboNom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboNom.SelectedIndex== 5)
            {
                maskedTextBox1.Visible = true;
                label4.Visible = true;
            }
        }
    }
}





