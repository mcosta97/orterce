using ProyectoTallerData;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerBussines {
    public class obUsuario {
        public static UsuarioEntity ValidarLogin(String usuario, String clave) {
            UsuarioEntity user = new daUsuario().Buscar(usuario, clave);
            return user;
        }

        public static void ActualizarUsuario(UsuarioEntity usuario) {
            daUsuario da = new daUsuario();
            da.Actualizar(usuario);
        }

        public static AdministrativoEntity esAdministrativo(UsuarioEntity usuario) {
            return new daAdministrativo().ObtenerAdministrativo(usuario.IdUsuario.ToString());
        }

        public static List<DireccionEntity> VerificarDomicilios(UsuarioEntity usuario) {
            return new daDireccion().ObtenerDirecciones(usuario.IdUsuario);
        }

        public static List<TelefonoEntity> VerificarTelefonos(UsuarioEntity usuario) {
            return new daTelefono().ObtenerTelefonos(usuario.IdUsuario);
        }

        public static bool ValidarUsuario(UsuarioEntity usuario) {
            bool valido = true;

            if(usuario.Nombre.Equals("")) {
                valido = false;
            }

            if(usuario.Apellido.Equals("")) {
                valido = false;
            }

            if(usuario.Usuario.Equals("")) {
                valido = false;
            }

            if(usuario.Clave.Equals("")) {
                valido = false;
            }

            if(usuario.Mail.Equals("")) {
                valido = false;
            }

            if(UsuarioExiste(usuario.Usuario)) {
                valido = false;
            }

            return valido;
        }

        private static bool UsuarioExiste(string usuario) {
            return new daUsuario().ObtenerRecuperacionUsuario(usuario) != null;
        }

        public static void CrearUsuario(UsuarioEntity usuario) {
            daUsuario da = new daUsuario();
            if (ValidarUsuario(usuario)) {
                da.Insertar(usuario);
            }
        }

        public static void EliminarUsuario(UsuarioEntity usuario) {
            daUsuario da = new daUsuario();
            da.Eliminar(usuario.IdUsuario);
        }

        public static int EstablecerPermisos(UsuarioEntity usuario) {
            return new daAdministrativo().ObtenerAcceso(usuario.IdUsuario);
        }

        public static UsuarioEntity RecuperarClave(string user) {
            daUsuario admin_usuario = new daUsuario();
            UsuarioEntity usuario = admin_usuario.ObtenerRecuperacionUsuario(user);
            if(usuario != null) {
                usuario.Clave = GenerarClave();
                admin_usuario.Actualizar(usuario);
            }
            return usuario;
        }

        private static string GenerarClave() {
            string permitidos = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789!@$?";
            Byte[] randomBytes = new Byte[8];
            char[] chars = new char[8];

            for(int i = 0; i < 8; i++) {
                Random randomObj = new Random();
                randomObj.NextBytes(randomBytes);
                chars[i] = permitidos[(int) randomBytes[i] % permitidos.Length];
            }

            return new string(chars);
        }

        public static void AgregarTelefono(string telefono, int idcliente) {
            TelefonoEntity tel = new TelefonoEntity();
            tel.IdCliente = idcliente;
            tel.Telefono = telefono;
            daTelefono da = new daTelefono();
            da.Insertar(tel);
        }

        public static void AgregarDireccion(DireccionEntity dire, int idUsuario) {
            daDireccion da = new daDireccion();
            da.Insertar(dire);
        }
    }
}
