using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.VALIDACIONES
{
    public partial class frmSinPagos : Form
    {
        private bool terminanDePagar { get; set; }
        public frmSinPagos(bool terminanDePagar = false)
        {
            InitializeComponent();
            this.terminanDePagar = terminanDePagar;
        }

        private void rdHipotecario_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmSinPagos_Load(object sender, EventArgs e)
        {
            int minimo = 1997;
            int maximo = DateTime.Now.Year + 1;
            for (int x = maximo; x >= minimo; x--)
                cmbAño.Items.Add(x);

            string[] meses = globales.getMeses();
            for (int x = 1; x < meses.Length; x++)
                cmbMes.Items.Add(meses[x]);

            cmbAño.SelectedIndex = 1;
            cmbMes.SelectedIndex = DateTime.Now.Month - 1;

            label4.Text = (this.terminanDePagar) ? "Saldo pagado.." : "Sin saldo..";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string R3F1 = string.Empty;
            string R3F2 = string.Empty;
            string R3EDOCTA = string.Empty;
            string R3EC = string.Empty;
            string R3ECD = string.Empty;
            string R3NCA = string.Empty;
            string R3JPT = string.Empty;
            string sFechaEmision = string.Empty;
            DateTime tiempo;

            if (rdQuincena.Checked)
            {
                R3F1 = string.Format("{0}-{1}-01", cmbAño.Text, cmbMes.SelectedIndex + 1);
                R3F2 = string.Format("{0}-{1}-15", cmbAño.Text, cmbMes.SelectedIndex + 1);
                R3JPT = (this.terminanDePagar)?"":" AND tipo_pago <> 'M' ";
            }
            else if (rdQuincena2.Checked)
            {
                R3F1 = string.Format("{0}-{1}-16", cmbAño.Text, cmbMes.SelectedIndex + 1);
                tiempo = new DateTime(Convert.ToInt32(cmbAño.Text), cmbMes.SelectedIndex + 1, 16);
                tiempo = tiempo.AddDays(30);
                tiempo = new DateTime(tiempo.Year, tiempo.Month, 01);
                tiempo = tiempo.AddDays(-1);
                R3F2 = string.Format("{0}-{1}-{2}", tiempo.Year, tiempo.Month, tiempo.Day);
            }
            else
            {
                R3F1 = string.Format("{0}-{1}-01", cmbAño.Text, cmbMes.SelectedIndex + 1);
                tiempo = new DateTime(Convert.ToInt32(cmbAño.Text), cmbMes.SelectedIndex + 1, 16);
                tiempo = tiempo.AddDays(30);
                tiempo = new DateTime(tiempo.Year, tiempo.Month, 01);
                tiempo = tiempo.AddDays(-1);
                R3F2 = string.Format("{0}-{1}-{2}", tiempo.Year, tiempo.Month, tiempo.Day);
            }

            R3EC = " descuentos ";
            R3ECD = " solicitud_dependencias ";
            string tipoprestamo = "Q";

            if (rdQuiro.Checked)
            {
                R3EDOCTA = "p_edocta";
            }
            else
            {
                R3EDOCTA = "p_edocth";
                tipoprestamo = "H";
            }

            if (rdNormal.Checked)
                R3NCA = " = '' ";
            else if (rdCobranzas.Checked)
                R3NCA = "   = 'C' ";
            else
                R3NCA = "   <> 'X' ";

            this.Cursor = Cursors.WaitCursor;
            #region obteniendo saldos
            DateTime fechaR1 = DateTime.Parse(R3F1);
            fechaR1 = new DateTime(fechaR1.Year, fechaR1.Month, 1);
            fechaR1 = fechaR1.AddDays(-1);


            string query = "";

            if (!this.terminanDePagar)
            {
                 query = "CREATE TEMP TABLE edo AS SELECT	folio,	f_emischeq,	rfc,	nombre_em,	secretaria, " +
               " f_primdesc,	proyecto,	importe,	ubic_pagare,	imp_unit FROM " +
               $" datos.{R3EDOCTA} WHERE	(f_emischeq <= '{string.Format("{0:yyyy-MM-dd}", (fechaR1))}' OR f_emischeq IS NULL) " +
               $"  AND ubic_pagare {R3NCA} {R3JPT}  order by folio asc;";

                query += $"create temp table des as select folio,max(numdesc) as numdesc,max(totdesc) as totdesc, Round(sum(importe)::Numeric,4) as pagado from datos.descuentos where t_prestamo = '{tipoprestamo}' and (f_descuento <= '{R3F2}' or f_descuento is null)  group by folio order by folio asc;";
                query += "create temp table uniendo as select edo.folio,edo.f_emischeq,edo.rfc,edo.nombre_em,edo.secretaria,edo.f_primdesc,edo.proyecto,edo.importe,edo.ubic_pagare,edo.imp_unit," +
                         "des.numdesc,des.totdesc,COALESCE(des.pagado,0) as pagado,Round((importe - COALESCE(pagado,0))::numeric,4) as saldo from edo left join des on edo.folio = des.folio;";
                query += "create temp table consaldo as select * from uniendo where Round(pagado::NUMERIC,2) < Round(importe::NUMERIC,2) ;";
                query += $" create temp table eliminar as select folio from datos.descuentos where (f_descuento >= '{R3F1}' and f_descuento <= '{R3F2}') and t_prestamo = '{tipoprestamo}' GROUP BY folio; ";
                query += " delete from consaldo using eliminar where consaldo.folio = eliminar.folio; ";
                query += $" create temp table ultimopago as select folio,max (f_descuento) as ultimop from datos.descuentos where t_prestamo = '{tipoprestamo}' group by folio;";
                query += " create temp table aux1 as SELECT	consaldo.*,ultimopago.ultimop FROM 	consaldo LEFT JOIN ultimopago ON consaldo.folio = ultimopago.folio; ";
                query += " create temp table solici_dependencias as SELECT	aux1.folio,	s.f_descuento,s.tipo_mov FROM	aux1 INNER JOIN datos.solicitud_dependencias s ON aux1.folio = s.folio WHERE	(aux1.ultimop IS NULL OR s.f_descuento > aux1.ultimop) and s.f_descuento is not null; ";
                query += " SELECT	aux1.*, solici_dependencias.f_descuento,	solici_dependencias.tipo_mov FROM	aux1 LEFT JOIN solici_dependencias ON aux1.folio = solici_dependencias.folio " +
                         " group by aux1.folio,aux1.f_emischeq,aux1.rfc,aux1.nombre_em,aux1.secretaria,aux1.f_primdesc,aux1.proyecto,aux1.importe,aux1.ubic_pagare,aux1.imp_unit,aux1.numdesc,aux1.totdesc, " +
                         " aux1.pagado,aux1.saldo,aux1.ultimop, solici_dependencias.f_descuento,	solici_dependencias.tipo_mov ORDER BY	aux1.folio ASC; ";
            }
            else {
                query = "CREATE TEMP TABLE edo AS SELECT	folio,	f_emischeq,	rfc,	nombre_em,	secretaria, " +
              " f_primdesc,	proyecto,	importe,	ubic_pagare,	imp_unit FROM " +
              $" datos.{R3EDOCTA} WHERE	(f_emischeq <= '{string.Format("{0:yyyy-MM-dd}", (fechaR1))}' OR f_emischeq IS NULL) " +
              $"  AND ubic_pagare {R3NCA}   order by folio asc;";

                query += $"create temp table des as select folio,max(numdesc) as numdesc,max(totdesc) as totdesc, Round(sum(importe)::Numeric,4) as pagado from datos.descuentos where t_prestamo = '{tipoprestamo}' and (f_descuento <= '{R3F2}' or f_descuento is null) group by folio order by folio asc;";
                query += "create temp table uniendo as select edo.folio,edo.f_emischeq,edo.rfc,edo.nombre_em,edo.secretaria,edo.f_primdesc,edo.proyecto,edo.importe,edo.ubic_pagare,edo.imp_unit," +
                         "des.numdesc,des.totdesc,COALESCE(des.pagado,0) as pagado,Round((importe - COALESCE(pagado,0))::numeric,4) as saldo from edo left join des on edo.folio = des.folio;";
                query += "create temp table consaldo as select * from uniendo where Round(pagado::NUMERIC,2) >= Round(importe::NUMERIC,2) ;";
                query += $" create temp table eliminar as select folio from datos.descuentos where f_descuento >= '{R3F1}' and f_descuento <= '{R3F2}' and t_prestamo = '{tipoprestamo}' GROUP BY folio; ";
                query += " select consaldo.*,'' as ultimop,'' as tipo_mov, '' as f_descuento from consaldo inner join eliminar on consaldo.folio = eliminar.folio order by consaldo.folio asc; ";
                
            }


            List<Dictionary<string, object>> resultado = globales.consulta(query);








            #endregion
            this.Cursor = Cursors.Default;
            object[] objetos = new object[resultado.Count];
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado)
            {

                string folio = Convert.ToString(item["folio"]);
                string rfc = Convert.ToString(item["rfc"]);
                string nombre = Convert.ToString(item["nombre_em"]);
                string ubicpagare = Convert.ToString(item["ubic_pagare"]);
                string proyecto = Convert.ToString(item["proyecto"]);
                string numdesc = Convert.ToString(item["numdesc"]) + "/" + Convert.ToString(item["totdesc"]);
                string importe = globales.checarDecimales(string.IsNullOrWhiteSpace(Convert.ToString(item["importe"])) ? "0.00" : Convert.ToString(item["importe"]));
                string pagado = string.IsNullOrWhiteSpace(Convert.ToString(item["pagado"])) ? "0.00" : Convert.ToString(item["pagado"]);
                string saldo = globales.checarDecimales((Convert.ToDouble(importe) - Convert.ToDouble(pagado)));
                string ultimop = string.IsNullOrWhiteSpace(Convert.ToString(item["ultimop"])) ? "" : string.Format("{0:d}", item["ultimop"]);
                string tipomov = Convert.ToString(item["tipo_mov"]);
                string f_descuento = string.IsNullOrWhiteSpace(Convert.ToString(item["f_descuento"])) ? "" : string.Format("{0:d}", item["f_descuento"]);



                objetos[contador] = new object[] {
                    folio,rfc,nombre,ubicpagare,proyecto,numdesc,importe,saldo,
                    ultimop,tipomov,f_descuento
                };

                contador++;
            }

            this.Cursor = Cursors.Default;

            globales.showModal(new frmSalida(rdQuiro.Checked, R3F1, R3F2, objetos, resultado));

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
