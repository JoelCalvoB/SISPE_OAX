using SISPE_MIGRACION.codigo.repositorios.financieros;
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

namespace SISPE_MIGRACION.formularios.MIGRACIÓN_CG.CUENTAS
{
    public partial class frmCuentas : Form
    {

        internal enviarDatos enviar;
        int columna;
        int row;    //joel

        bool bandera;
        string valor;

        public frmCuentas(bool poliza = false)
        {
            InitializeComponent();
            txtfolio.Focus();
            this.bandera = poliza;

            if (this.bandera==false)
            {
                this.panel5.Visible = false;


            }
            else
            {

                this.panel5.Visible = true;
                txtfolio.Select();
                dtggrid.ClearSelection();

            }
        }

        private void frmCuentas_Load(object sender, EventArgs e)
        {
            List< cuentas> obj =   new dbaseORM().queryForList<SISPE_MIGRACION.codigo.repositorios.financieros.cuentas>("select * from cg_fondos.cuentas order by cuenta");
            obj.ForEach(o => dtggrid.Rows.Add(o.cuenta,o.descripcion,o.naturaleza,o.nivel,o.tipo,o.id));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string valor = Convert.ToString(dtggrid.Rows[this.row].Cells[5].Value);
            if (string.IsNullOrWhiteSpace(valor)) return;
            DialogResult dialogo = globales.MessageBoxQuestion("¿Desea eliminar esta cuenta?", "", globales.menuPrincipal);
            if (dialogo == DialogResult.Yes)
            {
                string elimina = $"delete from cg_fondos.cuentas where id ={valor}";
                globales.consulta(elimina);
                dtggrid.Rows.Clear();
                frmCuentas_Load(null, null);

            }
        }

        private void dtggrid_Enter(object sender, EventArgs e)
        {
          
        }

        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.columna = e.ColumnIndex;
            this.row = e.RowIndex;

        }

        private void dtggrid_DoubleClick(object sender, EventArgs e)
        {
            if (this.bandera==true)
            {
                return;
            }
         
            if (this.columna == -1) return;

          

            string cuenta = Convert.ToString(dtggrid.Rows[row].Cells[0].Value);
            string descripcion = Convert.ToString(dtggrid.Rows[row].Cells[1].Value);
            string natu = Convert.ToString(dtggrid.Rows[row].Cells[2].Value);

            Dictionary<string, object> valor = new Dictionary<string, object>();


            string cuent = Convert.ToString(dtggrid.Rows[this.row].Cells[0].Value);


            if (this.bandera == false)
            {
                if (cuent.Length < 13)
                {
                    DialogResult dialogon = globales.MessageBoxExclamation("NO PUEDES SELECCIONAR ESTA CUENTA", "VERIFICAR", globales.menuPrincipal);
                    return;
                }
            }


            valor["cuenta"] = cuenta;
            valor["descripcion"] = descripcion;
            valor["naturaleza"] = natu;
            enviar(valor, true);




            this.Close();

        }

