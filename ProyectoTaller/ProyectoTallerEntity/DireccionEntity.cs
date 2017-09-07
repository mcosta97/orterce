namespace ProyectoTallerEntity
{
    class DireccionEntity
    {
        private int iddireccion;
        private int idcliente;
        private string direccion;
        private string localidad;
        private string provincia;

        public DireccionEntity()
        {
            IdDireccion = 0;
            IdCliente = 0;
            Direccion = "";
            Localidad = "";
            Provincia = "";
        }

        public int IdDireccion
        {
            get
            {
                return iddireccion;
            }
            set
            {
                iddireccion = value;
            }
        }

        public int IdCliente
        {
            get
            {
                return idcliente;
            }
            set
            {
                idcliente = value;
            }
        }

        public string Direccion
        {
            get
            {
                return direccion;
            }
            set
            {
                direccion = value;
            }
        }

        public string Localidad
        {
            get
            {
                return localidad;
            }
            set
            {
                localidad = value;
            }
        }

        public string Provincia
        {
            get
            {
                return provincia;
            }
            set
            {
                provincia = value;
            }
        }
    }
}