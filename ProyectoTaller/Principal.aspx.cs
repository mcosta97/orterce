using ProyectoTallerBussines;
using ProyectoTallerData;
using ProyectoTallerDataODBC;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Principal : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        AgregarCategorias();

        int id = Convert.ToInt32(Request.QueryString["Id"]);
        if(id > 0) {
            TraerProductos(obCategoria.CargarProductos(id));
        } else {
            MostrarPromocionados();
        }
    }

    private void AgregarCategorias() {
        d1.DataSource = new daCategoria().ObtenerCategoriasTabla();
        d1.DataBind();
    }

    private void MostrarPromocionados() {
        p1.DataSource = new daProducto().ObtenerProductosPromocionadosTabla();
        p1.DataBind();
    }

    private void TraerProductos(DataTable productos) {
        p1.DataSource = productos;
        p1.DataBind();
    }
}