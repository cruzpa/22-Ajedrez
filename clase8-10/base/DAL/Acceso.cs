using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    internal class Acceso
    {
        private SqlConnection conexion;

        public void Abrir()
        {
            conexion = new SqlConnection("Initial Catalog=PARCIAL; Integrated Security=SSPI; Data Source=.");
            conexion.Open();


        }

        public void Cerrar()
        {
            conexion.Close();
            conexion = null;
            GC.Collect();
        }

        private SqlCommand CrearComando(String sql, List<SqlParameter> parameters = null)
        {
            SqlCommand cmd = new SqlCommand(sql, conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }
            return cmd;
        }

        public DataTable Leer(String sql, List<SqlParameter> parameters = null)
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(); 
            adapter.SelectCommand = CrearComando(sql, parameters);
            adapter.Fill(dataTable);
            return dataTable;
        }

        public int Escribir(String sql, List<SqlParameter> parameters = null)
        {
            SqlCommand cmd = CrearComando(sql,parameters);
            int filas = 0;
            try
            {
                filas = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                filas = -1;
            }
            return filas;
        }

        public SqlParameter CrearParametro(string nombre, string valor)
        {
            SqlParameter param = new SqlParameter(nombre, valor);
            param.DbType = DbType.String;
            return param;
        }

        public SqlParameter CrearParametro(string nombre, int valor)
        {
            SqlParameter param = new SqlParameter(nombre, valor);
            param.DbType = DbType.Int32;
            return param;
        }
        public SqlParameter CrearParametro(string nombre, float valor)
        {
            SqlParameter param = new SqlParameter(nombre, valor);
            param.DbType = DbType.Single;
            return param;
        }
    }
}

