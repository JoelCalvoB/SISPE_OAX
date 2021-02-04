using ExcelDataReader;
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
using System.Data.OleDb;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.NOMINAS_ESPECIALES
{
    public partial class frmNomEspecial : Form
    {
        bool bandera = true;
        public frmNomEspecial()
        {
            InitializeComponent();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tipo_pago = "N";
            string filePath = "";
            DialogResult p = open1.ShowDialog();
            if (p == DialogResult.OK)
            {



                DialogResult resu = globales.MessageBoxQuestion("Archivo seleccionado ¿Deseas continuar con el registro de los datos seleccionados al SISPE?","Aviso",this);
                if (resu == DialogResult.No) return;

                    filePath = open1.FileName;

                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet();
                            string query = string.Empty;
                            // Ejemplos de acceso a datos
                            DataTable table = result.Tables[0];
                            DataColumn column = table.Columns[0];    // columna
                            DataRow row = table.Rows[0];               // fila
                            string valor = table.Rows[0][0].ToString();
                            try
                            {
                                foreach (DataRow item in table.Rows)
                                {
                                    if (bandera == true)
                                    {
                                        bandera = false;
                                        continue;
                                    }
                                    string jpp = item[0].ToString();
                                    if (jpp == "jpp") continue;
                                    string num = item[1].ToString();
                                    string clave = item[2].ToString();
                                string monto =Convert.ToString(item[3]);
                                    string leyen = item[4].ToString();

                                string queryclave = $"SELECT descri FROM nominas_catalogos.perded where clave={clave}";
                                List<Dictionary<string, object>> re = globales.consulta(queryclave);
                                if (string.IsNullOrWhiteSpace(Convert.ToString(re[0]["descri"])))
                                {
                                    DialogResult error = globales.MessageBoxError("NO EXISTE UN CLAVE EN EL CATÁLOGO", "UPSS", globales.menuPrincipal);
                                    return;
                                }
                                string descri = Convert.ToString(re[0]["descri"]);
                                string opcion = string.Empty;
                       
                                //checa

                                if (radioButton1.Checked)

                                {
                                    if (comboBox1.SelectedIndex == 0) opcion = "AG";  // aguinaldo
                                    if (comboBox1.SelectedIndex == 1) opcion = "CA";   // canasta
                                    if (comboBox1.SelectedIndex == 2) opcion = "DM"; //   madres
                                    if (comboBox1.SelectedIndex == 3) opcion = "UT";    // utiles 
                                    if (comboBox1.SelectedIndex == 4) opcion = "AGPDO";    // utiles 
                                    if (comboBox1.SelectedIndex == 5) opcion = "AGPTA";    // utiles 
                                    if (comboBox1.SelectedIndex == 6) opcion = "AGPEA";    // utiles 



                                    query += $" insert into nominas_catalogos.nominew (jpp,numjpp,clave,secuen,monto,leyen,descri,tipopago,tipo_nomina) values('{jpp}',{num},{clave},{1},{monto},'{leyen}','{descri}','{tipo_pago}','{opcion}');";
                                }
                                else if (radioButton2.Checked) 
                                    // NORMAL
                                {
                                    query += $" insert into nominas_catalogos.nominew (jpp,numjpp,clave,secuen,monto,leyen,descri,tipopago, tipo_nomina) values('{jpp}',{num},{clave},{1},{monto},'{leyen}',''{descri},'{tipo_pago}','N');";

                                }

                            }

                                //ee
                                globales.consulta(query);
                                DialogResult dialo = globales.MessageBoxSuccess("INSERCIÓN MASIVA HECHA CON EXITO", "TERMINADO", globales.menuPrincipal);
                            }
                            catch
                            {
                                DialogResult dialogoerror = globales.MessageBoxError("CONTACTE A SISTEMAS,SURGIO UN ERROR", "ERROR", globales.menuPrincipal);
                            }
                        }

                    
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel no se encuentra Instalado");
                return;
            }


            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "jpp";
            xlWorkSheet.Cells[1, 2] = "num";
            xlWorkSheet.Cells[1, 3] = "clave";
            xlWorkSheet.Cells[1, 4] = "importe";
            xlWorkSheet.Cells[1, 5] = "leyen";




            xlWorkBook.SaveAs("C:\\ESTRUCTURA.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            string directorio = "C:\\ESTRUCTURA.xls";

            string xlsPath = Path.Combine(Application.StartupPath, directorio);


            Process.Start(xlsPath);
            Cursor.Current = Cursors.Default;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmNomEspecial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F5)
            {
                DialogResult dialo = globales.MessageBoxQuestion("¿DESEA ELIMINAR LAS NÓMINAS ESPECIALES?, NO SE PODRÁ RECUPERAR!!!", "AVISO", globales.menuPrincipal);
                if (dialo == DialogResult.No) return;
             

                string query = "delete from nominas_catalogos.nominew where tipo_nomina <> 'N'";
                globales.consulta(query);

                DialogResult exito = globales.MessageBoxSuccess("SE ELIMINARON NÓMINAS ESPECIALES", "PROCESO TERMINADO", globales.menuPrincipal);
            }
        }
    }
}
