using SISPE_MIGRACION.codigo.herramientas.forms;
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
    public partial class frmopcionconsta : Form
    {
        private Dictionary<string, object> datos;
        private string rfc;
        private string anios, meses, quincenas, tquincenas, concepto, letra, saldo, nombre_em, nombre_fa, benefi;
        internal metodo1 getDetalle;

        private List<Dictionary<string, object>> listado;
        private double promedio = 0;

        private void frmopcionconsta_Load(object sender, EventArgs e)
        {

        }

        public frmopcionconsta(Dictionary<string, object> resultado, string anios, string meses, string quincenas, string tquincenas, string concepto, string letra, string saldo, string nombre_em)
        {
            InitializeComponent();
            this.datos = resultado;
            string rfc = Convert.ToString(this.datos["rfc"]);
            string nombre = Convert.ToString(this.datos["nombre_em"]);
            this.rfc = rfc;
            this.anios = anios;
            this.meses = meses;
            this.quincenas = quincenas;
            this.tquincenas = tquincenas;
            this.concepto = concepto;
            this.letra = letra;
            this.saldo = saldo;
            this.nombre_em = nombre_em;

         
                listBox1.Items.Add("PROMEDIO");
            

        }

        private void generahistorico()
        {
            // CONTADOR
            string aumenta = "update  datos.control_consta set num_histo=(num_histo+1)"; // aumenta folio 
            globales.consulta(aumenta);
            string actual = "select num_histo from datos.control_consta";
            List<Dictionary<string, object>> item = globales.consulta(actual);
            string num_histo = Convert.ToString(item[0]["num_histo"]);
            // FIRMAS DIR
            string firmas = "SELECT nombre FROM catalogos.firmas where clave='DIR'";
            List<Dictionary<string, object>> item1 = globales.consulta(firmas);
            string firma_dir = Convert.ToString(item1[0]["nombre"]);
            // FIRMAS UI
            string firmas1 = "SELECT nombre FROM catalogos.firmas where clave='DPE'";
            List<Dictionary<string, object>> item2 = globales.consulta(firmas1);
            string firma_ui = Convert.ToString(item2[0]["nombre"]);
            // FIRMAS mc
            string firmas2 = "SELECT nombre FROM catalogos.firmas where clave='MC'";
            List<Dictionary<string, object>> item3 = globales.consulta(firmas2);
            string firma_mc = Convert.ToString(item3[0]["nombre"]);
            // FIRMAS dp
            string firmas3 = "SELECT nombre FROM catalogos.firmas where clave='COOR'";
            List<Dictionary<string, object>> item4 = globales.consulta(firmas3);
            string firma_d_p = Convert.ToString(item4[0]["nombre"]);
            // inserta a tablas
            DateTime fecha = DateTime.Now;
            string solofecha, solohora;
            solofecha = fecha.ToShortDateString();
            solohora = fecha.ToShortTimeString();
            string anio = Convert.ToString(fecha.Year);
            string folioactual = ("H" + num_histo + "/" + anio.Substring(2, 2));
            double monto = double.Parse(saldo, System.Globalization.NumberStyles.Currency);
            string cantidad = Convert.ToString(monto);
            string id_usuario = globales.id_usuario;
            string inserta = "INSERT INTO datos.constancias (rfc,num_histo,fecha_exp,monto_total,total_con_letra,motivo,quincenas,antig_a,antig_m,antig_q,firma_dir,firma_ui,firma_mc,fecha,hora,firma_d_p,id_usuario)" +
           "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')";
            string nuevofolio = string.Format(inserta, this.rfc, folioactual, solofecha, monto, this.letra, this.concepto, this.tquincenas, this.anios, this.meses, this.quincenas, firma_dir, firma_ui, firma_mc, string.Format("{0:yyyy-MM-dd}", fecha), string.Format("{0:hh:mm}", fecha), firma_d_p, id_usuario);
            globales.consulta(nuevofolio, true);

            List<Dictionary<string, object>> detalle = getDetalle();

            foreach (var items in detalle)
            {
                try
                {
                    string inicio = Convert.ToString(items["inicio"]);
                    string final = Convert.ToString(items["final"]);
                    string montos = Convert.ToString(items["monto"]);
                    string detalles = Convert.ToString(items["detalle"]);
                    string truca = string.Format("{0:0.00}", items["monto"]);
                    string insertardetalle = "insert into datos.detalleconstancia (rfc,historico,desde,hasta,monto,descripcion) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')";
                    string paso = string.Format(insertardetalle, this.rfc, folioactual, inicio, final, truca, detalles);
                    globales.consulta(paso);
                }
                catch
                {

                }
            }

            // aqui empieza el reporte 


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

            string bs1 = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[][] request = new object[2][];
            object[] headers = { "numhisto", "rfc", "nombre", "total", "anios", "meses", "quincenas","logo" };
            object[] body = { folioactual, rfc, nombre_em, Convert.ToString(monto), anios, meses, quincenas, bs1 };

            request[0] = headers;
            request[1] = body;
            globales.reportes("historicosispeconstancia", "detallehistorico", aux2, "", false, request);
            this.Cursor = Cursors.Default;

        }


        private void generaconstancia()
        {
            // CONTADOR
            string aumenta = "update  datos.control_consta set num_histo=(num_histo+1), num_const=(num_const+1)"; // aumenta folio historial y constancia
            globales.consulta(aumenta);
            string actual = "select num_histo,num_const from datos.control_consta";
            List<Dictionary<string, object>> item = globales.consulta(actual);
            string num_histo = Convert.ToString(item[0]["num_histo"]);
            string num_const = Convert.ToString(item[0]["num_const"]);


            if (num_const.Length == 1) num_const = "00" + num_const;
            if (num_const.Length == 2) num_const = "0" + num_const;

            // FIRMAS DIR
            string firmas = "SELECT nombre FROM catalogos.firmas where clave='DIR'";
            List<Dictionary<string, object>> item1 = globales.consulta(firmas);
            string firma_dir = Convert.ToString(item1[0]["nombre"]);
            // FIRMAS UI
            string firmas1 = "SELECT nombre FROM catalogos.firmas where clave='DPE'";
            List<Dictionary<string, object>> item2 = globales.consulta(firmas1);
            string firma_ui = Convert.ToString(item2[0]["nombre"]);
            // FIRMAS mc
            string firmas2 = "SELECT nombre FROM catalogos.firmas where clave='MC'";
            List<Dictionary<string, object>> item3 = globales.consulta(firmas2);
            string firma_mc = Convert.ToString(item3[0]["nombre"]);
            // FIRMAS dp
            string firmas3 = "SELECT nombre FROM catalogos.firmas where clave='COOR'";
            List<Dictionary<string, object>> item4 = globales.consulta(firmas3);
            string firma_d_p = Convert.ToString(item4[0]["nombre"]);
            // inserta a tablas
            DateTime fecha = DateTime.Now;
            string solofecha, solohora;
            solofecha = fecha.ToShortDateString();
            solohora = fecha.ToShortTimeString();
            string anio = Convert.ToString(fecha.Year);
            string folioactual = ("H" + num_histo + "/" + anio.Substring(2, 2));
            string folio_const = ("C" + num_const + "/" + anio.Substring(2, 2));
            double monto = double.Parse(saldo, System.Globalization.NumberStyles.Currency);
            string cantidad = Convert.ToString(monto);
            string fec = "select datos.fechaletra('{0}')";
            string pasa = string.Format(fec, solofecha);
            List<Dictionary<string, object>> fch = globales.consulta(pasa);
            string fechaletra = Convert.ToString(fch[0]["fechaletra"]);
            string id_usuario = globales.id_usuario;

            string inserta = "INSERT INTO datos.constancias (rfc,num_histo,num_const,fecha_exp,monto_total,total_con_letra,motivo,quincenas,antig_a,antig_m,antig_q,firma_dir,firma_ui,firma_mc,fecha,hora,firma_d_p,id_usuario)" +
         "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')";
            string nuevofolio = string.Format(inserta, this.rfc, folioactual, folio_const, solofecha, monto, this.letra, this.concepto, this.tquincenas, this.anios, this.meses, this.quincenas, firma_dir, firma_ui, firma_mc, string.Format("{0:yyyy-MM-dd}", fecha), string.Format("{0:hh:mm}", fecha), firma_d_p, id_usuario);
            globales.consulta(nuevofolio, true);

            List<Dictionary<string, object>> detalle = getDetalle();

            foreach (var items in detalle)
            {
                try
                {
                    string inicio = Convert.ToString(items["inicio"]);
                    string final = Convert.ToString(items["final"]);
                    string montos = Convert.ToString(items["monto"]);
                    string detalles = Convert.ToString(items["detalle"]);
                    string truca = string.Format("{0:0.00}", items["monto"]);
                    string insertardetalle = "insert into datos.detalleconstancia (rfc,historico,desde,hasta,monto,descripcion) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')";
                    string paso = string.Format(insertardetalle, this.rfc, folioactual, inicio, final, truca, detalles);
                    globales.consulta(paso);
                }
                catch
                {

                }
            }

            // aqui empieza el reporte 
           

            string querydetalle = "SELECT desde,hasta,monto,descripcion FROM datos.detalleconstancia where historico='{0}'";
            string pasodetalle = string.Format(querydetalle, folioactual);

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

            string bs1 = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[][] request = new object[2][];
            object[] headers = { "folio", "fecha", "rfc", "nombre", "anios", "meses", "quincenas", "totalnum", "totalletra", "firma_mc", "firma_dir", "firma_ui", "motivo" ,"logo"};
            object[] body = { folio_const, fechaletra, rfc, nombre_em, anios, meses, quincenas, Convert.ToString(monto), letra, firma_mc, firma_dir, firma_ui, concepto, bs1 };

            request[0] = headers;
            request[1] = body;
            globales.reportes("emitirconstancias", "detallehistorico", aux2, "", false, request);
            this.Cursor = Cursors.Default;
            return;

        }

        private void generarconstanciayrecio()
        {
            // CONTADOR
            string aumenta = "update  datos.control_consta set num_histo=(num_histo+1), num_const=(num_const+1), num_recib=(num_recib+1)"; // aumenta folio historial y constancia
            globales.consulta(aumenta);
            string actual = "select num_histo,num_const,num_recib from datos.control_consta";
            List<Dictionary<string, object>> item = globales.consulta(actual);
            string num_histo = Convert.ToString(item[0]["num_histo"]);
            string num_const = Convert.ToString(item[0]["num_const"]);
            string num_recib = Convert.ToString(item[0]["num_recib"]);
            if (num_recib.Length == 1) num_recib = "00" + num_recib;
            if (num_recib.Length == 2) num_recib = "0" + num_recib;
            

            if (num_const.Length == 1) num_const = "00" + num_const;
            if (num_const.Length == 2) num_const = "0" + num_const;
            


            // FIRMAS DIR
            string firmas = "SELECT nombre FROM catalogos.firmas where clave='DIR'";
            List<Dictionary<string, object>> item1 = globales.consulta(firmas);
            string firma_dir = Convert.ToString(item1[0]["nombre"]);
            // FIRMAS UI
            string firmas1 = "SELECT nombre FROM catalogos.firmas where clave='DPE'";
            List<Dictionary<string, object>> item2 = globales.consulta(firmas1);
            string firma_ui = Convert.ToString(item2[0]["nombre"]);
            // FIRMAS mc
            string firmas2 = "SELECT nombre FROM catalogos.firmas where clave='MC'";
            List<Dictionary<string, object>> item3 = globales.consulta(firmas2);
            string firma_mc = Convert.ToString(item3[0]["nombre"]);
            // FIRMAS dp
            string firmas3 = "SELECT nombre  FROM catalogos.firmas where clave='COOR'";
            List<Dictionary<string, object>> item4 = globales.consulta(firmas3);
            string firma_d_p = Convert.ToString(item4[0]["nombre"]);
            // inserta a tablas
            DateTime fecha = DateTime.Now;
            string solofecha, solohora;
            solofecha = fecha.ToShortDateString();
            solohora = fecha.ToShortTimeString();
            string anio = Convert.ToString(fecha.Year);
            string folioactual = ("H" + num_histo + "/" + anio.Substring(2, 2));
            string folio_const = ("C" + num_const + "/" + anio.Substring(2, 2));
            string folionum_recib = ("R" + num_recib + "/" + anio.Substring(2, 2));
            double monto = double.Parse(saldo, System.Globalization.NumberStyles.Currency);
            string cantidad = Convert.ToString(monto);
            string fec = "select datos.fechaletra('{0}')";
            string pasa = string.Format(fec, solofecha);
            List<Dictionary<string, object>> fch = globales.consulta(pasa);
            string fechaletra = Convert.ToString(fch[0]["fechaletra"]);
            string id_usuario = globales.id_usuario;


            string inserta = "INSERT INTO datos.constancias (rfc,num_histo,num_const,num_recib,fecha_exp,monto_total,total_con_letra,motivo,quincenas,antig_a,antig_m,antig_q,firma_dir,firma_ui,firma_mc,fecha,hora,firma_d_p,id_usuario)" +
         "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')";
            string nuevofolio = string.Format(inserta, this.rfc, folioactual, folio_const, folionum_recib, solofecha, monto, this.letra, this.concepto, this.tquincenas, this.anios, this.meses, this.quincenas, firma_dir, firma_ui, firma_mc, string.Format("{0:yyyy-MM-dd}", fecha), string.Format("{0:hh:mm}", fecha), firma_d_p, id_usuario);
            globales.consulta(nuevofolio, true);

            List<Dictionary<string, object>> detalle = getDetalle();

            foreach (var items in detalle)
            {
                try
                {
                    string inicio = Convert.ToString(items["inicio"]);
                    string final = Convert.ToString(items["final"]);
                    string montos = Convert.ToString(items["monto"]);
                    string detalles = Convert.ToString(items["detalle"]);
                    string truca = string.Format("{0:0.00}", items["monto"]);
                    string insertardetalle = "insert into datos.detalleconstancia (rfc,historico,desde,hasta,monto,descripcion) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')";
                    string paso = string.Format(insertardetalle, this.rfc, folioactual, inicio, final, truca, detalles);
                    globales.consulta(paso);
                }
                catch
                {

                }
            }


            //ImprementacionNueva
            metodoBuscar(folioactual);

        }

        private void metodoBuscar(string num_histo)
        {
            string query = "select a1.rfc,a1.num_histo,a2.nombre_em,a1.num_const,a1.num_recib,a1.monto_total,a1.antig_a,a1.antig_m,a1.antig_q,a1.fecha_exp,a1.total_con_letra,a1.motivo,a1.firma_dir,a1.firma_mc,a1.firma_ui,a1.firma_d_p from datos.constancias a1 JOIN datos.empleados a2 ON a1.rfc=a2.rfc where a1.num_histo='{0}'";
            query = string.Format(query, num_histo);
            List<Dictionary<string, object>> dic = globales.consulta(query);

            foreach (var item in dic)
            {
                string folio_const = Convert.ToString(item["num_const"]);
                string folionum_recib = Convert.ToString(item["num_recib"]);
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
                    nombre2 = f.txtFallecido.Text;
                }

                string base2 = globales.getImagen(globales.imagenesSispe.logoreportes);

                object[][] request = new object[2][];
                object[] headers = { "folio", "fecha", "rfc", "nombre", "anios", "meses", "quincenas", "totalnum", "totalletra", "firma_mc", "firma_dir", "firma_ui", "motivo", "firma_d_p", "contenido", "num_recib", "monto1", "monto2", "monto3", "total", "concepto1", "concepto2", "concepto3", "fallecido", "nombre2","logo" };
                object[] body = { folio_const, fechaletra, rfc, nombre1, anios, meses, quincenas, Convert.ToString(monto), letra, firma_mc, firma_dir, firma_ui, motivo, firma_d_p, contenido, folionum_recib, monto1, monto2, monto3, total, concepto1, concepto2, concepto3, fallecido, nombre2, base2 };

                request[0] = headers;
                request[1] = body;
                globales.reportes("constayrecibo", "detallehistorico", aux2, "", false, request);
                this.Cursor = Cursors.Default;
                return;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void opcionesconsta()
        {
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    generahistorico();  // HISTORICO
                    this.Close();
                    break;
                case 1:
                    generaconstancia();
                    this.Close();
                    break;
                case 2:
                    generarconstanciayrecio();
                    this.Close();
                    break;
                case 3:
                    constanciasueldo();
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) opcionesconsta();
        }
        private List<Dictionary<string, object>> promedio_saldo()
        {



            double sueldo = 0;
            ingresarFecha f = new ingresarFecha();
            globales.showModal(f);
            if (!f.aceptar) return null;

            string fecha = f.fecha;

            string quey1 = ("select inicio, final, new_tipo, entrada, salida, cuenta, movimiento,fecharegistro, id" +
            " FROM datos.aportaciones WHERE rfc = '{0}' AND COALESCE (TRIM(status),'')<>'e' AND COALESCE (TRIM(status),'')<>'p' AND FINAL <='{1}'" +
            "ORDER BY inicio desc,new_tipo; ");
            string rfc1 = this.rfc;

            string convierte1 = string.Format(quey1, rfc1, fecha);
            List<Dictionary<string, object>> resultado1 = globales.consulta(convierte1);
            int CONTADOR = 0;
            double PILA = 0;

            List<Dictionary<string, object>> listado = new List<Dictionary<string, object>>();

            foreach (var item in resultado1)
            {
                string inicio = Convert.ToString(item["inicio"]).Replace("12:00:00 a. m.", "");
                string final = Convert.ToString(item["final"]).Replace("12:00:00 a. m.", "");
                string new_tipo = Convert.ToString(item["new_tipo"]);
                string entrada = Convert.ToString(item["entrada"]);
                string salida = Convert.ToString(item["salida"]);
                string cuenta = Convert.ToString(item["cuenta"]);
                string movimiento = Convert.ToString(item["movimiento"]);
                string id = Convert.ToString(item["id"]);

                if ((new_tipo == "AN" || new_tipo == "AR") && CONTADOR < 22)

                {
                    if (new_tipo == "AN") CONTADOR++;
                    sueldo = (Convert.ToDouble(entrada) / 9) * 100;
                    item.Add("sueldo", sueldo);
                    listado.Add(item);
                }
            }
            return listado;
        }

        private void constanciasueldo()
        {

            this.listado = promedio_saldo();

            double suma = listado.Sum(o => Convert.ToDouble(o["sueldo"]));
            this.promedio = Math.Round((suma / 12));
            // CONTADOR
            string aumenta = "update  datos.control_consta set num_histo=(num_histo+1), num_constpro=(num_constpro+1)"; // aumenta folio historial y constancia
            globales.consulta(aumenta);
            string actual = "select num_histo,num_constpro from datos.control_consta";
            List<Dictionary<string, object>> item = globales.consulta(actual);
            string num_histo = Convert.ToString(item[0]["num_histo"]);
            string num_constpro = Convert.ToString(item[0]["num_constpro"]);

            // FIRMAS DIR
            string firmas = "SELECT nombre FROM catalogos.firmas where clave='DIR'";
            List<Dictionary<string, object>> item1 = globales.consulta(firmas);
            string firma_dir = Convert.ToString(item1[0]["nombre"]);
            // FIRMAS UI
            string firmas1 = "SELECT nombre FROM catalogos.firmas where clave='DPE'";
            List<Dictionary<string, object>> item2 = globales.consulta(firmas1);
            string firma_ui = Convert.ToString(item2[0]["nombre"]);
            // FIRMAS mc
            string firmas2 = "SELECT nombre FROM catalogos.firmas where clave='MC'";
            List<Dictionary<string, object>> item3 = globales.consulta(firmas2);
            string firma_mc = Convert.ToString(item3[0]["nombre"]);
            // FIRMAS dp
            string firmas3 = "SELECT nombre FROM catalogos.firmas where clave='COOR'";
            List<Dictionary<string, object>> item4 = globales.consulta(firmas3);
            string firma_d_p = Convert.ToString(item4[0]["nombre"]);
            // inserta a tablas
            DateTime fecha = DateTime.Now;
            string solofecha, solohora;
            solofecha = fecha.ToShortDateString();
            solohora = fecha.ToShortTimeString();
            string anio = Convert.ToString(fecha.Year);
            string folioactual = ("H" + num_histo + "/" + anio.Substring(2, 2));
            string folio_const = ("CS" + num_constpro + "/" + anio.Substring(2, 2));
            double monto = double.Parse(saldo, System.Globalization.NumberStyles.Currency);
            string cantidad = Convert.ToString(monto);
            string fec = "select datos.fechaletra('{0}')";
            string pasa = string.Format(fec, solofecha);
            List<Dictionary<string, object>> fch = globales.consulta(pasa);
            string fechaletra = Convert.ToString(fch[0]["fechaletra"]);
            string id_usuario = globales.id_usuario;

            string inserta = "INSERT INTO datos.constancias (rfc,num_histo,num_constpro,fecha_exp,monto_total,total_con_letra,motivo,quincenas,antig_a,antig_m,antig_q,firma_dir,firma_ui,firma_mc,fecha,hora,firma_d_p,id_usuario)" +
         "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')";
            string nuevofolio = string.Format(inserta, this.rfc, folioactual, folio_const, solofecha, promedio, this.letra, this.concepto, this.tquincenas, this.anios, this.meses, this.quincenas, firma_dir, firma_ui, firma_mc, string.Format("{0:yyyy-MM-dd}", fecha), string.Format("{0:hh:mm}", fecha), firma_d_p, id_usuario);
            globales.consulta(nuevofolio, true);


            foreach (var items in listado)
            {
                try
                {
                    string inicio = Convert.ToString(items["inicio"]);
                    string final = Convert.ToString(items["final"]);
                    string montos = Convert.ToString(items["entrada"]);
                    string detalles = Convert.ToString(items["movimiento"]);
                   string truca = string.Format("{0:0.00}", items["entrada"]);
                    double sueldo = Convert.ToDouble(items["sueldo"]);
                    sueldo = Math.Round(sueldo);
                    string new_tipo = Convert.ToString(items["new_tipo"]);
                    string insertardetalle = "insert into datos.detalleconstancia (rfc,historico,desde,hasta,monto,descripcion,sueldo,new_tipo) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
                    string paso = string.Format(insertardetalle, this.rfc, folioactual, inicio, final, truca, detalles, sueldo, new_tipo);
                    globales.consulta(paso);
                }
                catch
                {

                }
            }
                // aqui empieza el reporte 


                string querydetalle = "SELECT desde,hasta,monto,descripcion,sueldo,new_tipo FROM datos.detalleconstancia where historico='{0}'";
                string pasodetalle = string.Format(querydetalle, folioactual);

                List<Dictionary<string, object>> res = globales.consulta(pasodetalle);
                object[] aux2 = new object[28];

            suma = 0;
            int orden = 0;
                for (int x = 0; x < 28; x++)
                {
                    object[] tt1 = { "", "", "", "","","","",""};
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
                        if (descrip=="AN")
                        {
                            orden++;
                            tt1[6] = orden;

                        }
                        tt1[0] = desde;
                            tt1[1] = hasta;
                            tt1[2] = string.IsNullOrWhiteSpace(montodetalle)?"0.00":string.Format("{0:C}",double.Parse(montodetalle,System.Globalization.NumberStyles.Currency)).Replace("$","");
                            tt1[3] = descrip;
                           tt1[7] = string.IsNullOrWhiteSpace(sueldo)?"": string.Format("{0:C}", double.Parse(sueldo,System.Globalization.NumberStyles.Currency)).Replace("$", "");

                        suma += string.IsNullOrWhiteSpace(sueldo) ? 0 :  Convert.ToDouble(sueldo);
                    }
                    catch
                        {

                        }
                    }
                    aux2[x] = tt1;
                }
            string base2 = globales.getImagen(globales.imagenesSispe.logoreportes);
                object[][] request = new object[2][];
                object[] headers = { "folio", "fecha", "rfc", "nombre", "anios", "meses", "quincenas", "totalnum", "totalletra", "firma_mc", "firma_dir", "firma_ui", "motivo","logo" };
                object[] body = { folio_const, fechaletra, rfc, nombre_em, string.Format("{0:C}", suma), meses, quincenas, string.Format("{0:C}", promedio), globales.convertirNumerosLetras(Convert.ToString (promedio),true), firma_mc, firma_dir, firma_ui, concepto, base2 };

                request[0] = headers;
                request[1] = body;
                globales.reportes("emitirconstasueldo", "detallehistorico", aux2, "", false, request);
                this.Cursor = Cursors.Default;
                return;

            }
    }
}
