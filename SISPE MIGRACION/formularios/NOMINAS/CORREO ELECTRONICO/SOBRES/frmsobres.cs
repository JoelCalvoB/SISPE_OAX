using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SISPE_MIGRACION.reportes;
using System.IO;
using System.Data.OleDb;

namespace SISPE_MIGRACION.formularios.sobres
{
    public partial class frmsobres : Form
    {
        private string proyecto, nombre, curp, rfc, imss, categ, clave, descri, monto, fecha, periodo;
        private Dictionary<string, string> meses;
        private BackgroundWorker trabajo;
        private List<Dictionary<string, object>> resultado;
        private bool bandera=true;
        private string maximo;
        private void button4_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private bool tipoGrafica = false;
        private void button3_Click(object sender, EventArgs e)
        {


            List<Dictionary<string, object>> resultadoChild;

            string query = "select \"RFC\" AS rfc from datos.rfcnuevo order by \"RFC\"";
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            int contador = 0;
            List<Dictionary<string, string>> quincenas = new List<Dictionary<string, string>>();
            Dictionary<string, string> obj;
            int auxcontador = 1;
            string rfc = string.Empty;
            foreach (Dictionary<string, object> item in resultado)
            {
                rfc = Convert.ToString(item["rfc"]);
                obj = new Dictionary<string, string>();
                obj.Add("rfc", rfc);
                resultadoChild = globales.consulta2($"select inicio,final,new_tipo from datos.aportaciones where rfc like '%{rfc.Substring(0, 10)}%' and final <='2017-12-31'");
                contador = calcularQuincenas(resultadoChild);
                obj.Add("Quincenas", contador.ToString());
                quincenas.Add(obj);
                contador = 0;

                System.Diagnostics.Debug.WriteLine(auxcontador);
                auxcontador++;
            }

            //obj = new Dictionary<string, string>();
            //obj.Add("RFC", actual);
            ////resultadoChild = resultado.Where(o => Convert.ToString(o["rfc"]) == actual).ToList<Dictionary<string, object>>();
            //resultadoChild = globales.consulta2($"select inicio,final,new_tipo from datos.aportaciones where rfc like '{actual}%'");
            //contador = calcularQuincenas(resultadoChild);
            //obj.Add("Quincenas", contador.ToString());
            //quincenas.Add(obj);

            object[] objeto = new object[quincenas.Count];
            int tmp = 0;
            foreach (Dictionary<string, string> item in quincenas)
            {
                int Qtotales = Convert.ToInt32(item["Quincenas"]);
                int AA = Convert.ToInt32((Qtotales) / 24);
                int QAux = Qtotales - (AA * 24);
                int AM = Convert.ToInt32((QAux / 2));
                int AQ = QAux - (AM * 2);

                int Ant_A = AA;
                int Ant_M = AM;
                int Ant_Q = AQ;
                object[] tt1 = { item["rfc"], item["Quincenas"], Ant_A.ToString(), Ant_M.ToString(), Ant_Q.ToString() };
                objeto[tmp] = tt1;
                tmp++;
            }
            globales.reportes("auxReporte", "p_quirog", objeto);
        }

        private void frmsobres_Shown(object sender, EventArgs e)
        {
         
        }

        private void txtArchivo_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtperiodo_Enter(object sender, EventArgs e)
        {
            


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

        private void cmb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.messs = cmb1.SelectedIndex;


            string aux = $"01/{cmb1.SelectedIndex + 1}/{Convert.ToInt32(txt1.Text)}";
            DateTime tiempo = DateTime.Parse(aux);
            tiempo = tiempo.AddMonths(1);
            tiempo = tiempo.AddDays(-1);



            string[] meses = globales.getMeses();

            string messeleccionado = meses[cmb1.SelectedIndex + 1];

            string mensaje = $"Del 01 al {tiempo.Day} de {messeleccionado} del {txt1.Text}";
            txtperiodo.Text = mensaje;
        }

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chk1_Click(object sender, EventArgs e)
        {
            this.lista.Visible = chk1.Checked;
            
        }

        private void lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionado = this.lista.SelectedIndex;
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

