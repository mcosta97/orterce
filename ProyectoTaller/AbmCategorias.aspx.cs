using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AbmCategoria : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        if (((AdministrativoEntity)this.Session["AdminID"]).Acceso != 1) {
            Response.Redirect("Principal.aspx");
        }
    }
}