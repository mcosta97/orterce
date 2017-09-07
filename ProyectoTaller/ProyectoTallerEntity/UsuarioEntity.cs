using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerEntity
{
    class UsuarioEntity
    {
        private int idusuario;
        private string usuario;
        private string clave;
        private string nombre;
        private string apellido;
        private string mail;

        public UsuarioEntity()
        {
            IdUsuario = 0;
            Usuario = "";
            Clave = "";
            Nombre = "";
            Apellido = "";
            Mail = "";
        }

        public int IdUsuario
        {
            get
            {
                return idusuario;
            }
            set
            {
                idusuario = value;
            }
        }

        public string Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                usuario = value;
            }
        }

        public string Clave
        {
            get
            {
                return clave;
            }
            set
            {
                clave = value;
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

        public string Apellido
        {
            get
            {
                return apellido;
            }
            set
            {
                apellido = value;
            }
        }

        public string Mail
        {
            get
            {
                return mail;
            }
            set
            {
                mail = value;
            }
        }
    }
}
