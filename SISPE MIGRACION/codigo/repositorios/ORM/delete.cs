using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SISPE_MIGRACION.codigo.repositorios.ORM
{
    class delete : IORM
    {
        public dynamic construir<clase>(clase obj,bool retornar)
        {
            MemberInfo informacion = typeof(clase);

            string retornarStr = string.Empty;
            if (retornar) retornarStr = " returning * ";

            Type tipoAssemby = typeof(clase);

            PropertyInfo[] propiedades = typeof(clase).GetProperties();
            string where = string.Empty;
            foreach (PropertyInfo item in propiedades)
            {
                var attributes = item.GetCustomAttributes(false);
                foreach (var attribute in attributes)
                {
                    if (attribute.GetType() == typeof(primeryKey))
                    {
                        string nombreWhere = item.Name;
                        string tipowhere = item.PropertyType.Name;
                        string valorWhere = Convert.ToString(obj.GetType().GetProperty(nombreWhere).GetValue(obj));

                        if ("string" == tipowhere.ToLower())
                        {
                            where += $" {nombreWhere} = '{valorWhere}',";
                        }
                        else if ("datetime" == tipowhere.ToLower())
                        {
                            DateTime tiempo = new DateTime(1900, 01, 01);
                            where += $"{nombreWhere} = " + ((DateTime.Parse(valorWhere) <= tiempo) ? "null," : $"'{string.Format("{0:yyyy-MM-dd}", DateTime.Parse(valorWhere))}',");
                        }
                        else
                        {
                            where += $" {nombreWhere} = {valorWhere},";
                        }
                    }
                  
                }

               
            }

            if (where.Length != 0)
            {
                where = where.Substring(0, where.Length - 1);
                where = where.Replace(",", " and ");
            }

            string tabla = informacion.Name;
            string[] nombreEsquemaArray = tipoAssemby.Namespace.Split('.');
            string nombreEsquema = nombreEsquemaArray[nombreEsquemaArray.Length - 1];

            tabla = $"{nombreEsquema}.{tabla}";


            string query = " delete from {0} where {1} {2}; ";
            query = string.Format(query, tabla, where,retornarStr);
            return query;
        }
    }
}
