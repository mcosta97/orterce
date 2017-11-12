using ProyectoTallerBussines;
using ProyectoTallerData;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Olvide : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void btnRecuperar_Click(object sender, EventArgs e) {
        UsuarioEntity usuario = obUsuario.RecuperarClave(txtUser.Text);
        if(usuario != null) {
            daUtils.EnviarMail("[Orterce] Recuperacion de clave", "Mediante este correo le informamos que su clave para ingresar es la siguiente: " + usuario.Clave, usuario.Mail);
        } else {
            Response.Write("<script language='JavaScript'>alert('El usuario ingresado no existe.')</script>");
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e) {
        Response.Redirect("Registrar.aspx");
    }
}