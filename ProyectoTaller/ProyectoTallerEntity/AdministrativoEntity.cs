using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerEntity
{
    class AdministrativoEntity : UsuarioEntity
    {
        private int acceso;

        public AdministrativoEntity()
        {
            acceso = 0;
        }

        public int Acceso
        {
            get
            {
                return acceso;
            }
            set
            {
                acceso = value;
            }
        }
    }
}
