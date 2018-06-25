using ProyectoTallerData;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerBussines {
    public class obDetalle {
        public void InsertarDetalles(List<DetalleEntity> detalles) {
            foreach (DetalleEntity detalle in detalles) {
                if(detalle != null) {
                    new daDetalle().Insertar(detalle);
                }
            }
        }

        public void EliminarDetalle(DetalleEntity detalle) {
            if(detalle != null) {
                new daDetalle().Eliminar(detalle.IdDetalle);
            }
        }

        public void ActualizarDetalle(DetalleEntity detalle) {
            if(detalle != null) {
                new daDetalle().Actualizar(detalle);
            }
        }

        public void EliminarDetalles(int idpedido) {
            new daDetalle().EliminarPorPedido(idpedido);
        }
    }
}
