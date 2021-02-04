using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones
{
    public partial class frmactuario : Form
    {
        private bool tipoGrafica = false;
        Dictionary<string, object> resultado;
        List<Dictionary<string, object>> resultado2;
        List<Dictionary<string, object>> resultado5;
        private bool entro;
        double sueldo_mensuals;



        private List<string[]> auxGraficaOriginal;
        private List<string[]> auxGraficoAportaciones;

        public frmactuario()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmreporlisaporta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void frmreporlisaporta_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogo = MessageBox.Show("¿Desea cerrar el módulo?",
          "Cerrar el módulo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogo == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {


            metodoactuario();


        }

        private void metodoactuario()
        {
            this.Cursor = Cursors.WaitCursor;

            string fecha = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(dateTimePicker1.Text));
            List<Dictionary<string, object>> resultadoChild;


            string query = "create temp table actuario as SELECT rfc,max(entrada) as aportacion FROM datos.aportaciones WHERE new_tipo LIKE 'A%' " +
                $"AND rfc <> '' AND FINAL = '{fecha}' AND COALESCE (TRIM(status), '') <> 'e' GROUP BY	rfc ORDER BY	rfc;" +
                " create temp table actuario2 as SELECT P .nombre_em,actuario.rfc,	P .sexo,	substr(actuario.rfc, 5, 6) as f_nac,	(actuario.aportacion/9*100)as " +
                " sueldo_mensual,	P .tipo_rel  , P.modalidad   FROM actuario left	JOIN datos.empleados P ON actuario.rfc  = P.rfc  ORDER BY	P .rfc; " +
                " drop table actuario; " +
                " create temp table actuario  as select rfc,max(nombre_em) as nombre_em ,max(sexo) as sexo, max(f_nac) as f_nacimiento,max(sueldo_mensual) as  " +
                " sueldo_mensual,max(tipo_rel) as tipo_rel  , max (modalidad) as modalidad from actuario2 group by rfc; " +
                " create temp table procesar as select actuario.*,min(d.inicio) as inicio from actuario inner join datos.aportaciones d on actuario.rfc = d.rfc group by  " +
                " actuario.rfc,actuario.nombre_em,actuario.sexo,actuario.f_nacimiento,actuario.sueldo_mensual,actuario.modalidad, actuario.tipo_rel; " +
                " create temp table tablasaldo as select procesar.rfc,(sum(d.entrada) - sum(d.salida)) as saldo from procesar left join datos.aportaciones d on procesar.rfc =  " +
                $" d.rfc where procesar.rfc=d.rfc  and COALESCE(trim(d.status),'')<>'e' and final <='{fecha}' group by procesar.rfc; " +
                " select procesar.*,tablasaldo.saldo from procesar inner join tablasaldo on procesar.rfc = tablasaldo.rfc; ";



            int contador = 0;
            int auxcontador = 0;
            string rfc = string.Empty;
            string nombre = string.Empty;
            string sexo = string.Empty;
            string fching = string.Empty;
            string nombre_em = string.Empty;
            string f_nacimiento = string.Empty;
            string sueldo_mensual = string.Empty;
            string tipo_rel = string.Empty;
            string inicio = string.Empty;
            string saldo = string.Empty;
            string modalidad = string.Empty;

            List<Dictionary<string, object>> resultado = globales.consulta(query);
            object[] aux2 = new object[resultado.Count];

            foreach (Dictionary<string, object> item in resultado)
            {
                rfc = Convert.ToString(item["rfc"]);

                nombre = Convert.ToString(item["nombre_em"]);
                // fching = Convert.ToString(item["fching"]);
                nombre_em = Convert.ToString(item["nombre_em"]);
                sexo = Convert.ToString(item["sexo"]);
                string mezclafecha = Convert.ToString(item["f_nacimiento"]);
                string anio = mezclafecha.Substring(0, 2); string mes = mezclafecha.Substring(2, 2); string dia = mezclafecha.Substring(4, 2);
                f_nacimiento = dia + "/" + mes + "/" + anio;
                sueldo_mensual = Convert.ToString(item["sueldo_mensual"]);
                if (!string.IsNullOrWhiteSpace(sueldo_mensual))
                {
                     sueldo_mensuals = Math.Round(Convert.ToDouble(sueldo_mensual));

                }

                double sueldo_b = Convert.ToDouble(sueldo_mensual);
                string sueldo_redondo = String.Format("{0:0.00}", sueldo_b);
                modalidad = Convert.ToString(item ["modalidad"]);

                if (string.IsNullOrWhiteSpace(modalidad))
                {
                    tipo_rel = Convert.ToString(item["tipo_rel"]);

                }
                else
                {
                    tipo_rel = modalidad;
                }

                inicio = Convert.ToString(item["inicio"]).Replace(" 12:00:00 a. m.", "");
                saldo = Convert.ToString(item["saldo"]);
                resultadoChild = globales.consulta($"select inicio, final, new_tipo, entrada, salida, cuenta, movimiento,fecharegistro, id" +
         $" FROM datos.aportaciones WHERE rfc = '{rfc}' AND COALESCE (TRIM(status),'')<>'e' AND COALESCE (TRIM(status),'')<>'p' and  final <='{fecha}' " +
         "ORDER BY inicio,new_tipo asc; ");
                contador = calcularQuincenas(resultadoChild);
                int AA = Convert.ToInt32((contador) / 24);
                int QAux = contador - (AA * 24);
                int AM = Convert.ToInt32((QAux / 2));
                int AQ = QAux - (AM * 2);

                int Ant_A = AA;
                int Ant_M = AM;
                int Ant_Q = AQ;

                item.Add("Quincenas", contador.ToString());
                item.Add("Años", Ant_A);
                item.Add("Meses", Ant_M);
                item.Add("Quincena", Ant_Q);



                this.Cursor = Cursors.Default;
                object[] tt1 = { rfc, nombre, sexo, f_nacimiento,this.sueldo_mensuals, tipo_rel, inicio, saldo, contador, Ant_A, Ant_M, Ant_Q };
                contador = 0;

                aux2[auxcontador] = tt1;

                System.Diagnostics.Debug.WriteLine(auxcontador);
                auxcontador++;
            }
            object[] parametros = { "fech1" };
            object[] valor = { dateTimePicker1.Text };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reportactuario", "tablaactuario", aux2, "", false, enviarParametros);

        }

        public int calcularQuincenas(List<Dictionary<string, object>> resultado2)
        {
            if (resultado2 == null) return 0;
            if (resultado2.Count == 0) return 0;
            regresar:

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
            List<Dictionary<string, DateTime>> guardaAportacionesIndebidas = new List<Dictionary<string, DateTime>>();
            List<Dictionary<string, DateTime>> guardaDevoluvcionCaja = new List<Dictionary<string, DateTime>>(); //Guarda si hay una devolucón de caja
            List<Dictionary<string, DateTime>> guardaPrenscripcion = new List<Dictionary<string, DateTime>>(); //Guarda si hay preinscripción (new_tipo = DP)
            List<Dictionary<string, DateTime>> guardaPeriodoNoLaborado = new List<Dictionary<string, DateTime>>(); //Guarda si hay preinscripción (new_tipo = DP)

            //------------------
            //Del resultado se saca el maxímo y el mínimo de las fecha de aportaciones y se gurdan en sus respectivas variables
            string sFechaInicio = Convert.ToString(resultado2.Min(o => o["inicio"]));
            string sFechaFinal = Convert.ToString(resultado2.Max(o => o["inicio"]));
            DateTime dFechaInicio = DateTime.Parse(sFechaInicio);
            DateTime dFechaFinal = DateTime.Parse(sFechaFinal);
            //-----------------------------------------------------------------------------------------------

            List<string[]> ldGrafica = new List<string[]>();

            int cantidad = 0;

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

                    if (año == 1969 && mes == 4)
                    {

                    }



                    realizarOperacion(aportacionNormal, mes, año, ref quincena1, ref quincena2, ref simbolo, "■", ref simboloQuincena1, ref simboloQuincena2);
                    realizarOperacion(guardaAportacionesIndebidas, mes, año, ref quincena1, ref quincena2, ref simbolo, "X", ref simboloQuincena1, ref simboloQuincena2);
                    realizarOperacion(guardarDf, mes, año, ref quincena1, ref quincena2, ref simbolo, "_", ref simboloQuincena1, ref simboloQuincena2);
                    realizarOperacion(guardaPrenscripcion, mes, año, ref quincena1, ref quincena2, ref simbolo, "◬", ref simboloQuincena1, ref simboloQuincena2, false, guardarDf, guardaDevoluvcionCaja);
                    realizarOperacion(aportacionXReintegro, mes, año, ref quincena1, ref quincena2, ref simbolo, "■", ref simboloQuincena1, ref simboloQuincena2, true, guardarDf);
                    realizarOperacion(guardaDevoluvcionCaja, mes, año, ref quincena1, ref quincena2, ref simbolo, "◫", ref simboloQuincena1, ref simboloQuincena2);
                    realizarOperacion(guardaPeriodoNoLaborado, mes, año, ref quincena1, ref quincena2, ref simbolo, "▥", ref simboloQuincena1, ref simboloQuincena2);

                    string simboloAnterior = simbolo;

                    foreach (Dictionary<string, object> item in registros)
                    {
                        cantidad++;
                        DateTime dtInicio = DateTime.Parse(Convert.ToString(item["inicio"]));
                        DateTime dtFinal = DateTime.Parse(Convert.ToString(item["final"]));


                        DateTime dtInicioaux = new DateTime(año, mes, 8);
                        DateTime dtFinalaux = new DateTime(año, mes, 23);



                        string new_tipo = Convert.ToString(item["new_tipo"]);
                        if (new_tipo == "MV")
                        {
                            aportacion += string.IsNullOrWhiteSpace(Convert.ToString(item["entrada"])) ? 0 : Convert.ToDouble(item["entrada"]);
                            devolucion += string.IsNullOrWhiteSpace(Convert.ToString(item["salida"])) ? 0 : Convert.ToDouble(item["salida"]);

                            continue;
                        }


                        aportacion += string.IsNullOrWhiteSpace(Convert.ToString(item["entrada"])) ? 0 : Convert.ToDouble(item["entrada"]);
                        if (new_tipo == "DP")
                        {
                            prescripcion += string.IsNullOrWhiteSpace(Convert.ToString(item["salida"])) ? 0 : Convert.ToDouble(item["salida"]);
                        }
                        else
                        {
                            devolucion += string.IsNullOrWhiteSpace(Convert.ToString(item["salida"])) ? 0 : Convert.ToDouble(item["salida"]);
                        }

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

                        if (new_tipo == "AI")//Aportación indebida
                        {
                            simbolo = "X";
                            if (dtInicioaux >= dtInicio && dtInicioaux <= dtFinal)
                            {
                                simboloQuincena1 = "X";
                            }

                            if (dtFinalaux >= dtInicio && dtFinalaux <= dtFinal)
                            {
                                simboloQuincena2 = "X";
                            }

                            bool existe = guardaAportacionesIndebidas.Any(o => DateTime.Parse(Convert.ToString(o["inicio"])) == DateTime.Parse(Convert.ToString(item["inicio"])));
                            if (!existe)
                            {
                                Dictionary<string, DateTime> diccionario = new Dictionary<string, DateTime>();
                                diccionario.Add("inicio", dtInicio);
                                diccionario.Add("final", dtFinal);
                                guardaAportacionesIndebidas.Add(diccionario);
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
                        if (new_tipo == "DC" || new_tipo == "MV")
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

                        if (new_tipo == "MN")
                        {
                            Dictionary<string, DateTime> tmpDiccionario = new Dictionary<string, DateTime>();
                            tmpDiccionario.Add("inicio", dtInicio);
                            tmpDiccionario.Add("final", dtFinal);
                            guardaPeriodoNoLaborado.Add(tmpDiccionario);
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


                        if (guardaPrenscripcion.Count != 0)
                        {
                            bool prenscripcion = guardaPrenscripcion.Any(o => dtInicio >= o["inicio"] && dtInicio <= o["final"]);
                            if (prenscripcion)
                            {
                                bool devolucionFondo = guardarDf.Any(o => dtInicio >= o["inicio"] && dtFinal <= o["final"]);
                                bool devolucionCaja = guardaDevoluvcionCaja.Any(o => dtInicio >= o["inicio"] && dtFinal <= o["final"]);

                                simboloAnterior = simbolo;
                                simbolo = (!tipoGrafica) ? (devolucionFondo || devolucionCaja) ? "." : simbolo : "◬";
                                if (dtInicioaux >= dtInicio && dtInicioaux <= dtFinal)
                                {
                                    simboloQuincena1 = (!tipoGrafica) ? (devolucionFondo || devolucionCaja) ? "." : simboloQuincena1 : "◬";
                                }

                                if (dtFinalaux >= dtInicio && dtFinalaux <= dtFinal)
                                {
                                    simboloQuincena2 = (!tipoGrafica) ? (devolucionFondo || devolucionCaja) ? "." : simboloQuincena2 : "◬";
                                }
                            }
                        }

                        if (aportacionXReintegro.Count != 0)
                        {
                            //if (guardarDf.Count > 1)
                            //{
                            //    for (int x = 1; x < guardarDf.Count; x++)
                            //    {
                            //        DateTime dInicio = guardarDf[x]["inicio"];
                            //        DateTime dFinal = guardarDf[x]["final"];
                            //        if (dtInicio >= dInicio && dtFinal <= dFinal)
                            //        {
                            //            goto continuar;
                            //        }
                            //    }
                            //}
                            bool devolucionFondo = aportacionXReintegro.Any(o => dtInicio >= o["inicio"] && dtInicio <= o["final"]);
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

                        if (guardaPeriodoNoLaborado.Count != 0)
                        {
                            bool nolaborado = guardaPeriodoNoLaborado.Any(o => dtInicio >= o["inicio"] && dtFinal <= o["final"]);
                            if (nolaborado)
                            {
                                simboloAnterior = simbolo;
                                simbolo = (!tipoGrafica) ? "." : "▥";
                                if (dtInicioaux >= dtInicio && dtInicioaux <= dtFinal)
                                {
                                    simboloQuincena1 = (!tipoGrafica) ? "." : "▥";
                                }

                                if (dtFinalaux >= dtInicio && dtFinalaux <= dtFinal)
                                {
                                    simboloQuincena2 = (!tipoGrafica) ? "." : "▥";
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

                        if (arreglo[(mes * 2 - 1) - 1] == null)
                            arreglo[(mes * 2 - 1) - 1] = "";

                        if (arreglo[(mes * 2) - 1] == null)
                            arreglo[(mes * 2) - 1] = "";
                        if (mes != 5 && mes != 12)
                        {
                            if (DateTime.Now < new DateTime(año, mes, 15) && DateTime.Now.Month == mes)
                            {
                                if (string.IsNullOrWhiteSpace(arreglo[(mes * 2 - 1) - 1]))
                                {
                                    arreglo[(mes * 2 - 1) - 1] = ".";
                                }
                            }
                        }
                    }

                    arreglo[(mes * 2 - 1) - 1] = arreglo[(mes * 2 - 1) - 1] == null ? quincena1 ? simboloQuincena1 : "." : arreglo[(mes * 2 - 1) - 1];
                    arreglo[(mes * 2) - 1] = arreglo[(mes * 2) - 1] == null ? quincena2 ? simboloQuincena2 : "." : arreglo[(mes * 2) - 1];
                }
                ldGrafica.Add(arreglo);
            }


            System.Diagnostics.Debug.WriteLine(cantidad);

            if (tipoGrafica)
            {
                tipoGrafica = false;
                entro = true;
                auxGraficaOriginal = ldGrafica;
                goto regresar;
            }

            if (entro)
            {
                List<string[]> aux1 = ldGrafica;
                ldGrafica = auxGraficaOriginal;
                auxGraficoAportaciones = aux1;
            }

            int contador = 0;
            int anio = dFechaInicio.Year;
            int totalQuincenas = 0;
            int contadorAportaciones = 0;
            foreach (string[] item in ldGrafica)
            {
                FlowLayoutPanel aux = new FlowLayoutPanel();
                aux.Margin = new Padding(0);
                aux.Size = new Size(885, 30);
                aux.BorderStyle = BorderStyle.None;
                aux.FlowDirection = FlowDirection.LeftToRight;
                contador += 30;
                //Parte de las fechas
                //▧ ▌◆◉
                int cantidadQuincenas = 0;
                if (!entro)
                {
                    cantidadQuincenas = item.Count(o => Convert.ToString(o) == "■" || Convert.ToString(o) == "▌");
                }
                else
                {
                    cantidadQuincenas = auxGraficoAportaciones[contadorAportaciones].Count(o => Convert.ToString(o) == "■" || Convert.ToString(o) == "▌");
                    contadorAportaciones++;
                }
                totalQuincenas += cantidadQuincenas;
            }
            return totalQuincenas;

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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmactuario_Shown(object sender, EventArgs e)
        {
            dateTimePicker1.Select();
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                metodoactuario();
            }
        }
    }
}