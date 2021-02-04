using ExcelDataReader;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.HISTORICO
{
    public partial class frmHistoricoListados : Form
    {
        private string tipo_nomina;
        private List<Dictionary<string, object>> resultadoAux;
        private string archivo;
        private bool pagoRetro;
        private string nombre;
        private object fechabetween;

        public frmHistoricoListados()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tipo_nomina = string.Empty;
            if (radioButton1.Checked) tipo_nomina = "AG";
            if (radioButton2.Checked) tipo_nomina = "CA";
            if (radioButton3.Checked) tipo_nomina = "DM";
            if (radioButton4.Checked) tipo_nomina = "UT";
            if (radioButton5.Checked) tipo_nomina = "CAN2";
            if (radioButton6.Checked) tipo_nomina = "PF";
            if (radioButton7.Checked) tipo_nomina = "E";



            btnGuardar.Visible = false;


            archivo = string.Empty;


            archivo = txtAño.Text.Substring(2) + ((cmbMes.SelectedIndex + 1 < 10) ? $"0{cmbMes.SelectedIndex + 1}" : (cmbMes.SelectedIndex + 1).ToString());

            string mes = ((cmbMes.SelectedIndex + 1 < 10) ? $"0{cmbMes.SelectedIndex + 1}" : (cmbMes.SelectedIndex + 1).ToString());
            DateTime tiempo1 = new DateTime(Convert.ToInt32(DateTime.Now.Year), Convert.ToInt32(mes), 1);
            DateTime tiempo2 = new DateTime(Convert.ToInt32(DateTime.Now.Year), Convert.ToInt32(mes), 1).AddMonths(1).AddDays(-1);

            string mesStr = globales.getMeses()[Convert.ToInt32(mes)];

            fechabetween = $"DEL {string.Format("{0:dd}", tiempo1)} AL {tiempo2.Day} DE {mesStr.ToUpper()} DEL {txtAño.Text}";

            if (cmbSalida.SelectedIndex == 0 || cmbSalida.SelectedIndex == 1)
            {
                this.generarListadoDeduccion(cmbSalida.SelectedIndex);
            }
            else if (cmbSalida.SelectedIndex == 2 || cmbSalida.SelectedIndex == 3)
            {
                this.generarListadoAlfaConLiquido(cmbSalida.SelectedIndex);
            }
            else if (cmbSalida.SelectedIndex == 4)
            {
                this.generarListadoAlfabetico();
            }
            else if (cmbSalida.SelectedIndex == 5 || cmbSalida.SelectedIndex == 6)
            {
                this.generarDiscoRecursos(cmbSalida.SelectedIndex);
            }
            else if (cmbSalida.SelectedIndex == 7)
            {
                timbrar();
            }
            else if (cmbSalida.SelectedIndex == 8)
            {
                listadoAportaciones();
            }
        }

        private void generarDiscoRecursos(int selectedIndex)
        {

            List<Dictionary<string, object>> resultado = null;
            SaveFileDialog dialogoGuardar;
            string especial;
            string mes;
            string query = string.Empty;
            if (selectedIndex == 5)
            {
                DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar el listado y disco para recursos humanos?", "Aviso", globales.menuPrincipal);

                if (dialogo == DialogResult.No) return;





                especial = this.chknomina.Checked ? $" nno.tipo_nomina = '{this.tipo_nomina}' " : " nno.tipo_nomina='N' ";

                query = "select mma.nombre,mma.rfc,mma.jpp,mma.num,concat(mma.jpp,mma.num) as proyecto,nno.clave  ,nno.pago4 as pagon ,0 as cheque ,nno.pagot ," +
                      " nno.monto  ,nno.tipo_pago as tipopago ,nno.folio , nno.fechaini ,nno.fechafin " +
                      " from nominas_catalogos.maestro mma inner join nominas_catalogos.respaldos_nominas nno on mma.jpp = nno.jpp and mma.num = nno.numjpp  " +
                      $" and nno.clave in (227,226,221) and {especial} and archivo = '{archivo}'  order by mma.jpp,mma.num,nno.clave";

                resultado = globales.consulta(query);
                mes = this.cmbMes.SelectedIndex < 5 ? $"0{(this.cmbMes.SelectedIndex + 1) * 2}" : Convert.ToString((this.cmbMes.SelectedIndex + 1) * 2);

                dialogoGuardar = new SaveFileDialog();
                dialogoGuardar.AddExtension = true;
                dialogoGuardar.DefaultExt = ".dbf";
                dialogoGuardar.FileName = $"J480{mes}{txtAño.Text.Substring(2)}";

                if (dialogoGuardar.ShowDialog() == DialogResult.OK)
                {
                    string ruta = dialogoGuardar.FileName;

                    Stream ops = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                    DotNetDBF.DBFWriter escribir = new DotNetDBF.DBFWriter();
                    escribir.DataMemoLoc = ruta.Replace("dbf", "dbt");

                    DotNetDBF.DBFField c1 = new DotNetDBF.DBFField("NOMBRE", DotNetDBF.NativeDbType.Char, 40);
                    DotNetDBF.DBFField c2 = new DotNetDBF.DBFField("RFC", DotNetDBF.NativeDbType.Char, 13);
                    DotNetDBF.DBFField c3 = new DotNetDBF.DBFField("PROYECTO", DotNetDBF.NativeDbType.Char, 17);
                    DotNetDBF.DBFField c4 = new DotNetDBF.DBFField("CVEDESC", DotNetDBF.NativeDbType.Numeric, 3);
                    DotNetDBF.DBFField c5 = new DotNetDBF.DBFField("FOLIO", DotNetDBF.NativeDbType.Numeric, 6);
                    DotNetDBF.DBFField c6 = new DotNetDBF.DBFField("NUMDESC", DotNetDBF.NativeDbType.Numeric, 3);
                    DotNetDBF.DBFField c7 = new DotNetDBF.DBFField("TOTDESC", DotNetDBF.NativeDbType.Numeric, 3);
                    DotNetDBF.DBFField c8 = new DotNetDBF.DBFField("IMPORTE", DotNetDBF.NativeDbType.Numeric, 11, 2);
                    DotNetDBF.DBFField c9 = new DotNetDBF.DBFField("REGISTRO", DotNetDBF.NativeDbType.Numeric, 10);

                    DotNetDBF.DBFField[] campos = new DotNetDBF.DBFField[] { c1, c2, c3, c4, c5, c6, c7, c8, c9 };
                    escribir.Fields = campos;

                    foreach (Dictionary<string, object> item in resultado)
                    {
                        string nombre = Convert.ToString(item["nombre"]);
                        string rfc = Convert.ToString(item["rfc"]);
                        string jpp = Convert.ToString(item["jpp"]);
                        string num = Convert.ToString(item["num"]);
                        string proyecto = jpp + num;
                        int clave = globales.convertInt(Convert.ToString(item["clave"]));
                        int folio = globales.convertInt(Convert.ToString(item["folio"]));
                        int numdesc = globales.convertInt(Convert.ToString(item["pagon"]));
                        int totdesc = globales.convertInt(Convert.ToString(item["pagot"]));
                        double importe = globales.convertDouble(Convert.ToString(item["monto"]));
                        int registro = globales.convertInt(Convert.ToString(item["cheque"]));


                        List<object> record = new List<object> {
                                nombre,rfc,proyecto,clave,folio,numdesc,totdesc,
                                importe,registro
                            };

                        escribir.AddRecord(record.ToArray());
                    }


                    escribir.Write(ops);
                    escribir.Close();
                    ops.Close();

                    globales.MessageBoxSuccess("Archivo .DBF generado exitosamente", "Archivo generado", globales.menuPrincipal);

                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtclave.Text))
                {
                    globales.MessageBoxExclamation("Favor de elegir clave de quincena", "Clave quincena", globales.menuPrincipal);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtnomina.Text))
                {
                    globales.MessageBoxExclamation("Favor de elegir clave de nomina", "Clave nomina", globales.menuPrincipal);
                    return;
                }

                globales.MessageBoxInformation("Se generara el archivo base", "Archivo base", globales.menuPrincipal);

                dialogoGuardar = new SaveFileDialog();
                dialogoGuardar.AddExtension = true;
                dialogoGuardar.DefaultExt = ".dbf";
                dialogoGuardar.FileName = $"BASE{archivo}";

               
                    // string ruta = dialogoGuardar.FileName;
                    var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    string ruta = Path.Combine(projectPath, "Resources\\humanos.dbf");
                    especial = this.chknomina.Checked ? $" tipo_nomina = '{this.tipo_nomina}' " : " tipo_nomina='N' ";

                if (dialogoGuardar.ShowDialog() == DialogResult.OK)
                {
                    ruta = dialogoGuardar.FileName;
                }
                else
                {

                    return;
                }


                    string cadena = "Provider=VFPOLEDB.1; Data Source= {0}\\; Extended Properties =dBase IV; ";
                    string pasa = string.Format(cadena, ruta.Substring(0, ruta.LastIndexOf("\\")));

                    using (OleDbConnection connection = new OleDbConnection(pasa))
                    using (OleDbCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        string queryClaves = $"select clave from nominas_catalogos.respaldos_nominas where {especial} and archivo = '{archivo}' group by clave order by clave";
                        List<Dictionary<string, object>> claves = globales.consulta(queryClaves);

                    ir:

                        string tipo_pago = !pagoRetro ? "N" : "R";

                        query = $"create temp table t1 as select JPP,numjpp from nominas_catalogos.respaldos_nominas where  {especial} and archivo = '{archivo}' and tipo_pago = '{tipo_pago}' group by jpp,numjpp; " +
                            $" create temp table sacar as select JPP,numjpp,clave,sum(monto) as monto from nominas_catalogos.respaldos_nominas where   {especial} and archivo = '{archivo}' and tipo_pago = '{tipo_pago}' group by jpp,numjpp,clave; " +
                           " create temp table t2 as  select mma.rfc,mma.nombre,mma.jpp,mma.num,mma.sexo,mma.curp,mma.banco,    mma.cuentabanc,	mma.fching from nominas_catalogos.maestro mma inner join t1 on mma.jpp = t1.jpp and mma.num = t1.numjpp  " +
                           " where  mma.jpp <> 'PEA'  ORDER BY mma.jpp,mma.num; ";


                        string selectPart = string.Empty;
                        string leftJoin = string.Empty;
                        string sumaPart = string.Empty;


                        string query2 = "select  clave,descri from nominas_catalogos.perded order by clave";
                        List<Dictionary<string, object>> perded = globales.consulta(query2);



                        //       string consultaperded = "select  clave,descri from nominas_catalogos.perded order by clave";
                        //     List<Dictionary<string, object>> perded = globales.consulta(consultaperded);

                        foreach (Dictionary<string, object> item in claves)
                        {

                            string descripcion = perded.Where(p => Convert.ToString(item["clave"]) == Convert.ToString(p["clave"])).First()["descri"] as string;
                            int clave = globales.convertInt(Convert.ToString(item["clave"]));
                            string queEs = clave > 68 ? "D" : "P";
                            //--------------ESTA PARTE ES PARA QUE SALGA EL NOMBRE EN LA COLUMNA DEL ARCHIVO COMO PENSION_MENSUAL,PSM , ETC..
                            //selectPart += $", COALESCE(sum(nn{clave}.monto),0) as {descripcion.Replace(" ","_").ToUpper().Replace(".","")} ";

                            selectPart += $",COALESCE(sum(nn{clave}.monto),0){queEs}{clave}"; //Así es como esta ORIGINALMENTE (NO TOCAR)
                            leftJoin += $" left join sacar nn{clave}  " +
                                $" on nn{clave}.jpp = t2.jpp and nn{clave}.numjpp = t2.num and nn{clave}.clave = {clave} ";

                            sumaPart += (clave > 68) ? $"- COALESCE(sum(nn{clave}.monto),0) " : $" + COALESCE(sum(nn{clave}.monto),0)";
                        }

                        query += $" select t2.* { selectPart},{sumaPart} from t2 {leftJoin} group by t2.rfc,t2.nombre,t2.jpp,t2.num,t2.sexo,t2.curp, t2.banco,  t2.cuentabanc,  t2.fching order by t2.jpp,t2.num ";
                    if (string.IsNullOrWhiteSpace(selectPart) && string.IsNullOrWhiteSpace(sumaPart)) {

                        globales.MessageBoxExclamation("No hay información solicitada en el mes seleccionado","Aviso",globales.menuPrincipal);
                        Cursor = Cursors.Default;
                        return;
                    }

                    resultado = globales.consulta(query);


                    if (!pagoRetro)
                        {
                            string tabla = @"CREATE TABLE {0} (CQNACVE   N( 6, 0), CQNAIND   N( 2, 0), PBPNUP    N( 6, 0), PBPNUE    N( 6, 0), SIRFC     C(13, 0)," +
                            "SINOM     C(45, 0), SIDEP     C(20, 0), SICATG    C( 7, 0), SISX      C( 1, 0), TIP_EMP   N( 2, 0)," +
                            " NUMFOLIO  N( 6, 0), UBICA     C( 1, 0), CVE       N( 3, 0), TIPOPAGO  N( 1, 0), PBPFIG    D( 8, 0)," +
                            " PBPIPTO   D( 8, 0), PBPFNOMB  D( 8, 0), PBPSTATUS N( 2, 0), LICDES    D( 8, 0), LICHAS    D( 8, 0)," +
                            "CMOTCVE   N( 3, 0), PBPHIJOS  N( 1, 0), GUADES    D( 8, 0), GUAHAS    D( 8, 0), CURP      C(21, 0)," +
                            "AREAADS   C(70, 0), NOMBANCO  C(20, 0), NUMCUENTA C(12, 0), CLAVE_INTE C(20, 0), CESTNIV   N( 2, 0)," +
                            "CESTGDO   N( 1, 0), QNIOS     N( 1, 0), PBPIMSS   C(11, 0), BGIMSS    N(12, 2), CUOPIMSS  N(13, 2)," +
                            "CUOPRCV   N(13, 2), INCDES    D( 8, 0), INCHAS    D( 8, 0), CUOPINF   N(13, 2), CUOPFPEN  N(13, 2)," +
                            "BGISPT    N(12, 2), PROFESION N( 4, 0), STATPAGO  C(180, 0), AGUIFDES  D( 8, 0), AGUIFHAS  D( 8, 0)," +
                            "DAFDES    D( 8, 0), DAFHAS    D( 8, 0),{1} , NFALTA N(4,2) , NRETAR N(4,2))";

                            string PercepcionesDeducciones = "";
                            foreach (Dictionary<string, object> item in claves)
                            {
                                int clave = globales.convertInt(Convert.ToString(item["clave"]));
                                PercepcionesDeducciones += (clave < 69) ? $"P{clave} N( 13, 2 )," : $"D{clave} N( 13, 2 ),";
                            }
                            PercepcionesDeducciones = PercepcionesDeducciones.Substring(0, PercepcionesDeducciones.Length - 1);

                            nombre = ruta.Substring(ruta.LastIndexOf("\\") + 1);
                            nombre = nombre.Split('.')[0];
                            tabla = string.Format(tabla, nombre, PercepcionesDeducciones);
                            command.CommandText = tabla;
                            command.ExecuteNonQuery();
                        }






                        foreach (Dictionary<string, object> item in resultado)
                        {
                            int CQNACVE = Convert.ToInt32(txtclave.Text);
                            int CQNAIND = Convert.ToInt32(txtnomina.Text);
                            int PBPNUP = 0;
                            int PBPNUE = 0;
                            string SIRFC = Convert.ToString(item["rfc"]);

                            string SINOM = Convert.ToString(item["nombre"]);
                            string SIDEP = Convert.ToString(item["jpp"]) + Convert.ToString(item["num"]);
                            string SICATG = "";
                            string SISX = Convert.ToString(item["sexo"]);
                            int TIP_EMP = 0;

                            string jpp = Convert.ToString(item["jpp"]);
                            if (jpp == "JUB")
                            {
                                TIP_EMP = 13;
                            }
                            else if (jpp == "PTA")
                            {
                                TIP_EMP = 14;
                            }
                            else if (jpp == "PDO")
                            {
                                TIP_EMP = 15;
                            }
                            else
                            {
                                TIP_EMP = 0;
                            }

                            int tipoppp = (pagoRetro) ? 1 : cmb2.SelectedIndex;

                            int NUMFOLIO = 0;
                            string UBICA = "3";
                            int CVE = 470;
                            int TIPOPAGO = tipoppp;
                            string PBPFIG = "";

                            string PBPIPTO = string.Empty;
                            string PBPFNOMB = string.Empty;
                            int PBPSTATUS = 11;
                            string LICDES = string.Empty;
                            string LICHAS = string.Empty;

                            int CMOTCVE = 0;
                            int PBPHIJOS = 0;
                            string GUADES = string.Empty;
                            string GUAHAS = string.Empty;
                            string CURP = Convert.ToString(item["curp"]);

                            string AREAADS = "";
                            string NOMBANCO = "";
                            string NUMCUENTA = "";
                            string CLAVE_INTE = "";
                            int CESTNIV = 0;

                            int CESTGDO = 0;
                            int QNIOS = 0;
                            string PBPIMSS = "";
                            double BGIMSS = 0;
                            double CUOPIMSS = 0;

                            double CUOPRCV = 0;
                            string INCDES = string.Empty;
                            string INCHAS = string.Empty;
                            double CUOPINF = 0;
                            double CUOPFPEN = 0;

                            double BGISPT = 0;
                            int PROFESION = 0;
                            string STATPAGO = "";
                            string AGUIFDES = string.Empty;
                            string AGUIFHAS = string.Empty;

                            string DAFDES = string.Empty;
                            string DAFHAS = string.Empty;

                            string sentencia = "INSERT INTO {0}({1},{3},{5},{7},{9},{11},{13},{15},{17},{19} {21}) VALUES({2},{4},{6},{8},{10},{12},{14},{16},{18},{20} {22})";

                            string part1 = "CQNACVE, CQNAIND, PBPNUP, PBPNUE, SIRFC";
                            string part11 = $"{CQNACVE},{ CQNAIND},{ PBPNUP},{ PBPNUE},'{SIRFC}'";

                            string part2 = "SINOM,SIDEP,SICATG,SISX,TIP_EMP";
                            string part22 = $"'{SINOM}','{SIDEP}','{SICATG}','{SISX}',{TIP_EMP}";

                            string part3 = "NUMFOLIO,UBICA,CVE,TIPOPAGO, PBPFIG";
                            string part33 = $"{NUMFOLIO},'{UBICA}',{CVE},{TIPOPAGO},CTOD('{PBPFIG}')";

                            string part4 = "PBPIPTO,PBPFNOMB,PBPSTATUS,LICDES,LICHAS";
                            string part44 = $"CTOD('{PBPIPTO}'),CTOD('{PBPFNOMB}'),{PBPSTATUS},CTOD('{LICDES}'),CTOD('{LICHAS}')";

                            string part5 = "CMOTCVE,PBPHIJOS,GUADES,GUAHAS,CURP";
                            string part55 = $"{CMOTCVE},{PBPHIJOS},CTOD('{GUADES}'),CTOD('{GUAHAS}'),'{CURP}'";

                            string part6 = "AREAADS,NOMBANCO,NUMCUENTA,CLAVE_INTE,CESTNIV";
                            string part66 = $"'{AREAADS}','{NOMBANCO}','{NUMCUENTA}','{CLAVE_INTE}',{CESTNIV}";

                            string part7 = "CESTGDO,QNIOS,PBPIMSS,BGIMSS,CUOPIMSS";
                            string part77 = $"{CESTGDO},{QNIOS},'{PBPIMSS}',{BGIMSS},{CUOPIMSS}";

                            string part8 = "CUOPRCV,INCDES,INCHAS,CUOPINF,CUOPFPEN";
                            string part88 = $"{CUOPRCV},CTOD('{INCDES}'),CTOD('{INCHAS}'),{CUOPINF},{CUOPFPEN}";

                            string part9 = "BGISPT,PROFESION,STATPAGO,AGUIFDES,AGUIFHAS";
                            string part99 = $"{BGISPT},{PROFESION},'{STATPAGO}',CTOD('{AGUIFDES}'),CTOD('{AGUIFHAS}')";

                            string part10 = "DAFDES, DAFHAS";
                            string part1010 = $"CTOD('{DAFDES}'),CTOD('{DAFDES}')";

                            string columna = "";
                            string valorColumna = "";
                            foreach (Dictionary<string, object> clav in claves)
                            {
                                int clave = globales.convertInt(Convert.ToString(clav["clave"]));
                                string costr = clave > 68 ? "D" : "P";
                                string llave = $"{costr}{clave}";
                                string llave2 = $"{costr.ToLower()}{clave}";
                                columna += $",{llave}";
                                valorColumna += $" ,{item[llave2]} ";

                            }




                            sentencia = string.Format(sentencia, nombre, part1, part11, part2, part22, part3, part33, part4, part44, part5, part55,
                                part6, part66, part7, part77, part8, part88, part9, part99, part10, part1010, columna, valorColumna);
                            command.CommandText = sentencia;
                            command.ExecuteNonQuery();


                        }

                        if (!pagoRetro)
                        {
                            pagoRetro = true;
                            goto ir;
                        }

                        connection.Close();


                    //empieza especial

                    if (this.chknomina.Checked)
                    {
                        projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                        string filePath = Path.Combine(projectPath, "Resources");

                        if (!File.Exists(ruta.Substring(0, ruta.LastIndexOf("\\")) + @"\estructura.dbf"))
                            File.Copy(filePath + @"\estructura.dbf", ruta.Substring(0, ruta.LastIndexOf("\\")) + @"\estructura.dbf");
                        else {
                            File.Delete(ruta.Substring(0, ruta.LastIndexOf("\\")) + @"\estructura.dbf");
                            File.Copy(filePath + @"\estructura.dbf", ruta.Substring(0, ruta.LastIndexOf("\\")) + @"\estructura.dbf");
                        }


                      //  string cadena2 = $"Provider=VFPOLEDB.1; Data Source= {ruta.Substring(0, ruta.LastIndexOf("\\"))}\\; Extended Properties =dBase IV; ";

                        connection.Open();

                    string vacia = "use estructura";
                    command.CommandText = vacia;
                    command.ExecuteNonQuery();
                       // string vacia1 = " delete from estructura; pack;";
                      //  command.CommandText = vacia1;
                       // command.ExecuteNonQuery();


                    string deduc = string.Empty;
                        if (tipo_nomina == "AG") deduc = "P34,P59,D217";
                        if (tipo_nomina == "CA") deduc = "P49,D217";
                        if (tipo_nomina == "DM") deduc = "D106";
                        if (tipo_nomina == "UT") deduc = "D107";
                        if (tipo_nomina == "CAN2") deduc = "P49,D217";
                    if (tipo_nomina == "PF") deduc = "P49";


                        string texto1 = "INSERT INTO estructura(cqnacve, cqnaind, pbpnup, pbpnue, sirfc, sinom, sidep, sicatg, sisx, tip_emp, numfolio, cve, tipopago, pbpfnomb, pbpstatus, licdes, lichas, cmotcve, pbphijos, guades, guahas, curp, cestniv, cestgdo, qnios, pbpimss, " +
                            $" bgimss, cuopimss, cuoprcv, incdes, inchas, cuopinf, cuopfpen, bgispt, profesion, statpago, aguifdes, aguifhas, dafdes, dafhas,pbpfig,pbpipto,nfalta,nretar, {deduc}) SELECT cqnacve, cqnaind, pbpnup, pbpnue, sirfc, sinom, sidep, sicatg, sisx, tip_emp, numfolio, cve, " +
                               $" tipopago, pbpfnomb, pbpstatus, licdes, lichas, cmotcve, pbphijos, guades, guahas, curp, cestniv, cestgdo, qnios, pbpimss, bgimss, cuopimss, cuoprcv, incdes, inchas, cuopinf, cuopfpen, bgispt, profesion, statpago, aguifdes, aguifhas, dafdes, dafhas,pbpfig,pbpipto,nfalta,nretar, {deduc} FROM {nombre}";


                    command.CommandText = texto1;
                    command.ExecuteNonQuery();

                    string trae = $"SELECT * FROM catalogos.estructura;";
                        List<Dictionary<string, object>> resulta = globales.consulta(trae);

                    
         
                    foreach (var item in resulta)
                    {
                        string rellena = string.Empty;
                        string var = Convert.ToString(item["descripcion"]);
                            if (Convert.ToBoolean(item["bandera"]) == true)
                        {


                                rellena = $"update estructura set {var} =''  where ISNULL({var});";
                            try
                            {
                                command.CommandText = rellena;
                                command.ExecuteNonQuery();

                            }
                            catch
                            {
                                    rellena = $"update estructura set {var} = 0  where isnull({var});";
                                command.CommandText = rellena;
                                command.ExecuteNonQuery();

                            }

                        }
                        else
                        {
                                rellena = $"update estructura set {var} = 0  where  isnull({var});";
                            try
                            {
                                command.CommandText = rellena;
                                command.ExecuteNonQuery();

                            }
                            catch
                            {

                            }
                        }
                     

                    }

                       
                    connection.Close();

                        File.Delete(ruta);

                        File.Copy(ruta.Substring(0, ruta.LastIndexOf("\\")) + @"\estructura.dbf",ruta);

                        File.Delete(ruta.Substring(0, ruta.LastIndexOf("\\")) + @"\estructura.dbf");

                        //Termina especial

                        
                    }



                    }

                        globales.MessageBoxSuccess("Archivo .DBF generado exitosamente", "Archivo generado", globales.menuPrincipal);

                
            }



        }

        private void generarListadoAlfabetico()
        {

            string especial = this.chknomina.Checked ? $" tipo_nomina = '{this.tipo_nomina}' " : "tipo_nomina='N' ";


            string query = $"create temp table uniendo as SELECT jpp,numjpp,sum(monto) FROM nominas_catalogos.respaldos_nominas where clave <= 60 and {especial} and archivo =  '{archivo}' group by jpp,numjpp " +
            $" union SELECT jpp,numjpp,(-1 * sum(monto)) FROM nominas_catalogos.respaldos_nominas where clave > 60 and {especial} and archivo = '{archivo}' group by jpp,numjpp; " +
            " create temp table suma as select jpp,numjpp,sum(sum) from uniendo group by jpp,numjpp order by jpp,numjpp; " +
            " select mma.*,ssu.sum from nominas_catalogos.maestro mma inner join suma ssu on ssu.jpp = mma.jpp and ssu.numjpp = mma.num " +
            "  ";

            List<Dictionary<string, object>> resultado = globales.consulta(query);
            btnGuardar.Visible = false;

            object[] obj = new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                string rfc = Convert.ToString(item["rfc"]);
                string jpp = Convert.ToString(item["jpp"]);
                string num = Convert.ToString(item["num"]);
                string liquido = Convert.ToString(item["nomelec"]);
                string nombre = Convert.ToString(item["nombre"]);

                object[] tt1 = { jpp, num, rfc, nombre, liquido };
                obj[contador] = tt1;
                contador++;
            }

            object[][] parametros = new object[2][];
            object[] header = { "encabezado" };
            object[] body = { $"LISTADO ALFABETICO DE JUBILADOS, PENSIONADOS Y PENSIONISTAS" };
            parametros[0] = header;
            parametros[1] = body;


            ReportViewer reporte = globales.reportesParaPanel("nominas_listadoliquidoA", "listadoliquido", obj, "", false, parametros);
            reporte.Dock = DockStyle.Fill;
            panelreporte.Controls.Clear();
            panelreporte.Controls.Add(reporte);

        }

        private void generarListadoAlfaConLiquido(int selectedIndex)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar el listado alfabetico por liquido?", "Aviso", globales.menuPrincipal);

            if (dialogo == DialogResult.No) return;

            btnGuardar.Visible = true;

            globales.MessageBoxInformation("Se va a generar el archivo de banamex", "Archivo banamex", globales.menuPrincipal);

            string especial = this.chknomina.Checked ? $" tipo_nomina = '{this.tipo_nomina}' " : " tipo_nomina='N' ";

            string aux = "create temp table uniendo as SELECT jpp,numjpp,sum(monto) FROM nominas_catalogos.respaldos_nominas where clave <= 60 and {1} and archivo = '{2}' group by jpp,numjpp " +
                " union SELECT jpp,numjpp,(-1 * sum(monto)) FROM nominas_catalogos.respaldos_nominas where clave > 60 and {1} and archivo = '{2}' group by jpp,numjpp; " +
                " create temp table suma as select jpp,numjpp,sum(sum) from uniendo group by jpp,numjpp order by jpp,numjpp; " +
                " select mma.*,ssu.sum from nominas_catalogos.maestro mma inner join suma ssu on ssu.jpp = mma.jpp and ssu.numjpp = mma.num " +
                " where mma.nomelec = 'S' and mma.banco = '{0}'  order by mma.jpp,mma.num ";

            string query = string.Empty;
            List<Dictionary<string, object>> resultado = null;

            if (selectedIndex == 3)
            {
                query = string.Format(aux, "BANAMEX", especial, archivo);
                resultado = globales.consulta(query);
                resultadoAux = resultado;
                generarReporteador(resultado, "BANAMEX");

            }
            else
            {
                query = string.Format(aux, "BANORTE", especial, archivo);
                resultado = globales.consulta(query);
                resultadoAux = resultado;
                generarReporteador(resultado, "BANORTE");
            }

        }

        private void generarReporteador(List<Dictionary<string, object>> resultado, string v)
        {

            object[] obj = new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                string rfc = Convert.ToString(item["rfc"]);
                string jpp = Convert.ToString(item["jpp"]);
                string num = Convert.ToString(item["num"]);
                string liquido = Convert.ToString(item["sum"]);
                string nombre = Convert.ToString(item["nombre"]);

                object[] tt1 = { jpp, num, rfc, nombre, liquido };
                obj[contador] = tt1;
                contador++;
            }

            object[][] parametros = new object[2][];
            object[] header = { "encabezado" };
            object[] body = { $"LISTADO ALFABETICO DE JUBILADOS, PENSIONADOS Y PENSIONISTAS CON LIQUIDO BANCO {v}" };
            parametros[0] = header;
            parametros[1] = body;



            ReportViewer reporte = globales.reportesParaPanel("nominas_listadoliquido", "listadoliquido", obj, "", false, parametros);
            reporte.Dock = DockStyle.Fill;
            panelreporte.Controls.Clear();
            panelreporte.Controls.Add(reporte);

        }

        private void generarArchivosBanco(string bancoStr, List<Dictionary<string, object>> resultado)
        {
            SaveFileDialog dialogoGuardar = new SaveFileDialog();
            dialogoGuardar.AddExtension = true;
            dialogoGuardar.DefaultExt = ".dbf";
            dialogoGuardar.FileName = bancoStr;

            if (dialogoGuardar.ShowDialog() == DialogResult.OK)
            {
                string ruta = dialogoGuardar.FileName;

                Stream ops = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                DotNetDBF.DBFWriter escribir = new DotNetDBF.DBFWriter();
                escribir.DataMemoLoc = ruta.Replace("dbf", "dbt");

                DotNetDBF.DBFField c1 = new DotNetDBF.DBFField("JPP", DotNetDBF.NativeDbType.Char, 3);
                DotNetDBF.DBFField c2 = new DotNetDBF.DBFField("NUM", DotNetDBF.NativeDbType.Numeric, 6, 0);
                DotNetDBF.DBFField c3 = new DotNetDBF.DBFField("NOMBRE", DotNetDBF.NativeDbType.Char, 40);
                DotNetDBF.DBFField c4 = new DotNetDBF.DBFField("NETO", DotNetDBF.NativeDbType.Numeric, 12, 2);
                DotNetDBF.DBFField c5 = new DotNetDBF.DBFField("BANCO", DotNetDBF.NativeDbType.Char, 3);
                DotNetDBF.DBFField c6 = new DotNetDBF.DBFField("TCTA", DotNetDBF.NativeDbType.Char, 2);
                DotNetDBF.DBFField c7 = new DotNetDBF.DBFField("CUENTA", DotNetDBF.NativeDbType.Char, 25);
                DotNetDBF.DBFField c8 = new DotNetDBF.DBFField("RFC", DotNetDBF.NativeDbType.Char, 13);

                DotNetDBF.DBFField[] campos = new DotNetDBF.DBFField[] { c1, c2, c3, c4, c5, c6, c7, c8 };
                escribir.Fields = campos;

                foreach (Dictionary<string, object> item in resultado)
                {
                    string jpp = Convert.ToString(item["jpp"]);
                    double num = Convert.ToDouble(item["num"]);
                    string nombre = Convert.ToString(item["nombre"]);
                    double neto = globales.convertDouble(Convert.ToString(item["sum"]));
                    string banco = "072";
                    string tcta = "01";
                    string cuentabanc = Convert.ToString(item["cuentabanc"]);
                    string rfc = Convert.ToString(item["rfc"]);

                    List<object> record = new List<object> {
                                jpp,num,nombre,neto,banco,tcta,cuentabanc,rfc
                            };

                    escribir.AddRecord(record.ToArray());
                }


                escribir.Write(ops);
                escribir.Close();
                ops.Close();

                globales.MessageBoxSuccess("Archivo .DBF generado exitosamente", "Archivo generado", globales.menuPrincipal);
            }
        }

        private void generarListadoDeduccion(int selectedIndex)
        {
            string query = string.Empty;
            List<Dictionary<string, object>> resultado = null;
            string especial = this.chknomina.Checked ? $" nno.tipo_nomina ='{this.tipo_nomina}' " : " nno.tipo_nomina='N' ";





            if (selectedIndex == 1)
            {
                DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar el historico del archivo de deducción?", "Aviso", globales.menuPrincipal);
                if (dialogo == DialogResult.Yes)
                {
                    string mes = this.cmbMes.SelectedIndex < 5 ? $"0{(this.cmbMes.SelectedIndex + 1) * 2}" : Convert.ToString((this.cmbMes.SelectedIndex + 1) * 2);


                    globales.MessageBoxInformation("Se va a generar el archivo de descuento", "Archivo de descuento", globales.menuPrincipal);
                    query = "select mma.nombre,mma.rfc,concat(mma.jpp,mma.num) as proyecto,nno.clave as cvedesc ,nno.pago4 as numdesc ,nno.pagot as totdesc ," +
                        " nno.monto as importe ,nno.tipo_pago as tipodesc ,nno.folio , nno.fechaini as desde ,nno.fechafin as hasta " +
                        " from nominas_catalogos.maestro mma inner join nominas_catalogos.respaldos_nominas nno on mma.jpp = nno.jpp and mma.num = nno.numjpp  " +
                        $" and nno.clave in (205,206) and archivo = '{archivo}' where  {especial} ";

                    resultado = globales.consulta(query);
                    SaveFileDialog dialogoGuardar = new SaveFileDialog();
                    dialogoGuardar.AddExtension = true;
                    dialogoGuardar.DefaultExt = ".dbf";
                    dialogoGuardar.FileName = $"D980{txtAño.Text.Substring(2)}{mes}";



                    if (dialogoGuardar.ShowDialog() == DialogResult.OK)
                    {
                        string ruta = dialogoGuardar.FileName;

                        Stream ops = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                        DotNetDBF.DBFWriter escribir = new DotNetDBF.DBFWriter();
                        escribir.DataMemoLoc = ruta.Replace("dbf", "dbt");

                        DotNetDBF.DBFField c1 = new DotNetDBF.DBFField("NOMBRE", DotNetDBF.NativeDbType.Char, 40);
                        DotNetDBF.DBFField c2 = new DotNetDBF.DBFField("RFC", DotNetDBF.NativeDbType.Char, 13);
                        DotNetDBF.DBFField c3 = new DotNetDBF.DBFField("PROYECTO", DotNetDBF.NativeDbType.Char, 11);
                        DotNetDBF.DBFField c4 = new DotNetDBF.DBFField("CVEDESC", DotNetDBF.NativeDbType.Numeric, 3);
                        DotNetDBF.DBFField c6 = new DotNetDBF.DBFField("FOLIO", DotNetDBF.NativeDbType.Numeric, 6);
                        DotNetDBF.DBFField c7 = new DotNetDBF.DBFField("NUMDESC", DotNetDBF.NativeDbType.Numeric, 3);
                        DotNetDBF.DBFField c8 = new DotNetDBF.DBFField("TOTDESC", DotNetDBF.NativeDbType.Numeric, 3);
                        DotNetDBF.DBFField c9 = new DotNetDBF.DBFField("IMPORTE", DotNetDBF.NativeDbType.Numeric, 9, 2);
                        DotNetDBF.DBFField c10 = new DotNetDBF.DBFField("TIPODESC", DotNetDBF.NativeDbType.Char, 1);
                        DotNetDBF.DBFField c11 = new DotNetDBF.DBFField("DESDE", DotNetDBF.NativeDbType.Char, 20);
                        DotNetDBF.DBFField c12 = new DotNetDBF.DBFField("HASTA", DotNetDBF.NativeDbType.Char, 20);

                        DotNetDBF.DBFField[] campos = new DotNetDBF.DBFField[] { c1, c2, c3, c4, c6, c7, c8, c9, c10, c11, c12 };
                        escribir.Fields = campos;



                        foreach (Dictionary<string, object> item in resultado)
                        {

                            string nombre = Convert.ToString(item["nombre"]);
                            string rfc = Convert.ToString(item["rfc"]);
                            string proyecto = Convert.ToString(item["proyecto"]);

                            if (proyecto.Contains("764")) {

                            }

                            string cvedesc = Convert.ToString(item["cvedesc"]);
                            int folio = globales.convertInt(Convert.ToString(item["folio"]));
                            int numdesc = globales.convertInt(Convert.ToString(item["numdesc"]));
                            int totdesc = globales.convertInt(Convert.ToString(item["totdesc"]));
                            double importe = globales.convertDouble(Convert.ToString(item["importe"]));
                            string tipodesc = Convert.ToString(item["tipodesc"]);
                            string desde = Convert.ToString(item["desde"]);
                            string hasta = Convert.ToString(item["hasta"]);

                            List<object> record = new List<object> {
                                nombre,rfc,proyecto,cvedesc,folio,numdesc,totdesc,importe,tipodesc,desde,hasta

                            };

                            escribir.AddRecord(record.ToArray());
                        }

                        escribir.Write(ops);
                        escribir.Close();
                        ops.Close();

                        globales.MessageBoxSuccess("Archivo .DBF generado exitosamente", "Archivo generado", globales.menuPrincipal);
                    }
                }



            }
            else
            {

                globales.MessageBoxInformation("Se va a generar el historico del listado de deducciones", "Listado deducciones", globales.menuPrincipal);

                query = "select mma.nombre,mma.rfc,mma.jpp,mma.num,concat(mma.jpp,mma.num) as proyecto,nno.clave as cvedesc ,nno.pago4 as numdesc ,nno.pagot as totdesc ," +
                  " nno.monto as importe ,nno.tipo_pago as tipodesc ,nno.folio , nno.fechaini as desde ,nno.fechafin as hasta, '' as descri " +
                  " from nominas_catalogos.maestro mma inner join nominas_catalogos.respaldos_nominas nno ON mma.jpp = nno.jpp and mma.num = nno.numjpp  " +
                  $" and nno.clave > 60 and {especial} and nno.archivo = '{archivo}' order by nno.clave,mma.jpp,mma.num";

                resultado = globales.consulta(query);

                query = "select  clave,descri from nominas_catalogos.perded order by clave";
                List<Dictionary<string, object>> perded = globales.consulta(query);

                resultado.ForEach(o =>
                {
                    o["descri"] = perded.Where(p => Convert.ToString(o["cvedesc"]) == Convert.ToString(p["clave"])).First()["descri"];
                    //  o["descri"] += " (RETROACTIVO)";
                });

                object[] objetos = new object[resultado.Count];
                int contador = 0;
                foreach (Dictionary<string, object> item in resultado)
                {
                    string nombre = Convert.ToString(item["nombre"]);
                    string rfc = Convert.ToString(item["rfc"]);
                    string jpp = Convert.ToString(item["jpp"]);
                    string num = Convert.ToString(item["num"]);
                    string proyecto = Convert.ToString(item["proyecto"]);
                    string cvedesc = Convert.ToString(item["cvedesc"]);
                    string numdesc = Convert.ToString(item["numdesc"]);
                    string totdesc = Convert.ToString(item["totdesc"]);
                    string importe = Convert.ToString(item["importe"]);
                    string tipodesc = Convert.ToString(item["tipodesc"]);
                    string folio = Convert.ToString(item["folio"]);
                    string desde = Convert.ToString(item["desde"]);
                    string hasta = Convert.ToString(item["hasta"]);
                    string cvedescripcion = Convert.ToString(item["descri"]);

                    object[] tt1 = { nombre,rfc,jpp,num,proyecto,cvedesc,numdesc,
                        totdesc,importe,tipodesc,folio,desde,hasta,cvedescripcion};
                    objetos[contador] = tt1;
                    contador++;
                }

                object[][] parametros = new object[2][];
                object[] header = { "fecha" };
                object[] body = { fechabetween };


                parametros[0] = header;
                parametros[1] = body;

                ReportViewer reporte = globales.reportesParaPanel("nominas_listadoDeducciones", "listadodeduccion", objetos, "", false, parametros);
                reporte.Dock = DockStyle.Fill;
                panelreporte.Controls.Clear();
                panelreporte.Controls.Add(reporte);
            }
        }

        private void frmHistoricoListados_Load(object sender, EventArgs e)
        {
            cmbtipo.SelectedIndex = 0;
            cmbMes.SelectedIndex = DateTime.Now.Month - 1;
            cmb2.SelectedIndex = 0;
            cmbSalida.SelectedIndex = 0;
            txtAño.Text = DateTime.Now.Year.ToString();

        }

        private void chknomina_CheckedChanged(object sender, EventArgs e)
        {
            panel5.Enabled = chknomina.Checked;
        }

        private void cmbSalida_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void cmbSalida_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbSalida_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbSalida.SelectedIndex == 3)
            {

                DialogResult result = globales.MessageBoxQuestion("¿Deseas generar el archivo de BANAMEX?", "Archivo", globales.menuPrincipal);
                if (result == DialogResult.No) return;

                generarArchivosBanco("BANAMEX", resultadoAux);
            }
            else if (cmbSalida.SelectedIndex == 2)
            {
                DialogResult result = globales.MessageBoxQuestion("¿Deseas generar el archivo de BANORTE?", "Archivo", globales.menuPrincipal);
                if (result == DialogResult.No) return;

                generarArchivosBanco("BANORTE", resultadoAux);
            }
        }



        private void timbrar()
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar el archivo de timbrado?", "Aviso", globales.menuPrincipal);
            if (dialogo == DialogResult.No)
                return;
            string jpp = string.Empty;
            string jppdescripcion = string.Empty;



            if (cmbtipo.SelectedIndex == 0)
            {
                return;
            }
            else if (cmbtipo.SelectedIndex == 1)
            {
                jpp = "JUB";
            }
            else if (cmbtipo.SelectedIndex == 2)
            {
                jpp = "PDO";
            }
            else if (cmbtipo.SelectedIndex == 3)
            {
                jpp = "PTA";
            }
            else if (cmbtipo.SelectedIndex == 4)
            {
                jpp = "PEA";
            }
            string query = string.Empty;

            string tipo_nomina = string.Empty;
            string titulo = "OFICINA DE PENSIONES DEL ESTADO DE OAXACA";

            archivo = string.Empty;


            archivo = txtAño.Text.Substring(2) + ((cmbMes.SelectedIndex + 1 < 10) ? $"0{cmbMes.SelectedIndex + 1}" : (cmbMes.SelectedIndex + 1).ToString());


            this.tipo_nomina = string.Empty;

            if (chknomina.Checked == false)
            {
                tipo_nomina = "N";
            }
            else
            {
                if (radioButton1.Checked) tipo_nomina = "AG";
                if (radioButton2.Checked) tipo_nomina = "CA";
                if (radioButton3.Checked) tipo_nomina = "DM";
                if (radioButton4.Checked) tipo_nomina = "UT";
                if (radioButton5.Checked) tipo_nomina = "CAN2";
            }

       

   

            string querys = $"create temp table tempo as (select * from nominas_catalogos.respaldos_nominas where archivo='{archivo}'); create TEMP table otratempo as(  SELECT	CONCAT (a1.jpp, a1.num) AS proyecto,a1.num,a1.nombre,a1.curp,a1.rfc,a1.imss,a1.categ,a2.clave,a2.descri,a2.monto,a2.pagot,a2.leyen" +
