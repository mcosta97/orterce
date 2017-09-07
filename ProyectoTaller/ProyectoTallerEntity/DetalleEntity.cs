using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerEntity
{
    class DetalleEntity
    {
        private int iddetalle;
        private int idproducto;
        private int cantidad;

        public DetalleEntity()
        {
            iddetalle = 0;
            idproducto = 0;
            cantidad = 0;
        }

        public int IdDetalle
        {
            get
            {
                return iddetalle;
            }
            set
            {
                iddetalle = value;
            }
        }

        public int IdProducto
        {
            get
            {
                return idproducto;
            }
            set
            {
                idproducto = value;
            }
        }

        public int Cantidad
        {
            get
            {
                return cantidad;
            }
            set
            {
                cantidad = value;
            }
        }
    }
}
