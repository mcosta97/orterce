using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerEntity
{
    class CategoriaEntity
    {
        private int idcategoria;
        private string nombre;

        public CategoriaEntity()
        {
            idcategoria = 0;
            nombre = "";
        }

        public int IdCategoria
        {
            get
            {
                return idcategoria;
            }
            set
            {
                idcategoria = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }
    }
}
