using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.NOMINAS_ESPECIALES
{
    public partial class frmAguinaldo : Form
    {
        public frmAguinaldo()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fecha = string.Empty; ;

            if (comboMes.SelectedIndex == 0)
            {
                fecha = "2020-05-30";
            }
            if (comboMes.SelectedIndex == 1)
            {
                fecha = "2020-11-30";
            }
            if (string.IsNullOrWhiteSpace(fecha)) return;


            string jppCombo = string.Empty;

            if (comboBox1.SelectedIndex == 0)
            {
                jppCombo = "JUB";
            }
            else
            {
                jppCombo = "PDO";
            }
            //string query = $"CREATE TEMP TABLE BASE AS (SELECT      a1.jpp, a1.num, a2.monto, a1.tiporel, a1.fching, a1.ley  FROM        nominas_catalogos.maestro a1    LEFT JOIN nominas_catalogos.nominew a2 ON a1.jpp = a2.jpp   AND a1.num = a2.numjpp  WHERE   a1.jpp = 'JUB' AND a2.clave = 1     AND a2.tipopago = 'N' AND a1.superviven = 'S'     ORDER BY    a1.tiporel)" +
            //"CREATE TEMP TABLE tabladias AS(SELECT *, '2020-11-30' - fching AS dias   FROM        BASE    ORDER BY        num);" +
            //"UPDATE tabladias SET monto = (monto / 30) * 35 WHERE DIAS >= 180 AND ley = 2; " +
            //"UPDATE tabladias SET monto = (monto / 30) * 35 / 180 * dias WHERE DIAS <= 179 and ley = 2;" +
            //"update tabladias set monto = (monto / 30) * 35 where dias >= 180 AND tiporel<> 'BASE' and ley = 1" +
            //"update tabladias  set monto = (monto / 30) * 39 where dias >= 180 AND tiporel = 'BASE' and ley = 1" +
            //"SELECT * FROM tabladias order by num;";

            string query = "CREATE TEMP TABLE BASE AS (   SELECT     a1.jpp , 	a1.num,		a2.monto,		a1.tiporel,		a1.fching,		a1.ley,		a1.dias_aguinaldo,		CASE   WHEN dias_aguinaldo = 70 THEN      35  WHEN dias_aguinaldo = 78 THEN      39   END d_aguinaldo    FROM       nominas_catalogos.maestro a1" +
   " LEFT JOIN nominas_catalogos.nominew a2 ON a1.jpp = a2.jpp  AND a1.num = a2.numjpp WHERE  a1.jpp = 'JUB' AND a2.clave = 1  AND a2.tipopago = 'N'  AND a1.superviven = 'S'  AND dias_aguinaldo IS NOT NULL  AND dias_aguinaldo <> 0  ORDER BY  a1.tiporel);" +
   " CREATE TEMP TABLE tabladias AS(  SELECT  *, ((((((extract(year FROM TIMESTAMP '2020-11-30') - extract(year FROM  fching)) * 12) + extract(MONTH FROM TIMESTAMP '2020-11-30') - extract(MONTH FROM  fching))) * 30) + CASE WHEN((extract(DAY FROM  fching) >= 16)) THEN 15 ELSE 30 END) AS dias  FROM   BASE  ORDER BY  num);" +
   " UPDATE tabladias SET monto = (monto / 30) * d_aguinaldo WHERE DIAS >= 180 AND dias_aguinaldo = 70;            UPDATE tabladias SET monto = (monto / 30) * d_aguinaldo / 180 * dias WHERE  dias <= 179 AND dias_aguinaldo = 70;" +
   " UPDATE tabladias SET monto = (monto / 30) * d_aguinaldo WHERE    dias >= 180 AND dias_aguinaldo = 78;            UPDATE tabladias SET monto = trunc(monto::NUMERIC, 2); SELECT *  FROM  tabladias ORDER BY num;";




            List<Dictionary<string, object>> resultado = globales.consulta(query);

            foreach (var item in resultado)
            {
                string jpp = Convert.ToString(item["jpp"]);
                string num = Convert.ToString(item["num"]);
                string monto = Convert.ToString(item["monto"]);


                string inserta = $"INSERT INTO nominas_catalogos.nominew (jpp,numjpp,clave,secuen,descri,monto,pagon,pagot,tipopago,tipo_nomina) values ('{jpp}',{num},59,1,'AGUINALDO',{monto},0,0,'N','AG');";

                globales.consulta(inserta, true);
                Cursor.Current = Cursors.WaitCursor;



            }

            DialogResult DIALOGO7 = globales.MessageBoxSuccess("PROCESO TERMINADO", "", globales.menuPrincipal);
            Cursor.Current = Cursors.Default;


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        } 
    }
}
