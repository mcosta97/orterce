using ProyectoTallerData;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerBussines {
    public class obDetalle {
        public static void InsertarDetalles(List<DetalleEntity> detalles) {
            foreach (DetalleEntity detalle in detalles) {
                if(detalle != null) {
                    new daDetalle().Insertar(detalle);
                }
            }
        }

        public static void EliminarDetalle(DetalleEntity detalle) {
            if(detalle != null) {
                new daDetalle().Eliminar(detalle.IdDetalle);
            }
        }

        public static void ActualizarDetalle(DetalleEntity detalle) {
            if(detalle != null) {
                new daDetalle().Actualizar(detalle);
            }
        }

        public static void EliminarDetalles(int idpedido) {
            new daDetalle().EliminarPorPedido(idpedido);
        }
    }
}
