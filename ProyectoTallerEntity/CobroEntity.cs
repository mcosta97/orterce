using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerEntity {
    public class CobroEntity {
        public string title { get; set; }
        public int quantity { get; set; }
        public string currency_id { get; set; }
        public float unit_price { get; set; }

        public CobroEntity(string titulo, int cantidad, string moneda, float precio_unitario) {
            title = titulo;
            quantity = cantidad;

            if (moneda.Equals("ARS") || moneda.Equals("USD")) {
                currency_id = moneda;
            } else {
                currency_id = "ARS";
            }
            
            unit_price = precio_unitario;
        }

    }       
}