        private void button2_Click(object sender, EventArgs e)
        {
            string ruta = @"C:\Users\samv\Desktop\work\ACTUDBF";
            int contador = 0;
            foreach (string item in Directory.GetFiles(ruta))
            {
                string cadena = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source= {0}\\; Extended Properties =dBase IV; User ID=;Password=;";
                string pasa = string.Format(cadena, ruta);
                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = pasa;
                con.Open();

                string n1 = item.Substring(item.LastIndexOf('\\') + 1);
                string archivo = n1.Substring(0, n1.IndexOf('.'));

                string query = $"select rfc,nombre,proyecto,aportacion,desde,hasta,tipoaporta from {archivo}";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                DataSet dt = new DataSet();
                try
                {
                    adapter.Fill(dt);
                }
                catch
                {
                    File.Copy(item, $@"C:\Users\samv\Desktop\work\{archivo}.DBF");
                    continue;
                }
                con.Close();
                query = "";

                foreach (DataRow x in dt.Tables[0].Rows)
                {
                    string rfc = x["rfc"].ToString();
                    string nombre = x["nombre"].ToString();
                    string proyecto = x["proyecto"].ToString();
                    string aportacion = x["aportacion"].ToString();
                    aportacion = string.IsNullOrWhiteSpace(aportacion) ? "0" : aportacion;


                    string aux = archivo;
                    aux = aux.Substring(aux.Length - 4);
                    string año = aux.Substring(0, 2);
                    int quincena = Convert.ToInt32(aux.Substring(2));

                    int[] quincenas = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12 };

                    string desde = "";
                    string hasta = "";
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(x["desde"])))
                    {
                        desde = string.Format("{0:yyyy-MM-dd}", x["desde"]);
                    }
                    else
                    {
                        if (quincena % 2 != 0)
                        {
                            desde = string.Format("{0}-{1}-{2}", "20" + año, quincenas[quincena - 1], 1);
                        }
                        else
                        {
                            desde = string.Format("{0}-{1}-{2}", "20" + año, quincenas[quincena - 1], 16);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(x["hasta"])))
                    {
                        hasta = string.Format("{0:yyyy-MM-dd}", x["hasta"]);
                    }
                    else
                    {
                        if (quincena % 2 != 0)
                        {
                            hasta = string.Format("{0}-{1}-{2}", "20" + año, quincenas[quincena - 1], 15);
                        }
                        else
                        {
                            hasta = string.Format("{0}-{1}-{2}", "20" + año, quincenas[quincena - 1], 28);
                        }
                    }

                    string tipoaporta = x["tipoaporta"].ToString();
                    query += $"insert into israel.aportaciones values ('{rfc}','{nombre}','{proyecto}',{aportacion},'{desde}','{hasta}','{tipoaporta}','{archivo}','',''); ";
                }

