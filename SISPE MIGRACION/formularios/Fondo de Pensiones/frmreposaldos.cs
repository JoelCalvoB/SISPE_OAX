using SISPE_MIGRACION.codigo.herramientas.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones
{

    public partial class frmreposaldos : Form
    {
        private List<Dictionary<string, object>> lista;
        private bool tipoGrafica = false;

        public frmreposaldos()
        {
            InitializeComponent();
            
        }

        private void btngenerar_Click(object sender, EventArgs e)
        {
            string tipo_movimiento = string.Empty;

            if (chk_movimiento.Checked) {
                if (string.IsNullOrWhiteSpace(cmbmovimiento.Text))
                {
                    globales.MessageBoxExclamation("Se debe seleccionar algun movimiento para buscar en salgos", "Aviso", globales.menuPrincipal);
                    return;
                }
            }
            seleccionar s = new seleccionar();
            s.ShowDialog();
            if (!s.seleccionado) return;



            DateTime fecha1 = DateTime.Parse(fec1.Text);
            DateTime fecha2 = DateTime.Parse(fec2.Text);

            this.Cursor = Cursors.AppStarting;


            string c1 = string.Format("{0:yyyy-MM-dd}", fecha1);
            string c2 = string.Format("{0:yyyy-MM-dd}", fecha2);


            string query = string.Empty;

            if (!chk_movimiento.Checked)
            {
                query = "SELECT datos.saldos ('{0}','{1}')";
                query = string.Format(query, c1, c2);
            }
            else {
                tipo_movimiento = "MOVIMIENTO: "+cmbmovimiento.Text;
                query =  "SELECT datos.\"saldos_movimientos(date, date, varchar)\"('{0}','{1}', '{2}') ";
                query = string.Format(query, c1, c2,cmbmovimiento.Text);
            }



            globales.consulta(query);
           
            string query2= $"SELECT emp.rfc, emp.nombre_em, emp.proyecto, sal.saldo, emp.nap, sal.mes, sal.anio FROM ( datos.empleados emp  JOIN datos.r_saldos sal ON(( (emp.rfc) ::TEXT = (sal.rfc) ::TEXT))) WHERE( (sal.saldo<>(0) :: NUMERIC) AND(emp.pendiente = FALSE))  ORDER BY emp.nombre_em";

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

                    object[] tt1 = { nombre_em, rfc, proyecto, nap, string.Format("{0:C}", saldo).Replace("$", "") };
                    aux2[contador] = tt1;
                    contador++;



                }
                
                this.Cursor = Cursors.Default;

                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                object[] parametros = { "fech1", "fech2", "tiporeporte","mov","logo" };
                object[] valor = { this.fec1.Text, this.fec2.Text, "NORMALES",tipo_movimiento, logo };
                object[][] enviarParametros = new object[2][];

                enviarParametros[0] = parametros;
                enviarParametros[1] = valor;

                globales.reportes("frmreportsaldo", "rsaldos_aporta", aux2, "", false, enviarParametros);
            }
            else {
                //SaveFileDialog p = new SaveFileDialog();
                //p.AddExtension = true;
                //p.DefaultExt = ".dbf";
                //p.ShowDialog();
                //string rutaa = p.FileName;
                ////StreamWriter escribir = new StreamWriter(rutaa);
                ////escribir.Close();
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
                    DotNetDBF.DBFField a4 = new DotNetDBF.DBFField("SALDO", DotNetDBF.NativeDbType.Numeric, 10, 2 );
                    DotNetDBF.DBFField a5 = new DotNetDBF.DBFField("NAP", DotNetDBF.NativeDbType.Numeric, 10, 2);
             

                    DotNetDBF.DBFField[] campos = new DotNetDBF.DBFField[] { a1, a2, a3, a4, a5};
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

        private void frmreposaldos_Load(object sender, EventArgs e)
        {
          //  fec1.Text = string.Format("{0:d}",DateTime.Now);
            fec2.Text = string.Format("{0:d}", DateTime.Now);
        }

        private void button3_Click(object sender, EventArgs e)
        {
        
                Owner.Close();
        }

        private void fec2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frmreposaldos_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = fec1;
        }

        private void fec1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void fec1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                SendKeys.Send("{TAB}");

            }

        
        }

        private void frmreposaldos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                button3_Click(null,null);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
        }

        private void chk_movimiento_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbmovimiento.Visible = this.chk_movimiento.Checked;
        }


        public int calcularQuincenas(List<Dictionary<string, object>> resultado2)
        {
            if (resultado2 == null) return 0;
            if (resultado2.Count == 0) return 0;



            double prescripcion = 0;
            double aportacion = 0;
            double devolucion = 0;

            //Declaración de variables....
            List<string> fechasAportacionesRetroactivas = new List<string>();//Guardara los letreros para todas las aportaciones retroactivas
            List<string> fechaRepetidos = new List<string>(); //Guarda todas las fechas de aportaciones repetidas
            List<string> fondoprescripcion = new List<string>(); //Guarda todas las que son fondo de prescripción
            List<Dictionary<string, DateTime>> aportacionNormal = new List<Dictionary<string, DateTime>>();
            List<Dictionary<string, DateTime>> aportacionXReintegro = new List<Dictionary<string, DateTime>>();

            List<Dictionary<string, DateTime>> guardarDf = new List<Dictionary<string, DateTime>>(); //Guarda todas las fechas por si hay algún repetido..
            List<Dictionary<string, DateTime>> guardaDevoluvcionCaja = new List<Dictionary<string, DateTime>>(); //Guarda si hay una devolucón de caja
            List<Dictionary<string, DateTime>> guardaPrenscripcion = new List<Dictionary<string, DateTime>>(); //Guarda si hay preinscripción (new_tipo = DP)

            //------------------
            //Del resultado se saca el maxímo y el mínimo de las fecha de aportaciones y se gurdan en sus respectivas variables
            string sFechaInicio = Convert.ToString(resultado2.Min(o => o["inicio"]));
            string sFechaFinal = Convert.ToString(resultado2.Max(o => o["inicio"]));
            DateTime dFechaInicio = DateTime.Parse(sFechaInicio);
            DateTime dFechaFinal = DateTime.Parse(sFechaFinal);
            //-----------------------------------------------------------------------------------------------

            List<string[]> ldGrafica = new List<string[]>();
            for (int año = dFechaInicio.Year; año <= DateTime.Now.Year; año++)
            {
                string[] arreglo = new string[24];
                for (int mes = 1; mes <= 12; mes++)
                {
                    DateTime inicioMes = new DateTime(año, mes, 1);
                    List<Dictionary<string, object>> registros = resultado2.Where(o => inicioMes.Year == DateTime.Parse(Convert.ToString(o["inicio"])).Year)
                        .Where(o => inicioMes.Month == DateTime.Parse(Convert.ToString(o["inicio"])).Month).ToList<Dictionary<string, object>>();
                    string simbolo = ".";
                    string simboloQuincena1 = ".";
                    string simboloQuincena2 = ".";

                    DateTime quincena = new DateTime(año, mes, 8);

                    bool quincena1 = false;
                    bool quincena2 = false;

                    if (año == 1992 && mes == 4)
                    {

                    }

                    realizarOperacion(aportacionNormal, mes, año, ref quincena1, ref quincena2, ref simbolo, "■", ref simboloQuincena1, ref simboloQuincena2);
                    realizarOperacion(guardarDf, mes, año, ref quincena1, ref quincena2, ref simbolo, "_", ref simboloQuincena1, ref simboloQuincena2);
                    realizarOperacion(aportacionXReintegro, mes, año, ref quincena1, ref quincena2, ref simbolo, "■", ref simboloQuincena1, ref simboloQuincena2, true, guardarDf);
                    realizarOperacion(guardaDevoluvcionCaja, mes, año, ref quincena1, ref quincena2, ref simbolo, "◫", ref simboloQuincena1, ref simboloQuincena2);
                    realizarOperacion(guardaPrenscripcion, mes, año, ref quincena1, ref quincena2, ref simbolo, "◬", ref simboloQuincena1, ref simboloQuincena2, false, guardarDf, guardaDevoluvcionCaja);

                    string simboloAnterior = simbolo;
                    foreach (Dictionary<string, object> item in registros)
                    {
                        DateTime dtInicio = DateTime.Parse(Convert.ToString(item["inicio"]));
                        DateTime dtFinal = DateTime.Parse(Convert.ToString(item["final"]));


                        DateTime dtInicioaux = new DateTime(año, mes, 8);
                        DateTime dtFinalaux = new DateTime(año, mes, 23);



                        string new_tipo = Convert.ToString(item["new_tipo"]);
                        if (new_tipo == "MV") continue;



                        if (new_tipo == "AN")//Aportación normal
                        {
                            simbolo = "■";
                            if (dtInicioaux >= dtInicio && dtInicioaux <= dtFinal)
                            {
                                simboloQuincena1 = "■";
                            }

                            if (dtFinalaux >= dtInicio && dtFinalaux <= dtFinal)
                            {
                                simboloQuincena2 = "■";
                            }

                            bool existe = aportacionNormal.Any(o => DateTime.Parse(Convert.ToString(o["inicio"])) == DateTime.Parse(Convert.ToString(item["inicio"])));
                            if (!existe)
                            {
                                Dictionary<string, DateTime> diccionario = new Dictionary<string, DateTime>();
                                diccionario.Add("inicio", dtInicio);
                                diccionario.Add("final", dtFinal);
                                aportacionNormal.Add(diccionario);
                            }
                        }

                        if (new_tipo == "AP")//Aportación por reintegro
                        {
                            simbolo = "■";
                            if (dtInicioaux >= dtInicio && dtInicioaux <= dtFinal)
                            {
                                simboloQuincena1 = "■";
                            }

                            if (dtFinalaux >= dtInicio && dtFinalaux <= dtFinal)
                            {
                                simboloQuincena2 = "■";
                            }
                            bool existe = aportacionXReintegro.Any(o => DateTime.Parse(Convert.ToString(o["inicio"])) == DateTime.Parse(Convert.ToString(item["inicio"])));
                            if (!existe)
                            {
                                Dictionary<string, DateTime> diccionario = new Dictionary<string, DateTime>();
                                diccionario.Add("inicio", dtInicio);
                                diccionario.Add("final", dtFinal);
                                aportacionXReintegro.Add(diccionario);
                            }
                        }

                        if (new_tipo == "AR")//Aportación retroactivo
                        {
                            fechasAportacionesRetroactivas.Add(string.Format("{0:d}", dtInicio));
                        }
                        if (new_tipo == "DF")
                        {//Devolución de fondo de pensiones
                            bool repetido = guardarDf.Any(o => dtInicio == o["inicio"] && dtFinal == o["final"]);
                            if (!repetido)
                            {
                                Dictionary<string, DateTime> tmpdiccionario = new Dictionary<string, DateTime>();
                                tmpdiccionario.Add("inicio", dtInicio);
                                tmpdiccionario.Add("final", dtFinal);
                                guardarDf.Add(tmpdiccionario);
                            }
                        }
                        if (new_tipo == "DC" || new_tipo == "MN")
                        {//Devolución de caja no laborado
                            Dictionary<string, DateTime> tmpDiccionario = new Dictionary<string, DateTime>();
                            tmpDiccionario.Add("inicio", dtInicio);
                            tmpDiccionario.Add("final", dtFinal);
                            guardaDevoluvcionCaja.Add(tmpDiccionario);
                        }

                        if (new_tipo == "DP")
                        {
                            Dictionary<string, DateTime> tmpDiccionario = new Dictionary<string, DateTime>();
                            tmpDiccionario.Add("inicio", dtInicio);
                            tmpDiccionario.Add("final", dtFinal);
                            guardaPrenscripcion.Add(tmpDiccionario);
                        }




                        int repetidoAn = registros.Count(o => ("AN" == Convert.ToString(o["new_tipo"])) && (dtInicio >= DateTime.Parse(Convert.ToString(o["inicio"])) && dtFinal <= DateTime.Parse(Convert.ToString(o["final"]))));
                        if (repetidoAn > 1)
                        {
                            simboloAnterior = simbolo;
                            simbolo = "▌";
                            if (dtInicioaux >= dtInicio && dtInicioaux <= dtFinal)
                            {
                                simboloQuincena1 = simbolo;
                            }

                            if (dtFinalaux >= dtInicio && dtFinalaux <= dtFinal)
                            {
                                simboloQuincena2 = simbolo;
                            }
                            bool boolRepetidoAn = fechaRepetidos.Any(o => DateTime.Parse(o) == dtInicio);
                            if (!boolRepetidoAn)
                                fechaRepetidos.Add(string.Format("{0:d}", dtInicio));
                        }

                        if (guardarDf.Count != 0 && simbolo != "▌")
                        {
                            bool devolucionFondo = guardarDf.Any(o => dtInicio >= o["inicio"] && dtInicio <= o["final"]);
                            if (devolucionFondo)
                            {
                                simboloAnterior = simbolo;
                                simbolo = (!tipoGrafica) ? "." : "_";
                                if (dtInicioaux >= dtInicio && dtInicioaux <= dtFinal)
                                {
                                    simboloQuincena1 = (!tipoGrafica) ? "." : "_";
                                }

                                if (dtFinalaux >= dtInicio && dtFinalaux <= dtFinal)
                                {
                                    simboloQuincena2 = (!tipoGrafica) ? "." : "_";
                                }
                            }
                        }

                        if (aportacionXReintegro.Count != 0)
                        {
                            if (guardarDf.Count > 1)
                            {
                                for (int x = 1; x < guardarDf.Count; x++)
                                {
                                    DateTime dInicio = guardarDf[x]["inicio"];
                                    DateTime dFinal = guardarDf[x]["final"];
                                    if (dtInicio >= dInicio && dtFinal <= dFinal)
                                    {
                                        goto continuar;
                                    }
                                }
                            }
                            bool devolucionFondo = aportacionXReintegro.Any(o => dtInicio >= o["inicio"] && dtFinal <= o["final"]);
                            if (devolucionFondo)
                            {
                                simboloAnterior = simbolo;
                                simbolo = "■";
                                if (dtInicioaux >= dtInicio && dtInicioaux <= dtFinal)
                                {
                                    simboloQuincena1 = "■";
                                }

                                if (dtFinalaux >= dtInicio && dtFinalaux <= dtFinal)
                                {
                                    simboloQuincena2 = "■";
                                }
                            }
                        }

                        continuar:
                        if (guardaDevoluvcionCaja.Count != 0)
                        {
                            bool devolucionFondo = guardaDevoluvcionCaja.Any(o => dtInicio >= o["inicio"] && dtFinal <= o["final"]);
                            if (devolucionFondo)
                            {
                                simboloAnterior = simbolo;
                                simbolo = (!tipoGrafica) ? "." : "◫";
                                if (dtInicioaux >= dtInicio && dtInicioaux <= dtFinal)
                                {
                                    simboloQuincena1 = (!tipoGrafica) ? "." : "◫";
                                }

                                if (dtFinalaux >= dtInicio && dtFinalaux <= dtFinal)
                                {
                                    simboloQuincena2 = (!tipoGrafica) ? "." : "◫";
                                }
                            }
                        }

                        if (guardaPrenscripcion.Count != 0)
                        {
                            bool prenscripcion = guardaPrenscripcion.Any(o => dtInicio >= o["inicio"] && dtFinal <= o["final"]);
                            if (prenscripcion)
                            {
                                bool devolucionFondo = guardarDf.Any(o => dtInicio >= o["inicio"] && dtFinal <= o["final"]);
                                bool devolucionCaja = guardaDevoluvcionCaja.Any(o => dtInicio >= o["inicio"] && dtFinal <= o["final"]);

                                simboloAnterior = simbolo;
                                simbolo = (!tipoGrafica) ? (devolucionFondo || devolucionCaja) ? "." : "■" : "◬";
                                if (dtInicioaux >= dtInicio && dtInicioaux <= dtFinal)
                                {
                                    simboloQuincena1 = (!tipoGrafica) ? (devolucionFondo || devolucionCaja) ? "." : "■" : "◬";
                                }

                                if (dtFinalaux >= dtInicio && dtFinalaux <= dtFinal)
                                {
                                    simboloQuincena2 = (!tipoGrafica) ? (devolucionFondo || devolucionCaja) ? "." : "■" : "◬";
                                }
                            }
                        }


                        DateTime auxFecha = new DateTime(año, mes, 8);
                        if (auxFecha >= dtInicio && auxFecha <= dtFinal)
                            arreglo[(mes * 2 - 1) - 1] = (arreglo[(mes * 2 - 1) - 1] == "▌") ? "▌" : simboloQuincena1;
                        auxFecha = new DateTime(año, mes, 23);
                        if (auxFecha >= dtInicio && auxFecha <= dtFinal)
                        {
                            arreglo[(mes * 2 - 1) - 1] = arreglo[(mes * 2 - 1) - 1] == null ? simboloQuincena1 : arreglo[(mes * 2 - 1) - 1];
                            arreglo[(mes * 2) - 1] = arreglo[(mes * 2) - 1] == "▌" ? "▌" : simboloQuincena2;
                        }

                    }

                    if (DateTime.Now.Year == año && DateTime.Now.Month <= mes)
                    {
                        arreglo[(mes * 2 - 1) - 1] = "";
                        arreglo[(mes * 2) - 1] = "";
                        if (mes != 5 && mes != 12)
                        {
                            if (DateTime.Now < new DateTime(año, mes, 15) && DateTime.Now.Month == mes)
                                arreglo[(mes * 2 - 1) - 1] = ".";
                        }
                    }

                    arreglo[(mes * 2 - 1) - 1] = arreglo[(mes * 2 - 1) - 1] == null ? quincena1 ? simboloQuincena1 : "." : arreglo[(mes * 2 - 1) - 1];
                    arreglo[(mes * 2) - 1] = arreglo[(mes * 2) - 1] == null ? quincena2 ? simboloQuincena2 : "." : arreglo[(mes * 2) - 1];
                }
                ldGrafica.Add(arreglo);
            }
            //
            int contador = 0;
            foreach (string[] item in ldGrafica)
            {
                foreach (string elemento in item)
                {
                    if (elemento == "■" || elemento == "▌")
                    {
                        contador++;
                    }
                }
            }

            return contador;
        }

        private void realizarOperacion(List<Dictionary<string, DateTime>> lista, int mes, int año, ref bool quincena1, ref bool quincena2, ref string simbolo, string stringImagen, ref string simboloQuincena1, ref string simboloQuincena2, bool reintegro = false, List<Dictionary<string, DateTime>> devoluciones = null, List<Dictionary<string, DateTime>> devCaja = null)
        {
            if (reintegro)
            {
                if (devoluciones.Count > 1)
                {
                    for (int x = 1; x < devoluciones.Count; x++)
                    {
                        DateTime dInicio = devoluciones[x]["inicio"];
                        DateTime dFinal = devoluciones[x]["final"];
                        foreach (Dictionary<string, DateTime> item in lista)
                        {
                            DateTime inicio = item["inicio"];
                            DateTime final = item["final"];
                            if (dInicio >= inicio && final <= dFinal)
                            {
                                return;
                            }
                        }
                    }
                }
            }

            foreach (Dictionary<string, DateTime> item in lista)
            {
                DateTime inicio = item["inicio"];
                DateTime final = item["final"];

                if (mes == 5 || mes == 12)
                {
                    DateTime aux1 = new DateTime(año, mes, 1);
                    DateTime aux2 = new DateTime(año, mes, 31);
                    if (aux1 >= inicio && (aux2 <= final || aux2.AddDays(-1) <= final))
                    {
                        if (stringImagen == "◬")
                        {
                            bool devolucionFondo = devoluciones.Any(o => aux1 >= o["inicio"] && (aux2 <= o["final"] || aux2.AddDays(-1) <= o["final"]));
                            bool devolucionCaja = devCaja.Any(o => aux1 >= o["inicio"] && (aux2 <= o["final"] || aux2.AddDays(-1) <= o["final"]));

                            simbolo = (!tipoGrafica) ? (devolucionFondo || devolucionCaja) ? "." : "■" : stringImagen;
                            simbolo = ("■" == stringImagen) ? "■" : simbolo;
                            simboloQuincena1 = simbolo;
                            simboloQuincena2 = simbolo;
                            quincena1 = true;
                            quincena2 = true;
                            break;
                        }
                        simbolo = (!tipoGrafica) ? "." : stringImagen;
                        simbolo = ("■" == stringImagen) ? "■" : simbolo;
                        simboloQuincena1 = simbolo;
                        simboloQuincena2 = simbolo;
                        quincena1 = true;
                        quincena2 = true;
                        break;
                    }
                }
                else
                {
                    DateTime aux1 = new DateTime(año, mes, 1);
                    DateTime aux2 = new DateTime(año, mes, 15);

                    if (aux1 >= inicio && aux2 <= final)
                    {
                        if (stringImagen == "◬")
                        {
                            bool existe = devoluciones.Any(o => aux1 >= o["inicio"] && aux2 <= o["final"]);
                            bool devCa = devCaja.Any(o => aux1 >= o["inicio"] && (aux2 <= o["final"] || aux2.AddDays(-1) <= o["final"]));
                            simbolo = (!tipoGrafica) ? (existe || devCa) ? "." : simbolo : stringImagen;
                            simbolo = ("■" == stringImagen) ? "■" : simbolo;
                            simboloQuincena1 = simbolo;
                            quincena1 = true;
                        }
                        else
                        {
                            simbolo = (!tipoGrafica) ? "." : stringImagen;
                            simbolo = ("■" == stringImagen) ? "■" : simbolo;
                            simboloQuincena1 = simbolo;
                            quincena1 = true;
                        }
                    }

                    aux1 = new DateTime(año, mes, 16);
                    aux2 = new DateTime(año, mes, (mes == 2) ? 28 : 30);


                    if (aux1 >= inicio && aux2 <= final)
                    {

                        if (stringImagen == "◬")
                        {
                            bool existe = devoluciones.Any(o => aux1 >= o["inicio"] && aux2 <= o["final"]);
                            bool devCa = devCaja.Any(o => aux1 >= o["inicio"] && (aux2 <= o["final"] || aux2.AddDays(-1) <= o["final"]));
                            simbolo = (!tipoGrafica) ? (existe || devCa) ? "." : simbolo : stringImagen;
                            simbolo = ("■" == stringImagen) ? "■" : simbolo;
                            simboloQuincena2 = simbolo;
                            quincena2 = true;
                        }
                        else
                        {
                            simbolo = (!tipoGrafica) ? "." : stringImagen;
                            simbolo = ("■" == stringImagen) ? "■" : simbolo;
                            simboloQuincena2 = simbolo;
                            quincena2 = true;
                        }
                    }

                }
            }
        }
    }
}
