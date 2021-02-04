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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA.REPORTES.SALDOS
{
    public partial class listaReportes : Form
    {
        private List<Dictionary<string, object>> lista;
        private string fecha { get; set; }
        private bool opcion { get; set; }

        public listaReportes(List<Dictionary<string, object>> lista, bool opcion, string fecha)
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

            int contador = 0;
            double sy = 0;
            foreach (Dictionary<string, object> item in this.lista)
            {   double folio = Convert.ToDouble(item["folio"]);

                
                string rfc = Convert.ToString(item["rfc"]);
                string nombre = Convert.ToString(item["nombre_em"]);
                string f_emischeq = string.IsNullOrWhiteSpace(Convert.ToString(item["f_emischeq"]))?"": string.Format("{0:d}", item["f_emischeq"]);
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
                string fechaUltimo = string.IsNullOrWhiteSpace(Convert.ToString(item["ultimop"])) ? "" : string.Format("{0:d}",item["ultimop"]);
                string resultadoFinal = string.Empty;
                if (!string.IsNullOrWhiteSpace(f_emischeq)) {
                    DateTime fec1 = DateTime.Parse(f_emischeq);
                    DateTime fec2 = DateTime.Parse(this.fecha);

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


                    resultadoFinal = string.Format("A:{0}-M:{1}", A, M);

                }
                sy += saldo;
                object[] aux = { folio, rfc, nombre, ubicPagare, proyecto, numdesc + "/" + totdesc, importe, imp_unit, pagado, saldo, cta, cta_descripcion, resultadoFinal };
                obj[contador] = aux;
                contador++;
            }
            string opcion1 = (opcion) ? "QUIROGRAFARIOS" : "HIPOTECARIOS";
            string fecha = this.fecha;

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[][] parametros = new object[2][];
            object[] headers = { "p1", "p2", "fecha" , "logo" };
            object[] body = { opcion1, string.Format("{0:d}",DateTime.Parse(fecha)), string.Format("{0:d}",DateTime.Now) ,logo };
            parametros[0] = headers;
            parametros[1] = body;


            if (listBox1.SelectedIndex == 0)
            {
                globales.reportes("reporteRsaldopSaldosPrestamo", "reporteSinSaldo", obj, "", false, parametros);
            }
            else if (listBox1.SelectedIndex == 1)
            {

                globales.reportes("reporteSaldopResumenCuenta", "reporteSinSaldo", obj, "", false, parametros);
            }
            else if (listBox1.SelectedIndex == 2)
            {

                globales.reportes("reporteRSaldoPAlfabetico", "reporteSinSaldo", obj, "", false, parametros);
            }
            else if (listBox1.SelectedIndex == 3)
            {


                globales.reportes("reporteRSaldoPAlfabeticoSinSaldordlc", "reporteSinSaldo", obj, "", false, parametros);
            }
            else if (listBox1.SelectedIndex == 4)
            {


                globales.reportes("reporteRSaldoPFolio", "reporteSinSaldo", obj, "", false, parametros);
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
                    DotNetDBF.DBFField c5 = new DotNetDBF.DBFField("IMP_UNIT", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c6 = new DotNetDBF.DBFField("F_PRIMDESC", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField c7 = new DotNetDBF.DBFField("IMPORTE", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c8 = new DotNetDBF.DBFField("UBIC_PAGAR", DotNetDBF.NativeDbType.Char, 80);
                    DotNetDBF.DBFField c9 = new DotNetDBF.DBFField("NUMDESC", DotNetDBF.NativeDbType.Numeric, 10,2);
                    DotNetDBF.DBFField c10 = new DotNetDBF.DBFField("TOTDESC", DotNetDBF.NativeDbType.Numeric, 10,2);
                    DotNetDBF.DBFField c11 = new DotNetDBF.DBFField("PAGADO", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField c12 = new DotNetDBF.DBFField("FECHA", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField c13 = new DotNetDBF.DBFField("CTA", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField c14 = new DotNetDBF.DBFField("SALDO", DotNetDBF.NativeDbType.Numeric, 10, 2);

                    DotNetDBF.DBFField[] campos = new DotNetDBF.DBFField[] { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14 };
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
                        Convert.ToString(item["f_emischeq"]).Replace(" 12:00:00 a. m.",""),
                        Convert.ToString(item["cuenta"]),
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["saldo"])))?0:Convert.ToDouble(item["saldo"])
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
            if (e.KeyCode == Keys.Enter) {
                button1_Click(null,null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
