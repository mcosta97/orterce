using ProyectoTallerBussines;
using ProyectoTallerEntity;
using System;

public partial class Productos : System.Web.UI.Page {
    ProductoEntity producto = null;
    protected void Page_Load(object sender, EventArgs e) {
        int productoCargado = Convert.ToInt32(Request.QueryString["Id"]);
        if (productoCargado != 0) {
            TraerProducto(obProducto.CargarProducto(productoCargado));
            for (int i = 1; i < 101; i++) {
                ddCantidad.Items.Add(i.ToString());
            }
        } else {
            Response.Redirect("Principal.aspx");
        }
    }

    protected void TraerProducto(ProductoEntity producto) {
        this.producto = producto;

        imagen.ImageUrl = producto.Imagen;
        titulo.Text = producto.Nombre;
        descripcion.Text = "Descripcion: " + producto.Descripcion;
        stock.Text = "Stock: " + producto.Stock.ToString();
        precio.Text = "$" + obProducto.CalcularPrecioIva(producto);
        iva.Text = "IVA: " + producto.Iva.ToString() + "%";
        modelo.Text = producto.Modelo;
        medidas.Text = "Medidas: " + producto.Medida;
        color.Text = "Color: " + producto.Color;
        peso.Text = "Peso: " + producto.Peso.ToString();
    }

    protected void add_Click(object sender, EventArgs e) {
        DetalleEntity detalle = new DetalleEntity();
        if (Session["UserID"] != null) {
            if (Session["PedID"] == null) {
                PedidoEntity pedido = new PedidoEntity();
                pedido.Estado = 4;
                pedido.Fecha = DateTime.Now;
                pedido.IdCliente = ((UsuarioEntity)Session["UserID"]).IdUsuario;
                Session["PedID"] = pedido;
            }
            detalle.Cantidad = 1;
            detalle.IdPedido = 0;
            detalle.IdProducto = producto.IdProducto;
            obPedido.AgregarProducto(detalle, (PedidoEntity)Session["PedID"]);
        }
    }
}