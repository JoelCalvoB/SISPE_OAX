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
    public partial class frmEtica : Form
    {
        string copias;
        public frmEtica()
        {
            InitializeComponent();
        }

        private void frmEtica_Shown(object sender, EventArgs e)
        {
         string query =  $"select coalesce (max (id) +1, 0) as id from etica.general ";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            string numero = Convert.ToString(resultado[0]["id"]);
            if (numero == "0") numero = "1";
            txtNumero.Text = numero;
            int id_usuario =Convert.ToInt32(globales.id_usuario);
            string query1 = $"select id_depto  from catalogos.usuarios where idusuario = {id_usuario} ";
            List<Dictionary<string, object>> res1 = globales.consulta(query1);
            string depto = Convert.ToString(res1[0]["id_depto"]);
            if (depto == "") return;
            string query2 = $"SELECT nombre FROM oficialia.areas_pensiones where id ={depto} ;";
            List<Dictionary<string, object>> res2 = globales.consulta(query2);
            string nombre_depto = Convert.ToString(res2[0]["nombre"]);
            txtDepto.Text = nombre_depto;

            string busca = "select nombre from oficialia.areas_pensiones order by nombre ";
            List<Dictionary<string, object>> resultados = globales.consulta(busca);
            foreach (var item in resultados)
            {
                string nombre = Convert.ToString(item["nombre"]);

                ListDependencias.Items.Add(nombre);

            }

        }

        private void btnP_hipote_Click(object sender, EventArgs e)
        {


            string ficherosSeleccionados = "";


            if (ListDependencias.CheckedItems.Count != 0)
            {
                //recorremos todos los elementos activados
                //CheckedItems sólo devuelve los elementos activados/chequeados
                for (int i = 0; i <= ListDependencias.CheckedItems.Count - 1; i++)
                {
                    if (ficherosSeleccionados != "")
                    {
                        ficherosSeleccionados =
                             ficherosSeleccionados + "," + ListDependencias.CheckedItems[i].ToString();
                    }
                    else
                    {
                        ficherosSeleccionados =
                             ListDependencias.CheckedItems[i].ToString();
                    }
                }
                this.copias = ficherosSeleccionados + "," + txtDepto.Text;
            }

            string uqery = $"insert into etica.general (responsable , departamento , feCha_termino , desc_compromiso , usuario_captura , fecha , porcentaje_de_avance , areas_involucradas ) values ('{txtResponsable.Text}' , '{txtDepto.Text}' , '{txtfecha.Text}' , '{txtdesc.Text}' , {globales.id_usuario} , current_date , '0%' ,' {this.copias}')";
            globales.consulta(uqery);
            DialogResult dialogo3 = globales.MessageBoxSuccess("SE GUARDO CORRECTAMENTE EL FOLIO", "TERMINADO", globales.menuPrincipal);
            this.Close();

        }



        private void limpia()
        {
            txtDepto.Clear();
            txtdesc.Clear();
            txtfecha.Clear();
            txtNumero.Text = "-";
            txtResponsable.Text = "";

            frmEtica_Shown(null, null);

        }
    }
}
