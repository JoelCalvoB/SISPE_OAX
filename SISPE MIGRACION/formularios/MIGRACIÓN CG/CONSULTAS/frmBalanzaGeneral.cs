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
    public partial class frmBalanzaGeneral : Form
    {

        private bool estadoresultados
        {
            get;set;
        }

        private string polmes { get; set; }

        public frmBalanzaGeneral()
        {
            InitializeComponent();
            this.estadoresultados = globales.estadoResultado;
        }

        private void frmBalanzaGeneral_Load(object sender, EventArgs e)
        {
            txtanio.Text = DateTime.Now.Year.ToString();
            comboBox1.SelectedIndex = 0;

            

            realizarConsulta();

        }


        public void realizarConsulta()
        {
            dtggrid.Rows.Clear();
            polmes = tabControl1.SelectedIndex < 10 ? $"0{tabControl1.SelectedIndex}" : tabControl1.SelectedIndex.ToString();

            if (this.estadoresultados)
                label1.Text = "Estado de resultados";



            string query = "select replace(cuenta,'.','') as cuenta,descripcion from cg_fondos.cuentas where (cuenta like '1%' or " +

               "cuenta like '2%' or cuenta like '3%') and length(trim(replace(cuenta, '.', ''))) <= 2 order by replace(cuenta, '.', ''); ";

            if (this.estadoresultados)
            {

                query = "select replace(cuenta,'.','') as cuenta,descripcion from cg_fondos.cuentas where (cuenta like '4%' or " +

               "cuenta like '5%' ) and length(trim(replace(cuenta, '.', ''))) <= 2 order by replace(cuenta, '.', ''); ";

            }


            List<Dictionary<string, object>> resultado = globales.consulta(query);

            polmes = calculandopolmethod();
            if (this.estadoresultados) {
                query = " create temp table t1 as select substring(cuenta,1,3) as cuenta ,sum(importe),naturaleza   from cg_fondos.pol where ({0}) and anio = {1} and polmes in ({2}) group by substring(cuenta,1,3),naturaleza order by substring(cuenta,1,3),naturaleza;select t1.*,hs.descripcion from t1 left join cg_fondos.cuentas hs on t1.cuenta = replace(hs.cuenta,'.','') ";
            } else {
                query = " create temp table t1 as select substring(cuenta,1,3) as cuenta ,sum(importe),naturaleza   from cg_fondos.pol where ({0}) and anio = {1} and polmes in ({2}) group by substring(cuenta,1,3),naturaleza order by substring(cuenta,1,3),naturaleza;select t1.*,hs.descripcion from t1 left join cg_fondos.cuentas hs on t1.cuenta = replace(hs.cuenta,'.','') ";
            }
            string loquefalta = string.Empty;
            foreach (Dictionary<string, object> item in resultado) {

                if (Convert.ToString(item["cuenta"]).Length == 2) {
                    loquefalta += $"cuenta  like '{item["cuenta"]}%',";
                }

            }

            loquefalta = loquefalta.Substring(0, loquefalta.Length - 1);

            loquefalta = loquefalta.Replace(",", " or ");

            query = string.Format(query, loquefalta,txtanio.Text,polmes);

            List<Dictionary<string, object>> rr = globales.consulta(query);


            List<Dictionary<string, object>> listafinal = new List<Dictionary<string, object>>();



          
            double totalCuenta = 0;
            bool primero = true;
            Dictionary<string, object> temp = null;
            foreach (Dictionary<string, object> item in resultado) {
                Dictionary<string, object> obj = new Dictionary<string, object>();
                obj.Add("cuenta",item["cuenta"]);
                obj.Add("descripcion", item["descripcion"]);
                obj.Add("naturaleza","");
                obj.Add("importe", "");
                listafinal.Add(obj);
               

                if (Convert.ToString(item["cuenta"]).Length == 2)
                {
                    List<Dictionary<string, object>> tempLista = rr.Where(o => Convert.ToString(o["cuenta"]).Substring(0,2) == (Convert.ToString(item["cuenta"]))).ToList();
                    foreach (Dictionary<string, object> mm in tempLista)
                    {
                        Dictionary<string, object> obj1 = new Dictionary<string, object>();
                        obj1.Add("cuenta", mm["cuenta"]);
                        obj1.Add("descripcion", mm["descripcion"]);
                        obj1.Add("naturaleza", mm["naturaleza"]);
                        obj1.Add("importe", mm["sum"]);
                        listafinal.Add(obj1);


                        if (Convert.ToString(mm["descripcion"]).ToLower().Contains("revaluo"))
                        {
                            double totalDelEjercicio  = obtenerEstadoActividades();

                            Dictionary<string, object> obj2 = new Dictionary<string, object>();
                            obj2.Add("cuenta", "no hay cuenta");
                            obj2.Add("descripcion", "            RESULTADO DEL EJERCICIO");
                            obj2.Add("naturaleza", "NO HAY");
                            obj2.Add("importe", totalDelEjercicio);
                            listafinal.Add(obj2);

                        }
                    }

                    
                }
               

            }


            int esuno = 0;
            int esDos = 0;

            string anterior = string.Empty;
            int rowanterior = 0;
            foreach (Dictionary<string,object> item in listafinal) {

                if (Convert.ToString(item["cuenta"]) == "12")
                {

                }
                if (Convert.ToString(item["cuenta"]).Length != 0)
                {
                    if (Convert.ToString(item["cuenta"]).Substring(0, 1) == "5")
                    {
                        if (Convert.ToString(item["naturaleza"]) == "H")
                        {
                            item["importe"] = globales.convertDouble(Convert.ToString(item["importe"])) * -1;
                        }
                    }
                    else if (Convert.ToString(item["cuenta"]).Substring(0, 1) == "1")
                    {
                        if (Convert.ToString(item["naturaleza"]) == "H")
                        {
                            item["importe"] = globales.convertDouble(Convert.ToString(item["importe"])) * -1;
                        }
                    }
                    else if (Convert.ToString(item["cuenta"]).Substring(0, 1) == "2")
                    {
                        if (Convert.ToString(item["naturaleza"]) == "D")
                        {
                            item["importe"] = globales.convertDouble(Convert.ToString(item["importe"])) * -1;
                        }
                    }
                    else if (Convert.ToString(item["cuenta"]).Substring(0, 1) == "3")
                    {
                        if (Convert.ToString(item["naturaleza"]) == "D")
                        {
                            item["importe"] = globales.convertDouble(Convert.ToString(item["importe"])) * -1;
                        }
                    }
                    else if (Convert.ToString(item["cuenta"]).Substring(0, 1) == "4")
                    {
                        if (Convert.ToString(item["naturaleza"]) == "D")
                        {
                            item["importe"] = globales.convertDouble(Convert.ToString(item["importe"])) * -1;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(Convert.ToString(item["cuenta"]))) {
                    if (anterior == Convert.ToString(item["cuenta"])) {

                        dtggrid.Rows[esuno].Cells[2].Value = globales.convertDouble(dtggrid.Rows[esuno].Cells[2].Value.ToString()) - globales.convertDouble(dtggrid.Rows[rowanterior].Cells[1].Value.ToString());
                        dtggrid.Rows[esDos].Cells[2].Value = globales.convertDouble(dtggrid.Rows[esDos].Cells[2].Value.ToString()) - globales.convertDouble(dtggrid.Rows[rowanterior].Cells[1].Value.ToString());

                        dtggrid.Rows[rowanterior].Cells[1].Value = globales.convertDouble(Convert.ToString(dtggrid.Rows[rowanterior].Cells[1].Value)) + globales.convertDouble(Convert.ToString(item["importe"]));
                        dtggrid.Rows[rowanterior].Cells[1].Value = globales.convertMoneda(globales.convertDouble(Convert.ToString(dtggrid.Rows[rowanterior].Cells[1].Value)));

                        dtggrid.Rows[esuno].Cells[2].Value = globales.convertDouble(dtggrid.Rows[esuno].Cells[2].Value.ToString()) + globales.convertDouble(dtggrid.Rows[rowanterior].Cells[1].Value.ToString());
                        dtggrid.Rows[esDos].Cells[2].Value = globales.convertDouble(dtggrid.Rows[esDos].Cells[2].Value.ToString()) + globales.convertDouble(dtggrid.Rows[rowanterior].Cells[1].Value.ToString());

                        dtggrid.Rows[esuno].Cells[2].Value = globales.convertMoneda(globales.convertDouble(dtggrid.Rows[esuno].Cells[2].Value.ToString()));
                        dtggrid.Rows[esDos].Cells[2].Value = globales.convertMoneda(globales.convertDouble(dtggrid.Rows[esDos].Cells[2].Value.ToString()));
                        continue;
                    }
                }
                anterior = Convert.ToString(item["cuenta"]);

                int row =   dtggrid.Rows.Add(item["descripcion"], (!string.IsNullOrWhiteSpace(Convert.ToString(item["importe"])))?globales.convertMoneda(globales.convertDouble(Convert.ToString(item["importe"]))):"","",item["naturaleza"],item["cuenta"]);

                rowanterior = row;

                esuno = Convert.ToString(dtggrid.Rows[row].Cells[4].Value).Length == 1 ? row : esuno;
                esDos = Convert.ToString(dtggrid.Rows[row].Cells[4].Value).Length == 2 ? row : esDos;


                dtggrid.Rows[esuno].Cells[2].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                dtggrid.Rows[esDos].Cells[2].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

               /*if (Convert.ToString(item["cuenta"]) == "126")
                {
                    dtggrid.Rows[esuno].Cells[2].Value = globales.convertDouble(dtggrid.Rows[esuno].Cells[2].Value.ToString()) - globales.convertDouble(dtggrid.Rows[row].Cells[1].Value.ToString());
                    dtggrid.Rows[esDos].Cells[2].Value = globales.convertDouble(dtggrid.Rows[esDos].Cells[2].Value.ToString()) - globales.convertDouble(dtggrid.Rows[row].Cells[1].Value.ToString());
                }
                else {
                    dtggrid.Rows[esuno].Cells[2].Value = globales.convertDouble(dtggrid.Rows[esuno].Cells[2].Value.ToString()) + globales.convertDouble(dtggrid.Rows[row].Cells[1].Value.ToString());
                    dtggrid.Rows[esDos].Cells[2].Value = globales.convertDouble(dtggrid.Rows[esDos].Cells[2].Value.ToString()) + globales.convertDouble(dtggrid.Rows[row].Cells[1].Value.ToString());
                }*/
               dtggrid.Rows[esuno].Cells[2].Value = globales.convertDouble(dtggrid.Rows[esuno].Cells[2].Value.ToString()) + globales.convertDouble(dtggrid.Rows[row].Cells[1].Value.ToString());
               dtggrid.Rows[esDos].Cells[2].Value = globales.convertDouble(dtggrid.Rows[esDos].Cells[2].Value.ToString()) + globales.convertDouble(dtggrid.Rows[row].Cells[1].Value.ToString());


                dtggrid.Rows[esuno].Cells[2].Value = globales.convertMoneda(globales.convertDouble(dtggrid.Rows[esuno].Cells[2].Value.ToString()));
                dtggrid.Rows[esDos].Cells[2].Value = globales.convertMoneda(globales.convertDouble(dtggrid.Rows[esDos].Cells[2].Value.ToString()));

                if (Convert.ToString(dtggrid.Rows[row].Cells[4].Value).Length <= 2) {
                     dtggrid.Rows[row].Cells[0].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }


                dtggrid.Rows[row].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dtggrid.Rows[row].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;


               

            }


            double totalPasivo = 0;
            double totalHacienda = 0;

            double totalingresos = 0;
            double totalgastos = 0;
            foreach (DataGridViewRow item in dtggrid.Rows) {

                if (Convert.ToString(item.Cells[4].Value) == "2") {
                     totalPasivo = globales.convertDouble(Convert.ToString(item.Cells[2].Value));
                }
                if (Convert.ToString(item.Cells[4].Value) == "3")
                {
                    totalHacienda = globales.convertDouble(Convert.ToString(item.Cells[2].Value));
                }


                if (Convert.ToString(item.Cells[4].Value) == "4")
                {
                    totalingresos = globales.convertDouble(Convert.ToString(item.Cells[2].Value));
                }
                if (Convert.ToString(item.Cells[4].Value) == "5")
                {
                    totalgastos = globales.convertDouble(Convert.ToString(item.Cells[2].Value));
                }

                
            }


            dtggrid.Rows.Add("");
            int posicion = dtggrid.Rows.Add((!this.estadoresultados)? "TOTAL PASIVO  + HACIENDA PUBLICA":"RESULTADO DEL EJERCICIO", "", (!this.estadoresultados)? globales.convertMoneda(totalPasivo + totalHacienda):globales.convertMoneda(totalingresos-totalgastos));
            dtggrid.Rows[posicion].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtggrid.Rows[posicion].Cells[2].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dtggrid.Rows[posicion].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtggrid.Rows[posicion].Cells[0].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        }

        private double obtenerEstadoActividades()
        {
            string query = string.Empty;
            query = "select replace(cuenta,'.','') as cuenta,descripcion from cg_fondos.cuentas where (cuenta like '4%' or " +

            "cuenta like '5%' ) and length(trim(replace(cuenta, '.', ''))) <= 2 order by replace(cuenta, '.', ''); ";
            List<Dictionary<string, object>> resultado = globales.consulta(query);


            polmes = calculandopolmethod();

            query = " create temp table t1 as select substring(cuenta,1,3) as cuenta ,sum(importe),naturaleza   from cg_fondos.pol where ({0}) and anio = {1} and polmes in ({2}) group by substring(cuenta,1,3),naturaleza order by substring(cuenta,1,3),naturaleza;select t1.*,hs.descripcion from t1 left join cg_fondos.cuentas hs on t1.cuenta = replace(hs.cuenta,'.','') ";
            
            
            string loquefalta = string.Empty;
            foreach (Dictionary<string, object> item in resultado)
            {

                if (Convert.ToString(item["cuenta"]).Length == 2)
                {
                    loquefalta += $"cuenta  like '{item["cuenta"]}%',";
                }

            }

            loquefalta = loquefalta.Substring(0, loquefalta.Length - 1);

            loquefalta = loquefalta.Replace(",", " or ");

            query = string.Format(query, loquefalta, txtanio.Text, polmes);

            List<Dictionary<string, object>> rr = globales.consulta(query);

            double totalIngreso = 0;
            double totalEgreso = 0;
            foreach (Dictionary<string,object> item in rr) {
                if (Convert.ToString(item["cuenta"]).Substring(0, 1) == "4") {
                    if (Convert.ToString(item["naturaleza"]) == "D")
                    {
                        item["sum"] = globales.convertDouble(Convert.ToString(item["sum"])) * -1;
                    }

                    totalIngreso += globales.convertDouble(Convert.ToString(item["sum"]));
                } else if (Convert.ToString(item["cuenta"]).Substring(0, 1) == "5") {
                    if (Convert.ToString(item["naturaleza"]) == "H")
                    {
                        item["sum"] = globales.convertDouble(Convert.ToString(item["sum"])) * -1;
                    }

                    totalEgreso += globales.convertDouble(Convert.ToString(item["sum"]));

                }

               



            }


            return totalIngreso - totalEgreso;
        }

        private string calculandopolmethod()
        {
            string resultado = string.Empty;

            for (int x = 0; x <= tabControl1.SelectedIndex; x++) {
                resultado += "'"+((x < 10)? $"0{x}":x.ToString())+"',";
            }

            resultado = resultado.Substring(0,resultado.Length - 1);

            return resultado;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            polmes = tabControl1.SelectedIndex < 10 ? $"0{tabControl1.SelectedIndex}" : tabControl1.SelectedIndex.ToString();
            realizarConsulta();
        }

        private void txtanio_Leave(object sender, EventArgs e)
        {
            realizarConsulta();
        }
    }
}
