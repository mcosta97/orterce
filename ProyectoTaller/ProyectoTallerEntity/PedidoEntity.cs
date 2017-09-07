using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerEntity
{
    class PedidoEntity
    {
        private int idpedido;
        private int idcliente;
        private Nullable<DateTime> fecha;
        private List<DetalleEntity> detalles;

        public PedidoEntity()
        {
            IdPedido = 0;
            IdCliente = 0;
            Fecha = null;
            Detalles = null;
        }

        public int IdPedido
        {
            get
            {
                return idpedido;
            }
            set
            {
                idpedido = value;
            }
        }

        public int IdCliente
        {
            get
            {
                return idcliente;
            }
            set
            {
                idcliente = value;
            }
        }

        public Nullable<DateTime> Fecha
        {
            get
            {
                return fecha;
            }
            set
            {
                fecha = value;
            }
        }

        public List<DetalleEntity> Detalles
        {
            get
            {
                return detalles;
            }
            set
            {
                detalles = value;
            }
        }
    }
}
