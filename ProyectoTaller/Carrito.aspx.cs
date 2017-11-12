using ProyectoTallerBussines;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Carrito : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        PedidoEntity pedido = (PedidoEntity) Session["PedId"];
        if(pedido != null) {
            int borra = 0;
            borra = Convert.ToInt32(Request.QueryString["RemDet"]);
            if(borra != 0) {
                obPedido.SacarProducto(pedido.Detalles.Find(detalles => detalles.IdDetalle == borra), pedido);
            }

            carrito.DataSource = obPedido.ObtenerItemsCarrito(((PedidoEntity) Session["PedId"]).IdPedido);
            //carrito.DataSource = obPedido.ObtenerItemsCarrito(1);
            carrito.DataBind();
        } else {
            Response.Redirect("Login.aspx");
        }
    }
}