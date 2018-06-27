using ProyectoTallerBussines;
using ProyectoTallerData;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AbmCategoria : Page {

    private obCategoria bussinesCategoria;

    protected void Page_Load(object sender, EventArgs e) {
        bussinesCategoria = new obCategoria();
        try {
            if(((AdministrativoEntity) this.Session["UserID"]) != null && ((AdministrativoEntity) this.Session["UserID"]).Acceso == 2) {
                MostrarCategorias();
            } else {
                Response.Redirect("Login.aspx");
            }
        } catch(InvalidCastException ic) {
            Response.Redirect("Principal.aspx");
        }
    }

    protected void MostrarCategorias() {
        categorias.DataSource = new daCategoria().ObtenerCategoriasTabla();
        categorias.DataBind();
    }

    protected void OnRowDataBoundProductos(object sender, GridViewRowEventArgs e) {
        if(e.Row.RowType == DataControlRowType.DataRow) {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(categorias, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click para seleccionar la categoria.";
        }
    }

    protected void ObtenerCategoriaSeleccionado(object sender, GridViewCommandEventArgs e) {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.categorias.Rows[index];
        string id = row.Cells[0].Text;
        CategoriaEntity categoria = new daCategoria().ObtenerCategoria(Convert.ToInt32(id));
        txtId.Text = categoria.IdCategoria.ToString();
        txtCategoria.Text = categoria.Nombre;
    }

    protected void btnAlta_Click(object sender, EventArgs e) {
        CategoriaEntity categoria = new CategoriaEntity();
        categoria.Nombre = txtCategoria.Text;
        bussinesCategoria.CrearCategoria(categoria);
    }

    protected void btnBaja_Click(object sender, EventArgs e) {
        CategoriaEntity categoria = new CategoriaEntity();
        categoria.IdCategoria = Convert.ToInt32(txtId.Text);
        bussinesCategoria.EliminarCategoria(categoria);
    }

    protected void btnEdit_Click(object sender, EventArgs e) {
        CategoriaEntity categoria = new CategoriaEntity();
        categoria.Nombre = txtCategoria.Text;
        categoria.IdCategoria = Convert.ToInt32(txtId.Text);
        bussinesCategoria.ActualizarCategoria(categoria);
    }
}