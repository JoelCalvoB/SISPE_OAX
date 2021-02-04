using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.FINANCIEROS.CHEQUES
{
    public partial class frmcierrechq : Form
    {
        string numchq1, entidaddes, subsistemades, poltipodes, polmesdes, polnumerodes, fechades, concepdes, grupodes, prefijodes, elaborodes, statusdes;
        private double debesuma, habersuma = 0;

        string ctacontab, debe_haber, importe, doctipo, referencia, conceptopol;
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogo = globales.MessageBoxExclamation("ESTE PROCESO BORRA TODA LA INFORMACIÓN DE CHEQUES", "CIERRE DE MES", globales.menuPrincipal);
            if (dialogo == DialogResult.Yes)
            {
                DialogResult dialogo1 = globales.MessageBoxQuestion("SEGURO DE HACER EL CIERRE DE MES", "AVISO", globales.menuPrincipal);
                if (dialogo1 == DialogResult.No)
                {
                    this.Close();

                }
                else
                {
                    obtendatos();
                }

            }
            else
            {
                this.Close();

            }

            eliminayrespalda();
            obtienedes();
            obtienepol();
            vaciatablas();
            this.Close();


        }

        public frmcierrechq()
        {
            InitializeComponent();
            DateTime actual = DateTime.Now;
            string solofecha = actual.ToLongDateString();
            string soloanio = string.Format("{0:yy}", solofecha);
            string solomes = string.Format("{0:MM}", solofecha);
        }

        private void vaciatablas()
        {
            string query = "delete from financieros.datoscheques";
            globales.consulta(query);
            query = "delete from financieros.detalle_cheque";
            globales.consulta(query);
        }

        private void eliminayrespalda()
        {
            string query =
            " drop table financieros.chqdatosrespaldo; " +
            "drop table financieros.chqdetallerespaldo;" +
           " SELECT * INTO financieros.chqdatosrespaldo FROM financieros.datoscheques; " +
          "  SELECT* INTO financieros.chqdetallerespaldo FROM financieros.detalle_cheque; " +
          "  delete from financieros.pol;" +
           " delete from financieros.des;"
            ;

            globales.consulta(query);
        }

        private void frmcierrechq_Load(object sender, EventArgs e)
        {
          
        }

        private void obtendatos()
        {
            DateTime actual = DateTime.Now;
            string fecha = string.Format("{0:dd/MM/yyyy}", actual);
            string soloanio = string.Format("{0:yy}", actual);
            string solomes = string.Format("{0:MM}", actual);
        }


        private void obtienedes()
        {
            try
            {
                string query = "select * from financieros.datoscheques order by numcheque";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                foreach (var item in resultado)
                {
                    // aqui empieza
                    string querty = $"select * from financieros.detalle_cheque where numcheque = {item["numcheque"]}";
                    List<Dictionary<string, object>> listado = globales.consulta(querty);

                    if (listado.Count > 0)
                    {
                        debesuma = (double)listado.Where(o => Convert.ToString(o["debe_haber"]) == "D").Sum(o => Convert.ToDouble(o["importe"]));
                        habersuma = (double)listado.Where(o => Convert.ToString(o["debe_haber"]) == "H").Sum(o => Convert.ToDouble(o["importe"]));
                    }

                    string banco = Convert.ToString(item["banco"]);
                    string chequera = Convert.ToString(item["chequera"]);
                    string solo = Convert.ToString(item["fecha"]);
                    fechades = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(solo));
                    string numcheque = Convert.ToString(item["numcheque"]);

                    string benefic = Convert.ToString(item["benefic"]);
                    string impcheque = Convert.ToString(item["impcheque"]);
                    concepdes = Convert.ToString(item["concep1"]);

                    prefijodes = (concepdes == "CANCELADO") ? "X" : "B";

                    int numpoliz = Convert.ToInt32(item["numpoliz"]);
                    polnumerodes = string.Format("{0:0000000}", numpoliz);
                    entidaddes = "801";
                    subsistemades = "P";
                    polmesdes = txtmes.Text;
                    poltipodes = "E";
                    grupodes = "000";
                    elaborodes = "DF";
                    statusdes = "0";

                    
                    string inserta = "insert into financieros.des (entidad,subsistema,polmes,poltipo,polnumero,fecha,debe,haber,concepto,grupo,prefijo,elaboro,status) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')";
                    string convierte = string.Format(inserta, entidaddes.Trim(), subsistemades.Trim(), polmesdes.Trim(), poltipodes.Trim(), polnumerodes.Trim(), fechades.Trim(), debesuma, habersuma, concepdes.Trim(), grupodes.Trim(), prefijodes.Trim(), elaborodes.Trim(), statusdes.Trim());
                    globales.consulta(convierte, true);
                    


                }
                DialogResult dialogo = globales.MessageBoxSuccess("PROCESO TERMINADO CORRECTAMENTE", "CIERRE DE MES", globales.menuPrincipal);
            }
            catch
            {
                DialogResult dialogo = globales.MessageBoxError("CONTACTE A SISTEMAS", "ERROR EN CARGA", globales.menuPrincipal);
            }

        }
        private void obtienepol()
        {
            try
            {
                string query = "select * from financieros.datoscheques order by numcheque";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                foreach (var item in resultado)
                {
                    // aqui empieza
                    string querty = $"select * from financieros.detalle_cheque where numcheque = {item["numcheque"]}";
                    List<Dictionary<string, object>> listado = globales.consulta(querty);
                    foreach (var otroitem in listado)
                    {
                        ctacontab = Convert.ToString(otroitem["ctacontab"]);
                        debe_haber = Convert.ToString(otroitem["debe_haber"]);
                        importe = Convert.ToString(otroitem["importe"]);
                        conceptopol = Convert.ToString(otroitem["concepto"]);
                        referencia = Convert.ToString(otroitem["referencia"]);
                        string cancelado = Convert.ToString(otroitem["cancelado"]);
                        // aqui termina detalle

                        string banco = Convert.ToString(item["banco"]);
                        string chequera = Convert.ToString(item["chequera"]);
                        string solo = Convert.ToString(item["fecha"]);
                        fechades = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(solo));
                        string numcheque = Convert.ToString(item["numcheque"]);
                        string benefic = Convert.ToString(item["benefic"]);
                        string impcheque = Convert.ToString(item["impcheque"]);
                        concepdes = Convert.ToString(item["concep1"]);
                        if (conceptopol == "CANCELADO")
                        {
                            prefijodes = "X";
                        }
                        else
                        {
                            prefijodes = "B";
                        }
                        int numpoliz = Convert.ToInt32(item["numpoliz"]);
                        polnumerodes = string.Format("{0:0000000}", numpoliz);
                        entidaddes = "801";
                        subsistemades = "P";
                        polmesdes = txtmes.Text;
                        poltipodes = "E";
                        grupodes = "00";
                        elaborodes = "DF";
                        statusdes = "0";
                        doctipo = "CH";


                        string inserpol = "insert into financieros.pol (entidad,subsistema,polmes,poltipo,polnumero,fecha,cuenta,naturaleza,importe,grupo,prefijo,doctipo,docnumero,docfecha,doccomentario,status,linea ) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')";
                        string pol = string.Format(inserpol, entidaddes.Trim(), subsistemades.Trim(), polmesdes.Trim(), poltipodes.Trim(), polnumerodes.Trim(), fechades.Trim(), ctacontab.Trim(), debe_haber.Trim(), importe.Trim(), grupodes.Trim(), prefijodes.Trim(), doctipo.Trim(), referencia.Trim(), fechades.Trim(), conceptopol.Trim(), "0", "0");
                        globales.consulta(pol, true);
                    }
                }
                DialogResult dialogo = globales.MessageBoxSuccess("PROCESO TERMINADO CORRECTAMENTE", "CIERRE DE MES", globales.menuPrincipal);
            }
            catch
            {
                DialogResult dialogo = globales.MessageBoxError("CONTACTE A SISTEMAS", "ERROR EN CARGA", globales.menuPrincipal);
            }
        }

    }
}
