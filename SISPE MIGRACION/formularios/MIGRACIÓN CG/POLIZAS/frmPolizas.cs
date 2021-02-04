using SISPE_MIGRACION.formularios.MIGRACIÓN_CG.CUENTAS;
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

namespace SISPE_MIGRACION.formularios.MIGRACIÓN_CG.POLIZAS
{
    public partial class frmPolizas : Form
    {

        int columna;
        int row;
        int contador = 0;
        double sumaDebe = 0;
        double sumaHaber = 0;


        public frmPolizas()
        {
            InitializeComponent();
            DateTime fecha = DateTime.Now;
            maskedTextBox1.Text = Convert.ToString( fecha);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(comboPoliza.Text) || string.IsNullOrWhiteSpace(comboPrefijo.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                DialogResult dialogo2 = globales.MessageBoxError("NO SE PUEDE INGRESAR UN DETALLE HASTA QUE SE CEPTURE EL ENCABEZADO", "VIOLACIÓN DE NORMAS", globales.menuPrincipal);
                return;
            }
            if (e.KeyCode == Keys.Insert)
            {
                dtgGrid.Rows.Insert(contador);
                contador++;

            }

            if (e.KeyCode == Keys.Delete)
            {
                //dtgGrid.Rows.Insert(contador);
                contador--;
                SacaSumas();

            }

            if (e.KeyCode == Keys.F1)
            {
                frmCuentas cuenta = new frmCuentas(false);
                cuenta.enviar = rellenar;
                cuenta.ShowDialog();
            }

            if (e.KeyCode == Keys.F12)
            {
                string fechas = maskedTextBox1.Text;
                string anioS = fechas.Substring(6, 4);
                string meSS = fechas.Substring(3, 2);

                int m = Convert.ToInt32(meSS) +1;

                string verifica = $"SELECT substr( mes , {m},1) as status FROM cg_fondos.cierremeses where anio= {anioS};";
                List<Dictionary<string, object>> resu = globales.consulta(verifica);
                int digito = Convert.ToInt32(resu[0]["status"]);

                if (digito != 0)
                {
                    DialogResult dialogo = globales.MessageBoxError("EL MES QUE DESEA AGREGAR LA POLIZA SE ENCUENTRA CERRADO", "", globales.menuPrincipal);
                }

                if (this.sumaDebe == this.sumaHaber)
                {
                    string prefijo = string.Empty;
                    if (comboPrefijo.SelectedIndex == 0)
                    {
                        prefijo = "B";
                    }
                    if (comboPrefijo.SelectedIndex == 1)
                    {
                        prefijo = "P";
                    }
                    if (comboPrefijo.SelectedIndex == 2)
                    {
                        prefijo = "R";
                    }
                    if (comboPrefijo.SelectedIndex == 3)
                    {
                        prefijo = "X";
                    }

                    //TIPO
                    string tipoPol = string.Empty;

                    if (comboPoliza.SelectedIndex == 0)
                    {
                        tipoPol = "D";
                    }
                    if (comboPoliza.SelectedIndex == 1)
                    {
                        tipoPol = "I";

                    }
                    if (comboPoliza.SelectedIndex == 2)
                    {
                        tipoPol = "E";
                    }
                    string polnumero = string.Empty;


                    string numero = txtNumero.Text;


                    if (numero.Length == 1)
                    {
                        numero = "000000" + numero;
                    }
                    if (numero.Length == 2)
                    {
                        numero = "00000" + numero;
                    }
                    if (numero.Length == 3)
                    {
                        numero = "0000" + numero;
                    }







                    try
                    {

                        string cuenta = string.Empty;
                        string query = $"insert into  cg_fondos.des values ('801', 'P' , '{meSS}' , '{tipoPol}', '{numero}' ,'{maskedTextBox1.Text}' , {this.sumaDebe} , {this.sumaHaber} , '{txtDescripcion.Text}' ,'000' , '{prefijo}' , '{globales.id_usuario}' , {0} ,'{anioS}' ) ";
                        globales.consulta(query, true);

                        int contador = 0;
                        foreach (DataGridViewRow item in dtgGrid.Rows)
                        {
                            if (contador == dtgGrid.Rows.Count) break;

                            string debe = Convert.ToString(item.Cells[3].Value);
                            string haber = Convert.ToString(item.Cells[4].Value);
                            cuenta = Convert.ToString(item.Cells[1].Value);
                            string descripcion = Convert.ToString(item.Cells[2].Value);

                            string doc = Convert.ToString(item.Cells[5].Value);


                            contador++;




                            if (debe == "0.00" || string.IsNullOrWhiteSpace(debe))
                            {
                                string debehaber = "H";
                                string inserta = $"insert into cg_fondos.pol values  ('801' , 'P' , '{meSS}' ,'{tipoPol}' , '{numero}' , '{maskedTextBox1.Text}' , '{cuenta}' , '{debehaber}' , '{double.Parse(haber, System.Globalization.NumberStyles.Currency)}' , '00' , '{prefijo}' ,'CH', '{doc}' , '{maskedTextBox1.Text}' ,'', '{descripcion}' ,'0' , 0 , 0 , '{anioS}' )";

                                globales.consulta(inserta);

                            }
                            else
                            {
                                string debehaber = "D";
                                string inserta = $"insert into cg_fondos.pol values ('801' , 'P' , '{meSS}' ,'{tipoPol}' , '{numero}' , '{maskedTextBox1.Text}' , '{cuenta}' , '{debehaber}' , '{double.Parse(debe, System.Globalization.NumberStyles.Currency)}' , '00' , '{prefijo}' ,'CH', '{doc}' , '{maskedTextBox1.Text}' ,'', '{descripcion}' ,'0' , 0 , 0  , '{anioS}' )";
                                globales.consulta(inserta);

                            }



                        }
                        DialogResult dialogo = globales.MessageBoxSuccess("POLIZA GUARDADA EXITOSAMENTE", "PROCESO TERMINADO", globales.menuPrincipal);




                    }
                    catch
                    {
                        DialogResult dialogo = globales.MessageBoxSuccess("ERROR AL GUARDAR SU PÓLIZA", "PROCESO TERMINADO", globales.menuPrincipal);

                    }


                }
                else
                {
                    DialogResult dialogo = globales.MessageBoxExclamation("LOS IMPORTES DEBE Y HABER NO COINCIDEN", "PROCESO ABORTADO", globales.menuPrincipal);

                }
            }



            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                int iColumn = dtgGrid.CurrentCell.ColumnIndex;
                int iRow = dtgGrid.CurrentCell.RowIndex;
                if (iColumn == dtgGrid.ColumnCount - 1)
                {
                    if (dtgGrid.RowCount > (iRow + 1))
                    {
                        dtgGrid.CurrentCell = dtgGrid[1, iRow + 1];
                    }
                    else
                    {
                        //focus next control
                    }
                }
                else
                    dtgGrid.CurrentCell = dtgGrid[iColumn + 1, iRow];
           
        }

    }


