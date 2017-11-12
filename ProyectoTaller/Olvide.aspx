<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Olvide.aspx.cs" Inherits="Olvide" %>

<asp:Content ContentPlaceHolderID="generico" runat="Server">
    <div class="form-group contenedor-login">
        <img src="/logo.png">
        <br>
        <br>
        <form runat="server">
            <asp:Label runat="server">Ingrese su usuario y recibira su nueva clave al correo</asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtUser"></asp:TextBox>
            <br>
            <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Recuperar" ID="btnRecuperar" OnClick="btnRecuperar_Click"></asp:Button>
        </form>
    </div>
</asp:Content>
