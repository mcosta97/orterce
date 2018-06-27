using ProyectoTallerBussines;
using ProyectoTallerData;
using ProyectoTallerDataSql;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PerfilUsuario : Page {
    protected void Page_Load(object sender, EventArgs e) {
        if(this.Session["UserID"] == null) {
            Response.Redirect("Login.aspx");
        }

        if(!Page.IsPostBack) {
            List<DireccionEntity> direcciones;
            List<TelefonoEntity> phones;
            UsuarioEntity usuario = (UsuarioEntity) this.Session["UserID"];
            txtNombre.Text = usuario.Nombre;
            txtApellido.Text = usuario.Apellido;
            CargarProvincias();

            if((direcciones = obUsuario.VerificarDomicilios(usuario)) == null) {
                domicilios.Visible = true;
                newdom.Visible = false;
                MostrarDomicilios(direcciones);
            } else {
                domicilios.Visible = false;
                newdom.Visible = true;
            }

            if((phones = obUsuario.VerificarTelefonos(usuario)) == null) {
                telefonos.Visible = true;
                newtel.Visible = false;
                MostrarTelefonos(phones);
            } else {
                telefonos.Visible = false;
                newtel.Visible = true;
            }
        }
    }

    private void MostrarTelefonos(List<TelefonoEntity> phones) {
        tel.DataSource = new daTelefono().TelefonosTabla(phones);
        tel.DataBind();
    }

    private void MostrarDomicilios(List<DireccionEntity> direcciones) {
        dom.DataSource = new daDireccion().DireccionesTabla(direcciones);
        dom.DataBind();
    }

    private void CargarProvincias() {
        ddProvincia.Items.Add("(Seleccionar)");
        foreach(ProvinciaEntity provincia in new daProvincia().ObtenerProvincias()) {
            ddProvincia.Items.Add(provincia.Nombre);
        }
    }

    private void CargarLocalidades() {
        if(!ddProvincia.SelectedItem.Text.Equals("")) {
            ddLocalidad.Items.Clear();
            ddLocalidad.Items.Add("(Seleccionar)");
            foreach(LocalidadEntity localidad in new daLocalidad().ObtenerLocalidades(ddProvincia.SelectedItem.Text)) {
                ddLocalidad.Items.Add(localidad.Nombre);
            }
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e) {
        UsuarioEntity usuario = new UsuarioEntity();
        usuario = new daUsuario().BuscarPorClavePrimaria(((UsuarioEntity) Session["UserID"]).IdUsuario.ToString());

        if(!txtAnterior.Text.Equals("")) {
            if(txtAnterior.Text.Equals(usuario.Clave)) {
                if(txtNueva.Text.Equals(txtReNueva.Text)) {
                    usuario.Clave = txtNueva.Text;
                } else {
                    Response.Write("<script language='JavaScript'>alert('Las claves no coinciden.')</script>");
                }
            } else {
                Response.Write("<script language='JavaScript'>alert('La clave anterior no es valida.')</script>");
            }
        }

        usuario.Nombre = txtNombre.Text;
        usuario.Apellido = txtApellido.Text;

        obUsuario.ActualizarUsuario(usuario);
        this.Session["UserID"] = usuario;
        VerificarAdicionales();
        Response.Write("<script language='JavaScript'>alert('Perfil guardado!')</script>");
    }

    protected void VerificarAdicionales() {
        if(!txtTelefono.Text.Equals("")) {
            obUsuario.AgregarTelefono(txtTelefono.Text, ((UsuarioEntity)Session["UserID"]).IdUsuario);
        }

        if(!txtDireccion.Text.Equals("")) {
            DireccionEntity dire = new DireccionEntity();
            if(ValidarDireccion()) {
                dire.Altura = Convert.ToInt32(txtAltura.Text);
                dire.Direccion = txtDireccion.Text;
                dire.Piso = txtPiso.Text;
                dire.Dpto = txtDpto.Text;
                LocalidadEntity localidad = new daLocalidad().ObtenerLocalidadPorNombre(ddLocalidad.Text);
                dire.Localidad = localidad;
                dire.Provincia = localidad.Provincia;
            }
            obUsuario.AgregarDireccion(dire, ((UsuarioEntity) Session["UserID"]).IdUsuario);
        }
    }

    private bool ValidarDireccion() {
        bool valida = true;

        if(!ddLocalidad.SelectedItem.ToString().Equals("(Seleccionar)")) {
            valida = false;
        }

        if(!ddProvincia.SelectedItem.ToString().Equals("(Seleccionar)")) {
            valida = false;
        }

        if(txtAltura.Text.Length == 0) {
            valida = false;
        } else {
            if(!daUtils.IsNumeric(txtAltura.Text)) {
                valida = false;
            }
        }
        return valida;
    }

    protected void ddProvincia_SelectedIndexChanged(object sender, EventArgs e) {
        CargarLocalidades();
    }
}