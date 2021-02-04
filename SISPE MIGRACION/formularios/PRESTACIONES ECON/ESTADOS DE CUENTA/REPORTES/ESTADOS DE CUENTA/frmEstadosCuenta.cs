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
    public partial class frmEstadosCuenta : Form
    {
        public frmEstadosCuenta()
        {
            InitializeComponent();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            p1.Visible = true;
            p2.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            p1.Visible = false;
            p2.Visible = true;
        }

        private void frmEstadosCuenta_Load(object sender, EventArgs e)
        {
            p2.Visible = false;
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ECEDOCTA = string.Empty;
            string ECEC = string.Empty;
            string ECNCA = string.Empty;
            string filtrado = string.Empty;

            DialogResult p = globales.MessageBoxQuestion("¿Desea seguir con la operación?","Aviso",this);
            if (p == DialogResult.No) return;

            ECEDOCTA = "p_edocta";
            ECEC = "descuentos";
            string tipo = "Q";

            if (rdHipotecario.Checked)
            {
                ECEDOCTA = "P_edocth";
                ECEC = "descuentos";
                tipo = "H";
            }
            else if(rdMoratorios.Checked){
                ECEDOCTA = "P_edocth";
                ECEC = "descuentos";
                tipo = "M";
            }


            if (rdNormal.Checked)
                ECNCA = "  = '' ";
            else if (rdCobranzas.Checked)
                ECNCA = " = 'C' ";
            else
                ECNCA = "  <> 'X' ";


            if (!rdDependencias.Checked)
                filtrado = $" where folio between {txtDel.Text} and {txtAl.Text} ";


            if (rdDependencias.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtdependencia.Text))
                {
                    globales.MessageBoxExclamation("Favor de ingresar una dependencia", "Aviso", this);
                    txtdependencia.Focus();
                    return;
                }
               
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtDel.Text))
                {
                    globales.MessageBoxExclamation("Favor de ingresar folio inicial", "Aviso", this);
                    txtDel.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtAl.Text))
                {
                    globales.MessageBoxExclamation("Favor de ingresar folio final", "Aviso", this);
                    txtAl.Focus();
                    return;
                }
            }

            List<Dictionary<string, object>> resultado = null;
            string fecha = string.Format("{0:yyyy-MM-dd}", dFecha.Value);

            string consaldo = chkFolios.Checked ? " where Round(pagado::NUMERIC,2) < Round(importe::NUMERIC,2) " : "";

            //Creación de la tabla temporal del estado de cuenta
            string query = "CREATE TEMP TABLE edo AS SELECT	folio,	f_emischeq,	rfc,	nombre_em,	secretaria, " +
                " f_primdesc,	proyecto,	importe,	ubic_pagare,	imp_unit FROM " +
                $" datos.{ECEDOCTA} WHERE	(f_emischeq <= '{fecha}' OR f_emischeq IS NULL) " +
                $" AND importe IS NOT NULL AND ubic_pagare { ECNCA} order by folio asc;";

            query += $"create temp table des as select folio,max(numdesc) as numdesc,max(totdesc) as totdesc,max(f_descuento) as ultimop, Round(sum(importe)::Numeric,2) as pagado from datos.descuentos where t_prestamo = '{tipo}' and (f_descuento <= '{fecha}' or f_descuento is null)  group by folio order by folio asc;";
            query += "create temp table uniendo as select edo.folio,edo.f_emischeq,edo.rfc,edo.nombre_em,edo.secretaria,edo.f_primdesc,edo.proyecto,edo.importe,edo.ubic_pagare,edo.imp_unit," +
                     "des.numdesc,des.totdesc,des.ultimop,COALESCE(des.pagado,0) as pagado,Round((importe - COALESCE(pagado,0))::numeric,2) as saldo from edo left join des on edo.folio = des.folio;";
            query += $"create temp table consaldo as select * from uniendo {consaldo};";
            query += $"create temp table cuentas as select consaldo.folio,max(d.cuenta) as cuenta from consaldo LEFT JOIN datos.descuentos d on consaldo.folio = d.folio where d.f_descuento = consaldo.ultimop and d.t_prestamo = '{tipo}' group by consaldo.folio;";
            query += "create temp table consaldocuentas as select consaldo.*,cuentas.cuenta from consaldo left join cuentas on cuentas.folio = consaldo.folio;";
            query += "create temp table resultadofinal as  select consaldocuentas.*,c.descripcion as descripcion_cta from consaldocuentas left join catalogos.cuentas c on c.cuenta = consaldocuentas.cuenta;";
            query += $" select * from resultadofinal {filtrado}";

            resultado = globales.consulta(query);
            foreach (Dictionary<string, object> item in resultado)
            {
                string cuenta = Convert.ToString(item["cuenta"]);
                if (string.IsNullOrWhiteSpace(cuenta))
                {
                    string secretaria = Convert.ToString(item["secretaria"]);
                    if (!string.IsNullOrWhiteSpace(secretaria))
                    {
                        string consultar = $"select cuenta,descripcion from catalogos.cuentas where proy = '{secretaria}'";
                        List<Dictionary<string, object>> tmp = globales.consulta(consultar);
                        if (tmp.Count != 0)
                        {
                            Dictionary<string, object> obj = tmp[0];
                            item["cuenta"] = obj["cuenta"];
                            item["descripcion_cta"] = obj["descripcion"];
                        }
                    }
                }
            }

            if (rdDependencias.Checked) {
                List<Dictionary<string, object>> tmp = resultado.Where(o => Convert.ToString(o["cuenta"]) == txtdependencia.Text).ToList<Dictionary<string, object>>();
                resultado = tmp;
            }

            this.Cursor = Cursors.Default;


            if (resultado.Count == 0)
            {

                globales.MessageBoxExclamation("No hay datos para el reporte","Aviso",this);
                return;

            }
            globales.showModal(new frmMostrarDatos(resultado,string.Format("{0:yyyy-MM-dd}",dFecha.Value), tipo));            
            this.Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void txtDel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
        }

        private void folios(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.numerico(e.KeyChar);
        }

        private void frmEstadosCuenta_FormClosing(object sender, FormClosingEventArgs e)
        {
       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
