using ProyectoTallerBussines;
using ProyectoTallerEntity;
using System;

public partial class MasterPage : System.Web.UI.MasterPage {

    private obPedido bussinesPedido;

    protected void Page_Load(object sender, EventArgs e) {
        bussinesPedido = new obPedido();
        if (Session["UserID"] == null) {
            logged.Visible = false;
            login.Visible = true;
        } else {
            login.Visible = false;
            logged.Visible = true;

            try {
                if (((AdministrativoEntity)Session["UserID"]) != null) {
                    admenu.Visible = true;
                }
            } catch (InvalidCastException ic) {
                admenu.Visible = false;
            }

            if(Session["PedID"] == null) {
                carrito.Visible = false;
            } else {
                carrito.Visible = true;
                items.DataSource = bussinesPedido.ObtenerItemsCarrito(1);
                items.DataBind();
            }
        }
    }
}