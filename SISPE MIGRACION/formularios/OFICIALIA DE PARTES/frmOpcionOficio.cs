using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.OFICIALIA_DE_PARTES
{
    public partial class frmOpcionOficio : Form
    {   //jo
        string id_depto;
        string folio;
        string id_tramite;
        string depto;
        string fecha_limite;
        Dictionary<string, object> diccionario;
        List<Dictionary<string, object>> lista = new List<Dictionary<string, object>>();
        string[] separa;
        int c;
        int r;

        private bool estaCerrado { get; set; }
        public frmOpcionOficio(string id_folio, string id_tramite, string depto, string fecha_limite,bool estaCerrado = false)
        {
            InitializeComponent();
            this.estaCerrado = estaCerrado;
            this.folio = id_folio;
            this.id_tramite = id_tramite;
            string id_user = globales.id_usuario;
            this.fecha_limite = fecha_limite;
            this.depto = depto;
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

        }

        private void frmOpcionOficio_Shown(object sender, EventArgs e)
        {
            combDepto.Text = this.depto;
            txtFolio.Text = this.folio;
            txtTipoDoc.Text = this.id_tramite;
            maskeFechalim.Text = this.fecha_limite;

            if (this.estaCerrado) {
                this.btnCarga.Visible = false;
                this.btnCarga.Enabled = false;

                txtFec_respuesta.Enabled = false;
                txtResponde.Enabled = false;
                txtAsuntoR.Enabled = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                checkhlistCopias.Visible = true;
                btnNotif.Visible = true;
                rellenalistCopia();
            }
            else
            {
                checkhlistCopias.Visible = false;
                btnNotif.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbResponder.Checked)
            {
                label9.Visible = true;
                txtAsuntoR.Visible = true;
                label5.Visible = true;
                txtFec_respuesta.Visible = true;
                label6.Visible = true;
                txtResponde.Visible = true;
                btnCarga.Visible = true;
                checkTermino.Visible = true;
            }
            else
            {
                label9.Visible = false;
                txtAsuntoR.Visible = false;
                label5.Visible = false;
                txtFec_respuesta.Visible = false;
                label6.Visible = false;
                txtResponde.Visible = false;
                btnCarga.Visible = false;
                checkTermino.Visible = false;

            }


        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHistorial.Checked)
            {
                dataOficios.Visible = true;
                llenaDtaoficios();
            }
            else
            {
                dataOficios.Visible = false;

            }

        }

        private void rellenalistCopia()
        {
            string input = this.folio;
            int index = input.IndexOf("/");
            if (index > 0)
                input = input.Substring(0, index);

            string query = $"select copias from oficialia.detalle_recepcion_oficio where folio={input}";
            List<Dictionary<string, object>> resulta = globales.consulta(query);

            string auxnombre = string.Empty;
            if (resulta.Count != 0)
            {
                auxnombre = Convert.ToString(resulta[0]["copias"]);
            }


            string busca = "select nombre from oficialia.areas_pensiones ";
            List<Dictionary<string, object>> resultados = globales.consulta(busca);

            checkhlistCopias.Items.Clear();
            foreach (var item in resultados)
            {


                string nombre1 = Convert.ToString(item["nombre"]);

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

        private void btnCarga_Click(object sender, EventArgs e)
        {
            string query = $"SELECT max(folio_respuesta)+1 as folio FROM oficialia.historial  where respuesta=TRUE and folio={this.folio}; ";
            List<Dictionary<string, object>> refe = globales.consulta(query);
            string folioR = string.Empty;
            string nombre_arch = string.Empty;
            foreach (var it in refe)
            {
                folioR = Convert.ToString(it["folio"]);
            }
            if (string.IsNullOrWhiteSpace(folioR)) folioR = "1";
            if (DialogResult.No == globales.MessageBoxQuestion("¿Quiere enviar la respuesta?", "Aviso", this)) { return; }
            globales.MessageBoxExclamation("Se enviara la respuesta a los departamentos involucrados", "Aviso", this);
            if (DialogResult.Yes == globales.MessageBoxQuestion("¿Quiere subir algun archivo escaneado para su respectiva descarga en los demas departamentos?", "Aviso", this))
            {
                DialogResult p = open2.ShowDialog();
                if (p == DialogResult.OK)
                {
                    string ruta = open2.FileName;
                    string nomArch = Path.GetFileName(ruta);
                    string server = @"\\192.168.100.103\pensiones\2020\RESPUESTAS";

                    Random r = new Random();
                    string letras = string.Empty;
                    for (int x = 0; x <= 3; x++)
                    {
                        int numero = r.Next(65, 90);
                        char letra = (char)numero;
                        letras += letra;
                    }

                    try
                    {
                        //  File.Copy(ruta, Path.Combine(server, (folioR) + "R" + letras + ".pdf"));
                        byte[] arrabytes = File.ReadAllBytes(ruta);
                        string base64 = Convert.ToBase64String(arrabytes);
                        nombre_arch = (folioR) + "R" + letras;

                        query = $"insert into oficios values ('{nombre_arch}','{base64}','RESPUESTAS',now())";
                        globales.consulta2(query);

                    }
                    catch
                    {
                        DialogResult dialogo = globales.MessageBoxExclamation("YA EXISTE UN ARCHIVO RELACIONADO A ESTE FOLIO", "AVISO", globales.menuPrincipal);
                        return;
                    }
                    globales.MessageBoxSuccess("DOCUMENTO CARGADO CON ÉXITO AL SERVER Y SE GUARADON LOS DATOS", "AVISO", globales.menuPrincipal);

                    // nombre_arch = server + @"\" + (folioR) + "R" + letras + ".pdf";

                    // nombre_arch = (folioR) + "R" + letras;
                }
            }

            string estado = string.Empty;
            if (checkTermino.Checked)
            {
                estado = "CERRADO";
            }
            else
            {
                estado = "RESPUESTA";
            }

            string input = this.folio;
            int index = input.IndexOf("/");
            if (index > 0)
                input = input.Substring(0, index);

            string insertaR = $"insert into oficialia.historial (folio,tramite,evidencia_ruta,tipo,responsable,respuesta,folio_respuesta,fecha , detalle ) values ({input},'{this.id_tramite}','{nombre_arch}','{estado}','{txtResponde.Text}','true',{folioR},current_date , '{txtAsuntoR.Text}')";



            string actualiza = $"update  oficialia.detalle_recepcion_oficio set estatus='{estado}' where folio={this.folio};";
            try
            {
                globales.consulta(insertaR);
                globales.consulta(actualiza);
                DialogResult DIALOGO = globales.MessageBoxSuccess("SE REGISTRO CORRECTAMENTE", "AVISO", globales.menuPrincipal);
                limpiaRespuesta();


            }
            catch
            {
                DialogResult dailogo = globales.MessageBoxError("OCURRIO UN ERROR AL GUARDAR LA INFORMACIÓN", "AVISO", globales.menuPrincipal);

            }
        }

        private void btnNotif_Click(object sender, EventArgs e)
        {

            string ficherosSeleccionados = string.Empty;

            if (checkhlistCopias.CheckedItems.Count != 0)
            {
                //recorremos todos los elementos activados
                //CheckedItems sólo devuelve los elementos activados/chequeados
                for (int i = 0; i <= checkhlistCopias.CheckedItems.Count - 1; i++)
                {
                    if (ficherosSeleccionados != "")
                    {
                        ficherosSeleccionados =
                             ficherosSeleccionados + "," + checkhlistCopias.CheckedItems[i].ToString() + ",";
                    }
                    else
                    {
                        ficherosSeleccionados =
                             checkhlistCopias.CheckedItems[i].ToString();
                    }
                }
            }

            string query = $"update oficialia.detalle_recepcion_oficio set copias='{ficherosSeleccionados}' where folio={txtFolio.Text}";
            globales.consulta(query, true);
            DialogResult dialogo = globales.MessageBoxSuccess("SE ACTUALIZO CORRECTAMENTE", "AVISO", globales.menuPrincipal);

        }


        private void llenaDtaoficios()

        {
            DateTime año = DateTime.Now;
            string añito = Convert.ToString(año.Year);

            string input = this.folio;
            int index = input.IndexOf("/");
            if (index > 0)
                input = input.Substring(0, index);

            string quey = $"SELECT * FROM oficialia.historial where folio={input} and EXTRACT(year from  fecha)='{añito}' order by id asc ;;";
            List<Dictionary<string, object>> res = globales.consulta(quey);
            if (res.Count <= 0) return;
            string folioP = string.Empty;
            dataOficios.Rows.Clear();
            foreach (var item in res)
            {
                folioP = Convert.ToString(item["folio_respuesta"]);

                if (!string.IsNullOrWhiteSpace(folioP))
                {
                    folioP = Convert.ToString(item["folio_respuesta"]) + "R";

                }

                if (string.IsNullOrWhiteSpace(folioP))
                {
                    folioP = Convert.ToString(item["folio"]);

                }
                string fecha = Convert.ToString(item["fecha"]).Replace("12:00:00 a. m.", "");
                string tipo = Convert.ToString(item["tipo"]);
                string responsable = Convert.ToString(item["responsable"]);
                string detalle = Convert.ToString(item["detalle"]);
                string id = Convert.ToString(item["id"]);

                dataOficios.Rows.Add(folioP, fecha, tipo, responsable, detalle, id);


            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void dataOficios_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;
            c = e.ColumnIndex;
            if (c == -1) return;
        }

        private void dataOficios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = Convert.ToString(dataOficios.Rows[this.r].Cells[5].Value);
            if (string.IsNullOrWhiteSpace(id)) return;

            if (DialogResult.No == globales.MessageBoxQuestion("¿Desea descargar el archivo?", "Aviso", this)) return;

            this.Cursor = Cursors.WaitCursor;
            string query = $"select evidencia_ruta as ruta from oficialia.historial where id={id}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            string ruta = Convert.ToString(resultado[0]["ruta"]);
            this.Cursor = Cursors.Default;
            if (string.IsNullOrWhiteSpace(ruta))
            {
                DialogResult dialogo = globales.MessageBoxExclamation("NO EXISTE UN ARCHIVO CARGADO ASOCIADO A ESTE MOVIMIENTO", "VALIDAR", globales.menuPrincipal);
                return;
            }

            

        repetir:

            SaveFileDialog guarda = new SaveFileDialog();
            guarda.DefaultExt = ".pdf";
            DialogResult resultad1 = guarda.ShowDialog();
            if (resultad1 == DialogResult.OK)
            {
                
                try {
                    string rutaaux = guarda.FileName;
                    Cursor = Cursors.WaitCursor;
                    query = $"select * from oficios where clave = '{ruta}'";
                    resultado = globales.consulta2(query);

                    if (resultado.Count != 0)
                    {
                        string base64 = Convert.ToString(resultado[0]["archivo"]);
                        byte[] bytes = Convert.FromBase64String(base64);
                        File.WriteAllBytes(rutaaux, bytes);
                    }
                    Cursor = Cursors.Default;
                    globales.MessageBoxSuccess("Archivo descargado correctamente","Aviso",this);
                }
                catch (Exception exception ) {
                    Cursor = Cursors.Default;
                    globales.MessageBoxError("Error al descargar el archivo","Aviso",this);
                    DialogResult resu = globales.MessageBoxQuestion("¿Deseas volver a internarlo","Aviso",this);
                    if (resu == DialogResult.Yes) {
                        goto repetir;
                    }
                }
            }
            
        }

        private void limpiaRespuesta()
        {
            txtAsuntoR.Text = "";
            txtFec_respuesta.Clear();
            txtResponde.Text = "";
            checkTermino.Checked = false;
        }

        private void frmOpcionOficio_Load(object sender, EventArgs e)
        {
            txtFec_respuesta.Text = string.Format("{0:d}",DateTime.Now);
            btnNotif.Visible = false;
        }
    }
}