        private void txtfolio_TextChanged(object sender, EventArgs e)
        {
            if (this.bandera==false)
            
            {
                string cuenta = txtfolio.Text;

                string completa = string.Empty;
                if (cuenta.Length==1)
                {
                    completa = cuenta + ".";
                }
                if (cuenta.Length==2)
                {
                    completa = cuenta.Substring(0, 1) + "." + cuenta.Substring(1, 1) +".";
                }
                if (cuenta.Length == 3)
                {
                    completa = cuenta.Substring(0, 1) + "." + cuenta.Substring(1, 1) + "." + cuenta.Substring(2,1) + ".";
                }
                if (cuenta.Length == 4)
                {
                    completa = cuenta.Substring(0, 1) + "." + cuenta.Substring(1, 1) + "." + cuenta.Substring(2, 1) + "." + cuenta.Substring(3,1) + ".";

                }
                if (cuenta.Length == 5)
                {
                    completa = cuenta.Substring(0, 1) + "." + cuenta.Substring(1, 1) + "." + cuenta.Substring(2, 1) + "." + cuenta.Substring(3, 1) + "." + cuenta.Substring(4,1) + ".";

                }

                if (cuenta.Length==6)
                {
                    completa = cuenta.Substring(0, 1) + "." + cuenta.Substring(1, 1) + "." + cuenta.Substring(2, 1) + "." + cuenta.Substring(3, 1) + "." + cuenta.Substring(4, 1) + "." + cuenta.Substring(5,1);

                }
                if (cuenta.Length == 7)
                {
                    completa = cuenta.Substring(0, 1) + "." + cuenta.Substring(1, 1) + "." + cuenta.Substring(2, 1) + "." + cuenta.Substring(3, 1) + "." + cuenta.Substring(4, 1) + "." + cuenta.Substring(5, 2);

                }
                if (cuenta.Length == 8)
                {
                    completa = cuenta.Substring(0, 1) + "." + cuenta.Substring(1, 1) + "." + cuenta.Substring(2, 1) + "." + cuenta.Substring(3, 1) + "." + cuenta.Substring(4, 1) + "." + cuenta.Substring(5, 3);

                }
                if (cuenta.Length == 9)
                {
                    completa = cuenta.Substring(0, 1) + "." + cuenta.Substring(1, 1) + "." + cuenta.Substring(2, 1) + "." + cuenta.Substring(3, 1) + "." + cuenta.Substring(4, 1) + "." + cuenta.Substring(5, 3) +"." +cuenta.Substring(8,1);

                }
                if (cuenta.Length == 10)
                {
                    completa = cuenta.Substring(0, 1) + "." + cuenta.Substring(1, 1) + "." + cuenta.Substring(2, 1) + "." + cuenta.Substring(3, 1) + "." + cuenta.Substring(4, 1) + "." + cuenta.Substring(5, 3) + "." + cuenta.Substring(8, 2);

                }
                if (cuenta.Length == 111)
                {
                    completa = cuenta.Substring(0, 1) + "." + cuenta.Substring(1, 1) + "." + cuenta.Substring(2, 1) + "." + cuenta.Substring(3, 1) + "." + cuenta.Substring(4, 1) + "." + cuenta.Substring(5, 3) + "." + cuenta.Substring(8, 3);

                }
                if (cuenta.Length == 12)
                {
                    completa = cuenta.Substring(0, 1) + "." + cuenta.Substring(1, 1) + "." + cuenta.Substring(2, 1) + "." + cuenta.Substring(3, 1) + "." + cuenta.Substring(4, 1) + "." + cuenta.Substring(5, 3) + "." + cuenta.Substring(8, 4);

                }


                dtggrid.Rows.Clear();
                List<cuentas> obj = new dbaseORM().queryForList<SISPE_MIGRACION.codigo.repositorios.financieros.cuentas>($"select * from cg_fondos.cuentas where (cuenta like '{completa}%' or   descripcion like '%{txtfolio.Text}%') 		AND CHAR_LENGTH (cuenta) >= 10 order by cuenta limit 30");
                obj.ForEach(o => dtggrid.Rows.Add(o.cuenta, o.descripcion, o.naturaleza, o.nivel, o.tipo, o.id));
            }
            else
            {
                dtggrid.Rows.Clear();
                List<cuentas> obj = new dbaseORM().queryForList<SISPE_MIGRACION.codigo.repositorios.financieros.cuentas>($"select * from cg_fondos.cuentas where (cuenta like '{txtfolio.Text}%' or   descripcion like '%{txtfolio.Text}%') 		AND CHAR_LENGTH (cuenta) >= 10 order by cuenta limit 30");
                obj.ForEach(o => dtggrid.Rows.Add(o.cuenta, o.descripcion, o.naturaleza, o.nivel, o.tipo, o.id));
            }

         
        }

