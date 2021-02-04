using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SISPE_MIGRACION.codigo.repositorios.ORM
{
    class update : IORM
    {
        public dynamic construir<clase>(clase obj, bool retornar)
        {
            //Esto lo descodifica y crea el sql necesario para 
            //Que lo consuma mi postgresql... :D ... 
            MemberInfo informacion = typeof(clase);
            string retornarStr = string.Empty;
            if (retornar) retornarStr = " returning * ";

            Type tipoAssemby = typeof(clase);

            Dictionary<string, object> columnas = new Dictionary<string, object>();
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
                Dictionary<string, object> tipoyvalor = new Dictionary<string, object>();
                    string nombre = item.Name;
                    string tipo = item.PropertyType.Name;

                    var valor = obj.GetType().GetProperty(nombre).GetValue(obj);

                    tipoyvalor.Add("tipo", tipo);
                    tipoyvalor.Add("valor", valor);
                    columnas.Add(nombre, tipoyvalor);
                
            }
            if (where.Length != 0)
            {
                where = where.Substring(0, where.Length - 1);
                where = where.Replace(",", " and ");
            }

            string tabla = informacion.Name;
            string[] nombreEsquemaArray = tipoAssemby.Namespace.Split('.');
            string nombreEsquema = nombreEsquemaArray[nombreEsquemaArray.Length - 1];
            string actualizacion = string.Empty;

            tabla = $"{nombreEsquema}.{tabla}";
            foreach (string llave in columnas.Keys)
            {
                Dictionary<string, object> dccValores = (Dictionary<string, object>)columnas[llave];
                string tipo = Convert.ToString(dccValores["tipo"]);
                string valor = Convert.ToString(dccValores["valor"]);
                if ("string" == tipo.ToLower())
                {
                    actualizacion += $" {llave} = '{valor}',";
                }
                else if ("datetime" == tipo.ToLower())
                {
                    DateTime tiempo = new DateTime(1900, 01, 01);
                    actualizacion += $"{llave} = " + ((DateTime.Parse(valor) <= tiempo) ? "null," : $"'{string.Format("{0:yyyy-MM-dd}", DateTime.Parse(valor))}',");
                }
                else
                {
                    actualizacion += $" {llave} = '{valor}',";
                }

            }
            actualizacion = actualizacion.Substring(0, actualizacion.Length - 1);
           


            string query = " update  {0} set {1} where {2} {3}; ";
            query = string.Format(query, tabla, actualizacion, where,retornarStr);
            return query;
        }
    }
}
