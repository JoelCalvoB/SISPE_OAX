using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS
{
    public partial class frmOficioPContinuar : Form
    {
        public frmOficioPContinuar()
        {
            InitializeComponent();
        }

        private void btbuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOficio.Text)) {
                globales.MessageBoxExclamation("Ingresar N° Oficio","Aviso",this);
                return;
            }

            string query = " create temp table prueba1 as select solici.expediente,solici.sec,solici.f_solicitud,solici.finalidad,solici.descri_finalid,solici.f_autorizacion,solici.cap_prest,hipote.nombre_em,hipote.direccion,hipote.ccatdes,notificacion.f_notif  from datos.h_solici solici " +
                " inner join datos.p_hipote hipote on hipote.folio = solici.expediente "+
                " inner join datos.h_snotif notificacion on solici.expediente = notificacion.expediente and solici.sec = notificacion.sec	 "+
                $" where solici.f_autorizacion = '{string.Format("{0:yyyy-MM-dd}",fecha1.Value)}'; "+
                " create temp table prueba2 as select solici.expediente,solici.sec,solici.f_solicitud,solici.finalidad,solici.descri_finalid,solici.f_autorizacion,solici.cap_prest,hipote.nombre_em,hipote.direccion,hipote.ccatdes,notificacion.f_convm as f_notif  from datos.h_solici solici " +
                " inner join datos.p_hipote hipote on hipote.folio = solici.expediente " +
                " inner join datos.h_sconvm notificacion on solici.expediente = notificacion.expediente and solici.sec = notificacion.sec	 " +
                $" where solici.f_autorizacion = '{string.Format("{0:yyyy-MM-dd}", fecha1.Value)}'; "+
                " select * from (select * from prueba1 union select * from prueba2) a order by a.expediente;";

            List<Dictionary<string, object>> resultado = new dbaseORM().query(query);
            if (resultado.Count ==0)
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO EXISTE INFORMACIÓN CON ESTA FECHA", "VERIFICAR", globales.menuPrincipal);
            }
            Cursor.Current = Cursors.WaitCursor;

            List<Dictionary<string, object>> final = new List<Dictionary<string, object>>();
            string[] meses = globales.getMeses();
            int oficio = globales.convertInt(txtOficio.Text);
            foreach (Dictionary<string,object> item in resultado) {
                Dictionary<string, object> diccionario = new Dictionary<string, object>();
                foreach (string llave in item.Keys)
                    diccionario.Add(llave, item[llave]);

                int secuencia = globales.convertInt(Convert.ToString(item["sec"]));
                string prestamo = (secuencia == 0) ? "Inicio" : $"{secuencia}° Ampliación";
                diccionario.Add("prestamo",prestamo);

                DateTime f1 = globales.convertDatetime(Convert.ToString(item["f_notif"]));
                string flnoti = $"{f1.Day} de {meses[f1.Month]} de {f1.Year}";
                diccionario.Add("flnoti",flnoti);

                f1 = globales.convertDatetime(Convert.ToString(item["f_solicitud"]));
                string f_solicitud = $"{f1.Day} de {meses[f1.Month]} de {f1.Year}";
                diccionario.Add("flsolici", f_solicitud);

                f1 = globales.convertDatetime(Convert.ToString(item["f_autorizacion"]));
                string f_autorizacion = $"{f1.Day} de {meses[f1.Month]} de {f1.Year}";
                diccionario.Add("flauto", f_autorizacion);

                query = $"select * from datos.numletra({item["cap_prest"]})";

                List<Dictionary<string, object>> re = globales.consulta(query);
                string letra = string.Empty;
                if (re.Count != 0) {
                    letra = Convert.ToString(re[0]["numletra"]);
                }
                diccionario.Add("caple",letra);
                diccionario.Add("oficio",oficio);

                oficio++;

                final.Add(diccionario);
            }

            query = "SELECT nombre from catalogos.firmas where clave = 'DIR'";
            List<Dictionary<string, object>> rst = globales.consulta(query);
            string nombre_director = string.Empty;
            if (rst.Count != 0)
                nombre_director = Convert.ToString(rst[0]["nombre"]);

            foreach (Dictionary<string,object> item in final) {
                string oficioStr = Convert.ToString(item["oficio"]);
                string flnotiStr = Convert.ToString(item["flnoti"]);
                string nombre_emStr = "C."+Convert.ToString(item["nombre_em"]);
                string direccion = Convert.ToString(item["direccion"]);
                string ccatdes = Convert.ToString(item["ccatdes"]);
                string ampliacion = (globales.convertInt(Convert.ToString(item["sec"])) == 0) ? "inicio" : Convert.ToString(item["sec"]) + "° Ampliación";
                int finalidad = globales.convertInt(Convert.ToString(item["finalidad"]));

                string anexoConstruccingravamen = (ampliacion == "inicio" && (finalidad == 4 || finalidad == 5 || finalidad == 6) )?"pagada en una sola emisión, cabe hacer mención que la diferencia que exista entre el importe del préstamo hipotecario otorgado y el importe total del adeudo, deberá ser cubierto en la misma operación por el interesado, de lo contrario, esta autorización quedará sin afecto, ": "";

                string cuerpo = "Con fundamento en el Artículo 73 de la ley de Pensiones para los Trabajadores del Gobierno del Estado de Oaxaca y Artículo 5 fracciones V y XIII del Reglamento Interno de la Oficina de Pensiones del Estado de Oaxaca, en atención"+
                    $" a su solicitud de Préstamo Hipotecario de {ampliacion} para {item["descri_finalid"]} de fecha {item["flsolici"]}, el H. Consejo Directivo de Pensiones, en sesión ordinaria "+
                    $"de trabajo celebrada el día {item["flauto"]}, acordó concederle su préstamo hipotecario que solicita, por la "+
                    $"cantidad de {string.Format("{0:C}",globales.convertDouble(Convert.ToString(item["cap_prest"])))} ({item["caple"]}), "+ anexoConstruccingravamen +
                    "por lo que se le invita acudir a nuestras oficinas a fin de continuar con el trámite correspondiente.";

                List<string> aux = globales.justificar(cuerpo,105);
                cuerpo = string.Empty;
                aux.ForEach(o => {
                    cuerpo += o + "\n";
                });

                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                object[][] parametros = new object[2][];
                object[] headers = { "oficio", "flnoti","nombre_em","ccatdes","cuerpo","director","direccion","anio" , "logo" };
                object[] body = { oficioStr,flnotiStr,nombre_emStr,ccatdes,cuerpo ,nombre_director,direccion,DateTime.Now.Year.ToString(), logo };


                body[1] = $"{DateTime.Now.Day} de {meses[DateTime.Now.Month]} del {DateTime.Now.Year} ";

                parametros[0] = headers;
                parametros[1] = body;
                Cursor.Current = Cursors.Default;


                globales.reportes("reporteHipotecarioOficioPContinuar", "p_quirog",new object[] { },"",false,parametros);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
            globales.showModal(new frmDocumentos(10));
        }
    }
}
