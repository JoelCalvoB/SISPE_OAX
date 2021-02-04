using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Seguros
{
    public partial class frmimportarseguros : Form
    {
        public frmimportarseguros()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime desde = fecha1.Value;
            DateTime hasta = fecha2.Value;

            String c1 = string.Format("{0}-{1}-{2}", desde.Year, desde.Month, desde.Day);
            String c2 = string.Format("{0}-{1}-{2}", hasta.Year, hasta.Month, hasta.Day);

            string query = "SELECT folio,rfc,nombre_em,proyecto,secretaria,f_emischeq,f_primdesc,antig_q,(plazo / 2) AS plazo,(imp_unit * 2) AS imp_unit FROM datos.p_quirog WHERE antig_q >= 1 AND COALESCE(TRIM(secretaria) <> 'J') AND COALESCE(TRIM(secretaria) <> 'P') AND COALESCE(TRIM(secretaria) <> 'T') AND f_emischeq >= '2018-07-01' AND f_emischeq <= '2018-07-31' UNION SELECT folio, rfc, nombre_em, proyecto, secretaria, f_emischeq, f_primdesc, antig_q, plazo, imp_unit FROM datos.p_quirog WHERE secretaria in ('J','P','T') AND f_emischeq >= '2018-07-1' AND f_emischeq <= '2018-07-31'";
            List<Dictionary<string, object>> resultado = globales.consulta(query);

            List<Dictionary<string, object>> res1 = globales.consulta("select * from datos.s_quirog");
            string query1 = string.Empty;
            string query2 = string.Empty;
            int folio=0;
            foreach (Dictionary<string, object> item in resultado) {
                string antig_q = Convert.ToString(item["antig_q"]);
                if (string.IsNullOrWhiteSpace(antig_q))
                {
                    antig_q = "0";
                }
              
                bool encontrado = res1.Any(o => Convert.ToString(o["folio"]) == Convert.ToString(item["folio"]));
                if (!encontrado) {
                    query1 += $"insert into datos.s_quirog(folio,rfc,nombre_em,proyecto,secretaria,f_emischeq,f_primdesc,antig_q,plazo,imp_unit,num_desc) values ({item["folio"]},'{item["rfc"]}','{item["nombre_em"]}','{item["proyecto"]}','{item["secretaria"]}','{string.Format("{0:yyyy-MM-dd}",DateTime.Parse(item["f_emischeq"].ToString()))}','{string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["f_primdesc"].ToString()))}',{antig_q},{item["plazo"]},{item["imp_unit"]},1); ";
                    query2 += $"insert into datos.i_quirog(folio,rfc,nombre_em,proyecto,secretaria,f_emischeq,f_primdesc,antig_q,plazo,imp_unit,num_desc) values ({item["folio"]},'{item["rfc"]}','{item["nombre_em"]}','{item["proyecto"]}','{item["secretaria"]}','{string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["f_emischeq"].ToString()))}','{string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["f_primdesc"].ToString()))}',{antig_q},{item["plazo"]},{item["imp_unit"]},1); ";
                    folio++;
                }
            }
            globales.MessageBoxInformation("SELECCIONANDO ACTIVOS , PENSIONADOS Y JUBILADOS","Aviso",this);
            globales.MessageBoxInformation("INSERTANDO FOLIOS NUEVOS","Aviso",this);

            string query3 = "SELECT  count (folio) FROM datos.i_quirog";
            List<Dictionary<string,object>> resul= globales.consulta(query3);
       //      folio = Convert.ToString(resul[0]["count"]);
            globales.MessageBoxInformation("SE AGREGARON "+ folio + " FOLIOS NUEVOS","Aviso",this);
            globales.consulta(query1,true);
            globales.consulta(query2, true);

            globales.MessageBoxSuccess("PROCESO TERMINADO","Proceso terminado",this);

        }
        private void frmimportarseguros_Load(object sender, EventArgs e)
        {
            DateTime fec1 = fecha1.Value;
            fecha1.Format = DateTimePickerFormat.Custom;
            fecha1.CustomFormat = "dd/MM/yyyy";
            DateTime fec2 = fecha2.Value;
            fecha2.Format = DateTimePickerFormat.Custom;
            fecha2.CustomFormat = "dd/MM/yyyy";

            DateTime auxFecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
            fecha1.Value = auxFecha;

            auxFecha = auxFecha.AddMonths(1);
            auxFecha = auxFecha.AddDays(-1);

            fecha2.Value = auxFecha;

//            begin
//TRUNCATE table datos.s_quirog;
//            create temp table impsegur as SELECT folio,rfc,nombre_em,proyecto,secretaria,f_emischeq,f_primdesc,antig_q,(plazo / 2) AS plazo,(imp_unit * 2) AS imp_unit FROM datos.p_quirog WHERE antig_q >= 1 AND COALESCE(TRIM(secretaria) <> 'J') AND COALESCE(TRIM(secretaria) <> 'P') AND COALESCE(TRIM(secretaria) <> 'T') AND f_emischeq >= $1 AND f_emischeq <= $2 UNION SELECT folio, rfc, nombre_em, proyecto, secretaria, f_emischeq, f_primdesc, antig_q, plazo, imp_unit FROM datos.p_quirog WHERE secretaria = 'J' AND f_emischeq >= $1 AND f_emischeq <= $2 ;
//            create temp table impsegur1 as SELECT folio,rfc,nombre_em,proyecto,secretaria,f_emischeq,f_primdesc,antig_q,(plazo / 2) AS plazo,(imp_unit * 2) AS imp_unit FROM datos.p_quirog WHERE secretaria IN('P', 'T') AND f_emischeq >= $1 AND f_emischeq <= $2;
//            end;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmimportarseguros_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

        private void frmimportarseguros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
    }

