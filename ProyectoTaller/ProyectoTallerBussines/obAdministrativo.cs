using ProyectoTallerData;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerBussines {
    public class obAdministrativo : obUsuario {
        public static bool ValidarAdministrativo(AdministrativoEntity administrativo) {
            bool valido = true;

            if (administrativo.Acceso.Equals("")) {
                valido = false;
            }

            if (!ValidarUsuario(administrativo)) {
                valido = false;
            }

            return valido;
        }

        public static void ActualizarAdministrativo(AdministrativoEntity administrativo) {
            daAdministrativo da = new daAdministrativo();
            da.Actualizar(administrativo);
        }

        public static void CrearAdministrativo(AdministrativoEntity administrativo) {
            if (ValidarAdministrativo(administrativo)) {
                daAdministrativo da = new daAdministrativo();
                da.Insertar(administrativo);
            }
        }

        public static void EliminarAdministrativo(AdministrativoEntity administrativo) {
            daAdministrativo da = new daAdministrativo();
            da.Eliminar(administrativo.IdAdministrativo);
        }
    }
}
