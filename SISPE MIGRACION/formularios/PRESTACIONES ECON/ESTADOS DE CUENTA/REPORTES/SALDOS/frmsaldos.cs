using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA.REPORTES.SALDOS;
using SISPE_MIGRACION.formularios.Seguros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.ESTADOS_DE_CUENTA
{
    public partial class frmsaldos : Form
    {
        public frmsaldos()
        {
            InitializeComponent();
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void frmsaldos_Load(object sender, EventArgs e)
        {

        }

        private void lbl1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            DialogResult dialogo = globales.MessageBoxQuestion("¿Desea realizar la operación?","Aviso",this);
            if (dialogo == DialogResult.No) return;
          
            string R2EDOCTA = string.Empty;
            string R2EC = string.Empty;
            string R3NCA = string.Empty;
            string quirografario = string.Empty;
            quirografario = "Q";
            R2EDOCTA = "p_edocta";
            R2EC = "descuentos";
            if (!rdQuiro.Checked)
            {
                R2EDOCTA = "P_edocth";
                R2EC = "descuentos";
                quirografario = "H";
            }
           

            if (rdNormal.Checked)
                R3NCA = " ='' ";
            else if (rdCobranzas.Checked)
                R3NCA = " ='C' ";
            else
                R3NCA = " <> 'X' ";

            
            this.Cursor = Cursors.WaitCursor;

            string fecha = string.Format("{0:yyyy-MM-dd}",dFecha.Value);

            //Creación de la tabla temporal del estado de cuenta
            string query = "CREATE TEMP TABLE edo AS SELECT	folio,	f_emischeq,	rfc,	nombre_em,	secretaria, " +
                " f_primdesc,	proyecto,	importe,	ubic_pagare,	imp_unit FROM " +
                $" datos.{R2EDOCTA} WHERE	(f_emischeq <= '{fecha}' OR f_emischeq IS NULL) " +
                $" AND importe IS NOT NULL AND ubic_pagare {R3NCA} order by folio asc;";

            query += $"create temp table des as select folio,max(numdesc) as numdesc,max(totdesc) as totdesc,max(f_descuento) as ultimop, Round(sum(importe)::Numeric,4) as pagado from datos.descuentos where t_prestamo = '{quirografario}' and (f_descuento <= '{fecha}' or f_descuento is null) group by folio order by folio asc;";
            query += "create temp table uniendo as select edo.folio,edo.f_emischeq,edo.rfc,edo.nombre_em,edo.secretaria,edo.f_primdesc,edo.proyecto,edo.importe,edo.ubic_pagare,edo.imp_unit,"+
                     "des.numdesc,des.totdesc,des.ultimop,COALESCE(des.pagado,0) as pagado,Round((importe - COALESCE(pagado,0))::numeric,4) as saldo from edo left join des on edo.folio = des.folio;";
            query += "create temp table consaldo as select * from uniendo where Round(pagado::NUMERIC,2) < Round(importe::NUMERIC,2) ;";
            query += $"create temp table cuentas as select consaldo.folio,max(d.cuenta) as cuenta from consaldo LEFT JOIN datos.descuentos d on consaldo.folio = d.folio where d.f_descuento = consaldo.ultimop and d.t_prestamo = '{quirografario}' group by consaldo.folio;";
            query += "create temp table consaldocuentas as select consaldo.*,cuentas.cuenta from consaldo left join cuentas on cuentas.folio = consaldo.folio;";
            query += " select consaldocuentas.*,c.descripcion as descripcion_cta from consaldocuentas left join catalogos.cuentas c on c.proy = consaldocuentas.cuenta;";
            List<Dictionary<string,object>> resultado = globales.consulta(query);
            foreach (Dictionary<string,object> item in resultado) {
                string folio = Convert.ToString(item["folio"]);
                if (folio == "34801") {

                }
                string cuenta = Convert.ToString(item["cuenta"]);
                string descripcion_cta = Convert.ToString(item["descripcion_cta"]);
               
                if (string.IsNullOrWhiteSpace(cuenta)) {
                    string secretaria = Convert.ToString(item["secretaria"]);
                    if (!string.IsNullOrWhiteSpace(secretaria)) {
                        string consultar = $"select cuenta,descripcion from catalogos.cuentas where proy = '{secretaria}'";
                        List<Dictionary<string, object>> tmp = globales.consulta(consultar);
                        if (tmp.Count != 0) {
                            Dictionary<string, object> obj = tmp[0];
                            item["cuenta"] = obj["cuenta"];
                            item["descripcion_cta"] = obj["descripcion"];
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(descripcion_cta)) {
                    string secretaria = Convert.ToString(item["cuenta"]);
                  
                        string consultar = $"select cuenta,descripcion from catalogos.cuentas where cuenta = '{secretaria}'";
                        List<Dictionary<string, object>> tmp = globales.consulta(consultar);
                        if (tmp.Count != 0)
                        {
                            Dictionary<string, object> obj = tmp[0];
                            item["cuenta"] = obj["cuenta"];
                            item["descripcion_cta"] = obj["descripcion"];
                        }
                    
                }


            }
            globales.showModal(new listaReportes(resultado, rdQuiro.Checked, string.Format("{0:d}", dFecha.Value)));
            this.Cursor = Cursors.Default;
        }

        private void frmsaldos_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
