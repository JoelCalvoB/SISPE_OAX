using SISPE_MIGRACION.codigo.repositorios.nominas_catalogos;
using SISPE_MIGRACION.formularios.NOMINAS.CATALOGOS.modal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.NOMINAS.SUPERVIVENCIA
{
    public partial class frmListadoNoNomina : Form
    {
        private string jpp;
        private maestro maestro;
        private string num;

        public frmListadoNoNomina()
        {
            InitializeComponent();
        }

        private void btnFolio_Click(object sender, EventArgs e)
        {
            frmModalJubilados jubi = new frmModalJubilados();
            jubi.enviar = rellenar;
            globales.showModal(jubi);
        }

        private void rellenar(Dictionary<string, object> diccinario)
        {

            supervive.Visible = true;
            dbaseORM orm = new dbaseORM();
            maestro = orm.getObject<maestro>(diccinario);

            txtNumerobuscar.Text = $"{maestro.jpp}{maestro.num}";
            this.jpp = maestro.jpp;
            this.num = Convert.ToString(maestro.num);
            txtNum.Text = Convert.ToString(maestro.num);
            txtRfc.Text = maestro.rfc;
            txtNombre.Text = maestro.nombre;
            txtDireccion.Text = maestro.domicilio;
            txtCat.Text = maestro.categ;
            txtTel.Text = maestro.telefono;
            txtSexo.Text = maestro.sexo;
            txtEle.Text = maestro.nomelec;
            txtProyecto.Text = maestro.proyecto;
            txtFin.Text = globales.parseDateTime(maestro.fching);
            txtLeyenda.Text = maestro.leyen;
            txtNCuenta.Text = maestro.cuentabanc;
            txtBanco.Text = maestro.banco;
            txtImss.Text = maestro.imss;
            txtCurp.Text = maestro.curp;
            txtFirma.Text = maestro.superviven;
            txtFecha.Text = globales.parseDateTime(maestro.fsupervive);

            preview.Visible = true;

            int periodo = 0;

            if (DateTime.Now.Month >= 1 && DateTime.Now.Month < 5) {
                periodo = 1;
                label27.Text = $"PERIODO 01 A 21 MAYO DEL {DateTime.Now.Year}";
                label27.BackColor = Color.LightBlue;

                label29.Text = $"PERIODO 01 A 21 SEPTIEMBRE DEL {DateTime.Now.Year}";
                label29.BackColor = Color.LightBlue;

                label26.Text = "PROXIMOS";
                label26.BackColor = Color.LightBlue;

                label28.Text = "PROXIMOS";
                label28.BackColor = Color.LightBlue;


                string query = $"select * from nominas_catalogos.supervive where jpp = '{maestro.jpp}' AND numjpp = {maestro.num} and anio = {DateTime.Now.Year} and periodo = 1 ORDER BY periodo ";
                List<supervive> listasupervive = orm.queryForList<supervive>(query);
                bool encontrado = listasupervive.Any(o=>o.periodo == 1);
                if (encontrado)
                {
                    label25.Text = $"PERIODO 01 A 21 ENERO DEL {DateTime.Now.Year}";
                    label25.BackColor = Color.LightGreen;

                    label24.Text = "FIRMADO";
                    label24.BackColor = Color.LightGreen;

                    pictureBox5.Visible = true;
                    pictureBox6.Visible = false;
                }
                else {
                    label25.Text = $"PERIODO 01 A 21 ENERO DEL {DateTime.Now.Year}";
                    label25.BackColor = Color.Pink;

                    label24.Text = "NO FIRMADO";
                    label24.BackColor = Color.Pink;

                    pictureBox5.Visible = false;
                    pictureBox6.Visible = true;
                }
            } else if (DateTime.Now.Month >= 5 && DateTime.Now.Month < 9) {
                periodo = 2;
                label29.Text = $"PERIODO 01 A 21 SEPTIEMBRE DEL {DateTime.Now.Year}";
                label29.BackColor = Color.LightBlue;

                label28.Text = "PROXIMOS";
                label28.BackColor = Color.LightBlue;


                string query = $"select * from nominas_catalogos.supervive where jpp = '{maestro.jpp}' AND numjpp = {maestro.num} and anio = {DateTime.Now.Year} and periodo in (1,2) ORDER BY periodo ";
                List<supervive> listasupervive = orm.queryForList<supervive>(query);

                bool encontrado = listasupervive.Any(o => o.periodo == 1);

                if (encontrado)
                {
                    label25.Text = $"PERIODO 01 A 21 ENERO DEL {DateTime.Now.Year}";
                    label25.BackColor = Color.LightGreen;

                    label24.Text = "FIRMADO";
                    label24.BackColor = Color.LightGreen;

                    pictureBox5.Visible = true;
                    pictureBox6.Visible = false;
                }
                else
                {
                    label25.Text = $"PERIODO 01 A 21 ENERO DEL {DateTime.Now.Year}";
                    label25.BackColor = Color.Pink;

                    label24.Text = "NO FIRMADO";
                    label24.BackColor = Color.Pink;

                    pictureBox5.Visible = false;
                    pictureBox6.Visible = true;
                }

                encontrado = listasupervive.Any(o => o.periodo == 2);


                if (encontrado)
                {
                    label27.Text = $"PERIODO 01 A 21 MAYO DEL {DateTime.Now.Year}";
                    label27.BackColor = Color.LightGreen;

                    label26.Text = "FIRMADO";
                    label26.BackColor = Color.LightGreen;

                    pictureBox7.Visible = true;
                    pictureBox8.Visible = false;
                }
                else
                {
                    label27.Text = $"PERIODO 01 A 21 MAYO DEL {DateTime.Now.Year}";
                    label27.BackColor = Color.Pink;

                    label26.Text = "NO FIRMADO";
                    label26.BackColor = Color.Pink;

                    pictureBox7.Visible = false;
                    pictureBox8.Visible = true;
                }

            } else {
                periodo = 3;


                string query = $"select * from nominas_catalogos.supervive where jpp = '{maestro.jpp}' AND numjpp = {maestro.num} and anio = {DateTime.Now.Year} and periodo in (1,2,3) ORDER BY periodo ";
                List<supervive> listasupervive = orm.queryForList<supervive>(query);

                bool encontrado = listasupervive.Any(o => o.periodo == 1);

                if (encontrado)
                {
                    label25.Text = $"PERIODO 01 A 21 ENERO DEL {DateTime.Now.Year}";
                    label25.BackColor = Color.LightGreen;

                    label24.Text = "FIRMADO";
                    label24.BackColor = Color.LightGreen;

                    pictureBox5.Visible = true;
                    pictureBox6.Visible = false;
                }
                else
                {
                    label25.Text = $"PERIODO 01 A 21 ENERO DEL {DateTime.Now.Year}";
                    label25.BackColor = Color.Pink;

                    label24.Text = "NO FIRMADO";
                    label24.BackColor = Color.Pink;

                    pictureBox5.Visible = false;
                    pictureBox6.Visible = true;
                }

                encontrado = listasupervive.Any(o => o.periodo == 2);


                if (encontrado)
                {
                    label27.Text = $"PERIODO 01 A 21 MAYO DEL {DateTime.Now.Year}";
                    label27.BackColor = Color.LightGreen;

                    label26.Text = "FIRMADO";
                    label26.BackColor = Color.LightGreen;

                    pictureBox7.Visible = true;
                    pictureBox8.Visible = false;
                }
                else
                {
                    label27.Text = $"PERIODO 01 A 21 MAYO DEL {DateTime.Now.Year}";
                    label27.BackColor = Color.Pink;

                    label26.Text = "NO FIRMADO";
                    label26.BackColor = Color.Pink;

                    pictureBox7.Visible = false;
                    pictureBox8.Visible = true;
                }

                encontrado = listasupervive.Any(o => o.periodo == 3);


                if (encontrado)
                {
                    label29.Text = $"PERIODO 01 A 21 MAYO DEL {DateTime.Now.Year}";
                    label29.BackColor = Color.LightGreen;

                    label28.Text = "FIRMADO";
                    label28.BackColor = Color.LightGreen;

                    pictureBox9.Visible = true;
                    pictureBox10.Visible = false;
                }
                else
                {
                    label29.Text = $"PERIODO 01 A 21 MAYO DEL {DateTime.Now.Year}";
                    label29.BackColor = Color.Pink;

                    label28.Text = "NO FIRMADO";
                    label28.BackColor = Color.Pink;

                    pictureBox9.Visible = false;
                    pictureBox10.Visible = true;
                }
            }


            sacarPanelAnterior(DateTime.Now.Year-1);
            

           
        }

        private void sacarPanelAnterior(int año)
        {
            string query = $"select * from nominas_catalogos.supervive where jpp = '{maestro.jpp}' AND numjpp = {maestro.num} and anio = {año} and periodo in (1,2,3) ORDER BY periodo ";
            List<supervive> listasupervive = new dbaseORM().queryForList<supervive>(query);


            bool encontrado = listasupervive.Any(o => o.periodo == 1);
            if (encontrado)
            {
                lblerror.Text = $"PERIODO 01 A 21 ENERO DEL {año}";
                lblerror.BackColor = Color.LightGreen;

                label22.Text = "FIRMADO";
                label22.BackColor = Color.LightGreen;

                imgcorrecto.Visible = true;
                imgerror.Visible = false;
            }
            else
            {
                lblerror.Text = $"PERIODO 01 A 21 ENERO DEL {año}";
                lblerror.BackColor = Color.Pink;

                label22.Text = "NO FIRMADO";
                label22.BackColor = Color.Pink;

                imgcorrecto.Visible = false;
                imgerror.Visible = true;
            }


            encontrado = listasupervive.Any(o => o.periodo == 2);
            if (encontrado)
            {
                label20.Text = $"PERIODO 01 A 21 MAYO DEL {año}";
                label20.BackColor = Color.LightGreen;

                label19.Text = "FIRMADO";
                label19.BackColor = Color.LightGreen;

                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
            }
            else
            {
                label20.Text = $"PERIODO 01 A 21 MAYO DEL {año}";
                label20.BackColor = Color.Pink;

                label19.Text = "NO FIRMADO";
                label19.BackColor = Color.Pink;

                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }

            encontrado = listasupervive.Any(o => o.periodo == 3);
            if (encontrado)
            {
                label23.Text = $"PERIODO 01 A 21 SEPTIEMBRE DEL {año}";
                label23.BackColor = Color.LightGreen;

                label21.Text = "FIRMADO";
                label21.BackColor = Color.LightGreen;

                pictureBox3.Visible = true;
                pictureBox4.Visible = false;
            }
            else
            {
                label23.Text = $"PERIODO 01 A 21 SEPTIEMBRE DEL {año}";
                label23.BackColor = Color.Pink;

                label21.Text = "NO FIRMADO";
                label21.BackColor = Color.Pink;

                pictureBox3.Visible = false;
                pictureBox4.Visible = true;
            }
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDebePersonas personas = new frmDebePersonas();
            globales.showModal(personas);
        }
    }
}