$" FROM nominas_catalogos.maestro a1 JOIN tempo a2 ON a1.num = a2.numjpp AND a1.jpp = a2.jpp WHERE  a1.jpp = a2.jpp AND a1.jpp = '{jpp}'" +
$" AND a2.tipo_nomina = '{tipo_nomina}' AND a2.archivo='{archivo}' ORDER BY a1.jpp, a1.num, a2.clave);" +
            "select proyecto, nombre,num, curp, rfc, imss, categ,	SUM (monto) FILTER (WHERE clave >= 1 AND clave <= 60 ) AS Totper,	COALESCE (SUM (monto) FILTER (WHERE clave >= 61 and clave not in (204,217)),0) AS Totded, COALESCE (SUM (monto) FILTER (where clave = 204),0) as IMSSS, COALESCE (SUM (monto) FILTER (where clave = 217),0) as DESC_JUDICIAL" +
            " from otratempo GROUP BY proyecto, nombre, curp, rfc, imss,num, categ ORDER BY  num; ";

            List<Dictionary<string, object>> result = globales.consulta(querys);



            string filePath = "";

            DialogResult p = open1.ShowDialog();
            if (p == DialogResult.OK)
            {
                filePath = open1.FileName;

                Cursor.Current = Cursors.WaitCursor;


                Microsoft.Office.Interop.Excel.Application ExApp;
                ExApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook oWBook;
                Microsoft.Office.Interop.Excel._Worksheet oSheet;
                oWBook = ExApp.Workbooks.Open($"{filePath}", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWBook.ActiveSheet;

                int numero = cmbMes.SelectedIndex + 1;
                DateTime anio = DateTime.Now;
                string año = Convert.ToString(anio.Year);
                int limite = result.Count();
                int calcula = limite / 100;
                //progressBar1.Visible = true;
                //int progress = 0;


                int c = 3;
                int a = 0;
                foreach (var item in result)
                {
                    oSheet.Cells[c, 1] = "si";
                    oSheet.Cells[c, 2] = Convert.ToString(item["proyecto"]);
                    oSheet.Cells[c, 3] = Convert.ToString(item["nombre"]);
                    oSheet.Cells[c, 4] = Convert.ToString(item["rfc"]);
                    oSheet.Cells[c, 5] = Convert.ToString(item["curp"]);
                    oSheet.Cells[c, 6] = "10";
                    oSheet.Cells[c, 7] = "12";
                   // oSheet.Cells[c, 8] = "";
                   // oSheet.Cells[c, 9] = "";

                   // oSheet.Cells[c, 10] = "";
                    //oSheet.Cells[c, 11] = "";
                    //oSheet.Cells[c, 12] = "";
                    //oSheet.Cells[c, 13] = "";
                //    oSheet.Cells[c, 14] = "";
                  //  oSheet.Cells[c, 15] = "";
                  //  oSheet.Cells[c, 16] = "";
                    //oSheet.Cells[c, 17] = "";
                    oSheet.Cells[c, 18] = "OPE631216S18";
                   // oSheet.Cells[c, 19] = "";
                    //oSheet.Cells[c, 20] = "";

                   // oSheet.Cells[c, 21] = "";
                    oSheet.Cells[c, 22] = "OAX";

                    oSheet.Cells[c, 23] = año + Convert.ToString(numero) + "JUB";
                   // oSheet.Cells[c, 24] = "";
                    oSheet.Cells[c, 24] = "68050";
                    oSheet.Cells[c, 25] = "30";
                   // oSheet.Cells[c, 26] = "";
                    //oSheet.Cells[c, 27] = "";


                   // oSheet.Cells[1, 28] = "";    // aqui comineza importegravado
                   // oSheet.Cells[1, 29] = "";
                    oSheet.Cells[c, 30] = Convert.ToDouble(item["totper"]);


                    oSheet.Cells[c, 31] = Convert.ToDouble(item["totper"]);
                    oSheet.Cells[c, 32] = "0";
                    double per = Convert.ToDouble(item["totper"]);
                    double ded = Convert.ToDouble(item["totded"]);
                    double total_neto = per - ded;
                    oSheet.Cells[c, 33] = Convert.ToDouble(item["totper"]);
                    oSheet.Cells[c, 34] = Convert.ToDouble(item["imsss"]);
                    oSheet.Cells[c, 35] = Convert.ToDouble(item["desc_judicial"]);
                    oSheet.Cells[c, 36] = "0";


                 //   oSheet.Cells[c, 37] = total_neto;
                    oSheet.Cells[c, 37] = "IP"; // oirigen recurso 

                    oSheet.Cells[c, 38] = total_neto; 
                    //oSheet.Cells[c, 39] = "";
                    //oSheet.Cells[c, 40] = "";

                    oSheet.Cells[c, 41] = total_neto;
                    //oSheet.Cells[c, 42] = "";
                   // oSheet.Cells[c, 43] = "";

                   // oSheet.Cells[c, 44] = "";
                    oSheet.Cells[c, 45] = total_neto;

                    c++;

                    //if (a==100)
                    //{
                    //    a = 0;
                    //}
                    //a++;
                    //progressBar1.Value = a;
                    
                }




                oWBook.Save();
                oWBook.Close(true);
                ExApp.Quit();





            }






            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            //if (xlApp == null)
            //{
            //    MessageBox.Show("Excel no se encuentra Instalado");
            //    return;
            //}

            //int numero = cmbMes.SelectedIndex + 1;
            //DateTime anio = DateTime.Now;
            //string año = Convert.ToString(anio.Year);
            //int limite = result.Count();


            //Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            //Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;


            //object misValue = System.Reflection.Missing.Value;

            //xlWorkBook = xlApp.Workbooks.Add(misValue);
            //xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //xlWorkSheet.Cells[1, 1] = "procesar";
            //xlWorkSheet.Cells[1, 2] = "num_empleado";
            //xlWorkSheet.Cells[1, 3] = "nombre";
            //xlWorkSheet.Cells[1, 4] = "rfc";
            //xlWorkSheet.Cells[1, 5] = "curp";
            //xlWorkSheet.Cells[1, 6] = "tipo_contrato";
            //xlWorkSheet.Cells[1, 7] = "tipo_regimen";
            //xlWorkSheet.Cells[1, 8] = "tipo_jornada";
            //xlWorkSheet.Cells[1, 9] = "registro_patronal";
            //xlWorkSheet.Cells[1, 10] = "nss";
            //xlWorkSheet.Cells[1, 11] = "fecha_ingreso";
            //xlWorkSheet.Cells[1, 12] = "semanas_antiguedad";
            //xlWorkSheet.Cells[1, 13] = "riesgo_puesto";
            //xlWorkSheet.Cells[1, 14] = "SBC";
            //xlWorkSheet.Cells[1, 15] = "SDI";
            //xlWorkSheet.Cells[1, 16] = "departamento";
            //xlWorkSheet.Cells[1, 17] = "rfc_patron_origen";
            //xlWorkSheet.Cells[1, 18] = "puesto";
            //xlWorkSheet.Cells[1, 19] = "banco";
            //xlWorkSheet.Cells[1, 20] = "cuenta_bancaria";
            //xlWorkSheet.Cells[1, 21] = "clave_entfed_labora";
            //xlWorkSheet.Cells[1, 22] = "serie";
            //xlWorkSheet.Cells[1, 23] = "folio";
            //xlWorkSheet.Cells[1, 24] = "lugar_expedición";
            //xlWorkSheet.Cells[1, 25] = "dias_trabajados";
            //xlWorkSheet.Cells[1, 26] = "sindicalizado";
            //xlWorkSheet.Cells[1, 27] = "observaciones";
            //xlWorkSheet.Cells[1, 28] = "importe_gravado";
            //xlWorkSheet.Cells[1, 29] = "importe_exento";
            //xlWorkSheet.Cells[1, 30] = "importe_gravado";
            //xlWorkSheet.Cells[1, 31] = "importe_exento";
            //xlWorkSheet.Cells[1, 32] = "total_percepciones";
            //xlWorkSheet.Cells[1, 33] = "otras_deducciones";
            //xlWorkSheet.Cells[1, 34] = "Clave 204 IMSSS";
            //xlWorkSheet.Cells[1, 35] = "Clave 217 DESC. JUDICIAL";
            //xlWorkSheet.Cells[1, 36] = "total_neto";
            //xlWorkSheet.Cells[1, 37] = "origen_recurso";
            //xlWorkSheet.Cells[1, 38] = "monto_recurso_propio";
            //xlWorkSheet.Cells[1, 39] = "relac_tipo_relación";
            //xlWorkSheet.Cells[1, 40] = "relac_docum";
            //xlWorkSheet.Cells[1, 41] = "total_una_exhibicion";
            //xlWorkSheet.Cells[1, 42] = "total_parcialidad";
            //xlWorkSheet.Cells[1, 43] = "monto_diario";
            //xlWorkSheet.Cells[1, 44] = "ingreso_acumulable";
            //xlWorkSheet.Cells[1, 45] = "ingreso_noacumulable";






            //int c = 2;
            //foreach (var item in result)
            //{
            //    xlWorkSheet.Cells[c, 1] = "si";
            //    xlWorkSheet.Cells[c, 2] = Convert.ToString(item["proyecto"]);
            //    xlWorkSheet.Cells[c, 3] = Convert.ToString(item["nombre"]);
            //    xlWorkSheet.Cells[c, 4] = Convert.ToString(item["rfc"]);
            //    xlWorkSheet.Cells[c, 5] = Convert.ToString(item["curp"]);
            //    xlWorkSheet.Cells[c, 6] = "10";
            //    xlWorkSheet.Cells[c, 7] = "12";
            //    xlWorkSheet.Cells[c, 8] = "";
            //    xlWorkSheet.Cells[c, 9] = "OPE631216S18";

            //    xlWorkSheet.Cells[c, 10] = "";
            //    xlWorkSheet.Cells[c, 11] = "";
            //    xlWorkSheet.Cells[c, 12] = "";
            //    xlWorkSheet.Cells[c, 13] = "";
            //    xlWorkSheet.Cells[c, 14] = "";
            //    xlWorkSheet.Cells[c, 15] = "";
            //    xlWorkSheet.Cells[c, 16] = "";
            //    xlWorkSheet.Cells[c, 17] = "";
            //    xlWorkSheet.Cells[c, 18] = "";
            //    xlWorkSheet.Cells[c, 19] = "";
            //    xlWorkSheet.Cells[c, 20] = "";

            //    xlWorkSheet.Cells[c, 21] = "OAX";

            //    xlWorkSheet.Cells[c, 22] = año + Convert.ToString(numero) + "JUB";
            //    xlWorkSheet.Cells[c, 23] = "";
            //    xlWorkSheet.Cells[c, 24] = "68050";
            //    xlWorkSheet.Cells[c, 25] = "30";
            //    xlWorkSheet.Cells[c, 26] = "";
            //    xlWorkSheet.Cells[c, 27] = "";


            //    xlWorkSheet.Cells[1, 28] = "";
            //    xlWorkSheet.Cells[1, 29] = "";
            //    xlWorkSheet.Cells[c, 30] = "";// importe excen


            //    xlWorkSheet.Cells[c, 31] = Convert.ToDouble(item["totper"]);
            //    xlWorkSheet.Cells[c, 32] = Convert.ToDouble(item["totper"]);
            //    xlWorkSheet.Cells[c, 33] = Convert.ToDouble(item["totded"]);
            //    double per = Convert.ToDouble(item["totper"]);
            //    double ded = Convert.ToDouble(item["totded"]);
            //    double total_neto = per - ded;
            //    xlWorkSheet.Cells[c, 34] = Convert.ToDouble(item["imsss"]);
            //    xlWorkSheet.Cells[c, 35] = Convert.ToDouble(item["desc_judicial"]);

            //    xlWorkSheet.Cells[c, 36] = total_neto;
            //    xlWorkSheet.Cells[c, 37] = "IP"; // oirigen recurso 

            //    xlWorkSheet.Cells[c, 38] = ""; // oirigen recurso 
            //    xlWorkSheet.Cells[1, 39] = "relac_tipo_relación";
            //    xlWorkSheet.Cells[1, 40] = "relac_docum";

            //    xlWorkSheet.Cells[c, 41] = total_neto;
            //    xlWorkSheet.Cells[c, 42] = "";
            //    xlWorkSheet.Cells[c, 43] = "";

            //    xlWorkSheet.Cells[c, 44] ="" ;
            //    xlWorkSheet.Cells[c, 45] = total_neto;

            //    c++;
            //}









            //xlWorkBook.SaveAs("C:\\ESTRUCTURA.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();

            //Marshal.ReleaseComObject(xlWorkSheet);
            //Marshal.ReleaseComObject(xlWorkBook);
            //Marshal.ReleaseComObject(xlApp);
            //string directorio = "C:\\ESTRUCTURA.xls";
            //DialogResult dia = globales.MessageBoxSuccess("EN BREVE SE ABRIRÁ EL ARCHIVO", "MENSAJE", globales.menuPrincipal);

            //string xlsPath = Path.Combine(Application.StartupPath, directorio);


            //Process.Start(xlsPath);
            //Cursor.Current = Cursors.Default;


        }

        private void panelreporte_Paint(object sender, PaintEventArgs e)
        {

        }





        private void listadoAportaciones()
        {
            string query = string.Empty;
            List<Dictionary<string, object>> resultado = null;
            string especial = this.chknomina.Checked ? $" nno.tipo_nomina ='{this.tipo_nomina}' " : " nno.tipo_nomina='N' ";


            globales.MessageBoxInformation("Se va a generar el historico del listado de deducciones", "Listado deducciones", globales.menuPrincipal);

            query = "select mma.nombre,mma.rfc,mma.jpp,mma.num,concat(mma.jpp,mma.num) as proyecto,nno.clave as cvedesc ,nno.pago4 as numdesc ,nno.pagot as totdesc ," +
              " nno.monto as importe ,nno.tipo_pago as tipodesc ,nno.folio , nno.fechaini as desde ,nno.fechafin as hasta, '' as descri " +
              " from nominas_catalogos.maestro mma inner join nominas_catalogos.respaldos_nominas nno ON mma.jpp = nno.jpp and mma.num = nno.numjpp  " +
              $" and nno.clave < 60 and {especial} and nno.archivo = '{archivo}' order by nno.clave,mma.jpp,mma.num";

            resultado = globales.consulta(query);

            query = "select  clave,descri from nominas_catalogos.perded order by clave";
            List<Dictionary<string, object>> perded = globales.consulta(query);

            resultado.ForEach(o =>
            {
                o["descri"] = perded.Where(p => Convert.ToString(o["cvedesc"]) == Convert.ToString(p["clave"])).First()["descri"];
                //  o["descri"] += " (RETROACTIVO)";
            });

            object[] objetos = new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                string nombre = Convert.ToString(item["nombre"]);
                string rfc = Convert.ToString(item["rfc"]);
                string jpp = Convert.ToString(item["jpp"]);
                string num = Convert.ToString(item["num"]);
                string proyecto = Convert.ToString(item["proyecto"]);
                string cvedesc = Convert.ToString(item["cvedesc"]);
                string numdesc = Convert.ToString(item["numdesc"]);
                string totdesc = Convert.ToString(item["totdesc"]);
                string importe = Convert.ToString(item["importe"]);
                string tipodesc = Convert.ToString(item["tipodesc"]);
                string folio = Convert.ToString(item["folio"]);
                string desde = Convert.ToString(item["desde"]);
                string hasta = Convert.ToString(item["hasta"]);
                string cvedescripcion = Convert.ToString(item["descri"]);

                object[] tt1 = { nombre,rfc,jpp,num,proyecto,cvedesc,numdesc,
                        totdesc,importe,tipodesc,folio,desde,hasta,cvedescripcion};
                objetos[contador] = tt1;
                contador++;
            }

            object[][] parametros = new object[2][];
            object[] header = { "fecha" };
            object[] body = { fechabetween };


            parametros[0] = header;
            parametros[1] = body;

            ReportViewer reporte = globales.reportesParaPanel("nominas_listadoDeducciones", "listadodeduccion", objetos, "", false, parametros);
            reporte.Dock = DockStyle.Fill;
            panelreporte.Controls.Clear();
            panelreporte.Controls.Add(reporte);
        }

    }
    }

