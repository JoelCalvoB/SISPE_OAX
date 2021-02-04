using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.REPORTES
{
    public partial class frmSobres : Form
    {
        private string proyecto, nombre, curp, rfc, imss, categ, clave, descri, monto, fecha, periodo, directorio, archivocompara, leyen;

        private double montohistorial;
        private Dictionary<string, string> meses;

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbEnero_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            int row = e.RowIndex;
            int colum = e.ColumnIndex;

            txtrfc.Text = Convert.ToString(dataGridView.Rows[row].Cells[3].Value);
            txtnombre.Text = Convert.ToString(dataGridView.Rows[row].Cells[2].Value);
            txtTipo.Text = Convert.ToString(dataGridView.Rows[row].Cells[0].Value);
            txtnum.Text = Convert.ToString(dataGridView.Rows[row].Cells[1].Value);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dataGridView.Focus();
            }

            if (e.KeyCode == Keys.Enter)
            {
                pictureBox1_Click(null, null);
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pictureBox1_Click(null, null);
                e.SuppressKeyPress = true;
            }
        }

        private void dataGridView_Click(object sender, EventArgs e)
        {

        }

        private void txtAnio_TextChanged(object sender, EventArgs e)
        {

        }

        public frmSobres()
        {
            InitializeComponent();
        }

        private void frmSobres_Load(object sender, EventArgs e)
        {
            llena_formulario();

            this.directorio = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\pdfjubilados";

            //Se crea los directorios necesarios}

            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }
        }

        private void llena_formulario()
        {
            comboBox1.SelectedIndex = 0;
            DateTime fecha_actual = DateTime.Now;
            int mes = fecha_actual.Month;
            int anio = fecha_actual.Year;
            switch (mes)
            {
                case 1:
                    rbEnero.Checked = true;
                    break;
                case 2:
                    rbFebrero.Checked = true;
                    break;
                case 3:
                    rbMarzo.Checked = true;
                    break;
                case 4:
                    rbAbril.Checked = true;
                    break;
                case 5:
                    rbMayo.Checked = true;
                    break;
                case 6:
                    rbJunio.Checked = true;
                    break;
                case 7:
                    rbJulio.Checked = true;
                    break;
                case 8:
                    rbAgosto.Checked = true;
                    break;
                case 9:
                    rbSeptiembre.Checked = true;
                    break;
                case 10:
                    rbOctubre.Checked = true;
                    break;
                case 11:
                    rbNoviembre.Checked = true;
                    break;
                case 12:
                    rbDiciembre.Checked = true;

                    break;
                default:
                    break;



            }

            txtAnio.Text = Convert.ToString(anio);

            string query = "SELECT jpp,num,rfc,nombre FROM nominas_catalogos.maestro where superviven='S' order by jpp,num LIMIT 100;";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            resultado.ForEach(o => dataGridView.Rows.Add(o["jpp"], o["num"], o["rfc"], o["nombre"]));



        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            //DataGridViewRow row = dataGridView.Rows[c];
            //txtTipo.Text = Convert.ToString(row.Cells[0].Value);
            //txtnum.Text = Convert.ToString(row.Cells[1].Value);
            //txtnombre.Text = Convert.ToString(row.Cells[2].Value);
            //txtrfc.Text = Convert.ToString(row.Cells[3].Value);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();
            string query = $"SELECT jpp,num,rfc,nombre FROM nominas_catalogos.maestro where superviven='S' AND (concat(jpp,num) like '%{textBox1.Text}%' OR RFC LIKE '%{textBox1.Text}%' or nombre like '%{textBox1.Text}%') order by jpp,num LIMIT 100;";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            resultado.ForEach(o => dataGridView.Rows.Add(o["jpp"], o["num"], o["rfc"], o["nombre"]));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = $"SELECT f_impresion,correo FROM nominas_catalogos.maestro where jpp='{txtTipo.Text}' and num={txtnum.Text};";
                List<Dictionary<string, object>> resulta = globales.consulta(query);
                string correo = Convert.ToString(resulta[0]["correo"]);
                DateTime fecha = DateTime.Now;
                Int32 mes = Convert.ToInt32(fecha.Month);
                DateTime f_impresion = Convert.ToDateTime(resulta[0]["f_impresion"]);
                Int32 mesf_impresion = Convert.ToInt32(f_impresion.Month);
                if (!string.IsNullOrWhiteSpace(correo) || mes == mesf_impresion)
                {

                    DialogResult dialo = globales.MessageBoxExclamation("YA SE IMPRIMIÓ O SE ENVIO ESTE SOBRE", "AVISO", globales.menuPrincipal);
                }
            }
            catch
            {

            }

            if (radioButton2.Checked == true)
            {
                if (!string.IsNullOrWhiteSpace(txtTipo.Text) && !string.IsNullOrWhiteSpace(txtnum.Text))
                {
                    sacarReporte();

                }

            }

            if (radioButton3.Checked == true)
            {
                if (!string.IsNullOrWhiteSpace(txtTipo.Text) && !string.IsNullOrWhiteSpace(txtnum.Text))
                {
                    sacarHistorial();

                }

            }
        }


        private void sacarReporte()
        {
            string mesRadio = "";
            string periodo = "";
            string fechaSobres = "";
            DateTime fecha = new DateTime(globales.convertInt(txtAnio.Text), 1, 1);
            if (rbEnero.Checked == true)
                mesRadio = "01";
            periodo = "01 al 31 de Enero del " + txtAnio.Text;
            fechaSobres = "31/12/" + ((Convert.ToString(fecha.Year - 1).Substring(2, 2)));
            if (rbFebrero.Checked == true)
            {
                mesRadio = "02";
                periodo = "01 al 28 de Febrero del " + txtAnio.Text;
                fechaSobres = "28/01/" + txtAnio.Text.Substring(2, 2);
            }
            if (rbMarzo.Checked == true)
            {
                mesRadio = "03";
                periodo = "01 al 31 de Marzo del " + txtAnio.Text;
                fechaSobres = "31/02/" + txtAnio.Text.Substring(2, 2);

            }
            if (rbAbril.Checked == true)
            {
                mesRadio = "04";
                periodo = "01 al 30 de Abril del " + txtAnio.Text;
                fechaSobres = "30/03/" + txtAnio.Text.Substring(2, 2);

            }
            if (rbMayo.Checked == true)
            {
                mesRadio = "05";
                periodo = "01 al 31 de Mayo del " + txtAnio.Text;
                fechaSobres = "31/04/" + txtAnio.Text.Substring(2, 2);

            }
            if (rbJunio.Checked == true)
            {
                mesRadio = "06";
                periodo = "01 al 30 de Junio del " + txtAnio.Text;
                fechaSobres = "30/05/" + txtAnio.Text.Substring(2, 2);

            }
            if (rbJulio.Checked == true)
            {
                mesRadio = "07";
                periodo = "01 al 31 de Julio del " + txtAnio.Text;
                fechaSobres = "31/06/" + txtAnio.Text.Substring(2, 2);

            }
            if (rbAgosto.Checked == true)
            {
                mesRadio = "08";
                periodo = "01 al 31 de Agosto del " + txtAnio.Text;
                fechaSobres = "31/07/" + txtAnio.Text.Substring(2, 2);

            }
            if (rbSeptiembre.Checked == true)
            {
                mesRadio = "09";
                periodo = "01 al 30 de Septiembre del " + txtAnio.Text;
                fechaSobres = "30/08/" + txtAnio.Text.Substring(2, 2);

            }
            if (rbOctubre.Checked == true)
            {
                mesRadio = "10";
                periodo = "01 al 31 de Octubre del " + txtAnio.Text;
                fechaSobres = "31/09/" + txtAnio.Text.Substring(2, 2);

            }
            if (rbNoviembre.Checked == true)
            {
                mesRadio = "11";
                periodo = "01 al 30 de Noviembre del " + txtAnio.Text;
                fechaSobres = "30/10/" + txtAnio.Text.Substring(2, 2);

            }
            if (rbDiciembre.Checked == true)
            {
                mesRadio = "12";
                periodo = "01 al 31 de Diciembre del " + txtAnio.Text;
                fechaSobres = "31/11/" + txtAnio.Text.Substring(2, 2);

            }

            fecha = new DateTime(globales.convertInt(txtAnio.Text), Convert.ToInt32(mesRadio), 1);
            fecha = fecha.AddDays(-1);
            fechaSobres = string.Format("{0:d}", fecha);



            string tipo_nomina = "";
            string parametro = string.Empty;
            parametro = txtTipo.Text + txtnum.Text;

            if (comboBox1.SelectedIndex == 0) tipo_nomina = "N";  // normal      
            if (comboBox1.SelectedIndex == 1) tipo_nomina = "AG";   // 
            if (comboBox1.SelectedIndex == 2) tipo_nomina = "CA"; //  
            if (comboBox1.SelectedIndex == 3) tipo_nomina = "DM";    // 
            if (comboBox1.SelectedIndex == 4) tipo_nomina = "UT";    // 
            if (comboBox1.SelectedIndex == 5) tipo_nomina = "RT";    // 



            string compuesto = txtAnio.Text.Substring(2, 2) + Convert.ToString(mesRadio);
            string query = $"CREATE TEMP TABLE t1 AS SELECT  a1.jpp,  a1.num,	a1.proyecto,	a1.nombre,	a1.curp,	a1.rfc,	a1.imss,	a1.categ FROM " +
                $" nominas_catalogos.maestro a1 WHERE 	a1.superviven = 'S' AND concat (a1.jpp, a1.num) = '{parametro}'; " +
                $" CREATE TEMP TABLE t2 AS SELECT	a2.numjpp,a2.leyen,a2.tipo_pago,	a2.jpp,	a2.clave,	a2.descri,	a2.monto,	a2.archivo,	a2.pago4,	a2.pagot " +
                $" FROM	nominas_catalogos.respaldos_nominas a2 WHERE	concat (a2.jpp, a2.numjpp) = '{parametro}'AND a2.archivo = '{compuesto}' " +
                $" AND a2.tipo_nomina = '{tipo_nomina}'; select t1.proyecto,	t1.nombre,	t1.curp,	t1.rfc,	t1.imss,	t1.categ,	t2.clave,	t2.descri,	t2.monto,	t2.archivo,	t2.pago4,	t2.pagot ,t2.leyen from t1  inner join t2  on t1.num = t2.numjpp order by t2.clave,t2.tipo_pago";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0)
            {
                DialogResult dialogo2 = globales.MessageBoxExclamation("NO SE ENCUENTRA INFORMACIÓN DE ESTE JUBILADO EN EL MES SELECCIONADO", "VERIFICAR", globales.menuPrincipal);
                return;
            }

            query = "select  clave,descri from nominas_catalogos.perded order by clave";
            List<Dictionary<string, object>> perded = globales.consulta(query);
            resultado.ForEach(o =>
            {
                o["descri"] = perded.Where(p => Convert.ToString(o["clave"]) == Convert.ToString(p["clave"])).First()["descri"];
                //  o["descri"] += " (RETROACTIVO)"; eeee
            });


            object[] aux2 = new object[resultado.Count];
            int contadorPercepcion = 0;
            int contadorDeduccion = 0;
            foreach (var item in resultado)
            {
                proyecto = string.Empty;
                nombre = string.Empty;
                curp = string.Empty;
                rfc = string.Empty;
                imss = string.Empty;
                categ = string.Empty;
                clave = string.Empty;
                descri = string.Empty;
                monto = string.Empty;
                //  fecha = fec2.ToString();

                int año = 0;
                int mes = 0;
                string pago4 = string.Empty;
                string pagot = string.Empty;
                try
                {

                    proyecto = Convert.ToString(item["proyecto"]);
                    leyen = Convert.ToString(item["leyen"]);
                    nombre = Convert.ToString(item["nombre"]);
                    //nombre = "MUJICA VILLEGAS ARACELI";
                    curp = Convert.ToString(item["curp"]);
                    rfc = Convert.ToString(item["rfc"]);
                    imss = Convert.ToString(item["imss"]);
                    categ = Convert.ToString(item["categ"]);
                    clave = Convert.ToString(item["clave"]);
                    descri = Convert.ToString(item["descri"]) + (string.IsNullOrWhiteSpace(leyen) ? "" : $"({leyen})");
                    monto = string.Format("{0:C}", Convert.ToDouble(item["monto"])).Replace("$", "");



                    pago4 = Convert.ToString(item["pago4"]);
                    pagot = Convert.ToString(item["pagot"]);

                }
                catch
                {

                }
                object[] tt1 = { "", "", "", "", "", "", "", "", "", "", "", "", "" };
                if (Convert.ToInt32(clave) < 60)
                {
                    if (aux2[contadorPercepcion] == null)
                    {
                        tt1[6] = clave;
                        tt1[7] = descri;
                        tt1[8] = monto;
                        aux2[contadorPercepcion] = tt1;
                    }
                    else
                    {
                        object[] tmp = (object[])aux2[contadorPercepcion];
                        tmp[6] = clave;
                        tmp[7] = descri;
                        tmp[8] = monto;
                    }
                    contadorPercepcion++;
                }
                else
                {

                    if (aux2[contadorDeduccion] == null)
                    {
                        tt1[9] = clave;
                        tt1[10] = descri;
                        tt1[12] = (string.IsNullOrWhiteSpace(pago4) || pago4 == "0") ? "" : $"{pago4}/{pagot}";
                        tt1[11] = monto;
                        aux2[contadorDeduccion] = tt1;
                    }
                    else
                    {
                        object[] tmp = (object[])aux2[contadorDeduccion];
                        tmp[9] = clave;
                        tmp[10] = descri;
                        tmp[12] = (string.IsNullOrWhiteSpace(pago4) || pago4 == "0") ? "" : $"{pago4}/{pagot}";
                        tmp[11] = monto;
                    }
                    contadorDeduccion++;
                }
            }

            //Restablece los objetos para evitar el break del reporteador

            int contadorPrincipal = 0;
            try
            {
                while (aux2[contadorPrincipal] != null)
                    contadorPrincipal++;
            }
            catch
            {

            }

            object[] objeto = new object[16];
            for (int x = 0; x < 16; x++)
            {
                object[] tt1 = { "", "", "", "", "", "", " ", "", "", " ", "", "", "" };
                objeto[x] = tt1;
            }
            double sumaPercepciones = 0;
            double sumaDeducciones = 0;


            aux2.Sum(o =>
            {
                object[] a = (object[])o;
                sumaDeducciones += o == null ? 0 : globales.convertDouble(Convert.ToString(a[11]));
                sumaPercepciones += o == null ? 0 : globales.convertDouble(Convert.ToString(a[8]));
                return 0;
            });


            for (int x = 0; x < contadorPrincipal; x++)
            {
                if (x == 16)
                {
                    System.Diagnostics.Debug.WriteLine(proyecto + " " + nombre + " " + rfc);
                    break;
                }
                objeto[x] = aux2[x];
                object[] sacarDato = (object[])aux2[x];
                //  double percepcion = string.IsNullOrWhiteSpace(Convert.ToString(sacarDato[8])) ? 0 : Convert.ToDouble(sacarDato[8]);
                // double deduccion = string.IsNullOrWhiteSpace(Convert.ToString(sacarDato[11])) ? 0 : Convert.ToDouble(sacarDato[11]);
                //sumaPercepciones += percepcion;
                //sumaDeducciones += deduccion;

            }

            //object[] parametros = { "proyecto", "nombre", "curp", "rfc", "imss", "categ", "fechapago", "periodo", "sumaPercepcion", "sumaDeduccion" };
            //object[] valor = { proyecto, nombre, curp, rfc, imss, categ, , , sumaPercepciones.ToString(), sumaDeducciones.ToString() };



            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData data = qr.CreateQrCode(proyecto, QRCodeGenerator.ECCLevel.Q);
            QRCode code = new QRCode(data);
            //Image img = code.GetGraphic(2);
            Bitmap bImage = code.GetGraphic(2); // Your Bitmap Image
            System.IO.MemoryStream ms = new MemoryStream();
            bImage.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var SigBase64 = Convert.ToBase64String(byteImage);


            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[] parametros = { "periodo", "fechapago", "proyecto", "nombre", "curp", "rfc", "imss", "categ", "sumaPercepcion", "sumaDeduccion", "qr" , "logo" };
            object[] valor = { periodo, string.Format("{0:d}", fechaSobres), proyecto, nombre, curp, rfc, imss, categ, sumaPercepciones.ToString(), sumaDeducciones.ToString(), SigBase64 , logo }; ;

            object[][] enviarParametros = new object[2][];   //joe

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reporte_sobrespago_nominas", "sobres", objeto, "", true, enviarParametros, true, "RECIBO");
            query = $"update  nominas_catalogos.maestro set f_impresion=CURRENT_DATE where jpp='{txtTipo.Text}' and num={txtnum.Text};";
            globales.consulta(query);
            // DialogResult dialogo = globales.MessageBoxSuccess("EN LA RUTA C:-Users-TU-USUARIO-pdfjubilados se guarda el PDF", "REVISAR CARPETA", globales.menuPrincipal);
            string pdfPath = Path.Combine(Application.StartupPath, directorio + "\\RECIBO.pdf");


            Process.Start(pdfPath);


        }
        //   foreach (Control ctrl in groupBox1.Controls)


        private void sacarHistorial()
        {
            string mesRadio = "";


            if (rbEnero.Checked == true)
                mesRadio = "01";

            if (rbFebrero.Checked == true)
            {
                mesRadio = "02";

            }
            if (rbMarzo.Checked == true)
            {
                mesRadio = "03";


            }
            if (rbAbril.Checked == true)
            {
                mesRadio = "04";


            }
            if (rbMayo.Checked == true)
            {
                mesRadio = "05";


            }
            if (rbJunio.Checked == true)
            {
                mesRadio = "06";


            }
            if (rbJulio.Checked == true)
            {
                mesRadio = "07";


            }
            if (rbAgosto.Checked == true)
            {
                mesRadio = "08";


            }
            if (rbSeptiembre.Checked == true)
            {
                mesRadio = "09";


            }
            if (rbOctubre.Checked == true)
            {
                mesRadio = "10";


            }
            if (rbNoviembre.Checked == true)
            {
                mesRadio = "11";


            }
            if (rbDiciembre.Checked == true)
            {
                mesRadio = "12";


            }

            string titulo = string.Empty;


            string tipo_nom = "";
            string parametro = txtTipo.Text + txtnum.Text;
            if (comboBox1.SelectedIndex == 0)
            {
                tipo_nom = "N";
            }
            if (comboBox1.SelectedIndex == 5)
            {
                tipo_nom = "RT";
            }


            string compuesto = txtAnio.Text.Substring(2, 2) + Convert.ToString(mesRadio);
            //   string query = $"select	a1.proyecto,a1.nombre,a1.curp,a1.rfc,a1.imss,a1.categ,a2.clave,a2.descri,a2.monto,a2.archivo,a2.pago4,a2.pagot,a2.tipo_pago  FROM	nominas_catalogos.maestro a1 JOIN nominas_catalogos.respaldos_nominas a2  ON a1.num = a2.numjpp and a1.jpp = a2.jpp and  a2.archivo >= '{compuesto}'  AND a2.tipo_nomina IN ('{tipo_nom}') and a2.numjpp = {txtnum.Text} and a1.jpp = '{txtTipo.Text}' ORDER BY a2.archivo,clave";
            string query = $"select	a1.proyecto,a1.nombre,a1.curp,a1.rfc,a1.imss,a1.categ,a2.clave,a2.descri,a2.monto,a2.archivo,a2.pago4,a2.pagot,a2.tipo_pago  FROM	nominas_catalogos.maestro a1 JOIN nominas_catalogos.respaldos_nominas a2  ON a1.num = a2.numjpp and a1.jpp = a2.jpp and  a2.archivo >= '{compuesto}'  AND a2.tipo_nomina IN ('{tipo_nom}') and a2.numjpp = {txtnum.Text} and a1.jpp = '{txtTipo.Text}' ORDER BY a2.archivo,clave";


            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0)
            {
                DialogResult dialogo2 = globales.MessageBoxExclamation("NO SE ENCUENTRA INFORMACIÓN DE ESTE JUBILADO EN EL MES SELECCIONADO", "VERIFICAR", globales.menuPrincipal);
                return;
            }



            query = "select  clave,descri from nominas_catalogos.perded order by clave";
            List<Dictionary<string, object>> perded = globales.consulta(query);
            resultado.ForEach(o =>
            {
                o["descri"] = perded.Where(p => Convert.ToString(o["clave"]) == Convert.ToString(p["clave"])).First()["descri"];
                //  o["descri"] += " (RETROACTIVO)"; eeee
            });


            object[] aux2 = new object[resultado.Count];
            int contadorPercepcion = 0;
            int contadorDeduccion = 0;



            string archivoPrimero = string.Empty;


            archivoPrimero = resultado[0]["archivo"].ToString();


            foreach (var item in resultado)
            {
                proyecto = string.Empty;
                nombre = string.Empty;
                curp = string.Empty;
                rfc = string.Empty;
                imss = string.Empty;
                categ = string.Empty;
                clave = string.Empty;
                descri = string.Empty;
                monto = string.Empty;
                //  fecha = fec2.ToString();
                string archivo = string.Empty;
                int año = 0;
                int mes = 0;
                string pago4 = string.Empty;
                string pagot = string.Empty;
                string tipo_pago = string.Empty;
                try
                {

                    proyecto = Convert.ToString(item["proyecto"]);
                    nombre = Convert.ToString(item["nombre"]);
                    curp = Convert.ToString(item["curp"]);
                    rfc = Convert.ToString(item["rfc"]);
                    imss = Convert.ToString(item["imss"]);
                    categ = Convert.ToString(item["categ"]);
                    clave = Convert.ToString(item["clave"]);
                    descri = Convert.ToString(item["descri"]);

                    tipo_pago = Convert.ToString(item["tipo_pago"]);
                    if (tipo_pago == "R")
                    {
                        descri = descri + " RETRO";
                    }
                    else
                    {
                        descri = descri;
                    }
                    //   monto = string.Format("{0:C}", Convert.ToDouble(item["monto"])).Replace("$", "");
                    montohistorial = Convert.ToDouble(item["monto"]); ;
                    archivo = Convert.ToString(item["archivo"]);

                    pago4 = Convert.ToString(item["pago4"]);
                    pagot = Convert.ToString(item["pagot"]);
                    string anioarch = " DE 20" + archivo.Substring(0, 2);
                    string mesarch = archivo.Substring(2, 2);
                    if (mesarch == "01") titulo = "ENERO " + anioarch;
                    if (mesarch == "02") titulo = "FEBRERO " + anioarch;
                    if (mesarch == "03") titulo = "MARZO " + anioarch;
                    if (mesarch == "04") titulo = "ABRIL " + anioarch;
                    if (mesarch == "05") titulo = "MAYO " + anioarch;
                    if (mesarch == "06") titulo = "JUNIO " + anioarch;
                    if (mesarch == "07") titulo = "JULIO " + anioarch;
                    if (mesarch == "08") titulo = "AGOSTO " + anioarch;
                    if (mesarch == "09") titulo = "SEPTIEMBRE " + anioarch;
                    if (mesarch == "10") titulo = "OCTUBRE " + anioarch;
                    if (mesarch == "11") titulo = "NOVIEMBRE " + anioarch;
                    if (mesarch == "12") titulo = "DICIEMBRE " + anioarch;


                }
                catch
                {

                }



                object[] tt1 = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", archivo, titulo };

                if (archivoPrimero != archivo)
                {
                    archivoPrimero = archivo;
                    int tope = contadorDeduccion <= contadorPercepcion ? contadorPercepcion : contadorDeduccion;
                    contadorDeduccion = tope;
                    contadorPercepcion = tope;
                }

                if (Convert.ToInt32(clave) < 60)
                {
                    if (aux2[contadorPercepcion] == null)
                    {
                        tt1[6] = clave;
                        tt1[7] = descri;
                        tt1[8] = montohistorial;
                        aux2[contadorPercepcion] = tt1;
                    }
                    else
                    {
                        object[] tmp = (object[])aux2[contadorPercepcion];
                        tmp[6] = clave;
                        tmp[7] = descri;
                        tmp[8] = montohistorial;
                    }
                    contadorPercepcion++;
                }
                else
                {

                    if (aux2[contadorDeduccion] == null)
                    {
                        tt1[9] = clave;
                        tt1[10] = descri;
                        tt1[12] = (string.IsNullOrWhiteSpace(pago4) || pago4 == "0") ? "" : $"{pago4}/{pagot}";
                        tt1[11] = montohistorial;
                        aux2[contadorDeduccion] = tt1;
                    }
                    else
                    {
                        object[] tmp = (object[])aux2[contadorDeduccion];
                        tmp[9] = clave;
                        tmp[10] = descri;
                        tmp[12] = (string.IsNullOrWhiteSpace(pago4) || pago4 == "0") ? "" : $"{pago4}/{pagot}";
                        tmp[11] = montohistorial;
                    }
                    contadorDeduccion++;
                }

            }



            int contador = 0;

            List<object> lista = new List<object>();
            foreach (object item in aux2)
            {
                if (item == null)
                    break;
                lista.Add(item);
            }


            aux2 = new object[lista.Count];

            int x = 0;
            foreach (object item in lista)
            {
                aux2[x] = item;
                x++;
            }
            object[] parametros = { "proyecto", "nombre", "rfc" };
            object[] valor = { proyecto, nombre, rfc };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;
            //Restablece los objetos para evitar el break del reporteador
            globales.reportes("nominasHistorialSobres", "historialnominas", aux2, "", false, enviarParametros);

        }
    }
}

