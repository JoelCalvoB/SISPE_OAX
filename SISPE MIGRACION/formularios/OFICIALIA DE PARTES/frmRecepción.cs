using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.OFICIALIA_DE_PARTES
{   //jioe
    public partial class frmRecepción : Form
    {
        string id_depto;
        int c;
        int r;
        string id_deptoselect;

        public frmRecepción()
        {
            InitializeComponent();
        }

        private void frmRecepción_Shown(object sender, EventArgs e)
        {
            string id_user = globales.id_usuario;
            string trae = $"select id_depto from catalogos.usuarios where idusuario={id_user}";
            List<Dictionary<string, object>> resultado = globales.consulta(trae);
            if (resultado.Count <= 0)
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO TE ENCUENTRAS ENLAZADO A ESTE MÓDULO, PIDA A SISTEMAS AJUSTAR SU PERFIL", "AVISO", globales.menuPrincipal);
            }
            else
            {
                this.id_depto = Convert.ToString(resultado[0]["id_depto"]);
            }

         if (id_depto== "1")
            {
                //  comboDepto.Enabled = true;
                comboDepto.Enabled = false;


            }
            else
            {
                comboDepto.Enabled = false;
            }
         try
            {
                string quer = $"select nombre from oficialia.areas_pensiones where id={this.id_depto} ";
                List<Dictionary<string, object>> resu = globales.consulta(quer);
                foreach (var item in resu)
                {
                    comboDepto.Text = Convert.ToString(item["nombre"]);
                }

            }
            catch
            {
                comboDepto_Enter(null, null);
            }

            llenaDataGrid();
        }


        private void llenaDataGrid()
        {
            dataDocumentos.Rows.Clear();
            string query = $"SELECT a1.folio,a1.dependencia_remitente,a1.nombre_remitente,a1.fecha_limite,a1.fecha_captura,a1.tipo_documento,a2.nombre,a1.asunto,a1.captura,a1.estatus,a1.ruta_archivo,a1.prioridad FROM oficialia.detalle_recepcion_oficio a1 INNER JOIN  oficialia.tramites a2 ON a1.tipo_documento = a2.id where a1.area_destinatario={this.id_depto} OR copias like '%{comboDepto.Text}%'";
            List<Dictionary<string, object>> resultados = globales.consulta(query);
            foreach (var item in resultados)
            {
                string folio = Convert.ToString(item["folio"]);
                string descripcion = Convert.ToString(item["nombre"]);
                string asunto = Convert.ToString(item["asunto"]);
                string captura = Convert.ToString(item["captura"]);
                string estatus = Convert.ToString(item["estatus"]);
                string fecha_limite = Convert.ToString(item["fecha_limite"]).Replace("12:00:00 a. m.", "");
                string fecha_captura = Convert.ToString(item["fecha_captura"]).Replace("12:00:00 a. m.", "");
                DateTime fecha = Convert.ToDateTime(item["fecha_captura"]);
                string anio = Convert.ToString( fecha.Year );
                string area_remitente = Convert.ToString(item["dependencia_remitente"]);
                string nombre_remitente = Convert.ToString(item["nombre_remitente"]);
                string prioridad = Convert.ToString(item["prioridad"]);


                int fila = dataDocumentos.Rows.Add(folio + "/"+anio , descripcion, fecha_captura, fecha_limite, area_remitente, asunto, estatus);

                rellenaColor(prioridad, fila, estatus);
            }
        }



        public void rellenaColor(string prioridad , int fila , string estatus)
        {
            if (prioridad == "0")
            {
                dataDocumentos.Rows[fila].Cells[7].Style.BackColor = Color.FromArgb(255, 0, 0);
            }
            else if (prioridad == "1")
            {
                dataDocumentos.Rows[fila].Cells[7].Style.BackColor = Color.FromArgb(255, 216, 0);
            }
            else
            {
                dataDocumentos.Rows[fila].Cells[7].Style.BackColor = Color.FromArgb(6, 210, 102);
            }


            if (estatus.ToLower().Contains("cerrado"))
            {
                dataDocumentos.Rows[fila].Cells[0].Style.BackColor = Color.FromArgb(210, 6, 6);
                dataDocumentos.Rows[fila].Cells[0].Style.ForeColor = Color.White;
                dataDocumentos.Rows[fila].Cells[1].Style.BackColor = Color.FromArgb(210, 6, 6);
                dataDocumentos.Rows[fila].Cells[1].Style.ForeColor = Color.White;
                dataDocumentos.Rows[fila].Cells[2].Style.BackColor = Color.FromArgb(210, 6, 6);
                dataDocumentos.Rows[fila].Cells[2].Style.ForeColor = Color.White;
                dataDocumentos.Rows[fila].Cells[3].Style.BackColor = Color.FromArgb(210, 6, 6);
                dataDocumentos.Rows[fila].Cells[3].Style.ForeColor = Color.White;
                dataDocumentos.Rows[fila].Cells[4].Style.BackColor = Color.FromArgb(210, 6, 6);
                dataDocumentos.Rows[fila].Cells[4].Style.ForeColor = Color.White;
                dataDocumentos.Rows[fila].Cells[5].Style.BackColor = Color.FromArgb(210, 6, 6);
                dataDocumentos.Rows[fila].Cells[5].Style.ForeColor = Color.White;
                dataDocumentos.Rows[fila].Cells[6].Style.BackColor = Color.FromArgb(210, 6, 6);
                dataDocumentos.Rows[fila].Cells[6].Style.ForeColor = Color.White;
                dataDocumentos.Rows[fila].Cells[7].Style.BackColor = Color.FromArgb(210, 6, 6);
                dataDocumentos.Rows[fila].Cells[7].Style.ForeColor = Color.White;

            }


        }

        private void dataDocumentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            string status = Convert.ToString(dataDocumentos.Rows[r].Cells[6].Value);

            if (status.ToLower().Contains("cerrado")) {
                globales.MessageBoxExclamation("Documento ya se encuentra cerrado\nSolo esta disponible para visualizar","Aviso",globales.menuPrincipal);
            }

            string id_folio = Convert.ToString(dataDocumentos.Rows[r].Cells[0].Value);
            if (string.IsNullOrWhiteSpace(id_folio)) return;
            string depto = comboDepto.Text;
            string id_tramite = Convert.ToString(dataDocumentos.Rows[r].Cells[1].Value);
            string fecha_limite = Convert.ToString(dataDocumentos.Rows[r].Cells[3].Value);


            frmOpcionOficio oficio = new frmOpcionOficio(id_folio, id_tramite, depto, fecha_limite, status.ToLower().Contains("cerrado"));
            globales.showModal(oficio);

        }

        private void dataDocumentos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;
            c = e.ColumnIndex;
            if (c == -1) return;
        }

        private void comboDepto_Enter(object sender, EventArgs e)
        {
            comboDepto.Items.Clear();
            string query = $"select nombre from oficialia.areas_pensiones";
            List<Dictionary<string, object>> rs = globales.consulta(query);
            foreach(var item in rs)
            {
                comboDepto.Items.Add(item["nombre"]);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFolio.Text)) return;
                string query = $"SELECT a1.folio,a1.dependencia_remitente,a1.nombre_remitente,a1.fecha_limite,a1.fecha_captura,a1.tipo_documento,a2.nombre,a1.asunto,a1.captura,a1.estatus,a1.ruta_archivo,a1.prioridad FROM oficialia.detalle_recepcion_oficio a1 INNER JOIN  oficialia.tramites a2 ON a1.tipo_documento = a2.id where a1.folio={txtFolio.Text} AND copias like '%{comboDepto.Text}%'";

                List<Dictionary<string, object>> result = globales.consulta(query);
                dataDocumentos.Rows.Clear();

                foreach (var item in result)
                {
                    string folio = Convert.ToString(item["folio"]);
                    string descripcion = Convert.ToString(item["nombre"]);
                    string asunto = Convert.ToString(item["asunto"]);
                    string captura = Convert.ToString(item["captura"]);
                    string estatus = Convert.ToString(item["estatus"]);
                    string fecha_limite = Convert.ToString(item["fecha_limite"]).Replace("12:00:00 a. m.", "");
                    string fecha_captura = Convert.ToString(item["fecha_captura"]).Replace("12:00:00 a. m.", "");
                    string area_remitente = Convert.ToString(item["dependencia_remitente"]);
                    string nombre_remitente = Convert.ToString(item["nombre_remitente"]);
                    string prioridad = Convert.ToString(item["prioridad"]);



               int fila =      dataDocumentos.Rows.Add(folio, descripcion, fecha_captura, fecha_limite, area_remitente,asunto, estatus);
                    rellenaColor(prioridad, fila, estatus);

                }
            }
           catch
            {
                llenaDataGrid();
            } 
          
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
        

        }

        private void comboDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string busca = $"select id from oficialia.areas_pensiones where nombre='{comboDepto.Text}' ";
            List<Dictionary<string,object>> refs= globales.consulta(busca);
            this.id_deptoselect = Convert.ToString(refs[0]["id"]);



            dataDocumentos.Rows.Clear();
            string query = $"SELECT a1.folio,a1.dependencia_remitente,a1.nombre_remitente,a1.fecha_limite,a1.fecha_captura,a1.tipo_documento,a2.nombre,a1.asunto,a1.captura,a1.estatus,a1.ruta_archivo, a1.prioridad FROM oficialia.detalle_recepcion_oficio a1 INNER JOIN  oficialia.tramites a2 ON a1.tipo_documento = a2.id where a1.area_destinatario={id_deptoselect} OR copias like '%{comboDepto.Text}%'";
            List<Dictionary<string, object>> resultados = globales.consulta(query);
            foreach (var item in resultados)
            {
                string folio = Convert.ToString(item["folio"]);
                string descripcion = Convert.ToString(item["nombre"]);
                string asunto = Convert.ToString(item["asunto"]);
                string captura = Convert.ToString(item["captura"]);
                string estatus = Convert.ToString(item["estatus"]);
                string fecha_limite = Convert.ToString(item["fecha_limite"]).Replace("12:00:00 a. m.", "");
                string fecha_captura = Convert.ToString(item["fecha_captura"]).Replace("12:00:00 a. m.", "");
                string area_remitente = Convert.ToString(item["dependencia_remitente"]);
                string nombre_remitente = Convert.ToString(item["nombre_remitente"]);
                string prioridad = Convert.ToString(item["priordad"]);


              int fila =   dataDocumentos.Rows.Add(folio, descripcion, fecha_captura, fecha_limite, area_remitente,asunto, estatus);

                rellenaColor(prioridad, fila, estatus);

            }

        }

        private void comboStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void comboDepto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void comboStatus_SelectedValueChanged(object sender, EventArgs e)
        {

            string status = string.Empty;
            if (comboStatus.SelectedIndex == 0)
                status = "INICIAL";
            if (comboStatus.SelectedIndex == 1)
                status = "RESPUESTA";
            if (comboStatus.SelectedIndex == 2)
                status = "CERRADO";

            dataDocumentos.Rows.Clear();
            string query = $"SELECT a1.folio,a1.dependencia_remitente,a1.nombre_remitente,a1.fecha_limite,a1.fecha_captura,a1.tipo_documento,a2.nombre,a1.asunto,a1.captura,a1.estatus,a1.ruta_archivo, a1.prioridad FROM oficialia.detalle_recepcion_oficio a1 INNER JOIN  oficialia.tramites a2 ON a1.tipo_documento = a2.id where estatus='{status}' AND copias like '%{comboDepto.Text}%'";
            List<Dictionary<string, object>> resultados = globales.consulta(query);
            foreach (var item in resultados)
            {
                string folio = Convert.ToString(item["folio"]);
                string descripcion = Convert.ToString(item["nombre"]);
                string asunto = Convert.ToString(item["asunto"]);
                string captura = Convert.ToString(item["captura"]);
                string estatus = Convert.ToString(item["estatus"]);
                string fecha_limite = Convert.ToString(item["fecha_limite"]).Replace("12:00:00 a. m.", "");
                string fecha_captura = Convert.ToString(item["fecha_captura"]).Replace("12:00:00 a. m.", "");
                string area_remitente = Convert.ToString(item["dependencia_remitente"]);
                string nombre_remitente = Convert.ToString(item["nombre_remitente"]);
                string prioridad = Convert.ToString(item["prioridad"]);



              int fila =   dataDocumentos.Rows.Add(folio, descripcion, fecha_captura, fecha_limite, area_remitente,asunto, estatus);

                rellenaColor(prioridad, fila, estatus);

            }

        }

        private void comboOrdenado_SelectedIndexChanged(object sender, EventArgs e)
        {
            // tipo
            // folio
            //status
            string orden = string.Empty;
            if (comboOrdenado.SelectedIndex == 0)
                orden = "tipo_documento";
            if (comboOrdenado.SelectedIndex == 1)
                orden = "folio";
            if (comboOrdenado.SelectedIndex == 2)
                orden = "estatus";
            if (comboOrdenado.SelectedIndex == 3)
                orden = "fecha_limite";


            //jo

            dataDocumentos.Rows.Clear();
            string query = $"SELECT a1.folio,a1.dependencia_remitente,a1.nombre_remitente,a1.fecha_limite,a1.fecha_captura,a1.tipo_documento,a2.nombre,a1.asunto,a1.captura,a1.estatus,a1.ruta_archivo, a1.prioridad FROM oficialia.detalle_recepcion_oficio a1 INNER JOIN  oficialia.tramites a2 ON a1.tipo_documento = a2.id where a1.area_destinatario={this.id_depto} OR copias like '%{comboDepto.Text}%' order by {orden}";
            List<Dictionary<string, object>> resultados = globales.consulta(query);
            foreach (var item in resultados)
            {
                string folio = Convert.ToString(item["folio"]);
                string descripcion = Convert.ToString(item["nombre"]);
                string asunto = Convert.ToString(item["asunto"]);
                string captura = Convert.ToString(item["captura"]);
                string estatus = Convert.ToString(item["estatus"]);
                string fecha_limite = Convert.ToString(item["fecha_limite"]).Replace("12:00:00 a. m.", "");
                string fecha_captura = Convert.ToString(item["fecha_captura"]).Replace("12:00:00 a. m.", "");
                string area_remitente = Convert.ToString(item["dependencia_remitente"]);
                string nombre_remitente = Convert.ToString(item["nombre_remitente"]);
                string prioridad = Convert.ToString(item["prioridad"]);



               int fila = dataDocumentos.Rows.Add(folio, descripcion, fecha_captura, fecha_limite, area_remitente, asunto, estatus);
                rellenaColor(prioridad, fila, estatus);

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nivel = string.Empty;
            if (comboNivel.SelectedIndex == 0)
                nivel = "0";
            if (comboNivel.SelectedIndex == 1)
                nivel = "1";
            if (comboNivel.SelectedIndex == 2)
                nivel = "2";

            dataDocumentos.Rows.Clear();
            string query = $"SELECT a1.folio,a1.dependencia_remitente,a1.nombre_remitente,a1.fecha_limite,a1.fecha_captura,a1.tipo_documento,a2.nombre,a1.asunto,a1.captura,a1.estatus,a1.ruta_archivo,a1.prioridad FROM oficialia.detalle_recepcion_oficio a1 INNER JOIN  oficialia.tramites a2 ON a1.tipo_documento = a2.id where prioridad={nivel} and copias like '%{comboDepto.Text}%'";
            List<Dictionary<string, object>> resultados = globales.consulta(query);
            foreach (var item in resultados)
            {
                string folio = Convert.ToString(item["folio"]);
                string descripcion = Convert.ToString(item["nombre"]);
                string asunto = Convert.ToString(item["asunto"]);
                string captura = Convert.ToString(item["captura"]);
                string estatus = Convert.ToString(item["estatus"]);
                string fecha_limite = Convert.ToString(item["fecha_limite"]).Replace("12:00:00 a. m.", "");
                string fecha_captura = Convert.ToString(item["fecha_captura"]).Replace("12:00:00 a. m.", "");
                string area_remitente = Convert.ToString(item["dependencia_remitente"]);
                string nombre_remitente = Convert.ToString(item["nombre_remitente"]);
                string prioridad = Convert.ToString(item["prioridad"]);



               int fila =  dataDocumentos.Rows.Add(folio, descripcion, fecha_captura, fecha_limite, area_remitente, asunto, estatus);

                rellenaColor(prioridad, fila, estatus);

            }
        }

        private void frmRecepción_Load(object sender, EventArgs e)
        {

        }

        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                textBox1_Leave(null, null);

            }
            return;
        }

     
    }
}
