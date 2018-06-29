using ProyectoTallerDataODBC;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using static ProyectoTallerData.daComun;

namespace ProyectoTallerData {
    public class daAdministrativo {
        private const string SQLSearchByPrimaryKey = "SELECT * FROM Administrativos WHERE IdAdministrativo = @IdAdministrativo";
        private const string SQLSearchAll = "SELECT * FROM Administrativos A INNER JOIN Usuarios U ON U.IdUsuario = A.IdUsuario WHERE Acceso LIKE @Acceso";
        private const string SQLSearchUser = "SELECT * FROM Administrativos A INNER JOIN Usuarios U ON U.IdUsuario = A.IdUsuario WHERE A.IdUsuario = @IdUsuario";
        private const string SQLInsert = "INSERT INTO Administrativos (IdAdministrativo, IdUsuario, Acceso) VALUES ((SELECT MAX(IdAdministrativo) + 1 FROM Administrativos), @IdUsuario, @Acceso)";
        private const string SQLUpdate = "UPDATE Administrativos SET IdUsuario = @IdUsuario, Acceso = @Acceso WHERE IdAdministrativo = @IdAdministrativo";
        private const string SQLDelete = "DELETE FROM Administrativos WHERE IdAdministrativo = @IdAdministrativo";
        private const string SQLAcceso = "SELECT acceso FROM Administrativos WHERE IdUsuario = @IdUsuario";

        private daConexion connectionDA = new daConexion();

        public daAdministrativo() { }

        private AdministrativoEntity CrearEntidad(SqlDataReader dr) {
            AdministrativoEntity entidad = new AdministrativoEntity();
            entidad.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
            entidad.IdAdministrativo = Convert.ToInt32(dr["IdAdministrativo"]);
            entidad.Usuario = dr["Usuario"].ToString();
            entidad.Clave = dr["Clave"].ToString();
            entidad.Nombre = dr["Nombre"].ToString();
            entidad.Apellido = dr["Apellido"].ToString();
            entidad.Mail = dr["Mail"].ToString();
            entidad.Acceso = Convert.ToInt32(dr["Acceso"]);
            return entidad;
        }

        private void CrearParametros(SqlCommand command, AdministrativoEntity entidad) {
            SqlParameter parameter = null;

            parameter = command.Parameters.Add("@IdUsuario", SqlDbType.Int);
            parameter.Value = entidad.IdUsuario;

            parameter = command.Parameters.Add("@Acceso", SqlDbType.Int);
            parameter.Value = entidad.Acceso;
        }

        private void EjecutarComando(TipoComando sqlCommandType, AdministrativoEntity entidad) {
            SqlConnection connection = null;
            SqlCommand command = null;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                IDataParameter paramId = new SqlParameter("@IdAdministrativo", SqlDbType.Int);
                paramId.Value = entidad.IdAdministrativo;

                switch (sqlCommandType) {
                    case TipoComando.Insertar:
                        command = new SqlCommand(SQLInsert, connection);
                        CrearParametros(command, entidad);
                        break;

                    case TipoComando.Actualizar:
                        command = new SqlCommand(SQLUpdate, connection);
                        command.Parameters.Add(paramId);
                        CrearParametros(command, entidad);
                        break;

                    case TipoComando.Eliminar:
                        command = new SqlCommand(SQLDelete, connection);
                        command.Parameters.Add(paramId);
                        CrearParametros(command, entidad);
                        break;
                }

                command.ExecuteNonQuery();
                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                if (command != null) { command.Dispose(); }
                if (connection != null) { connection.Dispose(); }
            }
        }

        public int ObtenerAcceso(int idUsuario) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            int acceso = 0;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLAcceso, connection);
                command.Parameters.Add("@IdUsuario", SqlDbType.Int);
                command.Parameters[0].Value = idUsuario;
                dr = command.ExecuteReader();

                while (dr.Read()) {
                    acceso = Convert.ToInt32(dr["acceso"]);
                }

                dr.Close();
                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;
                if (command != null) {command.Dispose();}
                if (connection != null) {connection.Dispose();}
            }

            return acceso;
        }

        public AdministrativoEntity ObtenerAdministrativo(string id) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            AdministrativoEntity administrativo = null;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchUser, connection);
                command.Parameters.Add("@IdUsuario", SqlDbType.Int);
                command.Parameters[0].Value = id;
                dr = command.ExecuteReader();

                administrativo = new AdministrativoEntity();

                while (dr.Read()) {
                    administrativo = CrearEntidad(dr);
                }

                dr.Close();
                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;
                if (command != null) {command.Dispose();}
                if (connection != null) {connection.Dispose();}
            }

            return administrativo;
        }

        public DataTable ObtenerAdministrativosTabla() {
            List<AdministrativoEntity> administrativos = ObtenerAdministrativos("");
            DataTable dt = new DataTable();
            dt.Columns.Add("IdUsuario");
            dt.Columns.Add("IdAdministrativo");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Apellido");
            dt.Columns.Add("Usuario");
            dt.Columns.Add("Clave");
            dt.Columns.Add("Mail");
            dt.Columns.Add("Acceso");

            foreach (AdministrativoEntity administrativo in administrativos) {
                dt.Rows.Add(administrativo.IdUsuario, administrativo.IdAdministrativo,
                            administrativo.Nombre, administrativo.Apellido, administrativo.Usuario,
                            administrativo.Clave, administrativo.Mail, administrativo.Acceso);
            }

            return dt;
        }

        public List<AdministrativoEntity> ObtenerAdministrativos(string acceso) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            List<AdministrativoEntity> administrativos = null;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchAll, connection);
                command.Parameters.Add("@Acceso", SqlDbType.VarChar);
                command.Parameters[0].Value = "%" + acceso + "%";
                dr = command.ExecuteReader();

                administrativos = new List<AdministrativoEntity>();

                while (dr.Read()) {
                    administrativos.Add(CrearEntidad(dr));
                }

                dr.Close();
                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;
                if (command != null) { command.Dispose(); }
                if (connection != null) { connection.Dispose(); }
            }

            return administrativos;
        }

        public void Insertar(AdministrativoEntity entidad) {
            EjecutarComando(TipoComando.Insertar, entidad);
            EjecutarComando(TipoComando.Insertar, entidad);
            new daUsuario().Insertar(entidad);
        }

        public void Actualizar(AdministrativoEntity entidad) {
            EjecutarComando(TipoComando.Actualizar, entidad);
            new daUsuario().Actualizar(entidad);
        }

        public void Eliminar(int id) {
            AdministrativoEntity entidad = new AdministrativoEntity();
            entidad.IdAdministrativo = id;
            EjecutarComando(TipoComando.Eliminar, entidad);
            new daCliente().Eliminar(entidad.IdUsuario);
        }
    }
}
