using SISPE_MIGRACION.codigo.herramientas.forms;
using SISPE_MIGRACION.formularios.CATÁLOGOS;
using SISPE_MIGRACION.formularios.CATÁLOGOS.modales_catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones
{
    delegate List<Dictionary<string, object>> metodo1();

    public partial class frmconstancias : Form
    {
        private bool insertar;
        string auxtipo;
        private Dictionary<string, object> resultado;
        private List<Dictionary<string, object>> resultado2;
        private string num_histo, num_const, num_recib;
        private List<Dictionary<string, object>> lista;
        double sueldo;
        private int row;
        private bool teclaEnter;
        public bool bandera = false;

        public frmconstancias()
        {
            string auxtipoglobal;
            auxtipoglobal = auxtipo;
            InitializeComponent();
            dataco.Rows.Clear();
            dtggrid.Rows.Clear();
            lblletra.Enabled = false;
            label22.Visible = false;
            lbltquincenas.Visible = false;
        }
        private void frmconstancias_Shown(object sender, EventArgs e)
        {
            busqueda();
            ActiveControl = button2;
        }


        private void busqueda()

        {
      
            limpia();
            modalEmpleados empleado = new modalEmpleados();
            empleado.enviar = llenacampos;
            globales.showModal(empleado);

            

        }


        private void prosigue()
        {
            //calcula();
            //historialconstancias();
        }

        private void llenacampos(Dictionary<string, object> datos)
        {

            this.resultado = datos;
            this.txtrfc.Text = Convert.ToString(datos["rfc"]);
            this.txtnombre.Text = Convert.ToString(datos["nombre_em"]);
            this.txtproyecto.Text = Convert.ToString(datos["proyecto"]);
            this.txtfechaing.Text = Convert.ToString(datos["fecha_ing"]);
            this.txtnap.Text = Convert.ToString(datos["nap"]);
            this.txtnue.Text = Convert.ToString(datos["nue"]);
            this.txtsueldob.Text = Convert.ToString(datos["sueldo_base"]);
            this.txttiporel.Text = Convert.ToString(datos["tipo_rel"]);
            this.txtdependencia.Text = Convert.ToString(datos["depe"]);


            string quey = ("select inicio, final, new_tipo, entrada, salida, cuenta, movimiento,fecharegistro, id" +
                " FROM datos.aportaciones WHERE rfc = '{0}' AND COALESCE (TRIM(status),'')<>'e' AND COALESCE (TRIM(status),'')<>'p'" +
                "ORDER BY inicio,final desc,new_tipo asc; ");
            string rfc = txtrfc.Text;

            string convierte = string.Format(quey, rfc);
            List<Dictionary<string, object>> resultado = globales.consulta(convierte);
            this.resultado2 = resultado;
            double contador = resultado.Count();




            foreach (var item in resultado)
            {
                string inicio = Convert.ToString(item["inicio"]).Replace("12:00:00 a. m.", "");
                string final = Convert.ToString(item["final"]).Replace("12:00:00 a. m.", "");
                string new_tipo = Convert.ToString(item["new_tipo"]);
                string entrada = string.IsNullOrWhiteSpace(Convert.ToString(item["entrada"])) ? "0.00" : string.Format("{0:C}", Convert.ToDouble(item["entrada"])).Replace("$",""); ;
                string salida = string.IsNullOrWhiteSpace(Convert.ToString(item["salida"])) ? "0.00" : string.Format("{0:C}", Convert.ToDouble(item["salida"])).Replace("$", ""); ;
                string cuenta = Convert.ToString(item["cuenta"]);
                string movimiento = Convert.ToString(item["movimiento"]);
                string id = Convert.ToString(item["id"]);

                dtggrid.Rows.Add(inicio, final, new_tipo, entrada, salida, cuenta, movimiento, id);

            }

            calcula();
            historialconstancias();

        }

        private void calcula()
        {
            //select inicio, final, new_tipo, entrada, salida, cuenta, movimiento, fecharegistro, id" +
            //    " FROM datos.aportaciones WHERE rfc = '{0}' AND COALESCE (TRIM(status),'')<>'e' AND COALESCE (TRIM(status),'')<>'p'" +
            //    "ORDER BY inicio,new_tipo asc
            string query = ("SELECT a1.nombre_em,a1.rfc,(SUM(entrada) - SUM(salida)) AS saldo, MIN(a2.inicio) as finicio, MAX(a2.final) as ffinal,SUM(entrada) as aportacion, SUM(salida) as salida FROM datos.empleados a1 INNER JOIN datos.aportaciones a2 ON a1.rfc = a2.rfc" +
                  " WHERE a1.RFC = '{0}' AND COALESCE (TRIM(a2.status),'')<>'e' AND COALESCE(TRIM(a2.status),'')<>'p' GROUP BY a1.nombre_em, a1.rfc");
            string rfc = txtrfc.Text;

            string convierte = string.Format(query, rfc);

            List<Dictionary<string, object>> datos = globales.consulta(convierte);
            if (datos.Count >= 0)
            {
                foreach (var item in datos)
                {


                    string saldo = Convert.ToString(item["saldo"]);
                    double saldo1 = Convert.ToDouble(saldo);
                    lblnumero.Text = saldo1.ToString("C");
                    string queryletra = "select * from datos.numletra('{0}')";
                    string pasanumletra = string.Format(queryletra, Convert.ToDouble(saldo));
                    List<Dictionary<string, object>> resul = globales.consulta(pasanumletra);
                    string obtieneletra = Convert.ToString(resul[0]["numletra"]);

                    lblletra.Text = obtieneletra;
                }

                {
                    DateTime guarda_fechas;
                    string fechas1;
                    guarda_fechas = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy").Replace("12:00:00 a. m.", ""));
                    fechas1 = Convert.ToString(guarda_fechas);
                    lblexp.Text = fechas1.Replace("12:00:00 a. m.", "");
                }


            }
        }


        private void historialconstancias()
        {
            string rfchist = txtrfc.Text;
            string qry = "SELECT num_histo,num_const,num_recib FROM datos.constancias WHERE rfc = '{0}' ORDER BY fecha DESC  LIMIT 1";   // valida histórico en constancias 
            string consta = string.Format(qry, rfchist);
            List<Dictionary<string, object>> arr = globales.consulta(consta);
            if (arr.Count > 0)
            {

                string num_histo = Convert.ToString(arr[0]["num_histo"]);
                string num_const = Convert.ToString(arr[0]["num_const"]);
                string num_recib = Convert.ToString(arr[0]["num_recib"]);
                lblhistorico.Text = num_histo;
                lblconstancia.Text = num_const;
                lblrecibo.Text = num_recib;

                llenahistorial();// si hay historial lo trae
                llenanatiguedadydemas();
                lblanios.Enabled = true;
                lblmeses.Enabled = true;
                lblquincenas.Enabled = true;
                lblnumero.Enabled = true;
                lblletra.Enabled = true;
                comboBox1.Enabled = true;
                txtconcepto.Enabled = true;

                comboBox1.Focus();
            }
        }

        private void llenahistorial()
        {
            if (string.IsNullOrWhiteSpace(lblhistorico.Text)) return;
            string query01 = "SELECT * FROM datos.detalleconstancia where historico='{0}'";
            string paso01 = string.Format(query01, lblhistorico.Text);
            List<Dictionary<string, object>> movimientos = globales.consulta(paso01);
            foreach (var item in movimientos)
            {
                string inicio = Convert.ToString(item["desde"]);
                string final = Convert.ToString(item["hasta"]);
                string monto = Convert.ToString(item["monto"]);
                string detalle = Convert.ToString(item["descripcion"]);

                dataco.Rows.Add(inicio, final, monto, detalle);

            }
        }

        private void llenanatiguedadydemas()
        {
            if (string.IsNullOrWhiteSpace(lblhistorico.Text)) return;
            string query = "SELECT antig_a,antig_m,antig_q,quincenas,motivo,monto_total,total_con_letra FROM datos.constancias where num_histo='{0}'";
            string pasa = string.Format(query, lblhistorico.Text);
            List<Dictionary<string, object>> lista = globales.consulta(pasa);
            if (lista.Count > 0)
            {
                lblanios.Text = Convert.ToString(lista[0]["antig_a"]);
                lblmeses.Text = Convert.ToString(lista[0]["antig_m"]);
                lblquincenas.Text = Convert.ToString(lista[0]["antig_q"]);
                lbltquincenas.Text = Convert.ToString(lista[0]["quincenas"]);
                string motivo = Convert.ToString(lista[0]["motivo"]);
                int indice = comboBox1.Items.IndexOf(motivo);
                comboBox1.SelectedIndex = indice;
                string mttotal = Convert.ToString(lista[0]["monto_total"]);
                double num = Convert.ToDouble(mttotal);
                lblnumero.Text = Convert.ToString(String.Format("{0:C}", num));
                lblletra.Text = Convert.ToString(lista[0]["total_con_letra"]);
            }
            else
            {

            }
        }




        private void calculaporfuera()
        {
            comboBox1.SelectedIndex = 0;
            dtggrid.Rows.Clear();
            //  //select inicio, final, new_tipo, entrada, salida, cuenta, movimiento, fecharegistro, id" +
            //    " FROM datos.aportaciones WHERE rfc = '{0}' AND COALESCE (TRIM(status),'')<>'e' AND COALESCE (TRIM(status),'')<>'p'" +
            //    "ORDER BY inicio,new_tipo asc
            string quey = ("SELECT ID,inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento,fecharegistro,rfc,idusuario,status,fum" +
          " FROM datos.aportaciones WHERE rfc = '{0}'AND COALESCE  (   TRIM(status),'')<>'e' AND COALESCE (TRIM(status),'')<>'p'" +
          "ORDER BY inicio,final desc,new_tipo asc; ");
            string rfc = txtrfc.Text;

            string convierte = string.Format(quey, rfc);
            List<Dictionary<string, object>> resultado = globales.consulta(convierte);
            this.resultado2 = resultado;
            double contador = resultado.Count();
            //santiago*****


            //*************
            Dictionary<string, object> primero = null;
            bool esPrimero = true;
            Dictionary<string, object> ultimo = null;
            double suma = 0;
            string descripcion = string.Empty;
            foreach (var item in resultado)
            {
                string inicio = Convert.ToString(item["inicio"]).Replace("12:00:00 a. m.", "");
                string final = Convert.ToString(item["final"]).Replace("12:00:00 a. m.", "");
                string new_tipo = Convert.ToString(item["new_tipo"]);
                double entrada = string.IsNullOrWhiteSpace(Convert.ToString(item["entrada"])) ? 0 : Convert.ToDouble(item["entrada"]);
                double salida = string.IsNullOrWhiteSpace(Convert.ToString(item["salida"])) ? 0 : Convert.ToDouble(item["salida"]);
                string cuenta = Convert.ToString(item["cuenta"]);
                string movimiento = Convert.ToString(item["movimiento"]);
                string id = Convert.ToString(item["id"]);

                if (new_tipo == "MB") continue;
                if (new_tipo == "MV") {
                    suma += entrada - salida;
                    continue;
                };

                dtggrid.Rows.Add(inicio, final, new_tipo, entrada, salida, cuenta, movimiento, id);


                if (Convert.ToString(item["new_tipo"]) == "AR" || Convert.ToString(item["new_tipo"]) == "AD" || Convert.ToString(item["new_tipo"]) == "AP")
                    item["new_tipo"] = "AN";

                if (esPrimero)
                {
                    primero = item;
                    esPrimero = false;
                }
                auxtipo = string.Empty;
                auxtipo = Convert.ToString(item["new_tipo"]);

                if (auxtipo == Convert.ToString(primero["new_tipo"]))
                {
                    suma += entrada - salida;
                }
                if (Convert.ToString(primero["new_tipo"]) != auxtipo)
                {
                    if (ultimo == null) ultimo = primero;

                    dataco.Rows.Add(string.Format("{0:d}", primero["inicio"]), string.Format("{0:d}", ultimo["final"]), string.Format("{0:C}", suma).Replace("$", ""), descripcion, primero["new_tipo"]);
                    primero = item;
                    ultimo = null;
                    suma = entrada - salida;
                    ultimo = item;
                    continue;
                }

                ultimo = item;


            }

            dataco.Rows.Add(string.Format("{0:d}", primero["inicio"]), string.Format("{0:d}", ultimo["final"]), string.Format("{0:C}", suma).Replace("$", ""), descripcion, primero["new_tipo"]);

            foreach (DataGridViewRow item in dataco.Rows) {
                string new_tipo = Convert.ToString(item.Cells[4].Value);
                descripcion = string.Empty;
                switch (new_tipo) {
                    case "AN":
                        descripcion = "APORTACIÓN";
                        break;
                    case "DP":
                        descripcion = "PRESCRIPCIÓN";
                        break;
                    case "DC":
                        descripcion = "DEVOLUCIÓN";
                        break;
                    case "MN":
                        descripcion = "PERIODO NO APORTADO";
                        break;
                    case "DF":
                        descripcion = "DEVOLUCIÓN DE FONDO";
                        break;
                    case "MB":
                        descripcion = "BAJA O RENUNCIA";
                        break;
                    case "MC":
                        descripcion = "EMISION DE CONSTANCIAS";
                        break;
                    case "MF":
                        descripcion = "MOVIMIENTO POR FALLECIMIENTO";
                        break;
                    case "MJ":
                        descripcion = "MOVIMIENTO POR JUBILACIÓN Y/O PENSIÓN";
                        break;
                    case "MV":
                        descripcion = "MOVIMIENTOS VARIOS";
                        break;
                    default:
                        break;
                }

                item.Cells[3].Value = descripcion;

            }

            dtggrid.ReadOnly = true;
        }



        private void frmaportaciones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
            if (e.KeyCode == Keys.F5)
            {
                frmconstancias_Shown(null, null);
            }

            if (e.KeyCode == Keys.F9)
            {

                DialogResult dialogResult = MessageBox.Show("¿DESEA ACTUALIZAR ESTE FOLIO?", "Constancias", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (!string.IsNullOrWhiteSpace(lblhistorico.Text))
                    {
                        string query = "delete from datos.detalleconstancia where historico ='{0}'";
                        string paso = string.Format(query, lblhistorico.Text);
                        globales.consulta(paso);

                        foreach (DataGridViewRow row in dataco.Rows)
                            try
                            {
                                {
                                    string inicio = Convert.ToString(row.Cells[0].Value);
                                    string final = Convert.ToString(row.Cells[1].Value);
                                    string monto = Convert.ToString(row.Cells[2].Value);
                                    string detalle = Convert.ToString(row.Cells[3].Value);

                                    string ins = "insert into datos.detalleconstancia (rfc,historico,desde,hasta,monto,descripcion) values ('{0}','{1}','{2}','{3}','{4}','{5}')";
                                    string insertadetalle = string.Format(ins, txtrfc.Text, lblhistorico.Text, inicio, final, monto, detalle);
                                    if (!string.IsNullOrWhiteSpace(inicio))
                                    {
                                        globales.consulta(insertadetalle);
                                    }
                                }
                            }
                            catch
                            {
                                MessageBox.Show("VERIFIQUE FORMATOS, CONTACTE A SISTEMAS");
                            }
                        string actu = "update datos.constancias set monto_total='{0}',total_con_letra='{1}',motivo='{2}',antig_a='{3}',antig_m='{4}',antig_q='{5}',quincenas='{6}' where num_histo='{7}'";
                        double numletr = double.Parse(lblnumero.Text, System.Globalization.NumberStyles.Currency);
                        string actualizaconsta = string.Format(actu, numletr, lblletra.Text, txtconcepto.Text, lblanios.Text, lblmeses.Text, lblquincenas.Text, lbltquincenas.Text, lblhistorico.Text);

                        globales.consulta(actualizaconsta);
                        MessageBox.Show("REGISTROS ACTUALIZADOS");
                    }
                    else
                    {
                        MessageBox.Show("NO ESTAS TRABAJANDO SOBRE UN FOLIO YA EXISTENTE");
                    }
                }
                this.Close();
            }


            if (e.KeyCode == Keys.F6)
            {
                if (string.IsNullOrWhiteSpace(txtrfc.Text)) return;
                dataco.Rows.Clear();
                lblhistorico.Text = "";
                lblconstancia.Text = "";
                lblrecibo.Text = "";
                dataco.Rows.Clear();
                frmgrafica p = new frmgrafica(this.resultado, this.resultado2, true);
                p.ShowDialog();
                string quincena = p.quincenas.Replace("Quincenas", "");

                int Qtotales = Convert.ToInt32(quincena);
                int AA = Convert.ToInt32((Qtotales) / 24);
                int QAux = Qtotales - (AA * 24);
                int AM = Convert.ToInt32((QAux / 2));
                int AQ = QAux - (AM * 2);
                lbltquincenas.Text = Convert.ToString(quincena);
                lblquincenas.Text = Convert.ToString(AQ);
                lblmeses.Text = Convert.ToString(AM);
                lblanios.Text = Convert.ToString(AA);
                lblanios.Enabled = true;
                lblmeses.Enabled = true;
                lblquincenas.Enabled = true;
                lblnumero.Enabled = true;
                lblletra.Enabled = true;
                comboBox1.Enabled = true;
                txtconcepto.Enabled = true;

                comboBox1.Focus();
                calculaporfuera();
                calcula();
            }  
             
            
            

        }



        private void limpia()
        {
            txtrfc.Clear();
            txtnombre.Clear();
            txtproyecto.Clear();
            txtfechaing.Clear();
            txtdependencia.Clear();
            txtnap.Clear();
            txtnue.Clear();
            txtsueldob.Clear();
            txttiporel.Clear();
            lblanios.Clear();
            lblconstancia.Text = "-";
            lblexp.Clear();
            lblhistorico.Text = "-";
            lblletra.Clear();
            lblmeses.Clear();
            lblnumero.Clear();
            lblquincenas.Clear();
            lblrecibo.Text = "-";
            lbltquincenas.Clear();
            
            dtggrid.Rows.Clear();
            dataco.Rows.Clear();
        }

        private void frmaportaciones_FormClosing(object sender, FormClosingEventArgs e)
        {


        }



        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int c = e.RowIndex;

            }

            catch
            {


            }
        }

        private void frmconstancias_Load_1(object sender, EventArgs e)
        {
            lblexp.Focus();
            comboBox1.SelectedIndex = 1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (comboBox1.SelectedIndex == 10)
            {
                txtconcepto.Clear();
            }
            else
            {
                txtconcepto.Text = comboBox1.Text;
            }
        }

        private void lblnumero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double numero = double.Parse(lblnumero.Text, System.Globalization.NumberStyles.Currency);
                string saldo = Convert.ToString(String.Format("{0:C}", numero));
                lblnumero.Text = saldo;
                string query = "select * from datos.numletra('{0}')";
                string pasa = string.Format(query, numero);
                List<Dictionary<string, object>> resul = globales.consulta(pasa);
                lblletra.Text = Convert.ToString(resul[0]["numletra"]);

            }
        }

        private void txtconcepto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(lblhistorico.Text))
                {
                    DialogResult dialogo2 = globales.MessageBoxExclamation("SI USTED DESEA GENERAR UN NUEVO FOLIO PULSE F6 O SI DESEA ACTUALIZAR PULSE F9", "AVISO", globales.menuPrincipal);
                    return;
                }

                

                frmopcionconsta consta = new frmopcionconsta(this.resultado, lblanios.Text, lblmeses.Text, lblquincenas.Text, lbltquincenas.Text, txtconcepto.Text, lblletra.Text, lblnumero.Text, txtnombre.Text);
                consta.getDetalle = obtenerLista;
                consta.ShowDialog();
                this.Close();

            }
        }

        private List<Dictionary<string, object>> obtenerLista()
        {
            List<Dictionary<string, object>> lista = new List<Dictionary<string, object>>();
            foreach (DataGridViewRow item in dataco.Rows)
            {
                if (string.IsNullOrWhiteSpace(Convert.ToString(item.Cells[0].Value))) break;
                Dictionary<string, object> diccionario = new Dictionary<string, object>();
                diccionario.Add("inicio", item.Cells[0].Value);
                diccionario.Add("final", item.Cells[1].Value);
                diccionario.Add("monto", item.Cells[2].Value);
                diccionario.Add("detalle", item.Cells[3].Value);
                lista.Add(diccionario);
            }

            return lista;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) {
                dataco.Rows.RemoveAt(this.row);
            }

            if (e.KeyCode == Keys.Insert) {

                dataco.Rows.Insert(this.row+1);
                dataco.Rows[row + 1].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);
                dataco.CurrentCell = dataco.Rows[row + 1].Cells[0];
            }


            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    int iColumn = dataco.CurrentCell.ColumnIndex;
                    int iRow = dataco.CurrentCell.RowIndex;
                    if (iColumn == dataco.ColumnCount - 1)
                    {
                        if (dataco.RowCount > (iRow + 1))
                        {
                            dataco.CurrentCell = dtggrid[1, iRow + 1];
                        }
                        else
                        {
                            //focus next control
                        }
                    }
                    else
                        dataco.CurrentCell = dtggrid[iColumn + 1, iRow];
                }
                catch
                {

                }
            }
        }

        private void dataco_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            int row = e.RowIndex;
            if (row == -1) return;
            int column = e.ColumnIndex;
            if (column == 2) {
                try
                {
                    dataco.Rows[row].Cells[2].Value = string.Format("{0:C}", double.Parse(Convert.ToString(dataco.Rows[row].Cells[2].Value), System.Globalization.NumberStyles.Currency)).Replace("$", "");
                }
                catch {
                    dataco.Rows[row].Cells[2].Value = 0;
                }
            }
        }

        private void dataco_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.teclaEnter)
            {
                var x = this.row + 1;
                var y = dataco.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                this.teclaEnter = false;
            }
        }

        private void dataco_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion);
        }

        private void viendoEdicion(object sender, PreviewKeyDownEventArgs e)
        {
            this.teclaEnter = e.KeyCode == Keys.Enter;
        }

        private void txtconcepto_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataco_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.row = e.RowIndex;
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtconcepto.Focus();
            }
        }

    
    }
}
