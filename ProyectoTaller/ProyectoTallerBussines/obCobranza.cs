using mercadopago;
using System.Collections;

namespace ProyectoTallerBussines {
    //Documentacion: https://www.mercadopago.com.ar/developers/es/tools/sdk/server/dotnet/
    public class obCobranza {
        private MP mp;

        public obCobranza() {
            mp = new MP("391067284597775", "KLZzoz2uxaTKo7v5hjvgdANF7rxEIs66");
        }

        public Hashtable ObtenerCobro(string id) {
            Hashtable cobro = mp.getPreference(id);
            return cobro;
        }

        public Hashtable CrearCobro() {
            Hashtable cobro = mp.createPreference("{\"items\":[{\"title\":\"sdk-dotnet\",\"quantity\":1,\"currency_id\":\"ARS\",\"unit_price\":10.5}]}");
            return cobro;
        }

        public Hashtable ActualizarCobro(string id) {
            Hashtable cobro = mp.updatePreference(id, "{\"items\":[{\"title\":\"sdk-dotnet\",\"quantity\":1,\"currency_id\":\"USD\",\"unit_price\":2}]}");
            return cobro;
        }
    }
}