        private void txtfolio_KeyUp(object sender, KeyEventArgs e)
       {
        

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxInformation("RELLENE LOS CAMPOS NECESARIOS Y POSTERIORMENTE PRESIONE LA TECLA INSERT", "INSTRUCCIÓN", globales.menuPrincipal);
            txtCuenta.Focus();
            txtCuenta.Select();
            txtCuenta.Enabled = true;
            txtDesc.Enabled = true;
            txtCuenta.Clear();
            txtDesc.Clear();
            txtNaturaleza.SelectedItem = null;
            txtEstruc.SelectedItem = null;
            ComboPosición.SelectedItem = null;
            comboEstado.SelectedItem = null;
          
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
           

            string cuentaPunto = txtMayor.Text;
            if (cuentaPunto.Length < 3) return;
            int c ;
            int largo = cuentaPunto.Length;
            string cadena = string.Empty;
            string pos = string.Empty;
            for (c=0; c<=2; c++)
            {
                if (c== 0 || c==2 )
                {
                    pos = ".";
                }
                if (c==1)
                {
                    pos = pos + cuentaPunto.Substring(0, 3);

                }
             
            }



            dtggrid.Rows.Clear();
            List<cuentas> obj = new dbaseORM().queryForList<SISPE_MIGRACION.codigo.repositorios.financieros.cuentas>($"SELECT	* FROM cg_fondos.cuentas WHERE (cuenta LIKE '{txtfolio.Text}%' OR descripcion LIKE '%{txtfolio.Text}%') AND CHAR_LENGTH (cuenta) >= 10 AND cuenta SIMILAR to '%{pos}%' ORDER BY	cuenta LIMIT 30");
            obj.ForEach(o => dtggrid.Rows.Add(o.cuenta, o.descripcion, o.naturaleza, o.nivel, o.tipo, o.id));
        }

