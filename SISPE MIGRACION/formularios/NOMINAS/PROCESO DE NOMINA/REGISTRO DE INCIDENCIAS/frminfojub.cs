using SISPE_MIGRACION.codigo.repositorios.nominas_catalogos;
using SISPE_MIGRACION.formularios.CATÁLOGOS;
using SISPE_MIGRACION.formularios.CATÁLOGOS.modales_catalogos;
using SISPE_MIGRACION.formularios.NOMINAS.CATALOGOS.modal;
using SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.REGISTRO_DE_INCIDENCIAS.OPCIONES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.PROCESO_DE_NOMINA.REGISTRO_DE_INCIDENCIAS
{
    public partial class frminfojub : Form
    {

        public bool bandera;
        public string tipopen;
        private frmEmpleados forma;
        int c;
        private bool entrar;
        private int indexrow;
        private List<string> claves;

        public frminfojub()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(frminfojub_KeyPress);



        }


        private void frminfojub_Load(object sender, EventArgs e)
        {

        }

        private void btnguardar_Click(object sender, EventArgs e)
        {

        }

        private void btnFolio_Click(object sender, EventArgs e)
        {

            limpiaForm();
            desbloquea();
            frmModalJubilados jubi = new frmModalJubilados();
            jubi.enviar = rellenar;
            globales.showModal(jubi);

        }

        private void rellenar(Dictionary<string, object> diccionario)
        {

            dtggrid.Rows.Clear();
            maestro maestro = new dbaseORM().getObject<maestro>(diccionario);
            txtnumemp.Text = Convert.ToString(maestro.num);
            string numemp = txtnumemp.Text;
            linkLabel1.Visible = false;
            checkBox2.Checked = false;
            checkBox2.Enabled = true;
            checkBox3.Checked = false;
            checkBox3.Enabled = true;


            if (maestro.jpp == "JUB") comboBoxnom.Text = "JUB";
            if (maestro.jpp == "PDO") comboBoxnom.Text = "PDO";
            if (maestro.jpp == "PTA") comboBoxnom.Text = "PTA";
            if (maestro.jpp == "PEA") comboBoxnom.Text = "PEA";
            string ceros = "000000";
            int contador = numemp.Length;
            string complemento = ceros.Substring(0, (6 - contador));

            string proyecto = comboBoxnom.Text + complemento + numemp;
            txtproyecto.Text = comboBoxnom.Text + txtnumemp.Text;

            string suspensionFondo = Convert.ToString(maestro.nosuspen);
            if (!string.IsNullOrWhiteSpace(suspensionFondo))
            {
                checkBox2.Checked = true;
                checkBox2.Enabled = false;
                panel2.Visible = false;
                linkLabel1.Visible = true;
            }


            if (!string.IsNullOrWhiteSpace(maestro.num_susp_seguro))
            {
                checkBox3.Checked = true;
                panel7.Visible = false;
                checkBox3.Enabled = false;
                linkLabel1.Visible = true;
            }



            txtrfc.Text = maestro.rfc;
            txtnombre.Text = maestro.nombre;
            txtfechanac.Text = Convert.ToString(maestro.fnacimien);
            txtdireccion.Text = maestro.domicilio;
            txtcodigo.Text = Convert.ToString(maestro.codigop);
            txtcorreo.Text = Convert.ToString(maestro.correo);
            txtcategoria.Text = Convert.ToString(maestro.categ);
            txttelefono.Text = Convert.ToString(maestro.telefono);
            txtingreso.Text = Convert.ToString(maestro.fching).Replace("12:00:00 a. m.", "");

            bool may = Convert.ToBoolean(diccionario["d_madres"]);
            if (may==true)
            {
                checkMamas.Checked=true;
            }
            else
            {
                checkMamas.Checked = false;

            }
            this.tipopen = maestro.tipopen;
            if (maestro.sexo == "1") rbm.Checked = true;
            if (maestro.sexo == "2") rbf.Checked = true;
            if (maestro.nomelec == "S") rbs.Checked = true;
            if (maestro.nomelec == "N") rbn.Checked = true;

            // CEDULA
            txtingreso.Text = Convert.ToString(maestro.fching);
            txtFechaCedula.Text = Convert.ToString(maestro.fcedula);

            llenagrid();


            if (maestro.banco == "BANORTE")
            {
                comboBoxban.Text = "BANORTE";
            }
            if (maestro.banco == "BANAMEX")
            {
                comboBoxban.Text = "BANAMEX";
            }

            txtcuenta.Text = maestro.cuentabanc;
            txtcurp.Text = maestro.curp;
            txtimss.Text = maestro.imss;
            if (maestro.ley == 1) rbleyant.Checked = true;
            if (maestro.ley == 2) rbnuevaley.Checked = true;
            if (maestro.tiporel == "CONF") comboBoxrela.Text = "CONF";
            if (maestro.tiporel == "MMYS") comboBoxrela.Text = "MMYS";
            if (maestro.tiporel == "BASE") comboBoxrela.Text = "BASE";

            txtQuinquenios.Text = Convert.ToString(maestro.quinquenios);
            txtDiasAguina.Text = Convert.ToString(maestro.dias_aguinaldo);
            txtnivLabo.Text = Convert.ToString(maestro.nivel);

            btnGuardar.Text = "ACTUALIZAR";
            txtnombre.Select();


            comboBoxnom_SelectedIndexChanged(null, null);


            if (maestro.jpp == "PEA") {
                dtggrid.Rows.Clear();
                string query = $"select pea.*,mma.nombre,mma.rfc from nominas_catalogos.pension_alimenticia pea inner join nominas_catalogos.maestro mma on mma.jpp = pea.jpp and mma.num = pea.numjpp where numpea = {txtnumemp.Text} ";
                List<Dictionary<string, object>> res = globales.consulta(query);
                foreach (Dictionary<string,object> item in res) {
                    dtggrid.Rows.Add(item["jpp"], item["numjpp"], item["rfc"], item["nombre"], Convert.ToString(item["descuento"])+"%",globales.convertMoneda(globales.convertDouble(Convert.ToString(item["total"]))),item["id"],item["id_enlace"],item["id_enlacepea"],item["claves_vinculadas"]);
                }
            }


            checkBox1.Enabled = false;
            comboBoxnom.Enabled = false;
            txtnumemp.Enabled = true;
            txtproyecto.Enabled = false;
        }



        private void llenaTab2()
        {
            datatab2.Rows.Clear();

            if (string.IsNullOrWhiteSpace(comboBoxnom.Text) && string.IsNullOrWhiteSpace(txtnumemp.Text)) return;
            string query = $"SELECT * FROM nominas_catalogos.cedula where jpp='{comboBoxnom.Text}' and num={txtnumemp.Text};";
            List<Dictionary<string, object>> cedula = globales.consulta(query);
            foreach (var item in cedula)
            {
                string beneficiarios = Convert.ToString(item["benefic"]);
                string porcen = Convert.ToString(item["porcen"]);
                string parentesco = Convert.ToString(item["paren"]);
                string id = Convert.ToString(item["id"]);
                datatab2.Rows.Add(beneficiarios, porcen + "%", parentesco, id);
            }

            txtnomced.Text = txtnombre.Text;
            if (comboBoxnom.Text == "JUB") txttipopen.Text = "JUBILADO";
            if (comboBoxnom.Text == "PDO") txttipopen.Text = "PENSIONADO";
            if (comboBoxnom.Text == "PTA") txttipopen.Text = "PENSIONISTA";
            if (comboBoxnom.Text == "PEA") txttipopen.Text = "PENSIÓN ALIMENTICIA";

            txtFcedula.Text = txtFechaCedula.Text;
        }

        private void limpiaForm()
        {
            this.tabPage1.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = "");
            this.tabPage1.Controls.OfType<MaskedTextBox>().ToList().ForEach(o => o.Text = "");
            datacedula.Rows.Clear();
            txtingreso.Clear();
            txtFechaCedula.Clear();
            txtfechanac.Clear();
            txtingreso.Clear();
            datatab2.Rows.Clear();
            this.tabPage2.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = "");
            txtFcedula.Clear();
            checkMamas.Checked = false;

            this.clavesVinculadas = string.Empty;


        }

        private void frminfojub_Shown(object sender, EventArgs e)
        {
            btnFolio.Select();
            bloquea();
            lbltitulo.Text = "SELECCIONAR JUBILADO";
            // comboBoxnom.SelectedIndex = 0;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiaForm();
            desbloquea();
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            btnGuardar.Text = "INSERTAR";
            comboBoxnom.Select();
            button5.Visible = true;
            comboBoxban.SelectedIndex = 0;
            lbltitulo.Text = "JUBILADO";
            comboBoxnom.SelectedIndex = 0;
            comboBoxnom_SelectedIndexChanged(null,null);


            checkBox1.Enabled = true;
            comboBoxnom.Enabled = true;
            txtnumemp.Enabled = false;
            txtproyecto.Enabled = false;

            dtggrid.Rows.Clear();
        }




        private void comboBoxnom_Leave(object sender, EventArgs e)
        {
            string query = "";
            string numero = "";
            if (comboBoxnom.Text == "JUB") txttipopen.Text = "JUBILADO";
            if (comboBoxnom.Text == "PDO") txttipopen.Text = "PENSIONADO";
            if (comboBoxnom.Text == "PTA") txttipopen.Text = "PENSIONISTA";
            if (comboBoxnom.Text == "PEA") txttipopen.Text = "PENSIÓN ALIMENTICIA";

            if (btnGuardar.Text == "INSERTAR")
            {
                if (comboBoxnom.Text == "JUB")
                {
                    query = "SELECT max(num)+1 as ultimo FROM nominas_catalogos.maestro where jpp='JUB' AND num<300000;";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    txtnumemp.Text = Convert.ToString(resultado[0]["ultimo"]);
                    txtnumemp.Select();
                    if (checkBox1.Checked == true)
                    {
                        txtcategoria.Text = "JUBILADO FORANEO";

                    }
                    else
                    {
                        txtcategoria.Text = "JUBILADO CENTRO";

                    }
                    txtproyecto.Text = comboBoxnom.Text + txtnumemp.Text;
                }
                if (comboBoxnom.Text == "PDO")
                {
                    query = "SELECT max(num)+1 as ultimo FROM nominas_catalogos.maestro where jpp='PDO' AND num<400000;";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    txtnumemp.Text = Convert.ToString(resultado[0]["ultimo"]);
                    txtnumemp.Select();
                    if (checkBox1.Checked == true)
                    {
                        txtcategoria.Text = "PENSIONADO FORANEO";

                    }
                    else
                    {
                        txtcategoria.Text = "PENSIONADO CENTRO";

                    }
                    txtproyecto.Text = comboBoxnom.Text + txtnumemp.Text;
                }
                if (comboBoxnom.Text == "PTA")
                {
                    query = "SELECT max(num)+1 as ultimo FROM nominas_catalogos.maestro where jpp='PTA' AND num<500000;";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    txtnumemp.Text = Convert.ToString(resultado[0]["ultimo"]);
                    txtnumemp.Select();
                    if (checkBox1.Checked == true)
                    {
                        txtcategoria.Text = "PENSIONISTA FORANEO";

                    }
                    else
                    {
                        txtcategoria.Text = "PENSIONISTA CENTRO";

                    }
                    txtproyecto.Text = comboBoxnom.Text + txtnumemp.Text;
                }
                if (comboBoxnom.Text == "PEA")
                {
                    query = "SELECT max(num)+1 as ultimo FROM nominas_catalogos.maestro where jpp='PEA' ";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    txtnumemp.Text = Convert.ToString(resultado[0]["ultimo"]);
                    txtnumemp.Select();
                    if (checkBox1.Checked == true)
                    {
                        txtcategoria.Text = "PENSION ALIM. FORANEO";

                    }
                    else
                    {
                        txtcategoria.Text = "PENSION ALIM. CENTRO";

                    }
                    txtproyecto.Text = comboBoxnom.Text + txtnumemp.Text;
                }
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string guarda_numero = txtnumemp.Text;
            string query = "";
            string numero = "";
            if (checkBox1.Checked == true)
            {
                if (comboBoxnom.Text == "JUB")
                {
                    query = "SELECT max(num)+50  as ultimo FROM nominas_catalogos.maestro where jpp='JUB'";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    txtnumemp.Text = Convert.ToString(resultado[0]["ultimo"]);
                    txtnumemp.Select();
                }
                if (comboBoxnom.Text == "PDO")
                {
                    query = "SELECT max(num)+50 as ultimo FROM nominas_catalogos.maestro where jpp='PDO';";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    txtnumemp.Text = Convert.ToString(resultado[0]["ultimo"]);
                    txtnumemp.Select();
                }
                if (comboBoxnom.Text == "PTA")
                {
                    query = "SELECT max(num)+50 as ultimo FROM nominas_catalogos.maestro where jpp='PTA'";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    txtnumemp.Text = Convert.ToString(resultado[0]["ultimo"]);
                    txtnumemp.Select();
                }
            }
            else

            {
                comboBoxnom_Leave(null,null);

            }


            txtproyecto.Text = comboBoxnom.Text + txtnumemp.Text;
        }

        private void frminfojub_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                SendKeys.Send("{TAB}");//Cuando se presiona la tecla enter, este le manda señal a la tecla TAB para que active el evento de traspaso...
            }
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxnom.Text) && string.IsNullOrWhiteSpace(txtnumemp.Text)) return;
            button5.Visible = false;

            if (btnGuardar.Text == "ACTUALIZAR")

            {

                DialogResult resultado1 = globales.MessageBoxQuestion("¿Deseas modificar el registro?", "Aviso", this);
                if (resultado1 == DialogResult.No) return;
                string sexo = "";
                string elec = "";
                string ley = "";
                if (rbm.Checked)
                {
                    sexo = "1";
                }
                else
                {
                    sexo = "2";

                }
                if (rbs.Checked)
                {
                    elec = "S";
                }
                else
                {
                    elec = "N";

                }
                if (rbleyant.Checked)
                {
                    ley = "1";
                }
                else
                {
                    ley = "2";

                }
                string Mayo = string.Empty;
                if (checkMamas.Checked)
                {
                    Mayo = "true";
                }
                else
                {
                    Mayo = "false";
                }
                
             
                try
                {

                    string query = $"update  nominas_catalogos.maestro set rfc='{txtrfc.Text}',nombre='{txtnombre.Text}',fnacimien='{txtfechanac.Text}',categ='{txtcategoria.Text}',curp='{txtcurp.Text}',sexo='{sexo}',proyecto='{txtproyecto.Text}',domicilio='{txtdireccion.Text}',codigop='{txtcodigo.Text}',telefono='{txttelefono.Text}',imss='{txtimss.Text}',fching='{txtingreso.Text}',nivel='{txtnivLabo.Text}',nomelec='{elec}',tiporel='{comboBoxrela.Text}',cuentabanc='{txtcuenta.Text}',banco='{comboBoxban.Text}',fcedula='{txtFechaCedula.Text}',quinquenios={txtQuinquenios.Text},ley='{ley}',usuario_sis='{globales.usuario}',correo='{txtcorreo.Text}',dias_aguinaldo={txtDiasAguina.Text} , d_madres={Mayo}  where jpp='{comboBoxnom.Text}' and num={txtnumemp.Text};";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    RecorreGrid();
                    checkBox3.Enabled = true;
                    checkBox2.Enabled = true;


                    if (comboBoxnom.SelectedIndex == 2)
                    {
                        List<pension_alimenticia> listaInsertar = new List<pension_alimenticia>();
                        List<pension_alimenticia> listaActualizar = new List<pension_alimenticia>();
                        List<pension_alimenticia> listaenviar = new List<pension_alimenticia>();
                        foreach (DataGridViewRow item in dtggrid.Rows)
                        {
                            pension_alimenticia obj = new pension_alimenticia();
                            obj.jpp = Convert.ToString(item.Cells[0].Value);
                            obj.numjpp = globales.convertInt(Convert.ToString(item.Cells[1].Value));
                            obj.numpea = Convert.ToInt32(txtnumemp.Text);
                            obj.descuento = globales.convertDouble(Convert.ToString(item.Cells[4].Value).Replace("%", ""));
                            obj.total = globales.convertDouble(Convert.ToString(item.Cells[5].Value));
                            obj.id = globales.convertInt(Convert.ToString(item.Cells[6].Value));
                            obj.id_enlace = globales.convertInt(Convert.ToString(item.Cells[7].Value));
                            obj.id_enlacepea = globales.convertInt(Convert.ToString(item.Cells[8].Value));
                            obj.claves_vinculadas = Convert.ToString(item.Cells[9].Value);
                            if (obj.id != 0)
                            {
                                listaActualizar.Add(obj);
                            }
                            else {
                                listaInsertar.Add(obj);
                            }

                            listaenviar.Add(obj);
                        }

                        if (dtggrid.Rows.Count != 0) {
                            new dbaseORM().insertAll<pension_alimenticia>(listaInsertar);
                            new dbaseORM().updateAll<pension_alimenticia>(listaActualizar);
                        }



                        if (listaeliminar.Count != 0) {
                            new dbaseORM().deleteAll<pension_alimenticia>(listaeliminar);
                        }
                        button8.Enabled = false;
                        button9.Enabled = false;

                        txtNumerobuscar.Text = "";
                        txtrfc1.Text = string.Empty;
                        txtnombre1.Text = string.Empty;
                        txttotal1.Text = string.Empty;
                        txtporcentaje1.Text = string.Empty;
                        txtsueldo1.Text = string.Empty;

                        panelPensionalimenticia.Enabled = false;
                        panelPensionalimenticia.Visible = false;

                        panel1.Enabled = true;
                        panel1.Visible = true;

                        tabPage2.Visible = true;
                        dtggrid.Rows.Clear();

                        DialogResult dia = globales.MessageBoxQuestion("¿Deseas modificar pensión de la nomina?", "Aviso", this);
                        if (dia == DialogResult.Yes)
                        {
                            frmAgregarNominaPension pension = new frmAgregarNominaPension(txtnumemp.Text, listaInsertar, listaeliminar,listaActualizar);
                            globales.showModalReturning(pension);
                            
                        }

                        listaeliminar.Clear();

                    }
                    btnGuardar.Text = "-";

                    DialogResult dialogo = globales.MessageBoxSuccess("SE ACTUALIZO REGISTRO EXITOSAMENTE", "FINALIZADO", globales.menuPrincipal);
                    limpiaForm();
                }
                catch
                {
                    DialogResult dialogoerror = globales.MessageBoxError("ERROR CONTACTE A SISTEMAS", "ERROR", globales.menuPrincipal);
                    return;
                }
            }


            if (btnGuardar.Text == "INSERTAR")
            {
                DialogResult resultado1 = globales.MessageBoxQuestion("¿Deseas insertar el registro?", "Aviso", this);
                if (resultado1 == DialogResult.No) return;
                string sexo = "";
                string elec = "";
                string ley = "";
                DateTime actual = DateTime.Now;

                if (rbm.Checked)
                {
                    sexo = "1";
                }
                else
                {
                    sexo = "2";

                }
                if (rbs.Checked)
                {
                    elec = "S";
                }
                else
                {
                    elec = "N";

                }
                if (rbleyant.Checked)
                {
                    ley = "1";
                }
                else
                {
                    ley = "2";

                }

                string Mayo = string.Empty;
                if (checkMamas.Checked)
                {
                    Mayo = "true";
                }
                else
                {
                    Mayo = "false";
                }



                try
                {
                    RecorreGrid();
                    string emp = txtnumemp.Text;
                    string ceros = "000000";
                    int contador = emp.Length;
                    string complemento = ceros.Substring(0, (6 - contador));

                    string clave = comboBoxnom.Text + complemento + emp;

                    string f_ingreso;
                    try 
                    {
                        f_ingreso = $"'{string.Format("{0:yyyy-MM-dd}",DateTime.Parse(txtingreso.Text))}'";
                    }
                    catch
                    {
                        f_ingreso = "null";
                    }
                    string cp;

                    if (string.IsNullOrWhiteSpace(txtcodigo.Text))
                    {
                        cp = "null";
                    }
                    else
                    {
                        cp = $"{txtcodigo.Text}";
                    }

                    string quinquenios;
                    if (string.IsNullOrWhiteSpace(txtQuinquenios.Text))
                    {
                        quinquenios = "null";
                    }
                    else
                    {
                        quinquenios = txtQuinquenios.Text;
                    }

                    string dias_aguinaldo;
                    if (string.IsNullOrWhiteSpace(txtDiasAguina.Text))
                    {
                        dias_aguinaldo = "null";
                    }
                    else
                    {
                        dias_aguinaldo = txtDiasAguina.Text;
                    }

                    

                   
                  

                    string f_nacimiento;

                    try
                    {
                        f_nacimiento = $"'{string.Format("{0:yyyy-MM-dd}",DateTime.Parse(txtfechanac.Text))}'";
                    }
                    catch
                    {
                        f_nacimiento = "null";
                    }

                 




                    string query = "insert into nominas_catalogos.maestro ( jpp  , clave,  num,            rfc,            nombre,               fnacimien,                         categ,              curp,            sexo,   proyecto,    domicilio,             codigop,                telefono,       imss,            fching,                   nivel,       nomelec,          tiporel,       cuentabanc,            banco,        quinquenios,              ley,f_captura,            correo,            dias_aguinaldo   , superviven     ,   tipopen    , usuario_sis ,d_madres , llave_qr )" +
                                                             $"VALUES ('{comboBoxnom.Text}','{clave}',{txtnumemp.Text},'{txtrfc.Text}','{txtnombre.Text}',{f_nacimiento},'{txtcategoria.Text}',  '{txtcurp.Text}',{sexo},'{txtproyecto.Text}','{txtdireccion.Text}',{cp},'{txttelefono.Text}','{txtimss.Text}',{f_ingreso},'{txtnivLabo.Text}', '{elec}',  '{comboBoxrela.Text}','{txtcuenta.Text}','{comboBoxban.Text}',{quinquenios},{ley},'{Convert.ToString(string.Format("{0:d}", actual))}','{txtcorreo.Text}',{dias_aguinaldo},'S','{txttipopen.Text}','{globales.usuario}',{Mayo} , substr(md5(random()::text), 0, 25))";
                     globales.consulta(query);

                    try
                    {

                        if (comboBoxnom.SelectedIndex == 2)
                        {
                            List<pension_alimenticia> lista = new List<pension_alimenticia>();
                            foreach (DataGridViewRow item in dtggrid.Rows)
                            {
                                pension_alimenticia obj = new pension_alimenticia();
                                obj.jpp = Convert.ToString(item.Cells[0].Value);
                                obj.numjpp = globales.convertInt(Convert.ToString(item.Cells[1].Value));
                                obj.numpea = Convert.ToInt32(txtnumemp.Text);
                                obj.descuento = globales.convertDouble(Convert.ToString(item.Cells[4].Value).Replace("%",""));
                                obj.total = globales.convertDouble(Convert.ToString(item.Cells[5].Value));
                                obj.claves_vinculadas = Convert.ToString(item.Cells[9].Value);
                                lista.Add(obj);
                            }

                            if (dtggrid.Rows.Count != 0) {
                                new dbaseORM().insertAll<pension_alimenticia>(lista);
                            }
                            button8.Enabled = false;
                            button9.Enabled = false;

                            txtNumerobuscar.Text = "";
                            txtrfc1.Text = string.Empty;
                            txtnombre1.Text = string.Empty;
                            txttotal1.Text = string.Empty;
                            txtporcentaje1.Text = string.Empty;
                            txtsueldo1.Text = string.Empty;

                            panelPensionalimenticia.Enabled = false;
                            panelPensionalimenticia.Visible = false;

                            panel1.Enabled = true;
                            panel1.Visible = true;

                            tabPage2.Visible = true;
                            dtggrid.Rows.Clear();

                            DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas agregar pensión a la nomina?","Aviso",this);
                            if (dialogo == DialogResult.Yes) {
                                frmAgregarNominaPension pension = new frmAgregarNominaPension(txtnumemp.Text,lista);
                                globales.showModalReturning(pension);
                            }

                        }
                        btnGuardar.Text = "-";
                    }
                    catch {
                        globales.MessageBoxError("Error al enlazar pensión alimenticia con JPP/PDO/PTA,No se preocupe todo el registro de la pensión capturada fue guardada exitosamente.","Aviso",globales.menuPrincipal);
                    }

                    globales.MessageBoxSuccess("SE ACTUALIZO REGISTRO EXITOSAMENTE", "FINALIZADO", globales.menuPrincipal);
                    limpiaForm();
                }
                catch
                {
                    DialogResult dialogoerror = globales.MessageBoxError("ERROR CONTACTE A SISTEMAS", "ERROR", globales.menuPrincipal);
                    return;
                }

            }

        }




        private void RecorreGrid() //joel
        {
            foreach (DataGridViewRow item in datacedula.Rows)
            {


                string beneficiario = Convert.ToString(item.Cells[0].Value);
                string porcentaje = Convert.ToString(item.Cells[1].Value).Replace("%", "");
                string parentesco = Convert.ToString(item.Cells[2].Value);
                string id = Convert.ToString(item.Cells[3].Value);
                string query = string.Empty;
                if (string.IsNullOrWhiteSpace(beneficiario)) continue;


                if (!string.IsNullOrWhiteSpace(id))
                {
                    if (!string.IsNullOrWhiteSpace(beneficiario))
                    {
                        query = $"update nominas_catalogos.cedula set benefic='{beneficiario}', porcen='{porcentaje}', paren='{parentesco}' where id='{id}'";
                    }
                }

                else
                {
                    query = $"insert into nominas_catalogos.cedula(jpp, num, rfc, benefic, porcen, paren) values('{comboBoxnom.Text}',{txtnumemp.Text},'{txtrfc.Text}','{beneficiario}','{porcentaje}','{parentesco}');";

                }
                globales.consulta(query);

            }
        }


        private void bloquea()
        {
            this.tabPage1.Controls.OfType<TextBox>().ToList().ForEach(o => o.Enabled = false);
            this.tabPage1.Controls.OfType<MaskedTextBox>().ToList().ForEach(o => o.Enabled = false);
            this.tabPage1.Controls.OfType<ComboBox>().ToList().ForEach(o => o.Enabled = false);
            this.panel1.Controls.OfType<MaskedTextBox>().ToList().ForEach(o => o.Enabled = false);
            datacedula.Enabled = false;
            btnGuardar.Enabled = false;
            datatab2.Enabled = false;
            txtnomced.Enabled = false;
            txttipopen.Enabled = false;
            txtFcedula.Enabled = false;
            btnimprimir2.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;

        }

        private void desbloquea()
        {
            this.tabPage1.Controls.OfType<TextBox>().ToList().ForEach(o => o.Enabled = true);
            this.tabPage1.Controls.OfType<MaskedTextBox>().ToList().ForEach(o => o.Enabled = true);
            this.tabPage1.Controls.OfType<ComboBox>().ToList().ForEach(o => o.Enabled = true);
            this.panel1.Controls.OfType<MaskedTextBox>().ToList().ForEach(o => o.Enabled = true);
            datacedula.Enabled = true;
            btnGuardar.Enabled = true;
            datatab2.Enabled = true;
            txtnomced.Enabled = true;
            txttipopen.Enabled = true;
            txtFcedula.Enabled = true;
            btnimprimir2.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;

        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                SendKeys.Send("{TAB}");//Cuando se presiona la tecla enter, este le manda señal a la tecla TAB para que active el evento de traspaso...
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            llenaTab2();
        }

        private void btnimprimir2_Click(object sender, EventArgs e)
        {
            string textop = "SUMA DE PROTECCIÓN : El valor económico de esta cédula será el que rige al momento del fallecimiento segun lo acordado por el Consejo Directivo de la Oficina de Pensiones del Estado de Oaxaca.";

            DateTime actual = DateTime.Now;
            string fechal = Convert.ToString(string.Format("{0:d}", actual).Replace("12:00:00 a. m.", ""));
            string quer = $"select * from datos.fechaletra('{fechal}')";
            List<Dictionary<string, object>> res = globales.consulta(quer);
            string fecha = Convert.ToString(res[0]["fechaletra"]);

            string query = $"select * from  nominas_catalogos.cedula WHERE jpp = '{comboBoxnom.Text}' AND num = {txtnumemp.Text};";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            object[] aux2 = new object[resultado.Count];
            int contador = 0;
            string beneficiarios = string.Empty;
            string porcen = string.Empty;
            string paren = string.Empty;
            string sexo = "";
            if (rbm.Checked) sexo = "M";
            if (rbf.Checked) sexo = "F";

            foreach (Dictionary<string, object> item in resultado)
            {
                try
                {
                    beneficiarios = Convert.ToString(item["benefic"]);
                    porcen = Convert.ToString(item["porcen"]);
                    paren = Convert.ToString(item["paren"]);
                }
                catch
                {

                }
                object[] tt1 = { beneficiarios, porcen, paren };
                aux2[contador] = tt1;
                contador++;
            }

            string titulotipo = "";
            switch (comboBoxnom.SelectedIndex) {
                case 0:
                    titulotipo = "JUBILADO No. ";
                    break;
                case 1:
                    titulotipo = "PENSIONADO No. ";
                    break;
                case 2:
                    titulotipo = "PENSION ALIMENTICIA No. ";
                    break;
                case 3:
                    titulotipo = "PENSIONISTA No. ";
                    break;
            }

            titulotipo += txtnumemp.Text;

            string logo = globales.getImagen(globales.imagenesSispe.logoreportes);


            object[] parametros = { "textop", "fecha", "nombre", "domicilio", "f_nac", "sexo", "tipo_pen", "f_pension", "tipo", "logo" };
            object[] valor = { textop, fecha, txtnombre.Text, txtdireccion.Text, txtfechanac.Text, sexo, txttipopen.Text, txtingreso.Text, titulotipo, logo };
            object[][] enviarParametros = new object[2][];

            enviarParametros[0] = parametros;
            enviarParametros[1] = valor;

            globales.reportes("reporteCedulaJub", "cedulaJub", aux2, "", false, enviarParametros);
            this.Cursor = Cursors.Default;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox2.Checked = true;
                panel2.Visible = true;

                if (string.IsNullOrWhiteSpace(comboBoxnom.Text)) return;
                string quer = $"SELECT nosuspen,fechaeje,fechasus FROM nominas_catalogos.maestro where jpp='{comboBoxnom.Text}' and num={txtnumemp.Text};";
                List<Dictionary<string, object>> res = globales.consulta(quer);
                if (res.Count <= 0) return;
                if (!string.IsNullOrWhiteSpace(Convert.ToString(res[0]["nosuspen"])))
                {
                    txtnoSuspen.Text = Convert.ToString(res[0]["nosuspen"]);
                    txtFechaEjec.Text = Convert.ToString(res[0]["fechaeje"]);
                    txtNofecha.Text = Convert.ToString(res[0]["fechasus"]);
                    button2.Enabled = false;
                }
                else
                {

                    txtNofecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaEjec.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    button2.Enabled = true;
                    txtnoSuspen.Select();
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtnoSuspen.Text)) return;
            string query = $"UPDATE nominas_catalogos.maestro set fechasus='{txtNofecha.Text}' ,fechaeje='{txtFechaEjec.Text}',nosuspen='{txtnoSuspen.Text}' where jpp='{comboBoxnom.Text}' and num='{txtnumemp.Text}'";
            try
            {
                globales.consulta(query);

            }
            catch
            {
                return;
            }
            DialogResult dialogo = globales.MessageBoxSuccess("Se suspendio de forma correcta", "Aviso", globales.menuPrincipal);
            panel2.Visible = false;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (checkBox2.Checked)
            {
                panel2.Visible = true;
            }
            if (checkBox3.Checked)
            {
                panel7.Visible = true;
            }



        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox3.Checked)
            {
                panel7.Visible = true;
                if (string.IsNullOrWhiteSpace(comboBoxnom.Text)) return;


                string quer = $"SELECT num_susp_seguro,fec_susp_seguro,fec_susp_ejec FROM nominas_catalogos.maestro where jpp='{comboBoxnom.Text}' and num={txtnumemp.Text};";
                List<Dictionary<string, object>> res = globales.consulta(quer);
                if (!string.IsNullOrWhiteSpace(Convert.ToString(res[0]["num_susp_seguro"])))
                {
                    txtNosuspSeguro.Text = Convert.ToString(res[0]["num_susp_seguro"]);
                    txtFecSuspSeguro.Text = Convert.ToString(res[0]["fec_susp_seguro"]);
                    txtFecSuspEjecSeguro.Text = Convert.ToString(res[0]["fec_susp_ejec"]);

                    btnacepseguro.Enabled = false;
                }
                else
                {
                    txtFecSuspEjecSeguro.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFecSuspSeguro.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    btnacepseguro.Enabled = true;
                }
            }
        }

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    panel2.Visible = false;
        //}

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void btnacepseguro_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNosuspSeguro.Text))
            {
                string query = $"update nominas_catalogos.maestro set num_susp_seguro='{txtNosuspSeguro.Text}',fec_susp_seguro='{txtFecSuspSeguro.Text}',fec_susp_ejec='{txtFecSuspEjecSeguro.Text}' where jpp='{comboBoxnom.Text}' and num={txtnumemp.Text};";
                try
                {
                    globales.consulta(query);
                    DialogResult doialo = globales.MessageBoxSuccess("SE ACTUALIZO AL JUBILADO CORRECTAMENTE", "AVISO", globales.menuPrincipal);


                }
                catch
                {
                    DialogResult dia = globales.MessageBoxError("OCURRIO UN ERROR CONTACTE A SISTEMAS", "UPS!", globales.menuPrincipal);

                }
                panel7.Visible = false;

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            modalEmpleados empleado = new modalEmpleados();
            empleado.enviar = llenacampos;
            globales.showModal(empleado);
            txtrfc.Select();
        }

        private void llenacampos(Dictionary<string, object> datos)
        {
            txtnombre.Text = Convert.ToString(datos["nombre_em"]);
            txtrfc.Text = Convert.ToString(datos["rfc"]);
            string rfc = Convert.ToString(datos["rfc"]);
            string año = string.Empty;
            string fecha_nac = string.Empty;
            if (año == "")
            {

            }
            else
            {
                año = rfc.Substring(4, 2);
                string mes = rfc.Substring(6, 2);
                string dia = rfc.Substring(8, 2);
                fecha_nac = dia + "/" + mes + "/19" + año;
            }

            txtcurp.Text = Convert.ToString(datos["curp"]);
            txtdireccion.Text = Convert.ToString(datos["direccion"]);
            txtfechanac.Text = fecha_nac;
        }

        private void txtnombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                button5_Click(null, null);
                txtrfc.Select();

            }
        }

        private void frminfojub_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (txtcorreo.CanSelect==true)
                {
                    return;
                }

                char S;

                S = Char.ToUpper(e.KeyChar);

                e.KeyChar = S;

            }
        }

        private void datacedula_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                string id = Convert.ToString(datacedula.Rows[c].Cells[3].Value);
                if (string.IsNullOrWhiteSpace(id)) return;
                string query = $"delete from nominas_catalogos.cedula where id='{id}'";
                globales.consulta(query);
                datacedula.Rows.Clear();
                llenagrid();
            }
        }

        private void datacedula_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            c = e.RowIndex;
            if (c == -1) return;
        }


        public void llenagrid()
        {
            string query = $"SELECT * FROM nominas_catalogos.cedula where jpp='{comboBoxnom.Text}' and num={txtnumemp.Text};";
            List<Dictionary<string, object>> cedula = globales.consulta(query);
            foreach (var item in cedula)
            {
                string beneficiarios = Convert.ToString(item["benefic"]);
                string porcen = Convert.ToString(item["porcen"]);
                string parentesco = Convert.ToString(item["paren"]);
                string id = Convert.ToString(item["id"]);
                datacedula.Rows.Add(beneficiarios, porcen + "%", parentesco, id);
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxnom_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            switch (comboBoxnom.SelectedIndex) {
                case 0:
                    lbltitulo.Text = "JUBILADO";
                    break;
                case 1:
                    lbltitulo.Text = "PENSIONADO";
                    break;
                case 2:
                    lbltitulo.Text = "PENSION ALIMENTICIA";
                    break;
                case 3:
                    lbltitulo.Text = "PENSIONISTA";
                    break;
            }

            if (comboBoxnom.SelectedIndex == 2)
            {

                panelPensionalimenticia.Enabled = true;
                panelPensionalimenticia.Visible = true;
                tabPage2.Visible = false;

            }
            else {
                try {
                    panelPensionalimenticia.Enabled = false;
                    panelPensionalimenticia.Visible = false;
                    tabPage2.Visible = true;
                }
                catch {
                }
            }



            comboBoxnom_Leave(null,null);

            if (checkBox1.Checked) {
                checkBox1_CheckedChanged(null,null);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmModalJubilados jubi = new frmModalJubilados(true);
            jubi.enviar = rellenarPension;
            globales.showModal(jubi);
        }

        public void rellenarPension(Dictionary<string, object> item) {

            clavesVinculadas = string.Empty;

            foreach (DataGridViewRow i in dtggrid.Rows) {
                string jpp = Convert.ToString(i.Cells[0].Value);
                string numjpp = (Convert.ToString(i.Cells[1].Value));



                if ((jpp+numjpp).Contains(Convert.ToString(item["jpp"]) + Convert.ToString(item["num"]))) {

                    globales.MessageBoxExclamation($"Ya se encuentra {Convert.ToString(item["jpp"]) + Convert.ToString(item["num"])} en esta pensión alimenticia.\nVerifique con el encargado del departamento","Aviso",this);

                    return;
                }
            }

            txtNumerobuscar.Text = Convert.ToString(item["jpp"]) + Convert.ToString(item["num"]);
            txtrfc1.Text = Convert.ToString(item["rfc"]);
            txtnombre1.Text = Convert.ToString(item["nombre"]);
            txtporcentaje1.Text = "0%";
            txttotal1.Text = string.Format("{0:c}", 0);
            button9.Enabled = true;
            button8.Enabled = true;


            string query = $"select sum(monto) as importe from nominas_catalogos.nominew where  jpp = '{item["jpp"]}' and numjpp = {item["num"]} and tipo_nomina = 'N' and clave <= 69";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            txtsueldo1.Text = globales.convertMoneda(globales.convertDouble(Convert.ToString(resultado[0]["importe"])));

            //dtggrid.Rows.Add(item["jpp"], item["num"], item["rfc"], item["nombre"]);
        }

        private void dtggrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtggrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) {
                return;
            }

            int row = e.RowIndex;
            if (e.ColumnIndex == 4) {
                calcularTotalCelda(row);
            }
        }

        private void calcularTotalCelda(int row)
        {
            string jpp = Convert.ToString(dtggrid.Rows[row].Cells[0].Value);
            int numjpp = globales.convertInt(Convert.ToString(dtggrid.Rows[row].Cells[1].Value));
            dtggrid.Rows[row].Cells[4].Value = globales.convertInt(Convert.ToString(dtggrid.Rows[row].Cells[4].Value).Replace("%", "").Trim()) + "%";
            double porcentaje = globales.convertInt(Convert.ToString(dtggrid.Rows[row].Cells[4].Value).Replace("%", "").Trim());
        }

        private void dtggrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        private void viendoEdicion(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void dtggrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            indexrow = e.RowIndex;

        }

        private void txtporcentaje1_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtporcentaje1.Text)) {
                return;
            }

            double porcentaje = globales.convertDouble(txtporcentaje1.Text.Replace("%", ""));
            double sueldo = globales.convertDouble(txtsueldo1.Text);

            double total = (porcentaje * sueldo) / 100;

            txttotal1.Text = globales.convertMoneda(total);
        }

        private void txtporcentaje1_Leave(object sender, EventArgs e)
        {
            txtporcentaje1.Text = globales.convertDouble(txtporcentaje1.Text.Replace("%","")).ToString() + "%";

            double porcentaje = globales.convertDouble(txtporcentaje1.Text.Replace("%", ""));
            double sueldo = globales.convertDouble(txtsueldo1.Text);

            double total = (porcentaje * sueldo) / 100;

            txttotal1.Text = globales.convertMoneda(total);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string jpp = txtNumerobuscar.Text.Substring(0, 3);
            string numero = txtNumerobuscar.Text.Substring(3);

            if (modificarPea)
            {
                this.dtggrid.Rows[this.indexrow].Cells[4].Value = txtporcentaje1.Text;
                this.dtggrid.Rows[this.indexrow].Cells[5].Value = txttotal1.Text;
                this.dtggrid.Rows[this.indexrow].Cells[9].Value = this.clavesVinculadas;
            }
            else {
                dtggrid.Rows.Add(jpp, numero, txtrfc1.Text, txtnombre1.Text, txtporcentaje1.Text, txttotal1.Text,0,0,0,clavesVinculadas);
            }

            txtNumerobuscar.Text = string.Empty;
            txtrfc1.Text = string.Empty;
            txtnombre1.Text = string.Empty;
            txtsueldo1.Text = string.Empty;
            txtporcentaje1.Text = string.Empty;
            txttotal1.Text = string.Empty;
            this.button8.Enabled = false;
            this.button9.Enabled = false;


            button8.Text = "Agregar";
            this.modificarPea = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (claves == null) {
                frmOpcionesPercepciones frm = new frmOpcionesPercepciones(claves, txtNumerobuscar.Text,this.clavesVinculadas);
                frm.enviar = this.insertartotal;
                globales.showModal(frm);
            }
        }

        private void insertartotal(string total,string clavesVinculadas) {
            this.txtsueldo1.Text = total;
            this.clavesVinculadas = clavesVinculadas;
        }

        private void txtsueldo1_TextChanged(object sender, EventArgs e)
        {
            txtporcentaje1_Leave(null,null);
        }

        private void dtggrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
          
        }

        private void dtggrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtggrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridViewRow dato = e.Row;

            pension_alimenticia pension = new pension_alimenticia();
            pension.jpp = Convert.ToString(dato.Cells[0].Value);
            pension.numjpp = globales.convertInt(Convert.ToString(dato.Cells[1].Value));
            pension.numpea = globales.convertInt(txtnumemp.Text);
            pension.descuento = globales.convertDouble(Convert.ToString(dato.Cells[4].Value).Replace("%",""));
            pension.total = globales.convertDouble(Convert.ToString(dato.Cells[5].Value));
            pension.id = globales.convertInt(Convert.ToString(dato.Cells[6].Value));
            pension.id_enlace = globales.convertInt(Convert.ToString(dato.Cells[7].Value));
            pension.id_enlacepea = globales.convertInt(Convert.ToString(dato.Cells[8].Value));

            if (pension.id != 0) {
                listaeliminar.Add(pension);
            }
        }


        private List<pension_alimenticia> listaeliminar = new List<pension_alimenticia>();
        private bool modificarPea;
        private string clavesVinculadas = string.Empty;

        private void dtggrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow datos = dtggrid.Rows[e.RowIndex];

            string jpp = Convert.ToString(datos.Cells[0].Value);
            string numjpp = Convert.ToString(datos.Cells[1].Value);

            txtNumerobuscar.Text = jpp + numjpp;


            txtporcentaje1.Text = Convert.ToString(datos.Cells[4].Value);
            txttotal1.Text = Convert.ToString(datos.Cells[5].Value);

            double total = (globales.convertDouble(txttotal1.Text) * 100) / globales.convertDouble(txtporcentaje1.Text.Replace("%", ""));

            if ( double.IsNaN(total)) {
                total = 0;
            }

            txtsueldo1.Text = globales.convertMoneda(total);
            string query = $"select *  from nominas_catalogos.maestro where jpp = '{jpp}' and num = {numjpp}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
            if (resultado.Count != 0) {
                Dictionary<string, object> diccionario = resultado[0];
                txtnombre1.Text = Convert.ToString(diccionario["nombre"]);
                txtrfc1.Text = Convert.ToString(diccionario["rfc"]);
            }

            button8.Enabled = true;
            button9.Enabled = true;


            button8.Text = "Modificar";
            this.modificarPea = true;

            //Claves vinculadas

            this.clavesVinculadas = Convert.ToString(datos.Cells[9].Value);

        }

 
    }

    delegate void metodo(string v,string clavesViculadas);
}


