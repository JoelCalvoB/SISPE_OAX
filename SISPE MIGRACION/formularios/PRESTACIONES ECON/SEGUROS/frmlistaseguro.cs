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

namespace SISPE_MIGRACION.formularios.Seguros
{
    public partial class frmlistaseguro : Form
    {
        private List<Dictionary<string, object>> lista;
        private string fecha { get; set; }
        private bool opcion { get; set; }

        public frmlistaseguro(List<Dictionary<string, object>> lista, bool opcion, string fecha)
        {
            InitializeComponent();
            this.lista = lista;
            this.fecha = fecha;
            this.opcion = opcion;
        }

        private void listaReportes_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            object[] obj = new object[lista.Count];
            string rfc = "";
            string edad = "";
            int x;
            DateTime anio = DateTime.Now;
            int y = Convert.ToInt32(anio.Year);

            int resultado;
            int contador = 0;
            double sy = 0;
            string sexo = "";
            foreach (Dictionary<string, object> item in this.lista)
            {
                rfc = Convert.ToString(item["rfc"]);

                string querysexo = $"select sexo as setso from datos.empleados where rfc='{rfc}'";
                List<Dictionary<string, object>>resulsexo = globales.consulta(querysexo);
                sexo = resulsexo.Count != 0 ? Convert.ToString(resulsexo[0]["setso"]) : "no hay";
                
                string value;
                value = rfc;
                int año;
                int starindex = 4;
                int length = 2;
                string substring = value.Substring(starindex, length);
                edad = "19" + substring;
                x = Convert.ToInt32(edad);
                resultado = y - x;
                edad = Convert.ToString(resultado);

                item.Add("edad", Convert.ToString(edad));
                double folio = Convert.ToDouble(item["folio"]);
                item.Add("sexo", Convert.ToString(sexo));


               
             

                string nombre = Convert.ToString(item["nombre_em"]);
                //if (nombre.Contains("fallecido") || nombre.Contains("FALLECIDO") || nombre.Contains("Fallecido") || nombre.Contains("FALLECIMIENTO") || nombre.Contains("fallecimiento") || nombre.Contains("fallecio") || nombre.Contains("FALLECIO"))
                //{

                //    continue;

                //}
                string f_emischeq = string.IsNullOrWhiteSpace(Convert.ToString(item["f_emischeq"])) ? "" : string.Format("{0:d}", item["f_emischeq"]);
                string ubicPagare = Convert.ToString(item["ubic_pagare"]);
                string proyecto = Convert.ToString(item["proyecto"]);
                string numdesc = Convert.ToString(item["numdesc"]);
                string totdesc = Convert.ToString(item["totdesc"]);
                double importe = (string.IsNullOrWhiteSpace(Convert.ToString(item["importe"]))) ? 0 : (Convert.ToDouble(item["importe"]));
                double imp_unit = (string.IsNullOrWhiteSpace(Convert.ToString(item["imp_unit"]))) ? 0 : (Convert.ToDouble(item["imp_unit"])); ;
                double pagado = (string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"]))) ? 0 : (Convert.ToDouble(item["pagado"]));
                double saldo = (string.IsNullOrWhiteSpace(Convert.ToString(item["saldo"]))) ? 0 : (Convert.ToDouble(item["saldo"]));
                string cta = Convert.ToString(item["cuenta"]);
                string cta_descripcion = Convert.ToString(item["descripcion_cta"]);
                string fechaUltimo = string.IsNullOrWhiteSpace(Convert.ToString(item["ultimop"])) ? "" : string.Format("{0:d}", item["ultimop"]);
                string resultadoFinal = string.Empty;
                if (!string.IsNullOrWhiteSpace(f_emischeq))
                {
                    DateTime dtInicio = DateTime.Parse(f_emischeq);
                    DateTime dtFinal = DateTime.Parse(this.fecha);


                    TimeSpan diferencia = dtFinal - dtInicio;
                    double dias = diferencia.TotalDays;
                    int años = Convert.ToInt32(dias / 365);
                    int sobraAños = Convert.ToInt32(dias % 365);

                    int meses = Convert.ToInt32(sobraAños / 30);
                    int sobraMes = Convert.ToInt32(sobraAños % 30);

                    resultadoFinal = string.Format("{0}A {1}M {2}D", años, meses, sobraMes);

                }
                sy += saldo;
                object[] aux = { folio, rfc, nombre, ubicPagare, proyecto, numdesc + "/" + totdesc, importe, imp_unit, pagado, saldo, cta, cta_descripcion, edad,"" ,sexo };
                obj[contador] = aux;
                contador++;
            }

            object[] auxArreglo = new object[contador];
            for (int joel= 0; joel < contador; joel++)
            {
                auxArreglo[joel] = obj[joel];
            }

            obj = auxArreglo;

            string opcion1 = (opcion) ? "QUIROGRAFARIOS" : "HIPOTECARIOS";
            string fecha = this.fecha;

            object[][] parametros = new object[2][];
            object[] headers = { "p1", "p2", "fecha" };
            object[] body = { opcion1, string.Format("{0:d}", DateTime.Parse(fecha)), string.Format("{0:d}", DateTime.Now) };
            parametros[0] = headers;
            parametros[1] = body;


            if (listBox1.SelectedIndex == 0)
            {
                globales.reportes("reportmensualseguro", "reporteSinSaldo", obj, "", false, parametros);
            }
            else if (listBox1.SelectedIndex == 1)
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
                    DotNetDBF.DBFField c5 = new DotNetDBF.DBFField("IMP_UNIT", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c6 = new DotNetDBF.DBFField("F_PRIMDESC", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField c7 = new DotNetDBF.DBFField("IMPORTE", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c8 = new DotNetDBF.DBFField("UBIC_PAGAR", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField c9 = new DotNetDBF.DBFField("NUMDESC", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c10 = new DotNetDBF.DBFField("TOTDESC", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c11 = new DotNetDBF.DBFField("PAGADO", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c12 = new DotNetDBF.DBFField("FECHA", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField c13 = new DotNetDBF.DBFField("CTA", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField c14 = new DotNetDBF.DBFField("SALDO", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField C15 = new DotNetDBF.DBFField("EDAD", DotNetDBF.NativeDbType.Numeric, 10, 2);

                    DotNetDBF.DBFField[] campos = new DotNetDBF.DBFField[] { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14 ,C15 };
                    escribir.Fields = campos;

                    foreach (Dictionary<string, object> item in this.lista)
                    {
                        List<object> record = new List<object> {
                        Convert.ToDouble(item["folio"]),
                        Convert.ToString(item["rfc"]),
                        Convert.ToString(item["nombre_em"]),
                        Convert.ToString(item["proyecto"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["imp_unit"])))?0:Convert.ToDouble(item["imp_unit"]),
                        Convert.ToString(item["f_primdesc"]).Replace(" 12:00:00 a. m.",""),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["importe"])))?0:Convert.ToDouble(item["importe"]),
                        Convert.ToString(item["ubic_pagare"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["numdesc"])))?0:Convert.ToDouble(item["numdesc"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["totdesc"])))?0:Convert.ToDouble(item["totdesc"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"])))?0:Convert.ToDouble(item["pagado"]),
                        Convert.ToString(item["fecha"]).Replace(" 12:00:00 a. m.",""),
                        Convert.ToString(item["cuenta"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["saldo"])))?0:Convert.ToDouble(item["saldo"]),
                    //    Convert.ToString(item["edad"])
                        
                    };

                        escribir.AddRecord(record.ToArray());
                    }

                    escribir.Write(ops);
                    escribir.Close();
                    ops.Close();

                    globales.MessageBoxSuccess("Archivo .DBF generado exitosamente", "Archivo generado", globales.menuPrincipal);
                }
            }

            this.Cursor = Cursors.Default;
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
