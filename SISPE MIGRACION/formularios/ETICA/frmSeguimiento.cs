using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.ETICA
{
    public partial class frmSeguimiento : Form
    {

        int porcen;
        public frmSeguimiento()
        {
            InitializeComponent();
        }

        private void btnBuscarRfc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCompromiso.Text)) return;
            string query = $"select * from etica.general where id={txtCompromiso.Text}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count <=0)
            {
                DialogResult dialogo2 = globales.MessageBoxError("NO EXISTE EL FOLIO INGRESADO", "VERIFICAR", globales.menuPrincipal);
            }
            foreach(var item in resultado)
            {
                txtResponsable.Text = Convert.ToString(item["responsable"]);
                txtDepto.Text = Convert.ToString(item["departamento"]);
                string areas_involucradas = Convert.ToString(item["areas_involucradas"]);
                txtfecha.Text = Convert.ToString(item["fecha_termino"]);
                txtPorcentaje.Text = Convert.ToString(item["porcentaje_de_avance"]);
                 string variabl = Convert.ToString(item["porcentaje_de_avance"]).Replace("%", "");
                this.porcen = Convert.ToInt32(variabl);
                    
                txtDesc.Text = Convert.ToString(item["desc_compromiso"]);




                string busca = "select nombre from oficialia.areas_pensiones ";
                List<Dictionary<string, object>> resultados1 = globales.consulta(busca);

                string auxnombre = string.Empty;
                auxnombre = areas_involucradas;

                checkhlistCopias.Items.Clear();
                foreach (var item1 in resultados1)
                {


                    string nombre1 = Convert.ToString(item1["nombre"]);

                    if (auxnombre.Contains(nombre1))
                    {
                        int posicion = checkhlistCopias.Items.Add(nombre1);
                        checkhlistCopias.SetItemChecked(posicion, true);
                    }
                    else
                    {
                        int posicion = checkhlistCopias.Items.Add(nombre1);
                    }

                }

            }
        }

        private void btnP_hipote_Click(object sender, EventArgs e)
        {

            string query= $"update etica.general set  porcentaje_de_avance ='{txtPorcentaje.Text}' , desc_compromiso = '{txtDesc.Text}'  where id ={txtCompromiso.Text} ";
            string varaible2 = txtPorcentaje.Text.Replace("%", "");

            int compara = Convert.ToInt32(varaible2);
            if (compara <=this.porcen)
            {
                DialogResult dialogo1 = globales.MessageBoxExclamation("NO PUEDES INGRESAR UN PORCENTAJE MENOR AL YA ESTABLECIDO ANTERIORMENTE", "MENSAJE", globales.menuPrincipal);
                return;
            }
            globales.consulta(query);
            DialogResult dialogo = globales.MessageBoxSuccess("Cambios guardados correctamente", "Fin", globales.menuPrincipal);
            this.Close();
        }
    }
}
