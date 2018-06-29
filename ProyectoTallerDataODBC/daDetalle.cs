using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoTallerEntity;
using ProyectoTallerDataODBC;
using System.Data;
using static ProyectoTallerData.daComun;
using System.Data.SqlClient;

namespace ProyectoTallerData {
    public class daDetalle {

        private const string SQLSearchByPrimaryKey = "SELECT * FROM Detalles WHERE IdDetalle = @IdDetalle";
        private const string SQLSearch = "SELECT * FROM Detalles WHERE IdPedido = @IdPedido";
        private const string SQLInsert = "INSERT INTO Detalles (IdDetalle, IdPedido, IdProducto, Cantidad) VALUES ((SELECT MAX(IdDetalle) + 1 FROM Detalles), @IdPedido, @IdProducto, @Cantidad)";
        private const string SQLUpdate = "UPDATE Detalles SET IdDetalle = @IdDetalle, IdProducto = @IdProducto, Cantidad = @Cantidad WHERE IdPedido = @IdPedido";
        private const string SQLDelete = "DELETE FROM Detalles WHERE IdDetalle = @IdDetalle";
        private const string SQLDeletePedido = "DELETE FROM Detalles WHERE IdPedido = @IdPedido";

        private daConexion connectionDA = new daConexion();

        public daDetalle() {}

        public DetalleEntity CrearEntidad(SqlDataReader dr) {
            DetalleEntity entidad = new DetalleEntity();
            entidad.IdDetalle = Convert.ToInt32(dr["IdDetalle"]);
            entidad.IdPedido = Convert.ToInt32(dr["IdPedido"]);
            entidad.IdProducto = Convert.ToInt32(dr["IdProducto"]);
            entidad.Cantidad = Convert.ToInt32(dr["Cantidad"]);
            return entidad;
        }

        private void CrearParametros(SqlCommand command, DetalleEntity entidad) {
            SqlParameter parameter = null;

            parameter = command.Parameters.Add("@IdDetalle", SqlDbType.Int);
            parameter.Value = entidad.IdDetalle;

            parameter = command.Parameters.Add("@IdPedido", SqlDbType.Int);
            parameter.Value = entidad.IdPedido;

            parameter = command.Parameters.Add("@IdProducto", SqlDbType.Int);
            parameter.Value = entidad.IdProducto;

            parameter = command.Parameters.Add("@Cantidad", SqlDbType.Int);
            parameter.Value = entidad.Cantidad;
        }

        private void EjecutarComando(TipoComando sqlCommandType, DetalleEntity entidad) {
            SqlConnection connection = null;
            SqlCommand command = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                IDataParameter paramId = new SqlParameter("@IdDetalle", SqlDbType.Int);
                paramId.Value = entidad.IdDetalle;

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

                    case TipoComando.Eliminar2:
                        command = new SqlCommand(SQLDeletePedido, connection);
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

        public List<DetalleEntity> ObtenerDetallesPorPedido(int idPedido) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            List<DetalleEntity> detalles = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearch, connection);
                command.Parameters.Add("@IdPedido", SqlDbType.Int);
                command.Parameters[0].Value = idPedido;
                dr = command.ExecuteReader();

                detalles = new List<DetalleEntity>(); 

                while(dr.Read()){
                    detalles.Add(CrearEntidad(dr));
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

            return detalles;
        }

        public void Insertar(DetalleEntity entidad) {
            EjecutarComando(TipoComando.Insertar, entidad);
        }

        public void Actualizar(DetalleEntity entidad) {
            EjecutarComando(TipoComando.Actualizar, entidad);
        }

        public void Eliminar(int id) {
            DetalleEntity entidad = new DetalleEntity();
            entidad.IdPedido = id;
            EjecutarComando(TipoComando.Eliminar, entidad);
        }

        public void EliminarPorPedido(int idpedido) {
            DetalleEntity entidad = new DetalleEntity();
            entidad.IdPedido = idpedido;
            EjecutarComando(TipoComando.Eliminar2, entidad);
        }
    }
}
