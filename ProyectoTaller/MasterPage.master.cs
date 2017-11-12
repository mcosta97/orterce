using ProyectoTallerBussines;
using ProyectoTallerEntity;
using System;

public partial class MasterPage : System.Web.UI.MasterPage {
    protected void Page_Load(object sender, EventArgs e) {
        if (this.Session["UserID"] == null) {
            logged.Visible = false;
            login.Visible = true;
        } else {
            login.Visible = false;
            logged.Visible = true;

            if((this.Session["UserID"]).GetType().IsAssignableFrom(new AdministrativoEntity().GetType())) {
                admenu.Visible = true;
            } else {
                admenu.Visible = false;
            }

            if(this.Session["PedID"] == null) {
                carrito.Visible = false;
            } else {
                carrito.Visible = true;
                items.DataSource = obPedido.ObtenerItemsCarrito(1);
                items.DataBind();
            }
        }
    }
}