        private void txtMayor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
   if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void dtggrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
       


        }

        private void dtggrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Delete)
            {
                string valor = Convert.ToString(dtggrid.Rows[this.row].Cells[5].Value);
                if (string.IsNullOrWhiteSpace(valor)) return;
                DialogResult dialogo = globales.MessageBoxQuestion("¿Desea eliminar esta cuenta?", "", globales.menuPrincipal);
                if (dialogo == DialogResult.Yes)
                {
                    string elimina = $"delete from cg_fondos.cuentas where id ={valor}";
                    globales.consulta(elimina);

                    txtCuenta.Clear();
                    txtDesc.Clear();
                    txtNaturaleza.SelectedItem = null;
                    txtEstruc.SelectedItem = null;
                    ComboPosición.SelectedItem = null;
                    comboEstado.SelectedItem = null;
                    txtCuenta.Enabled = false;
                    txtDesc.Enabled = false;

                    dtggrid.Rows.Clear();
                    frmCuentas_Load(null, null);

                }

            }

         
        }

        private void dtggrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             valor = Convert.ToString(dtggrid.Rows[this.row].Cells[5].Value);
           string cuent = Convert.ToString(dtggrid.Rows[this.row].Cells[0].Value);


            if (this.bandera==false)
            {
                if (cuent.Length < 13)
                {
                   // DialogResult dialogon = globales.MessageBoxExclamation("NO PUEDES SELECCIONAR ESTA CUENTA", "VERIFICAR", globales.menuPrincipal);
                    return;
                }
            }

            string busca = $"select * from cg_fondos.cuentas where id ={valor}";
            List<Dictionary<string, object>> resultado = globales.consulta(busca);
            foreach(var item in resultado)
            {
                txtCuenta.Text = Convert.ToString(item["cuenta"]);
                txtDesc.Text = Convert.ToString(item["descripcion"]);
                string naturaleza = Convert.ToString(item["naturaleza"]);
                if (!string.IsNullOrWhiteSpace(naturaleza))
                {
                    if (naturaleza == "H")
                    {
                        txtNaturaleza.SelectedIndex = 0;
                    }
                    if (naturaleza == "D")
                    {
                        txtNaturaleza.SelectedIndex = 1;
                    }
                }
                string estructura = Convert.ToString(item["estructura"]);
                if (!string.IsNullOrWhiteSpace(estructura))
                {
                    if (estructura == "1")
                    {
                        txtEstruc.SelectedIndex = 0;
                    }
                    if (estructura =="2")
                    {
                        txtEstruc.SelectedIndex = 1;
                    }
                    if (estructura == "3")
                    {
                        txtEstruc.SelectedIndex = 2;
                    }
                }

                string posicion = Convert.ToString(item["posicion"]);
                if (!string.IsNullOrWhiteSpace(posicion))
                {
                    if (posicion=="A")
                    {
                        ComboPosición.SelectedIndex = 0;
                    }
                    if (posicion=="P")
                    {
                        ComboPosición.SelectedIndex = 1;
                    }
                    if (posicion == "H")
                    {
                        ComboPosición.SelectedIndex = 2;
                    }
                    if (posicion == "O")
                    {
                        ComboPosición.SelectedIndex = 3;
                    }

                }
                string estado = Convert.ToString(item["estado"]);
                if (!string.IsNullOrWhiteSpace(estado))
                {
                    if(estado=="B")
                    {
                        comboEstado.SelectedIndex = 0;
                    }

                    if (estado == "R")
                    {
                        comboEstado.SelectedIndex = 1;
                    }

                }


            }



        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtCuenta.Text)) return;
            string naturaleza = string.Empty;
            if (txtNaturaleza.SelectedIndex == 0)
            {
                naturaleza = "H";
            }
            if (txtNaturaleza.SelectedIndex == 1)
            {
                naturaleza = "D";
            }

           

            string estructura = string.Empty;
            if (txtEstruc.SelectedIndex == 0)
            {
                estructura = "1";
            }
            if (txtEstruc.SelectedIndex == 1)
            {
                estructura = "2";
            }
            if (txtEstruc.SelectedIndex == 2)
            {
                estructura = "3";
            }

            if (string.IsNullOrWhiteSpace(estructura))
            {
                estructura = null;
            }

            string posicion = string.Empty;
            if (ComboPosición.SelectedIndex==0)
            {
                posicion = "A";
            }
            if (ComboPosición.SelectedIndex == 1)
            {
                posicion = "P";
            }
            if (ComboPosición.SelectedIndex == 2)
            {
                posicion = "H";
            }
            if (ComboPosición.SelectedIndex == 3)
            {
                posicion = "O";
            }


            string estado = string.Empty;
            if (comboEstado.SelectedIndex  == 0)
            {
                estado = "B";

            }
            if(comboEstado.SelectedIndex==1)
            {
                estado = "R";
            }




            string actualiza = $"update cg_fondos.cuentas set cuenta ='{txtCuenta.Text}' , descripcion ='{txtDesc.Text}' , naturaleza ='{naturaleza}' , estructura = {estructura} , posicion = '{posicion}' , estado = '{estado}' where id = {this.valor}";

            try
            {
                globales.consulta(actualiza);
                DialogResult dialogo = globales.MessageBoxSuccess("SE ACTUALIZO CORRECTAMENTE LA CUENTA ", "AVISO", globales.menuPrincipal);
                txtCuenta.Clear();
                txtDesc.Clear();
                txtNaturaleza.SelectedItem = null;
                txtEstruc.SelectedItem = null;
                ComboPosición.SelectedItem = null;
                comboEstado.SelectedItem = null;
                txtCuenta.Enabled = false;
                txtDesc.Enabled = false;

                dtggrid.Rows.Clear();
                frmCuentas_Load(null, null);


            }
            catch
            {
                DialogResult dialogo = globales.MessageBoxError("OCURRIO UN ERROR , NOTIFIQUE AL ÁREAS DE SISTEMAS ", "AVISO", globales.menuPrincipal);
                txtCuenta.Clear();
                txtDesc.Clear();
                txtNaturaleza.SelectedItem = null;
                txtEstruc.SelectedItem = null;
                ComboPosición.SelectedItem = null;
                comboEstado.SelectedItem = null;
                txtCuenta.Enabled = false;
                txtDesc.Enabled = false;

                dtggrid.Rows.Clear();
                frmCuentas_Load(null, null);


            }

        }

        private void frmCuentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                string posicion = string.Empty;
                posicion = ComboPosición.Text;
                if (string.IsNullOrWhiteSpace(posicion))
                {
                    posicion = "null";
                }



                string naturaleza = string.Empty;
                if (txtNaturaleza.SelectedIndex == 0)
                {
                    naturaleza = "H";
                }
                if (txtNaturaleza.SelectedIndex == 1)
                {
                    naturaleza = "D";
                }



                string estructura = string.Empty;
                if (txtEstruc.SelectedIndex == 0)
                {
                    estructura = "1";
                }
                if (txtEstruc.SelectedIndex == 1)
                {
                    estructura = "2";
                }
                if (txtEstruc.SelectedIndex == 2)
                {
                    estructura = "3";
                }

                if (string.IsNullOrWhiteSpace(estructura))
                {
                    estructura = null;
                }

                if (ComboPosición.SelectedIndex == 0)
                {
                    posicion = "A";
                }
                if (ComboPosición.SelectedIndex == 1)
                {
                    posicion = "P";
                }
                if (ComboPosición.SelectedIndex == 2)
                {
                    posicion = "H";
                }
                if (ComboPosición.SelectedIndex == 3)
                {
                    posicion = "O";
                }


                string estado = string.Empty;
                if (comboEstado.SelectedIndex == 0)
                {
                    estado = "B";

                }
                if (comboEstado.SelectedIndex == 1)
                {
                    estado = "R";
                }


                string query = $"insert into  cg_fondos.cuentas  (cuenta , descripcion , naturaleza , posicion , estado , estructura ) values ('{txtCuenta.Text}' , '{txtDesc.Text}' , '{naturaleza}' ,'{posicion}' , '{estado}' , {estructura} ) ";
                try
                {
                    globales.consulta(query);
                    DialogResult dialogo = globales.MessageBoxSuccess("SE INSERTO CORRECTAMENTE LA CUENTA", "PROCESO TERMINADO", globales.menuPrincipal);
                    txtCuenta.Clear();
                    txtDesc.Clear();
                    txtNaturaleza.SelectedItem =null;
                    txtEstruc.SelectedItem = null;
                    ComboPosición.SelectedItem = null;
                    comboEstado.SelectedItem = null;
                    txtCuenta.Enabled = false;
                    txtDesc.Enabled = false;

                    dtggrid.Rows.Clear();
                    frmCuentas_Load(null, null);

                }
                catch
                {
                    DialogResult dialogo1 = globales.MessageBoxError("OCURRIO UN ERROR , NOTIFIQUE A SISTEMAS", "UPS", globales.menuPrincipal);
                    txtCuenta.Clear();
                    txtDesc.Clear();
                    txtNaturaleza.SelectedItem = null;
                    txtEstruc.SelectedItem = null;
                    ComboPosición.SelectedItem = null;
                    comboEstado.SelectedItem = null;
                    txtCuenta.Enabled = false;
                    txtDesc.Enabled = false;

                    dtggrid.Rows.Clear();
                    frmCuentas_Load(null, null);

                }

            }

            if (Keys.Enter == e.KeyCode)
            {
                SendKeys.Send("{TAB}");//Cuando se presiona la tecla enter, este le manda señal a la tecla TAB para que active el evento de traspaso...
            }



        }
    }
}
