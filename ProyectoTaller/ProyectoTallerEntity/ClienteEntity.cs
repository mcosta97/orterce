using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerEntity
{
    class ClienteEntity : UsuarioEntity
    {
        private string dni;
        private List<TelefonoEntity> telefonos;
        private List<DireccionEntity> direcciones;

        public ClienteEntity()
        {
            Dni = "";
        }

        public string Dni
        {
            get
            {
                return dni;
            }
            set
            {
                dni = value;
            }
        }
    }
}
