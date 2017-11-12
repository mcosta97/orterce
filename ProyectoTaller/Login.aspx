<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ContentPlaceHolderID="generico" runat="Server">
    <div class="form-group contenedor-login">
        <img src="/logo.png">
        <br>
        <br>
        <form runat="server">
            <asp:Label runat="server">Usuario</asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtUser"></asp:TextBox>
            <br>
            <asp:Label runat="server">Clave</asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="txtPass"></asp:TextBox>
            <br>
            <asp:Button runat="server" CssClass="btn btn-block btn-success" Text="Ingresar" ID="btnIngresar" OnClick="btnLogin_Click"></asp:Button>
            <br>
            <div class="container">
                <asp:Button runat="server" CssClass="btn btn-link" Text="No tengo cuenta" ID="btnRegistrar" OnClick="btnRegister_Click"></asp:Button>
                |
                <asp:Button runat="server" CssClass="btn btn-link" Text="Olvide mi clave" ID="btnOlvide" PostBackUrl="~/Olvide.aspx"></asp:Button>
            </div>
        </form>
    </div>
</asp:Content>
