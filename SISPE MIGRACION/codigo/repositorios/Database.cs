using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISPE_MIGRACION.codigo.repositorios
{
    class Database
    {
        private NpgsqlConnection conexion { get; set; }
        private bool transactionActivada { get; set; }
        private NpgsqlCommand cmd { get; set; }
        private NpgsqlTransaction transaccion { get; set; }
        public Database(string query, bool beginTransaction)
        {
            string conexionQuery = query;

            conexion = new NpgsqlConnection(conexionQuery);
            conexion.Open();
            transaccion = conexion.BeginTransaction();
            conexion.Close();            
            this.transactionActivada = beginTransaction;
        }

        public dynamic consulta(string query, bool tipoSelect = false)
        {
            dynamic consulta = null;
            try
            {
                
                conexion.Open();
                cmd = new NpgsqlCommand(query,conexion,transaccion);
                if (!tipoSelect)
                {
                    consulta = new List<Dictionary<string, object>>();
                    System.Data.Common.DbDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        Dictionary<string, object> objeto = new Dictionary<string, object>();
                        for (int x = 0; x < datos.FieldCount; x++)
                        {
                            objeto.Add(datos.GetName(x), datos.GetValue(x));
                        }
                        consulta.Add(objeto);
                    }
                }
                else
                {
                    cmd.ExecuteNonQuery();
                    transaccion.Rollback();
                    consulta = true;
                }
            }
            catch {
                if (transactionActivada) {
                    transactionActivada = false;
                    transaccion.Rollback();
                }
                conexion.Close();
                consulta = false;
            }
            conexion.Close();
            return consulta;
        }


        public void commit() {
            conexion.Open();
            this.transactionActivada = false;
            transaccion.Commit();
            conexion.Close();
        }

        public void rollback()
        {
            conexion.Open();
            this.transactionActivada = false;
            transaccion.Rollback();
            conexion.Close();
        }


    }
}
