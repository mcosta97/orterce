using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerEntity {
    public class ProvinciaEntity {
        private int idprovincia;
        private string nombre;

        public ProvinciaEntity() {
            IdProvincia = 0;
            Nombre = "";
        }

        public int IdProvincia {
            get {
                return idprovincia;
            }
            set {
                idprovincia = value;
            }
        }

        public string Nombre {
            get {
                return nombre;
            }
            set {
                nombre = value;
            }
        }

    }
}
