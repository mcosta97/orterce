using ProyectoTallerData;
using ProyectoTallerEntity;
using System.Data;
using static ProyectoTallerData.daComun;

namespace ProyectoTallerBussines {
    public class obPedido {

        public static void CambiarEstadoPedido(PedidoEntity pedido, EstadoPedido tipo) {
            switch (tipo) {
                case EstadoPedido.Aprobado:
                    pedido.Estado = 1;
                    break;
                case EstadoPedido.Cancelado:
                    pedido.Estado = 2;
                    break;
                case EstadoPedido.Cobrado:
                    pedido.Estado = 3;
                    break;
                case EstadoPedido.Creado:
                    pedido.Estado = 4;
                    break;
            }
            new daPedido().Actualizar(pedido);
        }

        public static PedidoEntity ObtenerCarritoUsuario(int idUsuario) {
            daPedido da = new daPedido();
            return da.ObtenerPedido(da.ObtenerPedidoAbierto(idUsuario));
        }

        public static DataTable ObtenerItemsCarrito(int idPedido) {
            return new daPedido().ObtenerPedidoCarrito(idPedido);
        }

        public static bool ValidarPedido(PedidoEntity pedido) {
            bool valido = true;
            if (pedido.Detalles.Count == 0) {valido = false;}
            if (pedido.Fecha.Value == null) {valido = false;}
            if (pedido.IdCliente == 0) {valido = false;}
            if (pedido.Estado.Equals("")) {valido = false;}
            return valido;
        }

        public static int ObtenerCarritoAbierto(int idcliente) {
            return new daPedido().ObtenerPedidoAbierto(idcliente);
        }

        public static void AgregarProducto(DetalleEntity detalle, PedidoEntity pedido) {
            if(pedido != null && detalle != null) {
                pedido.Detalles.Add(detalle);
                pedido.Total += detalle.Cantidad * obProducto.CalcularPrecioIva(obProducto.CargarProducto(detalle.IdProducto));
                new daPedido().Actualizar(pedido);
            }
        }

        public static void SacarProducto(DetalleEntity detalle, PedidoEntity pedido) {
            if(pedido != null && detalle != null) {
                pedido.Detalles.Remove(detalle);
                new daPedido().Actualizar(pedido);
            }
        }

        public static void GuardarPedido(PedidoEntity pedido) {
            if (ValidarPedido(pedido)) {
                new daPedido().Insertar(pedido);
            }
        }

        public static void ActualizarPedido(PedidoEntity pedido) {
            new daPedido().Actualizar(pedido);
        }

        public static void EliminarPedido(PedidoEntity pedido) {
            new daPedido().Eliminar(pedido.IdPedido);
            obDetalle.EliminarDetalles(pedido.IdPedido);
        }
    }
}
