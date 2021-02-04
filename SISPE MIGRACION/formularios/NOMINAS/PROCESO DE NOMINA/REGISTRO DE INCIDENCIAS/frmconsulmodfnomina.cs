using SISPE_MIGRACION.codigo.repositorios.nominas_catalogos;
using SISPE_MIGRACION.formularios.NOMINAS.CATALOGOS.modal;
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
    public partial class frmconsulmodfnomina : Form
    {
        private bool teclaEnter;
        int c;
        int columna;
        public frmconsulmodfnomina()
        {
            InitializeComponent();
        }
     

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataNom_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            btnguardar.Enabled = true;
            int perded = 0;
            string clave = "";
            c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = dataNom.Rows[c];
            string valor = Convert.ToString(dataNom.Rows[c].Cells[0].Value);
            if (!string.IsNullOrWhiteSpace(valor))
            {
                string clave1 = Convert.ToString(row.Cells[0].Value);
                if (string.IsNullOrWhiteSpace(clave1)) return;
                perded = Convert.ToInt32(clave1);

                string query = $"select *  from nominas_catalogos.perded  where clave={perded} limit 1";
                List<Dictionary<string, object>> res = globales.consulta(query);
                if (res.Count == 0) return;

                string descri = Convert.ToString(res[0]["descri"]);
                dataNom.Rows[c].Cells[1].Value = descri;
                string booleano = Convert.ToString(res[0]["prestamo"]);
           
            }
        }
        private void dataNom_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int perded = 0;
            string clave = "";
            c = e.RowIndex;
            if (c == -1) return;

            //inicio,FINAL,new_tipo,entrada,salida,cuenta,movimiento

            DataGridViewRow row = dataNom.Rows[c];
            int column = e.ColumnIndex;
            this.columna = column;
            if (column == 0)
            {
                string clave2 = Convert.ToString(row.Cells[0].Value);
                if (string.IsNullOrWhiteSpace(clave2)) return;
                perded = Convert.ToInt32(clave2);

                string query = $"select *  from nominas_catalogos.perded  where clave={perded} limit 1";
                List<Dictionary<string, object>> res = globales.consulta(query);
                if (res.Count == 0) return;
                string descri = Convert.ToString(res[0]["descri"]);
                dataNom.Rows[c].Cells[1].Value = descri;
                string valida = $"select max(secuen) as ultimo from nominas_catalogos.nominew where clave={perded} and jpp='{comboBoxnom.Text}' and numjpp={txtnumemp.Text}";
                List<Dictionary<string, object>> rs = globales.consulta(valida);

                if (Convert.ToString(rs[0]["ultimo"])=="0" || Convert.ToString(rs[0]["ultimo"])=="1")
                {
                    int secuencia = Convert.ToInt32(rs[0]["ultimo"]) + 1;
                    dataNom.Rows[c].Cells[8].Value = Convert.ToString(secuencia);

                }

           
            }

        
            if (this.teclaEnter)
            {
                var x = this.c + 1;
                var y = dataNom.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                    this.teclaEnter = false;
            }



            // actualiza
            string claveD = Convert.ToString(dataNom.Rows[c].Cells[0].Value);
            if (claveD == "0") return;
            string descriD = Convert.ToString(dataNom.Rows[c].Cells[1].Value);
            string montopos = Convert.ToString(dataNom.Rows[c].Cells[2].Value);

            string npago = Convert.ToString(dataNom.Rows[c].Cells[3].Value);
            string tpago = Convert.ToString(dataNom.Rows[c].Cells[4].Value);

            string idposicionado = Convert.ToString(dataNom.Rows[c].Cells[5].Value);
            string tipopagoD = Convert.ToString(dataNom.Rows[c].Cells[6].Value);
            string leyenD = Convert.ToString(dataNom.Rows[c].Cells[7].Value);
            string secuen = Convert.ToString(dataNom.Rows[c].Cells[8].Value);
            string folio = Convert.ToString(dataNom.Rows[c].Cells[9].Value);
            if (string.IsNullOrWhiteSpace (folio))
            {
                folio = "0";
            }



            if (string.IsNullOrWhiteSpace(montopos) && string.IsNullOrWhiteSpace(idposicionado)) return;

            string query1 = $"update nominas_catalogos.nominew set clave='{claveD}',descri='{descriD}', monto={double.Parse(montopos, System.Globalization.NumberStyles.Currency)},tipopago='{tipopagoD}',leyen='{leyenD}',pagon={globales.convertInt(npago)},pagot={globales.convertInt(tpago)},secuen={secuen},q_captura={Convert.ToString(globales.id_usuario)} , folio={folio} where id={idposicionado};";
            if (string.IsNullOrWhiteSpace(idposicionado))
            {


                return;

            }
            globales.consulta(query1);
            //   dataNom.Rows.Clear();
            //  rellenaGrid();

            if (e.ColumnIndex == 2 && e.RowIndex == c)
            {
                string moneda = globales.convertMoneda(globales.convertDouble(montopos));
                dataNom.Rows[c].Cells[column].Value = moneda;
            }
        }    //


        private void viendoEdicion(object sender, PreviewKeyDownEventArgs e)
        {
            this.teclaEnter = e.KeyCode == Keys.Enter;
        }


        private void frmconsulmodfnomina_KeyPress(object sender, KeyPressEventArgs e)
        {
            char S;

            S = Char.ToUpper(e.KeyChar);

            e.KeyChar = S;

            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void dataNom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    int iColumn = dataNom.CurrentCell.ColumnIndex;
                    int iRow = dataNom.CurrentCell.RowIndex;
                    if (iColumn == dataNom.ColumnCount - 1)
                    {
                        if (dataNom.RowCount > (iRow + 1))
                        {
                            dataNom.CurrentCell = dataNom[1, iRow + 1];
                        }
                        else
                        {
                            //focus next control
                        }
                    }
                    else
                        dataNom.CurrentCell = dataNom[iColumn + 1, iRow];
                }
                catch
                {

                }
            }

            if (e.KeyCode==Keys.Delete)
            {
                DialogResult dia = globales.MessageBoxQuestion("¿DESEA ELIMINAR LA CLAVE?", "ELIMINAR", globales.menuPrincipal);
                if (dia==DialogResult.No)
                {
                    return;
                }

                string idposicionado = Convert.ToString(dataNom.Rows[c].Cells[5].Value);
                //      string query = $"delete from  nominas_catalogos.nominew where id={idposicionado}";
                string query = $"update nominas_catalogos.nominew  set jpp='E' , leyen ='{globales.usuario}' where id ={idposicionado}";
                globales.consulta(query);
                dataNom.Rows.Clear();
                rellenaGrid();
                DialogResult dialogo = globales.MessageBoxExclamation("ELIMINASTE UN REGISTRO DE LA NÓMINA", "AVISO", globales.menuPrincipal);
            }

            if (e.KeyCode == Keys.Insert)
            {
                 dataNom.Rows.Insert(0);
                string clave = Convert.ToString(dataNom.Rows[c].Cells[0].Value);
                string descri = Convert.ToString(dataNom.Rows[c].Cells[1].Value);
                string montopos = Convert.ToString(dataNom.Rows[c].Cells[2].Value);
                string tipopagoD = Convert.ToString(dataNom.Rows[c].Cells[6].Value);
                string leyenD = Convert.ToString(dataNom.Rows[c].Cells[7].Value);
                string folio = Convert.ToString(dataNom.Rows[c].Cells[9].Value);



                string tipo_nomina = string.Empty;
                if (radioButton1.Checked) tipo_nomina = "AG";
                if (radioButton2.Checked) tipo_nomina = "CA";
                if (radioButton3.Checked) tipo_nomina = "DM";
                if (radioButton4.Checked) tipo_nomina = "UT";
                if (radioButton5.Checked) tipo_nomina = "CAN2";
                if (radioButton7.Checked) tipo_nomina = "PF";


                string inserta = string.Empty;

                if  (checkBox1.Checked==true)
                {
                    inserta = $"insert into nominas_catalogos.nominew (jpp,numjpp,clave,descri,monto,pagon,pagot,tipopago,leyen,secuen,tipo_nomina,q_captura,f_captura) values('{comboBoxnom.Text}',{txtnumemp.Text},0,'nuevo',0.00,0,0,'N','',1,'{tipo_nomina}', {globales.id_usuario},current_date);";

                }
                else
                {
                    inserta = $"insert into nominas_catalogos.nominew (jpp,numjpp,clave,descri,monto,pagon,pagot,tipopago,leyen,secuen,tipo_nomina,folio,q_captura,f_captura) values('{comboBoxnom.Text}',{txtnumemp.Text},0,'nuevo',0.00,0,0,'N','',1,'N',0,{globales.id_usuario},current_date);";

                }
                globales.consulta(inserta);
                dataNom.Rows.Clear();
                rellenaGrid();
                dataNom.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);

            }
        }

        private void dataNom_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion);

        }

        private void frmconsulmodfnomina_Shown(object sender, EventArgs e)
        {
            bloquea();

        }

        private void btnFolio_Click(object sender, EventArgs e)
        {
            limpiaForm();
            bloquea();
            frmModalJubilados jubi = new frmModalJubilados();
            jubi.enviar = rellenar;
            globales.showModal(jubi);
        }

        private void rellenar(Dictionary<string, object> diccionario)
        {
            dataNom.Enabled = true;
            maestro maestro = new dbaseORM().getObject<maestro>(diccionario);
            comboBoxnom.Text = maestro.jpp;
            txtnom.Text = maestro.nombre;
            txtrfc.Text = maestro.rfc;
            txtnumemp.Text = Convert.ToString( maestro.num);
            rellenaGrid();

        }


        private void rellenaGrid()
        {
            dataNom.Rows.Clear();
            string query = string.Empty;

            string tipo_nomina = string.Empty;
            if (radioButton1.Checked)
            {
                tipo_nomina = "AG";
               if (comboBoxnom.Text=="PDO")
                {
                    tipo_nomina = "AG";

                }
                if (comboBoxnom.Text == "PTA")
                {
                    tipo_nomina = "AG";

                }
                if (comboBoxnom.Text == "PEA")
                {
                    tipo_nomina = "AG";

                }

            }
            if (radioButton2.Checked) tipo_nomina = "CA";
            if (radioButton3.Checked) tipo_nomina = "DM";
            if (radioButton4.Checked) tipo_nomina = "UT";
            if (radioButton5.Checked) tipo_nomina = "CAN2";
            if (radioButton7.Checked) tipo_nomina = "PF";


            if (checkBox1.Checked==true)
            {
                query = $"SELECT	a2.rfc,	a2.nombre,	a1.clave,	a1.descri,	a1.monto,	a1.pagon,	a1.pagot ,a1.id,a1.tipopago, a1.leyen,a1.secuen,a1.folio FROM	nominas_catalogos.nominew a1 LEFT JOIN nominas_catalogos.maestro a2 ON a1.jpp = a2.jpp AND a1.numjpp = a2.num where a1.jpp='{comboBoxnom.Text}' and a1.numjpp={txtnumemp.Text} and a1.tipo_nomina='{tipo_nomina}' ORDER BY a1.clave";

            }
            else
            {
                query = $"SELECT	a2.rfc,	a2.nombre,	a1.clave,	a1.descri,	a1.monto,	a1.pagon,	a1.pagot ,a1.id,a1.tipopago, a1.leyen,a1.secuen ,a1.folio FROM	nominas_catalogos.nominew a1 LEFT JOIN nominas_catalogos.maestro a2 ON a1.jpp = a2.jpp AND a1.numjpp = a2.num where a1.jpp='{comboBoxnom.Text}' and a1.numjpp={txtnumemp.Text} and a1.tipo_nomina='N' ORDER BY a1.clave";

            }
            if (string.IsNullOrWhiteSpace(txtnumemp.Text) || string.IsNullOrWhiteSpace(comboBoxnom.Text)) return;

            List<Dictionary<string, object>> res = globales.consulta(query);
            double percepcion_suma = 0;
            double deduccion_suma = 0;
            foreach (var item in res)
            {
                string clave = Convert.ToString(item["clave"]);
                string descri = Convert.ToString(item["descri"]);
                double tot = Convert.ToDouble(item["monto"]);
                string monto = string.Format("{0:C}", tot).Replace("$","");
                string n_pago = Convert.ToString(item["pagon"]);
                string t_pago = Convert.ToString(item["pagot"]);
                string id = Convert.ToString(item["id"]);
                string tipopago = Convert.ToString(item["tipopago"]);
                string leyen = Convert.ToString(item["leyen"]);
                string secuen = Convert.ToString(item["secuen"]);
                string folio = Convert.ToString(item["folio"]);

                if (Convert.ToInt32(clave)<=60)//per
                {
                     percepcion_suma = Convert.ToDouble(tot)+ percepcion_suma;
                    

                }
                else
                {
                    deduccion_suma = Convert.ToDouble(tot) + deduccion_suma;

                }
                double total = percepcion_suma - deduccion_suma;
            txtletraImport.Text=    globales.convertirNumerosLetras(Convert.ToString(total), true);
                txtaport.Text = Convert.ToString(string.Format("{0:C}", percepcion_suma));
                txtdedu.Text = Convert.ToString(string.Format("{0:C}", deduccion_suma));
                txtsueld.Text = Convert.ToString(string.Format("{0:C}", total));
                dataNom.Rows.Add(clave, descri, monto, n_pago, t_pago,id,tipopago,leyen,secuen,folio);


            }
                dataNom.Columns[3].DefaultCellStyle.ForeColor = Color.Red;
                dataNom.Columns[4].DefaultCellStyle.ForeColor = Color.Red;
        }

        private void limpiaForm()
        {
            this.panel1.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = "");
            comboBoxnom.Text = "";
            dataNom.Rows.Clear();
            //this.N_Pago.Visible = false;
            //this.T_Pago.Visible = false;
            //this.Secuen.Visible = false;
        }

        private void bloquea()
        {
            btnguardar.Enabled = false;
            this.panel1.Controls.OfType<TextBox>().ToList().ForEach(o => o.Enabled = false);
            comboBoxnom.Enabled = false;
            dataNom.Enabled = false;

        }

      

        private void btnguardar_Click(object sender, EventArgs e)
        {
            dataNom.Rows.Clear();
            rellenaGrid();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                groupBox2.Visible = true;
                rellenaGrid();

            }
            else
            {
                groupBox2.Visible = false;
                rellenaGrid();
            }

        }

        private void frmconsulmodfnomina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnguardar_Click(null, null);
            }

            if (e.KeyCode==Keys.F1)
            {
                btnFolio_Click(null,null);
                dataNom.Select();

            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            rellenaGrid();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            rellenaGrid();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            rellenaGrid();

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            rellenaGrid();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            rellenaGrid();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            rellenaGrid();

        }
    }
}


