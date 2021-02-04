using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA.REPORTES.SALDOS
{
    public partial class frmSaldoPend : Form
    {
        public frmSaldoPend()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = $"CREATE temp table  tabla1 as(SELECT DISTINCT(a1.folio),a2.importe,a2.pagado FROM   datos.descuentos a1 INNER JOIN datos.p_edocta a2 On a1.folio = a2.folio WHERE a1.f_descuento <= '{maskedTextBox1.Text}' AND a1.t_prestamo = 'Q' ORDER BY    folio);      SELECT folio , importe from tabla1; ";
            List<Dictionary<string, object>> result = globales.consulta(query);
            int contador = 0;
            object  [] aux2 = new object[result.Count];
            foreach(var item in result)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                string folio = Convert.ToString(item["folio"]);
                string quer = $"select sum(importe) as acumulado from datos.descuentos where t_prestamo = 'Q' and folio = {folio} ";
                List<Dictionary<string, object>> res = globales.consulta(quer);
                string suma = Convert.ToString(res[0]["acumulado"]);
                string importe = Convert.ToString(item["importe"]);
                object[] tt1 = { folio, importe , suma};
                aux2[contador] = tt1;
                contador++;

            }
            string texto = "hola";

            object[] parametros = { "texto" };
            object[] valor = { texto };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("saldo_pendiente", "pendientes", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;




        }

    }
}
