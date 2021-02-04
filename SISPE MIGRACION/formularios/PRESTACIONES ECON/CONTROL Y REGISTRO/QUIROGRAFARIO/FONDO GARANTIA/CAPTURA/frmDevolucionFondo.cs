using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.QUIROGRAFARIO.FONDO_GARANTIA.CAPTURA
{
    public partial class frmDevolucionFondo : Form
    {
        public frmDevolucionFondo()
        {
            InitializeComponent();
        }

        private void frmDevolucionFondo_Load(object sender, EventArgs e)
        {
            lbl1.Visible = false;
            using (SISPE_MIGRACION.formularios.CATÁLOGOS.frmCatalogoP_quirog p= new CATÁLOGOS.frmCatalogoP_quirog()) {
                p.tablaConsultar = "p_edocta";
                p.enviar2 = datos;
                p.ShowDialog();
            }
        }

        private void datos(Dictionary<string,object> datos) {
            string folio = Convert.ToString(datos["folio"]);
            string query = $"select * from datos.d_fondo where folio = {folio}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count != 0)
            {
                DialogResult p =
                    globales.MessageBoxQuestion($"ESTA SOLICITUD DE DEVOLUCIÓN DE FONDO DE GARANTÍA YA FUE ELABORADA.\nFECHA DE EMISIÓN DE CHEQUE: {string.Format("{0:d}", resultado[0]["fecha_pago"])}\n¿DESEA IMPRIMIR COPIA DE LA SOLICITUD?","AVISO",globales.menuPrincipal);
                if (p == DialogResult.Yes) {
                    Dictionary<string, object> obj = resultado[0];

                    realizarOperacion(folio);

                }


                Dispose();
            }
            else {
                string rfc = Convert.ToString(datos["rfc"]);
                string nombre_em = Convert.ToString(datos["nombre_em"]);
                string importe = Convert.ToString(datos["importe"]);

                query = $"select * from datos.p_quirog where folio = {folio}";
                List<Dictionary<string, object>> res = globales.consulta(query);
                if (res.Count != 0)
                {
                    string fondo_g = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["fondo_g"])) ? string.Format("{0:C}", 0) : string.Format("{0:C}", res[0]["fondo_g"]);
                    txtFondo.Text = fondo_g;
                }
                else {
                    txtFondo.Text = string.Format("{0:C}",0);
                }


                txtFolio.Text = folio;
                txtRfc.Text = rfc;
                txtNombre.Text = nombre_em;
                txtImporte.Text = string.IsNullOrWhiteSpace(importe) ? string.Format("{0:C}", 0) : string.Format("{0:C}", datos["importe"]);

                query = $"select sum(importe) as pagado from datos.descuentos where folio = {folio}";
                res = globales.consulta(query);
                double pagado = string.IsNullOrWhiteSpace(Convert.ToString(res[0]["pagado"])) ? 0 : Convert.ToDouble(res[0]["pagado"]);
                double dblImporte = double.Parse(txtImporte.Text, System.Globalization.NumberStyles.Currency);

                double saldo = dblImporte - pagado;

                string strSaldo = string.Format("{0:C}", saldo);

                txtSaldo.Text = strSaldo;
                txtPagado.Text = string.Format("{0:C}",pagado);

                double fondo = double.Parse(txtFondo.Text,System.Globalization.NumberStyles.Currency);
                if (fondo == 0)
                {
                    globales.MessageBoxExclamation("El folio solicitado no tiene fondo de garantía, no se seguira con el proceso de devolución de fondo", "Aviso", globales.menuPrincipal);
                    btnAceptar.Visible = false;
                    lbl1.Visible = true;
                }
                else {

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            double saldo = double.Parse(txtSaldo.Text,System.Globalization.NumberStyles.Currency);
            if (saldo > 0)
            {
                globales.MessageBoxExclamation("Falta pagos por cubrir, no se iniciara la solicitud de devolución de fondo de garantía.\nSe debe curbir el total de la deduda", "Aviso", this);
                return;
            }
            else {
                DialogResult p = globales.MessageBoxQuestion("¿Desea seguir con la operación?","Aviso",this);
                if (p == DialogResult.Yes) {
                    this.Cursor = Cursors.WaitCursor;
                    string query = "select max(folio_recibo +1 ) as maximo from datos.d_fondo";
                    List<Dictionary<string, object>> ree = globales.consulta(query);
                    int folioConsecutivo = Convert.ToInt32(ree[0]["maximo"]);
                     query = $"insert into datos.d_fondo values({txtFolio.Text},'{string.Format("{0:yyyy-MM-dd}",DateTime.Now)}','{string.Format("{0:yyyy-MM-dd}", dFecha.Value)}','{folioConsecutivo}',{double.Parse(txtFondo.Text, System.Globalization.NumberStyles.Currency)},'{""}')";

                    if (globales.consulta(query,true)) {
                        globales.MessageBoxSuccess("Registro insertado correctamente", "Aviso", this);
                        realizarOperacion(txtFolio.Text);
                        this.Close();
                    }

                }
            }
        }

        private void realizarOperacion(string folio)
        {
            string query = string.Empty;
            query = $"select * from datos.d_fondo where folio = {folio}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            Dictionary<string, object> obj = resultado[0];

            string letras = globales.convertirNumerosLetras(Convert.ToString(obj["importe"]),true);
            string recibo = Convert.ToString(obj["folio_recibo"]);
            string importeFondo = Convert.ToString(obj["importe"]);
            string leyenda = Convert.ToString(obj["leyenda"]);
            string fechaDevolucion = string.Format("{0:d}",obj["fecha_dev"]);
            string fechaEmisionCheque = string.Format("{0:d}", obj["fecha_pago"]);

            query = $"select * from datos.p_edocta where folio = {folio}";
            List<Dictionary<string, object>> resultado1 = globales.consulta(query);

            query = $"select sum(importe) as pagado,max(f_descuento) as ultimopago from datos.descuentos where t_prestamo = 'Q' and folio = {folio}";
            List<Dictionary<string, object>> resultado2 = globales.consulta(query);
            string nombre = Convert.ToString(resultado1[0]["nombre_em"]);
            string mrfc = Convert.ToString(resultado1[0]["rfc"]);

            bool esnull = string.IsNullOrWhiteSpace(Convert.ToString(resultado2[0]["ultimopago"]));
            DateTime auxUltPago = DateTime.Now;
            if (!esnull)
            {
                auxUltPago = DateTime.Parse(Convert.ToString(resultado2[0]["ultimopago"]));
            }
            string[] arreglo = globales.getMeses();


            DateTime dt1 = DateTime.Now;
            string expedicion = string.Empty;
            try
            {
                 dt1 = DateTime.Parse(fechaDevolucion);
                expedicion = $"FECHA DE EXPEDICIÓN DEL RECIBO: {dt1.Day} de {arreglo[dt1.Month]} deL {dt1.Year}";
            }
            catch {

            }
            string ultimopagoletra = $"{((esnull) ? "" : auxUltPago.Day.ToString())} de {(esnull ? "" : arreglo[auxUltPago.Month])} deL {((esnull) ? "" : auxUltPago.Year.ToString())}";


            
            string strFechaCheque = string.Empty;
            try
            {
                dt1 = DateTime.Parse(fechaEmisionCheque);
                strFechaCheque = string.Format("{0} DE {1} del {2}", dt1.Day, arreglo[dt1.Month], dt1.Year);
            }
            catch {

            }

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[][] parametros = new object[2][];
            object[] header = { "recibo", "importe", "letras", "folio", "ultimopagoletra", "nombre_em", "rfc", "expedicion", "leyenda", "fechacheque", "logo" };
            object[] body = { recibo, importeFondo, $"({letras})", folio, ultimopagoletra, nombre, mrfc, expedicion, leyenda, strFechaCheque, logo };

            parametros[0] = header;
            parametros[1] = body;

            globales.reportes("reporteFondoGarantia", "tasaInteres", new object[] { }, expedicion, false, parametros);
            this.Cursor = Cursors.Default;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
