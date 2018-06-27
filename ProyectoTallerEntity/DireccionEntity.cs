namespace ProyectoTallerEntity {
    public class DireccionEntity {
        private int iddireccion;
        private ClienteEntity cliente;
        private string direccion;
        private int altura;
        private string piso;
        private string dpto;
        private LocalidadEntity localidad;
        private ProvinciaEntity provincia;

        public DireccionEntity() {
            IdDireccion = 0;
            Cliente = null;
            Direccion = "";
            Localidad = null;
            Provincia = null;
            Altura = 0;
            Piso = "";
            Dpto = "";
        }

        public int IdDireccion {
            get {
                return iddireccion;
            }
            set {
                iddireccion = value;
            }
        }

        public ClienteEntity Cliente {
            get {
                return cliente;
            }
            set {
                cliente = value;
            }
        }

        public string Direccion {
            get {
                return direccion;
            }
            set {
                direccion = value;
            }
        }

        public LocalidadEntity Localidad {
            get {
                return localidad;
            }
            set {
                localidad = value;
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

        public int Altura {
            get {
                return altura;
            }
            set {
                altura = value;
            }
        }

        public string Piso {
            get {
                return piso;
            }
            set {
                piso = value;
            }
        }

        public string Dpto {
            get {
                return dpto;
            }
            set {
                dpto = value;
            }
        }
    }
}