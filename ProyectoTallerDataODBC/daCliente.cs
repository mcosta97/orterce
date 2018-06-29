using ProyectoTallerDataODBC;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProyectoTallerData.daComun;

namespace ProyectoTallerData {
    public class daCliente {
        private const string SQLSearchByPrimaryKey = "SELECT * FROM Clientes WHERE IdCliente = @IdCliente";
        private const string SQLSearchAll = "SELECT * FROM Clientes C INNER JOIN Usuarios U ON C.IdUsuario = U.IdUsuario WHERE Dni LIKE @Dni";
        private const string SQLSearchId = "SELECT * FROM Clientes C INNER JOIN Usuarios U ON C.IdUsuario = U.IdUsuario WHERE U.IdUsuario LIKE @IdUsuario";
        private const string SQLInsert = "INSERT INTO Clientes (IdCliente, IdUsuario, Dni) VALUES ((SELECT MAX(IdCliente) + 1 FROM Clientes) ,@IdUsuario, @Dni)";
        private const string SQLUpdate = "UPDATE Clientes SET IdUsuario=@IdUsuario, Dni = @Dni WHERE IdCliente = @IdCliente";
        private const string SQLDelete = "DELETE FROM Clientes WHERE IdCliente = @IdCliente";

        private daConexion connectionDA = new daConexion();

        public daCliente() {}

        private ClienteEntity CrearEntidad(SqlDataReader dr) {
            ClienteEntity entidad = new ClienteEntity();
            entidad.IdCliente = Convert.ToInt32(dr["IdCliente"]);
            entidad.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
            entidad.Usuario = dr["Usuario"].ToString();
            entidad.Clave = dr["Clave"].ToString();
            entidad.Nombre = dr["Nombre"].ToString();
            entidad.Apellido = dr["Apellido"].ToString();
            entidad.Mail = dr["Mail"].ToString();
            entidad.Dni = dr["Dni"].ToString();
            return entidad;
        }

        private void CrearParametros(SqlCommand command, ClienteEntity entidad) {
            SqlParameter parameter = null;

            parameter = command.Parameters.Add("@IdUsuario", SqlDbType.Int);
            parameter.Value = entidad.IdUsuario;

            parameter = command.Parameters.Add("@Dni", SqlDbType.VarChar);
            parameter.Value = entidad.Dni;
        }

        private void EjecutarComando(TipoComando sqlCommandType, ClienteEntity entidad) {
            SqlConnection connection = null;
            SqlCommand command = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                IDataParameter paramId = new SqlParameter("@IdCliente", SqlDbType.Int);
                paramId.Value = entidad.IdCliente;

                switch(sqlCommandType) {
                    case TipoComando.Insertar:
                        command = new SqlCommand(SQLInsert, connection);
                        CrearParametros(command, entidad);
                        break;

                    case TipoComando.Actualizar:
                        command = new SqlCommand(SQLUpdate, connection);
                        CrearParametros(command, entidad);
                        command.Parameters.Add(paramId);
                        break;

                    case TipoComando.Eliminar:
                        command = new SqlCommand(SQLDelete, connection);
                        command.Parameters.Add(paramId);
                        CrearParametros(command, entidad);
                        break;
                }

                command.ExecuteNonQuery();
                connection.Close();
            } catch(Exception ex) {
                throw new daException(ex);
            } finally {
                if(command != null) {command.Dispose();}
                if(connection != null) {connection.Dispose();}
            }
        }

        public ClienteEntity ObtenerCliente(string id) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            ClienteEntity usuario = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchId, connection);
                command.Parameters.Add("@IdUsuario", SqlDbType.VarChar);
                command.Parameters[0].Value = "%" + id + "%";
                dr = command.ExecuteReader();

                usuario = new ClienteEntity();

                while(dr.Read()) {
                    usuario = CrearEntidad(dr);
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

            return usuario;
        }

        public DataTable ObtenerClientesTabla() {
            List<ClienteEntity> clientes = ObtenerClientes("");
            DataTable dt = new DataTable();
            dt.Columns.Add("IdUsuario");
            dt.Columns.Add("IdCliente");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Apellido");
            dt.Columns.Add("Usuario");
            dt.Columns.Add("Clave");
            dt.Columns.Add("Mail");
            dt.Columns.Add("Dni");

            foreach(ClienteEntity cliente in clientes) {
                dt.Rows.Add(cliente.IdUsuario, cliente.IdCliente,
                            cliente.Nombre, cliente.Apellido, 
                            cliente.Usuario, cliente.Clave, 
                            cliente.Mail, cliente.Dni);
            }

            return dt;
        }

        public List<ClienteEntity> ObtenerClientes(string dni) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            List<ClienteEntity> usuarios = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchAll, connection);
                command.Parameters.Add("@Dni", SqlDbType.VarChar);
                command.Parameters[0].Value = "%" + dni + "%";
                dr = command.ExecuteReader();

                usuarios = new List<ClienteEntity>();

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

        public void Insertar(ClienteEntity entidad) {
            new daUsuario().Insertar(entidad);
            EjecutarComando(TipoComando.Insertar, entidad);
        }

        public void Actualizar(ClienteEntity entidad) {
            EjecutarComando(TipoComando.Actualizar, entidad);
            new daUsuario().Actualizar(entidad);
        }

        public void Eliminar(int id) {
            ClienteEntity entidad = new ClienteEntity();
            entidad.IdCliente = id;
            EjecutarComando(TipoComando.Eliminar, entidad);
            new daUsuario().Eliminar(entidad.IdUsuario);
        }
    }
}
