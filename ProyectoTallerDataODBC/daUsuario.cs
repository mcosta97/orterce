using ProyectoTallerDataODBC;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static ProyectoTallerData.daComun;

namespace ProyectoTallerData {
    public class daUsuario {
        private const string SQLSearchByPrimaryKey = "SELECT * FROM Usuarios WHERE IdUsuario = @IdUsuario";
        private const string SQLSearch = "SELECT * FROM Usuarios WHERE Apellido LIKE ? AND Nombre LIKE ?";
        private const string SQLValidate = "SELECT * FROM Usuarios WHERE Usuario=? AND Clave=?";
        private const string SQLInsert = "INSERT INTO Usuarios (Usuario, Clave, Apellido, Nombre, Mail) VALUES (?, ?, ?, ?, ?)";
        private const string SQLUpdate = "UPDATE Usuarios SET Usuario = ?, Clave = ?, Apellido = ?, Nombre = ?, Mail = ? WHERE IdUsuario = ?";
        private const string SQLDelete = "DELETE FROM Usuarios WHERE IdUsuario = ?";
        private const string SQLMailUsuario = "SELECT * FROM Usuarios WHERE Usuario = ?";

        private daConexion connectionDA = new daConexion();

        public daUsuario() {}

        public UsuarioEntity CrearEntidad(SqlDataReader dr) {
            UsuarioEntity entidad = new UsuarioEntity();

            entidad.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
            entidad.Usuario = dr["Usuario"].ToString();
            entidad.Clave = dr["Clave"].ToString();
            entidad.Apellido = dr["Apellido"].ToString();
            entidad.Nombre = dr["Nombre"].ToString();

            if(!dr.IsDBNull(dr.GetOrdinal("Mail"))) {
                entidad.Mail = dr["Mail"].ToString();
            }

            return entidad;
        }

        private void CrearParametros(SqlCommand command, UsuarioEntity entidad) {
            SqlParameter parameter = null;

            parameter = command.Parameters.Add("?", SqlDbType.VarChar);
            parameter.Value = entidad.Usuario;

            parameter = command.Parameters.Add("?", SqlDbType.VarChar);
            parameter.Value = entidad.Clave;

            parameter = command.Parameters.Add("?", SqlDbType.VarChar);
            parameter.Value = entidad.Apellido;

            parameter = command.Parameters.Add("?", SqlDbType.VarChar);
            parameter.Value = entidad.Nombre;

            parameter = command.Parameters.Add("?", SqlDbType.VarChar);

            if(entidad.TieneEmail())
                parameter.Value = entidad.Mail;
            else
                parameter.Value = DBNull.Value;
        }

        private void EjecutarComando(TipoComando sqlCommandType, UsuarioEntity entidad) {
            SqlConnection connection = null;
            SqlCommand command = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                IDataParameter paramId = new SqlParameter("?", SqlDbType.Int);
                paramId.Value = entidad.IdUsuario;

                switch(sqlCommandType) {
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
                        break;
                }

                command.ExecuteNonQuery();
                connection.Close();
            } catch(Exception ex) {
                throw new daException(ex);
            } finally {
                if(command != null) {command.Dispose(); }
                if(connection != null) {connection.Dispose(); }
            }
        }

        public UsuarioEntity ObtenerRecuperacionUsuario(string usuario) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            UsuarioEntity entidad = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLMailUsuario, connection);
                command.Parameters.Add("?", SqlDbType.VarChar);
                command.Parameters[0].Value = usuario;
                dr = command.ExecuteReader();

                if(dr.Read()) {
                    entidad = CrearEntidad(dr);
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

            return entidad;
        }

        public UsuarioEntity BuscarPorClavePrimaria(string idusuario) {
            SqlConnection connection = null;
            SqlCommand command = null; 
            SqlDataReader dr = null;
            UsuarioEntity entidad = null; 

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchByPrimaryKey, connection); 
                command.Parameters.Add("@IdUsuario", SqlDbType.VarChar); 
                command.Parameters[0].Value = idusuario;
                dr = command.ExecuteReader();

                if(dr.Read())
                {
                    entidad = CrearEntidad(dr); 
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

            return entidad;
        }

        public List<UsuarioEntity> Buscar(string id, string apellido, string nombre) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null; 
            List<UsuarioEntity> usuarios = null; 

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearch, connection);
                command.Parameters.Add("?", SqlDbType.VarChar);
                command.Parameters[0].Value = "%" + apellido + "%";

                command.Parameters.Add("?", SqlDbType.VarChar);
                command.Parameters[1].Value = "%" + nombre + "%";

                dr = command.ExecuteReader();
                usuarios = new List<UsuarioEntity>();

                while(dr.Read()) {
                    usuarios.Add(CrearEntidad(dr));
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

            return usuarios;
        }

        public UsuarioEntity Buscar(string user, string pass) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            UsuarioEntity usuario = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLValidate, connection);
                command.Parameters.Add("?", SqlDbType.VarChar);
                command.Parameters[0].Value = user;

                command.Parameters.Add("?", SqlDbType.VarChar);
                command.Parameters[1].Value = pass;
                dr = command.ExecuteReader();

                while(dr.Read()) {
                    usuario = CrearEntidad(dr);
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

            return usuario;
        }

        public void Insertar(UsuarioEntity entidad) {
            EjecutarComando(TipoComando.Insertar, entidad);
        }

        public void Actualizar(UsuarioEntity entidad) {
            EjecutarComando(TipoComando.Actualizar, entidad);
        }

        public void Eliminar(int id) {
            UsuarioEntity entidad = new UsuarioEntity();
            entidad.IdUsuario = id;
            EjecutarComando(TipoComando.Eliminar, entidad);
        }
    }
}
