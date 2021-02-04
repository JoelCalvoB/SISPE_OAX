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

namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones
{
    public partial class frmgrafica : Form
    {

        Dictionary<string, object> resultado;
        List<Dictionary<string, object>> resultado2;
        List<Dictionary<string, object>> resultado5;
        internal string quincenas
        {
            get
            {
                return lblQuincena.Text.Split(':')[0];
            }
        }

        bool tipoGrafica { get; set; }
        private bool entro;
        private List<string[]> auxGraficaOriginal;
        private List<string[]> auxGraficoAportaciones;

        public frmgrafica(Dictionary<string, object> resultado, List<Dictionary<string, object>> resultado2, bool tipoGrafica = false)
        {

            InitializeComponent();
            this.resultado = resultado;
            this.resultado2 = resultado2;

            this.panel5.VerticalScroll.Enabled = false;
            this.panel5.VerticalScroll.Visible = false;
            this.tipoGrafica = tipoGrafica;
            this.ShowInTaskbar = false;

        }

        private void frmgrafica_Load(object sender, EventArgs e)
        {

            string base64Imagen = globales.getImagen(globales.imagenesSispe.logoreportes);
            byte[] imageBytes = Convert.FromBase64String(base64Imagen);


            Image image = Image.FromStream(new MemoryStream(imageBytes));
            pictureBox1.Image = image;

            this.lblnombre.Text = Convert.ToString(this.resultado["nombre_em"]);
            this.lblrfc.Text = Convert.ToString(this.resultado["rfc"]);
            if (!this.tipoGrafica)
            {
                f6.Visible = true;
                ctrF6.Visible = false;
            }
            else
            {
                f6.Visible = false;
                ctrF6.Visible = true;
            }
            calcula();

           
                
        }

        private void calcula()
        {

            //APORTACIONES INDEBIDO ◍

            if (resultado2.Count == 0) return;

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

            int contador = 36;
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
                p1.Controls.Add(aux);
                contador += 30;
                p1.Size = new Size(883, contador);
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
                aux.Controls.Add(rellenarControl(anio.ToString(), 89, 28, 12));
                aux.Controls.Add(rellenarControl($"{item[0]}|{item[1]}", false));
                aux.Controls.Add(rellenarControl($"{item[2]}|{item[3]}", true));
                aux.Controls.Add(rellenarControl($"{item[4]}|{item[5]}", false));
                aux.Controls.Add(rellenarControl($"{item[6]}|{item[7]}", true));
                aux.Controls.Add(rellenarControl($"{item[8]}|{item[9]}", false));
                aux.Controls.Add(rellenarControl($"{item[10]}|{item[11]}", true));
                aux.Controls.Add(rellenarControl($"{item[12]}|{item[13]}", false));
                aux.Controls.Add(rellenarControl($"{item[14]}|{item[15]}", true));
                aux.Controls.Add(rellenarControl($"{item[16]}|{item[17]}", false));
                aux.Controls.Add(rellenarControl($"{item[18]}|{item[19]}", true));
                aux.Controls.Add(rellenarControl($"{item[20]}|{item[21]}", false));
                aux.Controls.Add(rellenarControl($"{item[22]}|{item[23]}", true));
                aux.Controls.Add(rellenarControl(cantidadQuincenas.ToString(), 197, 28, 12));

                anio++;
            }


            //Colocando avisos.............
            int tamañito = 30;
            if (fechasAportacionesRetroactivas.Count != 0)
            {
                Label etiqueta = rellenarControl("Fechas de aportaciones retroactivas:", 274, 23, 12, false, true);
                panelAvisos.Size = new Size(287, tamañito);
                panelAvisos.Controls.Add(etiqueta);
                foreach (string item in fechasAportacionesRetroactivas)
                {
                    bool existe = aportacionNormal.Any(o => DateTime.Parse(Convert.ToString(o["inicio"])) == DateTime.Parse((item)));
                    if (!existe)
                    {
                        etiqueta = rellenarControl(item, 274, 23, 12, false, true);
                        tamañito += 30;
                        panelAvisos.Size = new Size(287, tamañito);
                        panelAvisos.Controls.Add(etiqueta);
                    }
                }
            }

            if (fechaRepetidos.Count != 0)
            {
                tamañito += 30;
                Label etiqueta = rellenarControl("Existen aportaciones duplicadas:", 274, 23, 12, false, true);
                panelAvisos.Size = new Size(287, tamañito);
                panelAvisos.Controls.Add(etiqueta);
                foreach (string item in fechaRepetidos)
                {
                    etiqueta = rellenarControl(item, 274, 23, 12, false, true);
                    tamañito += 30;
                    panelAvisos.Size = new Size(287, tamañito);
                    panelAvisos.Controls.Add(etiqueta);
                }
            }

            if (fondoprescripcion.Count != 0)
            {
                Label etiqueta = rellenarControl("Fondo aplicado por prescripción:", 274, 23, 12, false, true);
            }



            //Calculo de las quincenas
            int Qtotales = Convert.ToInt32(totalQuincenas);
            int AA = Convert.ToInt32((Qtotales) / 24);
            int QAux = Qtotales - (AA * 24);
            int AM = Convert.ToInt32((QAux / 2));
            int AQ = QAux - (AM * 2);

            lblAntigQ.Text = string.Format("{0}A. {1}M. {2}Q.", AA, AM, AQ);
            lblQuincena.Text = totalQuincenas.ToString() + " Quincenas";
            double años = (Convert.ToDouble(totalQuincenas) / 24);
            años = Math.Round(años, 3);
            lblAños.Text = años.ToString() + " Años";

            double devolucionFinal = devolucion + prescripcion;
            double saldo = aportacion - devolucionFinal;

            lbl1.Text = string.Format("{0:C}", aportacion);
            lbl2.Text = string.Format("{0:C}", devolucion);
            lbl3.Text = string.Format("{0:C}", prescripcion);
            lbl4.Text = string.Format("{0:C}", aportacion);
            lbl5.Text = string.Format("{0:C}", devolucionFinal);
            lbl6.Text = string.Format("{0:C}", saldo);
        }

        public Panel rellenarControl(string texto, bool color)
        {

            FlowLayoutPanel aux = new FlowLayoutPanel();
            string txt1 = texto.Split('|')[0];
            string txt2 = texto.Split('|')[1];


            aux.BackColor = Color.White;
            aux.Margin = new Padding(0);
            aux.Width = 45;

            if (color) aux.BackColor = Color.SkyBlue;

            Label etiqueta = new Label();
            etiqueta.Text = txt1;
            etiqueta.Size = new Size(21, 20);
            etiqueta.Margin = new Padding(0, 4, 0, 0);
            etiqueta.Padding = new Padding(0);


            if (txt1 == "▌")
            {
                etiqueta.BackColor = Color.Black;
                etiqueta.Size = new Size(15, 20);
                etiqueta.Margin = new Padding(5, 4, 0, 0);
            }

            if (txt1 == "_")
            {
                etiqueta.Font = new Font("Adobe Kaiti Std", 12, FontStyle.Bold);
            }
            else
            {
                etiqueta.Font = new Font("Adobe Kaiti Std", 16, FontStyle.Bold);
            }


            Label etiqueta2 = new Label();
            etiqueta2.Text = txt2;
            etiqueta2.Size = new Size(21, 20);
            etiqueta2.Margin = new Padding(0, 4, 0, 0);
            etiqueta2.Padding = new Padding(0);

            if (txt2 == "▌")
            {
                etiqueta2.BackColor = Color.Black;
                etiqueta2.Size = new Size(15, 20);
                etiqueta2.Margin = new Padding(5, 4, 0, 0);
            }
            if (txt2 == "_")
            {
                etiqueta2.Font = new Font("Adobe Kaiti Std", 12, FontStyle.Bold);
            }
            else
            {
                etiqueta2.Font = new Font("Adobe Kaiti Std", 16, FontStyle.Bold);
            }


            aux.Controls.Add(etiqueta);
            aux.Controls.Add(etiqueta2);
            return aux;
        }

        public Label rellenarControl(string texto, int x, int y, int letra, bool fondo = false, bool color = false)
        {
            Label control = new Label();
            control.Text = texto;
            control.AutoSize = false;
            control.Size = new Size(x, y);
            control.Font = new Font("Adobe Kaiti Std", letra, FontStyle.Regular);
            control.Margin = new Padding(-5, 0, 0, 0);
            control.Padding = new Padding(-5, 0, 0, 0);
            control.BackColor = Color.White;
            control.TextAlign = ContentAlignment.BottomCenter;

            if (color) control.ForeColor = Color.Red;
            if (fondo) control.BackColor = Color.SkyBlue;
            return control;
        }
        private void detecta()
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void t1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void realizarOperacion(List<Dictionary<string, DateTime>> lista, int mes, int año, ref bool quincena1, ref bool quincena2, ref string simbolo, string stringImagen, ref string simboloQuincena1, ref string simboloQuincena2, bool reintegro = false, List<Dictionary<string, DateTime>> devoluciones = null, List<Dictionary<string, DateTime>> devCaja = null)
        {

            //El if de reintegro por algo lo puse jaja no lo borren att: Santiago
            if (reintegro)
            {
                //if (devoluciones.Count > 1)
                //{
                //    for (int x = 1; x < devoluciones.Count; x++)
                //    {
                //        DateTime dInicio = devoluciones[x]["inicio"];
                //        DateTime dFinal = devoluciones[x]["final"];
                //        foreach (Dictionary<string, DateTime> item in lista)
                //        {
                //            DateTime inicio = item["inicio"];
                //            DateTime final = item["final"];
                //            if (dInicio >= inicio && final <= dFinal)
                //            {
                //                return;
                //            }
                //        }
                //    }
                //}
            }

            foreach (Dictionary<string, DateTime> item in lista)
            {
                DateTime inicio = item["inicio"];
                DateTime final = item["final"];

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
                    } else if (stringImagen == "X") {
                        simbolo = (!tipoGrafica) ? "." : stringImagen;
                        simbolo = ("X" == stringImagen) ? "X" : simbolo;
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
                    else if (stringImagen == "X")
                    {
                        simbolo = (!tipoGrafica) ? "." : stringImagen;
                        simbolo = ("X" == stringImagen) ? "X" : simbolo;
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

        private void frmgrafica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.Close();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