                globales.consulta(query, true);
                //this.datos.DataSource = dt.Tables[0];
                contador++;
                System.Diagnostics.Debug.WriteLine(contador);
            }



        }

        private void frmsobres_Load(object sender, EventArgs e)
        {
            txt1.Text = DateTime.Now.Year.ToString();
            cmb1.SelectedIndex = DateTime.Now.Month - 1;


            messs = cmb1.SelectedIndex;


            lista.SelectedIndex = 0;
            this.seleccionado = 0;
        }

        private string agrupar;
        private int messs;
        private string directorio;

        public int seleccionado { get; private set; }

        public frmsobres()
        {
            InitializeComponent();
            meses = new Dictionary<string, string>();
            meses.Add("01", "Enero");
            meses.Add("02", "Febrero");
            meses.Add("03", "Marzo");
            meses.Add("04", "Abril");
            meses.Add("05", "Mayo");
            meses.Add("06", "Junio");
            meses.Add("07", "Julio");
            meses.Add("08", "Agosto");
            meses.Add("09", "Septiembre");
            meses.Add("10", "Octubre");
            meses.Add("11", "Noviembre");
            meses.Add("12", "Diciembre");


            string saca = $"SELECT max (archivo) as maximo  FROM nominas_catalogos.respaldos_nominas;";
            List<Dictionary<string, object>> archivo = globales.consulta(saca);
            this.maximo = Convert.ToString(archivo[0]["maximo"]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult p = globales.MessageBoxQuestion("¿Desea generar los sobres de manera masiva?", "Generación de sobres", globales.menuPrincipal);
            if (p == DialogResult.No)
            {
                return;
            }

            DialogResult a = globales.MessageBoxQuestion($"Es correcto el mes que se selecciono:{cmb1.Text} y el periodo {txtperiodo.Text}?" ,"VERIFICANDO", globales.menuPrincipal);
            if (a==DialogResult.No)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtperiodo.Text))
            {
                globales.MessageBoxExclamation("Se debe ingresar el periodo", "Aviso", globales.menuPrincipal);
                txtperiodo.Focus();
                return;
            }

            directorio = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\pdfjubilados";
            globales.MessageBoxInformation($"La ruta de los PDFs generados se encuentran en: \" {directorio} \"", "Aviso", globales.menuPrincipal);

            //Se crea los directorios necesarios}

            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            if (chk1.Checked) {
                foreach (string archivos in Directory.GetFiles(directorio))
                {
                    File.Delete(archivos);
                }
            }




            //  this.trabajo = new BackgroundWorker();
            // this.trabajo.DoWork += new DoWorkEventHandler(generarReporte);
            // this.trabajo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(completado);

            //  this.trabajo.RunWorkerAsync();

            generarReporte(null,null);



            this.Cursor = Cursors.Default;


         



        }

        private void completado(object sender, RunWorkerCompletedEventArgs e)
        {

            string directorio = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\pdfjubilados";

            int total = Directory.GetFiles(directorio, "*.*", SearchOption.AllDirectories).Length;

            if (total <= 1)
            {
                DialogResult dialogo4 = globales.MessageBoxError("OCURRIO UN ERROR", "VERIFICAR", globales.menuPrincipal);
            }
            else
            {
                globales.MessageBoxSuccess("Pdf's Generados correctamente", "Aviso", globales.menuPrincipal);

            }
        }

        private void generarReporte(object sender, DoWorkEventArgs e)
        {
            string query = "SELECT  concat(a1.jpp,a1.num) as proyecto  FROM 	nominas_catalogos.maestro a1 JOIN nominas_catalogos.nominew a2 ON a1.num = a2.numjpp WHERE 	a1.superviven = 'S' AND a1.jpp = a2.jpp AND ( 	a1.correo <> '' 	AND a1.correo IS NOT NULL ) group by a1.jpp,a1.num  ORDER BY a1.jpp,a1.num";
            this.resultado = globales.consulta(query);
            this.resultado.ForEach(o => sacarReporte(Convert.ToString(o["proyecto"])));

            
        }

        private void sacarReporte(string jubilado)
        {
            if (jubilado.Contains("PTA"))
            {
              //  MessageBox.Show("AQUI");
            }

            if (bandera == false) return;

           

            System.Diagnostics.Debug.WriteLine("generando el jubilado " + jubilado);
            DateTime fechaSobres = new DateTime();
            DateTime fec2 = fecha2.Value;
           // string c2 = string.Format("{0}-{1}-{2}", fec2.Year, fec2.Month, fec2.Day);

            string año1 = txt1.Text.Substring(2);
            string mes1 = this.messs < 10 ? "0" + (this.messs + 1 ) : (this.messs+1).ToString();

            string archivo1 = año1 + mes1;

             
            if (this.maximo != archivo1)
            {
                this.bandera = false;
                return;

            }

            string tipo_nomina = "N";
            if (chk1.Checked) {
                switch (this.seleccionado) {
                    case 0:
                        tipo_nomina = "AG";
                        break;
                }

                if (!Directory.Exists(directorio + "\\" + tipo_nomina))
                {
                    Directory.CreateDirectory(directorio + "\\" + tipo_nomina);
                }

            }
            

            string query = $"CREATE TEMP TABLE respaldoNom AS ( SELECT		*	FROM	nominas_catalogos.respaldos_nominas WHERE	archivo = '{archivo1}');" +
               $"SELECT concat (a1.jpp, a1.num) AS proyecto, a1.nombre,a1.curp,a1.rfc,a1.imss,a1.categ,a2.clave,a2.descri,a2.monto,	a2.pago4 AS pagon,	a2.pagot,	a2.leyen FROM nominas_catalogos.maestro a1 JOIN respaldoNom a2 ON a1.num = a2.numjpp WHERE a1.superviven = 'S'AND a1.jpp = a2.jpp AND concat (a1.jpp, a1.num) = '{jubilado}' AND a2.tipo_nomina = '{tipo_nomina}' order by clave";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0) {
               // this.trabajo.WorkerSupportsCancellation=true;
               // this.trabajo.CancelAsync();
               // this.bandera = false;
                return;                

            }
            string compara = Convert.ToString(resultado[0]["proyecto"]);
            if (compara != jubilado)
            {
                this.bandera = false;
                return;
            }
                
            query = "select  clave,descri from nominas_catalogos.perded order by clave";
            List<Dictionary<string, object>> perded = globales.consulta(query);
            resultado.ForEach(o => {
                o["descri"] = perded.Where(p => Convert.ToString(o["clave"]) == Convert.ToString(p["clave"])).First()["descri"];
                //  o["descri"] += " (RETROACTIVO)";
            });


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
                fecha = fec2.ToString();
                periodo = txtperiodo.Text;
                string archivo = string.Empty;
                int año = 0;
                int mes = 0;
                string pago4 = string.Empty;
                string pagot = string.Empty;
                try
                {

                    proyecto = Convert.ToString(item["proyecto"]);
                    nombre = Convert.ToString(item["nombre"]);
                    curp = Convert.ToString(item["curp"]);
                    rfc = Convert.ToString(item["rfc"]);
                    imss = Convert.ToString(item["imss"]);
                    categ = Convert.ToString(item["categ"]);
                    clave = Convert.ToString(item["clave"]);
                    descri = Convert.ToString(item["descri"])+(string.IsNullOrWhiteSpace(Convert.ToString(item["leyen"])) ? "" : $"({Convert.ToString(item["leyen"])})");
                    monto = string.Format("{0:C}", Convert.ToDouble(item["monto"])).Replace("$", "");

                    año = Convert.ToInt32(txt1.Text);
                    mes = messs+1;
                
                        fechaSobres = new DateTime(año, mes , 1);

                    fechaSobres = fechaSobres.AddDays(-1);
                    pago4 = Convert.ToString(item["pagon"]);
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

            object[] objeto = new object[13];
            for (int x = 0; x < 13; x++)
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
                if (x == 13)
                {
                    System.Diagnostics.Debug.WriteLine(proyecto + " " + nombre + " " + rfc);
                    break;
                }
                objeto[x] = aux2[x];
                object[] sacarDato = (object[])aux2[x];
                //  double percepcion = string.IsNullOrWhiteSpace(Convert.ToString(sacarDato[8])) ? 0 : Convert.ToDouble(sacarDato[8]);
                // double deduccion = string.IsNullOrWhiteSpace(Convert.ToString(sacarDato[11])) ? 0 : Convert.ToDouble(sacarDato[11]);
                //sumaPercepciones += percepcion;
                //sumaDeducciones += deduccion;

            }


            
            object[] parametros = { "proyecto", "nombre", "curp", "rfc", "imss", "categ", "fechapago", "periodo", "sumaPercepcion", "sumaDeduccion" };
            object[] valor = { proyecto, nombre, curp, rfc, imss, categ, string.Format("{0:d}", fechaSobres), periodo, sumaPercepciones.ToString(), sumaDeducciones.ToString() };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;


            //  globales.ocultarEpilep = true;
            string subcarpeta = @"\"+tipo_nomina;
            if (tipo_nomina.Contains("N")) {
                subcarpeta = string.Empty;
            }
            globales.reportes("sobresprueba", "sobres", objeto, "", true, enviarParametros, true, jubilado,subcarpeta);

        }
    }
}
