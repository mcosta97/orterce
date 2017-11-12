using System;
using System.Data;
using System.Data.Odbc;
using System.Configuration;

namespace ProyectoTallerDataODBC {
    public class daConexion {
        public IDbConnection GetOpenedConnection() {
            OdbcConnection connection = new OdbcConnection(ConfigurationManager.AppSettings["Conexion"]);
            connection.Open();
            return connection;
        }
    }
}
