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
    public partial class frmaportaciones : Form
    {
        private bool insertar = false;
        private Dictionary<string, object> resultado;
        private List<Dictionary<string, object>> resultado2;
        private List<Dictionary<string, object>> resultado5;
        private bool esConsulta;
        private int row = 0;
        private bool error = false;
        private bool esInsertar = false;
        private bool teclaEnter;
        private bool botonIsra;

        public frmaportaciones(bool esConsulta = false)
        {
            InitializeComponent();
            this.KeyPreview = true;

            this.KeyPress += new KeyPressEventHandler(frmaportaciones_KeyPress);
            this.esConsulta = globales.esConsulta;
        }
        public frmaportaciones()
        {
            InitializeComponent();
            txtrfc.Select();
        }

        private void busqueda()
        {
            modalEmpleados empleado = new modalEmpleados();
            empleado.enviar = llenacampos;
            globales.showModal(empleado);


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

            limpia();
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
            activarControles2(txtfechaing, true);


            //string quey = ("select inicio, final, new_tipo, entrada, salida, cuenta, movimiento,fecharegistro, id" +
            //     " FROM datos.aportaciones WHERE rfc = '{0}' AND COALESCE (TRIM(status) <> 'e')AND COALESCE(TRIM(status) <> 'p')" +
            //     "ORDER BY inicio,new_tipo asc; ");
            string quey = ("select inicio, final, new_tipo, entrada, salida, cuenta, movimiento,fecharegistro, id" +
                " FROM datos.aportaciones WHERE rfc = '{0}' AND COALESCE (TRIM(status),'')<>'e' AND COALESCE (TRIM(status),'')<>'p'" +
                "ORDER BY inicio,final desc,new_tipo asc; ");
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
        private void activarControles2(MaskedTextBox control, bool v)
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
            busqueda();

        }

        private void frmaportaciones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                if (string.IsNullOrWhiteSpace(txtrfc.Text)) return;

                if (dtggrid.Rows.Count != 0) return;

                DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar una nueva aportación?", "Aviso", globales.menuPrincipal);
                if (p == DialogResult.No) return;
                this.esInsertar = true;
                string inicio = string.Format("{0:d}",DateTime.Now);
                string final = string.Format("{0:d}", DateTime.Now);
                string entrada = "0.00";
                string salida = "0.00";
                string cuenta = "";
                string fecha = string.Format("{0:d}", DateTime.Now);

                dtggrid.Rows.Insert(0);
                dtggrid.Rows[0].Cells[0].Value = inicio;
                dtggrid.Rows[0].Cells[1].Value = final;
                dtggrid.Rows[0].Cells[2].Value = "";
                dtggrid.Rows[0].Cells[3].Value = entrada;
                dtggrid.Rows[0].Cells[4].Value = salida;
                dtggrid.Rows[0].Cells[5].Value = cuenta;
                dtggrid.Rows[0].Cells[6].Value = "";
                dtggrid.Rows[0].Cells[7].Value = fecha;
                dtggrid.Rows[0].Cells[8].Value = "";
                dtggrid.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);
                string query = "insert into datos.aportaciones(rfc,inicio,final,new_tipo,entrada,salida,cuenta,fum,fecharegistro,status,idusuario) values('{0}','{1}','{2}','',0,0,'{3}','{4}','{4}','n',{5}) returning id";

                query = string.Format(query, txtrfc.Text, string.Format("{0:yyyy-MM-dd}", DateTime.Parse(inicio)), string.Format("{0:yyyy-MM-dd}", DateTime.Parse(final)), cuenta, string.Format("{0:yyyy-MM-dd}", DateTime.Parse(fecha)), globales.id_usuario);

                List<Dictionary<string, object>> resultado = globales.consulta(query);
                dtggrid.Rows[0].Cells[8].Value = resultado[0]["id"];
                dtggrid.CurrentCell = dtggrid.Rows[0].Cells[0];
                this.esInsertar = false;
            }

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
            if (e.Control && e.KeyCode == Keys.F11)
            {
                btbuscar_Click(null, null);
            }
            if (e.Control && e.KeyCode == Keys.F6)
            {
                if (string.IsNullOrWhiteSpace(txtrfc.Text)) return;
                string query = "select inicio, final, new_tipo, entrada, salida, cuenta, movimiento,fecharegistro, id" +
                " FROM datos.aportaciones WHERE rfc = '{0}' AND COALESCE (TRIM(status),'')<>'e' AND COALESCE (TRIM(status),'')<>'p'" +
                "ORDER BY inicio,final desc,new_tipo asc; ";
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
                "ORDER BY inicio,final desc,new_tipo asc; ";
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

            if (e.KeyCode == Keys.F10) {
                globales.showModal(new frmModalModificarEmpleado(txtrfc.Text));
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

        private void frmaportaciones_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void dtggrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;
            if (c == -1) return;

            if (this.error)
            {
                this.error = false;
                return;
            }

            if (this.esInsertar)
            {
                return;
            }

            string inicio = string.Empty;
            string final = string.Empty;
            double entrada = 0;
            double salida = 0;
            if (e.ColumnIndex == 0)
            {
                try
                {
                    string f1 = Convert.ToString(dtggrid.Rows[c].Cells[0].Value);
                    if (!f1.Contains("/") && !f1.Contains("."))
                    {
                        this.error = true;
                        dtggrid.Rows[c].Cells[0].Value = $"{f1.Substring(0, 2)}/{f1.Substring(2, 2)}/{f1.Substring(4)}";
                        this.error = false;
                    }
                    else
                    {
                        this.error = true;
                        dtggrid.Rows[c].Cells[0].Value = f1.Replace(".", "/");
                        this.error = false;
                    }

                    inicio = string.Format("'{0:yyyy-MM-dd}'", DateTime.Parse(Convert.ToString(dtggrid.Rows[c].Cells[0].Value)));
                }
                catch
                {
                    globales.MessageBoxError("Ingresar fecha con formato correcto:\ndd/mm/yyyy.", "Error en fecha", globales.menuPrincipal);
                    globales.MessageBoxExclamation("Los registros no se actualizaran hasta que se ingrese la fecha de inicio correctamente", "Aviso", globales.menuPrincipal);
                    this.error = true;
                    dtggrid.Rows[c].Cells[0].Value = "";
                    inicio = "null";
                    return;
                }
            }

            if (e.ColumnIndex == 1)
            {
                try
                {
                    string f1 = Convert.ToString(dtggrid.Rows[c].Cells[1].Value);
                    if (!f1.Contains("/") && !f1.Contains("."))
                    {
                        this.error = true;
                        dtggrid.Rows[c].Cells[1].Value = $"{f1.Substring(0, 2)}/{f1.Substring(2, 2)}/{f1.Substring(4)}";
                        this.error = false;
                    }
                    else {
                        this.error = true;
                        dtggrid.Rows[c].Cells[1].Value = f1.Replace(".", "/");
                        this.error = false;
                    }
                    
                    final = string.Format("'{0:yyyy-MM-dd}'", DateTime.Parse(Convert.ToString(dtggrid.Rows[c].Cells[1].Value)));
                }
                catch
                {
                    globales.MessageBoxError("Ingresar fecha con formato correcto:\ndd/mm/yyyy.", "Error en fecha", globales.menuPrincipal);
                    globales.MessageBoxExclamation("Los registros no se actualizaran hasta que se ingrese la fecha de final correctamente", "Aviso", globales.menuPrincipal);
                    this.error = true;
                    dtggrid.Rows[c].Cells[1].Value = "";
                    final = "null";
                    return;
                }
            }
            if (e.ColumnIndex == 7)
            {
                try
                {
                    string f1 = Convert.ToString(dtggrid.Rows[c].Cells[7].Value);
                    if (!f1.Contains("/") && !f1.Contains("."))
                    {
                        this.error = true;
                        dtggrid.Rows[c].Cells[7].Value = $"{f1.Substring(0, 2)}/{f1.Substring(2, 2)}/{f1.Substring(4)}";
                        this.error = false;
                    }
                    else
                    {
                        this.error = true;
                        dtggrid.Rows[c].Cells[7].Value = f1.Replace(".", "/");
                        this.error = false;
                    }
                    final = string.Format("'{0:yyyy-MM-dd}'", DateTime.Parse(Convert.ToString(dtggrid.Rows[c].Cells[7].Value)));
                }
                catch
                {
                    globales.MessageBoxError("Ingresar fecha con formato correcto:\ndd/mm/yyyy.", "Error en fecha", globales.menuPrincipal);
                    globales.MessageBoxExclamation("Los registros no se actualizaran hasta que se ingrese la fecha de final correctamente", "Aviso", globales.menuPrincipal);
                    this.error = true;
                    dtggrid.Rows[c].Cells[7].Value = "";
                    final = "null";
                    return;
                }
            }

            if (e.ColumnIndex == 3)
            {

                try
                {
                    entrada = double.Parse(Convert.ToString(dtggrid.Rows[c].Cells[3].Value), System.Globalization.NumberStyles.Currency);
                    dtggrid.Rows[c].Cells[3].Value = string.Format("{0:C}", entrada).Replace("$", "");

                }
                catch
                {
                    globales.MessageBoxError("Ingresar la entrada en el formato correcto:\n$0.00", "Error entrada", globales.menuPrincipal);
                    dtggrid.Rows[c].Cells[3].Value = string.Format("{0:C}", 0).Replace("$", "");
                }

            }

            if (e.ColumnIndex == 4)
            {
                try
                {
                    salida = double.Parse(Convert.ToString(dtggrid.Rows[c].Cells[4].Value), System.Globalization.NumberStyles.Currency);
                    dtggrid.Rows[c].Cells[4].Value = string.Format("{0:C}", salida).Replace("$", "");
                }
                catch
                {
                    globales.MessageBoxError("Ingresar la salida en el formato correcto:\n$0.00", "Error entrada", globales.menuPrincipal);
                    dtggrid.Rows[c].Cells[4].Value = string.Format("{0:C}", 0).Replace("$", "");
                }
            }
            try
            {
                inicio = string.Format("'{0:yyyy-MM-dd}'", DateTime.Parse(Convert.ToString(dtggrid.Rows[c].Cells[0].Value)));
                final = string.Format("'{0:yyyy-MM-dd}'", DateTime.Parse(Convert.ToString(dtggrid.Rows[c].Cells[1].Value)));
            }
            catch
            {

            }


            if (string.IsNullOrWhiteSpace(inicio))
            {
                globales.MessageBoxExclamation("Para actualizar información ingresar la fecha de inicio correctamente", "Aviso", globales.menuPrincipal);
                return;
            }

            if (string.IsNullOrWhiteSpace(final))
            {
                globales.MessageBoxExclamation("Para actualizar información ingresar la fecha de final correctamente", "Aviso", globales.menuPrincipal);
                return;
            }

            entrada = double.Parse(Convert.ToString(dtggrid.Rows[c].Cells[3].Value), System.Globalization.NumberStyles.Currency);
            salida = double.Parse(Convert.ToString(dtggrid.Rows[c].Cells[4].Value), System.Globalization.NumberStyles.Currency);

            string mov = Convert.ToString(dtggrid.Rows[c].Cells[2].Value).ToUpper();
            string cuenta = Convert.ToString(dtggrid.Rows[c].Cells[5].Value).ToUpper();
            string comentario = Convert.ToString(dtggrid.Rows[c].Cells[6].Value).ToUpper();
            string rfc = txtrfc.Text;
            string id = Convert.ToString(dtggrid.Rows[c].Cells[8].Value);
            string fechaRegistro = Convert.ToString(dtggrid.Rows[c].Cells[7].Value);

            dtggrid.Rows[c].Cells[2].Value = mov;
            dtggrid.Rows[c].Cells[5].Value = cuenta;
            dtggrid.Rows[c].Cells[6].Value = comentario;




            string query = "update datos.aportaciones set inicio = {2},final = {3},entrada = {4},salida={5},cuenta='{6}',movimiento = '{7}',new_tipo = '{8}',fum='{9}',idusuario={10},fecharegistro='{11}' where rfc = '{0}' and id = {1}";
            query = string.Format(query, txtrfc.Text, id, inicio, final, entrada, salida, cuenta, comentario, mov, string.Format("{0:yyyy-MM-dd}", DateTime.Now), globales.id_usuario, fechaRegistro);

            globales.consulta(query, true);

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
            try
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
            catch {
            }
        }

        private void fecha1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.botonIsra = true;
                fecha2.Focus();
                
            }
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Deseas actualizar el registro?", "Aviso", globales.menuPrincipal);
            if (p == DialogResult.No) return;

            if (string.IsNullOrWhiteSpace(txtrfc.Text)) {
                globales.MessageBoxExclamation("Se debe seleccionar una tarjeta de empleado","Aviso",globales.menuPrincipal);
                return;
            }

            string fechaFinal = string.Empty;
            double sueldoBase = 0;

            try
            {
                fechaFinal = string.Format("'{0:yyyy-MM-dd}'", DateTime.Parse(txtfechaing.Text));
            }
            catch
            {
                fechaFinal = "null";
                txtfechaing.Text = "";
            }
            try
            {
                sueldoBase = double.Parse(txtsueldob.Text, System.Globalization.NumberStyles.Currency);
            }
            catch
            {
                txtsueldob.Text = "$0.00";
            }
            string query = $"update datos.empleados set nombre_em='{txtnombre.Text}',proyecto='{txtproyecto.Text}',depe='{txtdependencia.Text}',sueldo_base={sueldoBase},tipo_rel='{txttiporel.Text}',nue='{txtnue.Text}',fecha_ing={fechaFinal} where rfc = '{txtrfc.Text}'";
            if (globales.consulta(query, true))
            {
                globales.MessageBoxSuccess("Empleado actualizado correctamente.", "Aviso", globales.menuPrincipal);
            }
            else
            {
                globales.MessageBoxError("Error en actualizar el empleado.", "Aviso", globales.menuPrincipal);
            }
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
            if (dtggrid.Rows.Count == 0) return;

            if (this.esConsulta)
            {
                return;
            }

            try
            {
                if (e.KeyCode == Keys.Insert)
                {
                    DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar una nueva aportación?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;
                    this.esInsertar = true;
                    string inicio = Convert.ToString(dtggrid.Rows[row].Cells[0].Value);
                    string final = Convert.ToString(dtggrid.Rows[row].Cells[1].Value);
                    string entrada = "0.00";
                    string salida = "0.00";
                    string cuenta = Convert.ToString(dtggrid.Rows[row].Cells[5].Value);
                    string fecha = string.Format("{0:d}", DateTime.Now);

                    int rowfinal = row + 1;


                    dtggrid.Rows.Insert(rowfinal);
                    dtggrid.Rows[rowfinal].Cells[0].Value = inicio;
                    dtggrid.Rows[rowfinal].Cells[1].Value = final;
                    dtggrid.Rows[rowfinal].Cells[2].Value = "";
                    dtggrid.Rows[rowfinal].Cells[3].Value = entrada;
                    dtggrid.Rows[rowfinal].Cells[4].Value = salida;
                    dtggrid.Rows[rowfinal].Cells[5].Value = "";
                    dtggrid.Rows[rowfinal].Cells[6].Value = "";
                    dtggrid.Rows[rowfinal].Cells[7].Value = fecha;
                    dtggrid.Rows[rowfinal].Cells[8].Value = "";
                    dtggrid.Rows[rowfinal].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);
                    string query = "insert into datos.aportaciones(rfc,inicio,final,new_tipo,entrada,salida,cuenta,fum,fecharegistro,status,idusuario) values('{0}','{1}','{2}','',0,0,'{3}','{4}','{4}','n',{5}) returning id";

                    query = string.Format(query, txtrfc.Text, string.Format("{0:yyyy-MM-dd}", DateTime.Parse(inicio)), string.Format("{0:yyyy-MM-dd}", DateTime.Parse(final)), cuenta, string.Format("{0:yyyy-MM-dd}", DateTime.Parse(fecha)), globales.id_usuario);

                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    dtggrid.Rows[row + 1].Cells[8].Value = resultado[0]["id"];
                    dtggrid.CurrentCell = dtggrid.Rows[row + 1].Cells[0];

                }

                if (e.KeyCode == Keys.Delete)
                {
                    DialogResult p = globales.MessageBoxQuestion("¿Desea eliminar la aportación?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;
                    string id = dtggrid.Rows[row].Cells[8].Value.ToString();
                    dtggrid.Rows.RemoveAt(row);
                    string query = "delete from datos.aportaciones where rfc = '{0}' and id = {1}";
                    query = string.Format(query, txtrfc.Text, id);
                    if (globales.consulta(query, true))
                    {
                        globales.MessageBoxSuccess("Aportación eliminada correctamente", "Aviso", globales.menuPrincipal);
                    }
                }

                if (e.KeyCode == Keys.F8 && e.Control)
                {
                    string id = dtggrid.Rows[row].Cells[8].Value.ToString();
                    string query = $"select fum,fecharegistro,idusuario from datos.aportaciones where id = {id}";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    string fecha = Convert.ToString(resultado[0]["fum"]).Replace(" 12:00:00 a. m.", "");
                    string fecharegistro = Convert.ToString(resultado[0]["fecharegistro"]).Replace(" 12:00:00 a. m.", "");
                    if (string.IsNullOrWhiteSpace(fecha))
                    {
                        globales.MessageBoxInformation($"Fecha de registro: {fecharegistro}", "Aviso", globales.menuPrincipal);
                    }
                    else
                    {
                        string iduser = Convert.ToString(resultado[0]["idusuario"]);
                        if (!string.IsNullOrWhiteSpace(iduser))
                        {
                            List<Dictionary<string, object>> r = globales.consulta($"select usuario from catalogos.usuarios where idusuario = {iduser}");
                            if (r.Count != 0)
                            {
                                string usuario = Convert.ToString(r[0]["usuario"]);
                                globales.MessageBoxInformation($"Fecha de registro: {fecharegistro} \nUltima modificación:{fecha}\nUsuario: {usuario}", "Aviso", globales.menuPrincipal);
                            }
                            else
                            {
                                globales.MessageBoxInformation($"Fecha de registro: {fecharegistro} \nUltima modificación:{fecha}", "Aviso", globales.menuPrincipal);
                            }
                        }
                        else
                        {
                            globales.MessageBoxInformation($"Fecha de registro: {fecharegistro} \nUltima modificación:{fecha}", "Aviso", globales.menuPrincipal);
                        }
                    }
                }
            }
            catch(Exception ee )
            {

            }

            this.esInsertar = false;
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    int iColumn = dtggrid.CurrentCell.ColumnIndex;
                    int iRow = dtggrid.CurrentCell.RowIndex;
                    if (iColumn == dtggrid.ColumnCount - 1)
                    {
                        if (dtggrid.RowCount > (iRow + 1))
                        {
                            dtggrid.CurrentCell = dtggrid[1, iRow + 1];
                        }
                        else
                        {
                            //focus next control
                        }
                    }
                    else
                        dtggrid.CurrentCell = dtggrid[iColumn + 1, iRow];
                }
                catch
                {

                }
            }

        }

        private void frmaportaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (!this.botonIsra) {
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                    this.botonIsra = false;
                }
            }
            char S;

            S = Char.ToUpper(e.KeyChar);

            e.KeyChar = S;
        }

        private void dtggrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.teclaEnter)
            {
                var x = this.row + 1;
                var y = dtggrid.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                this.teclaEnter = false;
            }
        }

        private void dtggrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion);
        }
        private void viendoEdicion(object sender, PreviewKeyDownEventArgs e)
        {
            this.teclaEnter = e.KeyCode == Keys.Enter;
        }

        private void fecha1_Leave(object sender, EventArgs e)
        {
            
        }

        private void dtggrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fecha2_Enter(object sender, EventArgs e)
        {
            //this.botonIsra = false;
        }

        private void txtrfc_Enter(object sender, EventArgs e)
        {
            this.botonIsra = false;
        }
    }
}



