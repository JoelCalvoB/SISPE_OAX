using SISPE_MIGRACION.formularios.Fondo_de_Pensiones.DISKETTES.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones.DISKETTES
{
    public partial class frmFondoReportes : Form
    {
        public frmFondoReportes()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = string.Empty;
                if (r1.Checked)
                {
                    if (orden1.Checked)
                        query = "select rfc,nombre_em,proyecto,bb.cuenta as depe,bb.descripcion as dependencia from datos.empleados left join catalogos.dependencias BB on left(proyecto,3)=bb.proy where pendiente order by depe,rfc ;";
                    else
                        query = "select rfc,nombre_em,proyecto,depe,'' as dependencia from datos.empleados where pendiente order by rfc ;";
                }
                else if (r2.Checked)
                {
                    if (orden1.Checked)
                        query = "select rfc,nombre_em,proyecto,bb.cuenta as depe,bb.descripcion as dependencia from datos.empleados left join catalogos.dependencias BB on left(proyecto,3)=bb.proy where not pendiente order by depe,rfc ;";
                    else
                        query = "select rfc,nombre_em,proyecto,depe,'' as dependencia from datos.empleados where not pendiente order by rfc ;";
                }
                else {
                    if (orden1.Checked)
                        query = "select rfc,nombre_em,proyecto,bb.cuenta as depe,bb.descripcion as dependencia from datos.empleados left join catalogos.dependencias BB on left(proyecto,3)=bb.proy order by depe,rfc ;";
                    else
                        query = "select rfc,nombre_em,proyecto,depe,'' as dependencia from datos.empleados order by rfc ;";
                }

                this.Cursor = Cursors.WaitCursor;
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                this.Cursor = Cursors.Default;
                if (resultado.Count == 0) {
                    globales.MessageBoxExclamation("No hay resultados de información para el reporte o generación de archivo DBF","Aviso",globales.menuPrincipal);
                    return;
                }
                globales.showModal(new frmGenerarArchivo(resultado));
               
                
            }
            catch {
               globales.MessageBoxError("Error en el sistema favor de contactar a los de sistemas","Aviso",globales.menuPrincipal);
            }
        }

        private void frmFondoReportes_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button3_Click(null,null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
       
                Owner.Close();
        }
    }
}
