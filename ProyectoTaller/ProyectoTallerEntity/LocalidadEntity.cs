using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerEntity {
    public class LocalidadEntity {
        private int idlocalidad;
        private ProvinciaEntity provincia;
        private string nombre;

        public LocalidadEntity() {
            IdLocalidad = 0;
            Provincia = null;
            Nombre = "";
        }

        public int IdLocalidad {
            get {
                return idlocalidad;
            }
            set {
                idlocalidad = value;
            }
        }

        public ProvinciaEntity Provincia {
            get {
                return provincia;
            }
            set {
                provincia = value;
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
