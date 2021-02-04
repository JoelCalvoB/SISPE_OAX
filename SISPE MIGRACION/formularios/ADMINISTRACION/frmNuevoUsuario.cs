using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.ADMINISTRACION
{
    public partial class frmNuevoUsuario : Form
    {
        private List<Dictionary<string, object>> resultado;
        private List<Dictionary<string, object>> areas;

        private bool modificar;
        private string id;
        private string id_depto;
        public frmNuevoUsuario(List<Dictionary<string,object>> resultado = null,bool modificar = false,string id = "")
        {
            InitializeComponent();
            this.resultado = resultado;
            this.modificar = modificar;
            this.id = id;
        }

        private void frmNuevoUsuario_Load(object sender, EventArgs e)
        {
            if (!this.modificar)
            {
                
            }
            else {

            }

            this.cmbTipo.SelectedIndex = globales.tipomenu;
        }

        public void cargandoArboldeOpciones(int opcion) {
            try {

                List<Dictionary<string, object>> resultado = (List<Dictionary<string, object>>)globales.getMenu(opcion);
                arbol.Nodes.Clear();
                establecerNodo(resultado, arbol.Nodes);
            }
            catch
            {

            }
            if (this.modificar)
            {
                
                rellenarCampos();
                txtUsuario.Enabled = false;
                foreach (Dictionary<string, object> item in this.resultado)
                {
                    string nombre = Convert.ToString(item["nombre"]);
                    foreach (TreeNode nodo in arbol.Nodes)
                    {
                        if (nombre == nodo.Text)
                        {
                            bool activo = Convert.ToBoolean(item["activo"]);
                            nodo.Checked = activo;
                            activandoOpcionesUsuario((List<Dictionary<string, object>>)item["submenu"], nodo.Nodes);
                            break;
                        }
                    }
                }
            }

            
        }

        private void activandoOpcionesUsuario(List<Dictionary<string, object>> list, TreeNodeCollection nodes)
        {
            foreach (Dictionary<string,object> item in list) {
                foreach (TreeNode nodo in nodes) {                string nombre = Convert.ToString(item["nombre"]);
                    if (nombre == nodo.Text) {
                        bool activo = Convert.ToBoolean(item["activo"]);
                        nodo.Checked = activo;
                        activandoOpcionesUsuario((List<Dictionary<string, object>>)item["submenu"], nodo.Nodes);
                        break;
                    }
                }
            }
        }

        private void rellenarCampos()
        {
            string query = $"select * from catalogos.usuarios where idusuario = {this.id}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count != 0) {
                Dictionary<string, object> tmp = resultado[0];
                string nombre = Convert.ToString(tmp["nombre"]).Trim();
                string puesto = Convert.ToString(tmp["puesto"]).Trim();
                string password = Convert.ToString(tmp["password"]).Trim();
                string usuario = Convert.ToString(tmp["usuario"]).Trim();
                string observaciones = Convert.ToString(tmp["observaciones"]).Trim();
                bool activo = Convert.ToBoolean(tmp["activo"]);
                string id_depto = Convert.ToString(tmp["id_depto"]);
                if (!string.IsNullOrWhiteSpace(this.id_depto))
                {
                    string queri = $"select * oficialia.areas_pensiones where id_depto={id_depto}";
                    List<Dictionary<string, object>> heyYou = globales.consulta(queri);
                    foreach (var item in heyYou)
                    {
                        comboDepto.Text = Convert.ToString(item["nombre"]);
                    }
                }
             
                txtNombre.Text = nombre;
                txtUsuario.Text = usuario;
                txtPass1.Text = password;
                txtPass2.Text = password;
                txtObservaciones.Text = observaciones;
                txtPuesto.Text = puesto;
                chkActivo.Checked = activo;
            }
        }

        private void establecerNodo(List<Dictionary<string, object>> resultado, TreeNodeCollection auxArbol)
        {
            int contador = 0;
            foreach (Dictionary<string, object> item in resultado)
            {
                auxArbol.Add(Convert.ToString(item["nombre"]));
               
                try
                {
                    auxArbol[contador].Name = Convert.ToString(item["id"]);
                }
                catch {
                }
                auxArbol[contador].Checked = !this.modificar;
                establecerNodo((List<Dictionary<string, object>>)item["submenu"], auxArbol[contador].Nodes);
                contador++;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
           

            if(string.IsNullOrWhiteSpace(txtUsuario.Text)) {
                MessageBox.Show("Es necesario insertar un Usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUsuario.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Insertar nombre del usuario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNombre.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPass1.Text))
            {
                MessageBox.Show("Favor de insertar la contraseña.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPass1.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPass2.Text))
            {
                MessageBox.Show("Favor de confirmar contraseña.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPass2.Focus();
                return;
            }

            if (!txtPass2.Text.Equals(txtPass1.Text)) {
                MessageBox.Show("Las contraseñas no conciden.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPass1.Focus();
                return;
            }
            //if (string.IsNullOrWhiteSpace(comboDepto.Text))
            //{
            //    MessageBox.Show("Insertar Departamento O área.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    comboDepto.Focus();
            //    return;
            //}

            this.Cursor = Cursors.WaitCursor;

            if (!this.modificar)
            {
                string query = $"select * from catalogos.usuarios where usuario = '{txtUsuario.Text}'";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count > 0)
                {
                    MessageBox.Show("Usuario ya existe, favor de ingresar otro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUsuario.Focus();
                    return;
                }

                query = "select max(idusuario)+1 as cantidad from catalogos.usuarios";
                string fecha1 = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                int cantidad = Convert.ToInt32(globales.consulta(query)[0]["cantidad"]);
                query = "insert into catalogos.usuarios(idusuario,usuario,password,nombre,puesto,observaciones,fregistro,activo,tipomenu) " +
                    $"values ({cantidad},'{txtUsuario.Text}','{txtPass1.Text}','{txtNombre.Text}','{txtPuesto.Text}','{txtObservaciones.Text}','{fecha1}',{chkActivo.Checked},{cmbTipo.SelectedIndex})";
                if (globales.consulta(query, true))
                {
                    query = $"select * from catalogos.usuarios where usuario = '{txtUsuario.Text}'";
                    string id = Convert.ToString(globales.consulta(query)[0]["idusuario"]);
                    List<Dictionary<string, object>> listaFinal = new List<Dictionary<string, object>>();
                    foreach (TreeNode item in arbol.Nodes)
                    {
                        Dictionary<string, object> diccionario = new Dictionary<string, object>();
                        
                        diccionario.Add("nombre", item.Text);
                        diccionario.Add("activo", item.Checked);
                        diccionario.Add("id", (item.Name));
                        diccionario.Add("submenu", sacarSubMenu(item.Nodes));
                        listaFinal.Add(diccionario);
                    }

                    globales.insertarRoles(listaFinal, id);
                }
                this.Cursor = Cursors.Default;
                MessageBox.Show("Registro insertado correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Owner.Close();
            }
            else {
                string query = $"select * from catalogos.usuarios where idusuario = '{id}'";
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                if (resultado.Count > 0) {
                    string id = Convert.ToString(resultado[0]["idusuario"]);

                    query = $"update catalogos.usuarios set usuario='{txtUsuario.Text}', nombre = '{txtNombre.Text}',puesto = '{txtPuesto.Text}',activo = {chkActivo.Checked},observaciones = '{txtObservaciones.Text}',password = '{txtPass1.Text}',tipomenu={cmbTipo.SelectedIndex} where idusuario = '{id}'";
                    globales.consulta(query,true);

                    List<Dictionary<string, object>> listaFinal = new List<Dictionary<string, object>>();
                    foreach (TreeNode item in arbol.Nodes)
                    {
                        Dictionary<string, object> diccionario = new Dictionary<string, object>();
                        diccionario.Add("nombre", item.Text);
                        diccionario.Add("activo", item.Checked);
                        diccionario.Add("id", (item.Name));
                        diccionario.Add("submenu", sacarSubMenu(item.Nodes));
                        listaFinal.Add(diccionario);
                    }

                    globales.insertarRoles(listaFinal, id);
                }
                this.Cursor = Cursors.Default;
                MessageBox.Show("Registro actualizado correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Owner.Close();
            }
        }

        private object sacarSubMenu(TreeNodeCollection nodes)
        {
            List<Dictionary<string, object>> lista = new List<Dictionary<string, object>>();
            foreach (TreeNode item in nodes) {
                Dictionary<string, object> diccionario = new Dictionary<string, object>();
                if (!item.Checked) continue;
                diccionario.Add("nombre",item.Text);
                diccionario.Add("activo",item.Checked);
                diccionario.Add("submenu",sacarSubMenu(item.Nodes));
                lista.Add(diccionario);
            }
            return lista;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfa(e.KeyChar);
        }

        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !globales.alfaNumerico(e.KeyChar);
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPass1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int opcion = this.cmbTipo.SelectedIndex;
            cargandoArboldeOpciones(opcion);
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            //string query = $"select * from oficialia.areas_pensiones";
            //this.areas = globales.consulta(query);
            //comboDepto.Items.Clear();
            //foreach(var item in areas)
            //{
            //    string nombre = Convert.ToString(item["nombre"]);
            //    comboDepto.Items.Add(nombre);
            //}
        }

        private void comboDepto_Leave(object sender, EventArgs e)
        {
            foreach(var item in this.areas)
            {

                if (Convert.ToString(item["nombre"])==comboDepto.Text)
                {
                    this. id_depto = Convert.ToString(item["id"]);
                }
            }
        }
    }
}
