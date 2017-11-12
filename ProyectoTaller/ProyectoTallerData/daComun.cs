using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerData
{
    public class daComun
    {
        private daComun()
        {
        }

        public enum TipoComandoEnum
        {
            Insertar,
            Actualizar,
            Eliminar,
            Eliminar2
        }

        public enum Contador {
            Usuario,
            Cliente,
            Administrador,
            Categoria,
            Producto,
            Telefono,
            Direccion,
            Pedido,
            Detalle
        }

        public enum EstadoPedido {
            Aprobado,
            Cancelado,
            Cobrado,
            Enviado,
            Creado
        }
    }
}
