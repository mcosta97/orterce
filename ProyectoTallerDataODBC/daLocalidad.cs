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
    public class daLocalidad {
        private const string SQLSearchByPrimaryKey = "SELECT * FROM Localidades WHERE IdLocalidad = @IdLocalidad";
        private const string SQLSearch = "SELECT * FROM Localidades WHERE Nombre=@Nombre";
        private const string SQLSearchProv = "SELECT * FROM Localidades L INNER JOIN Provincias P ON P.IdProvincia = L.IdProvincia WHERE p.nombre=@Nombre";
        private const string SQLInsert = "INSERT INTO Localidades (IdLocalidad, IdProvincia, Nombre) VALUES ((SELECT MAX(IdLocalidad) + 1 FROM Localidades), @IdProvincia, @Nombre)";
        private const string SQLUpdate = "UPDATE Localidades SET IdProvincia = @IdProvincia, Nombre = @Nombre WHERE idLocalidad = @IdLocalidad";
        private const string SQLDelete = "DELETE FROM Localidades WHERE IdLocalidad = @IdLocalidad";

        private daConexion connectionDA = new daConexion();

        public daLocalidad() {
        }

        private LocalidadEntity CrearEntidad(SqlDataReader dr) {
            LocalidadEntity entidad = new LocalidadEntity();
            entidad.IdLocalidad = Convert.ToInt32(dr["IdLocalidad"]);
            entidad.Nombre = dr["Nombre"].ToString();
            entidad.Provincia = new daProvincia().ObtenerProvinciaPorId(Convert.ToInt32(dr["IdProvincia"]));
            return entidad;
        }

        public LocalidadEntity ObtenerLocalidadPorId(int idlocalidad) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            LocalidadEntity localidad;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchByPrimaryKey, connection);
                command.Parameters.Add("@IdLocalidad", SqlDbType.Int);
                command.Parameters[0].Value = idlocalidad;
                dr = command.ExecuteReader();

                localidad = new LocalidadEntity();

                while(dr.Read()) {
                    localidad = CrearEntidad(dr);
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

            return localidad;
        }

        public LocalidadEntity ObtenerLocalidadPorNombre(string loca) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            LocalidadEntity localidad;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearch, connection);
                command.Parameters.Add("@Nombre", SqlDbType.VarChar);
                command.Parameters[0].Value = loca;
                dr = command.ExecuteReader();

                localidad = new LocalidadEntity();

                while(dr.Read()) {
                    localidad = CrearEntidad(dr);
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

            return localidad;
        }

        public List<LocalidadEntity> ObtenerLocalidades(string provincia) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            List<LocalidadEntity> localidades = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchProv, connection);
                command.Parameters.Add("@Nombre", SqlDbType.VarChar);
                command.Parameters[0].Value = provincia;
                dr = command.ExecuteReader();

                localidades = new List<LocalidadEntity>();

                while(dr.Read()) {
                    localidades.Add(CrearEntidad(dr));
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

            return localidades;
        }
    }
}