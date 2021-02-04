using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.CATALOGOS
{
    public partial class frmPerded : Form
    {
        public bool bandera;
        public int inicio;
        public frmPerded()
        {
            InitializeComponent();
        }

        private void frmPerDed_Load(object sender, EventArgs e)
        {
            inicia();
        }


        private void inicia()
        {
            try {
                txtClaves.Enabled = true;
                this.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = "");
                string query = "SELECT * FROM nominas_catalogos.perded order by clave asc";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                txtClaves.Text = Convert.ToString(resultado[0]["clave"]);
                txtDescri.Text = Convert.ToString(resultado[0]["descri"]);
                txtClavePART.Text = Convert.ToString(resultado[0]["part_cve"]);
                txtPartida.Text = Convert.ToString(resultado[0]["partida"]);
                this.inicio = Convert.ToInt32(resultado[0]["id"]);
                btnFolio.Enabled = true;

                if (Convert.ToBoolean(resultado[0]["prestamo"]) == true)
                {
                    RbPrestamo.Checked = true;
                }
                txtSat.Text = Convert.ToString(resultado[0]["sat"]);
                if (Convert.ToString(resultado[0]["tipo"]) == "1")
                    RbPer.Checked = true;
                else
                    RbDed.Checked = true;
            }
            catch
            {

            }

          //  bloquea();
            btnFolio.Enabled = true;
                

        }



        private void bloquea()
        {
            this.Controls.OfType<TextBox>().ToList().ForEach(o => o.Enabled = false);
            txtClaves.Enabled = true;
            RbPrestamo.Enabled = false;
            RbPer.Enabled = false;
            RbDed.Enabled = false;
            btnSave.Enabled = false;
        }
        
        private void habilita()
        {
            txtClavePART.Enabled = true;
            txtPartida.Enabled = true;
            txtSat.Enabled = true;
            btnnext.Visible = true;
            btnback.Visible = true;
            btnFolio.Visible = true;
            btnSave.Enabled = true;
            RbPrestamo.Enabled = false;
            RbPer.Enabled = true;
            RbDed.Enabled = true;
            btnSave.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "";
        }

        private void btnFolio_Click_1(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas modificar un registro?", "Aviso", globales.menuPrincipal);
            if (p == DialogResult.No) { return; this.Controls.OfType<TextBox>().ToList().ForEach(o => o.Enabled = false); }
            this.bandera = true;
            this.Controls.OfType<TextBox>().ToList().ForEach(o => o.Enabled = true);
            RbPrestamo.Enabled = true;
            groupBox1.Enabled = true;
            RbPer.Enabled = true;
            RbDed.Enabled = true;
            btnSave.Enabled = true;
            txtClaves.Focus();
            btnFolio.Visible = false;
             

        }

        private void txtClaves_KeyDown(object sender, KeyEventArgs e)
        {
          
                if (e.KeyCode == Keys.Enter)
                {
                    if (!string.IsNullOrWhiteSpace(txtClaves.Text))
                    {
                        string query = $"SELECT * FROM nominas_catalogos.perded where clave='{txtClaves.Text}'";
                        List<Dictionary<string, object>> resultado = globales.consulta(query);
                        if (resultado.Count <= 0)
                        {
                            DialogResult dialogo2 = globales.MessageBoxError("NO EXISTE LA CLAVE INGRESADA", "VERIFICAR", globales.menuPrincipal);
                            inicia();
                            return;
                        }
                    //    habilita();
                     //   btnnext.Visible = false;
                      //  btnback.Visible = false;
                        txtClaves.Text = Convert.ToString(resultado[0]["clave"]);
                        txtDescri.Text = Convert.ToString(resultado[0]["descri"]);
                        txtClavePART.Text = Convert.ToString(resultado[0]["part_cve"]);
                        txtPartida.Text = Convert.ToString(resultado[0]["partida"]);
                        if (Convert.ToBoolean(resultado[0]["prestamo"]) == true)
                        {
                            RbPrestamo.Checked = true;
                        }
                        txtSat.Text = Convert.ToString(resultado[0]["sat"]);
                        if (Convert.ToString(resultado[0]["tipo"]) == "1")
                            RbPer.Checked = true;
                        else
                            RbDed.Checked = true;
                    }
                }
            }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (bandera==true) // actua
            {
                string query1 = $"select * from nominas_catalogos.perded where clave='{txtClaves.Text}'";
             List<Dictionary<string, object>> resultado = globales.consulta(query1);
                if (resultado.Count <= 0)
                {
                    DialogResult dialogo4 = globales.MessageBoxExclamation("LA CLAVE INGRESADA YA EXISTE, MODIQUE LA EXISTENTE", "VERIFICAR", globales.menuPrincipal);
                    return;
                }
                string prestamo = "";
                string tipo_prest = "";
                if (RbPrestamo.Checked)
                {
                     prestamo = "t";
                }
                if (RbPer.Checked)
                {
                    tipo_prest = "1";

                }
                else if (RbDed.Checked)
                {
                    tipo_prest = "2";
                }

                if (RbPrestamo.Checked==false)
                {
                    prestamo = "f";
                }

                string query = $"update nominas_catalogos.perded set descri='{txtDescri.Text}' , part_cve='{txtClavePART.Text}',prestamo='{prestamo}',sat='{txtSat.Text}',tipo='{tipo_prest}' where clave={txtClaves.Text};";
                try {
                    globales.consulta(query);
                    DialogResult dialog = globales.MessageBoxSuccess("CLAVE MODIFICADA EXITOSAMENTE", "TERMINADO", globales.menuPrincipal);

                    //inicia();
                }
                catch
                {
                    DialogResult dialogo3 = globales.MessageBoxError("EXISTE UN ERROR EN LA CAPTURA", "CONTACTA SISTEMAS", globales.menuPrincipal);
                    return;
                }
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtClaves.Text)&& !string.IsNullOrWhiteSpace(txtDescri.Text))
            {
                string query1 = $"select * from nominas_catalogos.perded where clave='{txtClaves.Text}'";
                List<Dictionary<string, object>> resultado = globales.consulta(query1);
                if (resultado.Count>=0)
                {
                    DialogResult dialogo4 = globales.MessageBoxExclamation("LA CLAVE INGRESADA YA EXISTE, MODIQUE LA EXISTENTE", "VERIFICAR", globales.menuPrincipal);
                    inicia();
                    return;
                }

                if (bandera == false) // nuev
                {
                    string prestamo = "";
                    string tipo_prest = "";
                    if (RbPrestamo.Checked)
                    {
                        prestamo = "t";
                    }
                    if (RbPer.Checked)
                    {
                        tipo_prest = "1";

                    }
                    else if (RbDed.Checked)
                    {
                        tipo_prest = "2";
                    }
                    string query = $"insert into nominas_catalogos.perded  (clave,descri,part_cve,partida,prestamo,sat,tipo) VALUES ('{txtClaves.Text}','{txtDescri.Text}','{txtClavePART.Text}',{txtPartida.Text},'{prestamo}','{txtSat.Text}','{tipo_prest}')";
                    try
                    {
                        globales.consulta(query);
                        DialogResult dialogo = globales.MessageBoxSuccess("NUEVA CLAVE INGRESADA", "PROCESO CORRECTO", globales.menuPrincipal);
                    //    inicia();
                    }
                    catch
                    {
                        DialogResult dialogo3 = globales.MessageBoxError("EXISTE UN ERROR EN LA CAPTURA", "CONTACTA SISTEMAS", globales.menuPrincipal);
                    //    inicia();
                        return;
                    }

                }
            }
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            // this.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = "");
            this.inicio++;
            string query = $"SELECT * FROM nominas_catalogos.perded where id='{this.inicio}'";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            try {
                txtClaves.Text = Convert.ToString(resultado[0]["clave"]);
                txtDescri.Text = Convert.ToString(resultado[0]["descri"]);
                txtClavePART.Text = Convert.ToString(resultado[0]["part_cve"]);
                txtPartida.Text = Convert.ToString(resultado[0]["partida"]);
                if (Convert.ToBoolean(resultado[0]["prestamo"]) == true)
                {
                    RbPrestamo.Checked = true;
                }
                txtSat.Text = Convert.ToString(resultado[0]["sat"]);
                if (Convert.ToString(resultado[0]["tipo"]) == "1")
                    RbPer.Checked = true;
                else
                    RbDed.Checked = true;

            }
            catch
            {

            }
            }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = "");
            this.inicio--;
            string query = $"SELECT * FROM nominas_catalogos.perded where id='{this.inicio}'";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            try
            {
                txtClaves.Text = Convert.ToString(resultado[0]["clave"]);
                txtDescri.Text = Convert.ToString(resultado[0]["descri"]);
                txtClavePART.Text = Convert.ToString(resultado[0]["part_cve"]);
                txtPartida.Text = Convert.ToString(resultado[0]["partida"]);
                if (Convert.ToBoolean(resultado[0]["prestamo"]) == true)
                {
                    RbPrestamo.Checked = true;
                }
                txtSat.Text = Convert.ToString(resultado[0]["sat"]);
                if (Convert.ToString(resultado[0]["tipo"]) == "1")
                    RbPer.Checked = true;
                else
                    RbDed.Checked = true;

            }
            catch
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = "");
            this.Controls.OfType<TextBox>().ToList().ForEach(o => o.Enabled = true);
            RbPrestamo.Enabled = true;
            groupBox1.Enabled = true;
            btnback.Visible = false;
            btnnext.Visible = false;
            btnFolio.Visible = false;
            this.bandera = false;
        }

        private void frmPerded_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F5)
            {
                btnSave_Click(null, null);
            }

            if (e.KeyData == (Keys.Control | Keys.Left))

            {
                btnback_Click(null, null);
            }

            if (e.KeyData == (Keys.Control | Keys.Right))
            {
                btnnext_Click(null, null);
            }

            if (e.KeyCode==Keys.Delete)
            {
                string quey = $"delete from nominas_catalogos.perded where clave={txtClaves.Text}";
                DialogResult diam = globales.MessageBoxQuestion("¿Desea eliminar la clave?", "Aviso", globales.menuPrincipal);
                if (diam==DialogResult.Yes)
                {
                    globales.consulta(quey);

                }
            }
        }

        private void frmPerded_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
    }
    }

