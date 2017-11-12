using ProyectoTallerData;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerBussines {
    public class obProducto {
        public static bool TieneStock(ProductoEntity producto, int cantidad) {
            ProductoEntity prod;
            bool tieneStock;

            prod = new daProducto().ObtenerProductosPorId(producto.IdProducto);

            if(prod.Stock >= cantidad) {
                tieneStock = true;
            } else {
                tieneStock = false;
            }

            return tieneStock;
        }

        public static ProductoEntity CargarProducto(int id) {
            return new daProducto().ObtenerProductosPorId(id);
        }

        public static double CalcularPrecioIva(ProductoEntity producto) {
            return producto.Precio + ((producto.Precio / 100) * producto.Iva);
        }

        public static bool ValidarProducto(ProductoEntity producto) {
            bool valido = true;

            if (producto.IdCategoria == 0) {
                valido = false;
            }

            if (producto.Stock == 0) {
                valido = false;
            }

            if (producto.Nombre.Equals("")) {
                valido = false;
            }

            if (producto.Descripcion.Equals("")) {
                valido = false;
            }

            if (producto.Color.Equals("")) {
                valido = false;
            }

            if (producto.Modelo.Equals("")) {
                valido = false;
            }

            if (producto.Medida.Equals("")) {
                valido = false;
            }

            if (producto.Peso.Equals("")) {
                valido = false;
            }

            if (producto.Imagen.Equals("")) {
                valido = false;
            }

            if (producto.Precio == 0) {
                valido = false;
            }

            if (producto.Iva == 0) {
                valido = false;
            }

            return valido;
        }

        public static void CrearProducto(ProductoEntity producto) {
            if (ValidarProducto(producto)) {
                daProducto da = new daProducto();
                da.Insertar(producto);
            }
        }

        public static void ActualizarProducto(ProductoEntity producto) {
            daProducto da = new daProducto();
            da.Actualizar(producto);
        }

        public static void EliminarProducto(ProductoEntity producto) {
            daProducto da = new daProducto();
            da.Eliminar(producto.IdProducto);
        }
    }
}