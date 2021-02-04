using SISPE_MIGRACION.formularios.CATÁLOGOS.modales_catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.CONSTANCIAS.REGISTRO
{
    public partial class frmmodificar : Form
    {
        public string folio_R = "";

        public frmmodificar(string folio = "" )
        {
            this.folio_R = folio;

            InitializeComponent();
            rellena();
            

            

        }

        public void rellena()
        {
            if (string.IsNullOrWhiteSpace(this.folio_R) == true)
            {
                txtfolio.Text = "AUTOGENERADO";
                DateTime fechaactual = DateTime.Now;
                txtfecha.Text = String.Format("{0:d}", fechaactual);
            }
            else {
                

                string query = $"SELECT * FROM catalogos.consta where folio={this.folio_R};";
                List<Dictionary<string, object>> resul = globales.consulta(query);


                txtfolio.Text = Convert.ToString(resul[0]["folio"]);
                int tipo = Convert.ToInt32(resul[0]["tipo"]);
                cmbTipo2.SelectedIndex = tipo - 1;
                txtfecha.Text = Convert.ToString(resul[0]["fecha"]);
                txtnombre.Text = Convert.ToString(resul[0]["nombre"]);
                txtrfc.Text = Convert.ToString(resul[0]["rfc"]);

                fecha1.Text = globales.parseDateTime(globales.convertDatetime(Convert.ToString(resul[0]["fecha1"])));
                fecha2.Text = globales.parseDateTime(globales.convertDatetime(Convert.ToString(resul[0]["fecha2"])));

                txtprestamos.Text = Convert.ToString(resul[0]["prestamos"]);
            }



        }







        public void rellena2()
        {
            /*
            //Tabla de constancias generales, rellenar modificar
            string query2 = $"SELECT * FROM catalogos.constag where folio={this.folio_R};";
            List<Dictionary<string, object>> resul2 = globales.consulta(query2);

            txtfolio.Text = Convert.ToString(resul2[0]["folio"]);
            string tipo = Convert.ToString(resul2[0]["tipo"]);
            txttipo.Text = tipo;
            txtfecha.Text = Convert.ToString(resul2[0]["fecha"]);
            txtnombre.Text = Convert.ToString(resul2[0]["nombre"]);
            txtrfc.Text = Convert.ToString(resul2[0]["rfc"]);

            string consta = string.Empty;

            if (tipo == "1") consta = "No Adeudo";
            if (tipo == "2") consta = "Adeudo";
            if (tipo == "3") consta = "Con Pagos";

            label7.Text = consta;
            */
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void frmmodificar_Load(object sender, EventArgs e)
        {
            cmbTipo2.SelectedIndex = 0;
            if (!string.IsNullOrWhiteSpace(this.folio_R))
            {
                rellena();
            }
            else
            {
                txtfolio.Text = "AUTOGENERADO";

            }
        }

        private void btnRfc1_Click(object sender, EventArgs e)
        {
            modalEmpleados empleados = new modalEmpleados();
            empleados.enviar = rellenar;
            globales.showModal(empleados);
        }

        public void rellenar(Dictionary<string, object> datos)
        {
            string rfc = Convert.ToString(datos["rfc"]);
            string nombre = Convert.ToString(datos["nombre_em"]);


            txtrfc.Text = rfc;
            txtnombre.Text = nombre;
        }

        private void txtfolio_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtrfc_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        private void aceptar(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(folio_R))
            {
                DialogResult resultado = globales.MessageBoxQuestion("¿Deseas insertar el registro?", "Aviso", globales.menuPrincipal);

                if (resultado == DialogResult.Yes)
                {
                    int tipo = cmbTipo2.SelectedIndex + 1;
                    string fechaStr = "";
                    try
                    {
                        DateTime fechaDt = DateTime.Parse(txtfecha.Text);
                        fechaStr = string.Format("{0:yyyy-MM-dd}", fechaDt);
                    }
                    catch
                    {
                        globales.MessageBoxExclamation("Favor de ingresar fecha de constancia", "Aviso", globales.menuPrincipal);
                        return;
                    }

                    string fechaStr1 = "";

                    try
                    {
                        DateTime fechaDt1 = DateTime.Parse(fecha1.Text);
                        fechaStr1 = "'" + string.Format("{0:yyyy-MM-dd}", fechaDt1) + "'";
                    }
                    catch
                    {
                        fechaStr1 = "null";
                    }


                    string fechaStr2 = "";
                    try
                    {
                        DateTime fechaDt2 = DateTime.Parse(fecha2.Text);
                        fechaStr2 = "'" + string.Format("{0:yyyy-MM-dd}", fechaDt2) + "'";
                    }
                    catch
                    {
                        fechaStr2 = "null";
                    }


                    string query = "select (max(folio) + 1) as folio from catalogos.consta";
                    List<Dictionary<string, object>> resultado2 = globales.consulta(query);
                    int folio = Convert.ToInt32(resultado2[0]["folio"]);


                    query = $"insert into catalogos.consta (rfc,nombre,tipo,fecha,prestamos,fecha1,fecha2,folio) values('{txtrfc.Text}','{txtnombre.Text}',{tipo},'{fechaStr}','{txtprestamos.Text}',{fechaStr1},{fechaStr2},{folio} ) ;";

                    globales.consulta(query, true);

                    globales.MessageBoxSuccess("Se a insertador correctamente", "Aviso", globales.menuPrincipal);
                    DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas imprimir la constancia?", "Aviso", globales.menuPrincipal);


                    if (dialogo == DialogResult.Yes)
                    {
                        if (cmbTipo2.SelectedIndex == 0)
                        {
                            object[] obj = { };


                            object[][] parametros = new object[2][];
                            string linea1 = "Que el(la) C. " + txtnombre.Text + "    " + txtrfc.Text;

                            DateTime fecha = DateTime.Parse(txtfecha.Text);

                            string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                            string mes = meses[fecha.Month];

                            string leyendalicencia = "Se extiende la presente a petición de el(la) interesado(a) y para los efectos a que haya lugar ante la Dirección de Recursos Humanos de la Secretaría de Administración  del Poder  Ejecutivo del Gobierno del Estado";

                            string fechalarga = "Oaxaca de Juárez Oax., a " + string.Format("{0:dd}", fecha) + " de " + mes + " del año " + fecha.Year;

                            object[] header = { "folio", "primera", "leyendalicencia", "fechalarga" };
                            object[] body = { folio.ToString(), linea1, leyendalicencia, fechalarga };

                            parametros[0] = header;
                            parametros[1] = body;


                            globales.reportes("constancia_constancianoadeudo", "prueba_randy", obj, "", false, parametros);
                        }
                        else if (cmbTipo2.SelectedIndex == 2)
                        {
                            object[] obj = { };


                            object[][] parametros = new object[2][];


                            string linea1 = "Que el(la) C. " + txtnombre.Text + "    " + txtrfc.Text;

                            DateTime fecha = DateTime.Parse(txtfecha.Text);


                            string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                            string mes = meses[fecha.Month];

                            string leyendalicencia = "Se extiende la presente a petición de el(la) interesado(a) y para los efectos a que haya lugar ante la Dirección de Recursos Humanos de la Secretaría de Administración  del Poder  Ejecutivo del Gobierno del Estado";

                            string fechalarga = "Oaxaca de Juárez Oax., a " + string.Format("{0:dd}", fecha) + " de " + mes + " del año " + fecha.Year;

                            object[] header = { "folio", "primera","prestamos","fecha1","fecha2", "leyendalicencia", "fechalarga" };
                            object[] body = { folio.ToString(), linea1,txtprestamos.Text, fecha1.Text, fecha2.Text, leyendalicencia, fechalarga };

                            parametros[0] = header;
                            parametros[1] = body;



                            globales.reportes("constancias_constancialicencia", "prueba_randy", obj, "", false, parametros);
                        }

                        else if (cmbTipo2.SelectedIndex == 1)
                        {
                            object[] obj = { };


                            object[][] parametros = new object[2][];


                            string linea1 = "Que el(la) C. " + txtnombre.Text + "    " + txtrfc.Text;

                            DateTime fecha = DateTime.Parse(txtfecha.Text);


                            string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                            string mes = meses[fecha.Month];

                            string leyendalicencia = "Se extiende la presente a petición de el(la) interesado(a) y para los efectos a que haya lugar ante la Dirección de Recursos Humanos de la Secretaría de Administración  del Poder  Ejecutivo del Gobierno del Estado";

                            string fechalarga = "Oaxaca de Juárez Oax., a " + string.Format("{0:dd}", fecha) + " de " + mes + " del año " + fecha.Year;

                            object[] header = { "folio", "primera","prestamos", "leyendalicencia", "fechalarga" };
                            object[] body = { folio.ToString(), linea1,txtprestamos.Text, leyendalicencia, fechalarga };

                            parametros[0] = header;
                            parametros[1] = body;



                            globales.reportes("constancias_constanciageneral", "prueba_randy", obj, "", false, parametros);
                        }

                        else
                        {

                        }
                    }

                    this.Owner.Close();

                }
            }
            else
            {
                DialogResult resultado = globales.MessageBoxQuestion("¿Deseas actualizar el registro?", "Aviso", globales.menuPrincipal);

                if (resultado == DialogResult.Yes)
                {
                    int tipo = cmbTipo2.SelectedIndex + 1;
                    string fechaStr = "";
                    try
                    {
                        DateTime fechaDt = DateTime.Parse(txtfecha.Text);
                        fechaStr = string.Format("{0:yyyy-MM-dd}", fechaDt);
                    }
                    catch
                    {
                        globales.MessageBoxExclamation("Favor de ingresar fecha de constancia", "Aviso", globales.menuPrincipal);
                        return;
                    }

                    string fechaStr1 = "";

                    try
                    {
                        DateTime fechaDt1 = DateTime.Parse(fecha1.Text);
                        fechaStr1 = "'" + string.Format("{0:yyyy-MM-dd}", fechaDt1) + "'";
                    }
                    catch
                    {
                        fechaStr1 = "null";
                    }


                    string fechaStr2 = "";
                    try
                    {
                        DateTime fechaDt2 = DateTime.Parse(fecha2.Text);
                        fechaStr2 = "'" + string.Format("{0:yyyy-MM-dd}", fechaDt2) + "'";
                    }
                    catch
                    {
                        fechaStr2 = "null";
                    }



                    string query = $"update catalogos.consta set rfc = '{txtrfc.Text}',nombre = '{txtnombre.Text}',tipo = {tipo},fecha = '{fechaStr}',prestamos = '{txtprestamos.Text}',fecha1 = {fechaStr1},fecha2={fechaStr2}  where folio={this.folio_R};";

                    globales.consulta(query, true);

                    globales.MessageBoxSuccess("Se a actualizado correctamente", "Aviso", globales.menuPrincipal);
                    DialogResult dialogo = globales.MessageBoxQuestion("¿Deseas imprimir la constancia?", "Aviso", globales.menuPrincipal);


                    if (dialogo == DialogResult.Yes)
                    {
                        if (cmbTipo2.SelectedIndex == 0)
                        {

                            object[] obj = { };


                            object[][] parametros = new object[2][];


                            string linea1 = "Que el(la) C. " + txtnombre.Text + "    " + txtrfc.Text;

                            DateTime fecha = DateTime.Parse(txtfecha.Text);

                            string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                            string mes = meses[fecha.Month];

                            string leyendalicencia = "Se extiende la presente a petición de el(la) interesado(a) y para los efectos a que haya lugar ante la Dirección de Recursos Humanos de la Secretaría de Administración  del Poder  Ejecutivo del Gobierno del Estado";

                            string fechalarga = "Oaxaca de Juárez Oax., a " + string.Format("{0:dd}", fecha) + " de " + mes + " del año " + fecha.Year;

                            object[] header = { "folio", "primera", "leyendalicencia", "fechalarga" };
                            object[] body = { txtfolio.Text, linea1, leyendalicencia, fechalarga };

                            parametros[0] = header;
                            parametros[1] = body;



                            globales.reportes("constancia_constancianoadeudo", "prueba_randy", obj, "", false, parametros);
                        }
                        else if (cmbTipo2.SelectedIndex == 2)
                        {
                            object[] obj = { };


                            object[][] parametros = new object[2][];


                            string linea1 = "Que el(la) C. " + txtnombre.Text + "    " + txtrfc.Text;

                            DateTime fecha = DateTime.Parse(txtfecha.Text);
                            

                            string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                            string mes = meses[fecha.Month];

                            string leyendalicencia = "Se extiende la presente a petición de el(la) interesado(a) y para los efectos a que haya lugar ante la Dirección de Recursos Humanos de la Secretaría de Administración  del Poder  Ejecutivo del Gobierno del Estado";

                            string fechalarga = "Oaxaca de Juárez Oax., a " + string.Format("{0:dd}", fecha) + " de " + mes + " del año " + fecha.Year;

                            object[] header = { "folio", "primera","prestamos","fecha1", "fecha2", "leyendalicencia", "fechalarga" };
                            object[] body = { txtfolio.Text, linea1,txtprestamos.Text,fecha1.Text,fecha2.Text, leyendalicencia, fechalarga };

                            parametros[0] = header;
                            parametros[1] = body;



                            globales.reportes("constancias_constancialicencia", "prueba_randy", obj, "", false, parametros);
                        }

                        else if (cmbTipo2.SelectedIndex == 1)
                        {
                            object[] obj = { };


                            object[][] parametros = new object[2][];


                            string linea1 = "Que el(la) C. " + txtnombre.Text + "    " + txtrfc.Text;

                            DateTime fecha = DateTime.Parse(txtfecha.Text);


                            string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                            string mes = meses[fecha.Month];

                            string leyendalicencia = "Se extiende la presente a petición de el(la) interesado(a) y para los efectos a que haya lugar ante la Dirección de Recursos Humanos de la Secretaría de Administración  del Poder  Ejecutivo del Gobierno del Estado";

                            string fechalarga = "Oaxaca de Juárez Oax., a " + string.Format("{0:dd}", fecha) + " de " + mes + " del año " + fecha.Year;

                            object[] header = { "folio", "primera","prestamos", "leyendalicencia",  "fechalarga" };
                            object[] body = { txtfolio.Text, linea1, txtprestamos.Text, leyendalicencia,  fechalarga };

                            parametros[0] = header;
                            parametros[1] = body;



                            globales.reportes("constancias_constanciageneral", "prueba_randy", obj, "", false, parametros);
                        }

                        else
                        {

                        }
                    }

                    this.Owner.Close();

                }
            }
        }

    }

}
