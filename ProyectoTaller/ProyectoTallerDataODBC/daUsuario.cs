using ProyectoTallerDataODBC;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerData {
    public class daUsuario {
        private const string SQLSearchByPrimaryKey = "SELECT * FROM Usuarios WHERE IdUsuario = ?";
        private const string SQLSearch = "SELECT * FROM Usuarios WHERE IdUsuario LIKE ? AND Apellido LIKE ? AND Nombre LIKE ?";
        private const string SQLValidate = "SELECT * FROM Usuarios WHERE Usuario=? AND Clave=?";
        private const string SQLInsert = "INSERT INTO Usuarios (IdUsuario, Usuario, Clave, Apellido, Nombre, Mail) VALUES (?, ?, ?, ?, ?, ?)";
        private const string SQLUpdate = "UPDATE Usuarios SET Usuario = ?, Clave = ?, Apellido = ?, Nombre = ?, Mail = ? WHERE IdUsuario = ?";
        private const string SQLDelete = "DELETE FROM Usuarios WHERE IdUsuario = ?";
        private const string SQLMailUsuario = "SELECT * FROM Usuarios WHERE Usuario = ?";

        private daConexion connectionDA = new daConexion();

        public daUsuario() {}

        public UsuarioEntity CrearEntidad(OdbcDataReader dr) {
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

        private void CrearParametros(OdbcCommand command, UsuarioEntity entidad) {
            OdbcParameter parameter = null;

            parameter = command.Parameters.Add("?", OdbcType.VarChar);
            parameter.Value = entidad.Usuario;

            parameter = command.Parameters.Add("?", OdbcType.VarChar);
            parameter.Value = entidad.Clave;

            parameter = command.Parameters.Add("?", OdbcType.VarChar);
            parameter.Value = entidad.Apellido;

            parameter = command.Parameters.Add("?", OdbcType.VarChar);
            parameter.Value = entidad.Nombre;

            parameter = command.Parameters.Add("?", OdbcType.VarChar);

            if(entidad.TieneEmail())
                parameter.Value = entidad.Mail;
            else
                parameter.Value = DBNull.Value;
        }

        private void EjecutarComando(daComun.TipoComandoEnum sqlCommandType, UsuarioEntity entidad) {
            OdbcConnection connection = null;
            OdbcCommand command = null;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                IDataParameter paramId = new OdbcParameter("?", OdbcType.Int);
                paramId.Value = entidad.IdUsuario;

                switch(sqlCommandType) {
                    case daComun.TipoComandoEnum.Insertar:
                        command = new OdbcCommand(SQLInsert, connection);
                        command.Parameters.Add(paramId);
                        CrearParametros(command, entidad);
                        break;

                    case daComun.TipoComandoEnum.Actualizar:
                        command = new OdbcCommand(SQLUpdate, connection);
                        CrearParametros(command, entidad);
                        command.Parameters.Add(paramId);
                        break;

                    case daComun.TipoComandoEnum.Eliminar:
                        command = new OdbcCommand(SQLDelete, connection);
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
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            UsuarioEntity entidad = null;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLMailUsuario, connection);
                command.Parameters.Add("?", OdbcType.VarChar);
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
            OdbcConnection connection = null;
            OdbcCommand command = null; 
            OdbcDataReader dr = null;
            UsuarioEntity entidad = null; 

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLSearchByPrimaryKey, connection); 
                command.Parameters.Add("?", OdbcType.VarChar); 
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
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null; 
            List<UsuarioEntity> usuarios = null; 

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLSearch, connection);
                command.Parameters.Add("?", OdbcType.VarChar);
                command.Parameters[0].Value = "%" + id + "%";

                command.Parameters.Add("?", OdbcType.VarChar);
                command.Parameters[1].Value = "%" + apellido + "%";

                command.Parameters.Add("?", OdbcType.VarChar);
                command.Parameters[2].Value = "%" + nombre + "%";

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
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            UsuarioEntity usuario = null;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLValidate, connection);
                command.Parameters.Add("?", OdbcType.VarChar);
                command.Parameters[0].Value = user;

                command.Parameters.Add("?", OdbcType.VarChar);
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
            daContadores da = new daContadores();
            entidad.IdUsuario = da.TraerContador(daComun.Contador.Usuario) + 1;
            EjecutarComando(daComun.TipoComandoEnum.Insertar, entidad);
            da.Sumar(daComun.Contador.Usuario);
        }

        public void Actualizar(UsuarioEntity entidad) {
            EjecutarComando(daComun.TipoComandoEnum.Actualizar, entidad);
        }

        public void Eliminar(int id) {
            UsuarioEntity entidad = new UsuarioEntity();
            entidad.IdUsuario = id;
            EjecutarComando(daComun.TipoComandoEnum.Eliminar, entidad);
        }
    }
}
