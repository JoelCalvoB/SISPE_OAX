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

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.REPORTES
{

    public partial class frmMenuOpciones : Form
    {
        private bool esReporte;
        public frmMenuOpciones(bool esReporte = false)
        {
            InitializeComponent();
            this.esReporte = esReporte;
        }

        private void frmMenuOpciones_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime tiempo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
                tiempo = tiempo.AddDays(30);
                tiempo = new DateTime(tiempo.Year, tiempo.Month, 15);
                txtFecha.Value = tiempo;

            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombreDbf = string.Empty;
            DateTime dFecha = txtFecha.Value;
            string qtipoRelacion = string.Empty;
            string sFecha = string.Format("{0}-{1}-{2}", dFecha.Year, dFecha.Month, dFecha.Day);

            string query = (this.esReporte) ? "select * from datos.solicitud_dependencias where t_prestamo = " : "select DISTINCT tipo_rel from datos.solicitud_dependencias where t_prestamo = ";

            if (rdQuiro.Checked)
            {
                query += " 'Q' ";
                nombreDbf = "PQ_";
            }
            else
            {
                query += " 'H' ";
                nombreDbf = "PH_";
            }

            string altas = string.Empty;
            if (rdAltas.Checked)
                altas = "A";
            else if (rdCambios.Checked)
                altas = "C";
            else
                altas = "B";


            if (chkAval.Checked && chkNormal.Checked)
            {
                query += string.Format(" and (tipo_mov = '{0}N' OR tipo_mov = '{0}A') and f_descuento = '{1}'", altas, sFecha);
                qtipoRelacion = "SUSCRIPTOR Y AVAL";
                nombreDbf += string.Format("{0}N_{0}A", altas);
            }
            else if (chkAval.Checked)
            {
                query += string.Format(" and tipo_mov = '{0}A' and f_descuento = '{1}'", altas, sFecha);
                qtipoRelacion = "AVAL";
                nombreDbf += string.Format("{0}A", altas);
            }
            else if (chkNormal.Checked)
            {
                query += string.Format(" and tipo_mov = '{0}N' and f_descuento = '{1}'", altas, sFecha);
                qtipoRelacion = "SUSCRIPTOR";
                nombreDbf += string.Format("{0}N", altas);
            }
            else
            {
                globales.MessageBoxExclamation("Error en selección", "Aviso!!", this);
                return;
            }
            query += "  order by tipo_rel asc";
            string queryGlobal = query;

            if (this.esReporte)
            {
                globales.MessageBoxInformation(string.Format("Seleccionando altas del {0} de sector central", sFecha), "Seleccionando", this);
            }
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            if (globales.esReporte) goto seccionReporte;

            List<Dictionary<string, string>> listaDiskets = new List<Dictionary<string, string>>();
            foreach (Dictionary<string, object> item in resultado)
            {
                string tipoRelacion = Convert.ToString(item["tipo_rel"]);
                if (!string.IsNullOrWhiteSpace(tipoRelacion))
                {
                    query = string.Format("select * from catalogos.disket where cuenta = '{0}'", item["tipo_rel"]);
                    List<Dictionary<string, object>> tmpDisket = globales.consulta(query);
                    if (tmpDisket.Count > 0)
                    {
                        Dictionary<string, string> diccionario = new Dictionary<string, string>();
                        diccionario.Add("cuenta", Convert.ToString(tmpDisket[0]["cuenta"]));
                        diccionario.Add("descripcion", Convert.ToString(tmpDisket[0]["descripcion"]));
                        listaDiskets.Add(diccionario);
                    }
                }
            }

            frmTiporelacion tr = new frmTiporelacion();



            tr.setLista(listaDiskets, queryGlobal, dFecha, qtipoRelacion, altas, rdQuiro.Checked);
            globales.showModal(tr);
            return;

            seccionReporte:

            query = "select * from catalogos.disket where status = 'C'";
            List<Dictionary<string, object>> disketTmp = globales.consulta(query);

            List<Dictionary<string, object>> auxtmp1 = new List<Dictionary<string, object>>();
            foreach (Dictionary<string, object> item in resultado)
            {
                foreach (Dictionary<string, object> item2 in disketTmp)
                {
                    if (Convert.ToString(item["tipo_rel"]) == Convert.ToString(item2["cuenta"]))
                    {
                        auxtmp1.Add(item);
                    }

                }
            }
            resultado = auxtmp1;
            auxtmp1 = null;
            SaveFileDialog dialogoGuardar = new SaveFileDialog();
            dialogoGuardar.AddExtension = true;
            dialogoGuardar.DefaultExt = ".dbf";



            if (dialogoGuardar.ShowDialog() == DialogResult.OK)
            {

                string ruta = dialogoGuardar.FileName;

                Stream ops = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                DotNetDBF.DBFWriter escribir = new DotNetDBF.DBFWriter();
                escribir.DataMemoLoc = ruta.Replace("dbf", "dbt");

                DotNetDBF.DBFField c1 = new DotNetDBF.DBFField("Inrfc", DotNetDBF.NativeDbType.Char, 13);
                DotNetDBF.DBFField c2 = new DotNetDBF.DBFField("Innom", DotNetDBF.NativeDbType.Char, 30);
                DotNetDBF.DBFField c3 = new DotNetDBF.DBFField("Inpro", DotNetDBF.NativeDbType.Char, 11);
                DotNetDBF.DBFField c4 = new DotNetDBF.DBFField("Innomina", DotNetDBF.NativeDbType.Char, 7);
                DotNetDBF.DBFField c5 = new DotNetDBF.DBFField("Inimp", DotNetDBF.NativeDbType.Numeric, 8, 2);
                DotNetDBF.DBFField c6 = new DotNetDBF.DBFField("Infolio", DotNetDBF.NativeDbType.Numeric, 6, 0);
                DotNetDBF.DBFField c7 = new DotNetDBF.DBFField("Inpag", DotNetDBF.NativeDbType.Numeric, 3, 0);
                DotNetDBF.DBFField c8 = new DotNetDBF.DBFField("Intpag", DotNetDBF.NativeDbType.Numeric, 3, 0);
                DotNetDBF.DBFField c9 = new DotNetDBF.DBFField("Indact", DotNetDBF.NativeDbType.Numeric, 1, 0);

                DotNetDBF.DBFField[] campos = new DotNetDBF.DBFField[] { c1, c2, c3, c4, c5, c6, c7, c8, c9 };
                escribir.Fields = campos;

                foreach (Dictionary<string, object> item in resultado)
                {

                    string rfcAux = Convert.ToString(item["rfc"]).Trim();

                    if (rfcAux.Length <= 10) {
                        query = $"select rfc from datos.empleados where rfc like '%{rfcAux}%'";
                        List<Dictionary<string, object>> resulAux = globales.consulta(query);
                        if (resulAux.Count != 0) {
                            item["rfc"] = resulAux[0]["rfc"];
                        }

                    }

                    List<object> record = new List<object> {
                        item["rfc"],
                        item["nombre_em"],
                        item["proyecto"],
                        item["tipo_rel"],
                        item["imp_unit"],
                        item["folio"],
                        item["numdesc"],
                        item["totdesc"],
                        0
                    };

                    escribir.AddRecord(record.ToArray());
                }

                escribir.Write(ops);
                escribir.Close();
                ops.Close();

                globales.MessageBoxSuccess("Archivo .DBF generado exitosamente", "Archivo generado", this);
                globales.MessageBoxInformation(string.Format("El archivo generado ( {0}.DBF ) tiene {1} registros.", nombreDbf, resultado.Count), "Archivo generado", this);
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }

}

