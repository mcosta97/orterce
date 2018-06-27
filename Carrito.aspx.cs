using ProyectoTallerBussines;
using ProyectoTallerEntity;
using System;

public partial class Carrito : System.Web.UI.Page {

    private obPedido bussinesPedido;

    protected void Page_Load(object sender, EventArgs e) {
        bussinesPedido = new obPedido();
        PedidoEntity pedido = (PedidoEntity) Session["PedID"];
        if(pedido != null) {
            try {
                int borra = Convert.ToInt32(Request.QueryString["RemDet"]);

                if (borra != 0) {
                    bussinesPedido.SacarProducto(pedido.Detalles.Find(detalles => detalles.IdDetalle == borra), pedido);
                }

                total.InnerText = "Total: $" + pedido.Total;
                carrito.DataSource = bussinesPedido.ObtenerItemsCarrito(pedido.IdPedido);
                carrito.DataBind();
            } catch (Exception) {
                Response.Redirect("Login.aspx");
            }
        } else {
            Response.Redirect("Login.aspx");
        }
    }
}