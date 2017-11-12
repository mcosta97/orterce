using ProyectoTallerBussines;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Registrar : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }

    private bool ValidarCampos() {
        bool valido = true;
    
        if (!txtMail.Text.Equals(txtReMail.Text)) {
            valido = false;
            txtReMail.Focus();
            Response.Write("<script language='JavaScript'>alert('Los mails no coinciden.')</script>");
        }

        if (!txtPass.Text.Equals(txtRePass.Text)) {
            valido = false;
            txtPass.Focus();
            Response.Write("<script language='JavaScript'>alert('Las claves no coinciden.')</script>");
        }

        if (txtPass.Text.Length < 4) {
            valido = false;
            txtPass.Focus();
            Response.Write("<script language='JavaScript'>alert('La clave tiene que tener un minimo de 4 caracteres.')</script>");
        }

        if (txtUser.Text.Equals("")) {
            valido = false;
            txtUser.Focus();
        }

        if (txtNombre.Text.Equals("")) {
            valido = false;
            txtNombre.Focus();
        }

        if (txtApellido.Text.Equals("")) {
            valido = false;
            txtApellido.Focus();
        }

        if (txtDni.Text.Equals("")) {
            valido = false;
            txtDni.Focus();
        }
        return valido;
    }

    protected void btnRegister_Click(object sender, EventArgs e) {
        ClienteEntity cliente = new ClienteEntity();
        if (ValidarCampos()) {
            cliente.Nombre = txtNombre.Text;
            cliente.Apellido = txtApellido.Text;
            cliente.Usuario = txtUser.Text;
            cliente.Clave = txtPass.Text;
            cliente.Mail = txtMail.Text;
            cliente.Dni = txtDni.Text;
            if (obCliente.ValidarCliente(cliente)) {
                obCliente.CrearCliente(cliente);
                Response.Redirect("Login.aspx");
            } else {
                Response.Write("<script language='JavaScript'>alert('El usuario ya existe.')</script>");
            }
        }
    }
}