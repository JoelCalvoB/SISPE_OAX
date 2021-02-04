using SISPE_MIGRACION.codigo.repositorios.nominas_catalogos;
using SISPE_MIGRACION.formularios.NOMINAS.PENSION_ALIMENTICIA.OPCIONES;
using SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.REGISTRO_DE_INCIDENCIAS.OPCIONES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PENSION_ALIMENTICIA
{
    public partial class frmActualizacionNomina : Form
    {

        private string titulo = "JUB {0} CON PEA {1}";
        private string jub;
        private string peaStr;
        private int indexJub;
        private int indexPea;

        public frmActualizacionNomina()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmActualizacionNomina_Load(object sender, EventArgs e)
        {


            lbl1.Text = string.Empty;

            if (jubs.Rows.Count != 0) {
                this.jub
                    = Convert.ToString(jubs.Rows[0].Cells[1].Value);

                ponerLabel();
            }

            if (pea.Rows.Count != 0)
            {
                this.peaStr
                    =  Convert.ToString(pea.Rows[0].Cells[1].Value);

                ponerLabel();
            }



            string query = "create temp table t1 as select jpp,numjpp,id from nominas_catalogos.nominew where clave = 217 and jpp <> 'PEA' and tipo_nomina = 'N' "+
                " except "+
                " select jpp,numjpp,id_enlace as id from nominas_catalogos.pension_alimenticia; "+
                " select mma.jpp,mma.num,mma.nombre,nn1.leyen,mma.rfc,t1.id from t1 inner join nominas_catalogos.maestro mma on mma.jpp = t1.jpp and mma.num = t1.numjpp inner join nominas_catalogos.nominew nn1 on nn1.id = t1.id order by mma.jpp,mma.num";

            List<Dictionary<string, object>> resultado = globales.consulta(query);

            foreach (Dictionary<string,object> item in resultado) {
                jubs.Rows.Add(item["jpp"], item["num"], item["nombre"],item["leyen"],item["rfc"],item["id"]);
            }


            //resultado de PEA--------------------


             query = "create temp table t1 as select jpp,numjpp,id from nominas_catalogos.nominew where clave = 34 and jpp = 'PEA' and tipo_nomina = 'N' " +
                " except " +
                " select jpp,numjpp,id_enlacepea as id from nominas_catalogos.pension_alimenticia; " +
                " select mma.jpp,mma.num,mma.nombre,nn1.leyen,mma.rfc,t1.id from t1 inner join nominas_catalogos.maestro mma on mma.jpp = t1.jpp and mma.num = t1.numjpp inner join nominas_catalogos.nominew nn1 on nn1.id = t1.id order by mma.jpp,mma.num";

            resultado = globales.consulta(query);

            foreach (Dictionary<string, object> item in resultado)
            {
                pea.Rows.Add(item["jpp"], item["num"], item["nombre"],item["leyen"], item["rfc"],item["id"]);
            }
        }

        private void ponerLabel()
        {
            lbl1.Text = string.Format(titulo,jub,peaStr);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas vincular "+lbl1.Text+"?","Aviso",globales.menuPrincipal);
            if (dialogo == DialogResult.Yes) {
                string etiqueta = this.lbl1.Text;

                etiqueta = etiqueta.Replace("CON", "|").Replace(" ", "");

                string jub = etiqueta.Split('|')[0];
                string pea = etiqueta.Split('|')[1];

                int numjub = Convert.ToInt32(jub.Substring(3));
                int numpea = Convert.ToInt32(pea.Substring(3));

                string query = $"select * from nominas_catalogos.pension_alimenticia where jpp = 'JUB' and numjpp = {numjub} and numpea = {numpea} and id_enlace = 0 and id_enlacepea = 0";
                List<Dictionary<string, object>> resultado = globales.consulta(query);

                string cuentas_vinculadas = string.Empty;
                double porcentaje1 = 0;
                bool existe = false;
                if (resultado.Count != 0) {
                    cuentas_vinculadas = Convert.ToString(resultado[0]["claves_vinculadas"]);
                    porcentaje1 = Convert.ToDouble(resultado[0]["porcentaje"]);
                    existe = true;
                }

                frmModalPorcentaje porcentaje = new frmModalPorcentaje(cuentas_vinculadas, Convert.ToString(jubs.Rows[indexJub].Cells[4].Value), Convert.ToString(jubs.Rows[indexJub].Cells[2].Value), numjub, porcentaje1, existe);
                porcentaje.enviar = recibiendodatos;
                globales.showModal(porcentaje);

            }
        }

        private void recibiendodatos(string rfc, string nombre, string sueldo, string porcentaje, string total,string clavesVinculadas,bool existe)
        {
            string jpp = "JUB";
            string numero = Convert.ToString(jubs.Rows[indexJub].Cells[1].Value);

          
                dtggrid.Rows.Add(jpp, numero, rfc, nombre, porcentaje, total, 0, jubs.Rows[this.indexJub].Cells[5].Value, pea.Rows[this.indexPea].Cells[5].Value, clavesVinculadas, pea.Rows[indexPea].Cells[1].Value,(existe)?"1":"0");
            
        }

       

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void jubs_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            this.jub = Convert.ToString(jubs.Rows[e.RowIndex].Cells[1].Value);
            ponerLabel();


            this.indexJub = e.RowIndex;
            
        }

        private void pea_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            this.peaStr =  Convert.ToString(pea.Rows[e.RowIndex].Cells[1].Value);

            this.indexPea = e.RowIndex;

            ponerLabel();
        }

        private void btnP_hipote_Click(object sender, EventArgs e)
        {
            DialogResult resultado1 = globales.MessageBoxQuestion("¿Deseas vincular las pensiones alimenticias?", "Aviso", globales.menuPrincipal);
            if (resultado1 == DialogResult.Yes) {
                List<pension_alimenticia> lista = new List<pension_alimenticia>();
                List<pension_alimenticia> listamodificar = new List<pension_alimenticia>();
                foreach (DataGridViewRow item in dtggrid.Rows)
                {
                    pension_alimenticia obj = new pension_alimenticia();
                    obj.jpp = Convert.ToString(item.Cells[0].Value);
                    obj.numjpp = globales.convertInt(Convert.ToString(item.Cells[1].Value));
                    obj.numpea = Convert.ToInt32(item.Cells[10].Value);
                    obj.descuento = globales.convertDouble(Convert.ToString(item.Cells[4].Value).Replace("%", ""));
                    obj.total = globales.convertDouble(Convert.ToString(item.Cells[5].Value));
                    obj.claves_vinculadas = Convert.ToString(item.Cells[9].Value);
                    obj.id_enlace = globales.convertInt(Convert.ToString(item.Cells[7].Value));
                    obj.id_enlacepea = globales.convertInt(Convert.ToString(item.Cells[8].Value));
                    if (Convert.ToString(item.Cells[11].Value) == "1")
                    {
                        listamodificar.Add(obj);

                    }
                    else {
                        lista.Add(obj);
                    }
                }

                if (dtggrid.Rows.Count != 0)
                {
                    new dbaseORM().insertAll<pension_alimenticia>(lista);
                    new dbaseORM().updateAll<pension_alimenticia>(listamodificar);
                }
          
                dtggrid.Rows.Clear();


                globales.MessageBoxSuccess("Pensiones afectadas correctamente","Aviso",globales.menuPrincipal);
            }
        }
    }
}
