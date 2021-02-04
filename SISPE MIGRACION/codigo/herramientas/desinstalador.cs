using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Management.Instrumentation;
using Microsoft.Win32;

namespace SISPE_MIGRACION.codigo.herramientas
{

        public class desintalador
    {
        public static void actualizando()
        {
            try
            {

                general actualizando = new desinstalando();
                actualizando.actualizar();
                actualizando = new instalando();
                actualizando.actualizar();
            }
            catch
            {

            }
        }
    }



    #region clase abstracta que llama dependiendo a que desinstalar o instalar la aplicacion de pcpay....
    public abstract class general
    {

        public abstract void actualizar();
        protected bool handler { get; set; }
        public List<string> getKeyPrograma(string productName)
        {

            System.Diagnostics.Debug.WriteLine("Obteniendo programas");
            List<string> Guids = new List<string>();
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {

                            string displayName = Convert.ToString(sk.GetValue("DisplayName"));
                            string size = Convert.ToString(sk.GetValue("EstimatedSize"));
                            string guid = Convert.ToString(sk.GetValue("UninstallString"));
                            if (displayName != null)
                            {

                                if (displayName.Contains("instalador"))
                                {
                                    guid = guid.Substring(guid.IndexOf('{'));
                                    Guids.Add(guid);
                                }
                            }
                        }
                        catch (Exception ex)
                        { }
                    }
                }
            }

            return Guids;
        }
        protected void ejecuta(string argumento)
        {
            handler = true;
            string carpetaSys = "";
            string fileName;
            string ruta;
            string exe;
            ruta = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            fileName = "msiexec.exe";

            if (Environment.Is64BitOperatingSystem)
                carpetaSys = "SysWOW64";
            else
                carpetaSys = "System32";

            exe = System.IO.Path.Combine(ruta + "\\" + carpetaSys, fileName);


            if (System.IO.File.Exists(exe))
            {
                try
                {
                    Process myProcess = new Process();
                    myProcess.StartInfo.FileName = exe;
                    myProcess.StartInfo.Arguments = argumento;
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.EnableRaisingEvents = true;
                    myProcess.Exited += new EventHandler(myProcess_Exited);
                    myProcess.Start();
                    while (handler)
                    {

                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("ocurrio un error al desinstalar versión anterior\nCódigo de error:" + ex.GetBaseException());
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No existe el programa Msiexec.exe, favor de descargarlo");
            }
        }

        private void myProcess_Exited(object sender, EventArgs e)
        {
            
            handler = false;
        }
    }
    #endregion

    #region instalando el pcpay
    public class instalando : general
    {
        public override void actualizar()
        {

            System.Diagnostics.Debug.WriteLine("INSTALANDO");
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\instalador.msi";
            string desinstalar = "/i \"" + path + "\" /qb";
            ejecuta(desinstalar);
        }
    }
    #endregion

    #region desinstalando el pcpay
    public class desinstalando : general
    {
        public override void actualizar()
        {
            System.Diagnostics.Debug.WriteLine("DESINTALANDO");
            List<string> lista = getKeyPrograma("instalador");
            foreach (string item in lista)
            {
                ejecuta("/x \"" + item + "\" /qb");
            }
        }
    }

    #endregion
}
