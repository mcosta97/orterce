using ProyectoTallerData;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerBussines {
    public class obCategoria {
        public bool ValidarCategoria(CategoriaEntity categoria) {
            bool valido = true;

            if (categoria.Nombre.Equals("")) {
                valido = false;
            }

            return valido;
        }

        public DataTable CargarProductos(int idcategoria) {
            return new daProducto().ObtenerProductosPorCategoriaTabla(idcategoria);
        }

        public void CrearCategoria(CategoriaEntity categoria) {
            if (ValidarCategoria(categoria)) {
                daCategoria da = new daCategoria();
                da.Insertar(categoria);
            }
        }

        public void ActualizarCategoria(CategoriaEntity categoria) {
            daCategoria da = new daCategoria();
            da.Actualizar(categoria);
        }

        public void EliminarCategoria(CategoriaEntity categoria) {
            daCategoria da = new daCategoria();
            da.Eliminar(categoria.IdCategoria);
        }
    }
}
