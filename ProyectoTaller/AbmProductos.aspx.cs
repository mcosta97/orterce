using ProyectoTallerBussines;
using ProyectoTallerData;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AbmProductos : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        try {
            if (((AdministrativoEntity)this.Session["UserID"]) != null && ((AdministrativoEntity)this.Session["UserID"]).Acceso == 2) {
                MostrarProductos();
            } else {
                Response.Redirect("Login.aspx");
            }
        } catch (InvalidCastException ic) {
            Response.Redirect("Principal.aspx");
        }
    }

    protected void MostrarProductos() {
        productos.DataSource = new daProducto().ObtenerProductosTabla();
        productos.DataBind();
        ddCategoria.Items.Add("(Seleccionar)");
        foreach (CategoriaEntity categoria in new daCategoria().ObtenerCategorias()) {
            ddCategoria.Items.Add(categoria.Nombre);
        }
    }

    protected void OnRowDataBoundProductos(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(productos, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click para seleccionar el producto.";
        }
    }

    protected void ObtenerProductoSeleccionado(object sender, GridViewCommandEventArgs e) {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.productos.Rows[index];
        string id = row.Cells[0].Text;
        ProductoEntity producto = new daProducto().ObtenerProductosPorId(Convert.ToInt32(id));
        txtId.Text = producto.IdProducto.ToString();
        ddCategoria.SelectedValue = new daCategoria().ObtenerCategoria(producto.IdCategoria).Nombre;
        txtDescripcion.Text = producto.Descripcion;
        txtNombre.Text = producto.Nombre;
        txtImagen.Text = producto.Imagen;
        txtIva.Text = producto.Iva.ToString();
        txtMedida.Text = producto.Medida;
        txtModelo.Text = producto.Modelo;
        txtPeso.Text = producto.Peso.ToString();
        txtPrecio.Text = producto.Precio.ToString();
        txtStock.Text = producto.Stock.ToString();
    }

    protected void btnAlta_Click(object sender, EventArgs e) {
        ProductoEntity producto = new ProductoEntity();
        producto.IdCategoria = new daCategoria().ObtenerCategoriaNombre(ddCategoria.Text).IdCategoria;
        producto.Descripcion = txtDescripcion.Text;
        producto.Nombre = txtNombre.Text;
        producto.Imagen = txtImagen.Text;
        producto.Iva = Convert.ToDouble(txtIva.Text);
        producto.Medida = txtMedida.Text;
        producto.Modelo = txtModelo.Text;
        producto.Peso = Convert.ToInt32(txtPeso.Text);
        producto.Precio = Convert.ToDouble(txtPrecio.Text);
        producto.Stock = Convert.ToInt32(txtStock.Text);
        obProducto.CrearProducto(producto);
        Response.Redirect("AbmProductos.aspx");
    }

    protected void btnBaja_Click(object sender, EventArgs e) {
        ProductoEntity producto = new ProductoEntity();
        producto.IdProducto = Convert.ToInt32(txtId.Text);
        obProducto.EliminarProducto(producto);
        Response.Redirect("AbmProductos.aspx");
    }

    protected void btnEdit_Click(object sender, EventArgs e) {
        ProductoEntity producto = new ProductoEntity();
        producto.IdCategoria = new daCategoria().ObtenerCategoriaNombre(ddCategoria.Text).IdCategoria;
        producto.Descripcion = txtDescripcion.Text;
        producto.Nombre = txtNombre.Text;
        producto.Imagen = txtImagen.Text;
        producto.Iva = Convert.ToDouble(txtIva.Text);
        producto.Medida = txtMedida.Text;
        producto.Modelo = txtModelo.Text;
        producto.Peso = Convert.ToInt32(txtPeso.Text);
        producto.Precio = Convert.ToDouble(txtPrecio.Text);
        producto.Stock = Convert.ToInt32(txtStock.Text);
        obProducto.ActualizarProducto(producto);
        Response.Redirect("AbmProductos.aspx");
    }
}