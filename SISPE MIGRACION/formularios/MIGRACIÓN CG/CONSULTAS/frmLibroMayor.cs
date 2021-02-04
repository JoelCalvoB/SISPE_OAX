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
    public partial class frmLibroMayor : Form
    {
        private string meses;
        private int row;
        private int cell;

        public frmLibroMayor()
        {
            InitializeComponent();
        }

        private void frmLibroMayor_Load(object sender, EventArgs e)
        {
            string query = $"select * from cg_fondos.cierremeses where anio = {DateTime.Now.Year}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count != 0)
            {
                meses = Convert.ToString(resultado[0]["mes"]);
            }
            txtanio.Text = DateTime.Now.Year.ToString();
            tabControl1.SelectedIndex = DateTime.Now.Month;

            rellenarLista();


        }

        private void rellenarLista()
        {
            Cursor = Cursors.WaitCursor;
            dtggrid.Rows.Clear();


            if (!string.IsNullOrWhiteSpace(meses))
            {
                string apertura = meses.Substring(tabControl1.SelectedIndex, 1);
                lblabierto.Visible = apertura == "1";
            }

            string polmes = tabControl1.SelectedIndex < 10 ? $"0{tabControl1.SelectedIndex}" : tabControl1.SelectedIndex.ToString();

            string query = $"select * from cg_fondos.pol where polmes = '{polmes}' and anio = {txtanio.Text} and entidad = '801' and subsistema = 'P' order by polnumero,poltipo,naturaleza";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            double sumadebe = 0;
            double sumahaber = 0;
            foreach (Dictionary<string,object> item in resultado) {
                string fecha = Convert.ToString(item["fecha"]);
                string dia = string.Empty;
                try
                {
                    dia = string.Format("{0:dd}",DateTime.Parse(fecha));
                }
                catch {

                }

                string poliza = $"{item["prefijo"]}-{item["poltipo"]}{item["polnumero"]}";
                string doctipo = Convert.ToString(item["doctipo"]);
                string docnumero = Convert.ToString(item["docnumero"]);
                string docfecha = string.IsNullOrEmpty(Convert.ToString(item["docfecha"]))?"": string.Format("{0:d}", DateTime.Parse(Convert.ToString(item["docfecha"])));
                string docRfc = Convert.ToString(item["docrfc"]);
                string docgrupo = Convert.ToString(item["docgrupo"]);
                string doccomentario = Convert.ToString(item["doccomentario"]);
                string debe = string.Empty;
                string haber = string.Empty;
                if (Convert.ToString(item["naturaleza"]) == "D")
                {
                    debe = globales.convertMoneda(globales.convertDouble(Convert.ToString(item["importe"])));
                    sumadebe += Convert.ToDouble(debe);
                }
                else
                {
                    haber = globales.convertMoneda(globales.convertDouble(Convert.ToString(item["importe"])));
                    sumahaber += globales.convertDouble(haber);
                }


                string x =string.Empty;



                dtggrid.Rows.Add(dia,poliza,doctipo,docnumero,docfecha,docRfc,docgrupo,doccomentario,debe,haber,x,item["polnumero"],item["poltipo"],item["prefijo"]);


            }

            txtDebe.Text = string.Format("{0:c}",sumadebe);
            txtHaber.Text = string.Format("{0:c}", sumahaber);

            this.Cursor = Cursors.Default;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            rellenarLista();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtggrid_Enter(object sender, EventArgs e)
        {
        }

        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.row = e.RowIndex;
            this.cell = e.ColumnIndex;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string numeroPoliza = Convert.ToString(dtggrid.Rows[this.row].Cells[11].Value);
            string tipopoliza = Convert.ToString(dtggrid.Rows[this.row].Cells[12].Value);
            string prefijo = Convert.ToString(dtggrid.Rows[this.row].Cells[13].Value);

            string mes = tabControl1.SelectedIndex < 10 ? $"0{tabControl1.SelectedIndex}" : tabControl1.SelectedIndex.ToString();

            string query = $"select * from cg_fondos.des  where entidad = '801' and subsistema = 'P' AND poltipo = '{tipopoliza}' and polmes = '{mes}' and anio = {txtanio.Text} and prefijo = '{prefijo}'  and polnumero = '{numeroPoliza}'";

            Dictionary<string, object> diccionario = globales.consulta(query)[0];
            switch (Convert.ToString(diccionario["poltipo"]))
            {
                case "D":
                    diccionario.Add("tipopol","DIARIO");
                    break;
                case "E":
                    diccionario.Add("tipopol", "EGRESO");
                    break;
                case "I":
                    diccionario.Add("tipopol", "INGRESO");
                    break;
            }

            frmLibroDiarioDetalle diario = new frmLibroDiarioDetalle(mes,Convert.ToInt32(txtanio.Text),numeroPoliza,tipopoliza,diccionario,lblabierto.Visible);
            globales.showModal(diario);
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
            string numeropoliza = Convert.ToString(item.Cells[11].Value);
            string tipopol = Convert.ToString(item.Cells[12].Value);
            string prefijo = Convert.ToString(item.Cells[13].Value);
            string polmes = tabControl1.SelectedIndex < 10 ? $"0{tabControl1.SelectedIndex}" : $"{tabControl1.SelectedIndex}";

            string descripcionpol = Convert.ToString(item.Cells[2].Value);
            string fecha = Convert.ToString(item.Cells[9].Value);

            string query = $"select * from cg_fondos.des  where entidad = '801' and subsistema = 'P' AND poltipo = '{tipopol}' and polmes = '{polmes}' and anio = {txtanio.Text} and prefijo = '{prefijo}' and polnumero = '{numeropoliza}'";
            Dictionary<string, object> re = globales.consulta(query)[0];
            descripcionpol = Convert.ToString(re["concepto"]);
            fecha = string.Format("{0:d}",re["fecha"]);

            string nombretipo = string.Empty;

            switch (tipopol) {
                case "D":
                    nombretipo = "DIARIO";
                    break;
                case "E":
                    nombretipo = "EGRESO";
                    break;
                case "I":
                    nombretipo = "INGRESO";
                    break;

            }

             query = $"select * from cg_fondos.pol where polnumero = '{numeropoliza}' and polmes = '{polmes}' and anio = {txtanio.Text} and poltipo = '{tipopol}' order by naturaleza, cuenta";

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
    }
}
