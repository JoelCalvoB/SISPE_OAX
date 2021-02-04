using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SISPE_MIGRACION.codigo.repositorios.ORM
{
    class insert : IORM
    {
        public dynamic construir<clase>(clase obj, bool retornar)
        {
            //Esto lo descodifica y crea el sql necesario para 
            //Que lo consuma mi postgresql... :D ... 
            MemberInfo informacion = typeof(clase);
            string retornarStr = string.Empty;
            if (retornar) retornarStr = " returning *";

            Type tipoAssemby = typeof(clase);

            Dictionary<string, object> columnas = new Dictionary<string, object>();
            PropertyInfo[] propiedades = typeof(clase).GetProperties();
            foreach (PropertyInfo item in propiedades)
            {
                var attributes = item.GetCustomAttributes(false);
                bool esIncrementable = false;
                foreach (var attribute in attributes)
                {
                    if (attribute.GetType() == typeof(autoincrementable))
                    {
                        esIncrementable = true;
                        break;
                    }
                }
                if (!esIncrementable)
                {
                    Dictionary<string, object> tipoyvalor = new Dictionary<string, object>();
                    string nombre = item.Name;
                    string tipo = item.PropertyType.Name;

                    var valor = obj.GetType().GetProperty(nombre).GetValue(obj);

                    tipoyvalor.Add("tipo", tipo);
                    tipoyvalor.Add("valor", valor);
                    columnas.Add(nombre, tipoyvalor);
                }
            }

            string tabla = informacion.Name;
            string[] nombreEsquemaArray = tipoAssemby.Namespace.Split('.');
            string nombreEsquema = nombreEsquemaArray[nombreEsquemaArray.Length - 1];
            string columnasQuery = string.Empty;
            string valoresQuery = string.Empty;

            tabla = $"{nombreEsquema}.{tabla}";
            foreach (string llave in columnas.Keys)
            {
                columnasQuery += $"{llave},";

                Dictionary<string, object> dccValores = (Dictionary<string, object>)columnas[llave];
                string tipo = Convert.ToString(dccValores["tipo"]);
                string valor = Convert.ToString(dccValores["valor"]);
                if ("string" == tipo.ToLower())
                {
                    valoresQuery += $"'{valor}',";
                }
                else if ("datetime" == tipo.ToLower())
                {
                    DateTime tiempo = new DateTime(1900, 01, 01);
                    valoresQuery += (DateTime.Parse(valor) <= tiempo) ? "null," : $"'{string.Format("{0:yyyy-MM-dd}", DateTime.Parse(valor))}',";
                }
                else
                {
                    valoresQuery += $"{valor},";
                }

            }
            valoresQuery = valoresQuery.Substring(0, valoresQuery.Length - 1);
            columnasQuery = columnasQuery.Substring(0, columnasQuery.Length - 1);


            string query = "insert into {0} ({1}) values ({2}) {3};";
            query = string.Format(query, tabla, columnasQuery, valoresQuery,retornarStr);
            return query;
        }
    }
}
