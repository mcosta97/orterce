using ProyectoTallerDataODBC;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static ProyectoTallerData.daComun;

namespace ProyectoTallerData {
    public class daPedido {
        private const string SQLSearchByPrimaryKey = "SELECT * FROM Pedidos WHERE IdPedido = @IdPedido";
        private const string SQLSearch = "SELECT * FROM Pedidos WHERE IdCliente = @IdCliente";
        private const string SQLInsert = "INSERT INTO Pedidos (IdPedido, IdCliente, Fecha, Estado) VALUES ((SELECT MAX(IdPedido) + 1 FROM Pedidos), @IdCliente, @Fecha, @Estado)";
        private const string SQLUpdate = "UPDATE Pedidos SET IdCliente = @IdCliente, Fecha = @Fecha, Estado = @Estado WHERE IdPedido = @IdPedido";
        private const string SQLDelete = "DELETE FROM Pedidos WHERE IdPedido = @IdPedido";
        private const string SQLItemsCarrito = "SELECT pro.idproducto, imagen, cantidad, nombre, precio, modelo, iddetalle FROM pedidos ped INNER JOIN detalles det ON det.idpedido = ped.idpedido INNER JOIN productos pro ON pro.idproducto = det.idproducto WHERE ped.idpedido = @IdCPedido";
        private const string SQLPedidoAbierto = "SELECT idpedido FROM Pedidos P INNER JOIN Clientes C ON C.idcliente = P.idcliente WHERE P.estado = 5 AND idusuario = @IdUsuario";

        private daConexion connectionDA = new daConexion();

        public daPedido() { }

        public PedidoEntity CrearEntidad(SqlDataReader dr) {
            PedidoEntity entidad = new PedidoEntity();
            entidad.IdPedido = Convert.ToInt32(dr["IdPedido"]);
            entidad.IdCliente = Convert.ToInt32(dr["IdCliente"]);
            entidad.Fecha = Convert.ToDateTime(dr["Fecha"]);
            entidad.Estado = Convert.ToInt32(dr["Estado"]);
            entidad.Detalles = new daDetalle().ObtenerDetallesPorPedido(entidad.IdPedido);
            entidad.Total = Convert.ToDouble(dr["Total"]);
            return entidad;
        }

        private void CrearParametros(SqlCommand command, PedidoEntity entidad) {
            SqlParameter parameter = null;

            parameter = command.Parameters.Add("@IdCliente", SqlDbType.Int);
            parameter.Value = entidad.IdCliente;

            parameter = command.Parameters.Add("@Fecha", SqlDbType.DateTime);
            parameter.Value = entidad.Fecha;

            parameter = command.Parameters.Add("@Estado", SqlDbType.VarChar);
            parameter.Value = entidad.Estado;

            parameter = command.Parameters.Add("@Total", SqlDbType.Decimal);
            parameter.Value = entidad.Total;
        }

        private void EjecutarComando(TipoComando sqlCommandType, PedidoEntity entidad) {
            SqlConnection connection = null;
            SqlCommand command = null;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                IDataParameter paramId = new SqlParameter("@IdPedido", SqlDbType.Int);
                paramId.Value = entidad.IdPedido;

                switch (sqlCommandType) {
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
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                if (command != null) { command.Dispose(); }
                if (connection != null) { connection.Dispose(); }
            }
        }

        public int ObtenerPedidoAbierto(int idusuario) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            int pedido = 0;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLPedidoAbierto, connection);
                command.Parameters.Add("@IdUsuario", SqlDbType.Int);
                command.Parameters[0].Value = idusuario;
                dr = command.ExecuteReader();

                while (dr.Read()) {
                    pedido = Convert.ToInt32(dr["idpedido"]);
                }

                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                if (command != null) { command.Dispose(); }
                if (connection != null) { connection.Dispose(); }
            }

            return pedido;
        }

        public PedidoEntity ObtenerPedido(int idpedido) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            PedidoEntity pedido = null;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchByPrimaryKey, connection);
                command.Parameters.Add("@IdPedido", SqlDbType.Int);
                command.Parameters[0].Value = idpedido;
                dr = command.ExecuteReader();

                while (dr.Read()) {
                    pedido = CrearEntidad(dr);
                }

                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                if (command != null) { command.Dispose(); }
                if (connection != null) { connection.Dispose(); }
            }

            return pedido;
        }

        public DataTable ObtenerPedidoCarrito(int idpedido) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLItemsCarrito, connection);
                command.Parameters.Add("@IdPedido", SqlDbType.Int);
                command.Parameters[0].Value = idpedido;
                da = new SqlDataAdapter(command);
                da.Fill(dt);

                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                if (command != null) { command.Dispose(); }
                if (connection != null) { connection.Dispose(); }
            }

            return dt;
        }

        public List<PedidoEntity> ObtenerPedidosPorCliente(int idcliente) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            List<PedidoEntity> pedidos = null;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearch, connection);
                command.Parameters.Add("@IdCliente", SqlDbType.Int);
                command.Parameters[0].Value = idcliente;
                dr = command.ExecuteReader();

                pedidos = new List<PedidoEntity>();

                while (dr.Read()) {
                    pedidos.Add(CrearEntidad(dr));
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

            return pedidos;
        }

        public void Insertar(PedidoEntity entidad) {
            EjecutarComando(TipoComando.Insertar, entidad);

            daDetalle detalles = new daDetalle();
            foreach (DetalleEntity detalle in entidad.Detalles) {
                detalles.Insertar(detalle);
            }
        }

        public void Actualizar(PedidoEntity entidad) {
            EjecutarComando(TipoComando.Actualizar, entidad);

            daDetalle detalles = new daDetalle();
            foreach (DetalleEntity detalle in entidad.Detalles) {
                detalles.Actualizar(detalle);
            }
        }

        public void Eliminar(int id) {
            PedidoEntity entidad = new PedidoEntity();
            entidad.IdPedido = id;
            EjecutarComando(TipoComando.Eliminar, entidad);
            new daDetalle().EliminarPorPedido(id);
        }

    }
}
