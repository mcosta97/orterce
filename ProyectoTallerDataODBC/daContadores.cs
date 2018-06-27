using ProyectoTallerData;
using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerDataODBC {
    public class daContadores {
        private const string SQLObtenerContadores = "SELECT * FROM Contadores";
        private const string SQLUpdUsuario = "Update contadores set usuario=usuario+1";
        private const string SQLUpdAdministrativo = "Update contadores set administrativo=administrativo+1";
        private const string SQLUpdCliente = "Update contadores set cliente=cliente+1";
        private const string SQLUpdCategoria = "Update contadores set categoria=categoria+1";
        private const string SQLUpdDetalle = "Update contadores set detalle=detalle+1";
        private const string SQLUpdDireccion = "Update contadores set direccion=direccion+1";
        private const string SQLUpdPedido = "Update contadores set pedido=pedido+1";
        private const string SQLUpdProducto = "Update contadores set producto=producto+1";
        private const string SQLUpdTelefono = "Update contadores set telefono=telefono+1";

        private daConexion connectionDA = new daConexion();

        public daContadores() {}

        private void EjecutarComando(Contador contador) {
            SqlConnection connection = null;
            SqlCommand command = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();

                switch(contador) {
                    case Contador.Usuario:
                        command = new SqlCommand(SQLUpdUsuario, connection);
                        break;

                    case Contador.Administrador:
                        command = new SqlCommand(SQLUpdAdministrativo, connection);
                        break;

                    case Contador.Cliente:
                        command = new SqlCommand(SQLUpdCliente, connection);
                        break;

                    case Contador.Categoria:
                        command = new SqlCommand(SQLUpdCategoria, connection);
                        break;

                    case Contador.Detalle:
                        command = new SqlCommand(SQLUpdDetalle, connection);
                        break;

                    case Contador.Direccion:
                        command = new SqlCommand(SQLUpdDireccion, connection);
                        break;

                    case Contador.Pedido:
                        command = new SqlCommand(SQLUpdPedido, connection);
                        break;

                    case Contador.Producto:
                        command = new SqlCommand(SQLUpdProducto, connection);
                        break;

                    case Contador.Telefono:
                        command = new SqlCommand(SQLUpdTelefono, connection);
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

        public void Sumar(Contador contador) {
            EjecutarComando(contador);
        }

        public int TraerContador(Contador contador) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            int cont;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLObtenerContadores, connection);
                dr = command.ExecuteReader();

                cont = -1;

                while(dr.Read()) {
                    switch(contador) {
                        case Contador.Usuario:
                            cont = Convert.ToInt32(dr["usuario"]);
                            break;
                        case Contador.Cliente:
                            cont = Convert.ToInt32(dr["cliente"]);
                            break;
                        case Contador.Administrador:
                            cont = Convert.ToInt32(dr["administrativo"]);
                            break;
                        case Contador.Categoria:
                            cont = Convert.ToInt32(dr["categoria"]);
                            break;
                        case Contador.Producto:
                            cont = Convert.ToInt32(dr["producto"]);
                            break;
                    }
                    
                }

                connection.Close();
            } catch(Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;
                if(command != null) {command.Dispose();}
                if(connection != null) {connection.Dispose();}
            }

            return cont;
        }
    }
}
