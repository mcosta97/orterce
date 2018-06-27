using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerEntity {
    public class AdministrativoEntity : UsuarioEntity {
        private int idadministrativo;
        private int acceso;

        public AdministrativoEntity() {
            Acceso = 0;
            IdAdministrativo = 0;
        }

        public int IdAdministrativo {
            get {
                return idadministrativo;
            }
            set {
                idadministrativo = value;
            }
        }

        public int Acceso {
            get {
                return acceso;
            }
            set {
                acceso = value;
            }
        }
    }
}
