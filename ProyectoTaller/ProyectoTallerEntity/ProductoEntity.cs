using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerEntity
{
    public class ProductoEntity
    {
        private int idproducto;
        private string nombre;
        private string descripcion;
        private double precio;
        private double iva;
        private int stock;
        private int peso;
        private string color;
        private string modelo;
        private string medida;

        public ProductoEntity()
        {
            IdProducto = 0;
            Nombre = "";
            Descripcion = "";
            Precio = 0.0d;
            Iva = 0.0d;
            Stock = 0;
            Peso = 0;
            Color = "";
            Modelo = "";
            Medida = "";
        }

        public int IdProducto
        {
            get
            {
                return idproducto;
            }
            set
            {
                idproducto = value;
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
                nombre = value.Trim();
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }
            set
            {
                descripcion = value.Trim();
            }
        }

        public double Precio
        {
            get
            {
                return precio;
            }
            set
            {
                precio = value;
            }
        }

        public double Iva
        {
            get
            {
                return iva;
            }
            set
            {
                iva = value;
            }
        }

        public int Stock
        {
            get
            {
                return stock;
            }
            set
            {
                stock = value;
            }
        }

        public int Peso
        {
            get
            {
                return peso;
            }
            set
            {
                peso = value;
            }
        }

        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value.Trim();
            }
        }

        public string Modelo
        {
            get
            {
                return modelo;
            }
            set
            {
                modelo = value.Trim();
            }
        }

        public string Medida
        {
            get
            {
                return medida;
            }
            set
            {
                medida = value.Trim();
            }
        }
    }
}