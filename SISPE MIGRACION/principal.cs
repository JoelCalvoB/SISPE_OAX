using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.formularios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION
{
    static class principal
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //**********************************************************
            //**       ICONOS PARA EL SISTEMAS PÁGINA                 **
            //**   https://icons8.com/icon/new-icons/office           **
            //**********************************************************

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string host = SISPE_MIGRACION.Properties.Resources.servidor;
            string usuario = SISPE_MIGRACION.Properties.Resources.usuario;
            string password = SISPE_MIGRACION.Properties.Resources.password;
            string database = SISPE_MIGRACION.Properties.Resources.baseDatos;
            string port = SISPE_MIGRACION.Properties.Resources.puerto;


            //host = "ec2-23-21-160-38.compute-1.amazonaws.com";
            //usuario = "hwvzppntjiviyu";
            //password = "8ec67b7ca03d1e00ba4ac06dc6cdf97e148da0ddc2b7bb69523dec23cdde5256";
            //database = "ddboilk04tmcso";
            //SSL Mode = Require; Trust Server Certificate = true
            string queryConexion = string.Format("Host={0};Username={1};Password={2};Database={3};port={4};", host, usuario, password, database, port);

            string ip = principal.obtenerIp();

            
                if (!ip.Contains("10.172.23") && host.ToLower() != "localhost")
                {

                    MessageBox.Show("TE ESTAS CONCECTANDO AL SISPE DE INTERNET", "IMPORTANTE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    queryConexion = string.Format("Host={0};Username={1};Password={2};Database={3};port={4};", "pensionesoaxaca.com", usuario, password, database, "3333");
                }
            

            globales.datosConexion = queryConexion;

            if (baseDatos.realizarConexion(queryConexion))
            {


                try
                {


                    string query = "select jpp,numjpp,clave,secuen,count(*) from nominas_catalogos.nominew where tipopago = 'N' and tipo_nomina = 'N' group by jpp,numjpp,clave,secuen having count(*) > 1 order by count(*) desc";
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    foreach (var item in resultado) {
                        query = $"SELECT ID FROM nominas_catalogos.nominew  where jpp = '{item["jpp"]}' and numjpp = {item["numjpp"]} and tipo_nomina = 'N' and tipopago = 'N' and clave = {item["clave"]} order by ID  ";
                        List<Dictionary<string, object>> rr = globales.consulta(query);
                        int contador = 1;
                        foreach (Dictionary<string,object> ii in rr) {

                            string nn = $"update nominas_catalogos.nominew set secuen = {contador}  where id = {ii["id"]}";
                            globales.consulta(nn);
                            contador++;

                        }
                    }


                    string[] ipArr = ip.Split('.');
                    string ultimonumero = ipArr[3];
                    if (ultimonumero == "241" || ultimonumero == "198" || ultimonumero == "148" || ultimonumero == "199")
                    {
                        if (host.Contains("192.168.100."))
                        {
                            MessageBox.Show("Cuidado se esta trabajando con la base de datos servidor 192.168.100.101", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        if (host.Contains("10.172.23."))
                        {
                            MessageBox.Show("Base de datos incorrecta, verificar con sistemas..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Atención el servidor 192.168.100.102 donde se encuentra las aportaciones perdidas del año 2011 al 2015 no se encuentra disponible, puede continuar su flujo de trabajo con normalidad pero no se podrá consultar información de esa base de datos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Application.Run(new login());///
            }
        }


        //Método para obtener una ip
        public static string obtenerIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No existe adaptador de red");
        }
    }
}
