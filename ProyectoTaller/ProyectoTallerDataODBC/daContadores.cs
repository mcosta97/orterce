using ProyectoTallerData;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
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
            OdbcConnection connection = null;
            OdbcCommand command = null;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();

                switch(contador) {
                    case Contador.Usuario:
                        command = new OdbcCommand(SQLUpdUsuario, connection);
                        break;

                    case Contador.Administrador:
                        command = new OdbcCommand(SQLUpdAdministrativo, connection);
                        break;

                    case Contador.Cliente:
                        command = new OdbcCommand(SQLUpdCliente, connection);
                        break;

                    case Contador.Categoria:
                        command = new OdbcCommand(SQLUpdCategoria, connection);
                        break;

                    case Contador.Detalle:
                        command = new OdbcCommand(SQLUpdDetalle, connection);
                        break;

                    case Contador.Direccion:
                        command = new OdbcCommand(SQLUpdDireccion, connection);
                        break;

                    case Contador.Pedido:
                        command = new OdbcCommand(SQLUpdPedido, connection);
                        break;

                    case Contador.Producto:
                        command = new OdbcCommand(SQLUpdProducto, connection);
                        break;

                    case Contador.Telefono:
                        command = new OdbcCommand(SQLUpdTelefono, connection);
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
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            int cont;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLObtenerContadores, connection);
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
