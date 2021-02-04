using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.REGISTRO_DE_INCIDENCIAS
{
    public partial class frmValidacionDescuentosFondo : Form
    {
        public frmValidacionDescuentosFondo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT a1.jpp,a1.num,a1.nombre,a1.nosuspen FROM nominas_catalogos.maestro a1 LEFT JOIN nominas_catalogos.nominew a2 ON a1.jpp=a2.jpp and a1.num=a2.numjpp  where a1.nosuspen <> '' and a1.jpp<>'PEA' and a2.clave=202 and tipo_nomina='N' order by a1.jpp";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            string num = string.Empty;
            string nombre = string.Empty;
            string nosuspen = string.Empty;

            if (resultado.Count <= 0)
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO SE ENCUENTRA INFORMACIÓN EN ESTE MOMENTO", "AVISO", globales.menuPrincipal);
                return;
            }
            foreach (var item in resultado)
            {
                try
                {
                    num = Convert.ToString(item["jpp"] + Convert.ToString(item["num"]));
                    nombre = Convert.ToString(item["nombre"]);
                    nosuspen = Convert.ToString(item["nosuspen"]);
                }
                catch
                {

                }
                object[] tt1 = { num, nombre, nosuspen };
                aux2[contador] = tt1;
                contador++;
            }
            DateTime f1 = DateTime.Now;
            string fch = string.Format("{0:d}", f1);
            string titulo = "VALIDACIÓN DE SUSPENSIÓN DE DESCUENTOS AL FONDO DE PENSIONES";

            string fec = $"select* from datos.fechaletra ('{fch}')";
            List<Dictionary<string, object>> ff = globales.consulta(fec);
            string fechacatual = Convert.ToString(ff[0]["fechaletra"]);
            object[] parametros = { "fecha", "titulo" };
            object[] valor = { fechacatual, titulo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reportValidacionFondo", "descFondo", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;

            DialogResult p = globales.MessageBoxQuestion("¿Deseas eliminar las claves 202, que fueron suspendidas?", "Aviso", globales.menuPrincipal);
            if (p == DialogResult.No) return;


            foreach (var item in resultado)
            {
                query = $"delete from nominas_catalogos.nominew where clave=202 and jpp='{Convert.ToString(item["jpp"])}' and numjpp={Convert.ToString(item["num"])} ";
                globales.consulta(query);

            }

            DialogResult dialogoacept = globales.MessageBoxSuccess("PROCESO TERMINADO CORRECTAMENTE", "FINALIZADO", globales.menuPrincipal);


            ValidaSeguros();


           
            
        }



        public void ValidaSeguros()
        {
            string query = "SELECT a1.jpp,a1.num,a1.nombre,a1.num_susp_seguro FROM nominas_catalogos.maestro a1 LEFT JOIN nominas_catalogos.nominew a2 ON a1.jpp=a2.jpp and a1.num=a2.numjpp where  a1.num_susp_seguro <> '' and a1.jpp<>'PEA' and a2.clave=212 and tipo_nomina='N' ";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <= 0)
            {
                DialogResult dialo = globales.MessageBoxExclamation("NO SE ENCUENTRA INFORMACIÓN PARA MOSTRAR", "AVISO", globales.menuPrincipal);
                return;
            }

            string num = string.Empty;
            string nombre = string.Empty;
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            string nosuspen = string.Empty;
            foreach (var item in resultado)
            {
                try
                {
                    num = Convert.ToString(item["jpp"] + Convert.ToString(item["num"]));
                    nombre = Convert.ToString(item["nombre"]);
                    nosuspen = Convert.ToString(item["num_susp_seguro"]);
                }
                catch
                {

                }
                object[] tt1 = { num, nombre, nosuspen };
                aux2[contador] = tt1;
                contador++;
            }
            DateTime f1 = DateTime.Now;
            string fch = string.Format("{0:d}", f1);

            string titulo = "VALIDACIÓN DE SUSPENSIÓN DE DESCUENTOS DE SEGURO DE VIDA";

            string fec = $"select* from datos.fechaletra ('{fch}')";
            List<Dictionary<string, object>> ff = globales.consulta(fec);
            string fechacatual = Convert.ToString(ff[0]["fechaletra"]);
            object[] parametros = { "fecha", "titulo" };
            object[] valor = { fechacatual, titulo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reportValidacionFondo", "descFondo", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;

            DialogResult p = globales.MessageBoxQuestion("¿Deseas eliminar las claves 212, que fueron suspendidas?", "Aviso", globales.menuPrincipal);
            if (p == DialogResult.No) return;


            foreach (var item in resultado)
            {
                query = $"delete from nominas_catalogos.nominew where clave=212 and jpp='{Convert.ToString(item["jpp"])}' and numjpp={Convert.ToString(item["num"])} ";
                globales.consulta(query);

            }

            DialogResult dialogoacept = globales.MessageBoxSuccess("PROCESO TERMINADO CORRECTAMENTE", "FINALIZADO", globales.menuPrincipal);

        }



    }
}
