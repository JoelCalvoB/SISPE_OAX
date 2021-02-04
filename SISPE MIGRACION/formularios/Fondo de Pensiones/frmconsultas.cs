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
    public partial class frmconsultas : Form
    {
        private bool insertar = false;
        private Dictionary<string, object> resultado;
        private List<Dictionary<string, object>> resultado2;
        private List<Dictionary<string, object>> resultado5;
        private bool esConsulta;
        private int row = 0;
        private bool error = false;
        private bool esInsertar = false;

        public frmconsultas(bool esConsulta = false)
        {
            InitializeComponent();
            this.KeyPreview = true;

            this.KeyPress += new KeyPressEventHandler(frmaportaciones_KeyPress);
            this.esConsulta = globales.esConsulta;
        }
        public frmconsultas()
        {
            InitializeComponent();
            txtrfc.Select();
        }

        private void busqueda()
        {
            modalEmpleados c_aporta = new modalEmpleados();
            c_aporta.enviar = llenacampos;
            globales.showModal(c_aporta);


            dtggrid.Select();
            dtggrid.Focus();
        }

        private void frmaportaciones_Load(object sender, EventArgs e)
        {


            this.Activate();
            txtrfc.Focus();
            if (this.esConsulta)
            {
                this.dtggrid.ReadOnly = true;
            }

            ActiveControl = txtrfc;
            // busqueda();
            
        }

        private void llenacampos(Dictionary<string, object> datos)
        {

            this.resultado = datos;
            this.txtrfc.Text = Convert.ToString(datos["rfc"]);
            this.txtnombre.Text = Convert.ToString(datos["nombre_em"]);
            this.txtproyecto.Text = Convert.ToString(datos["proyecto"]);
            this.txtfechaing.Text = string.Format("{0:d}", datos["fecha_ing"]);
            this.txtnap.Text = Convert.ToString(datos["nap"]);
            this.txtnue.Text = Convert.ToString(datos["nue"]);
            string sueldo = Convert.ToString(datos["sueldo_base"]);
            double numsaldo = double.Parse(string.IsNullOrWhiteSpace(sueldo) ? "0" : sueldo, System.Globalization.NumberStyles.Currency);
            this.txtsueldob.Text = string.Format("{0:C2}", numsaldo).Replace("$", "");
            this.txttiporel.Text = Convert.ToString(datos["tipo_rel"]);
            this.txtdependencia.Text = Convert.ToString(datos["depe"]);

            activarControles(txtnombre, true);
            activarControles(txtproyecto, true);
            activarControles(txtdependencia, true);
            activarControles(txtsueldob, true);
            activarControles(txttiporel, true);
            activarControles(txtnap, true);
            activarControles(txtnue, true);
            activarControles(txtfechaing, true);


            //string quey = ("select inicio, final, new_tipo, entrada, salida, cuenta, movimiento,fecharegistro, id" +
            //     " FROM datos.aportaciones WHERE rfc = '{0}' AND COALESCE (TRIM(status) <> 'e')AND COALESCE(TRIM(status) <> 'p')" +
            //     "ORDER BY inicio,new_tipo asc; ");
            string quey = ("select inicio, final, new_tipo, entrada, salida, cuenta, movimiento,fecharegistro, id" +
                " FROM datos.aportaciones WHERE rfc = '{0}' AND COALESCE (TRIM(status),'')<>'e' AND COALESCE (TRIM(status),'')<>'p'" +
                "ORDER BY inicio,new_tipo asc; ");
            string query5 = ("select sum(entrada) as aporta,sum(salida) as devol" +
                 " FROM datos.aportaciones WHERE rfc = '{0}'AND COALESCE (TRIM(status) <> 'e')AND COALESCE(TRIM(status) <> 'p'); ");
            string rfc = txtrfc.Text;
            string convierte5 = string.Format(query5, rfc);
            List<Dictionary<string, object>> resultado5 = globales.consulta(convierte5);
            foreach (var item in resultado5)
            {
                string aporta = string.IsNullOrWhiteSpace(Convert.ToString(item["aporta"])) ? "0" : Convert.ToString(item["aporta"]);
                string devol = string.IsNullOrWhiteSpace(Convert.ToString(item["devol"])) ? "0" : Convert.ToString(item["devol"]);
                double saldo = Convert.ToDouble(aporta) - Convert.ToDouble(devol);
            }

            string convierte = string.Format(quey, rfc);
            List<Dictionary<string, object>> resultado = globales.consulta(convierte);
            this.resultado2 = resultado;
            foreach (var item in resultado)
            {
                string inicio = Convert.ToString(item["inicio"]).Replace("12:00:00 a. m.", "");
                string final = Convert.ToString(item["final"]).Replace("12:00:00 a. m.", "");
                string new_tipo = Convert.ToString(item["new_tipo"]);
                string entrada = string.Format("{0:C}", item["entrada"]).Replace("$", "");
                double numentrada = double.Parse(string.IsNullOrWhiteSpace(entrada) ? "0" : entrada, System.Globalization.NumberStyles.Currency);
                entrada = Convert.ToString(numentrada);
                string salida = Convert.ToString(item["salida"]);
                double numsalida = double.Parse(string.IsNullOrWhiteSpace(salida) ? "0" : salida, System.Globalization.NumberStyles.Currency);
                salida = Convert.ToString(numsalida);
                string cuenta = Convert.ToString(item["cuenta"]);
                string movimiento = Convert.ToString(item["movimiento"]);
                string fecharegistro = Convert.ToString(item["fecharegistro"]).Replace("12:00:00 a. m.", "");
                string id = Convert.ToString(item["id"]);
                //      string aporta = Convert.ToString(item["aporta"]);
                //      string devol = Convert.ToString(item["devol"]);

                //          double saldo = Convert.ToDouble(aporta) - Convert.ToDouble(devol);
                dtggrid.Rows.Add(inicio, final, new_tipo, string.Format("{0:C}", numentrada).Replace("$", ""), string.Format("{0:C2}", numsalida).Replace("$", ""), cuenta, movimiento, fecharegistro, id);


            }


            dtggrid.Select();
            dtggrid.Focus();

        }

        private void activarControles(TextBox control, bool v)
        {
            if (v)
            {
                control.ReadOnly = !v;
                control.Cursor = Cursors.IBeam;
            }
            else
            {
                control.ReadOnly = v;
                control.Cursor = Cursors.IBeam;
            }
        }

        private void LABEL01_Click(object sender, EventArgs e)
        {

        }

        private void txtAdscripcion_TextChanged(object sender, EventArgs e)
        {

        }
        private void inicia()

        {
            limpia();
            busqueda();

        }

        private void frmconsultas_keydown(object sender, KeyEventArgs e)
        {
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        button2_Click(null, null);
        //    }
        //    if (e.KeyCode == Keys.F5)
        //    {
        //        inicia();
        //    }


        //    if (e.Control && e.KeyCode == Keys.T)
        //    {
        //        pnlAntiguedad.Visible = !pnlAntiguedad.Visible;
        //        lblQuincenas2.Text = "";
        //        lblquincenas1.Text = "";
        //        lblsaldo.Text = "";
        //        return;
        //    }
        //    if (e.Control && e.KeyCode == Keys.F11)
        //    {
        //        btbuscar_Click(null, null);
        //    }
        //    if (e.Control && e.KeyCode == Keys.F6)
        //    {
        //        if (string.IsNullOrWhiteSpace(txtrfc.Text)) return;
        //        string query = "select inicio, final, new_tipo, entrada, salida, cuenta, movimiento,fecharegistro, id" +
        //        " FROM datos.aportaciones WHERE rfc = '{0}' AND COALESCE (TRIM(status),'')<>'e' AND COALESCE (TRIM(status),'')<>'p'" +
        //        "ORDER BY inicio,new_tipo asc; ";
        //        query = string.Format(query, txtrfc.Text);
        //        this.resultado2 = globales.consulta(query);
        //        frmgrafica p = new frmgrafica(this.resultado, this.resultado2, true);
        //        p.ShowDialog();
        //        return;
        //    }
        //    if (e.KeyCode == Keys.F6)
        //    {

        //        if (string.IsNullOrWhiteSpace(txtrfc.Text)) return;
        //        string query = "select inicio, final, new_tipo, entrada, salida, cuenta, movimiento,fecharegistro, id" +
        //        " FROM datos.aportaciones WHERE rfc = '{0}' AND COALESCE (TRIM(status),'')<>'e' AND COALESCE (TRIM(status),'')<>'p'" +
        //        "ORDER BY inicio,new_tipo asc; ";
        //        query = string.Format(query, txtrfc.Text);
        //        this.resultado2 = globales.consulta(query);
        //        frmgrafica p = new frmgrafica(this.resultado, this.resultado2);

        //        p.ShowDialog();
        //    }


        //    if (e.KeyCode == Keys.Down)
        //    {
        //        ActiveControl = dtggrid;
        //    }

        //    if (e.KeyCode == Keys.F4)
        //    {
        //        dtggrid.Select();
        //        dtggrid.Focus();
        //        bool bandera;
        //        if (bandera = true)
        //        {
        //            txtrfc.Select();
        //            bandera = true;
        //        }
        //    }
        //}




        //private void limpia()
        //{
        //    txtrfc.Clear();
        //    txtnombre.Clear();
        //    txtproyecto.Clear();
        //    txtfechaing.Clear();
        //    txtdependencia.Clear();
        //    txtnap.Clear();
        //    txtnue.Clear();
        //    txtsueldob.Clear();
        //    txttiporel.Clear();

        //    dtggrid.Rows.Clear();
        }

        private void frmaportaciones_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void dtggrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btngenerar_Click(object sender, EventArgs e)
        {


        }

        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.row = e.RowIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
                this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void frmaportaciones_Shown(object sender, EventArgs e)
        {
            //  button2.Focus();

            dtggrid.Select();
            dtggrid.Focus();
            inicia();
        }

        private void fecha2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DateTime fec1 = DateTime.Parse(fecha1.Text);
                    DateTime fec2 = DateTime.Parse(fecha2.Text);

                    string c1 = string.Format("{0}-{1}-{2}", fec1.Year, fec1.Month, fec1.Day);
                    string c2 = string.Format("{0}-{1}-{2}", fec2.Year, fec2.Month, fec2.Day);
                    double Q = 0;
                    int quin = 0;
                    int concilia;


                    for (int A1 = fec1.Year; A1 <= fec2.Year; A1++)
                    {
                        for (int M1 = 1; M1 <= 12; M1++)
                        {
                            quin = (M1 * 2) - 1;
                            DateTime ftemp = new DateTime(A1, M1, 8);
                            if (ftemp >= fec1 && ftemp <= fec2)
                                Q++;

                            quin++;

                            ftemp = new DateTime(A1, M1, 23);

                            if (ftemp >= fec1 && ftemp <= fec2)
                                Q++;

                        }
                    }

                    Q = 0;
                    while (true)
                    {
                        DateTime aux1 = new DateTime(fec1.Year, fec1.Month, 8);
                        if (aux1 < fec2)
                        {
                            Q++;
                        }
                        else
                        {
                            break;
                        }

                        aux1 = new DateTime(fec1.Year, fec1.Month, 23);
                        if (aux1 < fec2)
                        {
                            Q++;
                        }
                        else
                        {
                            break;
                        }

                        fec1 = fec1.AddMonths(1);
                    }

                    int Qtotales = Convert.ToInt32(Q);
                    int AA = Convert.ToInt32((Qtotales) / 24);
                    int QAux = Qtotales - (AA * 24);
                    int AM = Convert.ToInt32((QAux / 2));
                    int AQ = QAux - (AM * 2);

                    int A = AA;
                    int M = AM;
                    int q1 = AQ;

                    lblquincenas1.Text = string.Format("A:{0} - M:{1} - Q:{2}", A, M, q1);
                    lblQuincenas2.Text = "T.Q : " + Q;


                    string query = $"select COALESCE((sum(COALESCE(entrada,0)) - sum(COALESCE(salida,0))),0) as saldo from datos.aportaciones where rfc = '{txtrfc.Text}' and inicio >= '{c1}' and final <= '{c2}' AND  TRIM(COALESCE(status,'')) <> 'e' AND TRIM(COALESCE(status,'')) <> 'p'";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    double saldo = Convert.ToDouble(resultado[0]["saldo"]);
                    lblsaldo.Text = string.Format("{0:C}", saldo).Replace("$", "");
                }
            }
            }

        private void fecha1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fecha2.Select();
            }
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            //DialogResult p = globales.MessageBoxQuestion("¿Deseas actualizar el registro?", "Aviso", globales.menuPrincipal);
            //if (p == DialogResult.No) return;

            //string fechaFinal = string.Empty;
            //double sueldoBase = 0;

            //try
            //{
            //    fechaFinal = string.Format("'{0:yyyy-MM-dd}'", DateTime.Parse(txtfechaing.Text));
            //}
            //catch
            //{
            //    fechaFinal = "null";
            //    txtfechaing.Text = "";
            //}
            //try
            //{
            //    sueldoBase = double.Parse(txtsueldob.Text, System.Globalization.NumberStyles.Currency);
            //}
            //catch
            //{
            //    txtsueldob.Text = "$0.00";
            //}
            //string query = $"update datos.empleados set nombre_em='{txtnombre.Text}',proyecto='{txtproyecto.Text}',depe='{txtdependencia.Text}',sueldo_base={sueldoBase},tipo_rel='{txttiporel.Text}',nue='{txtnue.Text}',fecha_ing={fechaFinal} where rfc = '{txtrfc.Text}'";
            //if (globales.consulta(query, true))
            //{
            //    globales.MessageBoxSuccess("Empleado actualizado correctamente.", "Aviso", globales.menuPrincipal);
            //}
            //else
            //{
            //    globales.MessageBoxError("Error en actualizar el empleado.", "Aviso", globales.menuPrincipal);
            //}
        }

        private void txtsueldob_MouseLeave(object sender, EventArgs e)
        {

        }

        private void txtsueldob_Leave(object sender, EventArgs e)
        {
            try
            {
                double sueldo = double.Parse(txtsueldob.Text, System.Globalization.NumberStyles.Currency);
                txtsueldob.Text = string.Format("{0:C}", sueldo).Replace("$", "");
            }
            catch
            {
                txtsueldob.Text = "$0.00";
            }
        }

        private void dtggrid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void frmaportaciones_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dtggrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SendKeys.Send("{UP}");
            SendKeys.Send("{TAB}");
        }

        private void frmconsultas_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                button2_Click(null, null);
            }
            if (e.KeyCode == Keys.F5)
            {
                inicia();
            }


            if (e.Control && e.KeyCode == Keys.T)
            {
                pnlAntiguedad.Visible = !pnlAntiguedad.Visible;
                lblQuincenas2.Text = "";
                lblquincenas1.Text = "";
                lblsaldo.Text = "";
                return;
            }
            //if (e.Control && e.KeyCode == Keys.F11)
            //{
            //    btbuscar_Click(null, null);
            //}
            if (e.Control && e.KeyCode == Keys.F6)
            {
                if (string.IsNullOrWhiteSpace(txtrfc.Text)) return;
                string query = "select inicio, final, new_tipo, entrada, salida, cuenta, movimiento,fecharegistro, id" +
                " FROM datos.aportaciones WHERE rfc = '{0}' AND COALESCE (TRIM(status),'')<>'e' AND COALESCE (TRIM(status),'')<>'p'" +
                "ORDER BY inicio,new_tipo asc; ";
                query = string.Format(query, txtrfc.Text);
                this.resultado2 = globales.consulta(query);
                frmgrafica p = new frmgrafica(this.resultado, this.resultado2, true);
                p.ShowDialog();
                return;
            }
            if (e.KeyCode == Keys.F6)
            {

                if (string.IsNullOrWhiteSpace(txtrfc.Text)) return;
                string query = "select inicio, final, new_tipo, entrada, salida, cuenta, movimiento,fecharegistro, id" +
                " FROM datos.aportaciones WHERE rfc = '{0}' AND COALESCE (TRIM(status),'')<>'e' AND COALESCE (TRIM(status),'')<>'p'" +
                "ORDER BY inicio,new_tipo asc; ";
                query = string.Format(query, txtrfc.Text);
                this.resultado2 = globales.consulta(query);
                frmgrafica p = new frmgrafica(this.resultado, this.resultado2);

                p.ShowDialog();
            }


            if (e.KeyCode == Keys.Down)
            {
                ActiveControl = dtggrid;
            }

            if (e.KeyCode == Keys.F4)
            {
                dtggrid.Select();
                dtggrid.Focus();
                bool bandera;
                if (bandera = true)
                {
                    txtrfc.Select();
                    bandera = true;
                }
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

            dtggrid.Rows.Clear();
        }

        private void CopyToExcel(bool hdr)
        {
            if (dtggrid.SelectedRows.Count > 0)
            {
                string scopy = "<table>{0}<tbody>";
                string sheaders = "";
                foreach (DataGridViewRow row in dtggrid.SelectedRows)
                {
                    if (hdr && string.IsNullOrEmpty(sheaders))
                    {
                        sheaders = "<theader><tr>";
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            sheaders += "<th>" + cell.OwningColumn.HeaderText +
                                "</th>";
                        }
                        sheaders += "</tr></theader>";
                    }
                    scopy += "<tr>";
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            if (cell.ValueType == typeof(DateTime))
                            {
                                scopy +=
                                    "<td style=mso-number-format:\"dd/MM/yyyy HH:mm\">"
                                    + cell.Value.ToString() + "</td>";
                            }
                            else if (cell.ValueType == typeof(bool))
                            {
                                scopy += "<td style=mso-number-format:\"\\@\">" +
                                   (Convert.ToBoolean(cell.Value) ? "Yes" : "No") +
                                   "</td>";
                            }
                            else if (cell.ValueType == typeof(int))
                            {
                                scopy += "<td style=mso-number-format:\"0\">" +
                                    cell.Value.ToString() + "</td>";
                            }
                            else if (cell.ValueType == typeof(double))
                            {
                                scopy += "<td style=mso-number-format:\"0.00\">" +
                                    cell.Value.ToString() + "</td>";
                            }
                            else
                            {
                                scopy += "<td style=mso-number-format:\"\\@\">" +
                                    cell.Value.ToString() + "</td>";
                            }
                        }
                        else
                        {
                            scopy += "<td style=mso-number-format:\"\\@\"/>";
                        }
                    }
                    scopy += "</tr>";
                }
                scopy += "</tbody></table>";
                Clipboard.SetData(DataFormats.Text, string.Format(scopy, sheaders));
           }
        }

        private void dtggrid_KeyDown_1(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.C) && (e.Modifiers == Keys.Control))
            {
                CopyToExcel(true);
                e.Handled = true;
            }
        }
    }
}


