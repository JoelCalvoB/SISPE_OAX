using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CITAS
{
    public partial class frmCitas_web : Form
    {
        int id_depto;
        int id_tramite;
        bool bandera;
        string ValorFecha;
        string idelim;
        public frmCitas_web()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCitas_web_Load(object sender, EventArgs e)
        {
            DateTime actual = DateTime.Now;
            fechapicker.Value = actual;

            string hora_e = "10:00am";
            DateTime hora_entr = Convert.ToDateTime(hora_e);
            hora_inicio.Value = hora_entr;

            string hora_s = "02:00pm";
            DateTime hora_sal = Convert.ToDateTime(hora_s);
            hora_final.Value = hora_sal;


        //    comboLapso.SelectedIndex = 0;
            //  comboTramite.SelectedIndex = 1;


            comboTramite_Click(null,null);
          //  comboTramite.SelectedIndex = 0;

        }

        private void comboTramite_Click(object sender, EventArgs e)
        {
            string query = $"SELECT id_depto FROM catalogos.usuarios where idusuario={globales.id_usuario};";
            List<Dictionary<string, object>> res = globales.consulta(query);
            this.id_depto = Convert.ToInt32(res[0]["id_depto"]);

            string que = $"SELECT * FROM oficialia.tramites where  web=true and id_depto={this.id_depto};";
            List<Dictionary<string, object>> resultado = globales.consulta(que);
            comboTramite.Items.Clear
                ();
            foreach (var item in resultado)
            {
                string nombre = Convert.ToString(item["nombre"]);
                comboTramite.Items.Add(nombre);
            }

        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboLapso.Text) || string.IsNullOrWhiteSpace(comboTramite.Text))
            {
                DialogResult dialogollena = globales.MessageBoxExclamation("No deje campos sin rellenar", "Cuidado", globales.menuPrincipal);
                return;
            }
            DateTime fe = fechapicker.Value;
            string fec = string.Format("{0:dd-MM-yyyy}", fe);
            string query = $"SELECT dia , hora  FROM oficialia.control_citas where dia ='{fec}' and id_depto ='{this.id_depto}'";
            List<Dictionary<string, object>> resta = globales.consulta(query);
            if (resta.Count>=1)
            {
                DialogResult dialogo1 = globales.MessageBoxExclamation("EXISTE YA UNA PROGRAMACIÓN DE ESTA FECHA", "ATENCIÓN", globales.menuPrincipal);

                DialogResult dialogo2 = globales.MessageBoxQuestion("¿DESEA AGREGAR CITAS A LAS YA EXISTENTES EN ESTE DÍA?", "AVISO", globales.menuPrincipal);
                if (dialogo2 == DialogResult.Yes)
                {
                    GenerAgenda();
                    return;
                }
                else
                {
                    return;
                }



                //DialogResult dialogo2 = globales.MessageBoxQuestion("DESEA BORRAR LA PROGRAMACIÓN DEL DÍA SELECCIONADO?", "AVISO", globales.menuPrincipal);
                //if (dialogo2 ==DialogResult.Yes)
                //{
                //    string borra = $"delete FROM oficialia.control_citas where dia ='{fec}' and id_depto ='{this.id_depto}'";
                //    globales.consulta(borra);
                //    DialogResult dialogo4 = globales.MessageBoxSuccess("SE BORRO LA PROGRAMACIÓN", "TERMINADO", globales.menuPrincipal);
                //    dataGridView1.Rows.Clear();
                //    return;
                //}
                //else
                //{
                //    DialogResult dialogo4 = globales.MessageBoxInformation("VISUALIZACIÓN DE LA  PROGRAMACIÓN", "DETALLE", globales.menuPrincipal);
                //    dataGridView1.Rows.Clear();
                //    llenaGrid();
                //    return ;
                //}
            }


            GenerAgenda();
            return;
        }




        public void GenerAgenda()

        {
            string query = string.Empty;
            Dictionary<string, string> res = new Dictionary<string, string>();

            DateTime fecha = fechapicker.Value;
            ValorFecha = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horai = hora_inicio.Value;
            DateTime horaf = hora_final.Value;



            string hi = string.Format("{0:HHmm}", horai);
            string hf = string.Format("{0:HHmm}", horaf);

            DateTime myDate = DateTime.ParseExact(hi, "HHmm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime myDate2 = DateTime.ParseExact(hf, "HHmm", System.Globalization.CultureInfo.InvariantCulture);




            var minutes = (myDate2 - myDate).TotalMinutes;

            double interacion = Convert.ToDouble(minutes) / Convert.ToDouble(comboLapso.Text);
            int contador;
            double it = Convert.ToDouble(comboLapso.Text);
            List<Dictionary<string, object>> resultado = new List<Dictionary<string, object>>();
            for (contador = 0; contador <= interacion; contador++)
            {
                string ValorHora = string.Format("{0:HH:mm}", myDate);

                string validaHora = $"select * from oficialia.control_citas where dia='{ValorFecha}' and hora ='{ValorHora}' AND id_depto ={this.id_depto} and id_tramite={this.id_tramite}";
                List<Dictionary<string, object>> rs = globales.consulta(validaHora);
                if (rs.Count >= 1)
                {
                    DialogResult dialog = globales.MessageBoxExclamation("EXISTEN CITAS DUPLICADAS", "VIOLACIÓN DE INTERVALOS", globales.menuPrincipal);
                    DialogResult dialogo1 = globales.MessageBoxInformation("POR RAZONES DE SEGURIDAD SE ELIMINARÁ EL DÍA CON ERRORES", "AVISO", globales.menuPrincipal);
                    string elimina = $"delete FROM oficialia.control_citas where dia ='{ValorFecha}' and id_depto ={this.id_depto} and id_tramite ={this.id_tramite}";
                    globales.consulta(elimina);
                    dataGridView1.Rows.Clear();
                    return; 
                }

                if (bandera == false)
                {
                    query = $"insert into oficialia.control_citas (dia , hora , status , id_depto , id_tramite ) values ('{ValorFecha}','{ValorHora}' , false , {this.id_depto}  , {id_tramite} );";
                    globales.consulta(query);
                    bandera = true;
                    myDate = myDate.AddMinutes(it);


                }
                else
                {
                    query = $"insert into oficialia.control_citas (dia , hora , status , id_depto , id_tramite) values ('{ValorFecha}','{ValorHora}' , false , {this.id_depto}  , {id_tramite} );";
                    globales.consulta(query);
                    myDate = myDate.AddMinutes(it);

                }


                Dictionary<string, object> diccionario = new Dictionary<string, object>();
                diccionario.Add("hora", myDate);
                resultado.Add(diccionario);
            }

            DialogResult dialogo = globales.MessageBoxSuccess("AGENDA GENERADA", "PROCESO TERMINADO CORRECTAMENTE", globales.menuPrincipal);
            llenaGrid();
        }


        public void llenaGrid()
        {
            dataGridView1.Rows.Clear();
           this.ValorFecha = string.Format("{0:yyyy-MM-dd}", fechapicker.Text);

            string query = $"SELECT dia , hora , a2.nombre , a1.id  FROM oficialia.control_citas a1 inner join oficialia.tramites a2 ON a1.id_tramite =a2.id where dia ='{this.ValorFecha}' and a1.id_depto ='{this.id_depto}' order by hora";
            List<Dictionary<string, object>> res = globales.consulta(query);
            foreach (var item in res)
            {
                string dia = Convert.ToString(item["dia"]).Replace(" 12:00:00 a. m.","");
                string hora = Convert.ToString(item["hora"]);
                string tramite = Convert.ToString(item["nombre"]);
                string id = Convert.ToString(item ["id"]);
                dataGridView1.Rows.Add(dia, hora, tramite,id);
            }
        }

        private void comboTramite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboTramite.Text)) return;
            string query = $"select id  from oficialia.tramites where nombre ='{comboTramite.Text}'";
            List<Dictionary<string, object>> resu = globales.consulta(query);
            try
            {
                this.id_tramite = Convert.ToInt32(resu[0]["id"]);

            }
            catch
            {

        }
    }

        private void frmCitas_web_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.R))
            {
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(comboLapso.Text) && !string.IsNullOrWhiteSpace(comboTramite.Text))
            {
                DateTime fe = fechapicker.Value;
                string fec = string.Format("{0:dd-MM-yyyy}", fe);

                string borra = $"delete FROM oficialia.control_citas where dia ='{fec}' and id_depto ={this.id_depto} and id_tramite ={this.id_tramite} ";
                globales.consulta(borra);
                DialogResult dialogo4 = globales.MessageBoxSuccess($"SE BORRO LA PROGRAMACIÓN DEL DÍA {fec}", "TERMINADO", globales.menuPrincipal);
                dataGridView1.Rows.Clear();
                return;
            }
            else
            {
                DialogResult dialogollena = globales.MessageBoxExclamation("No deje campos sin rellenar", "Cuidado", globales.menuPrincipal);
                return;
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r == -1) return;
            DataGridViewRow row = dataGridView1.Rows[r];
            this.idelim = Convert.ToString(dataGridView1.Rows[r].Cells[3].Value);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Delete)
            {
                if (string.IsNullOrWhiteSpace(this.idelim)) return;
                string borra = $"delete FROM oficialia.control_citas where id={this.idelim} ";
                globales.consulta(borra);
                

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ValorFecha = string.Format("{0:yyyy-MM-dd}", fechapicker.Text);

            string query = $"SELECT dia , hora , a2.nombre , a1.id  ,concat( a1.nombre ,' ',  a1.apellido) as interesado FROM oficialia.control_citas a1 inner join oficialia.tramites a2 ON a1.id_tramite =a2.id where dia ='{this.ValorFecha}' and a1.id_depto ='{this.id_depto}' order by hora";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            object[] aux1 = new object[resultado.Count];
            int contador = 0;
            foreach (var item in resultado)
            {
                string dia = Convert.ToString(item["dia"]).Replace(" 12:00:00 a. m.", "");
                string nombre = Convert.ToString(item["interesado"]);
                string hora = Convert.ToString(item["hora"]);
                string tramite = Convert.ToString(item["nombre"]);

                object[] tt1 = { dia, nombre, tramite, hora };
                aux1[contador] = tt1;
                contador++;

            }

            object[] parametros = { "fecha" };
            object[] valor = { this.ValorFecha };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reporte_citas", "citas", aux1, "", false, enviarParametros);
            this.Cursor = Cursors.Default;

        }

    
}
}
