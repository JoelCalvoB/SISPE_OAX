using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones
{
    public partial class imprimirconsta : Form
    {
        internal enviarDatos enviar;
        private List<Dictionary<string, object>> resultado;
        private Dictionary<string, object> valor;
        private string rfc = string.Empty;
        public imprimirconsta()
        {
            InitializeComponent();

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            if (resultado.Count == 0) return;
            foreach (Dictionary<string, object> item in resultado)
            {
                if (item["rfc"].Equals(rfc))
                {
                    valor = item;
                    break;
                }
            }
            limpiar();
            Hide();


        }

        private void datoscont()
        {


        }


        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select con.rfc,emp.nombre_em,con.num_histo,con.num_const,con.num_recib,con.num_constpro,con.fecha_exp from datos.constancias as con join datos.empleados as emp on con.rfc=emp.rfc order by con.fecha desc limit 50";
                List<Dictionary<string, object>> resultado = globales.consulta(query);

                resultado.ForEach(o => datos.Rows.Add(o["rfc"], o["nombre_em"], o["num_histo"], o["num_const"], o["num_recib"], o["num_constpro"], string.Format("{0:d}", o["fecha_exp"])));
            }
            catch
            {
                MessageBox.Show("Error en la consulta, favor de contactar a sistemas para dar solución", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            limpiar();
            Close();
        }

        private void limpiar()
        {
            txtBusqueda.Text = "";
        }

        private void txtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
          
        }

        //private void datos_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        //{
        //    rfc = Convert.ToString(datos.Rows[e.Cell.RowIndex].Cells[0].Value);
        //}

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                datos.Focus();
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (e.KeyChar == 13)
                btnseleccionar_Click(null, null);
        }

        private void datos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar))
            {
                txtBusqueda.Focus();
            }

        }

        private void datos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void datos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

            }
            if (e.KeyCode == Keys.Back)
            {
                txtBusqueda.Focus();
            }
        }
        private void imprimirconstancias()
        {

        }

        private void imprimirconsta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                this.Close();
        }

        private void datos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = datos.Rows[c];
            string rfc = Convert.ToString(row.Cells[0].Value);
            string nombre = Convert.ToString(row.Cells[1].Value);
            string num_histo = Convert.ToString(row.Cells[2].Value);
            string num_const = Convert.ToString(row.Cells[3].Value);
            string num_recib = Convert.ToString(row.Cells[4].Value);
            string num_constpro = Convert.ToString(row.Cells[5].Value);
            string fecha_exp = Convert.ToString(row.Cells[6].Value);


            ///  empieza selección de reportes
            if (!string.IsNullOrWhiteSpace(num_histo) && string.IsNullOrWhiteSpace(num_const) && string.IsNullOrWhiteSpace(num_recib) && string.IsNullOrWhiteSpace(num_constpro))    //historico
            {
                string query = "select a1.rfc,a2.nombre_em,a1.num_histo,a1.monto_total,a1.antig_a,a1.antig_m,a1.antig_q,a1.fecha_exp from datos.constancias a1 JOIN datos.empleados a2 ON a1.rfc=a2.rfc where a1.num_histo='{0}'";
                string paso = string.Format(query, num_histo);
                List<Dictionary<string, object>> dic = globales.consulta(paso);
                foreach (var item in dic)
                {
                    string folioactual = Convert.ToString(item["num_histo"]);
                    string fecha = Convert.ToString(item["fecha_exp"]);
                    string rfc1 = Convert.ToString(item["rfc"]);
                    string nombre1 = Convert.ToString(item["nombre_em"]);
                    string anios = Convert.ToString(item["antig_a"]);
                    string meses = Convert.ToString(item["antig_m"]);
                    string quincenas = Convert.ToString(item["antig_q"]);
                    string monto = Convert.ToString(item["monto_total"]);
                    DateTime solofe = Convert.ToDateTime(fecha);
                    string solofecha = solofe.ToShortDateString();
                    string queryfecha = "select  datos.fechaletra('{0}')";
                    string pasofecha = string.Format(queryfecha, solofecha);
                    List<Dictionary<string, object>> f1 = globales.consulta(pasofecha);
                    string fechaletra = Convert.ToString(f1[0]["fechaletra"]);
                    string querydetalle = "SELECT desde,hasta,monto,descripcion FROM datos.detalleconstancia where historico='{0}'";
                    string pasodetalle = string.Format(querydetalle, folioactual);

                    List<Dictionary<string, object>> res = globales.consulta(pasodetalle);
                    object[] aux2 = new object[res.Count];
                    int contador = 0;

                    foreach (var temp in res)
                    {
                        string desde = string.Empty;
                        string hasta = string.Empty;
                        string montodetalle = string.Empty;
                        string descrip = string.Empty;

                        try
                        {
                            desde = Convert.ToString(temp["desde"]);
                            hasta = Convert.ToString(temp["hasta"]);
                            montodetalle = Convert.ToString(temp["monto"]);
                            descrip = Convert.ToString(temp["descripcion"]);
                        }
                        catch
                        {

                        }

                        object[] tt1 = { desde, hasta, montodetalle, descrip };
                        aux2[contador] = tt1;
                        contador++;
                    }

                    string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                    object[][] request = new object[2][];
                    object[] headers = { "numhisto", "rfc", "nombre", "total", "anios", "meses", "quincenas","logo" };
                    object[] body = { folioactual, rfc, nombre, Convert.ToString(monto), anios, meses, quincenas,logo };

                    request[0] = headers;
                    request[1] = body;
                    globales.reportes("historicosispeconstancia", "detallehistorico", aux2, "", false, request);
                    this.Cursor = Cursors.Default;
                    return;
                }
            }

            else if (!string.IsNullOrWhiteSpace(num_histo) && !string.IsNullOrWhiteSpace(num_const) && string.IsNullOrWhiteSpace(num_recib))   // constancia 
            {
                string query = "select a1.rfc,a1.num_histo,a2.nombre_em,a1.num_const,a1.monto_total,a1.antig_a,a1.antig_m,a1.antig_q,a1.fecha_exp,a1.total_con_letra,a1.motivo,a1.firma_dir,a1.firma_mc,a1.firma_ui from datos.constancias a1 JOIN datos.empleados a2 ON a1.rfc=a2.rfc where a1.num_histo='{0}'";
                string paso = string.Format(query, num_histo);
                List<Dictionary<string, object>> dic = globales.consulta(paso);
                foreach (var item in dic)
                {
                    string folioactual = Convert.ToString(item["num_const"]);



                    if (folioactual.Length == 1) folioactual = "00" + folioactual;
                    if (folioactual.Length == 2) folioactual = "0" + folioactual;


                    string num_histoimp = Convert.ToString(item["num_histo"]);
                    string fecha = Convert.ToString(item["fecha_exp"]);
                    string rfc1 = Convert.ToString(item["rfc"]);
                    string nombre1 = Convert.ToString(item["nombre_em"]);
                    string anios = Convert.ToString(item["antig_a"]);
                    string meses = Convert.ToString(item["antig_m"]);
                    string quincenas = Convert.ToString(item["antig_q"]);
                    string monto = Convert.ToString(item["monto_total"]);
                    string letra = Convert.ToString(item["total_con_letra"]);
                    string motivo = Convert.ToString(item["motivo"]);
                    string firma_dir = Convert.ToString(item["firma_dir"]);
                    string firma_mc = Convert.ToString(item["firma_mc"]);
                    string firma_ui = Convert.ToString(item["firma_ui"]);
                    DateTime solofe = Convert.ToDateTime(fecha);
                    string solofecha = solofe.ToShortDateString();
                    string queryfecha = "select  datos.fechaletra('{0}')";
                    string pasofecha = string.Format(queryfecha, solofecha);
                    List<Dictionary<string, object>> f1 = globales.consulta(pasofecha);
                    string fechaletra = Convert.ToString(f1[0]["fechaletra"]);
                    string querydetalle = "SELECT desde,hasta,monto,descripcion FROM datos.detalleconstancia where historico='{0}'";
                    string pasodetalle = string.Format(querydetalle, num_histo);

                    List<Dictionary<string, object>> res = globales.consulta(pasodetalle);
                    object[] aux2 = new object[17];
                    int contador = 0;


                    for (int x = 0; x < 17; x++)
                    {
                        object[] tt1 = { "", "", "", "" };
                        if (x < res.Count)
                        {
                            string desde = string.Empty;
                            string hasta = string.Empty;
                            string montodetalle = string.Empty;
                            string descrip = string.Empty;

                            try
                            {
                                desde = Convert.ToString(res[x]["desde"]);
                                hasta = Convert.ToString(res[x]["hasta"]);
                                montodetalle = Convert.ToString(res[x]["monto"]);
                                descrip = Convert.ToString(res[x]["descripcion"]);

                                tt1[0] = desde;
                                tt1[1] = hasta;
                                tt1[2] = montodetalle;
                                tt1[3] = descrip;
                            }
                            catch
                            {

                            }
                        }
                        aux2[x] = tt1;
                    }

                    string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                    object[][] request = new object[2][];
                    object[] headers = { "folio", "fecha", "rfc", "nombre", "anios", "meses", "quincenas", "totalnum", "totalletra", "firma_mc", "firma_dir", "firma_ui", "motivo","logo" };
                    object[] body = { folioactual, fechaletra, rfc, nombre, anios, meses, quincenas, Convert.ToString(monto), letra, firma_mc, firma_dir, firma_ui, motivo,logo };

                    request[0] = headers;
                    request[1] = body;
                    globales.reportes("emitirconstancias", "detallehistorico", aux2, "", false, request); this.Cursor = Cursors.Default;
                    return;
                }
            }
            else if (!string.IsNullOrWhiteSpace(num_histo) && !string.IsNullOrWhiteSpace(num_const) && !string.IsNullOrWhiteSpace(num_recib))   // recibo
            {


                string query = "select a1.rfc,a1.num_histo,a2.nombre_em,a1.num_const,a1.num_recib,a1.monto_total,a1.antig_a,a1.antig_m,a1.antig_q,a1.fecha_exp,a1.total_con_letra,a1.motivo,a1.firma_dir,a1.firma_mc,a1.firma_ui,a1.firma_d_p from datos.constancias a1 JOIN datos.empleados a2 ON a1.rfc=a2.rfc where a1.num_histo='{0}'";
                string paso = string.Format(query, num_histo);
                List<Dictionary<string, object>> dic = globales.consulta(paso);

                foreach (var item in dic)
                {
                    string folio_const = Convert.ToString(item["num_const"]);
                    string folionum_recib = Convert.ToString(item["num_recib"]);

                    if (folionum_recib.Length == 1) folionum_recib = "00" + folionum_recib;
                    if (folionum_recib.Length == 2) folionum_recib = "0" + folionum_recib;


                    if (folio_const.Length == 1) folio_const = "00" + folio_const;
                    if (folio_const.Length == 2) folio_const = "0" + folio_const;

                    string fecha = Convert.ToString(item["fecha_exp"]);
                    string rfc1 = Convert.ToString(item["rfc"]);
                    string nombre1 = Convert.ToString(item["nombre_em"]);
                    string anios = Convert.ToString(item["antig_a"]);
                    string meses = Convert.ToString(item["antig_m"]);
                    string quincenas = Convert.ToString(item["antig_q"]);
                    string monto = Convert.ToString(item["monto_total"]);
                    string letra = Convert.ToString(item["total_con_letra"]);
                    string motivo = Convert.ToString(item["motivo"]);
                    string firma_dir = Convert.ToString(item["firma_dir"]);
                    string firma_mc = Convert.ToString(item["firma_mc"]);
                    string firma_ui = Convert.ToString(item["firma_ui"]);
                    string firma_d_p = Convert.ToString(item["firma_d_p"]);
                    DateTime solofe = Convert.ToDateTime(fecha);
                    string solofecha = solofe.ToShortDateString();
                    string queryfecha = "select  datos.fechaletra('{0}')";
                    string pasofecha = string.Format(queryfecha, solofecha);
                    List<Dictionary<string, object>> f1 = globales.consulta(pasofecha);
                    string fechaletra = Convert.ToString(f1[0]["fechaletra"]);
                    string contenido = "";
                    string fecharecortada = solofecha.Replace("Oaxaca de Juarez,Oax.,a", "");
                    // aqui empieza el reporte 

                    double auxmonto = string.IsNullOrWhiteSpace(monto) ? 0 : Convert.ToDouble(monto);

                    frmVentanaImpConstancias f = new frmVentanaImpConstancias(string.Format("{0:C}", auxmonto), nombre1);
                    f.ShowDialog();
                    if (!f.continuaVentana) return;


                    string monto1 = f.monto1;
                    string monto2 = f.monto2;
                    string monto3 = f.monto3;
                    string total = f.total;

                    string concepto1 = f.concepto1;
                    string concepto2 = f.concepto2;
                    string concepto3 = f.concepto3;


                    contenido = "Recibí  de  la  Oficina  de  Pensiones del  Gobierno del Estado de Oaxaca, la cantidad de: (" + letra + ") " +
                "Por concepto de Devolución de Fondo de Pensiones de conformidad con el Artículo 64 de la Ley de " +
                "Pensiones para los Trabajadores del Gobierno del Estado de Oaxaca,según el monto que ampara la Constancia de " +
                "Contribución N°" + folio_const + " de fecha " + fecharecortada + " del cual se deduce los saldos existentes por los conceptos " +
                "que a continuación se detallan:";
                    Label l = new Label();
                    l.Text = contenido;
                    l.Font = new Font("Arial", 12, FontStyle.Regular);


                    var cnd = globales.justificar(l.Text, 86);
                    contenido = string.Empty;
                    foreach (string aux1 in cnd)
                    {
                        contenido += aux1 + "\n";
                    }

                    string querydetalle = "SELECT desde,hasta,monto,descripcion FROM datos.detalleconstancia where historico='{0}'";
                    string pasodetalle = string.Format(querydetalle, num_histo);

                    List<Dictionary<string, object>> res = globales.consulta(pasodetalle);
                    object[] aux2 = new object[15];
                    int contador = 0;


                    for (int x = 0; x < 15; x++)
                    {
                        object[] tt1 = { "", "", "", "" };
                        if (x < res.Count)
                        {
                            string desde = string.Empty;
                            string hasta = string.Empty;
                            string montodetalle = string.Empty;
                            string descrip = string.Empty;

                            try
                            {
                                desde = Convert.ToString(res[x]["desde"]);
                                hasta = Convert.ToString(res[x]["hasta"]);
                                montodetalle = Convert.ToString(res[x]["monto"]);
                                descrip = Convert.ToString(res[x]["descripcion"]);

                                tt1[0] = desde;
                                tt1[1] = hasta;
                                tt1[2] = montodetalle;
                                tt1[3] = descrip;
                            }
                            catch
                            {

                            }
                        }
                        aux2[x] = tt1;
                    }

                    string fallecido = string.Empty;
                    string nombre2 = string.Empty;
                    if (f.chk1.Checked)
                    {
                        nombre2 = f.txtBenefi.Text;
                        fallecido = "FALLECIDO: " + f.txtFallecido.Text;
                    }
                    else
                    {
                        nombre2 = nombre;
                    }

                    string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                    object[][] request = new object[2][];
                    object[] headers = { "folio", "fecha", "rfc", "nombre", "anios", "meses", "quincenas", "totalnum", "totalletra", "firma_mc", "firma_dir", "firma_ui", "motivo", "firma_d_p", "contenido", "num_recib", "monto1", "monto2", "monto3", "total", "concepto1", "concepto2", "concepto3", "fallecido", "nombre2","logo" };
                    object[] body = { folio_const, fechaletra, rfc, nombre, anios, meses, quincenas, Convert.ToString(monto), letra, firma_mc, firma_dir, firma_ui, motivo, firma_d_p, contenido, folionum_recib, monto1, monto2, monto3, total, concepto1, concepto2, concepto3, fallecido, nombre2,logo};

                    request[0] = headers;
                    request[1] = body;
                    globales.reportes("constayrecibo", "detallehistorico", aux2, "", false, request);
                    this.Cursor = Cursors.Default;
                    return;
                }
            }
            else if (!string.IsNullOrWhiteSpace(num_histo) && string.IsNullOrWhiteSpace(num_const) && string.IsNullOrWhiteSpace(num_recib) && !string.IsNullOrWhiteSpace(num_constpro))
            {

                string query = "select a1.rfc,a1.num_histo,a1.num_constpro,a2.nombre_em,a1.num_const,a1.monto_total,a1.antig_a,a1.antig_m,a1.antig_q,a1.fecha_exp,a1.total_con_letra,a1.motivo,a1.firma_dir,a1.firma_mc,a1.firma_ui from datos.constancias a1 JOIN datos.empleados a2 ON a1.rfc=a2.rfc where a1.num_histo='{0}'";
                string paso = string.Format(query, num_histo);
                List<Dictionary<string, object>> dic = globales.consulta(paso);
                foreach (var item in dic)
                {
                    string folioactual = Convert.ToString(item["num_constpro"]);
                    string num_histoimp = Convert.ToString(item["num_histo"]);
                    string fecha = Convert.ToString(item["fecha_exp"]);
                    string rfc1 = Convert.ToString(item["rfc"]);
                    string nombre1 = Convert.ToString(item["nombre_em"]);
                    string anios = Convert.ToString(item["antig_a"]);
                    string meses = Convert.ToString(item["antig_m"]);
                    string quincenas = Convert.ToString(item["antig_q"]);
                    string monto = Convert.ToString(item["monto_total"]);
                    string letra = Convert.ToString(item["total_con_letra"]);
                    string motivo = Convert.ToString(item["motivo"]);
                    string firma_dir = Convert.ToString(item["firma_dir"]);
                    string firma_mc = Convert.ToString(item["firma_mc"]);
                    string firma_ui = Convert.ToString(item["firma_ui"]);
                    DateTime solofe = Convert.ToDateTime(fecha);
                    string solofecha = solofe.ToShortDateString();
                    string queryfecha = "select  datos.fechaletra('{0}')";
                    string pasofecha = string.Format(queryfecha, solofecha);
                    List<Dictionary<string, object>> f1 = globales.consulta(pasofecha);
                    string fechaletra = Convert.ToString(f1[0]["fechaletra"]);
                    string querydetalle = "SELECT desde,hasta,monto,descripcion,sueldo,new_tipo FROM datos.detalleconstancia where historico='{0}'";
                    string pasodetalle = string.Format(querydetalle, num_histoimp);

                    List<Dictionary<string, object>> res = globales.consulta(pasodetalle);
                    object[] aux2 = new object[28];

                    double suma = 0;
                    int orden = 0;
                    for (int x = 0; x < 28; x++)
                    {
                        object[] tt1 = { "", "", "", "", "", "", "", "" };
                        if (x < res.Count)
                        {
                            string desde = string.Empty;
                            string hasta = string.Empty;
                            string montodetalle = string.Empty;
                            string descrip = string.Empty;
                            string sueldo = string.Empty;

                            try
                            {
                                desde = Convert.ToString(res[x]["desde"]).Replace("12:00:00 a. m.", "");
                                hasta = Convert.ToString(res[x]["hasta"]).Replace("12:00:00 a. m.", "");
                                montodetalle = Convert.ToString(res[x]["monto"]);
                                descrip = Convert.ToString(res[x]["new_tipo"]);
                                sueldo = Convert.ToString(res[x]["sueldo"]);
                                tt1[6] = "";
                                if (descrip == "AN")
                                {
                                    orden++;
                                    tt1[6] = orden;

                                }
                                tt1[0] = desde;
                                tt1[1] = hasta;
                                tt1[2] = montodetalle;
                                tt1[3] = descrip;
                                tt1[7] = string.IsNullOrWhiteSpace(sueldo) ? "" : String.Format("{0:0}", sueldo);

                                suma += string.IsNullOrWhiteSpace(sueldo) ? 0 : Convert.ToDouble(sueldo);
                            }
                            catch
                            {

                            }
                        }
                        aux2[x] = tt1;
                    }
                    string logo = globales.getImagen(globales.imagenesSispe.logoreportes);
                    object[][] request = new object[2][];
                    object[] headers = { "folio", "fecha", "rfc", "nombre", "anios", "meses", "quincenas", "totalnum", "totalletra", "firma_mc", "firma_dir", "firma_ui", "motivo", "logo" };
                    object[] body = { folioactual, fechaletra, rfc, nombre, string.Format("{0:C}", suma), meses, quincenas, string.Format("{0:C}", monto), globales.convertirNumerosLetras(Convert.ToString(monto), true), firma_mc, firma_dir, firma_ui, motivo , logo };

                    request[0] = headers;
                    request[1] = body;
                    globales.reportes("emitirconstasueldo", "detallehistorico", aux2, "", false, request);
                    this.Cursor = Cursors.Default;
                    return;

                }// recibo

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text;
            string query = string.Empty;
            if (txtBusqueda.Text.Contains("..") || txtBusqueda.Text.Contains("."))
            {
                string texto = txtBusqueda.Text.Replace("..", ".");
                string[] split = texto.Split('.');

                string nombre_em = string.Empty;

                foreach (string i in split)
                {
                    if (string.IsNullOrWhiteSpace(i)) continue;
                    nombre_em += $" nombre_em like '%{i}%' ,";
                }
                nombre_em = nombre_em.Substring(0, nombre_em.Length - 1).Replace(",", " and ");
                query = string.Format("select con.rfc,emp.nombre_em,con.num_histo,con.num_const,con.num_recib,con.fecha_exp from datos.constancias as con join datos.empleados as emp on con.rfc=emp.rfc where {0}  order by con.fecha_exp desc limit 20", nombre_em);
            }
            else {
                 query = string.Format("select con.rfc,emp.nombre_em,con.num_histo,con.num_const,con.num_recib,con.fecha_exp from datos.constancias as con join datos.empleados as emp on con.rfc=emp.rfc where emp.rfc like '{0}%' OR emp.nombre_em like '{0}%' or con.num_const like '{0}%' order by con.fecha_exp desc limit 20", busqueda);
            }
            resultado = baseDatos.consulta(query);
            datos.Rows.Clear();
            resultado.ForEach(o => datos.Rows.Add(o["rfc"], o["nombre_em"], o["num_histo"], o["num_const"], o["num_recib"], string.Format("{0:d}", o["fecha_exp"])));
        }
    }
}



