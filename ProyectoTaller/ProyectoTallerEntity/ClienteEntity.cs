using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerEntity
{
    public class ClienteEntity : UsuarioEntity
    {
        private int idcliente;
        private string dni;
        private List<TelefonoEntity> telefonos;
        private List<DireccionEntity> direcciones;

        public ClienteEntity()
        {
            IdCliente = 0;
            Dni = "";
            telefonos = null;
            direcciones = null;
        }

        public int IdCliente {
            get {
                return idcliente;
            }
            set {
                idcliente = value;
            }
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

        public void AgregarTelefono(TelefonoEntity telefono, byte operacion)
        {
            telefonos.Add(telefono);
        }


        public void EliminarTelefono(TelefonoEntity telefono)
        {
            telefonos.Remove(telefono);
        }

        public void AgregarDireccion(DireccionEntity direccion)
        {
            direcciones.Add(direccion);
        }


        public void EliminarDireccion(DireccionEntity direccion)
        {
            direcciones.Remove(direccion);
        }
    }
}
