using System;
using ProyectoTallerBussines;
using ProyectoTallerEntity;
using ProyectoTallerData;

public partial class Login : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void btnLogin_Click(object sender, EventArgs e) {
        UsuarioEntity usuario = obUsuario.ValidarLogin(txtUser.Text, txtPass.Text);

        if (usuario != null) {
            AdministrativoEntity admin = obUsuario.esAdministrativo(usuario);
            if (admin.IdAdministrativo != 0) {
                this.Session["UserID"] = admin;
            } else {
                this.Session["UserID"] = usuario;
                this.Session["PedID"] = obPedido.ObtenerCarritoUsuario(usuario.IdUsuario);
            }

            Response.Redirect("Principal.aspx");
        } else {
            Response.Write("<script language='JavaScript'>alert('Usuario o contraseña invalido')</script>");
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e) {
        Response.Redirect("Registrar.aspx");
    }
}