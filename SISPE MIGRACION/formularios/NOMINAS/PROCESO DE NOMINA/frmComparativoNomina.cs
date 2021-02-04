using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA
{
    public partial class frmComparativoNomina : Form
    {
        public frmComparativoNomina()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas generar el comparativo de nomina?", "Aviso", globales.menuPrincipal);
            if (dialogo == DialogResult.No) return;

            if (string.IsNullOrWhiteSpace(txtAño.Text))
            {
                globales.MessageBoxExclamation("Favor de ingresar el año", "Aviso", globales.menuPrincipal);
            }

            string jpp = string.Empty;
            string año = txtAño.Text.Substring(2);
            int mesAux = (cmbMes.SelectedIndex + 1);
            string mes = (mesAux < 10) ? "0" + mesAux : Convert.ToString(mesAux);

            string archivo = año + mes;
            string archivoAnterior = "";

            if (cmbMes.SelectedIndex == 0)
            {
                int auxaño = Convert.ToInt32(año);
                auxaño = auxaño - 1;

                archivoAnterior = Convert.ToString(auxaño) + "12";
            }
            else
            {
                archivoAnterior = año + ((cmbMes.SelectedIndex < 10) ? "0" + cmbMes.SelectedIndex : Convert.ToString(cmbMes.SelectedIndex));
            }


            string descripcionjUB = string.Empty;

            if (jubilados.Checked)
            {
                jpp = "JUB";
                descripcionjUB = "JUBILADOS";
            }
            else if (pensionado.Checked)
            {
                jpp = "PDO";
                descripcionjUB = "PENSIONADOS";
            }
            else if (pensionistas.Checked)
            {
                jpp = "PTA";
                descripcionjUB = "PENSIONISTAS";
            }
            else if (alimenticia.Checked)
            {
                jpp = "PEA";
                descripcionjUB = "PENSION ALIMENTICIA";
            }


            string query = $"create temp table anterior as select clave,tipo_pago,sum(round(monto::decimal,2)) as nomina_anterior from nominas_catalogos.respaldos_nominas where JPP = '{jpp}' AND tipo_nomina = 'N'  and archivo = '{archivoAnterior}'  group by clave,tipo_pago order by clave;" +
            $" create temp table actual as select clave,tipo_pago,sum(round(monto::decimal,2)) as nomina_anterior from nominas_catalogos.respaldos_nominas where JPP = '{jpp}' AND tipo_nomina = 'N'  and archivo = '{archivo}'  group by clave,tipo_pago order by clave; " +
            $" create temp table altastabla as select jpp,numjpp,clave,sum(round(monto::decimal,2)) as monto,tipo_pago,secuen from nominas_catalogos.respaldos_nominas where archivo = '{archivo}' and tipo_nomina = 'N'  and jpp = '{jpp}' group by jpp,numjpp,clave,tipo_pago,secuen " +
            " except " +
            $" select jpp,numjpp,clave,sum(round(monto::decimal,2)) as monto,tipo_pago,secuen from nominas_catalogos.respaldos_nominas where archivo = '{archivoAnterior}' and tipo_nomina = 'N'  and jpp = '{jpp}' group by jpp,numjpp,clave,tipo_pago,secuen; " +
            " create temp table altas as select clave,tipo_pago,sum(round(monto::decimal,2)) from altastabla group by clave,tipo_pago order by clave; " +
            $" create temp table bajastabla as select jpp,numjpp,clave,sum(round(monto::decimal,2)) as monto,tipo_pago,secuen from nominas_catalogos.respaldos_nominas where archivo = '{archivoAnterior}' and tipo_nomina = 'N'  and jpp = '{jpp}' group by jpp,numjpp,clave,tipo_pago,secuen " +
            " except " +
            $" select jpp,numjpp,clave,sum(round(monto::decimal,2)) as monto,tipo_pago,secuen from nominas_catalogos.respaldos_nominas where archivo = '{archivo}' and tipo_nomina = 'N'  and jpp = '{jpp}' group by jpp,numjpp,clave,tipo_pago,secuen; " +
            " create temp table bajas as select clave,tipo_pago,sum(round(monto::decimal,2)) from bajastabla group by clave,tipo_pago order by clave; " +
            " select COALESCE(anterior.clave,actual.clave) as clave,COALESCE(anterior.tipo_pago,actual.tipo_pago) as tipo_pago, " +
            " COALESCE(anterior.nomina_anterior,0) as nomina_anterior,COALESCE(actual.nomina_anterior,0) as nomina_actual, " +
            " COALESCE(altas.sum,0) as altas,COALESCE(bajas.sum,0) bajas,'' as descripcion from anterior   " +
            " FULL OUTER JOIN actual on anterior.clave = actual.clave and anterior.tipo_pago = actual.tipo_pago " +
            " full outer join altas on (altas.clave = COALESCE(anterior.clave,actual.clave) and altas.tipo_pago = COALESCE(anterior.tipo_pago,actual.tipo_pago))  " +
            " full outer join bajas on (bajas.clave = COALESCE(anterior.clave,actual.clave) and bajas.tipo_pago = COALESCE(anterior.tipo_pago,actual.tipo_pago))  ";

            List<Dictionary<string, object>> resultado = globales.consulta(query);
            query = "select  clave,descri from nominas_catalogos.perded order by clave";
            List<Dictionary<string, object>> perded = globales.consulta(query);
            resultado.ForEach(o =>
            {
                o["descripcion"] = perded.Where(p => Convert.ToString(o["clave"]) == Convert.ToString(p["clave"])).First()["descri"];
                //  o["descri"] += " (RETROACTIVO)"; eeee
            });


            object[] tt2 = new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                string clave = Convert.ToString(item["clave"]);
                string tipo_pago = Convert.ToString(item["tipo_pago"]);
                string nomina_anterior = Convert.ToString(item["nomina_anterior"]);
                string nomina_actual = Convert.ToString(item["nomina_actual"]);
                string altas = Convert.ToString(item["altas"]);
                string bajas = Convert.ToString(item["bajas"]);
                string descripcion = Convert.ToString(item["descripcion"]);
                string tipo = Convert.ToInt32(clave) <= 60 ? "1" : "2";
                string todos = "1";

                object[] tt1 = { clave, tipo_pago, nomina_anterior, nomina_actual, altas, bajas, descripcion, tipo ,todos};

                tt2[contador] = tt1;

                contador++;

            }
            string titulo = string.Empty;
            DateTime actu = DateTime.Now;
            string fecha= string.Format("{0:d}",actu).Replace(" 12:00:00 a. m.", "");

             titulo =$"COMPARATIVO DE NOMINA  PARA EL PAGO A {descripcionjUB} DEL MES DE {cmbMes.Text} DE {txtAño.Text}" ;

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[] parametros = { "fecha","mes", "logo" };
            object[] valor = {  fecha,titulo, logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;
            //Restablece los objetos para evitar el break del reporteador

            globales.reportes("reporte_nominascomparativo", "comparativo",tt2, "", false, enviarParametros);


        }

        private void frmComparativoNomina_Load(object sender, EventArgs e)
        {
            DateTime hoy = DateTime.Now;


            txtAño.Text = hoy.Year.ToString();
            cmbMes.SelectedIndex = hoy.Month - 1;
        }
    }
}
