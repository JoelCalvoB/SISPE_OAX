using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.VALIDACIONES
{
    public partial class frmSalida : Form
    {
        private object[] arreglo;
        private bool tipoPrestamo;
        private string r3f1;
        private string r3f2;
        private List<Dictionary<string, object>> resultado;
        private bool sinEstadoCtA;
        public frmSalida(bool tipoPrestamo, string t1, string t2, object[] arreglo, List<Dictionary<string, object>> resultado)
        {
            InitializeComponent();
            this.arreglo = arreglo;
            this.tipoPrestamo = tipoPrestamo;
            this.r3f1 = t1;
            this.r3f2 = t2;
            this.resultado = resultado;
        }

        public frmSalida() {
            InitializeComponent();
            this.sinEstadoCtA = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Seguro que desea realizar la operación?", "Aviso", this);
            if (p == DialogResult.No) return;

            

            this.Cursor = Cursors.WaitCursor;
            if (!sinEstadoCtA)
                reporteEstados();
            else
                estadosCuenta();

            this.Cursor = Cursors.Default;
        }

        private void estadosCuenta()
        {
            globales.MessageBoxInformation("Se seleccionara folios de solicitudes", "Aviso", this);
            string query = "(select  folio from datos.p_quirog  EXCEPT SELECT  folio from datos.p_edocta) order by folio asc";
            List<Dictionary<string, object>> tmp1 = globales.consulta(query);
            query = "select  folio,f_solicitud,f_emischeq,nombre_em,descripcion from datos.p_quirog order by folio asc";
            List<Dictionary<string, object>> tmp2 = globales.consulta(query);
            List<Dictionary<string, object>> resultado = new List<Dictionary<string, object>>();
            int contador = 0;
            foreach (Dictionary<string,object> item in tmp2) {
                if (contador == tmp1.Count) break;
                string folio = Convert.ToString(item["folio"]);
                string folio2 = Convert.ToString(tmp1[contador]["folio"]);
                if (folio == folio2) {
                    resultado.Add(item);
                    contador++;
                }
            }

            if (rd1.Checked) {
                object[] obj = new object[resultado.Count];

                for (int x = 0; x < resultado.Count; x++) {
                    string folio = Convert.ToString(resultado[x]["folio"]);
                    string solicitud = Convert.ToString(resultado[x]["f_solicitud"]).Replace(" 12:00:00 a. m.","");
                    string f_emisionCheque = Convert.ToString(resultado[x]["f_emischeq"]).Replace(" 12:00:00 a. m.","");
                    string nombre = Convert.ToString(resultado[x]["nombre_em"]);
                    string descripcion = Convert.ToString(resultado[x]["descripcion"]);

                    object[] objeto = {folio,solicitud,f_emisionCheque,nombre,descripcion };
                    obj[x] = objeto;

                }

                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                object[][] parametros = new object[2][];
                object[] headers = { "fecha","total", "logo" };
                object[] body = { string.Format("{0:d}",DateTime.Now),resultado.Count.ToString() , logo };

                parametros[0] = headers;
                parametros[1] = body;

                globales.reportes("reporteSinEstadosCuenta", "tasaInteres", obj,"",false,parametros);
            }


        }

        private void frmSalida_Load(object sender, EventArgs e)
        {

        }
        private void reporteEstados() {
            if (rd1.Checked)
            {
                string tipoPrestamo = this.tipoPrestamo ? "QUIROGRAFARIOS" : "HIPOTECARIOS";
                DateTime f1 =  new DateTime(Convert.ToInt32(r3f1.Split('-')[0]), Convert.ToInt32(r3f1.Split('-')[1]),Convert.ToInt32( r3f1.Split('-')[2]));
                DateTime f2 = new DateTime(Convert.ToInt32(r3f2.Split('-')[0]), Convert.ToInt32(r3f2.Split('-')[1]), Convert.ToInt32(r3f2.Split('-')[2]));
                string fechaActual = string.Format("{0:d}", DateTime.Now);


                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);
                object[][] parametros = new object[2][];
                object[] headers = { "tipoPrestamo", "R3F2", "R3F1", "fechaActual", "total", "logo" };
                object[] body = { tipoPrestamo, string.Format("{0:d}",f2), string.Format("{0:d}",f1), fechaActual, this.arreglo.Length.ToString() , logo };

                parametros[0] = headers;
                parametros[1] = body;

                globales.reportes("reporteSinPagos", "p_quirog", this.arreglo, "", false, parametros);

            }
            else
            {
                SaveFileDialog dialogoGuardar = new SaveFileDialog();
                dialogoGuardar.AddExtension = true;
                dialogoGuardar.DefaultExt = ".dbf";
                if (dialogoGuardar.ShowDialog() == DialogResult.OK)
                {

                    string ruta = dialogoGuardar.FileName;

                    Stream ops = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                    DotNetDBF.DBFWriter escribir = new DotNetDBF.DBFWriter();
                    escribir.DataMemoLoc = ruta.Replace("dbf", "dbt");

                    DotNetDBF.DBFField c1 = new DotNetDBF.DBFField("FOLIO", DotNetDBF.NativeDbType.Numeric, 20, 2);
                    DotNetDBF.DBFField c2 = new DotNetDBF.DBFField("RFC", DotNetDBF.NativeDbType.Char, 100);
                    DotNetDBF.DBFField c3 = new DotNetDBF.DBFField("NOMBRE_EM", DotNetDBF.NativeDbType.Char, 100);
                    DotNetDBF.DBFField c4 = new DotNetDBF.DBFField("PROYECTO", DotNetDBF.NativeDbType.Char, 100);
                    DotNetDBF.DBFField c5 = new DotNetDBF.DBFField("IMPORTE", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c6 = new DotNetDBF.DBFField("UBIC_PAGAR", DotNetDBF.NativeDbType.Char, 10);
                    DotNetDBF.DBFField c7 = new DotNetDBF.DBFField("NUMDESC", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c8 = new DotNetDBF.DBFField("TOTDESC", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c9 = new DotNetDBF.DBFField("PAGADO", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c10 = new DotNetDBF.DBFField("ULTIMOP", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField c11 = new DotNetDBF.DBFField("TIPO_MOV", DotNetDBF.NativeDbType.Char, 10);
                    DotNetDBF.DBFField c12 = new DotNetDBF.DBFField("F_DESCUENT", DotNetDBF.NativeDbType.Char, 20);

                    DotNetDBF.DBFField[] campos = new DotNetDBF.DBFField[] { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12 };
                    escribir.Fields = campos;

                    foreach (Dictionary<string, object> item in resultado)
                    {
                        List<object> record = new List<object> {
                        Convert.ToDouble(item["folio"]),
                        Convert.ToString(item["rfc"]),
                        Convert.ToString(item["nombre_em"]),
                        Convert.ToString(item["proyecto"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["importe"])))?0:Convert.ToDouble(item["importe"]),
                        Convert.ToString(item["ubic_pagare"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["numdesc"])))?0:Convert.ToDouble(item["numdesc"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["totdesc"])))?0:Convert.ToDouble(item["totdesc"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"])))?0:Convert.ToDouble(item["pagado"]),
                        Convert.ToString(item["ultimop"]),Convert.ToString(item["tipo_mov"]),Convert.ToString(item["f_descuento"])
                    };

                        escribir.AddRecord(record.ToArray());
                    }

                    escribir.Write(ops);
                    escribir.Close();
                    ops.Close();

                    globales.MessageBoxSuccess("Archivo .DBF generado exitosamente", "Archivo generado", this);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
