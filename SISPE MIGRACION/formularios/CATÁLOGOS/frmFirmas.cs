using SISPE_MIGRACION.codigo.baseDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    public partial class frmFirmas : Form
    {
        private DataGridViewRow elemento;
        string clavedb, nombredb, cargodb, inirespdb, observdb, ordendb;
        public frmFirmas()
        {
            InitializeComponent();
        }

        private void esconde()
        {
            
            lblclave.Visible = false;
            txtclave.Visible = false;
            txtdesc.Visible = false;
            txtinic.Visible = false;
            txtnombre.Visible = false;
            txtobserv.Visible = false;
            lbldesc.Visible = false;
            lblinic.Visible = false;
            lblnomb.Visible = false;
            lblobsev.Visible = false;
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
         
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmFirmas_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void frmFirmas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                button2_Click(null, null);
            }

            if (e.KeyCode==Keys.F5)
            {
                txtclave.Select();
                lblclave.Visible = true;
                txtclave.Visible = true;
                txtdesc.Visible = true;
                txtinic.Visible = true;
                txtnombre.Visible = true;
                txtobserv.Visible = true;
                lbldesc.Visible = true;
                lblinic.Visible = true;
                lblnomb.Visible = true;
                lblobsev.Visible = true;
                
            }
        }

        private void frmFirmas_Load(object sender, EventArgs e)
        {
            carga();
            esconde();
        }

        private void carga()
        {
        //    dbfirmas.Rows.Clear();
            string firmas = "select * from catalogos.firmas ";
            var elemento = baseDatos.consulta(firmas);
            foreach (var item in elemento)
            {
                string clave = Convert.ToString(item["clave"]);
                string nombre = Convert.ToString(item["nombre"]);
                string cargo = Convert.ToString(item["cargo"]);
                string iniresp = Convert.ToString(item["iniresp"]);
                string observ = Convert.ToString(item["observ"]);
                string orden = Convert.ToString(item["orden"]);


                dbfirmas.Rows.Add(clave, nombre, cargo, iniresp, observ,orden);
            }
        }

        private void dbfirmas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void btnmodifica_Click(object sender, EventArgs e)

        {
            string clave = Convert.ToString(elemento.Cells[0].Value);
            string nombre = Convert.ToString(elemento.Cells[1].Value);
            string descripcion = Convert.ToString(elemento.Cells[2].Value);
            string iniciales = Convert.ToString(elemento.Cells[3].Value);
            string observaciones = Convert.ToString(elemento.Cells[4].Value);
            txtclave.Text = clave;
            txtnombre.Text = nombre;
            txtdesc.Text = descripcion;
            txtinic.Text = iniciales;
            txtobserv.Text = observaciones;
          
        }

        private void dbfirmas_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                elemento = dbfirmas.Rows[e.RowIndex];
                txtclave.Enabled = true;
                txtdesc.Enabled = true;
                txtinic.Enabled = true;
                txtobserv.Enabled = true;
                txtnombre.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Error en los datos del dataGrid.. contacte a sistemas para dar solución");
            }
        }

        private void dbfirmas_RowErrorTextChanged(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void dbfirmas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void txtclave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                txtnombre.Select();
            }
        }

        private void txtnombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtdesc.Select();
            }
        }

        private void txtdesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtinic.Select();
            }
        }

        private void txtinic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtobserv.Select();
            }
        }

        private void dbfirmas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Delete)
            {
                string query = "delete from catalogos.firmas where orden='{0}'";
                string pasa = string.Format(query,ordendb) ;
                globales.consulta(pasa);
                DialogResult dialogo5 = globales.MessageBoxSuccess("LA FIRMA SE CORRO CORRECTAMENTE", "", globales.menuPrincipal);
                dbfirmas.Rows.Clear();
                carga();
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas cerrar el modulo?", globales.menuPrincipal);
            if (p == DialogResult.Yes)
                this.Close();
        }

        private void txtobserv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtclave.Text))
                {
                    MessageBox.Show("Esta vacio clave");
                    txtclave.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtnombre.Text))
                {
                    MessageBox.Show("Esta vacio nombre");
                    txtnombre.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtinic.Text))
                {
                    MessageBox.Show("Esta vacio iniciales");
                    txtinic.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtobserv.Text))
                {
                    MessageBox.Show("Esta vacio observaciones");
                    txtobserv.Focus();
                    return;
                }

                string maxim = "SELECT max(orden) as maximo FROM catalogos.firmas";
                List<Dictionary<string, object>> lista = globales.consulta(maxim);
                string maximo = Convert.ToString(lista[0]["maximo"]);

                string firma = string.Format("insert into catalogos.firmas (clave,nombre,cargo,iniresp,observ,orden) values   ('{0}','{1}' ,'{2}','{3}','{4}','{5}')", txtclave.Text, txtnombre.Text, txtdesc.Text, txtinic.Text, txtobserv.Text,maximo);

                if (baseDatos.consulta(firma, true))
                {

                    DialogResult dialogo1 = globales.MessageBoxSuccess("REGISTRO GUARDADO CORRECTAMENTE", "", globales.menuPrincipal);
                    txtclave.Clear();
                    txtdesc.Clear();
                    txtinic.Clear();
                    txtnombre.Clear();
                    txtobserv.Clear();
                    dbfirmas.Rows.Clear();
                    carga();
                    esconde();
                  

                }
                else
                {
                    DialogResult dialogo2 = globales.MessageBoxError("ERROR EN LOS REGISTROS", "", globales.menuPrincipal);
                    txtclave.Clear();
                    txtdesc.Clear();
                    txtinic.Clear();
                    txtnombre.Clear();
                    txtobserv.Clear();
                    carga();
                    esconde();
                }
            }
        }

        private void dbfirmas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = dbfirmas.Rows[c];
            clavedb = Convert.ToString(row.Cells[0].Value);
            nombredb = Convert.ToString(row.Cells[1].Value);
            cargodb = Convert.ToString(row.Cells[2].Value);
            inirespdb = Convert.ToString(row.Cells[3].Value);
            observdb = Convert.ToString(row.Cells[4].Value);
            ordendb = Convert.ToString(row.Cells[5].Value);
            string firma ="update catalogos.firmas set clave='{0}', nombre = '{1}', cargo = '{2}', iniresp = '{3}',observ = '{4}' where orden = '{5}'";
            string pasa = string.Format(firma, clavedb, nombredb, cargodb, inirespdb, observdb, ordendb);
            globales.consulta(pasa,true);
            DialogResult DIALOGO3 = globales.MessageBoxSuccess("SE MODIFICARON LOS REGISTROS CORRECTAMENTE", "", globales.menuPrincipal);
            dbfirmas.Rows.Clear();
            carga();

        }

        private void dbfirmas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = dbfirmas.Rows[c];
            clavedb = Convert.ToString(row.Cells[0].Value);
            nombredb = Convert.ToString(row.Cells[1].Value);
            cargodb = Convert.ToString(row.Cells[2].Value);
            inirespdb = Convert.ToString(row.Cells[3].Value);
            observdb = Convert.ToString(row.Cells[4].Value);
            ordendb = Convert.ToString(row.Cells[5].Value);

        }
    }
}

