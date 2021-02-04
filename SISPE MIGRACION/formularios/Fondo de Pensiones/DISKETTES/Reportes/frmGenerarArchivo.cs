using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones.DISKETTES.Reportes
{
    public partial class frmGenerarArchivo : Form
    {
        private List<Dictionary<string, object>> resultado;
        public frmGenerarArchivo(List<Dictionary<string,object>> obj)
        {
            InitializeComponent();
            this.resultado = obj;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.rd1.Checked)
            {
                //Se genera el reporte
                int contador = 0;
                object[] obj = new object[resultado.Count];
                foreach (Dictionary<string, object> item in resultado) {
                    object[] tt1 = { item["rfc"], item["nombre_em"], item["proyecto"], item["depe"], item["dependencia"] };
                    obj[contador] = tt1;
                    contador++;
                }


                string logo = globales.getImagen(globales.imagenesSispe.logoreportes);

                object[][] parametros = new object[2][];

                object[] header = {"logo" };
                object[] body = { logo};


                parametros[0] = header;
                parametros[1] = body;



                globales.reportes("frmReporteDisketteFondoPensiones","p_quirog",obj,"",false, parametros);

            }
            else {
                //Se genera el archivo dbf
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }
    }
}
