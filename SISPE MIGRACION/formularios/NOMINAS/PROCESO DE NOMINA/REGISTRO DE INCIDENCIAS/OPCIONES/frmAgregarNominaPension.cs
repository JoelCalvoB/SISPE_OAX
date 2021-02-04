using SISPE_MIGRACION.codigo.repositorios.nominas_catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.REGISTRO_DE_INCIDENCIAS.OPCIONES
{
    public partial class frmAgregarNominaPension : Form
    {
        private List<nominew> listaEnviar = new List<nominew>();
        private List<pension_alimenticia> listaeliminar;

        private List<pension_alimenticia> listaactualizar
             = new List<pension_alimenticia>();

        private List<pension_alimenticia> listamodificar = new List<pension_alimenticia>();

        public frmAgregarNominaPension(string txtnumemp, List<pension_alimenticia> lista, List<pension_alimenticia> listaeliminar = null,List<pension_alimenticia> lista_actualizar = null)
        {
            InitializeComponent();
            this.txtnumemp = txtnumemp;
            this.lista = lista;
            this.listaeliminar = listaeliminar;
            this.listamodificar = lista_actualizar;
        }

      

        private void frmAgregarNominaPension_Load(object sender, EventArgs e)
        {
            List<nominew> listanominew = new dbaseORM().queryForList<nominew>($"select * from nominas_catalogos.nominew where jpp = 'PEA' and numjpp={txtnumemp} and tipo_nomina = 'N' order by jpp,numjpp,clave,secuen");

            foreach (pension_alimenticia item in this.lista) {
                                    cmb1.Items.Add(item.jpp + item.numjpp.ToString());               
            }

            if (this.listamodificar != null) {
                foreach (pension_alimenticia item in this.listamodificar)
                {
                    bool encontrado = listanominew.Any(o => o.id == item.id_enlacepea);
                    if (encontrado)
                    {
                        nominew actual = listanominew.Where<nominew>(o => o.id == item.id_enlacepea).First();
                        int posicion = dtgridp.Rows.Add("34", actual.secuen, "PENSION ALIM", item.total, item.id_enlace, item.id_enlacepea, item.id);
                        dtgridp.Rows[posicion].DefaultCellStyle.BackColor = Color.FromArgb(249, 199, 29);
                    }
                    else {
                        int posicion = dtgridp.Rows.Add("34", dtgridp.Rows.Count + 1, "PENSION ALIM", item.total, item.id_enlace, item.id_enlacepea, item.id);
                        dtgridp.Rows[posicion].DefaultCellStyle.BackColor = Color.FromArgb(249, 199, 29);

                        //Parte de moficar 
                        nominew percepcion = new nominew();
                        percepcion.jpp = "PEA";
                        percepcion.numjpp = globales.convertInt(this.txtnumemp);
                        percepcion.clave = 34;
                        percepcion.descri = "PENSION ALIM.";
                        percepcion.monto = item.total;
                        percepcion.secuen = dtgridp.Rows.Count + 1;
                        percepcion.tipopago = "N";
                        percepcion.tipo_nomina = "N";
                        percepcion.leyen = $"{item.jpp}{item.numjpp} {item.descuento}%";

                        listaEnviar.Add(percepcion);



                        int cantidad = 0;
                     
                        nominew deduccion = new nominew();
                        deduccion.jpp = item.jpp;
                        deduccion.numjpp = item.numjpp;
                        deduccion.clave = 217;
                        deduccion.descri = "DESC.JUD.X POR.";
                        deduccion.monto = item.total;
                        deduccion.secuen = cantidad + 1;
                        deduccion.tipopago = "N";
                        deduccion.tipo_nomina = "N";
                        deduccion.leyen = $"PEA{this.txtnumemp} {item.descuento}%";

                        listaEnviar.Add(deduccion);
                    }

                }
            }

            if (this.listaeliminar != null) {
                foreach (pension_alimenticia item in listaeliminar) {
                    bool encontrado = listanominew.Any(o => o.id == item.id_enlacepea);
                    if (encontrado)
                    {
                        nominew actual = listanominew.Where<nominew>(o => o.id == item.id_enlacepea).First(); ;
                        int posicion = dtgridp.Rows.Add("34", actual.secuen, "PENSION ALIM", item.total, item.id_enlace, item.id_enlacepea, item.id, 1);
                        dtgridp.Rows[posicion].DefaultCellStyle.BackColor = Color.FromArgb(249, 59, 29);
                    }
                    else {
                        int posicion = dtgridp.Rows.Add("34", "0", "PENSION ALIM", item.total, item.id_enlace, item.id_enlacepea, item.id, 1);
                        dtgridp.Rows[posicion].DefaultCellStyle.BackColor = Color.FromArgb(249, 59, 29);
                    }
                }
            }

            label1.Text = "Percepciones PEA"+this.txtnumemp;
        }

        private void cmb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb1.SelectedIndex == -1) return;
            label2.Text = "Deducciones de "+cmb1.Text;
            string query = $"select *  from nominas_catalogos.nominew where concat(jpp,numjpp) = '{cmb1.Text}' and clave >69 and tipo_nomina = 'N' order by clave,secuen";


            List<Dictionary<string, object>> resultado = globales.consulta(query);


            dtgridd.Rows.Clear();
            resultado.ForEach(o=> dtgridd.Rows.Add(o["clave"],o["secuen"],o["descri"],o["monto"]));



        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmb1.SelectedIndex == -1) return;

            int secuencia = dtgridp.Rows.Count + 1;
           int posicion =  dtgridp.Rows.Add("34", secuencia, "PENSION ALIM.",this.lista[this.cmb1.SelectedIndex].total);
            dtgridp.Rows[posicion].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);

            nominew percepcion = new nominew();
            percepcion.jpp = "PEA";
            percepcion.numjpp = globales.convertInt(this.txtnumemp);
            percepcion.clave = 34;
            percepcion.descri = "PENSION ALIM.";
            percepcion.monto = this.lista[this.cmb1.SelectedIndex].total;
            percepcion.secuen = secuencia;
            percepcion.tipopago = "N";
            percepcion.tipo_nomina = "N";
            percepcion.leyen = $"{cmb1.Text} {this.lista[this.cmb1.SelectedIndex].descuento}%";

            listaEnviar.Add(percepcion);



            int cantidad = 0;
            foreach (DataGridViewRow item in dtgridd.Rows) {
                if (Convert.ToString(item.Cells[0].Value) == "217") {
                    cantidad++;
                }
            }

            posicion =  dtgridd.Rows.Add("217",cantidad + 1, "DESC.JUD.X POR.", this.lista[this.cmb1.SelectedIndex].total);
            dtgridd.Rows[posicion].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);

            nominew deduccion = new nominew();
            deduccion.jpp = cmb1.Text.Substring(0,3);
            deduccion.numjpp = globales.convertInt(cmb1.Text.Substring(3));
            deduccion.clave = 217;
            deduccion.descri = "DESC.JUD.X POR.";
            deduccion.monto = this.lista[this.cmb1.SelectedIndex].total;
            deduccion.secuen = cantidad + 1;
            deduccion.tipopago = "N";
            deduccion.tipo_nomina = "N";
            deduccion.leyen = $"PEA{this.txtnumemp} {this.lista[this.cmb1.SelectedIndex].descuento}%";

            listaEnviar.Add(deduccion);


            lista.RemoveAt(cmb1.SelectedIndex);


            int seleccion = cmb1.SelectedIndex;
            cmb1.Items.RemoveAt(seleccion);


         

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas agregar y afectar nomina con estos cambios?","Aviso",this);
            if (dialogo == DialogResult.Yes) {

                if (this.listaeliminar == null)
                {
                    List<nominew> res = new dbaseORM().insertAll<nominew>(listaEnviar, true);

                    string query = $"select * from nominas_catalogos.pension_alimenticia where numpea = {txtnumemp} and id_enlace = 0";
                    this.lista = new dbaseORM().queryForList<pension_alimenticia>(query);

                    foreach (pension_alimenticia item in this.lista)
                    {
                        nominew nomi = res.Where(o => o.jpp == item.jpp && o.numjpp == item.numjpp && o.clave == 217).First();
                        item.id_enlace = nomi.id;
                        nomi = res.Where(o => o.numjpp == globales.convertInt(txtnumemp) && o.clave == 34 && o.leyen.Contains($"{item.jpp}{item.numjpp}")).First();
                        item.id_enlacepea = nomi.id;
                    }

                    new dbaseORM().updateAll<pension_alimenticia>(this.lista);

                    globales.MessageBoxSuccess("Datos insertados en la nomina", "Aviso", this);
                }
                else {
                    string query = "";
                    foreach (pension_alimenticia item in this.listaeliminar) {
                        query += $"delete from nominas_catalogos.nominew where id = {item.id_enlace};";
                        query += $"delete from nominas_catalogos.nominew where id = {item.id_enlacepea};";
                    }

                    globales.consulta(query);


                    List<nominew> res = new dbaseORM().insertAll<nominew>(this.listaEnviar,true);

                    query = string.Empty;
                    foreach (pension_alimenticia item in listamodificar) {
                        query += $"update nominas_catalogos.nominew set monto = {item.total},leyen = 'PEA{txtnumemp} {item.descuento}%' where id = {item.id_enlace};";
                        query += $"update nominas_catalogos.nominew set monto = {item.total},leyen = '{item.jpp}{item.numjpp} {item.descuento}%' where id = {item.id_enlacepea};";
                    }
                    globales.consulta(query);


                     query = $"select * from nominas_catalogos.pension_alimenticia where numpea = {txtnumemp} and id_enlace = 0";
                    this.lista = new dbaseORM().queryForList<pension_alimenticia>(query);

                    foreach (pension_alimenticia item in this.lista)
                    {
                        nominew nomi = res.Where(o => o.jpp == item.jpp && o.numjpp == item.numjpp && o.clave == 217).First();
                        item.id_enlace = nomi.id;
                        nomi = res.Where(o => o.numjpp == globales.convertInt(txtnumemp) && o.clave == 34 && o.leyen.Contains($"{item.jpp}{item.numjpp}")).First();
                        item.id_enlacepea = nomi.id;
                    }

                    new dbaseORM().updateAll<pension_alimenticia>(this.lista);
                }
                this.Owner.Close();
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
