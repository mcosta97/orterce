using System;
using System.Data;
using System.Data.Sql;
using System.Configuration;
using System.Data.SqlClient;

namespace ProyectoTallerDataODBC {
    public class daConexion {
        public IDbConnection GetOpenedConnection() {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Conexion"]);
            connection.Open();
            return connection;
        }
    }
}
