using SISPE_MIGRACION.codigo.herramientas.forms;
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

namespace SISPE_MIGRACION.formularios.CATÁLOGOS
{
    public partial class frmsaldospendientes : Form
    {
        private List<Dictionary<string, object>> lista;

        public frmsaldospendientes()
        {
            InitializeComponent();
        }

        private void btngenerar_Click(object sender, EventArgs e)
        {
            seleccionar s = new seleccionar();
            s.ShowDialog();
            if (!s.seleccionado) return;

            DateTime fecha1 = DateTime.Parse(fec1.Text);
            DateTime fecha2 = DateTime.Parse(fec2.Text);

            this.Cursor = Cursors.AppStarting;


            string c1 = string.Format("{0:yyyy-MM-dd}", fecha1);
            string c2 = string.Format("{0:yyyy-MM-dd}", fecha2);


            string query = "delete from datos.r_saldos_p; "+
    $" insert into datos.r_saldos_p select rfc,(sum(entrada)) as saldo,'{fecha2.Month}' as mes,'{fecha2.Year}' as anio from datos.aportaciones where COALESCE(status, '') = 'p' and fecharegistro between '{c1}' and '{c2}' group by rfc; ";


            globales.consulta(query);

            string query2 = "SELECT emp.rfc, emp.nombre_em, emp.proyecto, sal.saldo, emp.nap, sal.mes, sal.anio FROM ( datos.empleados emp  JOIN datos.r_saldos_p sal ON(( (emp.rfc) ::TEXT = (sal.rfc) ::TEXT))) WHERE( (sal.saldo<>(0) :: NUMERIC) AND(emp.pendiente = TRUE)) ORDER BY emp.nombre_em";

            List<Dictionary<string, object>> lista = globales.consulta(query2);
            if (!s.esDbf)
            {
                object[] aux2 = new object[lista.Count];
            int contador = 0;
                foreach (Dictionary<string, object> item in lista)
            {
                string nombre_em = string.Empty;
                string rfc = string.Empty;
                string proyecto = string.Empty;
                string nap = string.Empty;
                double saldo = 0;

                try
                {
                    nombre_em = Convert.ToString(item["nombre_em"]);
                    rfc = Convert.ToString(item["rfc"]);
                    proyecto = Convert.ToString(item["proyecto"]);
                    nap = Convert.ToString(item["nap"]);
                    saldo = Convert.ToDouble(item["saldo"]);
                }
                catch
                {

                }

                object[] tt1 = { nombre_em, rfc, proyecto, nap, saldo };
                aux2[contador] = tt1;
                contador++;



            }
            this.Cursor = Cursors.Default;

                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                object[] parametros = { "fech1", "fech2", "tiporeporte","mov", "logo" };
                object[] valor = { this.fec1.Text, this.fec2.Text, "PENDIENTES","", logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

     globales.reportes("frmreportsaldo", "rsaldos_aporta", aux2, "", false, enviarParametros);
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

                    DotNetDBF.DBFField a1 = new DotNetDBF.DBFField("RFC", DotNetDBF.NativeDbType.Char, 20);
                    DotNetDBF.DBFField a2 = new DotNetDBF.DBFField("NOMBRE", DotNetDBF.NativeDbType.Char, 100);
                    DotNetDBF.DBFField a3 = new DotNetDBF.DBFField("PROYECTO", DotNetDBF.NativeDbType.Char, 35);
                    DotNetDBF.DBFField a4 = new DotNetDBF.DBFField("SALDO", DotNetDBF.NativeDbType.Numeric, 10, 2);
                    DotNetDBF.DBFField a5 = new DotNetDBF.DBFField("NAP", DotNetDBF.NativeDbType.Numeric, 10, 2);


                    DotNetDBF.DBFField[] campos = new DotNetDBF.DBFField[] { a1, a2, a3, a4, a5 };
                    escribir.Fields = campos;

                    foreach (Dictionary<string, object> item in lista)
                    {
                        List<object> record = new List<object> {
                        item["rfc"],
                        item["nombre_em"],
                        item["proyecto"],
                        (string.IsNullOrWhiteSpace(Convert.ToString(item["saldo"])))?0:Convert.ToDouble(item["saldo"]),
                        string.IsNullOrWhiteSpace(Convert.ToString(item["nap"]))?0:Convert.ToDouble(item["nap"])
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

        private void frmsaldospendientes_Load(object sender, EventArgs e)
        {
           // fec1.Text = string.Format("{0:d}",DateTime.Now);
            fec2.Text = string.Format("{0:d}", DateTime.Now);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
         
                Owner.Close();
        }

        private void frmsaldospendientes_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = this.fec1;
        }

        private void frmsaldospendientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) {
                button3_Click(null,null);
            }
        }

        private void fec1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fec2.Select();

            }
        }
        private void fec2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btngenerar.Select();

            }
        }

           
        }
    }

