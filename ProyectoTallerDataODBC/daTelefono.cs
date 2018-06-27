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
    public class daTelefono {
        private const string SQLSearchByPrimaryKey = "SELECT * FROM Telefonos WHERE IdTelefono = ?";
        private const string SQLSearchAll = "SELECT * FROM Telefonos WHERE IdCliente LIKE ?";
        private const string SQLInsert = "INSERT INTO Telefonos (IdCliente, Telefono) VALUES (?,?)";
        private const string SQLUpdate = "UPDATE Telefonos SET Telefono = ? WHERE IdTelefono = ?";
        private const string SQLDelete = "DELETE FROM Telefonos WHERE IdTelefono = ?";

        private daConexion connectionDA = new daConexion();

        public daTelefono() {}

        private TelefonoEntity CrearEntidad(SqlDataReader dr) {
            TelefonoEntity entidad = new TelefonoEntity();
            entidad.IdTelefono = Convert.ToInt32(dr["IdTelefono"]);
            entidad.IdCliente = Convert.ToInt32(dr["IdCliente"]);
            entidad.Telefono = dr["Telefono"].ToString();
            return entidad;
        }

        private void CrearParametros(SqlCommand command, TelefonoEntity entidad) {
            SqlParameter parameter = null;

            parameter = command.Parameters.Add("?", SqlDbType.Int);
            parameter.Value = entidad.IdCliente;

            parameter = command.Parameters.Add("?", SqlDbType.VarChar);
            parameter.Value = entidad.Telefono;
        }

        public DataTable TelefonosTabla(List<TelefonoEntity> phones) {
            DataTable dt = new DataTable();
            dt.Columns.Add("IdTelefono");
            dt.Columns.Add("IdCliente");
            dt.Columns.Add("Telefono");

            foreach(TelefonoEntity telefono in phones) {
                dt.Rows.Add(telefono.IdTelefono, telefono.IdCliente, telefono.Telefono);
            }

            return dt;
        }

        private void EjecutarComando(TipoComando sqlCommandType, TelefonoEntity entidad) {
            SqlConnection connection = null;
            SqlCommand command = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                IDataParameter paramId = new SqlParameter("?", SqlDbType.Int);
                paramId.Value = entidad.IdTelefono;

                switch(sqlCommandType) {
                    case TipoComando.Insertar:
                        command = new SqlCommand(SQLInsert, connection);
                        command.Parameters.Add(paramId);
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
            } catch(Exception ex) {
                throw new daException(ex);
            } finally {
                if(command != null) {
                    command.Dispose();
                }

                if(connection != null) {
                    connection.Dispose();
                }
            }
        }

        public List<TelefonoEntity> ObtenerTelefonos(int idcliente) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            List<TelefonoEntity> usuarios = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchAll, connection);
                command.Parameters.Add("?", SqlDbType.VarChar);
                command.Parameters[0].Value = idcliente;
                dr = command.ExecuteReader();

                usuarios = new List<TelefonoEntity>();

                while(dr.Read()) {
                    usuarios.Add(CrearEntidad(dr));
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

            return usuarios;
        }

        public void Insertar(TelefonoEntity entidad) {
            EjecutarComando(TipoComando.Insertar, entidad);
        }

        public void Actualizar(TelefonoEntity entidad) {
            EjecutarComando(TipoComando.Actualizar, entidad);
        }

        public void Eliminar(int id) {
            TelefonoEntity entidad = new TelefonoEntity();
            entidad.IdTelefono = id;
            EjecutarComando(TipoComando.Eliminar, entidad);
        }
    }
}
