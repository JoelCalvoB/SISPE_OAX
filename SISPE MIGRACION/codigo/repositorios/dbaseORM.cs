using SISPE_MIGRACION.codigo.baseDatos;
using SISPE_MIGRACION.codigo.repositorios;
using SISPE_MIGRACION.codigo.repositorios.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



public class dbaseORM
{
    private Database bd;
    public dbaseORM(bool beginTransaction = false) {
        string queryConexion = globales.datosConexion;
        bd = new Database(queryConexion,beginTransaction);
    }
    public dynamic insert<clase>(clase obj)
    {
        IORM orm = new insert();
        string query = orm.construir<clase>(obj,false);
        return globales.consulta(query, true);
    }

    public dynamic insert<clase>(clase obj,bool retornar) where clase : class, new()
    {
        IORM orm = new insert();
        string query = orm.construir<clase>(obj, retornar);
        return this.queryForMap<clase>(query);
    }

    public dynamic insertAll<clase>(List<clase> listaObj)
    {
        string query = string.Empty;
        foreach (clase item in listaObj)
        {
            IORM orm = new insert();
            query += orm.construir<clase>(item, false);
        }


        return globales.consulta(query, true);
    }

    public dynamic insertAll<clase>(List<clase> listaObj, bool retornar) where clase:class , new ()
    {
        string query = string.Empty;
        foreach (clase item in listaObj)
        {
            IORM orm = new insert();
            query += orm.construir<clase>(item, false);
        }

        if (retornar)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                string[] separarQuerys = query.Split(';');
                if (separarQuerys.Length > 0)
                {
                    string primerSentencia = separarQuerys[0].Substring(0, separarQuerys[0].IndexOf("values"));
                    string segundaSentencia = string.Empty;
                    foreach (string item in separarQuerys)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            segundaSentencia += item.Substring(item.IndexOf("values")) + ",";
                        }
                    }

                    segundaSentencia = segundaSentencia.Substring(0, segundaSentencia.Length - 1);
                    primerSentencia = primerSentencia + " values " + segundaSentencia.Replace("values", "") + " returning *; ";
                    query = primerSentencia;
                }
            }
        }

        List<clase> lista = new List<clase>();
        List<Dictionary<string, object>> resu = globales.consulta(query);
        foreach (Dictionary<string,object> item in resu) {
            clase c = getObject<clase>(item);
            lista.Add(c);
        }

        return lista;
    }

    public dynamic update<clase>(clase obj)
    {
        IORM orm = new update();
        string query = orm.construir<clase>(obj, false);
        return globales.consulta(query, true);
    }

    public dynamic update<clase>(clase obj, bool retornar) where clase :class, new ()
    {
        IORM orm = new update();
        string query = orm.construir<clase>(obj, retornar);
        return this.queryForMap<clase>(query);
    }

    public dynamic updateAll<clase>(List<clase> listaObj)
    {
        string query = string.Empty;
        foreach (clase item in listaObj)
        {
            IORM orm = new update();
            query += orm.construir<clase>(item,false);
        }
        return globales.consulta(query);
    }

    public dynamic delete<clase>(clase obj)
    {
        IORM orm = new delete();
        string query = orm.construir<clase>(obj, false);
        return globales.consulta(query,true);
    }

 

    public dynamic delete<clase>(clase obj, bool retornar) where clase :class,new()
    {
        IORM orm = new delete();
        string query = orm.construir<clase>(obj, retornar);
        return this.queryForMap<clase>(query);
    }

    public dynamic deleteAll<clase>(List<clase> listaObj)
    {
        string query = string.Empty;
        foreach (clase item in listaObj)
        {
            IORM orm = new delete();
            query += orm.construir<clase>(item,false);
        }
        return globales.consulta(query);
    }

    public dynamic query(string query,bool tipoSelect = false) {
        return globales.consulta(query,tipoSelect);
    }

    public dynamic queryForMap<clase>(string query) where clase :class,new()
    {
        clase c = new clase();
        List<Dictionary<string, object>> resultado = globales.consulta(query);
        if (resultado.Count != 0) {
            Dictionary<string, object> diccionario = resultado[0];
            c = getObject<clase>(diccionario);
        }

        return c;
    }

   

    public dynamic queryForList<clase>(string query) where clase : class, new()
    {
        List<clase> lista = new List<clase>();
        List<Dictionary<string, object>> resultado = globales.consulta(query);
        foreach (Dictionary<string,object> item in resultado) {
            clase c = getObject<clase>(item);
            lista.Add(c);
        }

        return lista;
    }

    internal clase getObject<clase>(Dictionary<string, object> diccionario) where clase : class, new()
    {
        clase c = new clase();
        foreach (string llave in diccionario.Keys)
        {
            Type tipo = c.GetType();
            PropertyInfo atributo = tipo.GetProperty(llave);
            string tipodato = atributo.PropertyType.Name;
            if (tipodato.ToLower() == "datetime")
            {
                DateTime tiempo = string.IsNullOrWhiteSpace(Convert.ToString(diccionario[llave])) ? DateTime.Parse("01/01/0001") : DateTime.Parse(diccionario[llave].ToString());
                atributo.SetValue(c, tiempo, null);
            }
            else if (tipodato.ToLower() == "int32")
            {
                atributo.SetValue(c, string.IsNullOrWhiteSpace(Convert.ToString(diccionario[llave])) ? 0 : Convert.ToInt32(diccionario[llave]), null);
            } else if (tipodato.ToLower() == "double")
            {

                atributo.SetValue(c, string.IsNullOrWhiteSpace(Convert.ToString(diccionario[llave])) ? 0 : Convert.ToDouble(diccionario[llave]), null);
            } else if (tipodato.ToLower().Contains("bool")) {
                bool valor = Convert.ToString(diccionario[llave]).ToLower().Contains("f");
                atributo.SetValue(c, valor, null);
            }
            else
            {
                atributo.SetValue(c, Convert.ToString(diccionario[llave]), null);
            }
        }
        return c;
    }
    public void rollback() {
        this.bd.rollback();
    }

    public void commit()
    {
        this.bd.commit();
    }

   
}

