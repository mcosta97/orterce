using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerEntity {
    public class DetalleEntity {
        private int iddetalle;
        private int idpedido;
        private int idproducto;
        private int cantidad;

        public DetalleEntity() {
            iddetalle = 0;
            idpedido = 0;
            idproducto = 0;
            cantidad = 0;
        }

        public int IdPedido {
            get {
                return idpedido;
            }
            set {
                idpedido = value;
            }
        }

        public int IdDetalle {
            get {
                return iddetalle;
            }
            set {
                iddetalle = value;
            }
        }

        public int IdProducto {
            get {
                return idproducto;
            }
            set {
                idproducto = value;
            }
        }

        public int Cantidad {
            get {
                return cantidad;
            }
            set {
                cantidad = value;
            }
        }
    }
}