        private void LimpiaFormulario()
        {
            comboPrefijo.SelectedIndex = 0;
            comboPoliza.SelectedIndex = 0;
            maskedTextBox1.Clear();
            txtNumero.Clear();
            dtgGrid.Rows.Clear();
            comboPrefijo.Select();

        }

        private void validaSaldos()
        {
           
        }

        public void rellenar(Dictionary<string, object> obj, bool externo = false)
        {
          
                dtgGrid.Rows[this.row].Cells[1].Value = Convert.ToString(obj["cuenta"]);
            dtgGrid.Rows[this.row].Cells[2].Value = Convert.ToString(obj["descripcion"]);
            dtgGrid.Rows[this.row].Cells[3].Selected = true;
        }

        private void dataGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            this.columna = e.ColumnIndex;
            this.row = e.RowIndex;
            if (this.columna == -1) return;



        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (comboPrefijo.Text == "X- CANCELADA")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;

                DialogResult diaologo = globales.MessageBoxExclamation("PARA CANCELAR CHEQUE , TIENES QUE SELECCIONAR TIPO DE PÓLIZA PARA CANCELAR", "VERIFICAR", globales.menuPrincipal);
                return;
            }
        }

        private void comboPrefijo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboPrefijo.Text != "X- CANCELADA")
            {
                checkBox1.Checked = false;
            }
        }

        private void frmPolizas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");


                }
            char S;

            S = Char.ToUpper(e.KeyChar);

            e.KeyChar = S;
        }

        private void dataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SendKeys.Send("{DOWN}");
            SendKeys.Send("{TAB}");
        }

        private void dataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;

            if (c == -1) return;



            if (e.ColumnIndex == 3)
            {
                try
                {
                    string valor4 = Convert.ToString(dtgGrid.Rows[c].Cells[4].Value);
                    if (!string.IsNullOrWhiteSpace(valor4))
                    {
                        dtgGrid.Rows[c].Cells[3].Value = "0.00";

                        return;
                    }
                        

                    double entrada = 0;
                    entrada = double.Parse(Convert.ToString(dtgGrid.Rows[c].Cells[3].Value), System.Globalization.NumberStyles.Currency);
                    dtgGrid.Rows[c].Cells[3].Value = string.Format("{0:C}", entrada).Replace("$", "");

                }
                catch
                {
                    globales.MessageBoxError("Ingresar la entrada en el formato correcto:\n$0.00", "Error entrada", globales.menuPrincipal);
                    dtgGrid.Rows[c].Cells[3].Value = string.Format("{0:C}", 0).Replace("$", "");
                }
            }


            if (e.ColumnIndex == 4)
            {
                try
                {
                    string valor3 = Convert.ToString(dtgGrid.Rows[c].Cells[3].Value);
                    if (!string.IsNullOrWhiteSpace(valor3))
                    {
                        dtgGrid.Rows[c].Cells[4].Value = "0.00";
                        return;
                    }


                    double entrada = 0;
                    entrada = double.Parse(Convert.ToString(dtgGrid.Rows[c].Cells[4].Value), System.Globalization.NumberStyles.Currency);
                    dtgGrid.Rows[c].Cells[4].Value = string.Format("{0:C}", entrada).Replace("$", "");

                }
                catch
                {
                    globales.MessageBoxError("Ingresar la entrada en el formato correcto:\n$0.00", "Error entrada", globales.menuPrincipal);
                    dtgGrid.Rows[c].Cells[4].Value = string.Format("{0:C}", 0).Replace("$", "");
                }
            }

            SacaSumas();


        }




        private void SacaSumas()
        {
           try
            { 

         sumaDebe = 0;
             sumaHaber = 0;
            foreach (DataGridViewRow row in dtgGrid.Rows)
            {
                double debe = Convert.ToDouble(row.Cells[3].Value);
                double haber = Convert.ToDouble(row.Cells[4].Value);
                sumaDebe = sumaDebe + debe;
                sumaHaber = sumaHaber + haber;

            }

            txtDebe.Text = string.Format("{0:c}", sumaDebe);
            txtHaber.Text = string.Format("{0:c}", sumaHaber);
                }

            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void dtgGrid_KeyUp(object sender, KeyEventArgs e)
        {
          

        }

        private void dtgGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
        
        }

        private void dtgGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {





        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboPoliza.Text) || string.IsNullOrWhiteSpace(comboPrefijo.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                DialogResult dialogo2 = globales.MessageBoxError("NO SE PUEDE INGRESAR UN DETALLE HASTA QUE SE CEPTURE EL ENCABEZADO", "VIOLACIÓN DE NORMAS", globales.menuPrincipal);
                return;
            }

            dtgGrid.Rows.Insert(contador);
            contador++;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(comboPoliza.Text) || string.IsNullOrWhiteSpace(comboPrefijo.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                DialogResult dialogo2 = globales.MessageBoxError("NO SE PUEDE INGRESAR UN DETALLE HASTA QUE SE CEPTURE EL ENCABEZADO", "VIOLACIÓN DE NORMAS", globales.menuPrincipal);
                return;
            }
        //    if (contador == 0) return;
            contador--;
            dtgGrid.Rows.RemoveAt(contador);
        }

        private void dtgGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgGrid.Rows[this.row].Cells[0].Selected== true)
            {
                frmCuentas cuenta = new frmCuentas(false);
                cuenta.enviar = rellenar;
                cuenta.ShowDialog();
            }


            if (dtgGrid.Rows[this.row].Cells[5].Selected == true)
            {
                p1.Visible = true;
                limpiaP1();
                
                string doctipo = Convert.ToString(dtgGrid.Rows[this.row].Cells[5].Value);
                string doccomentario = Convert.ToString(dtgGrid.Rows[this.row].Cells[8].Value);
                string docfecha = Convert.ToString(dtgGrid.Rows[this.row].Cells[7].Value);
                string docnumero = Convert.ToString(dtgGrid.Rows[this.row].Cells[6].Value);
                if (!string.IsNullOrWhiteSpace(doctipo))
                {

                    switch (doctipo)
                    {
                        case "CLC":
                            auxtipo.SelectedIndex=0;
                            break;
                        case "CH":
                            auxtipo.SelectedIndex = 1;
                            break;
                        case "EO":
                            auxtipo.SelectedIndex = 2;
                            break;
                        case "FACT":
                            auxtipo.SelectedIndex = 3;
                            break;
                        case "FD":
                             auxtipo.SelectedIndex = 4;
                            break;
                        case "REM":
                            auxtipo.SelectedIndex = 5;
                            break;
                        case "NOTA":
                            auxtipo.SelectedIndex = 6;
                            break;
                        case "OTB":
                            auxtipo.SelectedIndex = 7;
                            break;
                        case "OTRO":
                            auxtipo.SelectedIndex = 8;
                            break;
                        case "RBO":
                            auxtipo.SelectedIndex = 9;
                            break;
                        case "SPEI":
                            auxtipo.SelectedIndex = 10;
                            break;

                    }

                    auxgrupo.Text = "NORMAL" ;
                    auxcomentario.Text = doccomentario;
                    auxfecha.Text = docfecha;
                    auxnumero.Text = docnumero;
                }

            }


        }


        public void limpiaP1()
        {
            auxtipo.SelectedItem = null;
            auxgrupo.SelectedItem = null;
            auxcomentario.Text = "";
            auxfecha.Clear();
            auxnumero.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string fechas = maskedTextBox1.Text;
            string anioS = fechas.Substring(6, 4);
            string meSS = fechas.Substring(3, 2);

            int m = Convert.ToInt32(meSS)+1;

            string verifica = $"SELECT substr( mes , {m},1) as status FROM cg_fondos.cierremeses where anio= {anioS};";
            List<Dictionary<string, object>> resu = globales.consulta(verifica);
            int digito = Convert.ToInt32(resu[0]["status"]);

            if (digito!=0)
            {
                DialogResult dialogo = globales.MessageBoxError("EL MES QUE DESEA AGREGAR LA POLIZA SE ENCUENTRA CERRADO", "", globales.menuPrincipal);
                return ;
            }

            if (this.sumaDebe == this.sumaHaber)
            {
                string prefijo = string.Empty;
                if (comboPrefijo.SelectedIndex == 0)
                {
                    prefijo = "B";
                }
                if (comboPrefijo.SelectedIndex == 1)
                {
                    prefijo = "P";
                }
                if (comboPrefijo.SelectedIndex == 2)
                {
                    prefijo = "R";
                }
                if (comboPrefijo.SelectedIndex == 3)
                {
                    prefijo = "X";
                }

                //TIPO
                string tipoPol = string.Empty;

                if (comboPoliza.SelectedIndex == 0)
                {
                    tipoPol = "D";
                }
                if (comboPoliza.SelectedIndex == 1)
                {
                    tipoPol = "I";

                }
                if (comboPoliza.SelectedIndex == 2)
                {
                    tipoPol = "E";
                }
                string polnumero = string.Empty;


                string numero = txtNumero.Text;


                if (numero.Length == 1)
                {
                    numero = "000000" + numero;
                }
                if (numero.Length == 2)
                {
                    numero = "00000" + numero;
                }
                if (numero.Length == 3)
                {
                    numero = "0000" + numero;
                }



               

           

                try
                {

                    string cuenta = string.Empty;
                    string query = $"insert into  cg_fondos.des values ('801', 'P' , '{meSS}' , '{tipoPol}', '{numero}' ,'{maskedTextBox1.Text}' , {this.sumaDebe} , {this.sumaHaber} , '{txtDescripcion.Text}' ,'000' , '{prefijo}' , '{globales.id_usuario}' , {0} ,'{anioS}' ) ";
                    globales.consulta(query, true);

                    int contador = 0;
                    foreach (DataGridViewRow item in dtgGrid.Rows)
                    {
                        if (contador == dtgGrid.Rows.Count) break;

                        string debe = Convert.ToString(item.Cells[3].Value);
                        string haber = Convert.ToString(item.Cells[4].Value);
                        cuenta = Convert.ToString(item.Cells[1].Value);
                        string descripcion = Convert.ToString(item.Cells[2].Value);

                        string doctipo= Convert.ToString(item.Cells[5].Value) ;
                        string doccomentario = Convert.ToString(item.Cells[8].Value) ;
                       string  docfecha = Convert.ToString(item.Cells[7].Value) ;
                       string docnumero =Convert.ToString( item.Cells[6].Value);


                        contador++;




                        if (debe == "0.00" || string.IsNullOrWhiteSpace(debe))
                        {
                            string debehaber = "H";
                            string inserta = $"insert into cg_fondos.pol values  ('801' , 'P' , '{meSS}' ,'{tipoPol}' , '{numero}' , '{maskedTextBox1.Text}' , '{cuenta}' , '{debehaber}' , '{double.Parse(haber, System.Globalization.NumberStyles.Currency)}' , '00' , '{prefijo}' ,'{doctipo}', '{docnumero}' , '{docfecha}' ,'', '{doccomentario}' ,'0' , 0 , 0 , '{anioS}' )";

                            globales.consulta(inserta);

                        }
                        else
                        {
                            string debehaber = "D";
                            string inserta = $"insert into cg_fondos.pol values ('801' , 'P' , '{meSS}' ,'{tipoPol}' , '{numero}' , '{maskedTextBox1.Text}' , '{cuenta}' , '{debehaber}' , '{double.Parse(debe, System.Globalization.NumberStyles.Currency)}' , '00' , '{prefijo}' ,'{doctipo}', '{docnumero}' , '{docfecha}' ,'', '{doccomentario}' ,'0' , 0 , 0  , '{anioS}' )";
                            globales.consulta(inserta);

                        }



                    }
                    DialogResult dialogo = globales.MessageBoxSuccess("POLIZA GUARDADA EXITOSAMENTE", "PROCESO TERMINADO", globales.menuPrincipal);




                }
                catch
                {
                    DialogResult dialogo = globales.MessageBoxSuccess("ERROR AL GUARDAR SU PÓLIZA", "PROCESO TERMINADO", globales.menuPrincipal);

                }


            }
            else
            {
                DialogResult dialogo = globales.MessageBoxExclamation("LOS IMPORTES DEBE Y HABER NO COINCIDEN", "PROCESO ABORTADO", globales.menuPrincipal);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            p1.Visible = false;
            string doctipo = string.Empty;

            switch (auxtipo.SelectedIndex)
            {
                case 0:
                    doctipo = "CLC";
                    break;
                case 1:
                    doctipo = "CH";
                    break;
                case 2:
                    doctipo = "EO";
                    break;
                case 3:
                    doctipo = "FACT";
                    break;
                case 4:
                    doctipo = "FD";
                    break;
                case 5:
                    doctipo = "REM";
                    break;
                case 6:
                    doctipo = "NOTA";
                    break;
                case 7:
                    doctipo = "OTB";
                    break;
                case 8:
                    doctipo = "OTRO";
                    break;
                case 9:
                    doctipo = "RBO";
                    break;
                case 10:
                    doctipo = "SPEI";
                    break;

            }


            string rfc = auxrfc.Text;
            string grupo = (auxgrupo.SelectedIndex == 0) ? "000" : "";
            string doccomentario = auxcomentario.Text;
            string docfecha = auxfecha.Text;
            string docnumero = auxnumero.Text;


           dtgGrid.Rows[this.row].Cells[5].Value=  doctipo;
            dtgGrid.Rows[this.row].Cells[8].Value = doccomentario;
            dtgGrid.Rows[this.row].Cells[7].Value = docfecha;
            dtgGrid.Rows[this.row].Cells[6].Value = docnumero;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            limpiaP1();
            p1.Visible = false; ;
        }
    }
    }

