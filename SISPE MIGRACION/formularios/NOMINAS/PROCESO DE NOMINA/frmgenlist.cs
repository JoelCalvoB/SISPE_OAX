using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA
{
    //joel    x2
    public partial class frmgenlist : Form
    {

        string tipo_nomina = string.Empty;
        public frmgenlist()
        {
            InitializeComponent();
        }

        private void rbdeduccion_CheckedChanged(object sender, EventArgs e)
        {
            txtclave.Enabled = false;
            txtnomina.Enabled = false;
            cmb2.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void llena_formulario()
        {
            cmbmes.SelectedIndex = 0;
            DateTime fecha_actual = DateTime.Now;
            int mes = fecha_actual.Month;
            int anio = fecha_actual.Year;
            switch (mes)
            {
                case 1:
                    cmbmes.SelectedIndex = 0;
                    break;
                case 2:
                    cmbmes.SelectedIndex = 1;
                    break;
                case 3:
                    cmbmes.SelectedIndex = 2;
                    break;
                case 4:
                    cmbmes.SelectedIndex = 3;
                    break;
                case 5:
                    cmbmes.SelectedIndex = 4;
                    break;
                case 6:
                    cmbmes.SelectedIndex = 5;
                    break;
                case 7:
                    cmbmes.SelectedIndex = 6;
                    break;
                case 8:
                    cmbmes.SelectedIndex = 7;
                    break;
                case 9:
                    cmbmes.SelectedIndex = 8;
                    break;
                case 10:
                    cmbmes.SelectedIndex = 9;
                    break;
                case 11:
                    cmbmes.SelectedIndex = 10;
                    break;
                case 12:
                    cmbmes.SelectedIndex = 11;

                    break;
                default:
                    break;



            }
            txtfecha.Text = Convert.ToString(anio);
        }

        private void frmgenlist_Load(object sender, EventArgs e)
        {
            llena_formulario();
            cmb2.SelectedIndex = 0;
        }

        private void btnaceptar_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.tipo_nomina = string.Empty;
            if (radioButton1.Checked) tipo_nomina = "AG";
            if (radioButton2.Checked) tipo_nomina = "CA";
            if (radioButton3.Checked) tipo_nomina = "DM";
            if (radioButton4.Checked) tipo_nomina = "UT";
            if (radioButton5.Checked) tipo_nomina = "CAN2";
            if (radioButton7.Checked) tipo_nomina = "PF";
            if (radioButton6.Checked) tipo_nomina = "E";




            if (this.rbdeduccion.Checked)
            {
                this.generarListadoDeduccion();
            }
            else if (this.rbalfaliquido.Checked)
            {
                this.generarListadoAlfaConLiquido();
            }
            else if (this.rbalfa.Checked)
            {
                this.generarListadoAlfabetico();
            }
            else if (this.rbdisco.Checked)
            {
                //globales.crearDbf();
                this.generarDiscoRecursos();
            }
        }

        private void generarDiscoRecursos()
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar el listado y disco para recursos humanos?", "Aviso", globales.menuPrincipal);

            if (dialogo == DialogResult.No) return;



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

            string especial = this.chknomina.Checked ? $" nno.tipo_nomina = '{this.tipo_nomina}' " : " nno.tipo_nomina='N' ";

            string query = "select mma.nombre,mma.curp,mma.rfc,mma.jpp,mma.num,concat(mma.jpp,mma.num) as proyecto,nno.clave  ,nno.pagon ,nno.cheque ,nno.pagot ," +
                   " nno.monto  ,nno.tipopago ,nno.folio , nno.fechaini ,nno.fechafin " +
                   " from nominas_catalogos.maestro mma inner join nominas_catalogos.nominew nno on mma.jpp = nno.jpp and mma.num = nno.numjpp  " +
                   $" and nno.clave in (227,226,221) and {especial} where mma.superviven = 'S'  order by mma.jpp,mma.num,nno.clave";

            List<Dictionary<string, object>> resultado = globales.consulta(query);
            string mes = this.cmbmes.SelectedIndex < 5 ? $"0{(this.cmbmes.SelectedIndex + 1) * 2}" : Convert.ToString((this.cmbmes.SelectedIndex + 1) * 2);

            SaveFileDialog dialogoGuardar = new SaveFileDialog();
            dialogoGuardar.AddExtension = true;
            dialogoGuardar.DefaultExt = ".dbf";  
            dialogoGuardar.FileName = $"J480{mes}{txtfecha.Text.Substring(2)}";

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




            //**********************************

            globales.MessageBoxInformation("Se generara el archivo base", "Archivo base", globales.menuPrincipal);

            dialogoGuardar = new SaveFileDialog();
            dialogoGuardar.AddExtension = true;
            dialogoGuardar.DefaultExt = ".dbf";
            dialogoGuardar.FileName = $"BASE{mes}{txtfecha.Text.Substring(2)}";

            if (dialogoGuardar.ShowDialog() == DialogResult.OK)
            {
                string ruta = dialogoGuardar.FileName;

                especial = this.chknomina.Checked ? $" tipo_nomina = '{this.tipo_nomina}' " : " tipo_nomina='N' ";


                string cadena = "Provider=VFPOLEDB.1; Data Source= {0}\\; Extended Properties =dBase IV; ";
                string pasa = string.Format(cadena, ruta.Substring(0, ruta.LastIndexOf("\\")));

                using (OleDbConnection connection = new OleDbConnection(pasa))
                using (OleDbCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    string queryClaves = $"select clave from nominas_catalogos.nominew where {especial} group by clave order by clave";
                    List<Dictionary<string, object>> claves = globales.consulta(queryClaves);


                    query = $"create temp table t1 as select JPP,numjpp from nominas_catalogos.nominew where  tipopago = 'N' and {especial} group by jpp,numjpp; " +
                       " create temp table t2 as  select mma.rfc,mma.nombre,mma.jpp,mma.num,mma.sexo,mma.curp from nominas_catalogos.maestro mma inner join t1 on mma.jpp = t1.jpp and mma.num = t1.numjpp  " +
                       " where  mma.jpp <> 'PEA' AND mma.superviven = 'S' ORDER BY mma.jpp,mma.num; ";


                    string selectPart = string.Empty;
                    string leftJoin = string.Empty;


                    foreach (Dictionary<string,object> item in claves) {
                        int clave = globales.convertInt(Convert.ToString(item["clave"]));
                        string queEs = clave > 68 ? "D" : "P";
                        selectPart += $", COALESCE(sum(nn{clave}.monto),0) as {queEs}{clave} ";
                        leftJoin  += $" left join nominas_catalogos.nominew nn{clave}  " +
                            $" on nn{clave}.jpp = t2.jpp and nn{clave}.numjpp = t2.num and nn{clave}.clave = {clave} ";
                    }

                    query += $" select t2.* {selectPart} from t2 {leftJoin} group by t2.rfc,t2.nombre,t2.jpp,t2.num,t2.sexo,t2.curp order by t2.jpp,t2.num ";
                    resultado = globales.consulta(query);

                    string tabla = @"CREATE TABLE {0} (CQNACVE   N( 6, 0), CQNAIND   N( 2, 0), PBPNUP    N( 6, 0), PBPNUE    N( 6, 0), SIRFC     C(13, 0),"+
                        "SINOM     C(45, 0), SIDEP     C(20, 0), SICATG    C( 7, 0), SISX      C( 1, 0), TIP_EMP   N( 2, 0),"+
                        " NUMFOLIO  N( 6, 0), UBICA     C( 1, 0), CVE       N( 3, 0), TIPOPAGO  N( 1, 0), PBPFIG    D( 8, 0),"+
                        " PBPIPTO   D( 8, 0), PBPFNOMB  D( 8, 0), PBPSTATUS N( 2, 0), LICDES    D( 8, 0), LICHAS    D( 8, 0),"+
                        "CMOTCVE   N( 3, 0), PBPHIJOS  N( 1, 0), GUADES    D( 8, 0), GUAHAS    D( 8, 0), CURP      C(21, 0),"+
                        "AREAADS   C(70, 0), NOMBANCO  C(20, 0), NUMCUENTA C(12, 0), CLAVE_INTE C(20, 0), CESTNIV   N( 2, 0),"+
                        "CESTGDO   N( 1, 0), QNIOS     N( 1, 0), PBPIMSS   C(11, 0), BGIMSS    N(12, 2), CUOPIMSS  N(13, 2),"+
                        "CUOPRCV   N(13, 2), INCDES    D( 8, 0), INCHAS    D( 8, 0), CUOPINF   N(13, 2), CUOPFPEN  N(13, 2),"+
                        "BGISPT    N(12, 2), PROFESION N( 4, 0), STATPAGO  C(180, 0), AGUIFDES  D( 8, 0), AGUIFHAS  D( 8, 0),"+
                        "DAFDES    D( 8, 0), DAFHAS    D( 8, 0),{1})";

                    string PercepcionesDeducciones = "";
                    foreach (Dictionary<string,object> item in claves) {
                        int clave = globales.convertInt(Convert.ToString(item["clave"]));
                        PercepcionesDeducciones += (clave < 69)?$"P{clave} N( 13, 2 )," :$"D{clave} N( 13, 2 )," ;
                    }
                    PercepcionesDeducciones = PercepcionesDeducciones.Substring(0,PercepcionesDeducciones.Length-1);

                    string nombre = ruta.Substring(ruta.LastIndexOf("\\")+1);
                    nombre = nombre.Split('.')[0];
                    tabla = string.Format(tabla, nombre,PercepcionesDeducciones);
                    command.CommandText = tabla;
                    command.ExecuteNonQuery();



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

                        int NUMFOLIO = 0;
                        string UBICA = "3";
                        int CVE = 470;
                        int TIPOPAGO = cmb2.SelectedIndex;
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
                        foreach (Dictionary<string,object> clav in claves) {
                            int clave = globales.convertInt(Convert.ToString(clav["clave"]));
                            string costr = clave > 68 ? "D" : "P";
                            string llave = $"{costr}{clave}";
                            string llave2 = $"{costr.ToLower()}{clave}";
                            columna += $",{llave}";
                            valorColumna += $" ,{item[llave2]} ";
                            
                        }

                        
                        

                        sentencia = string.Format(sentencia,nombre,part1,part11,part2,part22,part3,part33,part4,part44,part5,part55,
                            part6,part66,part7,part77,part8,part88,part9,part99,part10,part1010,columna,valorColumna);
                        command.CommandText = sentencia;
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                globales.MessageBoxSuccess("Archivo .DBF generado exitosamente", "Archivo generado", globales.menuPrincipal);

            }

        }

        private void generarListadoAlfabetico()
        {

            string especial = this.chknomina.Checked ? $" tipo_nomina = '{this.tipo_nomina}' " : "tipo_nomina='N' ";
            

            string query = $"create temp table uniendo as SELECT jpp,numjpp,sum(monto) FROM nominas_catalogos.nominew where clave <= 60 and {especial} group by jpp,numjpp " +
            $" union SELECT jpp,numjpp,(-1 * sum(monto)) FROM nominas_catalogos.nominew where clave > 60 and {especial} group by jpp,numjpp; " +
            " create temp table suma as select jpp,numjpp,sum(sum) from uniendo group by jpp,numjpp order by jpp,numjpp; " +
            " select mma.*,ssu.sum from nominas_catalogos.maestro mma inner join suma ssu on ssu.jpp = mma.jpp and ssu.numjpp = mma.num " +
            " where mma.superviven = 'S' ";

            List<Dictionary<string, object>> resultado = globales.consulta(query);


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


            globales.reportes("nominas_listadoliquidoA", "listadoliquido", obj, "", false, parametros);

        }

        private void generarListadoAlfaConLiquido()
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar el listado alfabetico por liquido?", "Aviso", globales.menuPrincipal);

            if (dialogo == DialogResult.No) return;

            globales.MessageBoxInformation("Se va a generar el archivo de banamex", "Archivo banamex", globales.menuPrincipal);

            string especial = this.chknomina.Checked ? $" tipo_nomina = '{this.tipo_nomina}' ": " tipo_nomina='N' ";

            string aux = "create temp table uniendo as SELECT jpp,numjpp,sum(monto) FROM nominas_catalogos.nominew where clave <= 60 and {1} group by jpp,numjpp " +
                " union SELECT jpp,numjpp,(-1 * sum(monto)) FROM nominas_catalogos.nominew where clave > 60 and {1} group by jpp,numjpp; " +
                " create temp table suma as select jpp,numjpp,sum(sum) from uniendo group by jpp,numjpp order by jpp,numjpp; " +
                " select mma.*,ssu.sum from nominas_catalogos.maestro mma inner join suma ssu on ssu.jpp = mma.jpp and ssu.numjpp = mma.num " +
                " where mma.superviven = 'S' and mma.nomelec = 'S' and mma.banco = '{0}'  order by mma.jpp,mma.num ";

            string query = string.Format(aux, "BANAMEX",especial);
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            generarArchivosBanco("BANAMEX", resultado);
            generarReporteador(resultado, "BANAMEX");

            globales.MessageBoxInformation("Se va a generar el archivo de banorte", "Archivo banorte", globales.menuPrincipal);
            query = string.Format(aux, "BANORTE", especial);
            resultado = globales.consulta(query);

            generarArchivosBanco("BANORTE", resultado);
            generarReporteador(resultado, "BANORTE");



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


            globales.reportes("nominas_listadoliquido", "listadoliquido", obj, "", false, parametros);

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

        private void generarListadoDeduccion()
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar el listado y el archivo de deducción?", "Aviso", globales.menuPrincipal);
            if (dialogo == DialogResult.Yes)
            {
                string mes = this.cmbmes.SelectedIndex < 5 ? $"0{(this.cmbmes.SelectedIndex + 1) * 2}" : Convert.ToString((this.cmbmes.SelectedIndex + 1) * 2);

                string especial = this.chknomina.Checked ? $" nno.tipo_nomina ='{this.tipo_nomina}' ": " nno.tipo_nomina='N' ";

                globales.MessageBoxInformation("Se va a generar el archivo de descuento", "Archivo de descuento", globales.menuPrincipal);
                string query = "select mma.nombre,mma.rfc,concat(mma.jpp,mma.num) as proyecto,nno.clave as cvedesc ,nno.pagon as numdesc ,nno.pagot as totdesc ," +
                    " nno.monto as importe ,nno.tipopago as tipodesc ,nno.folio , nno.fechaini as desde ,nno.fechafin as hasta " +
                    " from nominas_catalogos.maestro mma inner join nominas_catalogos.nominew nno on mma.jpp = nno.jpp and mma.num = nno.numjpp  " +
                    $" and nno.clave in (205,206) where mma.superviven = 'S' and {especial} ";

                List<Dictionary<string, object>> resultado = globales.consulta(query);
                SaveFileDialog dialogoGuardar = new SaveFileDialog();
                dialogoGuardar.AddExtension = true;
                dialogoGuardar.DefaultExt = ".dbf";
                dialogoGuardar.FileName = $"D980{mes}{txtfecha.Text.Substring(2)}";



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

                globales.MessageBoxInformation("Se va a generar el listado de deducciones", "Listado deducciones", globales.menuPrincipal);

                query = "select mma.nombre,mma.rfc,mma.jpp,mma.num,concat(mma.jpp,mma.num) as proyecto,nno.clave as cvedesc ,nno.pagon as numdesc ,nno.pagot as totdesc ," +
                  " nno.monto as importe ,nno.tipopago as tipodesc ,nno.folio , nno.fechaini as desde ,nno.fechafin as hasta, '' as descri " +
                  " from nominas_catalogos.maestro mma inner join nominas_catalogos.nominew nno on mma.jpp = nno.jpp and mma.num = nno.numjpp  " +
                  $" and nno.clave > 60 and {especial} where mma.superviven = 'S' order by nno.clave,mma.jpp,mma.num";

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
                object[] body = { "" };


                parametros[0] = header;
                parametros[1] = body;

                globales.reportes("nominas_listadoDeducciones", "listadodeduccion", objetos,"",false,parametros);


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void rbalfaliquido_CheckedChanged(object sender, EventArgs e)
        {
            txtclave.Enabled = false;
            txtnomina.Enabled = false;
            cmb2.Enabled = false;
        }

        private void rbalfa_CheckedChanged(object sender, EventArgs e)
        {
            txtclave.Enabled = false;
            txtnomina.Enabled = false;
            cmb2.Enabled = false;
        }

        private void rbdisco_CheckedChanged(object sender, EventArgs e)
        {
            txtclave.Enabled = true;
            txtnomina.Enabled = true;
            cmb2.Enabled = true;
            cmb2.SelectedIndex = 0;
        }

        private void chknomina_CheckedChanged(object sender, EventArgs e)
        {
            if (chknomina.Checked)
            {
                groupBox2.Visible = true;
            }
            else
            {
                groupBox2.Visible = false;
            }
        }
    }
}
