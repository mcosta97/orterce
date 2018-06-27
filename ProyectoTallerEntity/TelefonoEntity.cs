namespace ProyectoTallerEntity
{
    public class TelefonoEntity
    {
        private int idtelefono;
        private int idcliente;
        private string telefono;

        public TelefonoEntity()
        {
            IdTelefono = 0;
            IdCliente = 0;
            telefono = "";
        }

        public int IdTelefono
        {
            get
            {
                return idtelefono;
            }
            set
            {
                idtelefono = value;
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

        public string Telefono
        {
            get
            {
                return telefono;
            }
            set
            {
                telefono = value;
            }
        }
    }
}