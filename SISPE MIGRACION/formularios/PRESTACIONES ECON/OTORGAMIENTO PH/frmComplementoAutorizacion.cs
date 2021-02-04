using SISPE_MIGRACION.codigo.repositorios.datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH
{
    public partial class frmComplementoAutorizacion : Form
    {
        public frmComplementoAutorizacion()
        {
            InitializeComponent();
        }

        private void frmComplementoAutorizacion_Shown(object sender, EventArgs e)
        {
            DateTime actual = DateTime.Now;
            txtxFecha1.Text = Convert.ToString(actual);
            txtxFecha1.Focus();
        }

        private void txtxFecha1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            metodoConsejo();
        }

        private void metodoConsejo()
        {
            string fecha = globales.parseDateTime(globales.convertDatetime(txtxFecha1.Text));
            txtxFecha1.Text = fecha;
            if (string.IsNullOrWhiteSpace(fecha)) {
                globales.MessageBoxExclamation("Ingresar fecha correcta", "Aviso",globales.menuPrincipal);
                return;
            }

            dbaseORM orm = new dbaseORM();
            string query = $"select * from datos.h_solici where f_autorizacion = '{fecha}'";

            List<h_solici> lista = orm.queryForList<h_solici>(query);
            if (lista.Count != 0)
            {
                foreach (h_solici item in lista) {
                    string sec = item.sec;
                    int folio = item.expediente;
                    h_sconsj consejo = new h_sconsj();

                    query = $"select * from datos.h_sconsj where expediente = {folio} and sec = '{sec}'";
                    consejo = orm.queryForMap<h_sconsj>(query);

                    consejo.proyecto = (item.finalidad == "04" || item.finalidad == "05" || item.finalidad == "06" || item.finalidad == "07") ? "XXXXXXXXXX" : consejo.proyecto;

                    if (sec == "0")
                    {
                        consejo.proyecto = "CONGRUENTE";
                        consejo.necesida = "COMPROBADA";
                        consejo.solvencia = "COMPROBADA";
                    }
                    else {
                        consejo.cred_ant = "COMPROBADO";
                        consejo.avance_o = "COMPROBADO";
                        consejo.necesida = "COMPROBADA";
                        consejo.solvencia = "COMPROBADA";
                    }

                    bool actualizacion = false;

                    if (consejo.expediente == 0)
                    {
                        consejo.expediente = folio;
                        consejo.sec = sec;

                        actualizacion = orm.insert<h_sconsj>(consejo);
                        
                    }
                    else {
                        actualizacion = orm.update<h_sconsj>(consejo);
                    }

                   
                }

                
                    globales.MessageBoxSuccess("Se agregaron datos complementarios P/CONSEJO", "Aviso", globales.menuPrincipal);
            }
            else {
                globales.MessageBoxExclamation($"No se encuentran registros con fecha de autorización {txtxFecha1.Text}","Aviso",globales.menuPrincipal);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmComplementoAutorizacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) this.Owner.Close();
        }
    }
}
