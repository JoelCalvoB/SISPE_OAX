using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISPE_MIGRACION.formularios.FINANCIEROS.CHEQUES
{
    class procesoDbf
   
    {
        string cadena= string.Empty;

        public string convertirDBFtoText(bool pol)
        {
            DateTime fecha = DateTime.Now;
            string año = Convert.ToString(fecha.Year).Substring(2, 2);

            string tabla = pol ? $"pol" : $"des";
     //       string tabla = pol ? $"[fondos{año}].[dbo].[Pol]" : $"[fondos{año}].[dbo].[Des]";

            string query = $"select * from financieros.{tabla}";
            List<Dictionary<string, object>> resultado = globales.consulta(query);
             cadena = string.Empty;
            foreach (Dictionary<string,object> item in resultado) {
                string aux1 =   "insert into  {1} ({2}) values ({0});\n"; ;
                string adentro = string.Empty;
                string cuerpo = string.Empty;
                foreach (string x in item.Keys) {
                    string valor = "";


                   
                    if (x.ToLower() == "importe" || x.ToLower() == "debe" || x.ToLower() == "haber")
                    {
                        valor = string.IsNullOrWhiteSpace(Convert.ToString(item[x])) ? "0" : Convert.ToString(item[x])+",";
                    }
                    else if (x.ToLower() == "doccomentario")
                    {
                       string samv = (Convert.ToString(item[x]).Length <= 50) ? Convert.ToString(item[x]) : Convert.ToString(item[x]).Substring(0, 50);
                        valor = $"'{samv}',";
                    }

                    else
                    {
                        valor = $"'{Convert.ToString(item[x]).Replace(" 12:00:00 a. m.", "")}',";
                    }

                

                    adentro += valor;
                    cuerpo += x + ",";
                }
                adentro = adentro.Substring(0,adentro.Length-1);
                cuerpo = cuerpo.Substring(0, cuerpo.Length - 1);

                    aux1 = string.Format(aux1, adentro, $"[fondos{año}].[dbo].[{tabla}]"   ,cuerpo);

                cadena += aux1;




            }
            return cadena;
        }
    }
}
