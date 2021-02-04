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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.QUIROGRAFARIO.FONDO_GARANTIA.CAPTURA
{
    public partial class frmFondoGarantia : Form
    {
        private bool insertar = false;
        public frmFondoGarantia()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                frmCatalogoP_quirog p = new frmCatalogoP_quirog();
                p.tablaConsultar = "p_edocta";
                p.enviar2 = rellenarDatos;
                p.ShowDialog();

                string folio = txtfolio.Text;

                if (string.IsNullOrWhiteSpace(folio)) return;


                string query = $"select count(folio) as cantidad from datos.d_fondo where folio = {folio}";
                int cantidad = Convert.ToInt32(globales.consulta(query)[0]["cantidad"]);
                if (cantidad > 0) {
                    MessageBox.Show("Ya existe captura de este folio en la base de datos","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    limpiarCampos();
                    return;
                }

                this.insertar = true;
                this.ActiveControl = txtfechadevolucion;
            }
            catch
            {
                MessageBox.Show("Error, btnnuevo_click", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rellenarDatos(Dictionary<string, object> obj)
        {

            txtfolio.Text = Convert.ToString(obj["folio"]);
            txtrfc.Text = Convert.ToString(obj["rfc"]);
            txtNombre_em.Text = Convert.ToString(obj["nombre_em"]);
            txtF_Solicitud.Text = Convert.ToString(obj["f_solicitud"]);
            txtf_emischeque.Text = Convert.ToString(obj["f_emischeq"]);
            txtImporte.Text = Convert.ToString(obj["importe"]);
            txtdependencia.Text = Convert.ToString(obj["descripcion"]);

            //Código para calcular lo pagado... 

            string query = $"select sum(importe) as pagado,max(f_descuento) as fultimopago from datos.descuentos where t_prestamo = 'Q' and folio = {txtfolio.Text} ";
            List<Dictionary<string, object>> lista = globales.consulta(query);

            if (lista.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(lista[0]["pagado"])))
                {
                    txtpagado.Text = string.Format("{0:C}", lista[0]["pagado"]);
                }

                txtF_ultimopago.Text = Convert.ToString(lista[0]["fultimopago"]);
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Futuras validaciones....
            try
            {




                if (string.IsNullOrWhiteSpace(txtfoliorecibo.Text)) {
                    MessageBox.Show("Favor de insertar folio de recibo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtfoliorecibo.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtfechadevolucion.Text)) {
                    MessageBox.Show("Favor de insertar fecha de devolución", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtfechadevolucion.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtImporteFondo.Text)) {
                    MessageBox.Show("Favor de insertar importe de recibo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtImporteFondo.Focus();
                    return;
                }

                string fecha_dev = string.Format("{0:yyyy-MM-dd}",DateTime.Parse(txtfechadevolucion.Text));
                string query = string.Empty;
                if (this.insertar)
                {
                    query = $"insert into datos.d_fondo values({txtfolio.Text},'{fecha_dev}',null,'{txtfoliorecibo.Text}',{double.Parse(txtImporteFondo.Text, System.Globalization.NumberStyles.Currency)},'{txtleyenda.Text}')";
                    MessageBox.Show("Registro insertado correctamente","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else {
                    query = $"update datos.d_fondo set fecha_dev = '{string.Format("{0:yyyy-MM-dd}",DateTime.Parse(txtfechadevolucion.Text))}',folio_recibo = {txtfoliorecibo.Text}, importe = {txtImporteFondo.Text} , leyenda = '{txtleyenda.Text}' WHERE folio = {txtfolio.Text}";
                    MessageBox.Show("Registro actualizado correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (globales.consulta(query,true)) {
                    limpiarCampos();
                }

            }
            catch {
                MessageBox.Show("Error, Contactar al areá de sistemas","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void limpiarCampos()
        {
            txtfolio.Text = "";
            txtfoliorecibo.Text = "";
            txtfechadevolucion.Text = "";
            txtImporteFondo.Text = "";
            txtleyenda.Text = "";

            txtrfc.Text = "";
            txtF_Solicitud.Text = "";
            txtNombre_em.Text = "";
            txtImporte.Text = "";
            txtf_emischeque.Text = "";
            txtpagado.Text = "";
            txtF_ultimopago.Text = "";
            txtdependencia.Text = "";
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            this.insertar = false;
            frmFondoGarantiaCatalogo catalogo = new frmFondoGarantiaCatalogo();
            catalogo.enviar = rellenarFondo;
            catalogo.ShowDialog();
                
        }

        private void frmFondoGarantia_Load(object sender, EventArgs e)
        {

        }

        private void rellenarFondo(Dictionary<string,object> obj) {
            string folio = Convert.ToString(obj["folio"]);
            string fecha_dev = string.Format("{0:d}",obj["fecha_dev"]);
            string folio_recibo = Convert.ToString(obj["folio_recibo"]);
            string importe = Convert.ToString(obj["importe"]);
            string leyenda = Convert.ToString(obj["leyenda"]);

            txtfolio.Text = folio;
            txtfechadevolucion.Text = fecha_dev;
            txtfoliorecibo.Text = folio_recibo;
            txtImporteFondo.Text = importe;
            txtleyenda.Text = leyenda;

            string query = $"select * from datos.p_edocta WHERE folio = {folio}";
            List<Dictionary<string, object>> r1 = globales.consulta(query);
            if (r1.Count > 0) {
                Dictionary<string, object> dicc = r1[0];
                txtrfc.Text = Convert.ToString(dicc["rfc"]);
                txtF_Solicitud.Text = string.Format("{0:d}", dicc["f_solicitud"]);
                txtNombre_em.Text = Convert.ToString(dicc["nombre_em"]);
                txtImporte.Text = Convert.ToString(dicc["importe"]);
                txtf_emischeque.Text = string.Format("{0:d}",dicc["f_emischeq"]);
                txtdependencia.Text = Convert.ToString(dicc["proyecto"]);
                query = $"select sum(importe) as pagado,max(f_descuento) as fultimopago from datos.descuentos where t_prestamo = 'Q' and folio = {txtfolio.Text} ";
                Dictionary<string, object> res = globales.consulta(query)[0];
                txtpagado.Text = Convert.ToString(res["pagado"]);
                txtF_ultimopago.Text = string.Format("{0:d}",res["fultimopago"]);
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = MessageBox.Show("¿Desea buscar folio del prestamo?","Aviso",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
            if (dialogo == DialogResult.No) return;

            frmFondoGarantiaCatalogo catalogo = new frmFondoGarantiaCatalogo();
            catalogo.enviar = traerFondodegarantia;
            catalogo.ShowDialog();

        }

        private void traerFondodegarantia(Dictionary<string,object> obj) {

            string folio = Convert.ToString(obj["folio"]);
            string letras = globales.convertirNumerosLetras(Convert.ToString(obj["importe"]), true);
            string recibo = Convert.ToString(obj["folio_recibo"]);
            string importeFondo = Convert.ToString(obj["importe"]);
            string leyenda = Convert.ToString(obj["leyenda"]);

            string query = $"select * from datos.p_edocta where folio = {folio}";
            List<Dictionary<string, object>> resultado1 = globales.consulta(query);

            query = $"select sum(importe) as pagado,max(f_descuento) as ultimopago from datos.descuentos where t_prestamo = 'Q' and folio = {folio}";
            List<Dictionary<string, object>> resultado2 = globales.consulta(query);
            string nombre = Convert.ToString(resultado1[0]["nombre_em"]);
            string mrfc = Convert.ToString(resultado1[0]["rfc"]);

            bool esnull = string.IsNullOrWhiteSpace(Convert.ToString(resultado2[0]["ultimopago"]));
            DateTime auxUltPago = DateTime.Now;
            if (!esnull) {
                auxUltPago = DateTime.Parse(Convert.ToString(obj["ultimopago"]));
            }
            string[] arreglo = globales.getMeses();
            string expedicion = $"FECHA DE EXPEDICION DEL RECIBO: {DateTime.Now.Day} del {arreglo[DateTime.Now.Month]} del {DateTime.Now.Year}";
            string ultimopagoletra = $"Del prestamo {((esnull)?"":auxUltPago.Day.ToString())} de {(esnull?"": arreglo[auxUltPago.Month])} del {((esnull)?"": auxUltPago.Year.ToString())}";


            object[][] parametros = new object[2][];
            object[] header = { "recibo", "importe", "letras", "folio", "ultimopagoletra", "nombre_em", "rfc", "expedicion", "leyenda" };
            object[] body = { recibo, importeFondo, letras, folio, ultimopagoletra, nombre,mrfc, ultimopagoletra, leyenda };

            parametros[0] = header;
            parametros[1] = body;

            globales.reportes("reporteFondoGarantia","tasaInteres",new object[] { },expedicion,false,parametros);
        }
    }
}
