using DPUruNet;
using QRCoder;
using SISPE_MIGRACION.codigo.herramientas.forms.huella;
using SISPE_MIGRACION.codigo.repositorios.nominas_catalogos;
using SISPE_MIGRACION.formularios.NOMINAS.CATALOGOS.modal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SISPE_MIGRACION.formularios.NOMINAS.SUPERVIVENCIA
{
    public partial class frmSupervivencia : Form
    {
        private string jpp;
        private string num;
        private maestro maestro;
        private const int DPFJ_PROBABILITY_ONE = 0x7fffffff;
        public string piobtenida = "";
        public bool bandera = false;
        private bool noimprimir;
        private bool huellita;
        private bool registro;
        private string huella_xml;
        private bool masdedos;
        private string strMasDos;
        private string directorio;

        public frmSupervivencia()
        {
            InitializeComponent();
            this.printDocument3.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument2_PrintPage2);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            try
            {
                globales.dispositivo.Dispose();
            }
            catch
            {
            }

        }

        private void frmSupervivencia_Load(object sender, EventArgs e)
        {

            txtSuperivive_fecha.Text = string.Format("{0:d}", DateTime.Now);
            if (DateTime.Now >= new DateTime(DateTime.Now.Year, 1, 1) && DateTime.Now < new DateTime(DateTime.Now.Year, 5, 1))
            {
                txtsupervive_periodo.SelectedIndex = 0;
            }
            else if (DateTime.Now >= new DateTime(DateTime.Now.Year, 5, 1) && DateTime.Now < new DateTime(DateTime.Now.Year, 9, 1))
            {
                txtsupervive_periodo.SelectedIndex = 1;
            }
            else
            {
                txtsupervive_periodo.SelectedIndex = 2;
            }


            txtSupervive_supervivencia.SelectedIndex = 0;

            txtSupervive_anio.Text = DateTime.Now.Year.ToString();

            button3.Visible = false;

            this.directorio = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\pdfjubilados";

            //Se crea los directorios necesarios}

            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }


        }

        private void btnFolio_Click(object sender, EventArgs e)
        {
            noimprimir = true;
            huellita = false;
            frmModalJubilados jubi = new frmModalJubilados();
            jubi.enviar = rellenar;
            globales.showModal(jubi);
            txtsupervive_periodo.Select();
        }

        private void rellenar(Dictionary<string, object> diccinario)
        {
            if (this.InvokeRequired)
            {
                metodoEnviar delegado = new metodoEnviar(rellenar);
                this.Invoke(delegado, new Object[] { diccinario });
            }
            else
            {
                dbaseORM orm = new dbaseORM();
                maestro = orm.getObject<maestro>(diccinario);

                txtNumerobuscar.Text = $"{maestro.jpp}{maestro.num}";
                this.jpp = maestro.jpp;
                this.num = Convert.ToString(maestro.num);
                txtNum.Text = Convert.ToString(maestro.num);
                txtRfc.Text = maestro.rfc;
                txtNombre.Text = maestro.nombre;
                txtDireccion.Text = maestro.domicilio;
                txtCat.Text = maestro.categ;
                txtTel.Text = maestro.telefono;
                txtSexo.Text = maestro.sexo;
                txtEle.Text = maestro.nomelec;
                txtProyecto.Text = maestro.proyecto;
                txtFin.Text = globales.parseDateTime(maestro.fching);
                txtLeyenda.Text = maestro.leyen;
                txtNCuenta.Text = maestro.cuentabanc;
                txtBanco.Text = maestro.banco;
                txtImss.Text = maestro.imss;
                txtCurp.Text = maestro.curp;
                txtFirma.Text = maestro.superviven;
                txtFecha.Text = globales.parseDateTime(maestro.fsupervive);
                jpp = maestro.jpp;

                

                string queryhuella = $"select huella  from nominas_catalogos.maestro where jpp = '{maestro.jpp}' AND num = {maestro.num} ";
                List<Dictionary<string, object>> result = globales.consulta(queryhuella);
                string huella = Convert.ToString(result[0]["huella"]);
                this.huella_xml = huella;
                if (!string.IsNullOrWhiteSpace(huella))
                {
                    label25.Visible = true;
                }
                else

                {
                    label25.Visible = false;
                }


                string query = $"select * from nominas_catalogos.supervive where jpp = '{maestro.jpp}' AND numjpp = {maestro.num} ORDER BY ANIO ";
                List<supervive> listasupervive = orm.queryForList<supervive>(query);

                dtggrid.Rows.Clear();
                listasupervive.ForEach(datos =>
                {
                    string periodo = string.Empty;
                    switch (datos.periodo)
                    {
                        case 1:
                            periodo = "01 AL 21 DE ENERO";
                            break;
                        case 2:
                            periodo = "01 AL 21 DE MAYO";
                            break;
                        case 3:
                            periodo = "01 AL 21 DE SEPTIEMBRE";
                            break;
                        default:
                            break;
                    }
                    dtggrid.Rows.Add(datos.anio, periodo, globales.parseDateTime(datos.fecha));

                });

                query = $"select * from nominas_catalogos.supervive where jpp = '{maestro.jpp}' AND numjpp = {maestro.num} AND ANIO = {DateTime.Now.Year} AND PERIODO = {txtsupervive_periodo.SelectedIndex + 1}";
                List<Dictionary<string, object>> total = globales.consulta(query);
                if (total.Count != 0)
                {
                    exito.Visible = true;
                    no.Visible = false;
                }
                else
                {
                    exito.Visible = false;
                    no.Visible = true;
                }

                if (huellita)
                {
                    btnP_hipote_Click(null, null);
                    huellita = false;
                }


                if (!noimprimir)
                {
                  //  button6_Click(null, null);
                }

                noimprimir = false;
            }
        }

        private void btnP_hipote_Click(object sender, EventArgs e)
        {
            if (exito.Visible)
            {
                globales.MessageBoxExclamation("LA SUPERVIVENCIA YA SE REGISTRO EN EL PERIODO", "AVISO", globales.menuPrincipal);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumerobuscar.Text))
            {
                globales.MessageBoxExclamation("Favor de ingresar Jubilado, Pensionado o Pensionista para la firma de supervivencia", "Aviso", globales.menuPrincipal);
                return;
            }

            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas realizar la firma de supervivencia?", "Aviso", globales.menuPrincipal);
            if (dialogo == DialogResult.No) return;

            this.registro = true;

            supervive obj = new supervive();
            obj.jpp = this.jpp;
            obj.numjpp = globales.convertInt(txtNum.Text);
            obj.anio = globales.convertInt(txtSupervive_anio.Text);
            obj.periodo = txtsupervive_periodo.SelectedIndex + 1;
            obj.tipo = txtSupervive_supervivencia.SelectedIndex + 1;
            obj.observaciones = txtSupervive_observacion.Text;
            obj.firma = radio_firma.Checked ? 1 : 2;
            obj.q_captura = globales.usuario;
            obj.f_captura = DateTime.Now;
            obj.tomafirma = globales.usuario;
            obj.fecha = globales.convertDatetime(txtSuperivive_fecha.Text);

            string queyr = $"select * from nominas_catalogos.maestro where rfc = '{txtRfc.Text}'";

            dbaseORM orm = new dbaseORM();

            List<maestro> lista = orm.queryForList<maestro>(queyr);

            if (lista.Count > 1)
            {


                 
                strMasDos = "";

                List<supervive> supers = new List<supervive>();
                foreach (maestro ma in lista) {
                    
                    supervive obj1 = new supervive();
                    obj1.jpp = ma.jpp;
                    obj1.numjpp = ma.num;
                    obj1.anio = globales.convertInt(txtSupervive_anio.Text);
                    obj1.periodo = txtsupervive_periodo.SelectedIndex + 1;
                    obj1.tipo = txtSupervive_supervivencia.SelectedIndex + 1;
                    obj1.observaciones = txtSupervive_observacion.Text;
                    obj1.firma = radio_firma.Checked ? 1 : 2;
                    obj1.q_captura = globales.usuario;
                    obj1.f_captura = DateTime.Now;
                    obj1.tomafirma = globales.usuario;
                    obj1.fecha = globales.convertDatetime(txtSuperivive_fecha.Text);

                    supers.Add(obj1);

                    if (!(obj1.jpp == this.jpp && obj1.numjpp == globales.convertInt(txtNum.Text)))
                    {
                        strMasDos += $", {obj1.jpp}{obj1.numjpp} ";
                    }
                }

                orm.insertAll<supervive>(supers);

                globales.MessageBoxSuccess("Supervivencia registrada", "Aviso", globales.menuPrincipal);
                no.Visible = false;
                exito.Visible = true;
                string periodo = string.Empty;
                switch (obj.periodo)
                {
                    case 1:
                        periodo = "01 AL 21 DE ENERO";
                        break;
                    case 2:
                        periodo = "01 AL 21 DE MAYO";
                        break;
                    case 3:
                        periodo = "01 AL 21 DE SEPTIEMBRE";
                        break;
                    default:
                        break;
                }
                dtggrid.Rows.Add(txtSupervive_anio.Text, periodo, globales.parseDateTime(obj.fecha));
            }
            else {
                if (orm.insert<supervive>(obj))
                {
                    globales.MessageBoxSuccess("Supervivencia registrada", "Aviso", globales.menuPrincipal);
                    no.Visible = false;
                    exito.Visible = true;
                    string periodo = string.Empty;
                    switch (obj.periodo)
                    {
                        case 1:
                            periodo = "01 AL 21 DE ENERO";
                            break;
                        case 2:
                            periodo = "01 AL 21 DE MAYO";
                            break;
                        case 3:
                            periodo = "01 AL 21 DE SEPTIEMBRE";
                            break;
                        default:
                            break;
                    }
                    dtggrid.Rows.Add(txtSupervive_anio.Text, periodo, globales.parseDateTime(obj.fecha));

                }
            }
            
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                printDocument2.PrinterSettings.PrinterName = "EPSON TM-T20II Receipt5";
                printDocument3.PrinterSettings.PrinterName = "EPSON TM-T20II Receipt5";
                this.bandera = true;
            }
            else
            {
                printDocument2.PrinterSettings.PrinterName = $@"\\{piobtenida}\EPSON TM-T20II Receipt5";
                printDocument3.PrinterSettings.PrinterName = $@"\\{piobtenida}\EPSON TM-T20II Receipt5";
                this.bandera = true;

        }

            try
            {
                printDocument2.Print();
                printDocument3.Print();


                if (DialogResult.Yes == globales.MessageBoxQuestion("¿Deseas imprimir el sobre de págo?","Sobre de pago",globales.menuPrincipal)) {
                    this.button1_Click_2(null, null);
                }

            }
            catch
            {
                return;
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmEncontrarDispositivo n = new frmEncontrarDispositivo();
            n.ShowDialog();

        

            if (n.conectado == true)
            {
                dispo.Text = "LECTOR CONECTADO";
                dispo.ForeColor = Color.ForestGreen;
                DialogResult dialogo1 = globales.MessageBoxSuccess("VICULADO DISPOSITIVO CORRECTAMENTE", "AVISO", globales.menuPrincipal);
                
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNum.Text))
            {
                return;
            }

            try
            {
            globales.dispositivo.Dispose();

            }
            catch
            {
                DialogResult dialo = globales.MessageBoxError("CONECTE EL LECTOR DE HUELLA Y VINCULELO", "UPSS", globales.menuPrincipal);
                return;
            }

            frmEnrollar enrollar = new frmEnrollar();
            enrollar.enviar = enrolar;
            enrollar.ShowDialog();



            globales.dispositivo.Dispose();
            button5_Click(null, null);
          //  limipiar();
            this.registro = false;
          //  frmSupervivencia_Load(null, null);
        }


        public void limipiar()
        {
            button4.Visible = false;
            this.panel5.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = "");
            this.txtFin.Clear();
            this.txtFecha.Clear();
            this.panel4.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = "");
            txtsupervive_periodo.Text = "";
            txtSupervive_supervivencia.Text = "";
            dtggrid.Rows.Clear();
            txtNumerobuscar.Clear();
        }
        public void enrolar(string cadena)
        {


            if (string.IsNullOrWhiteSpace(txtNumerobuscar.Text))
            {
                globales.MessageBoxExclamation("No puedes enrolar, favor de elegir JPP", "Aviso", globales.menuPrincipal);
                return;
            }

            maestro.huella = cadena;
            maestro.huella = maestro.huella.Replace("\n", "");

            string query = $"update nominas_catalogos.maestro set huella = '{maestro.huella}' where jpp = '{maestro.jpp}' and num = {maestro.num}";
            globales.consulta(query, true);

            // new dbaseORM().update<maestro>(maestro);

            globales.MessageBoxSuccess("Jubilado enrolado exitosamente", "Aviso", globales.menuPrincipal);


            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                printDocument1.PrinterSettings.PrinterName = "EPSON TM-T20II Receipt5";
                this.bandera = true;
        }
            else
            {
                printDocument1.PrinterSettings.PrinterName = $@"\\{piobtenida}\EPSON TM-T20II Receipt5";
                this.bandera = true;

            }

            try
            {


                printDocument1.Print();

            }
            catch
            {
                return;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Constants.ResultCode result = Constants.ResultCode.DP_DEVICE_FAILURE;

            
            result = globales.dispositivo.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);

            

            if (result != Constants.ResultCode.DP_SUCCESS)
            {
                globales.MessageBoxExclamation("No se puede iniciar el lector de huellas", "Aviso", globales.menuPrincipal);
                globales.dispositivo.Dispose();
                dispo.Text = "DISPOSITIVO NO CONECTADO";
                return;
            }

            globales.dispositivo.On_Captured += new Reader.CaptureCallback(OnCaptured);

            if (!CaptureFingerAsync())
            {
                globales.MessageBoxExclamation("No se puede iniciar la captura del lector", "Aviso", globales.menuPrincipal);
                globales.dispositivo.Dispose();
                dispo.Text= "DISPOSITIVO NO CONECTADO";



                return;
            }
        }


        public enum tipo_mensaje
        {
            success,
            exclamation,
            error
        }

        delegate void mensajeCallback(tipo_mensaje tipo, string mensaje);

        public void realizarMensaje(tipo_mensaje tipo, string mensaje)
        {
            if (this.InvokeRequired)
            {
                mensajeCallback delegado = new mensajeCallback(realizarMensaje);
                this.Invoke(delegado, new Object[] { tipo, mensaje });
            }
            else
            {
                switch (tipo)
                {
                    case tipo_mensaje.success:
                        globales.MessageBoxSuccess(mensaje, "Aviso", globales.menuPrincipal);
                        break;
                    case tipo_mensaje.exclamation:
                        globales.MessageBoxExclamation(mensaje, "Aviso", globales.menuPrincipal);
                        break;
                    case tipo_mensaje.error:
                        globales.MessageBoxError(mensaje, "Aviso", globales.menuPrincipal);
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnCaptured(CaptureResult result)
        {
            frmEncontrarDispositivo n = new frmEncontrarDispositivo();
            //if (n.conectado==false)
            //{
            //    DialogResult dialogoerror = globales.MessageBoxError("Ocurrio un error, no se detecta dispositivo", "Verificar", globales.menuPrincipal);
            //    return;
            //    dispo.Text = "DISPOSITIVO NO CONECTADO";
            //}

            DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(result.Data, Constants.Formats.Fmd.ANSI);
            if (result.ResultCode != Constants.ResultCode.DP_SUCCESS)
            {
                //throw new Exception(result.ResultCode.ToString());
            }


            Fmd capturado = resultConversion.Data;

            try
            {
                string hexa = ByteArrayToString(capturado.Bytes);
            }
            catch
            {
                CheckForIllegalCrossThreadCalls = false;
                dispo.Text = "DISPOSITIVO NO CONECTADO";
                return;
            }

            //    string query = $"select jpp,num,huella from nominas_catalogos.maestro where huella is not null order by jpp,num";
            string query = "select jpp,num,huella from nominas_catalogos.maestro where huella is not null  and huella <>'' order by jpp,num";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0) return;
      

            string caaaa = Fmd.SerializeXml(capturado);
            Fmd dese = Fmd.DeserializeXml(caaaa);

            bool encontrado = true;

            foreach (Dictionary<string, object> item in resultado)
            {
                string xmlStr = Convert.ToString(item["huella"]);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmlStr);
                Fmd fmd = Fmd.DeserializeXml(xmlStr);
                Fmd[] fmds = new Fmd[1];
                fmds[0] = fmd;
                // fmds[1] = capturado;


                int thresholdScore = DPFJ_PROBABILITY_ONE * 1 / 100000;

                CompareResult resultadoAux = Comparison.Compare(capturado, 0, fmd, 0);
                IdentifyResult identifyResult = Comparison.Identify(capturado, 0, fmds, thresholdScore, 2);

                if (resultadoAux.Score == 0)
                {
                    realizarMensaje(tipo_mensaje.success, $"{item["jpp"]} encontrado");


                    query = $"select * from nominas_catalogos.maestro where huella is not null and jpp = '{item["jpp"]}' and num = {item["num"]} order by jpp,num";
                    resultado = globales.consulta(query);
                    if (resultado.Count != 0)
                    {
                        huellita = true;
                        rellenar(resultado[0]);
                    }
                    encontrado = true;
                    break;
                }
                else
                {
                    encontrado = false;
                }

                if (resultadoAux.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    realizarMensaje(tipo_mensaje.exclamation, "UPS, NO SE ENCUENTRA LA HUELLA EN LA BASE");
                    encontrado = true;
                    return;
                }
            }

            if (!encontrado)
            {
                realizarMensaje(tipo_mensaje.exclamation, "UPS, NO SE ENCUENTRA LA HUELLA EN LA BASE");
                
            }
           

        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }


        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public bool CaptureFingerAsync()
        {
            try
            {
                GetStatus();

                Constants.ResultCode captureResult = globales.dispositivo.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, globales.dispositivo.Capabilities.Resolutions[0]);
                if (captureResult != Constants.ResultCode.DP_SUCCESS)
                {
                    //reset = true;
                    throw new Exception("" + captureResult);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
                return false;
            }
        }


        public void GetStatus()
        {
            Constants.ResultCode result = globales.dispositivo.GetStatus();

            if ((result != Constants.ResultCode.DP_SUCCESS))
            {
                throw new Exception("" + result);
            }

            if ((globales.dispositivo.Status.Status == Constants.ReaderStatuses.DP_STATUS_BUSY))
            {
                Thread.Sleep(50);
            }
            else if ((globales.dispositivo.Status.Status == Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
            {
                globales.dispositivo.Calibrate();
            }
            else if ((globales.dispositivo.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY))
            {
                throw new Exception("Reader Status - " + globales.dispositivo.Status.Status);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void frmSupervivencia_Shown(object sender, EventArgs e)
        {
            if (dispo.Text == "DISPOSITIVO NO CONECTADO")
            {
               button3_Click(null, null);
            }

            txtFirma.Enabled = true;
            btnFolio.Focus();
        }

        private void dispo_TextChanged(object sender, EventArgs e)
        {
            if (dispo.Text == "LECTOR CONECTADO")
            {
                button3.Visible = false;
                button5_Click(null, null);
                button5.Visible = false;
            }
            if (dispo.Text == "DISPOSITIVO NO CONECTADO")
            {
                button3.Visible = true;

                dispo.Visible = false;
                DialogResult dialogonohay = globales.MessageBoxExclamation("Ocurrio un error,no se detecta el dispositivo", "ALERTA", globales.menuPrincipal);
                button3_Click(null, null);

            }
        }

        private void txtNumerobuscar_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void txtNum_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNum.Text))
            {
                button4.Visible = true;
            }
            else
            {
                button4.Visible = false;
            }
        }

        private void frmSupervivencia_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
            {

            }

        }

        private void frmSupervivencia_KeyDown(object sender, KeyEventArgs e)
        {
     




        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                printDocument1.PrinterSettings.PrinterName = "EPSON TM-T20II Receipt5";
                this.bandera = true;
            }
            else
            {
                printDocument1.PrinterSettings.PrinterName = $@"\\{piobtenida}\EPSON TM-T20II Receipt5";
                this.bandera = true;

            }

            try
            {
            printDocument1.Print();
            }
            catch(Exception ee)
            {
                globales.MessageBoxError(ee.Message, "Error", globales.menuPrincipal);
            }


         
            
          
         
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string queryhuella = $"select huella  from nominas_catalogos.maestro where jpp = '{maestro.jpp}' AND num = {maestro.num} ";
            List<Dictionary<string, object>> result = globales.consulta(queryhuella);
            string huella = Convert.ToString(result[0]["huella"]);
            this.huella_xml = huella;
            string recortado = this.huella_xml.Substring(50, 60);
            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData data = qr.CreateQrCode(recortado, QRCodeGenerator.ECCLevel.Q);
            QRCode code = new QRCode(data);
            pictureBox1.Image = code.GetGraphic(2);
            //pictureBox1.Image.Save("c:\\estado.png", ImageFormat.Jpeg);

            //Bitmap estado = new Bitmap(@"C:\estado.png");
            //System.Drawing.Image img = estado;
            Point loc = new Point(100, 0);
            e.Graphics.DrawImage(pictureBox1.Image, loc);
            e.Graphics.DrawString($"\n\n\n\n\nEnrolamiento de huella digital\n\nNumero:\t{txtNumerobuscar.Text}\nRFC:\t{txtRfc.Text}\nNombre:\t{txtNombre.Text}\nFecha:\t{string.Format("{0:dd/MM/yyyy}", DateTime.Now)}\nHora:\t{string.Format("{0:hh:mm}", DateTime.Now)}\nOFICINA PENSIONES DEL ESTADO DE\nOAXACA\n\n .", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 10);

            

            //System.Drawing.Image img = Image.FromFile("c:\\estado.png");
            //Point loc = new Point(50, 50);
            //e.Graphics.DrawImage(img, loc);
            //e.Graphics.DrawString($"Enrrolamiento de huella digital\n\nNumero:\t{txtNumerobuscar.Text}\nRFC:\t{txtRfc.Text}\nNombre:\t{txtNombre.Text}\nFecha:\t{string.Format("{0:dd/MM/yyyy}", DateTime.Now)}\nHora:\t{string.Format("{0:hh:mm}", DateTime.Now)}\n\n\n\nOFICINA PENSIONES DEL ESTADO DE\nOAXACA\n\n .", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 10);



        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            piobtenida = textBox1.Text;
        }

        private void txtFirma_TextChanged(object sender, EventArgs e)
        {
            if (txtFirma.Text == "F")
            {
                string query = $"update nominas_catalogos.maestro set superviven='F' where jpp='{this.jpp}' and num={this.num}";
                globales.consulta(query);
                DialogResult dialo = globales.MessageBoxSuccess("SE ACTUALIZO EL ESTADO DEL JUBILADO", "CORRECTO", globales.menuPrincipal);
                return;
            }
            if (txtFirma.Text == "P")
            {
                string query = $"update nominas_catalogos.maestro set superviven='P' where jpp='{this.jpp}' and num={this.num}";
                globales.consulta(query);
                DialogResult dialo = globales.MessageBoxSuccess("SE ACTUALIZO EL ESTADO DEL JUBILADO", "CORRECTO", globales.menuPrincipal);

                return;
            }

            if (txtFirma.Text == "T")
            {
                string query = $"update nominas_catalogos.maestro set superviven='P' where jpp='{this.jpp}' and num={this.num}";
                globales.consulta(query);
                DialogResult dialo = globales.MessageBoxSuccess("SE ACTUALIZO EL ESTADO DEL JUBILADO", "CORRECTO", globales.menuPrincipal);

                return;
            }

            if (txtFirma.Text == "S" || txtFirma.Text=="") 
        {
           

                return;
            }

            DialogResult dialogo = globales.MessageBoxError("LETRA INCORRECTA O NO VALIDA", "ERROR", globales.menuPrincipal);
            
           
            
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string recortado = this.huella_xml.Substring(50, 60);
            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData data = qr.CreateQrCode(recortado, QRCodeGenerator.ECCLevel.Q);
            QRCode code = new QRCode(data);
            pictureBox1.Image = code.GetGraphic(2);
           

            
            
            Point loc = new Point(100, 0);
            e.Graphics.DrawImage(this.pictureBox1.Image, loc);
            e.Graphics.DrawString($"\n\n\n\n\n\nRegistro de supervivencia\n\nPeriodo:\t{txtsupervive_periodo.Text}\n\nNumero:\t{txtNumerobuscar.Text}{this.strMasDos}\nRFC:\t{txtRfc.Text}\nNombre:\t{txtNombre.Text}\nFecha:\t{string.Format("{0:dd/MM/yyyy}", DateTime.Now)}\nHora:\t{string.Format("{0:hh:mm}", DateTime.Now)}\n\n\n\nOFICINA PENSIONES DEL ESTADO DE\nOAXACA\n\n .", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 10);

        }

        private void printDocument2_PrintPage2(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string recortado = this.huella_xml.Substring(50, 60);
            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData data = qr.CreateQrCode(recortado, QRCodeGenerator.ECCLevel.Q);
            QRCode code = new QRCode(data);
            pictureBox1.Image = code.GetGraphic(2);




            Point loc = new Point(100, 0);
            e.Graphics.DrawImage(this.pictureBox1.Image, loc);
            e.Graphics.DrawString($"\n\n\n\n\n\n\t\tC O P I A \nRegistro de supervivencia\n\nPeriodo:\t{txtsupervive_periodo.Text}\n\nNumero:\t{txtNumerobuscar.Text}{this.strMasDos}\nRFC:\t{txtRfc.Text}\nNombre:\t{txtNombre.Text}\nFecha:\t{string.Format("{0:dd/MM/yyyy}", DateTime.Now)}\nHora:\t{string.Format("{0:hh:mm}", DateTime.Now)}\n\n\n\nOFICINA PENSIONES DEL ESTADO DE\nOAXACA\n\n .", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 10, 10);
            this.strMasDos = string.Empty;

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            string mesRadio = "";
            string periodo = "";
            string fechaSobres = "";
            DateTime fecha = new DateTime(DateTime.Now.Year, 1, 1);

            int mes = DateTime.Now.Month;

            switch (mes) {
                case 1:
                    mesRadio = "01";
                    periodo = "01 al 31 de Enero del " + DateTime.Now.Year;
                    fechaSobres = "31/12/" + ((Convert.ToString(fecha.Year - 1).Substring(2, 2)));
                    break;
                case 2:
                    mesRadio = "02";
                    periodo = "01 al 28 de Febrero del " + DateTime.Now.Year;
                    fechaSobres = "28/01/" + DateTime.Now.Year.ToString().Substring(2, 2);
                    break;
                case 3:
                    mesRadio = "03";
                    periodo = "01 al 31 de Marzo del " + DateTime.Now.Year;
                    fechaSobres = "31/02/" + DateTime.Now.Year.ToString().Substring(2, 2);
                    break;
                case 4:
                    mesRadio = "04";
                    periodo = "01 al 30 de Abril del " + DateTime.Now.Year;
                    fechaSobres = "30/03/" + DateTime.Now.Year.ToString().Substring(2, 2);
                    break;
                case 5:
                    mesRadio = "05";
                    periodo = "01 al 31 de Mayo del " + DateTime.Now.Year;
                    fechaSobres = "31/04/" + DateTime.Now.Year.ToString().Substring(2, 2);
                    break;
                case 6:
                    mesRadio = "06";
                    periodo = "01 al 30 de Junio del " + DateTime.Now.Year;
                    fechaSobres = "30/05/" + DateTime.Now.Year.ToString().Substring(2, 2);
                    break;
                case 7:

                    mesRadio = "07";
                    periodo = "01 al 31 de Julio del " + DateTime.Now.Year;
                    fechaSobres = "31/06/" + DateTime.Now.Year.ToString().Substring(2, 2);
                    break;
                case 8:
                    mesRadio = "08";
                    periodo = "01 al 31 de Agosto del " + DateTime.Now.Year;
                    fechaSobres = "31/07/" + DateTime.Now.Year.ToString().Substring(2, 2);
                    break;
                case 9:
                    mesRadio = "09";
                    periodo = "01 al 30 de Septiembre del " + DateTime.Now.Year;
                    fechaSobres = "30/08/" + DateTime.Now.Year.ToString().Substring(2, 2);
                    break;
                case 10:
                    mesRadio = "10";
                    periodo = "01 al 31 de Octubre del " + DateTime.Now.Year;
                    fechaSobres = "31/09/" + DateTime.Now.Year.ToString().Substring(2, 2);
                    break;
                case 11:
                    mesRadio = "11";
                    periodo = "01 al 30 de Noviembre del " + DateTime.Now.Year;
                    fechaSobres = "30/10/" + DateTime.Now.Year.ToString().Substring(2, 2);
                    break;
                case 12:
                    mesRadio = "12";
                    periodo = "01 al 31 de Diciembre del " + DateTime.Now.Year;
                    fechaSobres = "31/11/" + DateTime.Now.Year.ToString().Substring(2, 2);
                    break;
            }


            fecha = new DateTime(DateTime.Now.Year, Convert.ToInt32(mesRadio), 1);
            fecha = fecha.AddDays(-1);
            fechaSobres = string.Format("{0:d}", fecha);



            string tipo_nomina = "";
            string parametro = txtNumerobuscar.Text;

            tipo_nomina = "N";  // normal


            string compuesto = DateTime.Now.Year.ToString().Substring(2, 2) + Convert.ToString(mesRadio);
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

            query = "select  clave,descri from nominas.perded order by clave";
            List<Dictionary<string, object>> perded = globales.consulta(query);
            resultado.ForEach(o =>
            {
                o["descri"] = perded.Where(p => Convert.ToString(o["clave"]) == Convert.ToString(p["clave"])).First()["descri"];
                //  o["descri"] += " (RETROACTIVO)"; eeee
            });

             string proyecto, nombre, curp, rfc, imss, categ, clave, descri, monto, directorio, archivocompara, leyen;
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
                mes = 0;
                string pago4 = string.Empty;
                string pagot = string.Empty;
                try
                {

                    proyecto = Convert.ToString(item["proyecto"]);
                    leyen = Convert.ToString(item["leyen"]);
                    nombre = Convert.ToString(item["nombre"]);
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

            object[] objeto = new object[17];
            for (int x = 0; x < 17; x++)
            {
                object[] tt1 = { "", "", "", "", "", "", "", "", "", "", "", "", "" };
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
                if (x == 17)
                {
                    System.Diagnostics.Debug.WriteLine(txtNumerobuscar.Text + " " + txtNombre.Text + " " + txtRfc.Text);
                    break;
                }
                objeto[x] = aux2[x];
                object[] sacarDato = (object[])aux2[x];
                //  double percepcion = string.IsNullOrWhiteSpace(Convert.ToString(sacarDato[8])) ? 0 : Convert.ToDouble(sacarDato[8]);
                // double deduccion = string.IsNullOrWhiteSpace(Convert.ToString(sacarDato[11])) ? 0 : Convert.ToDouble(sacarDato[11]);
                //sumaPercepciones += percepcion;
                //sumaDeducciones += deduccion;

            }


            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[] parametros = { "proyecto", "nombre", "curp", "rfc", "imss", "categ", "fechapago", "periodo", "sumaPercepcion", "sumaDeduccion" , "logo" };
            object[] valor = { txtNumerobuscar.Text, txtNombre.Text, txtCurp.Text, txtRfc.Text, txtImss.Text, txtCat.Text, string.Format("{0:d}", fechaSobres), periodo, sumaPercepciones.ToString(), sumaDeducciones.ToString(), logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            try
            {
                globales.reportes("sobres", "sobres", objeto, "", true, enviarParametros, true, "RECIBO");
                query = $"update  nominas_catalogos.maestro set f_impresion=CURRENT_DATE where jpp='{txtNumerobuscar.Text.Substring(0,3)}' and num={txtNum.Text};";
                globales.consulta(query);
                // DialogResult dialogo = globales.MessageBoxSuccess("EN LA RUTA C:-Users-TU-USUARIO-pdfjubilados se guarda el PDF", "REVISAR CARPETA", globales.menuPrincipal);
                string pdfPath = Path.Combine(Application.StartupPath, this.directorio + "\\RECIBO.pdf");


                Process.Start(pdfPath);
            }
            catch
            {
                globales.MessageBoxError("Favor de cerrar el visualizador de PDF para poder visualizar el sobre de pago", "Aviso", globales.menuPrincipal);
            }
        }

        private void txtsupervive_periodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtSupervive_supervivencia_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtSupervive_supervivencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
