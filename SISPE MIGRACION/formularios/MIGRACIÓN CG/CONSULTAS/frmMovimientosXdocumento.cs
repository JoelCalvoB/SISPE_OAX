using SISPE_MIGRACION.codigo.repositorios.cg_fondos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.MIGRACIÓN_CG.CONSULTAS
{
    public partial class frmMovimientosXdocumento : Form
    {

        private int row;

        public frmMovimientosXdocumento()
        {
            InitializeComponent();
        }

        private void frmMovimientosXdocumento_Load(object sender, EventArgs e)
        {

        }

        private void txtanio_Leave(object sender, EventArgs e)
        {
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
         if (string.IsNullOrWhiteSpace(txtanio.Text))
            {
                DialogResult diaglo = globales.MessageBoxExclamation("INDIQUE EL AÑO A BUSCAR", "VERIFICAR", globales.menuPrincipal);
                return;
            }
            dtggrid.Rows.Clear();
            string mes = string.Empty;


            mes = tabControl1.SelectedIndex < 10 ? $"0{tabControl1.SelectedIndex}" : $"{tabControl1.SelectedIndex}";

            string query = $" SELECT doctipo, EXTRACT (DAY FROM a1.fecha) AS dia, concat (	a1.poltipo,	'-',	a1.prefijo,	a1.polnumero) AS poliza,docnumero,doccomentario,a1.cuenta,a1.naturaleza,a1.importe,a1.prefijo,a2.concepto,a2.elaboro FROM	cg_fondos.pol a1  inner JOIN cg_fondos.des  a2 on a1.polnumero = a2.polnumero "+
 $"WHERE EXTRACT (MONTH FROM a1.fecha) = '{mes}' AND EXTRACT (YEAR FROM a1.fecha) = '{txtanio.Text}' ORDER BY a1.polnumero,	a1.doctipo;";

            List<Dictionary<string, object>> resultado = globales.consulta(query);
            string importe_debe = string.Empty;

            string importe_haber = string.Empty; 
            string cancelado = string.Empty;
            foreach (var item in resultado)
            {
                string doctipo = Convert.ToString(item["doctipo"]);
                string dia = Convert.ToString(item["dia"]);
                string poliza = Convert.ToString(item["poliza"]);
                string docnumero = Convert.ToString(item["docnumero"]);
                string doccomentario = Convert.ToString(item["doccomentario"]);
                string cuenta = Convert.ToString(item["cuenta"]);
                string naturaleza = string.Empty;
                string concepto = Convert.ToString(item["concepto"]);
                string elaboro = Convert.ToString(item["elaboro"]);
                 naturaleza = Convert.ToString(item["naturaleza"]);
                if (naturaleza == "D")
                {
                    importe_debe = "";
                    importe_haber = "";

                    importe_debe = string.Format("{0:C}", Convert.ToDouble(item["importe"]));
                }
                if (naturaleza == "H")

                {
                    importe_haber = "";
                    importe_debe = "";

                    importe_haber = string.Format("{0:C}", Convert.ToDouble(item["importe"]));
                }
                string prefijo = Convert.ToString(item["prefijo"]);
                if (prefijo == "X")
                    cancelado = "X";

                dtggrid.Rows.Add(doctipo, dia, poliza, docnumero, doccomentario, cuenta, importe_debe, importe_haber, cancelado,  concepto , elaboro);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            globales.MessageBoxInformation("Se creara el reporte de polizas", "Aviso", globales.menuPrincipal);

            if (this.dtggrid.Rows.Count == 0)
            {
                globales.MessageBoxExclamation("No hay datos que visualizar", "Aviso", globales.menuPrincipal);

                return;
            }

            DataGridViewRow item = this.dtggrid.Rows[this.row];
            string descripcionpol = Convert.ToString(item.Cells[9].Value);
            string numeropoliza = Convert.ToString(item.Cells[2].Value).Substring(3,7);
            string prefijo = Convert.ToString(item.Cells[8].Value);
            string polmes = tabControl1.SelectedIndex < 10 ? $"0{tabControl1.SelectedIndex}" : $"{tabControl1.SelectedIndex}";

            string fecha = Convert.ToString(item.Cells[1].Value) +"/"+$"{polmes}"+"/"+txtanio.Text;
            string tipopol = Convert.ToString(item.Cells[2].Value).Substring(0, 1);
            string nombretipo = string.Empty;
            if (tipopol == "D")
            {
                tipopol = "D";
                nombretipo = "DIARIO";
            }
            else if (tipopol == "E")
            {
                
                nombretipo = "EGRESOS";
            }
            else if (tipopol == "I")
            {
                
                nombretipo = "INGRESOS";
            }

            string query = $"select * from cg_fondos.pol where polnumero = '{numeropoliza}' and polmes = '{polmes}' and anio = {txtanio.Text} and poltipo = '{tipopol}' order by naturaleza, cuenta";

            List<pol> obj = new dbaseORM().queryForList<pol>(query);


            object[] arreglo = new object[obj.Count];
            int contador = 0;


            foreach (pol ii in obj)
            {

                string cuentamayor = (ii.cuenta.Replace(".", "").Length > 5) ? ii.cuenta.Replace(".", "").Substring(0, 5) : ii.cuenta.Replace(".", "").Substring(0, ii.cuenta.Length);

                query = $"select  replace(cuenta,'.','') as cuenta,descripcion from cg_fondos.cuentas where replace(cuenta,'.','') like '{cuentamayor}%' order by cuenta limit 1";

                string descripcion = string.Empty;

                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count != 0)
                {
                    descripcion = Convert.ToString(resultado[0]["descripcion"]);
                    cuentamayor = Convert.ToString(resultado[0]["cuenta"]);
                }

                object[] tt1 = { ii.cuenta, ii.doccomentario, ii.doctipo, (ii.naturaleza == "D" ? Convert.ToString(ii.importe) : "0"), (ii.naturaleza == "H" ? Convert.ToString(ii.importe) : "0"), cuentamayor, descripcion };


                arreglo[contador] = tt1;

                contador++;


            }


            string imglogo = globales.getImagen(globales.imagenesSispe.logoreportes);


            object[][] parametros = new object[2][];
            object[] header = { "logo", "tipo", "prefijo", "polnumero", "fecha", "elaboro", "descripcion" };
            object[] body = { imglogo, nombretipo, prefijo, numeropoliza, fecha, "CG", descripcionpol };


            parametros[0] = header;
            parametros[1] = body;


            globales.reportes("frmCG_polizasLibrodiario", "poltabla", arreglo, "", false, parametros);
        }

   

        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (this.dtggrid.Rows.Count == 0) return;
            DataGridViewRow item = this.dtggrid.Rows[this.row];
            string descripcionpol = Convert.ToString(item.Cells[9].Value);
            string numeropoliza = Convert.ToString(item.Cells[2].Value).Substring(3, 7);
            string prefijo = Convert.ToString(item.Cells[8].Value);
            string polmes = tabControl1.SelectedIndex < 10 ? $"0{tabControl1.SelectedIndex}" : $"{tabControl1.SelectedIndex}";

            string fecha = Convert.ToString(item.Cells[1].Value) + "/" + $"{polmes}" + "/" + txtanio.Text;
            string tipopol = Convert.ToString(item.Cells[2].Value).Substring(0, 1);
            string concepto = Convert.ToString(item.Cells[9].Value);


            string nombretipo = string.Empty;
            if (tipopol == "D")
            {
                tipopol = "D";
                nombretipo = "DIARIO";
            }
            else if (tipopol == "E")
            {

                nombretipo = "EGRESOS";
            }
            else if (tipopol == "I")
            {

                nombretipo = "INGRESOS";
            }

            string elaboro = "USUARIO ANTERIOR CG";
           

            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("tipopol", nombretipo);
            obj.Add("prefijo", (item.Cells[8].Value));
            obj.Add("fecha", fecha);
            obj.Add("elaboro", elaboro);
            obj.Add("concepto", concepto);





            frmLibroDiarioDetalle detalle = new frmLibroDiarioDetalle(polmes, Convert.ToInt16(this.txtanio.Text), numeropoliza, tipopol, obj, this.lblabierto.Visible);
            globales.showModal(detalle);
        }
    }
}
