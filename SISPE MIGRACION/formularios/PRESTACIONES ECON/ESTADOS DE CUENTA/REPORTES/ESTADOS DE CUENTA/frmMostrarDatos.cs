using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA.REPORTES.ESTADOS_DE_CUENTA
{
    public partial class frmMostrarDatos : Form
    {
        private List<Dictionary<string, object>> lista;
        private string fecha;
        private string tipo;
        public frmMostrarDatos(List<Dictionary<string,object>> lista,string fecha,string tipo)
        {
            InitializeComponent();
            this.lista = lista;
            this.fecha = fecha;
            this.tipo = tipo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int indice = datos.CurrentRow.Index;
            Dictionary<string, object> o = lista[indice];

            

            string query = $"select * from datos.descuentos where folio = {Convert.ToString(o["folio"])} and t_prestamo = '{this.tipo}' and f_descuento <= '{this.fecha}' order by f_descuento ";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            object[] objReporte = new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                
                string tipo_rel = Convert.ToString(item["tipo_rel"]);
                double folio = Convert.ToDouble(o["folio"]);
                string rfc = Convert.ToString(o["rfc"]);
                string nombre = Convert.ToString(o["nombre_em"]);
                string proyecto = Convert.ToString(item["proyecto"]);
                double importe = (string.IsNullOrWhiteSpace(Convert.ToString(o["importe"]))) ? 0 : Convert.ToDouble(o["importe"]);
                string ubic_pagare = Convert.ToString(o["ubic_pagare"]);
                string numDesc = Convert.ToString(item["numdesc"]);
                string totDesc = Convert.ToString(item["totdesc"]);
                string serie = numDesc + "/" + totDesc;
                double pagado = string.IsNullOrWhiteSpace(Convert.ToString(o["pagado"])) ? 0 : Convert.ToDouble(o["pagado"]);
                string fecha_primdescuento = Convert.ToString(item["f_descuento"]).Replace(" 12:00:00 a. m.", "");
                //string fecha_primdescuento = Convert.ToString(o["f_primdesc"]).Replace(" 12:00:00 a. m.", "");
                double importeUnitario = string.IsNullOrWhiteSpace(Convert.ToString(item["importe"])) ? 0 : Convert.ToDouble(item["importe"]);
                double saldo = string.IsNullOrWhiteSpace(Convert.ToString(o["saldo"])) ? 0 : Convert.ToDouble(o["saldo"]);
                string fecha = Convert.ToString(o["ultimop"]).Replace(" 12:00:00 a. m.", "");
                string scta = Convert.ToString(item["cuenta"]);
                string descripcionCuenta = Convert.ToString(o["descripcion_cta"]);
                string letrasSaldo = string.Format("({0})", globales.convertirNumerosLetras(Convert.ToString(saldo), true));

                object[] obj2 = { contador+1,tipo_rel,folio,rfc,nombre,proyecto,importe,ubic_pagare,numDesc,
                                  pagado,fecha_primdescuento,saldo,fecha,scta,descripcionCuenta,serie,importeUnitario,letrasSaldo};

                objReporte[contador] = obj2;
                contador++;

            }

            if (resultado.Count == 0) {
                objReporte = new object[1];
                int secuencia = Convert.ToInt32(o["secuencia"]);
                string tipo_rel = Convert.ToString(o["tipo_rel"]);
                double folio = Convert.ToDouble(o["folio"]);
                string rfc = "";
                string nombre = "";
                string proyecto = "";
                double importe = (string.IsNullOrWhiteSpace(Convert.ToString(o["importe"]))) ? 0 : Convert.ToDouble(o["importe"]);
                string ubic_pagare = Convert.ToString(o["ubic_pagare"]);
                string numDesc = Convert.ToString(o["numdesc"]);
                string totDesc = Convert.ToString(o["totdesc"]);
                string serie = numDesc + "/" + totDesc;
                double pagado = string.IsNullOrWhiteSpace(Convert.ToString(o["pagado"])) ? 0 : Convert.ToDouble(o["pagado"]);
                string fecha_primdescuento = Convert.ToString(o["f_primdesc"]).Replace(" 12:00:00 a. m.", "");
                double importeUnitario = 0;
                double saldo = string.IsNullOrWhiteSpace(Convert.ToString(o["saldo"])) ? 0 : Convert.ToDouble(o["saldo"]);
                string fecha = Convert.ToString(o["fecha"]).Replace(" 12:00:00 a. m.", "");
                string scta = "";
                string descripcionCuenta = Convert.ToString(o["cta_descripcion"]);
                string letrasSaldo = globales.convertirNumerosLetras(Convert.ToString(saldo), true);
                object[] obj2 = { secuencia,tipo_rel,folio,rfc,nombre,proyecto,importe,ubic_pagare,numDesc,
                                  pagado,fecha_primdescuento,saldo,fecha,scta,descripcionCuenta,serie,importeUnitario,letrasSaldo};

                objReporte[0] = obj2;

            }

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

            object[][] parametros = new object[2][];
            object[] header = { "logo" };
            object[] body = { logo };
            parametros[0] = header;
            parametros[1] = body;

            globales.reportes("reporteEstadosDecuenta", "estadosCuenta", objReporte,"",false, parametros);
        }

        private void frmMostrarDatos_Load(object sender, EventArgs e)
        {
            lista.ForEach(o => datos.Rows.Add(o["folio"], o["rfc"], o["nombre_em"], o["proyecto"], string.IsNullOrWhiteSpace(Convert.ToString(o["importe"]))?string.Format("{0:C}",0):string.Format("{0:C}", o["importe"]),
                string.IsNullOrWhiteSpace(Convert.ToString(o["pagado"])) ? string.Format("{0:C}", 0) : string.Format("{0:C}", o["pagado"]),
                string.IsNullOrWhiteSpace(Convert.ToString(o["saldo"])) ? string.Format("{0:C}", 0) : string.Format("{0:C}", o["saldo"])));
        }

        private void datos_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void datos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(null,null);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
