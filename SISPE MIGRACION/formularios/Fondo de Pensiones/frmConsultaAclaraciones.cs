using SISPE_MIGRACION.formularios.CATÁLOGOS;
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
    public partial class frmConsultaAclaraciones : Form
    {
        private List<Dictionary<string, object>> resultado;
        private Dictionary<string, object> resultado1;

        private string fec1;
        private string MCgrid, rfcgrid, idgrid;
        private string rfcnuevo;
        private string fecha_reg;
        List<Dictionary<string, string>> listado;
        public frmConsultaAclaraciones()
        {
            InitializeComponent();
            this.Column4.ReadOnly = false;
            this.KeyPreview = true;

            this.KeyPress += new KeyPressEventHandler(frmaclaraciones_KeyPress);
            this.ShowInTaskbar = false;
        }

        internal void set_Fecha(string fecha)
        {
            this.fec1 = fecha;

        }

        private void frmaclaraciones_Load(object sender, EventArgs e)
        {
            string query = "select id,rfc,nombre_em from datos.empleados where pendiente=True order by rfc limit 300";

            List<Dictionary<string, object>> resultado = globales.consulta(query);

            Cursor.Current = Cursors.WaitCursor;

            foreach (var item in resultado)
            {
                string rfc = Convert.ToString(item["rfc"]);
                string nombre_em = Convert.ToString(item["nombre_em"]);
                string id = Convert.ToString(item["id"]);

                datos.Rows.Add("", rfc, nombre_em, "", id);


            }

        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                datos.Focus();
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);

        }

        private void txtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            string query = string.Empty;
            string paso;



            this.listado = new List<Dictionary<string, string>>();
            foreach (DataGridViewRow item in datos.Rows)
            {
                string marca = Convert.ToString(item.Cells[0].Value);
                if (marca == "N" || marca == "C" || marca == "F" || marca == "I")
                {
                    Dictionary<string, string> diccionario = new Dictionary<string, string>();
                    string rfc = Convert.ToString(item.Cells[1].Value);
                    string nombre = Convert.ToString(item.Cells[2].Value);
                    string aclaracion = Convert.ToString(item.Cells[3].Value);
                    string id = Convert.ToString(item.Cells[4].Value);
                    diccionario.Add("marca", marca);
                    diccionario.Add("rfc", rfc);
                    diccionario.Add("nombre", nombre);
                    diccionario.Add("aclaracion", aclaracion);
                    diccionario.Add("id", id);
                    listado.Add(diccionario);
                }
            }

            datos.Rows.Clear();

            foreach (Dictionary<string, string> item in this.listado)
            {
                string marca = item["marca"];
                string rfc = item["rfc"].Trim();
                string nombre = item["nombre"];
                string aclaracion = item["aclaracion"];
                string id = item["id"];
                datos.Rows.Add(marca, rfc, nombre, aclaracion, id);
            }


            query = $"SELECT ID,rfc,nombre_em FROM  datos.empleados WHERE pendiente = {!chkFusion.Checked} AND(nombre_em ILIKE '{"{0}"}%' OR rfc LIKE '{"{0}"}%') ORDER BY nombre_em  limit 50";
            query = string.Format(query, txtBusqueda.Text);


            List<Dictionary<string, object>> resultado = globales.consulta(query);
            bool continuar = true;
            foreach (var item in resultado)
            {
                string rfc = Convert.ToString(item["rfc"]).Trim();
                foreach (Dictionary<string, string> i in this.listado)
                {
                    string rfc2 = i["rfc"].Trim();
                    if (rfc2 == rfc)
                    {
                        continuar = false;
                        break;
                    }
                }

                if (!continuar)
                {
                    continuar = true;
                    continue;
                }
                string nombre_em = Convert.ToString(item["nombre_em"]);
                string id = Convert.ToString(item["id"]);

                datos.Rows.Add("", rfc, nombre_em, "", id);
            }


        }

        private void chkFusion_Click(object sender, EventArgs e)
        {
            string query = string.Empty;
            if (chkFusion.Checked)
            {
                datos.Rows.Clear();
                query = "select id,rfc,nombre_em from datos.empleados where pendiente=False order by rfc limit 300 ";
            }
            else
            {
                datos.Rows.Clear();
                query = "select id,rfc,nombre_em from datos.empleados where pendiente=True order by rfc limit 300";
            }
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            Cursor.Current = Cursors.WaitCursor;
            try
            {

                resultado.ForEach(item => datos.Rows.Add("", item["rfc"], item["nombre_em"], "", item["id"]));

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }


        private bool entro = false;
        private void datos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (entro)
            {
                this.entro = false;
                return;
            }

            int c = e.RowIndex;
            if (c == -1) return;
            DataGridViewRow row = datos.Rows[c];
            if (e.ColumnIndex == 3) return;
            // OBTINE DATOS DE LA POSICIÓN DE LA OPERACIÓN 
            MCgrid = Convert.ToString(row.Cells[0].Value);
            rfcgrid = Convert.ToString(row.Cells[1].Value);
            idgrid = Convert.ToString(row.Cells[4].Value);
            //



            //
            if ((MCgrid == "C" || MCgrid == "I") || (MCgrid == "F" && chkFusion.Checked))
            {
                frmEmpleados c_aporta = new frmEmpleados();
                c_aporta.enviar = llenacampos;
                c_aporta.ShowDialog();

                // POCISIONA

                int iColumn = datos.CurrentCell.ColumnIndex;
                int iRow = datos.CurrentCell.RowIndex;
                if (iColumn == datos.ColumnCount - 1)
                {
                    if (datos.RowCount > (iRow + 1))
                    {
                        datos.CurrentCell = datos[1, iRow + 1];
                    }

                }
                else
                {
                    this.entro = true;
                    datos.CurrentCell = datos[iColumn + 1, iRow];
                    datos.CurrentCell = datos.CurrentRow.Cells[3];
                    datos.CurrentCell.Value = rfcnuevo;
                }

            }

            if (MCgrid == "N")
            {
                int iColumn = datos.CurrentCell.ColumnIndex;
                int iRow = datos.CurrentCell.RowIndex;
                if (iColumn == datos.ColumnCount - 1)
                {
                    if (datos.RowCount > (iRow + 1))
                    {
                        datos.CurrentCell = datos[1, iRow + 1];
                    }
                    else
                    {
                        //focus next control
                    }
                }
                else
                {
                    this.entro = true;
                    datos.CurrentCell = datos[iColumn + 1, iRow];
                    datos.CurrentCell = datos.CurrentRow.Cells[3];
                    // datos.CurrentCell.Value = rfcnuevo;

                }


            }



        }



        private void llenacampos(Dictionary<string, object> datos, bool externo = false)
        {

            this.resultado1 = datos;
            this.rfcnuevo = Convert.ToString(datos["rfc"]);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool bandera = false;
            // proceso para barrer datagrid
            foreach (DataGridViewRow row in datos.Rows)
            {
                string movimiento = Convert.ToString(row.Cells[0].Value);
                string rfc = Convert.ToString(row.Cells[1].Value);
                string rfc_aclaracion = Convert.ToString(row.Cells[3].Value);
                string id = Convert.ToString(row.Cells[4].Value);


                if (movimiento == "N")
                {
                    try
                    {

                        if (!chkFusion.Checked)
                        {
                           
                        }
                        else
                        {
                            fecha_reg = "null";
                        }

                        string query = "SELECT max((nap)+1) as nap from datos.empleados";
                        List<Dictionary<string, object>> res = globales.consulta(query);
                        string nap = Convert.ToString(res[0]["nap"]);
                        DateTime fecha = DateTime.Now;
                        string solofecha = string.Format("{0:yyyy-MM-dd}", fecha);
                        string solohora = String.Format("{0:hh:mm}", fecha);
                        string fechahoy = string.Format("{0:yyyy-MM-dd hh:mm:ss}", fecha);
                        string idusuario = globales.id_usuario;
                        string actualiza = "UPDATE datos.empleados SET pendiente = FALSE,fum = '{0}',hum = '{1}', faclaracion = '{2}', taclaracion = '{3}',idusuarioa = '{4}',nap = '{5}' WHERE  rfc = '{6}' AND ID = '{7}'";
                        string pasa = string.Format(actualiza, solofecha, solohora, fechahoy, movimiento, idusuario, nap, rfc, id);
                        globales.consulta(pasa);
                        if (fecha_reg == "null")
                        {
                            string actualiza2 = "UPDATE datos.aportaciones SET status = 'n'  WHERE	rfc = TRIM ('{0}') AND status = 'p' ";
                            string paso2 = string.Format(actualiza2, rfc);
                            globales.consulta(paso2);
                        }
                        else
                        {
                            string actualiza2 = "UPDATE datos.aportaciones SET status = 'n', fecharegistro = '{0}' WHERE	rfc = TRIM ('{1}') AND status = 'p' ";
                            string paso2 = string.Format(actualiza2, fecha_reg, rfc);
                            globales.consulta(paso2);
                        }


                        string inserta = "INSERT INTO historial.movimientos (rfc_ant,movimiento,fmovimiento,idusuario)VALUES  ('{0}', ' N -> REGISTRO NUEVO SE AFECTO ESTATUS DE PENDIENTE EN EMPLEADOS Y EL RFC DE LA TABLA APORTACIONES ','{1}','{2}')";
                        string paso3 = string.Format(inserta, rfc, fec1, idusuario);
                        globales.consulta(paso3);
                        DialogResult dialogo = globales.MessageBoxSuccess("PROCESO EFECTUADO CORRECTAMENTE", "", globales.menuPrincipal);
                        //    datos.Rows.Clear();
                    }
                    catch
                    {
                        string roll = "rollback;";
                        globales.consulta(roll);
                        MessageBox.Show("ERROR, FAVOR DE VERIFICAR INFORMACIÓN");
                        datos.Rows.Clear();

                    }

                }

                if (movimiento == "I")

                    try
                    {
                        {

                            if (bandera == false)
                            {
                                if (!chkFusion.Checked)
                                {
                                
                                }
                                else
                                {
                                    fecha_reg = "null";
                                }
                            }

                            DateTime fecha = DateTime.Now;
                            string solofecha = string.Format("{0:yyyy-MM-dd hh:mm:ss}", fecha);
                            string idusuario = globales.id_usuario;
                            string inserta = "INSERT INTO historial.movimientos (rfc_ant,rfc_nue,movimiento,fmovimiento,idusuario) VALUES ( '{0}', '{1}',' I -> RFC INCORRECTO SE AFECTO ESTATUS DE PENDIENTE EN EMPLEADOS ASI COMO ACTUALIZO EL RFC CON EL DEL CAMPO ACLARACION SE AFECTA TAMBIEN LA TABLA APORTACIONES','{2}','{3}')";
                            string paso1 = string.Format(inserta, rfc, rfc_aclaracion, solofecha, idusuario);
                            globales.consulta(paso1);
                            string borra = "delete from datos.empleados where id='{0}'";
                            string paso2 = string.Format(borra, id);
                            globales.consulta(paso2);
                            if (fecha_reg == "null")
                            {
                                string actualiza = "UPDATE datos.aportaciones SET rfc = '{0}', status = 'n' WHERE rfc = Trim('{1}')  ";
                                string paso4 = string.Format(actualiza, rfc_aclaracion, rfc);
                                globales.consulta(paso4);
                                DialogResult dialogo = globales.MessageBoxSuccess("PROCESO EFECTUADO CORRECTAMENTE", "", globales.menuPrincipal);
                            }
                            else
                            {
                                string actualiza = string.Empty;

                                actualiza = "UPDATE datos.aportaciones SET rfc = '{0}', status = 'n' WHERE rfc = Trim('{1}')  ";
                                actualiza = string.Format(actualiza, rfc_aclaracion, rfc);
                                if (rfc_aclaracion.Trim() != rfc.Trim())
                                {
                                    actualiza = "UPDATE datos.aportaciones SET rfc = '{0}', status = 'n', fecharegistro = '{1}' WHERE rfc = Trim('{2}')  ";
                                    actualiza = string.Format(actualiza, rfc_aclaracion, fecha_reg, rfc);
                                }

                                globales.consulta(actualiza);
                                DialogResult dialogo = globales.MessageBoxSuccess("PROCESO EFECTUADO CORRECTAMENTE", "", globales.menuPrincipal);
                            }
                        }

                    }

                    catch
                    {
                        string roll = "rollback;";
                        globales.consulta(roll);
                        MessageBox.Show("ERROR, FAVOR DE VERIFICAR INFORMACIÓN");
                        datos.Rows.Clear();

                    }

                if (movimiento == "C")
                {
                    try
                    {
                        if (bandera == false)
                        {
                            if (!chkFusion.Checked)
                            {
                             
                            }
                            else
                            {
                                fecha_reg = "null";
                            }
                        }

                        DateTime fecha = DateTime.Now;
                        string solofecha = string.Format("{0:yyyy-MM-dd }", fecha);
                        string solohora = String.Format("{0:hh:mm}", fecha);
                        string select = "SELECT nap FROM datos.empleados WHERE rfc = '{0}' AND pendiente = FALSE ORDER BY	nap ASC LIMIT 1";
                        string paso1 = string.Format(select, rfc_aclaracion);
                        List<Dictionary<string, object>> resul = globales.consulta(paso1);
                        string nap = Convert.ToString(resul[0]["nap"]);
                        if (nap == "0" || string.IsNullOrWhiteSpace(nap))
                        {
                            string query = "SELECT max(nap) as nap from datos.empleados";
                            List<Dictionary<string, object>> lis = globales.consulta(query);
                            nap = Convert.ToString(lis[0]["nap"]);

                        }
                        string idusuario = globales.id_usuario;
                        string query2 = "INSERT INTO historial.movimientos (rfc_ant,rfc_nue,movimiento,fmovimiento,idusuario) VALUES('{0}','{1}','C -> RFC CORRECTO SE AFECTO ESTATUS DE PENDIENTE EN EMPLEADOS ASI COMO ACTUALIZO EL RFC CON EL DEL CAMPO ACLARACION SE AFECTA TAMBIEN LA TABLA APORTACIONES','{2}','{3}')";
                        string paso2 = string.Format(query2, rfc, rfc_aclaracion, solofecha, idusuario);
                        globales.consulta(paso2);

                        string query3 = "delete from datos.empleados where rfc='{0}' and pendiente=false";
                        string paso3 = string.Format(query3, rfc_aclaracion);
                        globales.consulta(paso3);

                        string query4 = "UPDATE datos.empleados SET nap = '{0}', fum = '{1}', hum = '{2}', pendiente = FALSE, faclaracion = '{3}', taclaracion = '{4}', idusuarioa = '{5}' WHERE  rfc = '{6}' AND ID = '{7}'";
                        string paso4 = string.Format(query4, nap, solofecha, solohora, solofecha, movimiento, idusuario, rfc, id);
                        globales.consulta(paso4);

                        string query5 = "UPDATE datos.aportaciones SET rfc ='{0}',status = 'n' WHERE rfc = '{1}' AND COALESCE (TRIM(status),'''') <>'e'";
                        string paso5 = string.Format(query5, rfc, rfcnuevo);
                        globales.consulta(paso5);
                        if (fecha_reg == "null")
                        {
                            string query6 = "UPDATE datos.aportaciones SET status = 'n' WHERE	rfc = '{0}' AND COALESCE (TRIM(status),'''') = 'p'";
                            string paso6 = string.Format(query6, rfc);
                            globales.consulta(paso6);
                            DialogResult dialogo = globales.MessageBoxSuccess("PROCESO EFECTUADO CORRECTAMENTE", "", globales.menuPrincipal);
                        }
                        else
                        {
                            string query6 = "UPDATE datos.aportaciones SET status = 'n',fecharegistro = '{0}' WHERE	rfc = '{1}' AND COALESCE (TRIM(status),'''') = 'p'";
                            string paso6 = string.Format(query6, fecha_reg, rfc);
                            globales.consulta(paso6);
                            DialogResult dialogo = globales.MessageBoxSuccess("PROCESO EFECTUADO CORRECTAMENTE", "", globales.menuPrincipal);
                        }

                    }
                    catch
                    {
                        string roll = "rollback;";
                        globales.consulta(roll);
                        MessageBox.Show("ERROR, FAVOR DE VERIFICAR INFORMACIÓN");
                        datos.Rows.Clear();

                    }


                }
                if (movimiento == "F" && chkFusion.Checked)
                {
                    try
                    {


                        DateTime fecha = DateTime.Now;
                        string solofecha = string.Format("{0:dd-mm-yy hh:mm:ss}", fecha);
                        string solohora = String.Format("{0:hh:mm}", fecha);
                        bool continuar = false;
                        ir:


                        if ((rfc.Substring(0, 6) == rfc_aclaracion.Substring(0, 6)) || continuar)
                        {
                            continuar = false;

                            string query1 = "INSERT INTO historial.empleados SELECT* FROM datos.empleados WHERE rfc = '{0}'";
                            string paso = string.Format(query1, rfc);
                            globales.consulta(paso);

                            string query2 = "insert into historial.aportaciones select * from datos.aportaciones where rfc='{0}'";
                            string paso2 = string.Format(query2, rfc);
                            globales.consulta(paso2);

                            string query3 = "update datos.aportaciones set rfc='{0}' where rfc='{1}'";
                            string paso3 = string.Format(query3, rfc_aclaracion, rfc);
                            globales.consulta(paso3);

                            string query4 = "delete from datos.empleados where rfc='{0}'";
                            string paso4 = string.Format(query4, rfc);
                            globales.consulta(paso4);

                            string query6 = "INSERT INTO historial.movimientos (rfc_ant,rfc_nue,movimiento,fmovimiento,idusuario)VALUES('{0}','{1}','F -> UNIFICAR DOS TARJETAS DEL MISMO EMPLEADO CON RFC DISTINTO.','{2}','{3}');";
                            string paso6 = string.Format(query6, rfcgrid, rfcnuevo, fecha_reg, idgrid);
                            globales.consulta(paso6);

                            DialogResult dialogo = globales.MessageBoxSuccess("PROCESO EFECTUADO CORRECTAMENTE", "", globales.menuPrincipal);
                            datos.Rows.Clear();
                        }
                        else
                        {
                            DialogResult dialogo2 = globales.MessageBoxQuestion("LOS PRIMEROS CARACTERES NO COINCIDEN ENTRE RFC'S", "Aviso", globales.menuPrincipal);
                            if (dialogo2 == DialogResult.Yes)
                            {
                                continuar = true;
                                goto ir;
                            }
                        }
                    }
                    catch
                    {
                        string roll = "rollback;";
                        globales.consulta(roll);
                        MessageBox.Show("ERROR, FAVOR DE VERIFICAR INFORMACIÓN");
                        datos.Rows.Clear();

                    }
                }
            }
            datos.Rows.Clear();
            txtBusqueda.Clear();
        }

        private void frmaclaraciones_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; e.SuppressKeyPress = true;

            }
        }

        private void frmaclaraciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            char S;

            S = Char.ToUpper(e.KeyChar);

            e.KeyChar = S;
        }

        private void datos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // SendKeys.Send("{UP}");
            //SendKeys.Send("{TAB}");
        }
        private int row = 0;
        private int columna = 0;
        private void datos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.row = e.RowIndex;
            this.columna = e.ColumnIndex;
        }

        private void chkFusion_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frmaclaraciones_Shown(object sender, EventArgs e)
        {
            txtBusqueda.Select();
        }

        private void datos_KeyDown(object sender, KeyEventArgs e)
        {

            if (this.columna == 0)
            {
                int oke = e.KeyValue;

                if ((e.KeyValue >= 65 && e.KeyValue <= 90))
                {
                    this.datos.Rows[this.row].Cells[0].Value = e.KeyCode;
                }

                if (e.KeyValue == 8)
                {
                    this.datos.Rows[this.row].Cells[0].Value = "";
                }




                return;
            }
            if (e.KeyCode == Keys.F5)
            {
                txtBusqueda.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }


        }



        private void datos_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

    }
}
