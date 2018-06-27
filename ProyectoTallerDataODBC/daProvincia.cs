using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoTallerEntity;
using ProyectoTallerData;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoTallerDataODBC {
    public class daProvincia {
        private const string SQLSearchByPrimaryKey = "SELECT * FROM Provincias WHERE IdProvincia = ?";
        private const string SQLSearch = "SELECT * FROM Provincias";
        private const string SQLInsert = "INSERT INTO Provincias (Nombre) VALUES (?)";
        private const string SQLUpdate = "UPDATE Provincias SET Nombre = ? WHERE IdProvincia = ?";
        private const string SQLDelete = "DELETE FROM Provincias WHERE IdProvincia = ?";

        private daConexion connectionDA = new daConexion();

        public daProvincia() {
        }

        private ProvinciaEntity CrearEntidad(SqlDataReader dr) {
            ProvinciaEntity entidad = new ProvinciaEntity();
            entidad.IdProvincia = Convert.ToInt32(dr["IdProvincia"]);
            entidad.Nombre = dr["Nombre"].ToString();
            return entidad;
        }

        public ProvinciaEntity ObtenerProvinciaPorId(int idprovincia) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            ProvinciaEntity provincia;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchByPrimaryKey, connection);
                command.Parameters.Add("?", SqlDbType.Int);
                command.Parameters[0].Value = idprovincia;
                dr = command.ExecuteReader();

                provincia = new ProvinciaEntity();

                while(dr.Read()) {
                    provincia = CrearEntidad(dr);
                }

                dr.Close();
                connection.Close();
            } catch(Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;

                if(command != null) {
                    command.Dispose();
                }

                if(connection != null) {
                    connection.Dispose();
                }
            }

            return provincia;
        }

        public List<ProvinciaEntity> ObtenerProvincias() {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            List<ProvinciaEntity> provincias = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearch, connection);
                dr = command.ExecuteReader();

                provincias = new List<ProvinciaEntity>();

                while(dr.Read()) {
                    provincias.Add(CrearEntidad(dr));
                }

                dr.Close();
                connection.Close();
            } catch(Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;
                if(command != null) {command.Dispose();}
                if(connection != null) {connection.Dispose();}
            }

            return provincias;
        }
    }
}
