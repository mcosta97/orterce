<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Registrar.aspx.cs" Inherits="Registrar" %>

<asp:Content ContentPlaceHolderID="generico" runat="Server">
    <div class="contenedor-login">
        <img src="/logo.png">
        <br>
        <br>
        <form runat="server" class="form-group form-group-sm">
            <asp:Label runat="server">Usuario</asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtUser"></asp:TextBox>
            <br>
            <asp:Label runat="server">Dni</asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtDni"></asp:TextBox>
            <br>
            <asp:Label runat="server">Clave</asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="txtPass"></asp:TextBox>
            <br>
            <asp:Label runat="server">Reingresar clave</asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="txtRePass"></asp:TextBox>
            <br>
            <asp:Label runat="server">Nombre</asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre"></asp:TextBox>
            <br>
            <asp:Label runat="server">Apellido</asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido"></asp:TextBox>
            <br>
            <asp:Label runat="server">Mail</asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtMail"></asp:TextBox>
            <br>
            <asp:Label runat="server">Reingresar mail</asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtReMail"></asp:TextBox>
            <br>

            <asp:Button runat="server" CssClass="btn btn-block btn-primary" Text="Registrar" ID="btnRegistrar" OnClick="btnRegister_Click"></asp:Button>
            <br>
        </form>
    </div>
</asp:Content>