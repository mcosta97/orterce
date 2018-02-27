using ProyectoTallerBussines;
using ProyectoTallerData;
using ProyectoTallerEntity;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AbmUsuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) {
        try {
            if (((AdministrativoEntity)this.Session["UserID"]) != null && ((AdministrativoEntity)this.Session["UserID"]).Acceso == 2) {
                MostrarAdministrativos();
                MostrarClientes();
            } else {
                Response.Redirect("Login.aspx");
            }
        } catch(InvalidCastException ic) {
            Response.Redirect("Principal.aspx");
        }
    }

    protected void btnAlta_Click(object sender, EventArgs e) {
        if(txtAdic.Text.Length != 1) {
            ClienteEntity user = new ClienteEntity();
            user.Usuario = txtUsuario.Text;
            user.Clave = txtClave.Text;
            user.Nombre = txtNombre.Text;
            user.Apellido = txtApellido.Text;
            user.Mail = txtMail.Text;
            user.Dni = txtAdic.Text;
            obCliente.CrearCliente(user);
        } else {
            AdministrativoEntity admin = new AdministrativoEntity();
            admin.Usuario = txtUsuario.Text;
            admin.Clave = txtClave.Text;
            admin.Nombre = txtNombre.Text;
            admin.Apellido = txtApellido.Text;
            admin.Mail = txtMail.Text;
            admin.Acceso = Convert.ToInt32(txtAdic.Text);
            obAdministrativo.CrearAdministrativo(admin);
        }
        Response.Redirect("AbmUsuarios.aspx");
    }

    protected void btnBaja_Click(object sender, EventArgs e) {
        if(txtAdic.Text.Length != 1) {
            ClienteEntity cliente = new ClienteEntity();
            cliente.IdCliente = Convert.ToInt32(txtIdE.Text);
            cliente.IdUsuario = Convert.ToInt32(txtIdU.Text);
            obCliente.EliminarCliente(cliente);
        } else {
            AdministrativoEntity admin = new AdministrativoEntity();
            admin.IdAdministrativo = Convert.ToInt32(txtIdE.Text);
            admin.IdUsuario = Convert.ToInt32(txtIdU.Text);
            obAdministrativo.EliminarAdministrativo(admin);
        }
        Response.Redirect("AbmUsuarios.aspx");
    }

    protected void btnEdit_Click(object sender, EventArgs e) {
        if(txtAdic.Text.Length != 1) {
            ClienteEntity cliente = new ClienteEntity();
            cliente.IdCliente = Convert.ToInt32(txtIdE.Text);
            cliente.IdUsuario = Convert.ToInt32(txtIdU.Text);
            cliente.Dni = txtAdic.Text;
            cliente.Nombre = txtNombre.Text;
            cliente.Apellido = txtApellido.Text;
            cliente.Usuario = txtUsuario.Text;
            cliente.Clave = txtClave.Text;
            cliente.Mail = txtMail.Text;
            obCliente.ActualizarCliente(cliente);
        } else {
            AdministrativoEntity admin = new AdministrativoEntity();
            admin.IdAdministrativo = Convert.ToInt32(txtIdE.Text);
            admin.IdUsuario = Convert.ToInt32(txtIdU.Text);
            admin.Acceso = Convert.ToInt32(txtAdic.Text);
            admin.Nombre = txtNombre.Text;
            admin.Apellido = txtApellido.Text;
            admin.Usuario = txtUsuario.Text;
            admin.Clave = txtClave.Text;
            admin.Mail = txtMail.Text;
            daAdministrativo da = new daAdministrativo();
            da.Actualizar(admin);
        }
        Response.Redirect("AbmUsuarios.aspx");
    }

    protected void MostrarClientes() {
        clientes.DataSource = new daCliente().ObtenerClientesTabla();
        clientes.DataBind();
    }

    protected void MostrarAdministrativos() {
        administrativos.DataSource = new daAdministrativo().ObtenerAdministrativosTabla();
        administrativos.DataBind();
    }

    protected void OnRowDataBoundClientes(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(clientes, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click para seleccionar el cliente.";
        }
    }

    protected void OnRowDataBoundAdmin(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(administrativos, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click para seleccionar el administrativo.";
        }
    }

    protected void ObtenerClienteSeleccionado(object sender, GridViewCommandEventArgs e) {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.clientes.Rows[index];
        string id = row.Cells[0].Text;
        ClienteEntity cliente = new daCliente().ObtenerCliente(id);
        txtIdU.Text = cliente.IdUsuario.ToString();
        txtIdE.Text = cliente.IdCliente.ToString();
        txtUsuario.Text = cliente.Usuario;
        txtClave.Text = cliente.Clave;
        txtNombre.Text = cliente.Nombre;
        txtApellido.Text = cliente.Apellido;
        txtMail.Text = cliente.Mail;
        txtAdic.Text = cliente.Dni;
    }

    protected void ObtenerAdminSeleccionado(object sender, GridViewCommandEventArgs e) {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.administrativos.Rows[index];
        string id = row.Cells[0].Text;
        AdministrativoEntity admin = new daAdministrativo().ObtenerAdministrativo(id);
        txtIdU.Text = admin.IdUsuario.ToString();
        txtIdE.Text = admin.IdAdministrativo.ToString();
        txtUsuario.Text = admin.Usuario;
        txtClave.Text = admin.Clave;
        txtNombre.Text = admin.Nombre;
        txtApellido.Text = admin.Apellido;
        txtMail.Text = admin.Mail;
        txtAdic.Text = admin.Acceso.ToString();
    }
}