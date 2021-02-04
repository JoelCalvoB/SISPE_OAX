using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios.CATÁLOGOS;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.DOCUMENTOS;
using SISPE_MIGRACION.formularios.PRESTACIONES_ECON.OTORGAMIENTO_PH.PRINCIPAL_PH;
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
    public partial class frmdocusolic : Form
    {


        private bool teclaEnter;
        private int row;
        private int column;
        private bool esInsertar;
        private bool editadoprogramadamente;
        private int secuencia;
        private string expediente;
        private bool consolicitud;

        public frmdocusolic()
        {
            InitializeComponent();




        }





        private void llenacampos(Dictionary<string, object> datos)
        {


            string folio = Convert.ToString(datos["folio"]);
            frmAmpliacion ampliacion = new frmAmpliacion("Documentación P/Solicitud", folio, datos, "Solicitud inicial", "1° Ampliación", "2° Ampliación", "3° Ampliación");
            ampliacion.enviar = recibiendoampliacion;
            globales.showModal(ampliacion);
        }







        private void frmdocusolic_Load(object sender, EventArgs e)
        {




        }

        private void griddocument_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void frmdocusolic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                cerrar();
            }
            if (griddocument.Rows.Count != 0) return;
            if (e.KeyCode == Keys.Insert)
            {
                if (!this.consolicitud) {
                    globales.MessageBoxExclamation("No se puede insertar nuevo registro en una ampliación que no existe","Aviso",globales.menuPrincipal);
                    return;
                }

                DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar una nuevo registro?", "Aviso", globales.menuPrincipal);
                if (p == DialogResult.No) return;
                this.esInsertar = true;



                griddocument.Rows.Insert(0);
                griddocument.Rows[0].Cells[0].Value = "";
                griddocument.Rows[0].Cells[1].Value = "";
                griddocument.Rows[0].Cells[2].Value = false;
                griddocument.Rows[0].Cells[3].Value = false;
                griddocument.Rows[0].Cells[4].Value = false;

                griddocument.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);



                string query = "insert into datos.h_sdocum(expediente,sec,cve_docum,original,copia,documento,ubicacion) values (" +
                 $"{txtExpediente.Text},'{this.secuencia}','','','','','' ) returning id";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                griddocument.Rows[0].Cells[5].Value = resultado[0]["id"];
                griddocument.CurrentCell = griddocument.Rows[0].Cells[0];
            }
            this.esInsertar = false;

        }




        private void frmdocusolic_KeyPress(object sender, KeyPressEventArgs e)
        {
            char S;

            S = Char.ToUpper(e.KeyChar);

            e.KeyChar = S;

            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }





        private void griddocument_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.row = e.RowIndex;
            this.column = e.ColumnIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cerrar();
        }
        private void cerrar()
        {
            this.Close();
            globales.showModal(new frmDocumentos());
        }

        private void btnFolio_Click(object sender, EventArgs e)
        {
            frmCatalogoP_quirog p_quirog = new frmCatalogoP_quirog();
            p_quirog.enviar2 = llenacampos;
            p_quirog.tablaConsultar = "p_hipote";
            p_quirog.ShowDialog();
        }

        public void recibiendoampliacion(string expediente, int opcion, Dictionary<string, Object> datos)
        {
            limpiacampos();
            this.expediente = expediente;
            string query = $"select expediente,sec from datos.h_solici where expediente = {expediente} and sec = '{opcion}'";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            string tramite = string.Empty;
            tramite = (opcion == 0) ? "Solicitud inicial" : opcion + "° Ampliación";
            

            this.txtamplia.Text = tramite;


            this.txtsolicitante.Text = Convert.ToString(datos["nombre_em"]);
            this.txtExpediente.Text = Convert.ToString(datos["folio"]);
            this.txtdomi.Text = Convert.ToString(datos["direccion"]);
            this.txtlabora.Text = Convert.ToString(datos["descripcion"]);
            this.txtproyecto.Text = Convert.ToString(datos["proyecto"]);
            this.txtubicacion.Text = Convert.ToString(datos["direc_inmu"]);
            this.txttel.Text = Convert.ToString(datos["tel_partic"]);

            if (resultado.Count == 0)
            {
                globales.MessageBoxExclamation($"Expediente N° {expediente} \nNo se encontro {tramite}", "Aviso", globales.menuPrincipal);
                consolicitud = false;
                return;
            }
            consolicitud = true;

            query = "select *  from datos.h_sdocum where expediente={0} and sec='{1}' order by id asc";
            query = string.Format(query, expediente, opcion);
            resultado = globales.consulta(query);
            griddocument.Rows.Clear();
            foreach (var item in resultado)
            {

                string cve_docum = Convert.ToString(item["cve_docum"]);
                string documento = Convert.ToString(item["documento"]);
                bool original = string.IsNullOrWhiteSpace(Convert.ToString(item["original"])) ? false : true;
                bool copia = string.IsNullOrWhiteSpace(Convert.ToString(item["copia"])) ? false : true;
                bool ubicacion = string.IsNullOrWhiteSpace(Convert.ToString(item["ubicacion"])) ? false : true; 
                string id = Convert.ToString(item["id"]);
                griddocument.Rows.Add(cve_docum, documento, original, copia, ubicacion,id);

            }
            this.secuencia = opcion;
        }

        private void txtExpediente_LocationChanged(object sender, EventArgs e)
        {

        }

        private void txtExpediente_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtExpediente.Text))
            {
                if (expediente == txtExpediente.Text) return;
                expediente = txtExpediente.Text;
                string query = "select * from datos.p_hipote where folio={0}";
                query = string.Format(query, txtExpediente.Text);
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count != 0)
                {
                    Dictionary<string, object> item = resultado[0];
                    llenacampos(item);

                }
                else
                {
                    globales.MessageBoxExclamation($"No existe el expediente N°: {txtExpediente.Text}", "Aviso", globales.menuPrincipal);
                    limpiacampos();

                }
            }
        }

        private void limpiacampos()
        {
            this.txtsolicitante.Text = string.Empty;
            this.txtExpediente.Text = string.Empty;
            this.txtdomi.Text = string.Empty;
            this.txtlabora.Text = string.Empty;
            this.txtproyecto.Text = string.Empty;
            this.txtubicacion.Text = string.Empty;
            this.txttel.Text = string.Empty;
            this.txtamplia.Text = string.Empty;

            griddocument.Rows.Clear();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void griddocument_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.teclaEnter)
            {
                var x = this.row + 1;
                var y = griddocument.Rows.Count;
                if (x != y)
                    SendKeys.Send("{UP}");
                SendKeys.Send("{TAB}");

                this.teclaEnter = false;
            }
        }

        private void griddocument_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            this.row = e.RowIndex;
            this.column = e.ColumnIndex;
        }

        private void griddocument_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.PreviewKeyDown -= new PreviewKeyDownEventHandler(viendoEdicion);
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(viendoEdicion);

            e.Control.KeyDown -= griddocument_KeyDown;
            e.Control.KeyDown += griddocument_KeyDown;
        }

        private void viendoEdicion(object sender, PreviewKeyDownEventArgs e)
        {
            this.teclaEnter = e.KeyCode == Keys.Enter;
        }

        private void griddocument_KeyDown(object sender, KeyEventArgs e)
        {
            if (griddocument.Rows.Count == 0) return;

            try
            {
                int rowactual = griddocument.Rows.Count;
                if (e.KeyCode == Keys.Insert && !globales.boolConsulta)
                {
                    if (!this.consolicitud)
                    {
                        globales.MessageBoxExclamation("No se puede insertar nuevo registor en una ampliación que no existe", "Aviso", globales.menuPrincipal);
                        return;
                    }

                    DialogResult p = globales.MessageBoxQuestion("¿Deseas registrar una nuevo registro?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;
                    this.esInsertar = true;

                    string fecha = string.Format("{0:d}", DateTime.Now);




                    griddocument.Rows.Insert(rowactual);
                    griddocument.Rows[rowactual].Cells[0].Value = "";
                    griddocument.Rows[rowactual].Cells[1].Value = "";
                    griddocument.Rows[rowactual].Cells[2].Value = false;
                    griddocument.Rows[rowactual].Cells[3].Value = false;
                    griddocument.Rows[rowactual].Cells[4].Value = false;

                    griddocument.Rows[rowactual].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);



                    string query = "insert into datos.h_sdocum(expediente,sec,cve_docum,original,copia,documento,ubicacion) values (" +
                  $"{txtExpediente.Text},'{this.secuencia}','','','','','' ) returning id";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    griddocument.Rows[rowactual].Cells[5].Value = resultado[0]["id"];
                    griddocument.CurrentCell = griddocument.Rows[rowactual].Cells[0];
                }

                if (e.KeyCode == Keys.Delete && !globales.boolConsulta)
                {
                    

                    DialogResult p = globales.MessageBoxQuestion("¿Desea eliminar el registro?", "Aviso", globales.menuPrincipal);
                    if (p == DialogResult.No) return;



                    string id = griddocument.Rows[row].Cells[5].Value.ToString();

                    griddocument.Rows.RemoveAt(row);
                    string query = "delete from datos.h_sdocum where expediente = {0} and id = {1} and sec = '{2}' ";
                    query = string.Format(query, txtExpediente.Text, id,this.secuencia);
                    if (globales.consulta(query, true))
                    {
                        globales.MessageBoxSuccess("Registro eliminado correctamente", "Aviso", globales.menuPrincipal);
                    }
                }



                if (e.Control && e.KeyCode == Keys.D)
                {
                    if (this.row > 1)
                    {
                        griddocument.Rows[row].Cells[this.column].Value = griddocument.Rows[row - 1].Cells[this.column].Value;
                    }
                }



                if (e.KeyCode == Keys.F1)
                {
                    if (this.column == 0)
                    {
                        frmCatalogoGeneral documentos = new frmCatalogoGeneral();
                        documentos.tabla = "documento_hipotecarios";
                        documentos.metodo = rellenarDocumentos;
                        documentos.ShowDialog();
                        SendKeys.Send("{TAB}");
                    }
                }
            }
            catch
            {

            }

            this.esInsertar = false;

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                try
                {
                    e.SuppressKeyPress = true;
                    int iColumn = griddocument.CurrentCell.ColumnIndex;
                    int iRow = griddocument.CurrentCell.RowIndex;
                    if (iColumn == griddocument.ColumnCount - 1)
                    {
                        if (griddocument.RowCount > (iRow + 1))
                        {
                            griddocument.CurrentCell = griddocument[1, iRow + 1];
                        }
                        else
                        {
                            //focus next control
                        }
                    }
                    else
                        griddocument.CurrentCell = griddocument[iColumn + 1, iRow];
                }
                catch
                {

                }
            }
        }

        private void rellenarDocumentos(Dictionary<string, object> obj)
        {

            griddocument.Rows[row].Cells[0].Value = obj["clave"];
            griddocument.Rows[row].Cells[1].Value = obj["descripcion"];

        }

        private void griddocument_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (editadoprogramadamente)
            {
                editadoprogramadamente = false;
                return;
            }
            int c = e.RowIndex;
            if (c == -1) return;



            if (this.esInsertar)
            {
                return;
            }

            string clave = griddocument.Rows[this.row].Cells[0].Value.ToString();

            if (column == 0)
            {
                string consulta = $"select * from catalogos.documento_hipotecarios where clave = '{clave}'";
                List<Dictionary<string, object>> res = globales.consulta(consulta);
                if (res.Count == 0)
                {
                    globales.MessageBoxExclamation("Documento no se encuentra en el catálogo, presione F1 para seleccionar alguno", "Aviso", globales.menuPrincipal);
                    this.editadoprogramadamente = true;
                    griddocument.Rows[c].Cells[0].Value = "";
                    clave = "";
                    this.editadoprogramadamente = true;
                    griddocument.Rows[c].Cells[1].Value = "";
                    this.editadoprogramadamente = false;

                }
                else
                {
                    Dictionary<string, object> dicc = res[0];
                    string descripcion = Convert.ToString(dicc["descripcion"]);
                    this.editadoprogramadamente = true;
                    griddocument.Rows[c].Cells[1].Value = descripcion;
                    this.editadoprogramadamente = false;
                }
            }

            string documento = griddocument.Rows[this.row].Cells[1].Value.ToString();
            string original = (Convert.ToBoolean(griddocument.Rows[this.row].Cells[2].Value)) ? "X" : "";
            string copia = (Convert.ToBoolean(griddocument.Rows[this.row].Cells[3].Value)) ? "X" : "";
            string ubic = (Convert.ToBoolean(griddocument.Rows[this.row].Cells[4].Value)) ? "X" : "";
            string id = griddocument.Rows[this.row].Cells[5].Value.ToString();

            string query = "update datos.h_sdocum set cve_docum = '{0}',original = '{1}',copia = '{2}',documento = '{3}',ubicacion = '{4}' where id = {5} and expediente = {6} and sec = '{7}'";

            query = string.Format(query,clave,original,copia,documento,ubic,id,txtExpediente.Text,secuencia);
            globales.consulta(query,true);

            editadoprogramadamente = false;
        }

        private void txtsolicitante_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtdomi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtlabora_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtubicacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtproyecto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtExpediente_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void frmdocusolic_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void frmdocusolic_Shown(object sender, EventArgs e)
        {
            ActiveControl = button2;
        }
    }
}

