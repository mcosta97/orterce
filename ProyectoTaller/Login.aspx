<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="ctLogin" ContentPlaceHolderID="titulo" Runat="Server">
    Usuario:<br>
    <input type="text" name="usuario">
    <br>
    Clave:<br>
    <input type="password" name="clave">
    <br><br>
    <input type="submit" value="Ingresar">
</asp:Content>