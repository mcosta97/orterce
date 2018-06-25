using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoTallerEntity;
using ProyectoTallerDataODBC;
using System.Data.Odbc;
using System.Data;
using static ProyectoTallerData.daComun;

namespace ProyectoTallerData {
    public class daDetalle {

        private const string SQLSearchByPrimaryKey = "SELECT * FROM Detalles WHERE IdDetalle = ?";
        private const string SQLSearch = "SELECT * FROM Detalles WHERE IdPedido = ?";
        private const string SQLInsert = "INSERT INTO Detalles (IdPedido, IdProducto, Cantidad) VALUES (?, ?, ?)";
        private const string SQLUpdate = "UPDATE Detalles SET IdPedido = ?, IdProducto = ?, Cantidad = ? WHERE IdPedido = ?";
        private const string SQLDelete = "DELETE FROM Detalles WHERE IdDetalle = ?";
        private const string SQLDeletePedido = "DELETE FROM Detalles WHERE IdPedido = ?";

        private daConexion connectionDA = new daConexion();

        public daDetalle() {}

        public DetalleEntity CrearEntidad(OdbcDataReader dr) {
            DetalleEntity entidad = new DetalleEntity();
            entidad.IdDetalle = Convert.ToInt32(dr["IdDetalle"]);
            entidad.IdPedido = Convert.ToInt32(dr["IdPedido"]);
            entidad.IdProducto = Convert.ToInt32(dr["IdProducto"]);
            entidad.Cantidad = Convert.ToInt32(dr["Cantidad"]);
            return entidad;
        }

        private void CrearParametros(OdbcCommand command, DetalleEntity entidad) {
            OdbcParameter parameter = null;

            parameter = command.Parameters.Add("?", OdbcType.Int);
            parameter.Value = entidad.IdDetalle;

            parameter = command.Parameters.Add("?", OdbcType.Int);
            parameter.Value = entidad.IdPedido;

            parameter = command.Parameters.Add("?", OdbcType.Int);
            parameter.Value = entidad.IdProducto;

            parameter = command.Parameters.Add("?", OdbcType.Int);
            parameter.Value = entidad.Cantidad;
        }

        private void EjecutarComando(TipoComando sqlCommandType, DetalleEntity entidad) {
            OdbcConnection connection = null;
            OdbcCommand command = null;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                IDataParameter paramId = new OdbcParameter("?", OdbcType.Int);
                paramId.Value = entidad.IdDetalle;

                switch(sqlCommandType) {
                    case TipoComando.Insertar:
                        command = new OdbcCommand(SQLInsert, connection);
                        command.Parameters.Add(paramId);
                        CrearParametros(command, entidad);
                        break;

                    case TipoComando.Actualizar:
                        command = new OdbcCommand(SQLUpdate, connection);
                        command.Parameters.Add(paramId);
                        CrearParametros(command, entidad);
                        break;

                    case TipoComando.Eliminar:
                        command = new OdbcCommand(SQLDelete, connection);
                        command.Parameters.Add(paramId);
                        CrearParametros(command, entidad);
                        break;

                    case TipoComando.Eliminar2:
                        command = new OdbcCommand(SQLDeletePedido, connection);
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
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            List<DetalleEntity> detalles = null;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLSearch, connection);
                command.Parameters.Add("?", OdbcType.Int);
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
            EjecutarComando(daComun.TipoComando.Insertar, entidad);
        }

        public void Actualizar(DetalleEntity entidad) {
            EjecutarComando(daComun.TipoComando.Actualizar, entidad);
        }

        public void Eliminar(int id) {
            DetalleEntity entidad = new DetalleEntity();
            entidad.IdPedido = id;
            EjecutarComando(daComun.TipoComando.Eliminar, entidad);
        }

        public void EliminarPorPedido(int idpedido) {
            DetalleEntity entidad = new DetalleEntity();
            entidad.IdPedido = idpedido;
            EjecutarComando(daComun.TipoComando.Eliminar2, entidad);
        }
    }
}
