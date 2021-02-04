using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.FINANCIEROS.CHEQUES
{
    public partial class frmcuentas : Form
    {
        private string cuenta,descripcion,id;
        private string query;
        internal enviarDatos enviar;
       private List<Dictionary<string, object>> resultado;
        private bool aceptar { get; set; }
        private string cuentagrid = string.Empty;
        private string descrigrid = string.Empty;

        public frmcuentas()
        {
            InitializeComponent();
            this.KeyPreview = true;

            this.KeyPress += new KeyPressEventHandler(frmcuentas_KeyPress);
        }

        private void frmcuentas_Load(object sender, EventArgs e)
        {
            cargagrid();
            txtBusqueda.Select();
            label1.Visible = false;
            label2.Visible = false;
            txtcuenta.Visible = false;
            txtdesc.Visible = false;
        }


        private void cargagrid ()
        {
            string query = "SELECT * FROM financieros.cuentas order by id DESC";
            List<Dictionary<string, object>> resul = globales.consulta(query);

            foreach (var item in resul)
            {
                string cuenta = Convert.ToString(item["cuenta"]);
                string descripcion = Convert.ToString(item["descripcion"]);
                string id = Convert.ToString(item["id"]);

                data.Rows.Add(cuenta, descripcion, id);
            }
        

        }
        private void data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
    
                 
        }

        private void data_KeyDown(object sender, KeyEventArgs e)
        {
            Dictionary<string, object> valor = new Dictionary<string, object>();
            valor.Add("cuenta", "");
            valor.Add("descripcion", "");
            if (e.KeyCode == Keys.Delete)
            {

                DialogResult dialogo = globales.MessageBoxQuestion("¿Desea eliminar la cuenta?", this);
                if (dialogo == DialogResult.Yes)
                {
                    string query = "delete from financieros.cuentas where id='{0}'";
                    string borra = string.Format(query, id);
                    globales.consulta(borra);
                    DialogResult dialogo1 = globales.MessageBoxSuccess("CUENTA ELIMINADA", "", this);
                    this.data.Rows.Clear();
                    cargagrid();
                }
                else
                {
                    MessageBox.Show("Operación cancelada");
                    this.data.Rows.Clear();
                    cargagrid();
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    Close();
                    valor["cuenta"] = this.cuenta;
                    valor["descripcion"] = this.descripcion;
                    enviar(valor, true);
            }
                catch { }

            
            }

            if (e.KeyCode == Keys.F7)
            {
                data.Enabled = false;
                label1.Visible = true;
                label2.Visible = true;
                txtcuenta.Visible = true;
                txtdesc.Visible = true;
                txtcuenta.Select();
              
            }
            if (e.KeyCode == Keys.F9)
            {
             

                DialogResult dialogo = globales.MessageBoxQuestion("¿Desea actualizar la cuenta?", this);
                if (dialogo == DialogResult.Yes)
                {
                    string query = "update financieros.cuentas set cuenta='{0}',descripcion='{1}' where id='{2}'";
                    query = string.Format(query, cuenta, descripcion, id);
                    globales.consulta(query);
                    try
                    {
                        {
                            DialogResult dialogo1 = globales.MessageBoxSuccess("ACTUALIZADO CORRECTAMENTE", "", this);
                            data.Rows.Clear();
                            cargagrid();

                        }
                    }

                    catch
                    {
                        {
                            DialogResult dialogo2 = globales.MessageBoxError("ACTUALIZADO CORRECTAMENTE", "", this);
                            this.data.Rows.Clear();
                            cargagrid();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Operación cancelada");
                    this.data.Rows.Clear();
                    cargagrid();
                }

            }
        }

        private void frmcuentas_KeyPress(object sender, KeyPressEventArgs e)
        {
            char S;

            S = Char.ToUpper(e.KeyChar);

            e.KeyChar = S;
        }

        private void textDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void textBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                data.Focus();
            }
        }

        private void textBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico (e.KeyChar);
            
        }

        private void data_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           
            int c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = data.Rows[c];
            cuenta = Convert.ToString(row.Cells[0].Value);
            descripcion = Convert.ToString(row.Cells[1].Value);
            id = Convert.ToString(row.Cells[2].Value);


        }

        private void data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SendKeys.Send("{UP}");
        SendKeys.Send("{TAB}");
        }

        private void txtcuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                txtdesc.Select();
                txtdesc.Focus();
            }
        }

        private void txtdesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                DialogResult dialogo = globales.MessageBoxQuestion("¿Desea insertar nueva cuenta?", this);
                if (dialogo == DialogResult.Yes)
                {

                    string maxid = "select (max(id)+1) as  id from financieros.cuentas";
                    List<Dictionary<string, object>> lis = globales.consulta(maxid);
                    string maximid = Convert.ToString(lis[0]["id"]);
                    string qry = "insert into financieros.cuentas (cuenta,descripcion,id) values ('{0}','{1}','{2}')";
                    string actualiza = string.Format(qry, txtcuenta.Text, txtdesc.Text, maximid);
                    globales.consulta(actualiza);
                    DialogResult dialogo1 = globales.MessageBoxSuccess("INSERTADO CORRECTAMENTE", "", this);
                    label1.Visible = false;
                    label2.Visible = false;
                    txtcuenta.Visible = false;
                    txtdesc.Visible = false;
                    txtcuenta.Clear();
                    txtdesc.Clear();
                    this.data.Rows.Clear();
                    cargagrid();
                }
                else
                {
                    cargagrid();
                }
                data.Enabled = true;

            }
        }

        private void textBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            string busqueda = txtBusqueda.Text;
            string query = string.Format("select * from financieros.cuentas where cuenta like '{0}%' ;", busqueda);
            resultado = baseDatos.consulta(query);
            data.Rows.Clear();
            resultado.ForEach(o => data.Rows.Add(o["cuenta"], o["descripcion"], o["id"]));
        }

        private void frmcuentas_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.F2)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.F7)
            {
                data.Enabled = false;
                label1.Visible = true;
                label2.Visible = true;
                txtcuenta.Visible = true;
                txtdesc.Visible = true;
                txtcuenta.Select();

            }


        }

           
        }
    }
    

