using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoTallerEntity;
using ProyectoTallerData;

namespace ProyectoTallerBussines {
    public class obCliente : obUsuario {
        public bool ValidarCliente(ClienteEntity cliente) {
            bool valido = true;
            
            if (cliente.Dni.Equals("")) {
                valido = false;
            }

            if (!ValidarUsuario(cliente)) {
                valido = false;
            }

            return valido;
        }

        public void ActualizarCliente(ClienteEntity cliente) {
            new daCliente().Actualizar(cliente);
        }

        public void CrearCliente(ClienteEntity cliente) {
            if (ValidarCliente(cliente)) {
                new daCliente().Insertar(cliente);
            }
        }

        public void EliminarCliente(ClienteEntity cliente) {
            new daCliente().Eliminar(cliente.IdCliente);
        }

        public List<PedidoEntity> ObtenerPedidos(ClienteEntity cliente) {
            return new daPedido().ObtenerPedidosPorCliente(cliente.IdCliente);
        }
    }
}