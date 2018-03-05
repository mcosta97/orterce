using ProyectoTallerDataODBC;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

namespace ProyectoTallerData {
    public class daAdministrativo {
        private const string SQLSearchByPrimaryKey = "SELECT * FROM Administrativos WHERE IdAdministrativo = ?";
        private const string SQLSearchAll = "SELECT * FROM Administrativos A INNER JOIN Usuarios U ON U.IdUsuario = A.IdUsuario WHERE Acceso LIKE ?";
        private const string SQLSearchUser = "SELECT * FROM Administrativos A INNER JOIN Usuarios U ON U.IdUsuario = A.IdUsuario WHERE A.IdUsuario = ?";
        private const string SQLInsert = "INSERT INTO Administrativos (IdUsuario, Acceso) VALUES (?, ?)";
        private const string SQLUpdate = "UPDATE Administrativos SET IdUsuario = ?, Acceso = ? WHERE IdAdministrativo = ?";
        private const string SQLDelete = "DELETE FROM Administrativos WHERE IdAdministrativo = ?";
        private const string SQLAcceso = "SELECT acceso FROM Administrativos WHERE IdUsuario = ?";

        private daConexion connectionDA = new daConexion();

        public daAdministrativo() { }

        private AdministrativoEntity CrearEntidad(OdbcDataReader dr) {
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

        private void CrearParametros(OdbcCommand command, AdministrativoEntity entidad) {
            OdbcParameter parameter = null;

            parameter = command.Parameters.Add("?", OdbcType.Int);
            parameter.Value = entidad.IdUsuario;

            parameter = command.Parameters.Add("?", OdbcType.Int);
            parameter.Value = entidad.Acceso;
        }

        private void EjecutarComando(daComun.TipoComandoEnum sqlCommandType, AdministrativoEntity entidad) {
            OdbcConnection connection = null;
            OdbcCommand command = null;

            try {
                connection = (OdbcConnection)connectionDA.GetOpenedConnection();
                IDataParameter paramId = new OdbcParameter("?", OdbcType.Int);
                paramId.Value = entidad.IdAdministrativo;

                switch (sqlCommandType) {
                    case daComun.TipoComandoEnum.Insertar:
                        command = new OdbcCommand(SQLInsert, connection);
                        CrearParametros(command, entidad);
                        break;

                    case daComun.TipoComandoEnum.Actualizar:
                        command = new OdbcCommand(SQLUpdate, connection);
                        command.Parameters.Add(paramId);
                        CrearParametros(command, entidad);
                        break;

                    case daComun.TipoComandoEnum.Eliminar:
                        command = new OdbcCommand(SQLDelete, connection);
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
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            int acceso = 0;

            try {
                connection = (OdbcConnection)connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLAcceso, connection);
                command.Parameters.Add("?", OdbcType.Int);
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
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            AdministrativoEntity administrativo = null;

            try {
                connection = (OdbcConnection)connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLSearchUser, connection);
                command.Parameters.Add("?", OdbcType.Int);
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

        public AdministrativoEntity ObtenerUsuarioAdministrativo(string usuario) {
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            AdministrativoEntity administrativo = null;

            try {
                connection = (OdbcConnection)connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLSearchUser, connection);
                command.Parameters.Add("?", OdbcType.Int);
                command.Parameters[0].Value = usuario;
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
                if (command != null) { command.Dispose(); }
                if (connection != null) { connection.Dispose(); }
            }

            return administrativo;
        }

        public List<AdministrativoEntity> ObtenerAdministrativos(string acceso) {
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            List<AdministrativoEntity> administrativos = null;

            try {
                connection = (OdbcConnection)connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLSearchAll, connection);
                command.Parameters.Add("?", OdbcType.VarChar);
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
            EjecutarComando(daComun.TipoComandoEnum.Insertar, entidad);
            new daUsuario().Insertar(entidad);
        }

        public void Actualizar(AdministrativoEntity entidad) {
            EjecutarComando(daComun.TipoComandoEnum.Actualizar, entidad);
            new daUsuario().Actualizar(entidad);
        }

        public void Eliminar(int id) {
            AdministrativoEntity entidad = new AdministrativoEntity();
            entidad.IdAdministrativo = id;
            EjecutarComando(daComun.TipoComandoEnum.Eliminar, entidad);
            new daCliente().Eliminar(entidad.IdUsuario);
        }
    }
}
